using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Serialized;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers
{
    /// <summary>
    /// Serialized static model compiler
    /// </summary>
    internal class SerializedStaticModelCompiler : StaticModelCompiler
    {

        /// <summary>
        /// Gets the type of package that this compiler can handle
        /// </summary>
        public override Type PackageType
        {
            get
            {
                return typeof(SerializedStaticModel);
            }
        }

        /// <summary>
        /// Set the package. All this compiler really does is converts the serialized static model
        /// to a static model
        /// </summary>
        public override object Package
        {
            set
            {
                SerializedStaticModel ssm = value as SerializedStaticModel;

                // Copy SSM to GSM
                GlobalStaticModel gsm = new GlobalStaticModel()
                {
                    Annotations = ssm.Annotations,
                    BusinessName = ssm.BusinessName,
                    ConformanceLevel = ssm.ConformanceLevel,
                    DefinitionalCode = ssm.DefinitionalCode,
                    DerivationClient = ssm.DerivationClient,
                    DerivedFrom = ssm.DerivedFrom,
                    HashCode = ssm.HashCode,
                    Header = ssm.Header,
                    ImportedCommonModelElementPackage = ssm.ImportedCommonModelElementPackage,
                    ImportedDatatypeModelPackage = ssm.ImportedDatatypeModelPackage,
                    ImportedStubPackage = ssm.ImportedStubPackage,
                    ImportedVocabularyModelPackage = ssm.ImportedVocabularyModelPackage,
                    IsAbstract = ssm.IsAbstract,
                    IsSerializable = ssm.IsSerializable,
                    MemberOfRepository = ssm.MemberOfRepository,
                    Name = ssm.Name,
                    OwnedSubjectAreaPackage = ssm.OwnedSubjectAreaPackage,
                    PackageKind = ssm.PackageKind,
                    PackageLocation = ssm.PackageLocation,
                    Replaces = ssm.Replaces,
                    RepresentationKind = ssm.RepresentationKind,
                    SchemaVersion = ssm.SchemaVersion,
                    SecondaryId = ssm.SecondaryId,
                    SortKey = ssm.SortKey,
                    Title = ssm.Title
                };

                gsm.OwnedClass = new List<ClassElement>(1);
                // Convert entry point class to a class
                gsm.OwnedClass.Add(new ClassElement() { Choice = CreateFlatModelElement(ssm.OwnedEntryPoint.SpecializedClass.Item, gsm) });

                base.Package = gsm;
            }
        }

        /// <summary>
        /// Create a class element
        /// </summary>
        /// <param name="modelElement"></param>
        /// <param name="gsm"></param>
        /// <returns></returns>
        private ModelElement CreateFlatModelElement(MohawkCollege.EHR.HL7v3.MIF.MIF20.ModelElement modelElement, GlobalStaticModel gsm)
        {
            if (modelElement is SerializedClass)
                return CreateFlatClass(modelElement as SerializedClass, gsm);
            else if (modelElement is SerializedCommonModelElementRef)
                return CreateFlatCMETRef(modelElement as SerializedCommonModelElementRef, gsm);
            else
                return modelElement;

        }

        /// <summary>
        /// Create a flat CMET Ref
        /// </summary>
        private ModelElement CreateFlatCMETRef(SerializedCommonModelElementRef serializedCommonModelElementRef, GlobalStaticModel gsm)
        {
            CommonModelElementRef retVal = new CommonModelElementRef()
            {
                Annotations = serializedCommonModelElementRef.Annotations,
                BusinessName = serializedCommonModelElementRef.BusinessName,
                CmetName = serializedCommonModelElementRef.CmetName ?? serializedCommonModelElementRef.Name,
                DerivedFrom = serializedCommonModelElementRef.DerivedFrom,
                IsAbstract = serializedCommonModelElementRef.IsAbstract,
                Name = serializedCommonModelElementRef.Name,
                SortKey = serializedCommonModelElementRef.SortKey,
                SupplierStructuralDomain = serializedCommonModelElementRef.SupplierStructuralDomain
            };
            retVal.Argument = new List<ClassBindingArgument>(10);
            return retVal;
        }

        /// <summary>
        /// Create a flat class from a serialized class
        /// </summary>
        private Class CreateFlatClass(SerializedClass serializedClass, GlobalStaticModel ownedModel)
        {
            // Convert SerializedClass to regular class
            Class cele = new Class()
            {
                Annotations = serializedClass.Annotations,
                Attribute = serializedClass.Attribute,
                Behavior = serializedClass.Behavior,
                BusinessName = serializedClass.BusinessName,
                Container = ownedModel,
                DerivedFrom = serializedClass.DerivedFrom,
                InterestedCommittee = serializedClass.InterestedCommittee,
                IsAbstract = serializedClass.IsAbstract,
                Name = serializedClass.Name,
                SortKey = serializedClass.SortKey,
                StewardCommittee = serializedClass.StewardCommittee,
                SupplierStructuralDomain = serializedClass.SupplierStructuralDomain
            };
            cele.SpecializationChild = CreateSpecializationChildren(serializedClass.SpecializationChild, ownedModel);
            
            // Process associations
            if (serializedClass.Association != null)
            {
                if(ownedModel.OwnedAssociation == null)
                    ownedModel.OwnedAssociation = new List<Association>(serializedClass.Association.Count);

                // Create owned associations
                ownedModel.OwnedAssociation.AddRange(CreateFlatAssociations(serializedClass.Association, ownedModel));
            }

            return cele;
        }

        /// <summary>
        /// Create flat associations from the serialized models
        /// </summary>
        private IEnumerable<Association> CreateFlatAssociations(List<SerializedAssociationEnd> associations, GlobalStaticModel ownedModel)
        {
            List<Association> retVal = new List<Association>(associations.Count);
            foreach (var assoc in associations)
            {
                // Conver the serialized association to flat association
                Association flatAssoc = new Association()
                {
                    Annotations = assoc.Annotations,
                    SortKey = assoc.SortKey
                };
                flatAssoc.Ends = new List<Relationship>() { assoc.SourceConnection.Content };

                // Since a flat association simply points to a class name, we need to add the serialized class to the
                // owned model as a class and reference it by name
                var classItem = CreateFlatModelElement(assoc.TargetConnection.ParticipantClass.Content as ModelElement, ownedModel);
                string assocName = null;
                if (classItem is Class)
                    assocName = (classItem as Class).Name;
                else if(classItem is CommonModelElementRef)
                    assocName = (classItem as CommonModelElementRef).Name;

                flatAssoc.Ends.Add(new AssociationEnd()
                {
                    Annotations = assoc.TargetConnection.Annotations,
                    BusinessName = assoc.TargetConnection.BusinessName,
                    ChoiceItem = assoc.TargetConnection.ChoiceItem,
                    Conformance = assoc.TargetConnection.Conformance,
                    DerivedFrom = assoc.TargetConnection.DerivedFrom,
                    IsMandatory = assoc.TargetConnection.IsMandatory,
                    MaximumMultiplicity = assoc.TargetConnection.MaximumMultiplicity,
                    MinimumMultiplicity = assoc.TargetConnection.MinimumMultiplicity,
                    Name = assoc.TargetConnection.Name,
                    ParticipantClassName = assocName,
                    ReferenceHistory = assoc.TargetConnection.ReferenceHistory,
                    SortKey = assoc.TargetConnection.SortKey,
                    UpdateModeDefault = assoc.TargetConnection.UpdateModeDefault,
                    UpdateModesAllowed = assoc.TargetConnection.UpdateModesAllowed
                });
                ownedModel.OwnedClass.Add(new ClassElement() { Choice = classItem });
                retVal.Add(flatAssoc);
            }
            return retVal;
        }

        /// <summary>
        /// Create specialization children
        /// </summary>
        private List<ClassGeneralization> CreateSpecializationChildren(List<SerializedClassGeneralization> childSpecializations, GlobalStaticModel ownedModel)
        {
            List<ClassGeneralization> retVal = new List<ClassGeneralization>(childSpecializations.Count);

            // Iterate through specializations
            foreach (var child in childSpecializations)
            {
                // Create class generialization
                ClassGeneralization cg = new ClassGeneralization()
                {
                    Annotations = child.Annotations,
                    Conformance = child.Conformance,
                    IsMandatory = child.IsMandatory,
                    SortKey = child.SortKey
                };
                var classItem = CreateFlatModelElement(child.SpecializedClass.Content as ModelElement, ownedModel);
                if (classItem is Class)
                    cg.ChildClassName = (classItem as Class).Name;
                else if (classItem is CommonModelElementRef)
                    Trace.WriteLine("fixme: Don't support CMET refs here", "warn"); // TODO: Create a common model element ref thingy here
                ownedModel.OwnedClass.Add(new ClassElement() { Choice = classItem });

                retVal.Add(cg);
            }
            return retVal;
        }

        /// <summary>
        /// Compile the serialized static model
        /// </summary>
        public override void Compile()
        {
            base.Compile();
        }
    }
}
