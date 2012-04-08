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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UVP"), Serializable]
    [Structure(Name = "UVP", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("UVP", Namespace = "urn:hl7-org:v3")]
    public class UVP<T> : PDV<T>, IEquatable<UVP<T>>, IProbability
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
        /// Convert a concrete UVP to a generic version
        /// </summary>
        /// <exception cref="T:System.InvalidCastException">When the <paramref name="o"/> instance cannot be parsed to a strongly typed UVP of <typeparamref name="T"/></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static UVP<T> Parse(MARC.Everest.DataTypes.UVP<Object> o)
        {
            UVP<T> retVal = new UVP<T>();
            retVal.NullFlavor = o.NullFlavor == null ? null : o.NullFlavor.Clone() as CS<NullFlavor>;
            retVal.ControlActExt = o.ControlActExt;
            retVal.ControlActRoot = o.ControlActRoot;
            retVal.Flavor = o.Flavor;
            retVal.Probability = o.Probability;
            retVal.UpdateMode = o.UpdateMode == null ? null : o.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.ValidTimeHigh = o.ValidTimeHigh;
            retVal.ValidTimeLow = o.ValidTimeLow;
            try
            {
                retVal.Value = (T)Util.FromWireFormat(o.Value, typeof(T));
            }
            catch (Exception e)
            {
                throw new InvalidCastException("Can't parse surrogate into a strongly typed UVP<T>", e);
            }

            return retVal;
        }

        #region IEquatable<UVP<T>> Members

        /// <summary>
        /// Determines if this UVP of T equals another UVP of T
        /// </summary>
        public bool Equals(UVP<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((PDV<T>)other) &&
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
    }
}