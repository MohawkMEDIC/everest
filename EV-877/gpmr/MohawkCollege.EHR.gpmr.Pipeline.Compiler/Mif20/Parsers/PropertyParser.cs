/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: Justin Fyfe
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// The property parser is responsible for the parsing of MIF 2.0 class attribute structures into 
    /// COR Property structures
    /// </summary>
    internal class PropertyParser
    {
        /// <summary>
        /// Private constructor so the compiler will not create a default one for this class.
        /// </summary>
        private PropertyParser() { }

        public static Dictionary<string, Property.CodingStrengthKind> defaultCodingStrengths = new Dictionary<string, Property.CodingStrengthKind>(); 

        /// <summary>
        /// Parse the class attribute into a COR property class
        /// </summary>
        /// <param name="clsa">The class attribute to parse</param>
        /// <param name="vocabBindingRealm">The vocabulary realm that this is bound to</param>
        /// <returns>The parsed class attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        internal static Property Parse(ClassAttribute clsa, String vocabBindingRealm, ClassRepository cr, Dictionary<string, Package> derivationSuppliers)
        {

            // TODO: Support EnumerationValue stuff
            Property retVal = new Property();

            // Name (and busines names)
            retVal.Name = clsa.Name;
            foreach (BusinessName bn in clsa.BusinessName)
                if (bn.Language == MifCompiler.Language || bn.Language == null)
                    retVal.BusinessName = bn.Name;

            // Documentation
            if(clsa.Annotations != null)
                retVal.Documentation = DocumentationParser.Parse(clsa.Annotations.Documentation);

            // Conformance
            retVal.Conformance = clsa.IsMandatory ? ClassContent.ConformanceKind.Mandatory : clsa.Conformance == ConformanceKind.Required ? ClassContent.ConformanceKind.Required :
                ClassContent.ConformanceKind.Optional;

            if (retVal.Conformance != Property.ConformanceKind.Mandatory && clsa.MinimumMultiplicity == "1") retVal.Conformance = Property.ConformanceKind.Populated;

            // Min / Max occurs
            if (retVal.Conformance == MohawkCollege.EHR.gpmr.COR.Property.ConformanceKind.Mandatory) retVal.MinOccurs = "1";
            else retVal.MinOccurs = clsa.MinimumMultiplicity;
            retVal.MaxOccurs = clsa.MaximumMultiplicity == "-1" ? "*" : clsa.MaximumMultiplicity;
            retVal.MaxLength = clsa.MaximumLength;
            retVal.MinLength = clsa.MinimumLength;

            // Structural or non?
            retVal.PropertyType = clsa.IsImmutable ? Property.PropertyTypes.Structural : Property.PropertyTypes.NonStructural;

            // Default value
            retVal.DefaultValue = clsa.DefaultValue;
            retVal.Initializor = clsa.DefaultFrom == DefaultDeterminerKind.ITS ? Property.InitializorTypes.ITS :
                clsa.DefaultFrom == DefaultDeterminerKind.Realm ? Property.InitializorTypes.Realm :
                clsa.DefaultFrom == DefaultDeterminerKind.ReferencingAttribute ? Property.InitializorTypes.ReferencedAttributes : Property.InitializorTypes.DefaultValue;

            if (clsa.DerivedFrom != null )
            {
                retVal.Realization = new List<ClassContent>();
                foreach (var dei in clsa.DerivedFrom)
                {
                    
                    MohawkCollege.EHR.gpmr.COR.Feature ss = null;
                    Package derivationPkg = null;
                    if(!derivationSuppliers.TryGetValue(dei.StaticModelDerivationId, out derivationPkg) || derivationPkg == null)
                    {
                        continue;
                    }

                    // Has the package been compiled?
                    if (!cr.TryGetValue(string.Format("{0}", derivationPkg.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : derivationPkg.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), out ss))
                    {
                        // Attempt to parse 
                        PackageParser.Parse(derivationPkg.PackageLocation.ToString(MifCompiler.NAME_FORMAT), derivationPkg.MemberOfRepository, cr);
                        // Ditch if still can't find
                        if (!cr.TryGetValue(string.Format("{0}", derivationPkg.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : derivationPkg.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), out ss))
                            System.Diagnostics.Trace.WriteLine(String.Format("Can't find derivation class '{0}' for association '{1}' (derivation supplier {2})", dei.ClassName, dei.AttributeName, dei.StaticModelDerivationId), "warn");
                    }

                    // Feature was found
                    var f = (ss as MohawkCollege.EHR.gpmr.COR.SubSystem).FindClass(dei.ClassName);
                    if(f != null)
                    {
                        ClassContent cc = f.GetFullContent().Find(o => o.Name == dei.AttributeName);
                        retVal.Realization.Add(cc);
                    }
                    else
                        System.Diagnostics.Trace.WriteLine(String.Format("Can't find derivation class '{0}' for association '{1}' (derivation supplier {2})", dei.ClassName, dei.AttributeName, dei.StaticModelDerivationId), "warn");
                }
            }

            // Derived from
            retVal.DerivedFrom = clsa;

            // Fixed Value
            retVal.FixedValue = clsa.FixedValue;

            // Sort key
            retVal.SortKey = clsa.SortKey;

            // Datatype
            retVal.Type = TypeReferenceParser.Parse(clsa.Type);

            // Update Modes
            retVal.UpdateMode = clsa.UpdateModeDefault.ToString();
            if (clsa.UpdateModesAllowed != null)
            {
                retVal.AllowedUpdateModes = new List<string>();
                foreach (string s in clsa.UpdateModesAllowed.Split(','))
                    retVal.AllowedUpdateModes.Add(s);
            }

            // Supplier domains
            if (clsa.Vocabulary != null)
            {
               

                if (clsa.Vocabulary.Code != null && !String.IsNullOrEmpty(clsa.Vocabulary.Code.Code)) // Fixed code 
                    retVal.FixedValue = string.Format("{0}", clsa.Vocabulary.Code.Code);
                
                // JF: If the code system is identified, then bind
                if (clsa.Vocabulary.Code != null && !String.IsNullOrEmpty(clsa.Vocabulary.Code.CodeSystemName)) // Very odd thing that is present in UV mifs
                {
                    Trace.WriteLine(String.Format("'{0}' is specified as fixed code's code system, however no fixed code is present. Assuming this is a bound code system instead", "assumption"));
                    retVal.SupplierDomain = cr.Find(o => o is CodeSystem && (o as CodeSystem).Name.Equals(clsa.Vocabulary.Code.CodeSystemName)) as Enumeration;
                    if (retVal.SupplierDomain == null)
                        Trace.WriteLine(String.Format("'{0}' could not be bound to '{1}' as the code system was not found", clsa.Name, clsa.Vocabulary.Code.CodeSystemName), "warn"); 
                }
                if (clsa.Vocabulary.ConceptDomain != null)
                {
                    retVal.SupplierDomain = cr.Find(o => o is ConceptDomain && (o as ConceptDomain).Name.Equals(clsa.Vocabulary.ConceptDomain.Name)) as Enumeration;
                    if (retVal.SupplierDomain == null && MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks)
                    {
                        retVal.SupplierDomain = cr.Find(o => o is Enumeration && o.Name.Equals(clsa.Vocabulary.ConceptDomain.Name)) as Enumeration;
                        if (retVal.SupplierDomain != null)
                            Trace.WriteLine(String.Format("'{0}' couldn't be bound to concept domain '{1}', '{2}' with name '{1}' was located, so the binding was changed", clsa.Name, clsa.Vocabulary.ConceptDomain.Name, retVal.SupplierDomain.EnumerationType), "quirks");
                    }
                    if (retVal.SupplierDomain == null)
                        Trace.WriteLine(String.Format("'{0}' could not be bound to '{1}' as the concept domain was not found", clsa.Name, clsa.Vocabulary.ConceptDomain.Name), "warn");
                }
                if (clsa.Vocabulary.ValueSet != null)
                {
                    retVal.SupplierDomain = cr.Find(o => o is ValueSet && (o as ValueSet).Name.Equals(clsa.Vocabulary.ValueSet.Name)) as Enumeration;
                    if (retVal.SupplierDomain == null)
                        Trace.WriteLine(String.Format("'{0}' could not be bound to '{1}' as the value set was not found", clsa.Name, clsa.Vocabulary.ValueSet.Name), "warn");

                    if (!String.IsNullOrEmpty(clsa.Vocabulary.ValueSet.RootCode))
                    {
                        bool shouldFix = false;
                        if (retVal.SupplierDomain != null) {
                            var enumLiteral = retVal.SupplierDomain.Literals.Find(o => o.Name == clsa.Vocabulary.ValueSet.RootCode);
                            shouldFix = enumLiteral != null && enumLiteral.RelatedCodes != null && enumLiteral.RelatedCodes.Count == 0;
                        }
                        if(shouldFix) retVal.FixedValue = String.Format("{0}", clsa.Vocabulary.ValueSet.RootCode);
                    }
                }

                // Supplier strength(s)
                if (clsa.Vocabulary.ValueSet != null)
                    retVal.SupplierStrength = clsa.Vocabulary.ValueSet.CodingStrength == CodingStrengthKind.CNE ? (Property.CodingStrengthKind?)Property.CodingStrengthKind.CodedNoExtensions :
                        clsa.Vocabulary.ValueSet.CodingStrength == CodingStrengthKind.CWE ? (Property.CodingStrengthKind?)Property.CodingStrengthKind.CodedNoExtensions : null;

                // Supplier domain strength
                if (retVal.SupplierDomain != null)
                {
                    if (defaultCodingStrengths.ContainsKey(retVal.SupplierDomain.Name ?? "") && !retVal.SupplierStrength.HasValue)
                        retVal.SupplierStrength = defaultCodingStrengths[retVal.SupplierDomain.Name];
                    else
                    {
                        retVal.SupplierStrength = Property.CodingStrengthKind.CodedNoExtensions;
                        System.Diagnostics.Trace.WriteLine(string.Format("No vocabulary value set on property {0}! Defaulting to CNE for supplier strength", retVal.Name), "assumption");
                    }
                }
            }

            return retVal;
        }
    }
}
