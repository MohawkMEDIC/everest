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
using MARC.Everest.Interfaces;
using MARC.Everest.Attributes;
using System.Xml.Serialization;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// An enumeration of part types.
    /// </summary>
    /// <example>
    /// <code lang="cs" title="Using EN">
    /// <![CDATA[
    ///
    /// // EntityNameUse: as recorded on a license
    /// EN name = new EN(
    ///     EntityNameUse.Legal, new ENXP[] { 
    ///         new ENXP("James", EntityNamePartType.Given), 
    ///             new ENXP("Tiberius", EntityNamePartType.Given), 
    ///             new ENXP("Kirk", EntityNamePartType.Family) });
    ///
    /// LIST<ENXP> part = new LIST<ENXP>(new ENXP[] {
    ///     new ENXP("Tiberius", EntityNamePartType.Given),
    ///     new ENXP("Kirk", EntityNamePartType.Prefix)});
    ///     part[0].Qualifier = EntityNamePartQualifier.Academic;
    ///     part[1].Qualifier = EntityNamePartQualifier.Nobility;
    ///     name.Part.Add(part[0]);
    ///     name.Part.Add(part[1]);
    ///     Console.Write(name.ToString("{FAM}, {GIV}"));
    /// ]]>
    /// </code>
    /// </example>
    [XmlType("EntityNamePartType", Namespace = "urn:hl7-org:v3")]
    [Structure(Name = "EntityNamePartType", CodeSystem = "2.16.840.1.113883.5.1121", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    public enum EntityNamePartType
    {
        /// <summary>
        /// The family name that links to the genealogy
        /// </summary>
        [Enumeration(Value = "FAM")]
        [XmlEnum("FAM")]
        Family,
        /// <summary>
        /// Given name (first, middle or other given names)
        /// </summary>
        [Enumeration(Value = "GIV")]
        [XmlEnum("GIV")]
        Given,
        /// <summary>
        /// A prefix has a string association to the immediately following name part
        /// </summary>
        [Obsolete("Use Qualifier Prefix", false)]
        [Enumeration(Value = "PFX")]
        [XmlEnum("PFX")]
        Prefix,
        /// <summary>
        /// A suffix has a strong association to the part immediately before the name part
        /// </summary>
        [Obsolete("Use Qualifier Suffix", false)]
        [Enumeration(Value = "SFX")]
        [XmlEnum("SFX")]
        Suffix,
        /// <summary>
        /// Part of a name acquired due to an academic, legal, employment or certification
        /// </summary>
        [Enumeration(Value = "TITLE")]
        [XmlEnum("TITLE")]
        Title,
        /// <summary>
        /// A delimiter has no meaning other than being literally printed in this name representation
        /// </summary>
        [Enumeration(Value = "DEL")]
        [XmlEnum("DEL")]
        Delimiter
    }
    /// <summary>
    /// An enumeration of part qualifiers.
    /// </summary>
    [XmlType("EntityNamePartQualifier", Namespace = "urn:hl7-org:v3")]
    [Structure(Name = "EntityNamePartQualifier", CodeSystem= "2.16.840.1.113883.5.1122", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    public enum EntityNamePartQualifier 
    {
        /// <summary>
        /// For organizations indicating legal status
        /// </summary>
        [Enumeration(Value = "LS")]
        [XmlEnum("LS")]
        LegalStatus,
        /// <summary>
        /// Indicates that a prefix like "Dr" or a suffix like "M.D." is an 
        /// academic title
        /// </summary>
        [Enumeration(Value = "AC")]
        [XmlEnum("AC")]
        Academic,
        /// <summary>
        /// In Europe and Asia there are some individuals with titles of nobility
        /// </summary>
        [Enumeration(Value = "NB")]
        [XmlEnum("NB")]
        Nobility,
        /// <summary>
        /// Primarily in the British imperial culture people tend to have an abbreviation of their professional
        /// organizations as part of the credential suffixes.
        /// </summary>
        [Enumeration(Value = "PR")]
        [XmlEnum("PR")]
        Professional,
        /// <summary>
        /// An honorific
        /// </summary>
        [Enumeration(Value = "HON")]
        [XmlEnum("HON")]
        Honorific,
        /// <summary>
        /// A name the person had shortly after being born
        /// </summary>
        [Enumeration(Value = "BR")]
        [XmlEnum("BR")]
        Birth,
        /// <summary>
        /// A name part a person acquired through adoption, or a chosen name
        /// </summary>
        [Enumeration(Value = "AD")]
        [XmlEnum("AD")]
        Acquired,        
        /// <summary>
        /// The name assumed from the partner in a marital relationship
        /// </summary>
        [Enumeration(Value = "SP")]
        [XmlEnum("SP")]
        Spouse,
        /// <summary>
        /// A call me name is usually a given name that is preferred when the person is addressed
        /// </summary>
        [Enumeration(Value = "CL")]
        [XmlEnum("CL")]
        CallMe,
        /// <summary>
        /// Indicates that a name part is just an initial
        /// </summary>
        [Enumeration(Value = "IN")]
        [XmlEnum("IN")]
        Initial,
        /// <summary>
        /// Identifies a middle name
        /// </summary>
        [Enumeration(Value = "MID")]
        [XmlEnum("MID")]
        Middle,
        /// <summary>
        /// Identifies a middle name
        /// </summary>
        [Enumeration(Value = "PFX")]
        [XmlEnum("PFX")]
        Prefix,
        /// <summary>
        /// Identifies a middle name
        /// </summary>
        [Enumeration(Value = "SFX")]
        [XmlEnum("SFX")]
        Suffix

    }

    /// <summary>
    /// A character string token representing a part of a name
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENXP"), Serializable]
    [Structure(Name = "ENXP", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("ENXP", Namespace = "urn:hl7-org:v3")]
    public class ENXP : ANY, IGraphable, IEquatable<ENXP>
    {
        /// <summary>
        /// A collection of EntityNamePartTypes paired to a EntityNamePartQualifier in a dictionary.
        /// </summary>
        private static Dictionary<EntityNamePartQualifier?, List<EntityNamePartType?>> validation = new Dictionary<EntityNamePartQualifier?, List<EntityNamePartType?>>();
        /// <summary>
        /// Static CTOR
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ENXP()
        {
            // Create keys
            foreach (EntityNamePartQualifier key in Enum.GetValues(typeof(EntityNamePartQualifier)))
                validation.Add(key, new List<EntityNamePartType?>());

            // Add
            validation[EntityNamePartQualifier.LegalStatus].AddRange(new EntityNamePartType?[] { EntityNamePartType.Title });
            validation[EntityNamePartQualifier.Academic].AddRange(new EntityNamePartType?[] { EntityNamePartType.Title });
            validation[EntityNamePartQualifier.Nobility].Add(EntityNamePartType.Title);
            validation[EntityNamePartQualifier.Professional].AddRange(new EntityNamePartType?[] { EntityNamePartType.Title });
            validation[EntityNamePartQualifier.Honorific].Add(EntityNamePartType.Title);
            validation[EntityNamePartQualifier.Acquired].AddRange(new EntityNamePartType?[] { 
                EntityNamePartType.Family, EntityNamePartType.Given });
            validation[EntityNamePartQualifier.Birth].AddRange(new EntityNamePartType?[] { 
                EntityNamePartType.Family, EntityNamePartType.Given });
            validation[EntityNamePartQualifier.Spouse].AddRange(new EntityNamePartType?[] { 
                EntityNamePartType.Family, EntityNamePartType.Given });
            validation[EntityNamePartQualifier.CallMe].AddRange(new EntityNamePartType?[] { 
                EntityNamePartType.Family, EntityNamePartType.Given });
            validation[EntityNamePartQualifier.Initial].AddRange(new EntityNamePartType?[] {
                EntityNamePartType.Given, EntityNamePartType.Family
            });
            validation[EntityNamePartQualifier.Prefix].AddRange(new EntityNamePartType?[] { EntityNamePartType.Title, EntityNamePartType.Given, EntityNamePartType.Family });
            validation[EntityNamePartQualifier.Suffix].AddRange(new EntityNamePartType?[] { EntityNamePartType.Title, EntityNamePartType.Given, EntityNamePartType.Family });
            validation[EntityNamePartQualifier.Middle].AddRange(new EntityNamePartType?[] { EntityNamePartType.Given, EntityNamePartType.Family });
        }

        /// <summary>
        /// Create a new instance of the ENXP type
        /// </summary>
        public ENXP() : base() { }
        /// <summary>
        /// Create a new instance of the ENXP type
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>Type set to Given</remarks>
        public ENXP(String value) : base() { this.Value = value; }
        /// <summary>
        /// Create a new instance of the ENXP type
        /// </summary>
        /// <param name="value">The value of the name part</param>
        /// <param name="type">The type of name part</param>
        public ENXP(String value, EntityNamePartType type) : base() { this.Value = value; this.Type = (EntityNamePartType?)type; }
        /// <summary>
        /// The value of the ENXP
        /// </summary>
        [Property(Name = "value", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("value")]
        public string Value { get; set; }
        /// <summary>
        /// A code assigned to the name part by a coding system if applicable
        /// </summary>
        [Property(Name = "code", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("code")]
        public string Code { get; set; }
        /// <summary>
        /// The code system from which the code is taken
        /// </summary>
        [Property(Name = "codeSystem", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("codeSystem")]
        public string CodeSystem { get; set; }
        /// <summary>
        /// The version of the coding system
        /// </summary>
        [Property(Name = "codeSystemVersion", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("codeSystemVersion")]
        public string CodeSystemVersion { get; set; }
        /// <summary>
        /// The type of entity name part
        /// </summary>
        /// <remarks>
        /// Two of the part types, namely <see cref="P:EntityNamePartType.Suffix"/> and <see cref="P:EntityNamePartType.Prefix"/> are not supported in the latest 
        /// version of the ISO data types (DT R2). Whenever the DT R2 formatter encounters a type named Suffix or Prefix, it will not render the type attribute, rather
        /// it will translate the instance to a PART with no type and a qualifier of SFX or PFX. Conversely, whenever an instance is received that has no part type
        /// and a qualifier of SFX or PFX, then type is set to PFX or SFX and Qualifier is kept.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods"), Property(Name = "type", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("type")]
        public EntityNamePartType? Type { get; set; }
        /// <summary>
        /// The qualifier is a set of codes each of which specifies a certain sub-category
        /// </summary>
        /// <remarks>
        /// Qualifier is supported only by Data Types R2, however it modifies the manner in which Data Types R1 formatters 
        /// graph the data to the wire. This is because of changes to <see cref="T:EntityNamePartType"/> enumeration. The rules when formatting
        /// R1 are as follows:
        /// <list type="bullet">
        ///     <item><description>When Qualifier is set and is SUFFIX, no matter what <see cref="P:Type"/> is set to, the element is rendered as a Suffix</description></item>
        ///     <item><description>When Qualifier is set and is PREFIX, no matter what <see cref="P:Type"/> is set to, the element is rendered as a Prefix</description></item>
        /// </list>
        /// </remarks>
        [Property(Name = "qualifier", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("qualifier")]
        public SET<CS<EntityNamePartQualifier>> Qualifier { get; set; }
        /// <summary>
        /// Translate to the string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "enxp")]
        public static implicit operator string(ENXP enxp)
        {
            return enxp.Value;
        }
        /// <summary>
        /// Validate
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            var retVal = (NullFlavor != null) ^ (Value != null && ((CodeSystem != null && Code != null) ^ (Code == null)));
            foreach (var qlfr in this.Qualifier ?? new SET<CS<EntityNamePartQualifier>>())
            {
                if (!qlfr.Code.IsAlternateCodeSpecified)
                    retVal &= validation[qlfr].Contains(this.Type);
            }
            return retVal;
        }

        /// <summary>
        /// Extended validation function which returns the details of the validation
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = base.ValidateEx() as List<IResultDetail>;

            if (NullFlavor != null && Value != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ENXP", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (CodeSystem != null && Code == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ENXP", String.Format(ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "CodeSystem", "Code"), null));
            foreach (var q in this.Qualifier ?? new SET<CS<EntityNamePartQualifier>>())
                if (!q.Code.IsAlternateCodeSpecified && !validation[q].Contains(this.Type))
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ENXP", String.Format("Qualifier must be one of '{0}' when type is populated with '{1}'", Util.ToWireFormat(validation[q]), this.Type), null));

            return retVal;                
        }

        #region IEquatable<ENXP> Members

         /// <summary>
         /// Determine if this ENXP equals another instance of ENXP
         /// </summary>
        public bool Equals(ENXP other)
        {
            if (other != null)
                return base.Equals((ANY)other) &&
                    other.Code == this.Code &&
                    other.CodeSystem == this.CodeSystem &&
                    other.CodeSystemVersion == this.CodeSystemVersion &&
                    other.Value == this.Value &&
                    (other.Qualifier == null ? this.Qualifier == null : other.Qualifier.Equals(this.Qualifier)) &&
                    other.Type == this.Type;
            return false;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is ENXP)
                return Equals(obj as ENXP);
            return base.Equals(obj);
        }

        #endregion
    }
}