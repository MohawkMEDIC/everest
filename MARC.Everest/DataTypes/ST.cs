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
using System.Globalization;
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// The character string datatype stands for text data primarily intended for machine processing
    /// </summary>
    /// <example>Using the <see cref="T:MARC.Everest.DataTypes.ST"/> class
    /// <code lang="cs" title="ST datatype">
    /// <![CDATA[
    /// // String Hello with current language code 
    /// ST test = new ST();
    /// test.Value = "Hello!";
    /// // String hello! with current language code
    /// test = "Hello!";
    /// // String hello! in a different language
    /// test = new ST("Bonjour!", "fr-ca");
    /// ]]>
    /// </code>
    /// </example>
    [Structure(Name = "ST", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("ST", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class ST : PDV<String>, IEquatable<ST>
    {

        /// <summary>
        /// Create a new instance of the <see cref="T:MARC.Everest.DataTypes.ST"/> type
        /// </summary>
        public ST() : base() { }
        /// <summary>
        /// Create a new instance of the <see cref="T:MARC.Everest.DataTypes.ST"/> type using <paramref name="data"/> as the initial contents
        /// </summary>
        /// <param name="data">The initial data</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ST(string data) : this() { this.Value = data; }
        /// <summary>
        /// Create a new instance of the <see cref="T:MARC.Everest.DataTypes.ST"/> type using <paramref name="data"/> as the initial contents with 
        /// <paramref name="language"/>
        /// </summary>
        /// <param name="data">The intial data</param>
        /// <param name="language">The language the data is represented in</param>
        public ST(string data, string language) : this(data) { this.Language = language; }

        /// <summary>
        /// The human language of the content
        /// </summary>
        [Property(Name = "language", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public string Language { get; set; }

        /// <summary>
        /// Alternative renditions of the same content translated into a different language
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Property(Name = "translation", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public SET<ST> Translation { get; set; }

        #region Operations

        /// <summary>
        /// Gets the length of the string
        /// </summary>
        public int Length { get { return this.Value != null ? this.Value.Length : 0; } }

        /// <summary>
        /// Concatenate this string value with <paramref name="other"/>
        /// </summary>
        public ST Concat(ST other)
        {
            if (other == null)
                return null;
            else if (other.IsNull && this.IsNull)
                return new ST() { NullFlavor = this.NullFlavor.GetCommonParent(other.NullFlavor) };
            else if (other.IsNull)
                return new ST() { NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation };
            else
                return new ST(this.Value + other.Value);
        }

        /// <summary>
        /// Performs a substring on the <see cref="T:MARC.Everest.DataTypes.ST"/> as per HL7v3 datatypes rules
        /// </summary>
        /// <exception cref="T:ArgumentNullException">When either <paramref name="start"/> or <paramref name="end"/> are null or nullFlavored</exception>
        /// <exception cref="T:ArgumentOutOfRangeException">When <paramref name="start"/> is greater than <paramref name="end"/> or either <paramref name="end"/> or <paramref name="start"/> are
        /// beyond the length of the string</exception>
        public ST Substring(INT start, INT end)
        {
            if (start == null || start.IsNull)
                throw new ArgumentNullException("start", "Argument must not be null and must not contain a null flavor");
            else if (end == null || end.IsNull)
                throw new ArgumentNullException("end", "Argument must not be null and must not contain a null flavor");
            else if (start > this.Length)
                throw new ArgumentOutOfRangeException("start", "Value must be less than the size of the string");
            else if (end > this.Length)
                throw new ArgumentOutOfRangeException("end", "Value must be less than the size of the string");
            else if (start > end)
                throw new ArgumentOutOfRangeException("start", "Value must be less than end");
            else if(this.NullFlavor != null)
                return new ST() { NullFlavor = this.NullFlavor };
            return new ST(this.Value == null ? "" : this.Value.Substring((int)start, (int)(end - start)));
        }

        #endregion

        #region Operators

        /// <summary>
        /// Concatenation operator +
        /// </summary>
        public static ST operator +(ST a, ST b)
        {
            if (a != null)
                return a.Concat(b);
            else
                throw new ArgumentNullException("Cannot concatenate null string value");
        }

        /// <summary>
        /// Converts a <see cref="T:ST"/> to a <see cref="T:System.String"/> in a safe way
        /// </summary>
        /// <param name="o">The <see cref="T:MARC.Everest.DataTypes.ST"/> to convert</param>
        /// <returns>The converted string</returns>
        /// <remarks>The implicit cast operator will convert only if <see cref="T:MARC.Everest.DataTypes.ST"/> is not null. This differs from the explicit cast where 
        /// no exception is thrown</remarks>
        /// <exception cref="T:System.InvalidCastException">When the <see cref="T:MARC.Everest.DataTypes.ST"/> instance has a nullflavor</exception>
        public static implicit operator String(ST o)
        {
            if (o == null || o.IsNull)
                return null;
            else
                return o.Value;
        }
        
        /// <summary>
        /// Casts a <see cref="T:MARC.Everest.DataTypes.Ed"/> into an <see cref="T:ST"/>
        /// </summary>
        /// <param name="o">The object to cast</param>
        /// <returns>The cast <see cref="T:MARC.Everest.DataTypes.ST"/></returns>
        public static implicit operator ST(ED o)
        {
            if (o == null) return null;

#if WINDOWS_PHONE
            string value = o.Representation == MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.B64 ?
                o.Base64Data : o.Representation == MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.XML ?
                o.Value : o.Value;
#else
            string value = o.Representation == MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.B64 ?
                o.Base64Data : o.Representation == MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.XML ? 
                o.XmlData.OuterXml : o.Value;
#endif
            return new ST(value)
            {
                ControlActExt = o.ControlActExt,
                ControlActRoot = o.ControlActRoot,
                Flavor = o.Flavor,
                Language = o.Language,
                NullFlavor = o.NullFlavor,
                UpdateMode = o.UpdateMode,
                ValidTimeHigh = o.ValidTimeHigh,
                ValidTimeLow = o.ValidTimeLow
            };
                
        }

        /// <summary>
        /// Convert a ST into an ED
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        internal ED ToEDUtil(ST o)
        {
            if (o == null)
                return null;

            // Copy
            ED retVal = new ED(o.Value, o.Language);
            retVal.ControlActExt = o.ControlActExt;
            retVal.ControlActRoot = o.ControlActRoot;
            retVal.Flavor = o.Flavor;
            retVal.NullFlavor = o.NullFlavor;
            retVal.Translation = new SET<ED>(o.Translation);
            retVal.UpdateMode = o.UpdateMode;
            retVal.ValidTimeHigh = o.ValidTimeHigh;
            retVal.ValidTimeLow = o.ValidTimeLow;
            retVal.MediaType = "text/plain";
            retVal.Representation = EncapsulatedDataRepresentation.TXT;
            return retVal;
        }

        /// <summary>
        /// Converts a <see cref="String"/> to a <see cref="ST"/>
        /// </summary>
        /// <param name="o">String to convert</param>
        /// <returns>Converted <see cref="T:MARC.Everest.DataTypes.ST"/></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator ST(String o)
        {
            if (o == null)
                return null;

            ST retVal = new ST();
            retVal.Language = CultureInfo.CurrentCulture.Name; 
            retVal.Value = o;
            return retVal;
        }

        /// <summary>
        /// Converts a <see cref="T:MARC.Everest.DataTypes.ST"/> to a <see cref="T:MARC.Everest.DataTypes.REAL"/>
        /// </summary>
        /// <param name="o">ST to convert</param>
        /// <returns>Converted REAL</returns>
        /// <remarks>The resulting REAL will have a nullflavor of NoInformation if the cast was not possible. 
        /// <para>The result will also have a precision set based on the location of the decimal point in the string representation</para></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static explicit operator REAL(ST o)
        {
            if (o == null)
                return null;

            Double d = 0;
            if (!Double.TryParse(o.Value, out d))
                return new REAL() { NullFlavor = DataTypes.NullFlavor.NoInformation };

            int precision = 0;
            if (o.Value.Contains("."))
                precision = o.Value.Length - o.Value.IndexOf(".") - 1;

            return new REAL(d)
            {
                Precision = precision
            };
        }

        /// <summary>
        /// Converts a <see cref="T:MARC.Everest.DataTypes.ST"/> to a <see cref="T:MARC.Everest.DataTypes.INT"/>
        /// </summary>
        /// <param name="o">ST to convert</param>
        /// <returns>Converted INT</returns>
        /// <remarks>The resulting INT will have a nullflavor of NoInformation if the cast was not possible. </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static explicit operator INT(ST o)
        {
            if (o == null)
                return null;

            int d = 0;
            if (!Int32.TryParse(o.Value, out d))
                return new INT() { NullFlavor = DataTypes.NullFlavor.NoInformation };

            return new INT(d);
        }

        #endregion


        #region IEquatable<ST> Members

        /// <summary>
        /// Determines if this ST equals another ST
        /// </summary>
        public bool Equals(ST other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((PDV<string>)other) &&
                    other.Language == this.Language &&
                    (other.Translation != null ? other.Translation.Equals(this.Translation) : this.Translation == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is ST || obj is string)
                return Equals(obj as ST);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Validate the <see cref="T:MARC.Everest.DataTypes.ST"/> instance
        /// </summary>
        /// <remarks>
        /// An <see cref="T:MARC.Everest.DataTypes.ST"/> is considered valid if:
        /// <list type="bullet">
        ///     <item><see cref="P:Value"/>Value is populated XOR <see cref="P:NullFlavor"/> is populated, and</item>
        ///     <item>No value in the <see cref="P:Translation"/> contains a translation</item>
        /// </list>
        /// </remarks>
        public override bool Validate()
        {
            return ((this.Value != null) ^ (this.NullFlavor != null)) &&
                (this.Translation != null && this.Translation.FindAll(o => o.Translation == null).Count == this.Translation.Count ||
                this.Translation == null);
        }

        /// <summary>
        /// Extended validation function that returns detected issues
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = base.ValidateEx() as List<IResultDetail>;

            if(this.NullFlavor != null && this.Value != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ST", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            else if(this.Value == null && this.NullFlavor == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ST", ValidationMessages.MSG_NULLFLAVOR_MISSING, null));
            if (this.Translation != null && this.Translation.FindAll(o => o.Translation != null).Count == this.Translation.Count)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ST", String.Format(ValidationMessages.MSG_PROPERTY_NOT_PERMITTED_ON_PROPERTY, "Translation", "Translation"), null));
            return retVal;
        }

        /// <summary>
        /// ST.NT (no translations) flavor
        /// </summary>
        [Flavor("NT")]
        [Flavor("ST.NT")]
        public static bool IsValidNtFlavor(ST st)
        {
            return st.Translation == null || st.Translation.IsEmpty;
        }

        /// <summary>
        /// Creates an instance of the <see cref="T:MARC.Everest.DataTypes.ST"/> type such that it is conformant to the NoTranslations 
        /// flavor
        /// </summary>
        /// <param name="value">The value of the string</param>
        /// <returns>The constructed <see cref="T:MARC.Everest.DataTypes.ST"/> conformant to the NT flavor</returns>
        public static ST CreateNt(string value)
        {
            return new ST(value) { Translation = null };
        }

        /// <summary>
        /// ST.SIMPLE contains no language
        /// </summary>
        [Flavor("SIMPLE")]
        [Flavor("ST.SIMPLE")]
        public static bool IsValidSimpleFlavor(ST st)
        {
            // Language is usually set to the default locale
            if (st.Language == CultureInfo.CurrentCulture.Name)
                st.Language = null;
            return st.Language == null;
        }

        /// <summary>
        /// Creates an instance of <see cref="T:MARC.Everest.DataTypes.ST"/> type that is conformant to the SIMPLE flavor
        /// </summary>
        /// <param name="value">The value of the <see cref="T:MARC.Everest.DataTypes.ST"/></param>
        /// <returns>An instance of <see cref="T:MARC.Everest.DataTypes.ST"/> conformant to the SIMPLE flavor</returns>
        public static ST CreateSimple(string value)
        {
            return new ST(value) { Language = null };
        }

        /// <summary>
        /// Semantic equals
        /// </summary>
        public override BL SemanticEquals(IAny other)
        {
            if (other == null)
                return null;
            else if (this.IsNull && other.IsNull)
                return new BL() { NullFlavor = NullFlavorUtil.GetCommonParent(this.NullFlavor, other.NullFlavor) };
            else if (this.IsNull ^ other.IsNull)
                return new BL() { NullFlavor = DataTypes.NullFlavor.NotApplicable };

            ST otherSt = other as ST;
            if(otherSt != null)
                return otherSt.Value != null ? otherSt.Value.Equals(this.Value) : this.Value == null;
            ED otherEd = other as ED; // ST may also be equal to ED when value is text/plain
            if (otherEd.MediaType == "text/plain")
                return otherEd.Value != null ? otherEd.Value.Equals(this.Value) : this.Value == null;
            return false;
        }
    }
}
