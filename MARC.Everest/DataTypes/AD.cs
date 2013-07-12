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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

#if WINDOWS_PHONE
using MARC.Everest.Phone;
#endif 

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Identifies how an address can be used
    /// </summary>
    /// <see cref="T:MARC.Everest.DataTypes.AD"/>
    /// <example>
    /// <code title="Creating multiple addresses for one person" lang="cs">
    /// <![CDATA[
    ///                 LIST<AD> personAddresses = new LIST<AD>(3);
    ///                 ///  Add a home address
    ///                 personAddresses.Add(
    ///                     new AD(PostalAddressUse.HomeAddress, 
    ///                     new ADXP[] { 
    ///                             new ADXP("123 Main Street", AddressPartType.StreetAddressLine),
    ///                             new ADXP("Hamilton", AddressPartType.City),
    ///                         new ADXP("Ontario", AddressPartType.State)
    ///                         }
    ///                     )
    ///                 ); 
    ///                 ///  Add a Business Address
    ///                 personAddresses.Add(
    ///                 new AD(PostalAddressUse.WorkPlace, 
    ///                         new ADXP[]{
    ///                         new ADXP("123 Fake Street", AddressPartType.StreetAddressLine),
    ///                             new ADXP("Hamilton", AddressPartType.City),
    ///                             new ADXP("Ontario", AddressPartType.State)
    ///                         }                   
    ///                     )
    ///                 ); 
    ///                 ///  Add a Direct Buisness Phone #
    ///                 personAddresses.Add(
    ///                     new AD(PostalAddressUse.Direct,
    ///                         new ADXP[]{
    ///                             new ADXP("555-867-5309")
    ///                     }
    ///                     )
    ///                 );
    ///                 foreach (AD address in personAddresses)
    ///                 {
    ///                     Console.WriteLine(address.ToString());
    ///                 }
    ///                 Console.ReadKey();
    /// ]]>
    /// </code>
    /// </example>
    [Structure(Name = "PostalAddressUse", CodeSystem = "2.16.840.1.113883.5.1012", StructureType = StructureAttribute.StructureAttributeType.ValueSet, Publisher = "Health Level 7 International")]
    [XmlType("PostalAddressUse", Namespace = "urn:hl7-org:v3")]
    public enum PostalAddressUse
    {
        /// <summary>
        /// A communication address at a home, attempted contacts for business purposes
        /// </summary>
        
        [Enumeration(Value = "H")]
        [XmlEnum("H")]
        HomeAddress,
        /// <summary>
        /// The primary home to reach a person after business hours
        /// </summary>
        [Enumeration(Value = "HP")]
        [XmlEnum("HP")]
        PrimaryHome,
        /// <summary>
        /// A vacation home
        /// </summary>
        [Enumeration(Value = "HV")]
        [XmlEnum("HV")]
        VacationHome,
        /// <summary>
        /// An office address
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "WorkPlace")]
        [Enumeration(Value = "WP")]
        [XmlEnum("WP")]
        WorkPlace,
        /// <summary>
        /// Indicates a work place address or a telecommunication address
        /// </summary>
        [Enumeration(Value = "DIR")]
        [XmlEnum("DIR")]
        Direct,
        /// <summary>
        /// Indicates a work place address or telecommunication address that is a standard
        /// </summary>
        [Enumeration(Value = "PUB")]
        [XmlEnum("PUB")]
        Public,
        /// <summary>
        /// A flag indicating that the address is bad
        /// </summary>
        [Enumeration(Value = "BAD")]
        [XmlEnum("BAD")]
        BadAddress,
        /// <summary>
        /// Used primarily to visit an address
        /// </summary>
        [Enumeration(Value = "PHYS")]
        [XmlEnum("PHYS")]
        PhysicalVisit,
        /// <summary>
        /// Used to send mail
        /// </summary>
        [Enumeration(Value = "PST")]
        [XmlEnum("PST")]
        PostalAddress,
        /// <summary>
        /// A temporary address may be good for visit or mailing
        /// </summary>
        [Enumeration(Value = "TMP")]
        [XmlEnum("TMP")]
        TemporaryAddress,
        /// <summary>
        /// Alphabetic transcription
        /// </summary>
        [Enumeration(Value = "ABC")]
        [XmlEnum("ABC")]
        Alphabetic,
        /// <summary>
        /// Address as understood by the datacentre
        /// </summary>
        [Enumeration(Value = "IDE")]
        [XmlEnum("IDE")]
        Ideographic,
        /// <summary>
        /// Syllabic translation of the address
        /// </summary>
        [Enumeration(Value = "SYL")]
        [XmlEnum("SYL")]
        Syllabic,
        /// <summary>
        /// An address spelled according to the soundex algorithm
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Soundex")]
        [Enumeration(Value = "SNDX")]
        [XmlEnum("SNDX")]
        Soundex,
        /// <summary>
        /// The address as understood by the datacentre
        /// </summary>
        [Enumeration(Value = "PHON")]
        [XmlEnum("PHON")]
        Phonetic
    }

    /// <summary>
    /// Mailing at home or office addresses is primarily used to communicate data that will 
    /// allow printing mail labels.
    /// </summary>
    /// <example>
    /// <code title="Instantiating an AD" lang="cs">
    /// <![CDATA[
    /// AD test = new AD( 
    /// PostalAddressUse.HomeAddress, 
    /// new ADXP[] { 
    ///         new ADXP("123", AddressPartType.BuildingNumber),  
    ///             new ADXP("Main", AddressPartType.StreetNameBase),  
    ///             new ADXP("Street", AddressPartType.StreetType), 
    ///             new ADXP("West", AddressPartType.Direction),  
    ///             new ADXP("Hamilton", AddressPartType.City),  
    ///             new ADXP("Ontario", AddressPartType.State),  
    ///             new ADXP("Canada", AddressPartType.Country), 
    ///             new ADXP("L8K 3K4", AddressPartType.PostalCode) 
    /// });
    /// 
    /// // Write to console
    /// Console.Write(test.ToString("{BNR} {STB} {STTYP} {DIR}\r\n{CTY}, {STA}\r\n{CNT}\r\n{ZIP}")); 
    /// 
    /// // Output: 
    /// // 123 Main Street West 
    /// // Hamilton, Ontario 
    /// // Canada 
    /// // L8K3K4 
    /// ]]>
    /// </code>
    /// </example>
    /// 
    /// <example>
    /// <code title="Using the AD(SET&lt;CS&lt;PostalAddressUse&gt;&gt; use, IEnumerable&lt;ADXP&gt; parts) Constructor" lang="cs">
    /// <![CDATA[
    ///
    /// // SET(IEnumerable collection, Comparison<T> comparator)
    ///        SET<CS<PostalAddressUse>> setCS01 = new SET<CS<PostalAddressUse>>(
    ///                                        new CS<PostalAddressUse>[]{
    ///                                        new CS<PostalAddressUse>(PostalAddressUse.WorkPlace),
    ///                                        new CS<PostalAddressUse>(PostalAddressUse.HomeAddress)
    ///                                        },
    ///                                        CS<PostalAddressUse>.Comparator);
    ///                                        
    ///        // Address Parts enumeration
    ///        ADXP[] addressParts01 = new ADXP[] {
    ///                        new ADXP("123", AddressPartType.BuildingNumber),
    ///                        new ADXP("Bromley", AddressPartType.StreetName)               
    ///                        };
    ///
    ///        // Two Parameter Constructor -- (SET<CS<PostalAddressUse>> use, IEnumerable<ADXP> parts)
    ///        AD test04 = new AD(setCS01, addressParts01);
    /// 
    /// 
    /// ]]>
    /// </code>
    ///</example>
    ///<remarks>
    ///<para>
    /// Mailing, home and office addresses are commonly communicated using the AD class. The AD data type
    /// is primarily intended to facilitate the printing of address labels or providing a location that an
    /// entity can be located.
    /// </para>
    /// <para>
    /// Although not recommended, the AD data type can also be used, in some cases, to facilitate the location
    /// of points of interest on a map (which require additional geocoding)
    /// </para>
    /// <para>An AD instance is considered valid when either the <see cref="P:ANY.NullFlavor"/> property is null
    /// or there is at least one <see cref="P:Part"/>.
    /// </para>
    /// </remarks>
    [XmlType("AD",Namespace="urn:hl7-org:v3")]
    [Structure(Name = "AD", StructureType = StructureAttribute.StructureAttributeType.DataType)]
