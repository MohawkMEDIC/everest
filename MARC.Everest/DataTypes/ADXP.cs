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
using System.Xml.Serialization;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// Identifies how an address part contributes to the address. 
    /// </summary>
    /// <example>
    /// In the address "123 Main Street W, Hamilton ON", the following address parts contribute to the address
    /// <list type="bullet">
    ///     <item><b>123</b> - Building Number</item>
    ///     <item><b>Main</b> - Street Name</item>
    ///     <item><b>Street</b> - Street Type</item>
    ///     <item><b>West</b> - Direction</item>
    ///     <item><b>Hamilton</b> - City</item>
    ///     <item><b>ON</b> - State</item>
    /// </list>
    /// </example>
    [Structure(Name = "AddressPartType", CodeSystem = "2.16.840.1.113883.5.16", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    [XmlType("AddressPartType", Namespace = "urn:hl7-org:v3")]
    public enum AddressPartType
    {
        /// <summary>
        /// An address line is for either an additional locator, a delivery address or a street address.
        /// </summary>
        [Enumeration(Value = "AL")]
        [XmlEnum("AL")]
        AddressLine,
        /// <summary>
        /// This can be a unit designator such as apartment number, suite, etc...
        /// </summary>
        [Enumeration(Value = "ADL")]
        [XmlEnum("ADL")]
        AdditionalLocator,
        /// <summary>
        /// The number or name of a specific unit contained within a building
        /// </summary>
        [Enumeration(Value = "UNID")]
        [XmlEnum("UNID")]
        UnitIdentifier,
        /// <summary>
        /// Indicates the type of specific unit contained within a building
        /// </summary>
        [Enumeration(Value = "UNIT")]
        [XmlEnum("UNIT")]
        UnitDesignator,
        /// <summary>
        /// A delivery address line is frequently used instead of breaking out delivery mode, 
        /// delivery installation, etc...
        /// </summary>
        [Enumeration(Value = "DAL")]
        [XmlEnum("DAL")]
        DeliveryAddressLine,
        /// <summary>
        /// Indicates the type of delivery installation (facility to which the mail will be delivered prior
        /// to final shipping)
        /// </summary>
        [Enumeration(Value = "DINST")]
        [XmlEnum("DINST")]
        DeliveryInstallationType,
        /// <summary>
        /// The location of the delivery location, usually a town or city
        /// </summary>
        [Enumeration(Value = "DINSTA")]
        [XmlEnum("DINSTA")]
        DeliveryInstallationArea,
        /// <summary>
        /// A number, letter or name identifying a delivery location (eg: Station A)
        /// </summary>
        [Enumeration(Value = "DINSTQ")]
        [XmlEnum("DINSTQ")]
        DeliveryInstallationQualifier,
        /// <summary>
        /// Indicates the type of service offered, method of delivery. Example: A PO box 
        /// </summary>
        [Enumeration(Value = "DMOD")]
        [XmlEnum("DMOD")]
        DeliveryMode,
        /// <summary>
        /// Represents the routing information such as a letter carrier route number
        /// </summary>
        [Enumeration(Value = "DMODID")]
        [XmlEnum("DMODID")]
        DeliveryModeIdentifier,
        /// <summary>
        /// A full streeet address line, including number details and the street name and type 
        /// as appropriate
        /// </summary>
        [Enumeration(Value = "SAL")]
        [XmlEnum("SAL")]
        StreetAddressLine,
        /// <summary>
        /// The number of a building, house or lot alongside the street
        /// </summary>
        [Enumeration(Value = "BNR")]
        [XmlEnum("BNR")]
        BuildingNumber,
        /// <summary>
        /// The numeric portion of a building number
        /// </summary>
        [Enumeration(Value = "BNN")]
        [XmlEnum("BNN")]
        BuildingNumberNumeric,
        /// <summary>
        /// Any alphabetic character, fraction or other text that may appear after 
        /// the numeric portion of a building number
        /// </summary>
        [Enumeration(Value = "BNS")]
        [XmlEnum("BNS")]
        BuildingNumberSuffix,
        /// <summary>
        /// The name of the street including the type
        /// </summary>
        [Enumeration(Value = "STR")]
        [XmlEnum("STR")]
        StreetName,
        /// <summary>
        /// The base name of a roadway, or artery recognized by a municipality
        /// </summary>
        [Enumeration(Value = "STB")]
        [XmlEnum("STB")]
        StreetNameBase,
        /// <summary>
        /// The designation given to the street (avenue, crescent, street, etc)
        /// </summary>
        [Enumeration(Value = "STTYP")]
        [XmlEnum("STTYP")]
        StreetType,
        /// <summary>
        /// The direction (N,W,S,E)
        /// </summary>
        [Enumeration(Value = "DIR")]
        [XmlEnum("DIR")]
        Direction,
        /// <summary>
        /// The name of the party who will take receipt at the specified address
        /// </summary>
        [Enumeration(Value = "CAR")]
        [XmlEnum("CAR")]
        CareOf,
        /// <summary>
        /// A geographic sub-unit delineated for demographic purposes
        /// </summary>
        [Enumeration(Value = "CEN")]
        [XmlEnum("CEN")]
        CensusTract,
        /// <summary>
        /// Country
        /// </summary>
        [Enumeration(Value = "CNT")]
        [XmlEnum("CNT")]
        Country,
        /// <summary>
        /// A sub-unit of a state or province (49 of the United States of America uses county while
        /// Louisiana uses the term "parish")
        /// </summary>
        [Enumeration(Value = "CPA")]
        [XmlEnum("CPA")]
        County,
        /// <summary>
        /// The name of the city, town, village, etc...
        /// </summary>
        [Enumeration(Value = "CTY")]
        [XmlEnum("CTY")]
        City,
        /// <summary>
        /// Delimiters are printed without framing white space
        /// </summary>
        [Enumeration(Value = "DEL")]
        [XmlEnum("DEL")]
        Delimiter,
        /// <summary>
        /// A numbered box located in a post station
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "PostBox")]
        [Enumeration(Value = "POB")]
        [XmlEnum("POB")]
        PostBox,
        /// <summary>
        /// A subsection of a municipality
        /// </summary>
        [Enumeration(Value = "PRE")]
        [XmlEnum("PRE")]
        Precinct,
        /// <summary>
        /// A sub-unit of a country
        /// </summary>
        [Enumeration(Value = "STA")]
        [XmlEnum("STA")]
        State,
        /// <summary>
        /// A postal code designating a region defined by the post service.
        /// </summary>
        [Enumeration(Value = "ZIP")]
        [XmlEnum("ZIP")]
        PostalCode
    }

    /// <summary>
    /// A character string that may have a type-tag signifying its role in the address.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ADXP"), Serializable]
    [Structure(Name = "ADXP", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("ADXP", Namespace = "urn:hl7-org:v3")]
    public class ADXP : ANY, IEquatable<ADXP>
    {
        /// <summary>
        /// Create a new instance of ADXP
        /// </summary>
        public ADXP() : base() { }
        /// <summary>
        /// Create a new instance of ADXP using the values specified
        /// </summary>
        /// <param name="type">The address part type</param>
        /// <param name="value">The value of the type</param>
        public ADXP(String value, AddressPartType type)
            : base()
        {
            Type = type;
            Value = value;
        }
        /// <summary>
        /// Create a new instance of ADXP using the value specified
        /// </summary>
        /// <param name="value">The value of the ADXP</param>
        /// <remarks>Type is set to "AddressLine"</remarks>
        public ADXP(String value) : base()
        {
            Value = value;
        }

        /// <summary>
        /// Convert a string into an address part
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static implicit operator ADXP(string s)
        {
            return new ADXP(s);
        }

        /// <summary>
        /// Convert an ADXP into a string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public static implicit operator String(ADXP a)
        {
            return a.Value;
        }

        /// <summary>
        /// The actual string value of the part
        /// </summary>
        [Property(Name = "value", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("value")]
        public string Value { get; set; }
        /// <summary>
        /// A code assigned to the part by some coding system if appropriate
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
        /// The version of the coding system if required
        /// </summary>
        [Property(Name = "codeSystemVersion", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("codeSystemVersion")]
        public string CodeSystemVersion { get; set; }
        /// <summary>
        /// Specified whether an address part names the street, city, country, etc.. If not specified, the
        /// address will appear on the label as is
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods"), Property(Name = "type", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("type")]
        public AddressPartType? Type { get; set; }

        /// <summary>
        /// Represent this value as a string
        /// </summary>
        public override string ToString()
        {
            return (string)this ;
        }

        /// <summary>
        /// Validate this type
        /// </summary>
        /// <remarks>An address part type is valid if:
        ///     <list type="bullet">
        ///         <item>NullFlavor is not assigned, XOR</item>          
        ///         <item>Value property has a value, AND</item>
        ///         <item>Code AND CodeSystem are Assigned OR Code is not assigned, AND</item>
        ///         <item>CodeSystemVersion AND CodeSystem are Assigned OR CodeSystemVersion is not assigned</item>             
        ///     </list>
        /// </remarks>
        public override bool Validate()
        {
            return (NullFlavor != null) ^ (Value != null && ((Code != null && CodeSystem != null) || Code == null) &&
                ((CodeSystemVersion != null && CodeSystem != null) || CodeSystemVersion == null));
        }

        /// <summary>
        /// Extended validation which returns the results of the validation
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());
            if (this.CodeSystemVersion != null && this.CodeSystem == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ADXP", "When CodeSystemVersion is specified, a CodeSystem must also be specified", null));
            if (this.Code != null && this.CodeSystem == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ADXP", ValidationMessages.MSG_CODE_REQUIRES_CODESYSTEM, null));
            if (this.NullFlavor != null && this.Value == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ADXP", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (this.Value == null && this.NullFlavor == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ADXP", ValidationMessages.MSG_NULLFLAVOR_MISSING, null));
            return retVal;
        }
        #region IEquatable<ADXP> Members

        /// <summary>
        /// Determines if this ADXP equals another
        /// </summary>
        public bool Equals(ADXP other)
        {
            if (other != null)
            {
                return base.Equals((ANY)other) &&
                    other.Code == this.Code &&
                    other.CodeSystem == this.CodeSystem &&
                    other.CodeSystemVersion == this.CodeSystemVersion &&
                    other.Value == this.Value &&
                    other.Type == this.Type;
            }
            return false;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is ADXP)
                return Equals(obj as ADXP);
            return base.Equals(obj);
        }

        #endregion
    }
}
