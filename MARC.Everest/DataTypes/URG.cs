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
using MARC.Everest.Connectors;
using System.Xml.Serialization;

namespace MARC.Everest.DataTypes
{
    
    /// <summary>
    /// A union of <see cref="T:MARC.Everest.DataTypes.UVP{T}"/>(Probability) and <see cref="T:MARC.Everest.DataTypes.IVL{T}"/>(Interval). 
    /// </summary>
    /// <example>
    /// <code title="Blood Sugar Reading" lang="cs">
    /// <![CDATA[
    ///        // blood reading
    ///        URG<PQ> test = new URG<PQ>();
    ///
    ///        // blood sugar level (2 readings)
    ///        test.Low = new PQ(5, "mmol/L");
    ///        test.High = new PQ(5.2, "mmol/L");
    ///        test.Probability = 0.95; // The glucose meter has a 95% confidence interval  
    /// ]]>
    /// </code>
    /// </example>
    /// <remarks>It is being included for compatibility with R1 datatypes.</remarks>
    /// <seealso cref="T:MARC.Everest.DataTypes.UVP{T}"/>
    /// <seealso cref="T:MARC.Everest.DataTypes.IVL{T}"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG")]
    [Structure(Name = "URG", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(IQuantity))]
    [XmlType("URG", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class URG<T> : UVP<T>, IInterval<T>, IEquatable<URG<T>>
        where T : IAny
    {

        #region IInterval<T> Members

        /// <summary>
        /// Text indicating where this interval was derived
        /// </summary>
        [Property(Name = "originalText", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public ED OriginalText { get; set; }

        /// <summary>
        /// This is the low limit. If the low limit is not known a null flavor should be specified
        /// </summary>
        [Property(Name = "low", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public T Low { get; set; }

        /// <summary>
        /// Specifies wheter low is included in the IVL or excluded from the IVL
        /// </summary>
        [Property(Name = "lowIncluded", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public bool? LowClosed { get; set; }

        /// <summary>
        /// The high limit. If the high limit is not known, a null flavor should be specified
        /// </summary>
        [Property(Name = "high", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public T High { get; set; }

        /// <summary>
        /// Specifies whether high is inlcluded in the IVL or excluded in the IVL
        /// </summary>
        [Property(Name = "highIncluded", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public bool? HighClosed { get; set; }

        // HACK: To support the width
        /// <summary>
        /// The difference between the high and low bondary. Width is used when the size of the interval is known
        /// but the actual start and end points are not known. 
        /// </summary>
        [Property(Name = "width", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public PQ Width { get; set; }
        
        #endregion


        /// <summary>
        /// Convert a concrete URG to a generic version
        /// </summary>
        /// Obsoleted because of where clause
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //public static URG<T> Parse(MARC.Everest.DataTypes.URG<Object> o)
        //{
        //    URG<T> retVal = new URG<T>();
        //    retVal.NullFlavor = o.NullFlavor == null ? null : o.NullFlavor.Clone() as CS<NullFlavor>;
        //    retVal.ControlActExt = o.ControlActExt;
        //    retVal.ControlActRoot = o.ControlActRoot;
        //    retVal.Flavor = o.Flavor;
        //    retVal.Probability = o.Probability;
        //    retVal.UpdateMode = o.UpdateMode == null ? null : o.UpdateMode.Clone() as CS<UpdateMode>;
        //    retVal.ValidTimeHigh = o.ValidTimeHigh;
        //    retVal.ValidTimeLow = o.ValidTimeLow;
        //    retVal.Value = (T)Util.FromWireFormat(o.Value, typeof(T));
        //    retVal.OriginalText = o.OriginalText == null ? null : o.OriginalText.Clone() as ED;
        //    retVal.Low = (T)Util.FromWireFormat(o.Low, typeof(T));
        //    retVal.High = (T)Util.FromWireFormat(o.High, typeof(T));
        //    retVal.LowClosed = o.LowClosed;
        //    retVal.HighClosed = o.HighClosed;
        //    retVal.Width = (PQ)Util.FromWireFormat(o.Width, typeof(PQ));
        //    return retVal;
        //}

        /// <summary>
        /// Validates this instance of URG
        /// </summary>
        /// <returns>True if the instance is valid</returns>
        public override bool Validate()
        {
            return (NullFlavor != null) ^ ((Low != null || Width != null || High != null || Value != null) &&
                ((LowClosed != null && Low != null) || LowClosed == null) &&
                ((HighClosed != null && High != null) || HighClosed == null)) &&
                ((this.Probability != null) ^ this.IsNull);
            // Either IVL is null, has a width or has low and/or high. Low/High and width can't be mixed
            //return (NullFlavor != null) ^ ((Width != null) ^ (Low != null || High != null)) &&
            //    ((LowClosed != null && Low != null) || LowClosed == null) &&
            //    ((HighClosed != null && High != null) || HighClosed == null) && base.Validate();
        }

        /// <summary>
        /// Validates the instance of URG and returns the detected issues
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = base.ValidateEx() as List<IResultDetail>;

            // Validation
            if (NullFlavor != null && (Low != null || High != null || Width != null || Value != null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "URG", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (LowClosed != null && Low == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "URG", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "LowClosed", "Low"), null));
            if (HighClosed != null && High == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "URG", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "HighClosed", "High"), null));

            return retVal;
        }

        /// <summary>
        /// Determine semantic equality
        /// </summary>
        /// <remarks>Since this data-type is an adaptation of an R1 data type with no semantic equality statements, 
        /// the value of this method will be that of the IVL type combined with the UVP type meaning that two 
        /// URG instances are semantically equal when their <see cref="P:Low"/> and <see cref="P:High"/> bounds
        /// are specified and equal and their <see cref="P:Probability"/> properties are equal</remarks>
        public override BL SemanticEquals(IAny other)
        {

            // Based on set, first, is the other a DSET? 
            if (other is URG<T>)
            {
                URG<T> ivlOther = other as URG<T>;
                // Parameters to semantic equality
                bool otherHighInfinite = (ivlOther.High == null || (NullFlavor)ivlOther.High.NullFlavor == DataTypes.NullFlavor.PositiveInfinity),
                    thisHighInfinite = (this.High == null || (NullFlavor)this.High.NullFlavor == DataTypes.NullFlavor.PositiveInfinity),
                    otherLowInifinite = (ivlOther.Low == null || (NullFlavor)ivlOther.Low.NullFlavor == DataTypes.NullFlavor.NegativeInfinity),
                    thisLowInfinite = (this.Low == null || (NullFlavor)this.Low.NullFlavor == DataTypes.NullFlavor.NegativeInfinity),
                    isOtherUnbound = (ivlOther.High == null || ivlOther.High.IsNull) && !otherHighInfinite ||
                        (ivlOther.Low == null || ivlOther.Low.IsNull) && !otherLowInifinite,
                    isThisUnbound = (this.High == null || this.High.IsNull) && !thisHighInfinite ||
                        (this.Low == null || this.Low.IsNull) && !thisLowInfinite;

                // Case 1 : Both are bound
                if (!isOtherUnbound && !isThisUnbound)
                    return ((otherHighInfinite && thisHighInfinite) || (bool)this.High.SemanticEquals(ivlOther.High)) &&
                        ((otherLowInifinite && thisLowInfinite) || (bool)this.Low.SemanticEquals(ivlOther.Low)) &&
                        (this.Probability == null ? ivlOther.Probability == null : this.Probability.Equals(ivlOther.Probability));
                return false; // all others are not equal

            }
            return false;
        }

        #region IEquatable<URG<T>> Members

        /// <summary>
        /// Determines if this URG of T is equal to another URG of T
        /// </summary>
        public bool Equals(URG<T> other)
        {
            bool result = false;
            if(other != null)
                result = base.Equals((UVP<T>)other) &&
                    (other.High != null ? other.High.Equals(this.High) : this.High == null) &&
                    other.HighClosed == this.HighClosed &&
                    (other.Low != null ? other.Low.Equals(this.Low) : this.Low == null) &&
                    other.LowClosed == this.LowClosed &&
                    (other.OriginalText != null ? other.OriginalText.Equals(this.OriginalText) : this.OriginalText == null) &&
                    (other.Width != null ? other.Width.Equals(this.Width) : this.Width == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is URG<T>)
                return Equals(obj as URG<T>);
            return base.Equals(obj);
        }

        #endregion
    }
}