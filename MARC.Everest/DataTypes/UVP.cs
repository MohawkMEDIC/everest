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
using MARC.Everest.Connectors;
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// A generic datatype extension used to specify a proabability(percentage, from 0 to 1) expressing the information
    /// </summary>
    /// <example>
    /// <code title="Blood Alcohol test" lang="cs">
    /// <![CDATA[
    /// 
    ///       UVP<Decimal> DUItest = new UVP<Decimal>();
    ///       // The item was(or is to be) removed
    ///       DUItest.UpdateMode = UpdateMode.Remove;
    ///       DUItest.Value = 0.08M; // This is what the police are testing for (legal limit)
    ///        
    ///       // Probability that roadside breathalizer is accurate
    ///       DUItest.Probability = 0.75;
    ///        
    ///       // Probability that breathalizer is accurate in the police station
    ///       // DUItest.Probability = 0.90;
    /// ]]>
    /// </code>`
    /// </example>
    /// <seealso cref="T:MARC.Everest.DataTypes.PDV{T}"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UVP")]
    [Structure(Name = "UVP", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(IQuantity))]
    [XmlType("UVP", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class UVP<T> : ANY, IEquatable<UVP<T>>, IProbability
        where T : IAny
    {
        /// <summary>
        /// Create a new instance of UVP
        /// </summary>
        public UVP() { }

        /// <summary>
        /// Create a new instance of UVP
        /// </summary>
        /// <param name="value">The value of the UVP</param>
        /// <param name="probability">The probability assigned to the value. Between 0 and 1</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UVP(T value, decimal probability)
        {
            this.Value = value;
            this.Probability = probability;
        }

        /// <summary>
        /// Gets or sets the value of the probability
        /// </summary>
        [Property(Name = "value", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Mandatory)]
        public T Value { get; set; }

        /// <summary>
        /// The probability assigned to the value a decimal between 0 and 1
        /// </summary>
        [Property(Name = "probability", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Mandatory)]
        public decimal? Probability { get; set; }

        /// <summary>
        /// Validate the type
        /// </summary>
        public override bool Validate()
        {
            return (NullFlavor != null) ^ ( Value != null && Probability != null && (Probability >= 0 && Probability <= 1));
        }

        /// <summary>
        /// Validate the UVP instance returning any detected issues
        /// </summary>
        /// <remarks>
        /// An instance of UVP is considered valid when:
        /// <list type="number">
        ///     <item><description>When the <see cref="P:NullFlavor"/> property has a value then neither <see cref="P:Value"/> nor <see cref="P:Probability"/> carry a a value, and</description></item>
        ///     <item><description>When the <see cref="P:NullFlavor"/> property has no value, then the <see cref="P:Value"/> and <see cref="P:Probability"/> properties must carry a value, and </description></item>
        ///     <item><description>If populated, <see cref="P:Probability"/> must be carry a value between 0 and 1</description></item>
        /// </list>
        /// </remarks>
        public override IEnumerable<IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>();
            if (this.NullFlavor != null && (this.Value != null || this.Probability != null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "UVP", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            else if (this.NullFlavor == null)
            {
                if (this.Value == null && !(this is URG<T>))
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "UVP", String.Format(ValidationMessages.MSG_PROPERTY_NOT_POPULATED, "Value", typeof(T).Name), null));
                if (this.Probability == null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "UVP", String.Format(ValidationMessages.MSG_PROPERTY_NOT_POPULATED, "Probability", "Decimal"), null));
                else if (this.Probability < 0 || this.Probability > 1)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "UVP", "Probabiliy must carry a value between 0 and 1", null));
            }
            return retVal;
        }
        /// <summary>
        /// Convert a concrete UVP to a generic version
        /// </summary>
        /// <exception cref="T:System.InvalidCastException">When the <paramref name="o"/> instance cannot be parsed to a strongly typed UVP of <typeparamref name="T"/></exception>
        /// Obsolete
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //public static UVP<T> Parse(MARC.Everest.DataTypes.UVP<Object> o)
        //{
        //    UVP<T> retVal = new UVP<T>();
        //    retVal.NullFlavor = o.NullFlavor == null ? null : o.NullFlavor.Clone() as CS<NullFlavor>;
        //    retVal.ControlActExt = o.ControlActExt;
        //    retVal.ControlActRoot = o.ControlActRoot;
        //    retVal.Flavor = o.Flavor;
        //    retVal.Probability = o.Probability;
        //    retVal.UpdateMode = o.UpdateMode == null ? null : o.UpdateMode.Clone() as CS<UpdateMode>;
        //    retVal.ValidTimeHigh = o.ValidTimeHigh;
        //    retVal.ValidTimeLow = o.ValidTimeLow;
        //    try
        //    {
        //        retVal.Value = (T)Util.FromWireFormat(o.Value, typeof(T));
        //    }
        //    catch (Exception e)
        //    {
        //        throw new InvalidCastException("Can't parse surrogate into a strongly typed UVP<T>", e);
        //    }

        //    return retVal;
        //}

        #region IEquatable<UVP<T>> Members

        /// <summary>
        /// Determines if this UVP of T equals another UVP of T
        /// </summary>
        public bool Equals(UVP<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((ANY)other) &&
                    other.Value == null ? this.Value == null : other.Value.Equals(this.Value) &&
                    other.Probability == this.Probability;
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is UVP<T>)
                return Equals(obj as UVP<T>);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Override of semantic equality between this and <paramref name="other"/>
        /// </summary>
        /// <remarks>
        /// Two non-null, not null-flavored instances of UVP are considered semantically equal with their <see cref="P:Probability"/>
        /// and <see cref="P:Value"/> properties are equal.
        /// </remarks>
        public override BL SemanticEquals(MARC.Everest.DataTypes.Interfaces.IAny other)
        {
            var baseEq = base.SemanticEquals(other);
            if (!(bool)baseEq)
                return baseEq;

            // Null-flavored
            if (this.IsNull && other.IsNull)
                return true;
            else if (this.IsNull ^ other.IsNull)
                return false;

            // Values are equal?
            UVP<T> uvOther = other as UVP<T>;
            if (uvOther == null)
                return false;
            return (this.Value == null ? uvOther.Value == null : this.Value.Equals(uvOther.Value)) &&
                (this.Probability == null ? uvOther.Probability == null : this.Probability.Equals(uvOther.Probability));

        }

        /// <summary>
        /// Represent this UVP as a string
        /// </summary>
        public override string ToString()
        {
            return this.Value == null || this.Probability == null ? base.ToString() : String.Format("~{0} ({1:#.00})", this.Value, this.Probability); 
        }

        #region IPrimitiveDataValue Members

        /// <summary>
        /// Generic Value implementation
        /// </summary>
        object IPrimitiveDataValue.Value
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = (T)value;
            }
        }

        #endregion
    }
}