#if !WINDOWS_PHONE
    [Serializable]
#endif

    public class AD : ANY, IEquatable<AD>
    {
        private static Dictionary<String, AddressPartType> PartTypeMap;

        /// <summary>
        /// Static CTOR
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static AD()
        {
            PartTypeMap = new Dictionary<string,AddressPartType>();

            foreach(FieldInfo fi in typeof(AddressPartType).GetFields())
                if (fi.GetCustomAttributes(typeof(EnumerationAttribute), true).Length > 0)
                {
                    EnumerationAttribute ea = fi.GetCustomAttributes(typeof(EnumerationAttribute), true)[0] as EnumerationAttribute;
                    PartTypeMap.Add(ea.Value, (AddressPartType)fi.GetValue(null));
                }
        }

        /// <summary>
        /// Create a new address instance
        /// </summary>
        public AD() : base() { Part = new List<ADXP>(); }

        /// <summary>
        /// Create a new address instance with the specified use
        /// </summary>
        /// <param name="use">The use that should constructed address should have</param>
        public AD(PostalAddressUse use) : this() { this.Use = new SET<CS<PostalAddressUse>>(use); }

        /// <summary>
        /// Create a new address instance using the parts specified
        /// </summary>
        /// <param name="parts">The parts to create</param>
        /// <example>
        /// <code title="Creating an AD using an existing array of parts" lang="cs">
        /// <![CDATA[
        /// List<ADXP> parts = new List<ADXP>() {
        ///     new ADXP(),
        ///     new ADXP()
        /// };
        /// AD myAddress = new AD(parts);
        /// ]]>
        /// </code>
        /// </example>
        public AD(IEnumerable<ADXP> parts)
            : this()
        {
            Part = new List<ADXP>(parts);
        }
        
        /// <summary>
        /// Create a new address instance using the parts and use specified
        /// </summary>
        /// <param name="parts">The parts to create</param>
        /// <param name="use">Contextual information about the postal address</param>
        /// <example>
        /// 
        /// </example>
        public AD(CS<PostalAddressUse> use, IEnumerable<ADXP> parts)
            : this(new SET<CS<PostalAddressUse>>(use, (a,b)=>((PostalAddressUse)a.Code).CompareTo(((PostalAddressUse)b.Code))),  parts)
        {
        }

        /// <summary>
        /// Helper method for creating AD instances
        /// </summary>
        /// <param name="use">The use of the returned address</param>
        /// <param name="parts">The parts that make up the address</param>
        public static AD CreateAD(PostalAddressUse use, params ADXP[] parts)
        {
            return new AD(use, parts);
        }

        /// <summary>
        /// Helper method for creating AD instances
        /// </summary>
        /// <param name="use">The use of the returned address</param>
        /// <param name="parts">The parts that make up the address</param>
        public static AD CreateAD(SET<PostalAddressUse> use, params ADXP[] parts)
        {
            return new AD(new SET<CS<PostalAddressUse>>(use), parts);
        }

        /// <summary>
        /// Helper method for creating AD instances
        /// </summary>
        /// <param name="use">The use of the returned address</param>
        /// <param name="parts">The parts that make up the address</param>
        public static AD CreateAD(params ADXP[] parts)
        {
            return new AD(parts);
        }

        /// <summary>
        /// Create a new address instance using the parse and uses specified
        /// </summary>
        /// <param name="use">The uses of the address</param>
        /// <param name="parts">The parts of the address</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public AD(SET<CS<PostalAddressUse>> use, IEnumerable<ADXP> parts)
            : this(parts)
        {
            Use = use;
        }

        /// <summary>
        /// A sequence of address parts that makes up the address
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), Property(Name = "part", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("part")]
        public List<ADXP> Part { get; private set; }

        /// <summary>
        /// A code advising a system or user which address in a set of like addresses to select for a 
        /// given purpose
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Property(Name = "use", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("use")]
        public SET<CS<PostalAddressUse>> Use { get; set; }

        /// <summary>
        /// A time specification that dictates the periods of time during which the address can be used.
        /// </summary>
        /// <example>
        /// <code title="Creating an address that is valid for the month of December" lang="cs">
        /// <![CDATA[
        /// 
        /// AD myAddress = new AD(
        ///     PostalAddressUse.VacationHome,
        ///     new ADXP[] {
        ///         new ADXP("123 Main Street West", AddressPartType.StreetAddressLine),
        ///         new ADXP("Thunder Bay", AddressPartType.City),
        ///         new ADXP("Ontario", AddressPartType.State)
        ///     }
        /// );
        /// myAddress.UseablePeriod = new SET<SXCM<TS>>((a,b)=>a.Value.DateValue.CompareTo(b.Value.DateValue));
        /// test.UseablePeriod.Add(
        ///     new IVL<TS>(DateTime.Parse("1/12/2010"), DateTime.Parse("31/12/2010"))
        /// );
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), Property(Name = "useablePeriod", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("usablePeriod")]
        public GTS UseablePeriod { get; set; }

        /// <summary>
        /// A boolean value specifying whether the order of the address parts is known or not
        /// </summary>
        [Property(Name = "isNotOrdered", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("isNotOrdered")]
        public bool? IsNotOrdered { get; set; }

        /// <summary>
        /// Validates that <paramref name="n"/> conforms to AD.Basic Flavor
        /// </summary>
        /// <param name="n">The AD to validate</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "n"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "n"), Flavor(Name = "BASIC")]
        public static bool IsValidBasicFlavor(AD n)
        {
            return n.Validate() &&
                !n.Part.Exists(o => o.Type != AddressPartType.City &&
                    o.Type != AddressPartType.State);
        }
        /// <summary>
        /// Create an AD instance from simple address data
        /// </summary>
        /// <param name="use">The intended use of the address</param>
        /// <param name="addressLine1">The first address line of the address</param>
        /// <param name="addressLine2">The second address line of the address</param>
        /// <param name="city">The city the address should carry</param>
        /// <param name="state">The state the address should carry</param>
        /// <param name="country">The country the address should carry</param>
        /// <param name="zip">The zip code the address should carry</param>
        public static AD FromSimpleAddress(PostalAddressUse use, String addressLine1, String addressLine2, String city, String state, String country, String zip)
        {
            // Sanity check
            AD retVal = AD.CreateAD(use);

            if (!String.IsNullOrEmpty(addressLine1))
                retVal.Part.Add(new ADXP(addressLine1, AddressPartType.AddressLine));
            if (!String.IsNullOrEmpty(addressLine2))
                retVal.Part.Add(new ADXP(addressLine2, AddressPartType.AddressLine));
            if(!String.IsNullOrEmpty(city))
                retVal.Part.Add(new ADXP(city, AddressPartType.City));
            if(!String.IsNullOrEmpty(state))
                retVal.Part.Add(new ADXP(state, AddressPartType.State));
            if(!String.IsNullOrEmpty(country))
                retVal.Part.Add(new ADXP(country, AddressPartType.Country));
            if(!String.IsNullOrEmpty(zip))
                retVal.Part.Add(new ADXP(zip, AddressPartType.PostalCode));
            return retVal;
        }
        /// <summary>
        /// Represent this address as a string. Parts are formatted using the standard string.format notation
        /// </summary>
        /// <example>
        /// <code title="Writing AD structure to console" lang="cs">
        /// <![CDATA[
        /// Console.Write(test.ToString("{BNR} {STB} {STTYP} {DIR}\r\n{CTY}, 
        /// {STA}\r\n{CNT}\r\n{ZIP}")); 
        /// // Output: 
        /// // 123 Main Street West 
        /// // Hamilton, Ontario 
        /// // Canada 
        /// // L8K3K4 
        /// ]]>
        /// </code>
        /// </example>
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
                AddressPartType adpt = PartTypeMap[tag];

                string partString = "";
                foreach (ADXP adxp in Part.FindAll(o => o.Type == adpt))
                    partString += adxp.Value + " "; 

                format = format.Replace("{" + tag + "}", partString);
                
            }
            return format.Remove(format.Length - 1);
        }

        /// <summary>
        /// Represent this address as a string. Parts appear in whatever order they appear in the sequence
        /// </summary>
        /// <remarks>
        /// The formatting string is referenced by <see cref="T:MARC.Everest.DataTypes.AddressPartType"/>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor")]
        public override string ToString()
        {
            StringWriter sw = new StringWriter();

            // Iterate through the sequence
            foreach (ADXP p in Part)
            {
                if(p.Type == null)
                    sw.Write(p.Value);
                else
                    switch ((AddressPartType)p.Type)
                    {
                        case AddressPartType.Delimiter:
                            sw.Write(p.Value == String.Empty ? "\n" : p.Value);
                            break;
                        default:
                            sw.Write(p.Value);
                            break;
                    }
            }

            return sw.ToString();
        }

        /// <summary>
        /// Validate the address
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Type is valid if:
        ///     <list type="bullet">
        ///         <item>NullFlavor is not assigned, XOR</item>
        ///         <item>There is at least one part</item>
        ///     </list>
        /// </remarks>
        public override bool Validate()
        {
            // JF - BUG : 
//            return false;
            return (NullFlavor != null) ^ (Part.Count > 0);
        }

        /// <summary>
        /// Extended validation method that returns the results of validation
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());
            if (!((this.NullFlavor != null) ^ (this.Part.Count > 0)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "AD", "NullFlavor must be specified, or more than one Part must be present", null));
            return retVal;
        }

        #region IEquatable<AD> Members

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is AD)
                return Equals(obj as AD);
            return base.Equals(obj);
        }

        /// <summary>
        /// Determine if this AD equals another
        /// </summary>
        public bool Equals(AD other)
        {
            if (other != null)
            {
                bool result = other.IsNotOrdered == this.IsNotOrdered &&
                    (other.Use != null ? other.Use.Equals(this.Use) : this.Use == null) &&
                    (other.UseablePeriod != null ? other.UseablePeriod.Equals(this.UseablePeriod) : this.UseablePeriod == null);
                foreach(var part in this.Part)
                    result &= other.Part.Exists(o=>o.Equals(part));
                return result && base.Equals((ANY)other) && this.Part.Count == other.Part.Count;
            }
            return false;
                
        }

        #endregion

        #region ISemanticEquatable<AD> Members

        /// <summary>
        /// Determines if this AD is semantically equal to <paramref name="other"/>
        /// </summary>
        /// <remarks>Two non-null, non-null flavored instances of AD are semantically equal when they contain
        /// the same parts regardless of order, and the <see cref="P:IsNotOrdered"/> and <see cref="P:Use"/> properties are equal to <paramref name="other"/></remarks>
        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;

            bool result = true;
            AD otherAd = other as AD;
            if ((this.Part == null) ^ (otherAd.Part == null))
                return false;
            else if (this.Part == otherAd.Part)
                return true;
            else
            {
                foreach (var part in this.Part)
                    result &= otherAd.Part.Exists(o => o.Equals(part));
                return result && this.Part.Count == otherAd.Part.Count;
            }

        }

        #endregion
    }
}
