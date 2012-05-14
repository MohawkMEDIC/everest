using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format;
using System.Diagnostics;
using MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Annotations;
using System.Text.RegularExpressions;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta
{
    /// <summary>
    /// A delta engine that can apply a delta set 
    /// against a class repository
    /// </summary>
    public class DeltaEngine
    {

        /// <summary>
        /// Gets the delta set
        /// </summary>
        private DeltaSet m_deltaSet;

        /// <summary>
        /// Creates a new delta engine with the specified
        /// delta set
        /// </summary>
        public DeltaEngine(DeltaSet deltaSet)
        {
            this.m_deltaSet = deltaSet;
            ValidateDeltaSet();
        }

        /// <summary>
        /// Validate the delta set for processing
        /// </summary>
        private void ValidateDeltaSet()
        {
            if (this.m_deltaSet.Realm == null ||
                this.m_deltaSet.Realm.Code == null)
                throw new InvalidOperationException("Delta set must provide a realm code");
            else if (this.m_deltaSet.MetaData == null ||
                this.m_deltaSet.MetaData.Description == null)
                throw new InvalidOperationException("Delta set must provide meta data and a description");
            else if (this.m_deltaSet.EntryPoint == null)
                throw new InvalidOperationException("Delta set must have at least one entry point");
            foreach (var ep in this.m_deltaSet.EntryPoint)
            {
                // Validate deltas
                foreach (var delta in ep.Delta)
                    ValidateDelta(delta);
            }
        }

        /// <summary>
        /// Validate a delta
        /// </summary>
        private void ValidateDelta(DeltaData delta)
        {

            if (String.IsNullOrEmpty(delta.Name))
                throw new InvalidOperationException("Delta set Delta must provide a name");
            foreach (var constraint in delta.Constraint)
                ValidateConstraint(constraint);
            if (delta is ClassDeltaData)
            {
                if((delta as ClassDeltaData).Deltas != null)
                    foreach (var subDelta in (delta as ClassDeltaData).Deltas)
                        ValidateDelta(subDelta.Value);
            }
        }

        /// <summary>
        /// Validate constraint
        /// </summary>
        private void ValidateConstraint(Constraint constraint)
        {
            if (constraint.Value == null)
                throw new InvalidOperationException("Constraint has no value");

            object expected = null;
            switch (constraint.Type)
            {
                case ConstraintDeltaType.BusinessName:
                case ConstraintDeltaType.SubstituteCmet:
                case ConstraintDeltaType.Datatype:
                case ConstraintDeltaType.DefaultValue:
                case ConstraintDeltaType.Fixed:
                case ConstraintDeltaType.Conformance:
                case ConstraintDeltaType.VocabularyStrength:
                case ConstraintDeltaType.UpdateModeDefault:
                    expected = constraint.Value as ConstraintValue<String>;
                    break;
                case ConstraintDeltaType.UpdateModeValue:
                    expected = constraint.Value as ListConstraintValue;
                    break;
                case ConstraintDeltaType.Length:
                    expected = constraint.Value as ConstraintValue<Int32>;
                    break;
                case ConstraintDeltaType.VocabularyBinding:
                    expected = constraint.Value as VocabularyConstraintValue;
                    break;
                case ConstraintDeltaType.Cardinality:
                    expected = constraint.Value as CardinalityConstraintValue;
                    break;
                case ConstraintDeltaType.Remove:
                    expected = constraint.Value as RemoveConstraintValue;
                    break;
                case ConstraintDeltaType.Annotation:
                    expected = constraint.Value as AnnotationConstraintValue;
                    break;
                default:
                    throw new InvalidOperationException(String.Format("Don't understand '{0}' constraint", constraint.Type));

            }
            if (expected == null)
            {
                throw new InvalidOperationException(String.Format("Constraint value of type '{0}' is not valid for constraint type of '{1}'",
                    constraint.Value.GetType().Name, constraint.Type));
            }
        }

        /// <summary>
        /// Apply the delta set against the class repository
        /// </summary>
        public void Apply(ClassRepository classRepository)
        {

            // Notify user
            Trace.WriteLine(String.Format("Delta: Adding supported annotation for realm '{0}'...", this.m_deltaSet.Realm.Code), "debug");
            // Mark all features as supported
            foreach (var itm in classRepository)
                AddSupportedAnnotation(itm.Value);

            // Apply the class deltas and models
            foreach (var ep in this.m_deltaSet.EntryPoint)
            {
                Feature subSystem = null;

                // Try to get the sub-system
                if (!classRepository.TryGetValue(ep.Id, out subSystem))
                {
                    Trace.WriteLine(String.Format("Delta: Can't find '{0}' in the repository, skipping...", ep.Id), "error");
                    continue;
                }

                // apply class delta
                foreach (var delta in ep.Delta)
                    ApplyDelta(subSystem as SubSystem, delta);
            }
        }

        /// <summary>
        /// Add supported annotation
        /// </summary>
        private void AddSupportedAnnotation(Feature featureData)
        {
            if(!featureData.Annotations.Exists(o=>o is SupportedConstraintAnnotation && (o as SupportedConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
                featureData.Annotations.Add(new SupportedConstraintAnnotation()
                {
                    RealmCode = this.m_deltaSet.Realm.Code
                });

            if (featureData is Class)
                foreach (var dat in (featureData as Class).Content)
                    AddSupportedAnnotation(dat);
            else if (featureData is Choice)
                foreach (var dat in (featureData as Choice).Content)
                    AddSupportedAnnotation(dat);
        }

        /// <summary>
        /// Apply the delta against the sub-system
        /// </summary>
        private void ApplyDelta(SubSystem subSystem, ClassDeltaData delta)
        {

            Class corClass = subSystem.OwnedClasses.Find(o => o.Name == delta.Name) as Class;

            // Core class was not found
            if (corClass == null)
            {
                Trace.WriteLine(String.Format("Delta: Can't find '{0}' in sub-system '{1}', skipping...", delta.Name, subSystem.Name), "error");
                return;
            }
            else if (corClass.Annotations.Exists(o => o is CodeCombineAnnotation))
                throw new InvalidOperationException("It appears optimizations have already been performed on this set of classes. Will not continue...");
            
            // Let everyone know
            Trace.WriteLine(String.Format("Delta: Applying constraints for '{0}.{1}'", subSystem.Name, corClass.Name), "debug");
                        
            // Apply constraints
            foreach (var constraint in delta.Constraint)
                ApplyConstraint(corClass, constraint);

            // Apply sub-deltas
            if (delta.Deltas != null)
            {
                foreach (var deltaData in delta.Deltas)
                {
                    // Find the feature that is in the delta
                    Feature ftr = corClass.Content.Find(o => o.Name == deltaData.Value.Name);
                    if (ftr == null)
                    {
                        Trace.WriteLine(String.Format("Delta: Can't find property '{0}' in class '{1}.{2}', skipping...", deltaData.Value.Name,
                            subSystem.Name, corClass.Name), "error");
                        continue;
                    }
                    ApplyDelta(ftr as ClassContent, deltaData.Value);
                }
            }
        }

        /// <summary>
        /// Apply delta against a property
        /// </summary>
        private void ApplyDelta(ClassContent property, RelationshipDeltaData delta)
        {
            Trace.WriteLine(string.Format("Delta: Applying deltas for '{0}.{1}'...", property.Container.Name, property.Name), "debug");

            // Apply constraints
            foreach (var constraint in delta.Constraint)
                ApplyConstraint(property, constraint);
            
        }

        /// <summary>
        /// Parses a type reference string back into COR
        /// </summary>
        /// <remarks>
        /// This method shouldn't have to exist, but it does because 
        /// the /\-sets lose some fidelity of data between MIF and 
        /// themselves.
        /// </remarks>
        private TypeReference ParseTypeReference(string typeString)
        {
            // A stack of currently "being processed" references
            // The concept is simple:
            //  Termination of a token with "<" pushes the current type into the stack
            //  Termination of a token with "," peeks the stack and adds a generic parameter
            //  Termination of a token with ">" peeks the stack and adds a generic parameter and pops the type
            Stack<TypeReference> typeReferences = new Stack<TypeReference>();
            TypeReference retVal = null;
            Regex matchRegex = new Regex(@"^([A-Za-z\.\s]+)([<,>])?(.*)$", RegexOptions.Multiline);
            while (matchRegex.IsMatch(typeString))
            {
                // Get matches for the current token
                var match = matchRegex.Match(typeString);
                String[] typeTokens = match.Groups[1].Value.Split('.');
                // Create the type reference
                TypeReference tr = new TypeReference()
                {
                    Name = typeTokens[0].Trim(),
                    Flavor = typeTokens.Length == 1 ? null : typeTokens[1].Trim(),
                    GenericSupplier = new List<TypeReference>()
                };

                if (match.Groups[2].Value == "<") // We're going into another scope
                    typeReferences.Push(tr);
                else if (match.Groups[2].Value == ",") // We add to current type reference to the current item in the stack
                {
                    if (!String.IsNullOrEmpty(tr.Name))
                        typeReferences.Peek().GenericSupplier.Add(tr);
                    else
                        typeReferences.Peek().GenericSupplier.Add(retVal);
                }
                else if (match.Groups[2].Value == ">") // We add to the current and pop the stack
                {
                    if (!String.IsNullOrEmpty(tr.Name))
                        typeReferences.Peek().GenericSupplier.Add(tr);
                    else
                        typeReferences.Peek().GenericSupplier.Add(retVal);
                    tr = typeReferences.Pop();
                }

                retVal = tr;

                typeString = match.Groups[3].Value;
                if (typeString.StartsWith(",") || typeString.StartsWith(">"))
                    typeString = " " + typeString;
            }

            return retVal;
        }

        /// <summary>
        /// Create a conformance change annotation
        /// </summary>
        private void CreateConformanceChange(Feature feature, Constraint constraint)
        {
            #region Conformance Change
            // 1. Validate the constraint is against a property
            ClassContent pFeature = feature as ClassContent;
            if (pFeature == null)
            {
                Trace.WriteLine(String.Format("Delta: Conformance constraint cannot be applied against a '{1}' named '{0}'", feature.Name, feature.GetType().Name), "error");
                return;
            }

            // 2. Validate the conformance old value is in fact a match
            ConstraintValue<String> stConstraint = constraint.Value as ConstraintValue<string>;
            if (stConstraint == null || stConstraint.Original.ToLower() != pFeature.Conformance.ToString().ToLower())
            {
                Trace.WriteLine(String.Format("Delta: Conformance constraint cannot be applied because the original value of '{0}' does not match the actual original property conformance of '{1}'",
                    stConstraint.Original.ToLower(), pFeature.Conformance.ToString().ToLower()), "error");
                return;
            }

            // Map the SCREAMING CAPS to ScreamingCaps
            string friendlyValue = String.Format("{0}{1}", stConstraint.New[0], stConstraint.New.Substring(1).ToLower());
            if (friendlyValue == "Not_allowed")
                friendlyValue = "NotAllowed";
            // 3. Verify a constraint for the same jurisdication has not already been added
            if (feature.Annotations.Exists(o => o is ConformanceConstraintAnnotation && (o as ConformanceConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Conformance constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }

            feature.Annotations.Add(
                new ConformanceConstraintAnnotation()
                {
                    NewValue = (Property.ConformanceKind)Enum.Parse(typeof(Property.ConformanceKind), friendlyValue),
                    ChangeType = ChangeType.Edit,
                    RealmCode = this.m_deltaSet.Realm.Code,
                    RealmName = this.m_deltaSet.MetaData.Description
                }
            );
            #endregion
        }

        /// <summary>
        /// Create an annotation change delta
        /// </summary>
        private void CreateAnnotationChange(Feature feature, Constraint constraint)
        {
            #region Annotation Change
            AnnotationConstraintValue acv = constraint.Value as AnnotationConstraintValue;
            if (acv.ChangeType == ChangeType.Remove)
            {

                // Determine modification type
                var modType = AnnotationConstraintAnnotation.ModificationTargetType.Other;
                switch (acv.Type)
                {
                    case AnnotationType.Description:
                        modType = AnnotationConstraintAnnotation.ModificationTargetType.Description;
                        break;
                    case AnnotationType.UseageNotes:
                        modType = AnnotationConstraintAnnotation.ModificationTargetType.Usage;
                        break;
                    case AnnotationType.Rationale:
                        modType = AnnotationConstraintAnnotation.ModificationTargetType.Rationale;
                        break;
                }
                feature.Annotations.Add(new AnnotationConstraintAnnotation()
                {
                    AnnotationType = modType,
                    ChangeType = ChangeType.Remove,
                    RealmCode = this.m_deltaSet.Realm.Code
                });
            }
            else
                switch (acv.Type)
                {
                    case AnnotationType.UseageNotes:
                    case AnnotationType.Description:
                    case AnnotationType.Rationale:
                        // 1. Validate the current value is equal
                        if (feature.Documentation == null ||
                            acv.Type == AnnotationType.Description &&
                            (feature.Documentation.Description == null ||
                            feature.Documentation.Description.Count == 0 ||
                             !feature.Documentation.Description[0].Equals(acv.Original)) ||
                            acv.Type == AnnotationType.UseageNotes &&
                            (feature.Documentation.Usage == null ||
                            feature.Documentation.Usage.Count == 0 ||
                            !feature.Documentation.Usage[0].Equals(acv.Original)) ||
                            acv.Type == AnnotationType.Rationale &&
                            (feature.Documentation.Rationale == null ||
                            feature.Documentation.Rationale.Count == 0 ||
                            !feature.Documentation.Rationale[0].Equals(acv.Original)))
                            Trace.WriteLine(String.Format("Delta: Annotation constraint for '{0}' of type '{1}' does not have the same origial content, this may be an error!", feature.Name, acv.Type), "warn");

                        // 2. Add the annotation
                        feature.Annotations.Add(new AnnotationConstraintAnnotation()
                        {
                            AnnotationType = acv.Type == AnnotationType.Description ? AnnotationConstraintAnnotation.ModificationTargetType.Description : acv.Type == AnnotationType.Rationale ? AnnotationConstraintAnnotation.ModificationTargetType.Rationale : AnnotationConstraintAnnotation.ModificationTargetType.Usage,
                            ChangeType = acv.ChangeType,
                            NewValue = acv.New,
                            RealmCode = this.m_deltaSet.Realm.Code,
                            RealmName = this.m_deltaSet.MetaData.Description
                        });
                        break;
                    case AnnotationType.Constraint:
                        // 1. Create a new titled documentation
                        feature.Annotations.Add(new AnnotationConstraintAnnotation()
                        {
                            AnnotationType = AnnotationConstraintAnnotation.ModificationTargetType.Constraint,
                            ChangeType = acv.ChangeType,
                            NewValue = acv.New,
                            RealmCode = this.m_deltaSet.Realm.Code,
                            RealmName = this.m_deltaSet.MetaData.Description
                        });
                        break;
                    case AnnotationType.DesignComments:
                    case AnnotationType.Mapping:
                    case AnnotationType.OpenIssue:
                    case AnnotationType.OtherNotes:
                        // 1. Create a new titled documentation
                        feature.Annotations.Add(new AnnotationConstraintAnnotation()
                        {
                            AnnotationType = AnnotationConstraintAnnotation.ModificationTargetType.Other,
                            ChangeType = acv.ChangeType,
                            NewValue = acv.New,
                            RealmCode = this.m_deltaSet.Realm.Code,
                            RealmName = this.m_deltaSet.MetaData.Description
                        });
                        break;
                }
            #endregion
        }

        /// <summary>
        /// Create a remove change delta
        /// </summary>
        private void CreateRemoveChange(Feature feature, Constraint constraint)
        {
            #region Remove Change
            // 1. Find the feature that is to be removed
            Feature removeFeature = null;
            ClassContent pFeature = feature as ClassContent;
            RemoveConstraintValue rcv = constraint.Value as RemoveConstraintValue;
            if (rcv.RelationshipName == feature.Name)
                removeFeature = feature;
            else if (!String.IsNullOrEmpty(rcv.OwnedEntryPoint) && !String.IsNullOrEmpty(rcv.ClassName))
            {
                if (feature is Property)
                {
                    var ssContainer = ((feature as Property).Container as Class).ContainerPackage;
                    if (ssContainer != null && ssContainer.Name.Equals(rcv.OwnedEntryPoint))
                        removeFeature = ssContainer.OwnedClasses.Find(o => o.Name.Equals(rcv.ClassName));
                }
            }

            // 2. Validate the feature
            if (removeFeature == null)
            {
                Trace.WriteLine(String.Format("Delta: Cannot apply remove constraint on '{0}.{1}{2}' as the path was not found...",
                    rcv.OwnedEntryPoint, rcv.ClassName, rcv.RelationshipName == null ? "" : "." + rcv.RelationshipName), "error");
                return;
            }

            // 3. Apply the remove constraint
            if (removeFeature.Annotations.Exists(o => o is RemoveConstraintAnnotation && (o as RemoveConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Conformance constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }
            removeFeature.Annotations.Add(
                new RemoveConstraintAnnotation()
                {
                    ChangeType = ChangeType.Remove,
                    RealmCode = this.m_deltaSet.Realm.Code,
                    RealmName = this.m_deltaSet.MetaData.Description
                }
            );

            // 4. Remove the supported constraint
            removeFeature.Annotations.RemoveAll(o => o is SupportedConstraintAnnotation && (o as SupportedConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code);

            #endregion
        }

        /// <summary>
        /// Create a change that modifies the business name
        /// </summary>
        private void CreateBusinessNameChange(Feature feature, Constraint constraint)
        {
            #region Business Name Change
            // 1. Validate constraint if possible
            BusinessNameConstraintAnnotation bnc = new BusinessNameConstraintAnnotation();
            var stConstraint = constraint.Value as ConstraintValue<String>;
            if (stConstraint.Original != null && !stConstraint.Original.Equals(feature.BusinessName))
                Trace.WriteLine(String.Format("Delta: Business name constraint original text does not match actual business name on feature '{0}'", feature.Name), "warn");

            // 2. Check for duplicates
            if (feature.Annotations.Exists(o => o is BusinessNameConstraintAnnotation && (o as BusinessNameConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Business name constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }

            // 3. Append constraint
            feature.Annotations.Add(new BusinessNameConstraintAnnotation()
            {
                RealmCode = this.m_deltaSet.Realm.Code,
                NewValue = stConstraint.New,
                ChangeType = ChangeType.Edit,
                RealmName = this.m_deltaSet.MetaData.Description
            });
            #endregion
        }
        
        /// <summary>
        /// Create a cardinality change
        /// </summary>
        private void CreateCardinalityChange(Feature feature, Constraint constraint)
        {
            #region Cardinality Change
            // 1. Validate constraint & target
            CardinalityConstraintValue ccv = constraint.Value as CardinalityConstraintValue;
            ClassContent ccFeature = feature as ClassContent;
            if (ccFeature == null)
            {
                Trace.WriteLine(String.Format("Delta: Cannot apply cardinality constraint against a feature of type '{0}', skipping...", feature.GetType().Name), "error");
                return;
            }
            // 2. Validate existing data
            if ((ccv.OriginalMaxValue.HasValue ? ccv.OriginalMaxValue.ToString() != ccFeature.MaxOccurs : ccFeature.MaxOccurs != "*") ||
                (ccv.OriginalMinValue.HasValue ? ccv.OriginalMinValue.ToString() != ccFeature.MinOccurs : ccFeature.MaxOccurs != null))
                Trace.WriteLine(String.Format("Delta: Applying cardinality constraint on '{0}' even though original Min/Max occurs do not match!", feature.Name), "warn");
            // 3. Validate that the cardinality doesn't already exist
            if (ccFeature.Annotations.Exists(o => o is CardinalityConstraintAnnotation && (o as CardinalityConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Cardinality constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }
            // 4. Append the annotation
            ccFeature.Annotations.Add(new CardinalityConstraintAnnotation()
            {
                ChangeType = ChangeType.Edit,
                RealmCode = this.m_deltaSet.Realm.Code,
                MaxOccurs = ccv.NewMaxValue.HasValue ? ccv.NewMaxValue.Value.ToString() : "*",
                MinOccurs = ccv.NewMinValue.HasValue ? ccv.NewMinValue.Value.ToString() : null,
                RealmName = this.m_deltaSet.MetaData.Description
            });
            #endregion
        }

        /// <summary>
        /// Create a data type change
        /// </summary>
        private void CreateDatatypeChange(Feature feature, Constraint constraint)
        {
            #region Data Type Change
            // 1. Validate constraint & target
            var stConstriant = constraint.Value as ConstraintValue<String>;
            Property ccFeature = feature as Property;
            if (ccFeature == null)
            {
                Trace.WriteLine(String.Format("Delta: Cannot apply data type change constraint against a feature of type '{0}', skipping...", feature.GetType().Name), "error");
                return;
            }
            TypeReference trNew = this.ParseTypeReference(stConstriant.New),
                trOld = this.ParseTypeReference(stConstriant.Original);

            // 2. Validate current data type
            if (trNew == null || trOld == null)
            {
                Trace.WriteLine(String.Format("Delta: Could not process '{0}' or '{1}', skipping...", stConstriant.Original, stConstriant.New), "error");
                return;
            }
            if (!TypeReferenceEqual(ccFeature.Type, trOld))
                Trace.WriteLine(String.Format("Delta: Current type of '{0}' is not '{1}' as specified in delta, rather it is '{2}', will continue processing anyways...", feature.Name,
                    trOld, ccFeature.Type), "warn");
            // 3. Validate that the cardinality doesn't already exist
            if (ccFeature.Annotations.Exists(o => o is DatatypeChangeConstraintAnnotation && (o as DatatypeChangeConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Datatype constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }
            // 4. Append the annotation
            ccFeature.Annotations.Add(new DatatypeChangeConstraintAnnotation()
            {
                ChangeType = ChangeType.Edit,
                RealmCode = this.m_deltaSet.Realm.Code,
                NewValue = trNew,
                RealmName = this.m_deltaSet.MetaData.Description
            });
            #endregion
        }

        /// <summary>
        /// Create a change that modifies the fixed value
        /// </summary>
        private void CreateFixedValueChange(Feature feature, Constraint constraint)
        {
            #region Fixed Value
            // 1. Validate constraint & target
            var stConstriant = constraint.Value as ConstraintValue<String>;
            Property ccFeature = feature as Property;
            if (ccFeature == null)
            {
                Trace.WriteLine(String.Format("Delta: Cannot apply fixed value change constraint against a feature of type '{0}', skipping...", feature.GetType().Name), "error");
                return;
            }

            // 2. Validate current data type
            if (stConstriant.Original != null && !stConstriant.Original.Equals(ccFeature.FixedValue))
                Trace.WriteLine(String.Format("Delta: Current fixed value of '{0}' does not match original listed in delta of '{1}', will continue processing anyways...", ccFeature.FixedValue, stConstriant.Original), "warn");
            // 3. Validate that the annotation doesn't already exist
            if (ccFeature.Annotations.Exists(o => o is FixedValueConstraintAnnotation && (o as FixedValueConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Fixed value constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }
            // 4. Append the annotation
            ccFeature.Annotations.Add(new FixedValueConstraintAnnotation()
            {
                ChangeType = ChangeType.Edit,
                RealmCode = this.m_deltaSet.Realm.Code,
                NewValue = stConstriant.New,
                RealmName = this.m_deltaSet.MetaData.Description
            });
            #endregion
        }

        /// <summary>
        /// Create a default value change 
        /// </summary>
        private void CreateDefaultValueChange(Feature feature, Constraint constraint)
        {
            #region Default Value

            // Validate the constraint
            var dfConstraint = constraint.Value as ConstraintValue<String>;
            Property ccFeature = feature as Property;
            if (ccFeature == null)
            {
                Trace.WriteLine(String.Format("Delta: Cannot apply the default value constraint against a a feature of type '{0}', skipping...", feature.GetType().Name), "error");
                return;
            }

            // Validate the current default value
            if (!String.IsNullOrEmpty(dfConstraint.Original) && !dfConstraint.Original.Equals(ccFeature.DefaultValue))
                Trace.WriteLine(String.Format("Delta: Current default value of '{0}' does not match original listed in the delta of '{1}', will continue processing anyways...", ccFeature.DefaultValue, dfConstraint.Original), "warn");
            // Validate the annotation doesn't already exist
            if (ccFeature.Annotations.Exists(o => o is DefaultValueConstraintAnnotation && (o as DefaultValueConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Default value constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }

            // Add the annotation to the feature
            ccFeature.Annotations.Add(new DefaultValueConstraintAnnotation()
            {
                ChangeType = ChangeType.Edit,
                RealmCode = this.m_deltaSet.Realm.Code,
                NewValue = dfConstraint.New,
                RealmName = this.m_deltaSet.MetaData.Description
            });
            #endregion
        }

        /// <summary>
        /// Create a length value change
        /// </summary>
        // TODO: Find a delta-set example that uses this so we can test.
        // TODO: Can't determine if this changes maximum or minimum length, should bring this 
        //       up with CHI to see if there is a definition for what this attribute changes
        private void CreateLengthValueChange(Feature feature, Constraint constraint)
        {
            #region Length Value

            // Validate the constraint
            var lnConstraint = constraint.Value as ConstraintValue<Int32>;
            Property ccFeature = feature as Property;
            if (ccFeature == null)
            {
                Trace.WriteLine(String.Format("Delta: Cannot apply the length value constraint against a a feature of type '{0}', skipping...", feature.GetType().Name), "error");
                return;
            }

            // Validate the current default value
            if (!lnConstraint.Original.Equals(ccFeature.MaxLength))
                Trace.WriteLine(String.Format("Delta: Current length of '{0}' does not match original listed in the delta of '{1}', will continue processing anyways...", ccFeature.DefaultValue, lnConstraint.Original), "warn");
            // Validate the annotation doesn't already exist
            if (ccFeature.Annotations.Exists(o => o is LengthConstraintAnnotation && (o as LengthConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Length constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }

            // Add the annotation to the feature
            ccFeature.Annotations.Add(new LengthConstraintAnnotation()
            {
                ChangeType = ChangeType.Edit,
                RealmCode = this.m_deltaSet.Realm.Code,
                NewValue = lnConstraint.New,
                RealmName = this.m_deltaSet.MetaData.Description
            });
            #endregion
        }

        /// <summary>
        /// Create a vocbulary change strength
        /// </summary>
        private void CreateVocabularyStrengthChange(Feature feature, Constraint constraint)
        {
            #region Default Value

            // Validate the constraint
            var strConstraint = constraint.Value as ConstraintValue<String>;
            Property ccFeature = feature as Property;
            if (ccFeature == null)
            {
                Trace.WriteLine(String.Format("Delta: Cannot apply the vocabulary strength constraint against a a feature of type '{0}', skipping...", feature.GetType().Name), "error");
                return;
            }

            // Default
            Property.CodingStrengthKind newStrength = Property.CodingStrengthKind.CodedNoExtensions;
            // Parse the new value
            switch (strConstraint.New)
            {
                case "CWE":
                    newStrength = Property.CodingStrengthKind.CodedWithExtensions;
                    break;
                case "CNE":
                    break;
                default:
                    Trace.WriteLine(String.Format("Delta: Don't understand coding strength '{0}', ignoring...", strConstraint.New), "warn");
                    return;
            }

            // Validate the annotation doesn't already exist
            if (ccFeature.Annotations.Exists(o => o is SupplierStrengthConstraintAnnotation && (o as SupplierStrengthConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Supplier constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }

            // Add the annotation to the feature
            ccFeature.Annotations.Add(new SupplierStrengthConstraintAnnotation()
            {
                ChangeType = ChangeType.Edit,
                RealmCode = this.m_deltaSet.Realm.Code,
                NewValue = newStrength,
                RealmName = this.m_deltaSet.MetaData.Description
            });
            #endregion
        }

        /// <summary>
        /// Create a vocabulary domain change
        /// </summary>
        private void CreateVocabularyDomainChange(Feature feature, Constraint constraint)
        {

            #region Vocabulary Domain Change
            // 1. Validate the constraint
            var vocConstraint = constraint.Value as VocabularyConstraintValue;
            Property ccFeature = feature as Property;
            if (ccFeature == null)
            {
                Trace.WriteLine(String.Format("Delta: Cannot apply the vocabulary binding constraint against a a feature of type '{0}', skipping...", feature.GetType().Name), "error");
                return;
            }


            // 2. Validate the parameters
            Type originalType = typeof(Enumeration),
                newType = typeof(Enumeration);
            // Type map
            Dictionary<DomainSourceType, Type> typeMap = new Dictionary<DomainSourceType, Type>()
            {
                { DomainSourceType.CodeSystem, typeof(CodeSystem) },
                { DomainSourceType.ConceptDomain, typeof(ConceptDomain) },
                { DomainSourceType.ValueSet, typeof(ValueSet) }
            };

            // Map
            if (!typeMap.TryGetValue(vocConstraint.OriginalSourceType, out originalType))
            {
                Trace.WriteLine(String.Format("Delta: Don't understand the source type of the vocabulary constraint '{0}', skipping", vocConstraint.OriginalSourceType), "error");
                return;
            }
            else if (!typeMap.TryGetValue(vocConstraint.NewSourceType, out newType))
            {
                Trace.WriteLine(String.Format("Delta: Don't understand the destination type of the vocabulary constraint '{0}', skipping", vocConstraint.NewSourceType), "error");
                return;
            }

            // a. Validate original source parameters
            if (!ccFeature.SupplierDomain.Equals(vocConstraint.OriginalDomain) ||
                ccFeature.SupplierDomain != null && !ccFeature.SupplierDomain.GetType().Equals(originalType))
                Trace.WriteLine(String.Format("Delta: Current supplier domain '{2}:{0}' does not match original listed in the delta of '{3}:{1}', will continue processing anyways...", ccFeature.SupplierDomain, vocConstraint.OriginalDomain, ccFeature.SupplierDomain.GetType().Name, originalType.Name), "warn");

            // b. Validate the destination parameters
            
            // Try to get the destination vocabulary domain
            ClassRepository cr = ccFeature.MemberOf ?? ccFeature.Container.MemberOf;
            Feature targetEnum = null;
            if (cr != null && !cr.TryGetValue(vocConstraint.NewDomain, out targetEnum))
                Trace.WriteLine(String.Format("Delta: Could not find vocabulary '{0}' in repository! Cannot verify parameters", vocConstraint.NewDomain), "warn");
            else if (targetEnum != null && !targetEnum.GetType().Equals(newType))
                Trace.WriteLine(String.Format("Delta: Could not verify the destination vocabulary type. Actual type is '{0}:{1}', delta set describes '{2}:{3}'",
                    targetEnum.GetType().Name, targetEnum.Name, vocConstraint.NewSourceType, vocConstraint.NewDomain), "warn");
            
            // Identify the new type
            object newValue = (object)targetEnum ?? (object)vocConstraint.NewDomain;

            // Validate the annotation doesn't already exist
            if (ccFeature.Annotations.Exists(o => o is SupplierDomainConstraintAnnotation && (o as SupplierDomainConstraintAnnotation).RealmCode == this.m_deltaSet.Realm.Code))
            {
                Trace.WriteLine(String.Format("Delta: Supplier domain constraint has already been applied to '{0}'", feature.Name), "error");
                return;
            }

            // Add the annotation to the feature
            ccFeature.Annotations.Add(new SupplierDomainConstraintAnnotation()
            {
                ChangeType = ChangeType.Edit,
                RealmCode = this.m_deltaSet.Realm.Code,
                NewValue = newValue,
                RealmName = this.m_deltaSet.MetaData.Description
            });
            #endregion

        }

       
        /// <summary>
        /// Apply constraints for the specified feature
        /// </summary>
        private void ApplyConstraint(Feature feature, Constraint constraint)
        {
            // Determine the type of constraint
            switch (constraint.Type)
            {
                case ConstraintDeltaType.Conformance:
                    CreateConformanceChange(feature, constraint);
                    break;
                case ConstraintDeltaType.Annotation:
                    CreateAnnotationChange(feature, constraint);
                    break;
                case ConstraintDeltaType.Remove:
                    CreateRemoveChange(feature, constraint); 
                    break;
                case ConstraintDeltaType.BusinessName:
                    CreateBusinessNameChange(feature, constraint);
                    break;
                case ConstraintDeltaType.Cardinality:
                    CreateCardinalityChange(feature, constraint);
                    break;
                case ConstraintDeltaType.SubstituteCmet:
                case ConstraintDeltaType.Datatype:
                    CreateDatatypeChange(feature, constraint);
                    break;
                case ConstraintDeltaType.Fixed:
                    CreateFixedValueChange(feature, constraint);
                    break;
                case ConstraintDeltaType.DefaultValue:
                    CreateDefaultValueChange(feature, constraint);
                    break;
                case ConstraintDeltaType.Length:
                    CreateLengthValueChange(feature, constraint);
                    break;
                case ConstraintDeltaType.VocabularyStrength:
                    CreateVocabularyStrengthChange(feature, constraint);
                    break;
                case ConstraintDeltaType.VocabularyBinding:
                    CreateVocabularyDomainChange(feature, constraint);
                    break;
                case ConstraintDeltaType.UpdateModeDefault:
                case ConstraintDeltaType.UpdateModeValue:
                    {
                        Trace.WriteLine(String.Format("Delta: Update mode deltas are not supported by this delta engine"));
                        break;
                    }
                default:
                    {
                        Trace.WriteLine(String.Format("Delta: Don't understand constraint of type '{0}'", constraint.Type), "warn");
                        break;
                    }
            }

        }


        /// <summary>
        /// Determine equality of two type references
        /// </summary>
        private bool TypeReferenceEqual(TypeReference a, TypeReference b)
        {
            bool isEqual = a.Name == b.Name;
            isEqual &= a.Flavor == b.Flavor;
            isEqual &= a.GenericSupplier != null && b.GenericSupplier != null &&
                a.GenericSupplier.Count == b.GenericSupplier.Count ||
                a.GenericSupplier == null && b.GenericSupplier == null;
            for (int i = 0; isEqual && a.GenericSupplier != null && i < a.GenericSupplier.Count; i++)
                isEqual &= TypeReferenceEqual(a.GenericSupplier[i], b.GenericSupplier[i]);
            return isEqual;
        }
    }
}
