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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Attributes;
using System.Xml.Serialization;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// A telephone number, voice, fax, email address, or other locator used for a resouce mediated by 
    /// telecommunications equipment. 
    /// </summary>
    /// <example>
    /// <code lang="cs" title="TEL flavors">
    /// <![CDATA[
    ///        string url = "mailto:b_vanarragon@hotmail.com"; // valid e-mail
    ///        string url2 = "http://www.tsn.ca"; // valid web address
    ///        string url3 = "tel:+1905-389-6509"; // valid phone number
    ///
    ///        TEL urlTEL = url; //implicit conversion from string to TEL
    ///        TEL urlTEL2 = url2;
    ///        TEL urlTEL3 = url3;
    ///
    ///        Console.WriteLine(TEL.EMail(urlTEL)); //should return false, urlTEL is not an email
    ///        Console.WriteLine(TEL.Phone(urlTEL3)); //should return false, urlTEL is not a phone number
    ///        Console.WriteLine(TEL.Url(urlTEL2)); //should return true, is a valid URL
    ///        Console.ReadKey();
    ///]]>
    ///</code>
    ///</example>
    ///<remarks>
    ///The address is specified as a universal resource locator qualified
    /// by time specification and use codes that help in deciding which address to use for a given time 
    /// and purpose
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL")]
    [Structure(Name = "TEL", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("TEL", Namespace = "urn:hl7-org:v3")]
    [TypeMap(Name = "URL")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class TEL : PDV<String>, ITelecommunicationAddress, IEquatable<TEL>
    {

        /// <summary>
        /// Creates a new instance of the the telecommunications address class
        /// </summary>
        public TEL() { }
        /// <summary>
        /// Creates a new telecommunications address with the specified <paramref name="value"/>
        /// </summary>
        /// <param name="value">The initial value of the telecommunications addressadhere to RFC2396 <a href="http://www.ietf.org/rfc/rfc2396.txt">http://www.ietf.org/rfc/rfc2396.txt</a></param>
        public TEL(String value) : base(value) { }
        /// <summary>
        /// Creates a new telecommunications address with the specified <paramref name="value"/> and
        /// <paramref name="use"/>
        /// </summary>
        /// <param name="value">The initial value of the telecommunications addressadhere to RFC2396 <a href="http://www.ietf.org/rfc/rfc2396.txt">http://www.ietf.org/rfc/rfc2396.txt</a></param>
        /// <param name="use">A value describing the conditions under which the address can be used</param>
        public TEL(String value, TelecommunicationAddressUse use) : this(value, new CS<TelecommunicationAddressUse>[] { use }) { }
        /// <summary>
        /// Creates a new telecommunications address with the specified <paramref name="value"/> and
        /// <paramref name="use"/> set.
        /// </summary>
        /// <param name="value">The initial value of the telecommunications address, adhere to RFC2396 <a href="http://www.ietf.org/rfc/rfc2396.txt">http://www.ietf.org/rfc/rfc2396.txt</a></param>
        /// <param name="use">A set of unique values describing the use of the telecommunications address</param>
        public TEL(String value, IEnumerable<CS<TelecommunicationAddressUse>> use) : this(value)
        {
            this.Use = new SET<CS<TelecommunicationAddressUse>>(use);
        }
        /// <summary>
        /// Creates a new telecommunications address with the specified <paramref name="value"/> 
        /// </summary>
        /// <param name="value">The initial value of the telecommunications address</param>
        public TEL(Uri value)
            : this(value.ToString())
        { }
        /// <summary>
        /// Creates a new telecommunications address with the specified <paramref name="value"/> and
        /// <paramref name="use"/> set.
        /// </summary>
        /// <param name="value">The initial value of the telecommunications address</param>
        /// <param name="use">A set of unique values describing the use of the telecommunications address</param>
        public TEL(Uri value, TelecommunicationAddressUse use)
            : this(value.ToString(), use)
        { }
        /// <summary>
        /// Creates a new telecommunications address with the specified <paramref name="value"/> and
        /// <paramref name="use"/> set.
        /// </summary>
        /// <param name="value">The initial value of the telecommunications address</param>
        /// <param name="use">A set of unique values describing the use of the telecommunications address</param>
        public TEL(Uri value, IEnumerable<CS<TelecommunicationAddressUse>> use)
            : this(value.ToString(), use)
        { }

        /// <summary>
        /// Identifies the value of the telecommunications address. Note that valid telecommunications
        /// address values must adhere to RFC2396 <a href="http://www.ietf.org/rfc/rfc2396.txt">http://www.ietf.org/rfc/rfc2396.txt</a>
        /// </summary>
        [Property(Name = "value", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public override string Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        /// <summary>
        /// One or more codes advising system or use which telecommunication address in a set of like addresses
        /// should be used and for what purpose
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Property(Name = "use", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("use")]
        public SET<CS<TelecommunicationAddressUse>> Use { get; set; }

        /// <summary>
        /// Advises processing systems of the capabilities of a particular 
        /// telecommunications address.
        /// </summary>
        [Property(Name= "capabilities", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("capabilities")]
        public SET<CS<TelecommunicationCabability>> Capabilities { get; set; }

        /// <summary>
        /// Specifies the periods of time during which the telecommunication address can be used
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Property(Name = "useablePeriod", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("usablePeriod")]
        public GTS UseablePeriod { get; set; }

        /// <summary>
        /// TEL.URL Validator
        /// </summary>
        /// <param name="tel">The TEL structure to validate</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), Flavor(Name = "Url")]
        [Flavor("TEL.URL")]
        public static bool IsValidUrlFlavor(TEL tel)
        {
            // Nothing to validate
            if (tel.Value == null) return true;

            string[] allowedSchemes = new string[] { "file:", "nfs://", "ftp://", "cid://", "http://", "https://" };
            bool valid = tel.Use == null;
            foreach (string s in allowedSchemes)
                if (((string)tel).StartsWith(s))
                    return valid;
            return false;
        }

        /// <summary>
        /// Link to URL
        /// </summary>
        [Flavor(Name = "Uri")]
        [Flavor(Name = "TEL.URI")]
        public static bool IsValidUriFlavor(TEL tel)
        {
            return IsValidUrlFlavor(tel);
        }

        /// <summary>
        /// TEL.Person Validator
        /// </summary>
        /// <param name="tel">The TEL structure to validate</param>
        [Flavor(Name = "Person")]
        [Flavor(Name = "TEL.PERSON")]
        public static bool IsValidPersonFlavor(TEL tel)
        {
            return IsValidPhoneFlavor(tel) || IsValidEMailFlavor(tel);
        }

        /// <summary>
        /// TEL.Phone validator
        /// </summary>
        /// <param name="tel">The tel structure to validate</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), Flavor(Name = "Phone")]
        [Flavor("TEL.PHONE")]
        public static bool IsValidPhoneFlavor(TEL tel)
        {
            string[] validSchemes = new string[] { "tel", "x-text-fax", "x-text-tel" };
            foreach (string s in validSchemes)
                if (tel.Value != null && tel.Value.StartsWith(s))
                    return true;
            return false;
        }

        /// <summary>
        /// TEL.Email validator
        /// </summary>
        /// <param name="tel">The tel structure to validate</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "EMail"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), Flavor(Name = "Email")]
        [Flavor("TEL.EMAIL")]
        public static bool IsValidEMailFlavor(TEL tel)
        {
            return tel.Value != null && tel.Value.StartsWith("mailto:");
        }

        #region ITelecommunicationAddress Members

        ISet<CS<TelecommunicationAddressUse>> ITelecommunicationAddress.Use
        {
            get
            {
                return Use;
            }
        }

        #endregion

        #region Operators
        /// <summary>
        /// Converts a <see cref="TEL"/> to a <see cref="String"/>
        /// </summary>
        /// <param name="o">TEL to convert</param>
        /// <returns>Converted String</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator String(TEL o)
        {
            return o.Value;
        }
        /// <summary>
        /// Converts a <see cref="T:TEL"/> to a <see cref="T:System.String"/>
        /// </summary>
        public static explicit operator Uri(TEL o)
        {
            return new Uri(o);
        }
        /// <summary>
        /// Converts a <see cref="T:System.Uri"/> to a <see cref="T:TEL"/>
        /// </summary>
        public static implicit operator TEL(Uri o)
        {
            return new TEL(o);
        }
        /// <summary>
        /// Converets a <see cref="String"/> to a <see cref="TEL"/>
        /// </summary>
        /// <param name="o">String to convert</param>
        /// <returns>Converted TEL</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator TEL(String o)
        {
            TEL retVal = new TEL();
            retVal.Value = o;
            return retVal;
        }
        #endregion

        #region IEquatable<TEL> Members

        /// <summary>
        /// Determeins if this TEL is equal to another TEL
        /// </summary>
        public bool Equals(TEL other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((PDV<String>)other) &&
                    (other.Use != null ? other.Use.Equals(this.Use) : this.Use == null) &&
                    (other.UseablePeriod != null ? other.UseablePeriod.Equals(this.UseablePeriod) : this.UseablePeriod == null) &&
                    (other.Capabilities != null ? other.Capabilities.Equals(this.Capabilities) : this.Capabilities == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is TEL || obj is string || obj is Uri)
                return Equals(obj as TEL);
            
            return base.Equals(obj);
        }

        #endregion

        #region ISemanticEquatable<TEL> Members

        /// <summary>
        /// Semantically equal?
        /// </summary>
        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;
            return this.Value == ((TEL)other).Value;
        }

        #endregion

        #region ITelecommunicationAddress Members

        /// <summary>
        /// Gets the usable period as an ISetComponent
        /// </summary>
        ISetComponent<IPointInTime> ITelecommunicationAddress.UseablePeriod
        {
            get { return this.UseablePeriod; }
        }

        #endregion
    }
}