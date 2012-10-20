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
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Enumeration of entity name uses.
    /// </summary>
    [XmlType("EntityNameUse", Namespace = "urn:hl7-org:v3")]
    [Structure(Name = "EntityNameUse", CodeSystem = "2.16.840.1.113883.5.1120", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    public enum EntityNameUse
    {
        /// <summary>
        /// The name the entity uses in a legal context.
        /// </summary>
        [Enumeration(Value = "L")]
        [XmlEnum("L")]
        Legal,
        /// <summary>
        /// The formal name as registered in an official registry
        /// </summary>
        [Enumeration(Value = "OR")]
        [XmlEnum("OR")]
        OfficialRecord,
        /// <summary>
        /// As recorded on a license, record, certificate
        /// </summary>
        [Enumeration(Value = "C")]
        [XmlEnum("C")]
        License,
        /// <summary>
        /// A name used prior to marriage
        /// </summary>
        /// <remarks>
        /// This use code is used for applications that collect and store maiden names
        /// </remarks>
        [Enumeration(Value = "M")]
        [XmlEnum("M")]
        MaidenName,
        /// <summary>
        /// eg: (From HL7 ISO DOC) Chief Red Cloud
        /// </summary>
        [Enumeration(Value = "I")]
        [XmlEnum("I")]
        Indigenous,
        /// <summary>
        /// A self asserted name that the person is using
        /// </summary>
        [Enumeration(Value = "P")]
        [XmlEnum("P")]
        Pseudonym,
        /// <summary>
        /// Includes writer's pseudonym/stage name
        /// </summary>
        [Enumeration(Value = "A")]
        [XmlEnum("A")]
        Artist,
        /// <summary>
        /// eg: Sister Mary Francis
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Religous")]
        [Enumeration(Value = "R")]
        [XmlEnum("R")]
        Religious,
        /// <summary>
        /// A named assigned to a person. This is an ALIAS
        /// </summary>
        [Enumeration(Value = "ASGN")]
        [XmlEnum("ASGN")]
        Assigned,
        /// <summary>
        /// Alphabetic transcription (eg: Romaji in Japan)
        /// </summary>
        [Enumeration(Value = "ABC")]
        [XmlEnum("ABC")]
        Alphabetic,
        /// <summary>
        /// Ideographic representation (eg: Kanji in Japan)
        /// </summary>
        [Enumeration(Value = "IDE")]
        [XmlEnum("IDE")]
        Ideographic,
        /// <summary>
        /// Syllabic transcription (eg: Hangul in Korea)
        /// </summary>
        [Enumeration(Value = "SYL")]
        [XmlEnum("SYL")]
        Syllabic,
        /// <summary>
        /// An address spelled according to the SoundEx algorithm
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Soundex")]
        [Enumeration(Value = "SNDX")]
        [XmlEnum("SNDX")]
        Soundex,
        /// <summary>
        /// The address as understood by the datacenter. (eg: A close proximity to)
        /// </summary>
        [Enumeration(Value = "PHON")]
        [XmlEnum("PHON")]
        Phonetic,
        /// <summary>
        /// Anonymously assigned name
        /// </summary>
        [Enumeration(Value = "ANON")]
        [XmlEnum("ANON")]
        Anonymous,
        /// <summary>
        /// A name intended to be used in search or matching
        /// </summary>
        [Enumeration(Value = "SRCH")]
        [XmlEnum("SRCH")]
        Search
    }

    /// <summary>
    /// A name for a person, organization, place or thing
    /// </summary>
    /// <example>
    /// <code lang="cs" title="Using EN">
    /// <![CDATA[
    /// // EntityNameUse: as recorded on a license
    /// EN name = new EN(
    /// EntityNameUse.Legal, new ENXP[] { 
    /// new ENXP("James", EntityNamePartType.Given), 
    /// new ENXP("Tiberius", EntityNamePartType.Given), 
    /// new ENXP("Kirk", EntityNamePartType.Family) });
    ///
    /// LIST<ENXP> part = new LIST<ENXP>(new ENXP[] {
    /// new ENXP("Tiberius", EntityNamePartType.Given),
    /// new ENXP("Kirk", EntityNamePartType.Prefix)});
    /// part[0].Qualifier = EntityNamePartQualifier.Academic;
    /// part[1].Qualifier = EntityNamePartQualifier.Nobility;
    /// name.Part.Add(part[0]);
    /// name.Part.Add(part[1]);
    /// Console.Write(name.ToString("{FAM}, {GIV}"));
    /// ]]>
    /// </code>
    /// </example>
    [FlavorMap(FlavorId = "PN", Implementer = typeof(PN))]
    [FlavorMap(FlavorId = "TN", Implementer = typeof(TN))]
    [FlavorMap(FlavorId = "ON", Implementer = typeof(ON))]
    [Serializable][Structure(Name = "EN", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("EN", Namespace = "urn:hl7-org:v3")]
    public class EN : ANY, IEquatable<EN>
    {
        /// <summary>
        /// Part type map
        /// </summary>
        private static Dictionary<String, EntityNamePartType> PartTypeMap;
        
        /// <summary>
        /// Static CTOR
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static EN()
        {
            
            PartTypeMap = new Dictionary<string,EntityNamePartType>();

            foreach (FieldInfo fi in typeof(EntityNamePartType).GetFields())
                if (fi.GetCustomAttributes(typeof(EnumerationAttribute), true).Length > 0)
                {
                    EnumerationAttribute ea = fi.GetCustomAttributes(typeof(EnumerationAttribute), true)[0] as EnumerationAttribute;
                    PartTypeMap.Add(ea.Value, (EntityNamePartType)fi.GetValue(null));
                }
        }

        /// <summary>
        /// Create a new instance of EN
        /// </summary>
        public EN() : base() { Part = new List<ENXP>(); }
        /// <summary>
        /// Create a new entity named instance using the specified values
        /// </summary>
        /// <param name="parts">The parts of the names</param>
        /// <param name="use">The uses of this name</param>
        public EN(EntityNameUse use, IEnumerable<ENXP> parts) : this() { Part = new List<ENXP>(parts); this.Use = new SET<CS<EntityNameUse>>(use); }

        /// <summary>
        /// Helper method for creating EN instances
        /// </summary>
        /// <param name="use">The use of the returned name</param>
        /// <param name="parts">The parts that make up the name</param>
        public static EN CreateEN(EntityNameUse use, params ENXP[] parts)
        {
            return new EN(use, parts);
        }

        /// <summary>
        /// The uses of this name
        /// </summary>
        [Property(Name = "use", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("use")]
        public virtual SET<CS<EntityNameUse>> Use { get; set; }
        /// <summary>
        /// Gets the parts that make up this entity name
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), Property(Name = "part", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional,
            MinOccurs = 0, MaxOccurs = -1)]
        [XmlElement("part")]
        public List<ENXP> Part { get; protected set; }

        /// <summary>
        /// Validates this EntityName to ensure that it represents a valid instance of EN
        /// </summary>
        /// <remarks>
        /// An instance of EN is valid if:
        /// <list type="bullet">
        ///     <list>NullFlavor is specified, XOR</list>
        ///     <list>The Name contains at least one part</list>
        /// </list>
        /// </remarks>
        public override bool Validate()
        {
            return (Part.Count > 0) ^ (NullFlavor != null);
        }

        /// <summary>
        /// Extended validation function which returns the details of the validation
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = base.ValidateEx() as List<IResultDetail>;
            if (this.NullFlavor != null && this.Part.Count > 0)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "EN", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            else if (this.Part.Count == 0 && this.NullFlavor == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "EN", ValidationMessages.MSG_NULLFLAVOR_MISSING, null));

            // Validate parts
            foreach (var pt in this.Part)
                retVal.AddRange(pt.ValidateEx());

            return retVal;
        }

        /// <summary>
        /// Represent this entity name as a string. Parts are formatted using the standard string.format notation
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String,System.Int32)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
        public string ToString(string format)
        {
            while (format.Contains("{"))
            {
                // Get the first tag
                int sPos = format.IndexOf("{"), ePos = format.IndexOf("}", sPos);
                string tag = format.Substring(sPos + 1, ePos - sPos - 1);
                // Get enumerated value
                EntityNamePartType adpt = PartTypeMap[tag];

                string partString = "";
                foreach (ENXP enxp in Part.FindAll(o => o.Type == adpt))
                    partString += String.Format("{0} ", enxp.Value);

                format = format.Replace("{" + tag + "}", partString);
                
            }

            return format.Remove(format.Length - 1);
        }

        /// <summary>
        /// Represent this address as a string. Parts appear in whatever order they appear in the sequence
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor")]
        public override string ToString()
        {
            StringWriter sw = new StringWriter();

            // Iterate through the sequence
            foreach (ENXP p in Part)
            {
                if (p.Type == null)
                    sw.Write(p.Value);
                else
                    switch ((EntityNamePartType)p.Type)
                    {
                        case EntityNamePartType.Delimiter:
                            sw.Write(p.Value == String.Empty ? "\n" : p.Value);
                            break;
                        default:
                            sw.Write(p.Value);
                            break;
                    }
            }

            return sw.ToString();
        }

        #region IEquatable<EN> Members

        /// <summary>
        /// Determine if this EN equals another instance of EN
        /// </summary>
        public bool Equals(EN other)
        {
            if (other != null)
            {
                bool result = base.Equals((ANY)other) &&
                    (other.Use == null ? this.Use == null : this.Use.Equals(other.Use));
                foreach (var part in this.Part)
                    result &= other.Part.Find(o => o.Equals(part)) != null;
                return result && this.Part.Count == other.Part.Count;
            }
            return false;
                    
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is EN)
                return Equals(obj as EN);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Determine semantic equality of this type
        /// </summary>
        /// <remarks>Two non-null non-nullflavored instance of EN are considered semantically equal when
        /// they both contain the same parts in the same order.</remarks>
        public override BL SemanticEquals(MARC.Everest.DataTypes.Interfaces.IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;

            bool result = true;
            EN otherAd = other as EN;
            if ((this.Part == null) ^ (otherAd.Part == null))
                return false;
            else if (this.Part == otherAd.Part)
                return true;
            else
            {
                for (int i = 0; i < this.Part.Count; i++)
                    result &= otherAd.Part[i].Equals(this.Part[i]);
                return result && this.Part.Count == otherAd.Part.Count;
            }
        }
    }
}