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
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Primitives;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// A CD represents any kind of concept usually by giving a code defined in a code system. A CD builds upon a CE by allowing the assignment of qualifiers.
    /// </summary>
    #if !WINDOWS_PHONE
    [Serializable]
    #endif
    [Structure(Name = "CodingRationale", CodeSystem = "2.16.840.1.113883.5.1074", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    [XmlType("CodingRationale", Namespace = "urn:hl7-org:v3")]
    public enum CodingRationale
    {
        /// <summary>
        /// Originally produced code
        /// </summary>
        [Enumeration(Value = "O")]
        [XmlEnum("O")]
        Original,
        /// <summary>
        /// Post coding from free text source
        /// </summary>
        [Enumeration(Value = "P")]
        [XmlEnum("P")]
        PostCoding,
        /// <summary>
        /// Required by the specification describing the use of the coded concept
        /// </summary>
        [Enumeration(Value = "R")]
        [XmlEnum("R")]
        Required,
        /// <summary>
        /// The source of a required code
        /// </summary>
        [Enumeration(Value = "S")]
        [XmlEnum("S")]
        Source
    }

    /// <summary>
    /// Represents a non genericized (ie: unbound) concept descriptor.
    /// </summary>
    /// <remarks>
    /// Represents a concept descriptor that is not bound to a particular value set. Equivalent to CD{String}
    /// </remarks>
    /// <seealso cref="T:MARC.Everest.DataType.CD{}"/>
    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance"), Serializable]
    //[XmlType("CD", Namespace = "urn:hl7-org:v3")]
    //[Obsolete("CD is obsolete, consider using CD<String>", true)]
    //public class CD : CD<string>
    //{
    //    /// <summary>
    //    /// Create a new instance of CD
    //    /// </summary>
    //    public CD() { }
    //    /// <summary>
    //    /// Create a new instance of CD with the specified code
    //    /// </summary>
    //    /// <param name="code">The initial code</param>
    //    public CD(string code) : base(code) { }
    //    /// <summary>
    //    /// Create a new instance of CD with the specified code and code system
    //    /// </summary>
    //    /// <param name="code">The initial code of the CS</param>
    //    /// <param name="codeSystem">The code system the code was picked from</param>
    //    public CD(string code, string codeSystem) : base(code, codeSystem) { }
    //    /// <summary>
    //    /// Create a new instance of CD with the specified parameters
    //    /// </summary>
    //    /// <param name="code">The initial code</param>
    //    /// <param name="codeSystem">The code system the code was picked from</param>
    //    /// <param name="codeSystemName">The name of the code system</param>
    //    /// <param name="codeSystemVersion">The version of the code system</param>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1025:ReplaceRepetitiveArgumentsWithParamsArray")]
    //    public CD(string code, string codeSystem, string codeSystemName, string codeSystemVersion) : base(code, codeSystem, codeSystemName, codeSystemVersion) { }
    //    /// <summary>
    //    /// Create a new instance of the CD data type with the parameters specified
    //    /// </summary>
    //    /// <param name="code">The initial code</param>
    //    /// <param name="codeSystem">The code system the code was picked from</param>
    //    /// <param name="codeSystemName">The name of the code system</param>
    //    /// <param name="codeSystemVersion">The version of the code system</param>
    //    /// <param name="displayName">The display name for the code</param>
    //    /// <param name="originalText">The original text, the reason the code was selected</param>
    //    public CD(string code, string codeSystem, string codeSystemName, string codeSystemVersion, string displayName, string originalText) : base(code, codeSystem, codeSystemName, codeSystemVersion, displayName, originalText) { }
    //    /// <summary>
    //    /// Create a new instance of CD with the specified code, code system, and translation
    //    /// </summary>
    //    /// <param name="code">The initial code of the CS</param>
    //    /// <param name="codeSystem">The code system the code was picked from</param>
    //    /// <param name="translation">Translations for this concept descriptor</param>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
    //    public CD(string code, string codeSystem, IEnumerable<CD<string>> translation) : base(code, codeSystem, translation) { }
    //    /// <summary>
    //    /// Create a new instance of CD with the specified code, code system, translation, and qualifiers
    //    /// </summary>
    //    /// <param name="code">The initial code of the CS</param>
    //    /// <param name="codeSystem">The code system the code was picked from</param>
    //    /// <param name="translation">Translations for this concept descriptor</param>
    //    /// <param name="qualifier">Specified additional codes that increase the specificity of the primary code</param>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
    //    public CD(string code, string codeSystem, IEnumerable<CD<string>> translation, IEnumerable<CR<string>> qualifier)
    //        : base(code, codeSystem, translation, qualifier) { }

    //    /// <summary>
    //    /// Converts a <see cref="string"/> to a <see cref="CD"/>
    //    /// </summary>
    //    /// <param name="s">string to convert</param>
    //    /// <returns>Converted CD</returns>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
    //    public static implicit operator CD(string s)
    //    {
    //        CD retVal = new CD();
    //        retVal.Code = new CodeValue<string>(s);
    //        return retVal;
    //    }

    //    /// <summary>
    //    /// Converts a <see cref="CD"/> to a <see cref="string"/>
    //    /// </summary>
    //    /// <param name="cs">CD to convert</param>
    //    /// <returns>Converted string</returns>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
    //    public static implicit operator string(CD cs)
    //    {
    //        return cs.Code;
    //    }

    //    /// <summary>
    //    /// Converts a strongly typed CD to a generic CD
    //    /// </summary>
    //    public static CD Parse<T>(CD<T> cd)
    //    {
    //        CD retVal = new CD();
    //        // Parse from wire format into this format
    //        retVal.Code = new CodeValue<string>(Util.ToWireFormat(cd.Code));
    //        // Code System
    //        retVal.CodeSystem = cd.CodeSystem;
    //        retVal.CodeSystemName = cd.CodeSystemName;
    //        retVal.CodeSystemVersion = cd.CodeSystemVersion;
    //        retVal.ControlActExt = cd.ControlActExt;
    //        retVal.ControlActRoot = cd.ControlActRoot;
    //        retVal.UpdateMode = cd.UpdateMode == null ? null : cd.UpdateMode.Clone() as CS<UpdateMode>;
    //        retVal.NullFlavor = cd.NullFlavor == null ? null : cd.NullFlavor.Clone() as CS<NullFlavor>;
    //        retVal.ValidTimeHigh = cd.ValidTimeHigh;
    //        retVal.ValidTimeLow = cd.ValidTimeLow;
    //        retVal.DisplayName = cd.DisplayName;
    //        retVal.OriginalText = cd.OriginalText == null ? null : cd.OriginalText.Clone() as ED;
    //        retVal.Qualifier = cd.Qualifier == null ? null : new LIST<CR<string>>(cd.Qualifier);
    //        retVal.Translation = cd.Translation == null ? null : new SET<CD<string>>(cd.Translation, CD<String>.Comparator);

    //        return retVal;
    //    }
    //}

    /// <summary>
    /// Represents any kind of concept usually given by a code with qualifications.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A CD represents any kind of concept usually by giving a code defined in a code system. A CD can contain 
    /// the original text or phrase that served as the basis of the coding and one or more translations into
    /// the same or different coding systems. A CD can also contain qualifiers to describe the concept of left foot
    /// as a postcoordinated term built from the primary code "FOOT"
    /// </para>
    /// Concept Descriptors are used to represent complex, codified concepts within an HL7v3 message. 
    /// </remarks>
    /// <example>
    /// <code title="Representing a complex code" lang="cs">
    /// <![CDATA[
    /// // Declare the Concept Descriptor (Burn of Skin)
    /// CD<String> cd = new CD<String>();
    /// cd.Code = "284196006";
    /// cd.DisplayName = "Burn of Skin";
    /// 
    /// // Declare Severity as Severe so code becomes Severe Burn of Skin
    /// CR<String> severity = new CR<String>();
    /// severity.Name = new CD<string>();
    /// severity.Value = new CD<string>();
    /// cd.Qualifier = new LIST<CR<string>>();
    /// cd.Qualifier.Add(severity);
    /// severity.Name.Code = "246112005";
    /// severity.Name.CodeSystem = "2.16.840.1.113883.6.96";
    /// severity.Name.CodeSystemName = "SNOMED CT";
    /// severity.Name.DisplayName = "Severity";
    /// severity.Value.Code = "24484000";
    /// severity.Value.CodeSystem = "2.16.840.1.113883.6.96";
    /// severity.Value.CodeSystemName = "SNOMED CT";
    /// severity.Value.DisplayName = "Severe";
    /// 
    /// // Identify the finding site so code becomes Severe Burn of Skin between Fourth and Fifth Toes
    /// CR findingsite = new CR();
    /// findingsite.Name = new CD<string>();
    /// findingsite.Value = new CD<string>();
    /// cd.Qualifier.Add(findingsite);
    /// findingsite.Name.Code = "363698007";
    /// findingsite.Name.CodeSystem = "2.16.840.1.113883.6.96";
    /// findingsite.Name.CodeSystemName = "SNOMED CT";
    /// findingsite.Name.DisplayName = "Finding site";
    /// findingsite.Value.Code = "113185004";
    /// findingsite.Value.CodeSystem = "2.16.840.1.113883.6.96";
    /// findingsite.Value.CodeSystemName = "SNOMED CT";
    /// findingsite.Value.DisplayName = "Skin between Fourth and Fifth Toes";
    /// 
    /// // Identify the laterality so code becomes Severe Burn of Skin between Fourth and Fifth Toes on Left side
    /// CR laterality = new CR();
    /// laterality.Name = new CD<string>();
    /// laterality.Value = new CD<string>();
    /// findingsite.Value.Qualifier = new LIST<CR<string>>();
    /// findingsite.Value.Qualifier.Add(laterality);
    /// laterality.Name.Code = "272741003";
    /// laterality.Name.CodeSystem = "2.16.840.1.113883.6.96";
    /// laterality.Name.CodeSystemName = "SNOMED CT";
    /// laterality.Name.DisplayName = "Laterality";
    /// laterality.Value.Code = "7771000";
    /// laterality.Value.CodeSystem = "2.16.840.1.113883.6.96";
    /// laterality.Value.CodeSystemName = "SNOMED CT";
    /// laterality.Value.DisplayName = "Left";
    /// ]]>
    /// </code>
    /// </example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    [Structure(Name = "CD", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(String))]
    [FlavorMapAttribute(FlavorId = "CV", Implementer = typeof(CV<>))]
    [FlavorMapAttribute(FlavorId = "CE", Implementer = typeof(CE<>))]
    #if !WINDOWS_PHONE
    [Serializable]
    #endif
    public class CD<T> : CE<T>, IConceptDescriptor, IEquatable<CD<T>>
    {

        /// <summary>
        /// Create a new instance of CD
        /// </summary>
        public CD() { }
        /// <summary>
        /// Create a new instance of CD with the specified code
        /// </summary>
        /// <param name="code">The initial code</param>
        public CD(T code) : base(code) { }
        /// <summary>
        /// Create a new instance of CD with the specified code and code system
        /// </summary>
        /// <param name="code">The initial code of the CS</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        public CD(T code, string codeSystem) : base(code, codeSystem) { }
        /// <summary>
        /// Create a new instance of CD with the specified parameters
        /// </summary>
        /// <param name="code">The initial code</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        /// <param name="codeSystemName">The name of the code system</param>
        /// <param name="codeSystemVersion">The version of the code system</param>
        public CD(T code, string codeSystem, string codeSystemName, string codeSystemVersion) : base(code, codeSystem, codeSystemName, codeSystemVersion) { }
        /// <summary>
        /// Create a new instance of the CD data type with the parameters specified
        /// </summary>
        /// <param name="code">The initial code</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        /// <param name="codeSystemName">The name of the code system</param>
        /// <param name="codeSystemVersion">The version of the code system</param>
        /// <param name="displayName">The display name for the code</param>
        /// <param name="originalText">The original text, the reason the code was selected</param>
        public CD(T code, string codeSystem, string codeSystemName, string codeSystemVersion, ST displayName, ED originalText) : base(code, codeSystem, codeSystemName, codeSystemVersion, displayName, originalText) { }
        /// <summary>
        /// Create a new instance of CD with the specified code and code system
        /// </summary>
        /// <param name="code">The initial code of the CS</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        /// <param name="translation">Translations for this concept descriptor</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public CD(T code, string codeSystem, IEnumerable<CD<T>> translation) : base(code, codeSystem, translation) { }
        /// <summary>
        /// Create a new instance of CD with the specified code and code system
        /// </summary>
        /// <param name="code">The initial code of the CS</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        /// <param name="translation">Translations for this concept descriptor</param>
        /// <param name="qualifier">Specified additional codes that increase the specificity of the primary code</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public CD(T code, string codeSystem, IEnumerable<CD<T>> translation, IEnumerable<CR<T>> qualifier)
            : base(code, codeSystem, translation)
        {
            this.Qualifier = new LIST<CR<T>>(qualifier);
        }

        /// <summary>
        /// Specifies additonal codes that increase the specificity of the primary code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), Property(Name = "qualifier", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Required)]
        [XmlElement("qualifier")]
        public LIST<CR<T>> Qualifier { get; set; }

        /// <summary>
        /// Validate
        /// </summary>
        /// <remarks>A concept descriptor is valid if:
        /// <list type="bullet">
        ///     <item>All validation criteria from <see cref="T:MARC.Everest.DataTypes.CE{T}"/></item>
        ///     <item>If a qualifier is specified, then a code must be specified</item>
        /// </list></remarks>
        public override bool Validate()
        {
            bool isValid = ((Qualifier != null && Code != null) || (Qualifier == null));
            return isValid && base.Validate();
        }

        /// <summary>
        /// Validates the data type returning the validation errors that occur
        /// </summary>
        public override IEnumerable<IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());
            if (this.Qualifier != null && this.Code == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CD", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Qualifier", "Code"), null));
            return retVal;
        }

        #region Operators

        /// <summary>
        /// Explicit operator for casting a CD of <typeparamref name="T"/>
        /// to a string
        /// </summary>
        internal static CD<String> DownParse(CD<T> cd)
        {
            CD<String> retVal = new CD<String>();
            // Parse from wire format into this format
            retVal.Code = new CodeValue<string>(Util.ToWireFormat(cd.Code));
            // Code System
            retVal.CodeSystem = cd.CodeSystem;
            retVal.CodeSystemName = cd.CodeSystemName;
            retVal.CodeSystemVersion = cd.CodeSystemVersion;
            retVal.ControlActExt = cd.ControlActExt;
            retVal.ControlActRoot = cd.ControlActRoot;
            retVal.UpdateMode = cd.UpdateMode == null ? null : cd.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.NullFlavor = cd.NullFlavor == null ? null : cd.NullFlavor.Clone() as CS<NullFlavor>;
            retVal.ValidTimeHigh = cd.ValidTimeHigh;
            retVal.ValidTimeLow = cd.ValidTimeLow;
            retVal.DisplayName = cd.DisplayName;
            retVal.OriginalText = cd.OriginalText == null ? null : cd.OriginalText.Clone() as ED;
            retVal.Qualifier = cd.Qualifier == null ? null : new LIST<CR<string>>(cd.Qualifier);
            retVal.Translation = cd.Translation == null ? null : new SET<CD<string>>(cd.Translation, CD<String>.Comparator);
            retVal.Flavor = cd.Flavor;
            retVal.CodingRationale = cd.CodingRationale;
            //retVal.Group = cd.Group != null ? cd.Group.Clone() as LIST<CDGroup> : null;
            retVal.UpdateMode = cd.UpdateMode;
            return retVal;
        }

        /// <summary>
        /// Converts a CD to a strongly typed CD
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        public static CD<T> Parse(CD<String> cd)
        {
            CD<T> retVal = new CD<T>();
            // Parse from wire format into this format
            retVal.Code = CodeValue<T>.Parse(cd.Code);
            // Code System
            retVal.CodeSystem = cd.CodeSystem;
            retVal.CodeSystemName = cd.CodeSystemName;
            retVal.CodeSystemVersion = cd.CodeSystemVersion;
            retVal.ControlActExt = cd.ControlActExt;
            retVal.ControlActRoot = cd.ControlActRoot;
            retVal.UpdateMode = cd.UpdateMode == null ? null : cd.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.NullFlavor = cd.NullFlavor as CS<NullFlavor>;
            retVal.ValidTimeHigh = cd.ValidTimeHigh;
            retVal.ValidTimeLow = cd.ValidTimeLow;
            retVal.OriginalText = cd.OriginalText == null ? null : cd.OriginalText.Clone() as ED;
            retVal.DisplayName = cd.DisplayName;
            retVal.Translation = cd.Translation == null ? null : new SET<CD<T>>(cd.Translation, CD<T>.Comparator);
            retVal.Qualifier = cd.Qualifier == null ? null : new LIST<CR<T>>(cd.Qualifier);
            retVal.CodingRationale = cd.CodingRationale;
            //retVal.Group = cd.Group != null ? cd.Group.Clone() as LIST<CDGroup> : null;
            retVal.UpdateMode = cd.UpdateMode;
            retVal.Flavor = cd.Flavor;

            return retVal;
        }

        /// <summary>
        /// Converts a CD to a strongly typed CD
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        public static CD<T> Parse(CV<String> cd)
        {
            CD<T> retVal = new CD<T>();
            // Parse from wire format into this format
            retVal.Code = CodeValue<T>.Parse(cd.Code);
            // Code System
            retVal.CodeSystem = cd.CodeSystem;
            retVal.CodeSystemName = cd.CodeSystemName;
            retVal.CodeSystemVersion = cd.CodeSystemVersion;
            retVal.ControlActExt = cd.ControlActExt;
            retVal.ControlActRoot = cd.ControlActRoot;
            retVal.UpdateMode = cd.UpdateMode == null ? null : cd.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.NullFlavor = cd.NullFlavor as CS<NullFlavor>;
            retVal.ValidTimeHigh = cd.ValidTimeHigh;
            retVal.ValidTimeLow = cd.ValidTimeLow;
            retVal.OriginalText = cd.OriginalText == null ? null : cd.OriginalText.Clone() as ED;
            retVal.DisplayName = cd.DisplayName;
            retVal.CodingRationale = cd.CodingRationale;
            //retVal.Group = cd.Group != null ? cd.Group.Clone() as LIST<CDGroup> : null;
            retVal.UpdateMode = cd.UpdateMode;
            retVal.Flavor = cd.Flavor;

            return retVal;
        }

        /// <summary>
        /// Converts a <see cref="CD"/> to a <typeparamref name="T"/>
        /// </summary>
        /// <param name="o">CD`1 to convert</param>
        /// <returns>Converted T</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator T(CD<T> o)
        {
            return o.Code;
        }

        /// <summary>
        /// Converts a <typeparamref name="T"/> to a <see cref="CD"/>
        /// </summary>
        /// <param name="o">T to convert</param>
        /// <returns>Converted CD`1</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator CD<T>(T o)
        {
            CD<T> retVal = new CD<T>();
            retVal.Code = o;
            return retVal;
        }
        #endregion

        #region IConceptDescriptor Members

        /// <summary>
        /// Specifies additonal codes that increase the specificity of the primary code
        /// </summary>
        LIST<IGraphable> IConceptDescriptor.Qualifier
        {
            get
            {
                return new LIST<IGraphable>(this.Qualifier);
            }
            set
            {
                this.Qualifier = LIST<CR<T>>.Parse(value);
            }
        }

        #endregion

        /// <summary>
        /// Comparator for sets
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static new Comparison<CD<T>> Comparator = delegate(CD<T> a, CD<T> b)
        {
            if (
                (a.Code != null && a.Code.Equals(b.Code) || b.Code == null) && 
                a.CodeSystem == b.CodeSystem)
                return 0;
            else
                return 1;
        };

        #region IEquatable<CD<T>> Members

        /// <summary>
        /// Determine if this CD of T is equal to another CD of T
        /// </summary>
        public bool Equals(CD<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((CE<T>)other) &&
                    (other.Qualifier != null ? other.Qualifier.Equals(this.Qualifier) : this.Qualifier == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is CD<T>)
                return Equals(obj as CD<T>);
            return base.Equals(obj);
        }

        #endregion
    }
}