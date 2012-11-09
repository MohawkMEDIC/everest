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
using MARC.Everest.Interfaces;
using System.ComponentModel;
using System.Xml.Serialization;
using MARC.Everest.Connectors;

#if WINDOWS_PHONE
using MARC.Everest.Phone;
#else
using MARC.Everest.Design;
using System.Drawing.Design;
#endif

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// A set of consecutive values of an ordered base datatype. Any ordered type can be the basis of an IVL
    /// </summary>
    /// <example>
    /// <code lang="cs" title="Interval from Oct 1 2009 - Nov 15 2009">
    /// <![CDATA[
    /// // Example: Patient must visit doctor between these dates
    /// IVL<TS> effectiveTime = new IVL<TS> 
    /// ( 
    ///     DateTime.Parse("October 1, 2009"),  
    /// DateTime.Parse("November 15, 2009") 
    /// ); 
    /// effectiveTime.LowClosed = true; 
    /// effectiveTime.HighClosed = true; 
    /// effectiveTime.Operator = SetOperator.Inclusive; 
    /// ]]>
    /// </code>
    /// </example>
    /// <seealso cref="T:MARC.Everest.DataTypes.SXCM{T}"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL")]
    [Structure(Name = "IVL", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("IVL", Namespace = "urn:hl7-org:v3")]
    #if !WINDOWS_PHONE
    [Serializable]
    #endif

    public class IVL<T> : SXCM<T>, IInterval<T>, IEquatable<IVL<T>>, IPqTranslatable<IVL<T>>, IOriginalText
        where T : IAny
    {

        /// <summary>
        /// Create a new instance of IVL
        /// </summary>
        public IVL() : base() { }
        /// <summary>
        /// Create a new instance of IVL with the value <paramref name="value"/>
        /// </summary>
        /// <param name="value">The parameter</param>
        public IVL(T value) : base(value) { }
        /// <summary>
        /// Create a new instance of IVL using the range specified
        /// </summary>
        /// <param name="low">The lower bound of the range</param>
        /// <param name="high">The upper bound of the range</param>
        public IVL(T low, T high) : base() { Low = low; High = high; }

        #region IInterval<T> Members

        /// <summary>
        /// Text indicating where this interval was derived
        /// </summary>
        [Description("Text indicating where this interval was derived")]
        [Property(Name = "originalText", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
#if !WINDOWS_PHONE
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NewInstanceTypeEditor), typeof(UITypeEditor))]
#endif
        public ED OriginalText { get; set; }

        /// <summary>
        /// This is the low limit. If the low limit is not known a null flavor should be specified
        /// </summary>
        [Description("This is the low limit. If the low limit is not known a null flavor should be specified")]
        [Property(Name = "low", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
#if !WINDOWS_PHONE
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NewInstanceTypeEditor), typeof(UITypeEditor))]
#endif
        public T Low { get; set; }

        /// <summary>
        /// Specifies wheter low is included in the IVL or excluded from the IVL
        /// </summary>
        /// <remarks>
        /// For some reason this is described in HL7v3 Data Types R2 but not in R1. However, in R1
        /// the <see cref="P:Low"/> and <see cref="P:High"/> properties are of type IVXB, however in R2
        /// IVXB is never defined. This property will be used in place of the IVXB "inclusive" attribute.
        /// </remarks>
        [Description("Specifies wheter low is included in the IVL or excluded from the IVL")]
        [Property(Name = "lowClosed", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public bool? LowClosed { get; set; }

        /// <summary>
        /// The high limit. If the hign limit is not known, a null flavour should be specified
        /// </summary>
        [Description("The high limit. If the hign limit is not known, a null flavour should be specified")]
        [Property(Name = "high", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
#if !WINDOWS_PHONE
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NewInstanceTypeEditor), typeof(UITypeEditor))]
#endif
        public T High { get; set; }
        
        /// <summary>
        /// Specifies whether high is inlcluded in the IVL or excluded in the IVL
        /// </summary>
        /// <remarks>
        /// For some reason this is described in HL7v3 Data Types R2 but not in R1. However, in R1
        /// the <see cref="P:Low"/> and <see cref="P:High"/> properties are of type IVXB, however in R2
        /// IVXB is never defined. This property will be used in place of the IVXB "inclusive" attribute.
        /// </remarks>
        [Description("Specifies whether high is inlcluded in the IVL or excluded in the IVL")]
        [Property(Name = "highClosed", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public bool? HighClosed { get; set; }

        /// <summary>
        /// The difference between the high and low bondary. Width is used when the size of the interval is known
        /// but the actual start and end points are not known. 
        /// </summary>
        [Description("The difference between the high and low bondary. Width is used when the size of the interval is known but the actual start and end points are not known. ")]
        [Property(Name = "width", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
#if !WINDOWS_PHONE
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NewInstanceTypeEditor), typeof(UITypeEditor))]
#endif
        public PQ Width { get; set; }

        /// <summary>
        /// Flavor validator for IVL.WIDTH
        /// </summary>
        [Flavor(Name= "IVL.WIDTH")]
        public static bool IsValidWidthFlavor(IVL<T> ivl)
        {
            // IVL must have a Width and either a High / Low
            return (ivl.Low == null && ivl.High == null && !ivl.LowClosed.HasValue && !ivl.HighClosed.HasValue && ivl.Width != null) ^ (ivl.NullFlavor != null);
        }

        /// <summary>
        /// Flavor validator for IVL.HIGH
        /// </summary>
        [Flavor(Name = "IVL.HIGH")]
        public static bool IsValidHighFlavor(IVL<T> ivl)
        {
            return (ivl.Low == null && ivl.High != null && !ivl.LowClosed.HasValue && ivl.HighClosed == true && ivl.Width == null) ^ (ivl.NullFlavor != null);
        }

        /// <summary>
        /// Flavor validator for IVL.LOW
        /// </summary>
        [Flavor(Name = "IVL.LOW")]
        public static bool IsValidLowFlavor(IVL<T> ivl)
        {
            return (ivl.Low != null && ivl.High == null && ivl.LowClosed == true && !ivl.HighClosed.HasValue && ivl.Width == null) ^ (ivl.NullFlavor != null);
        }

        /// <summary>
        /// Determines semantic equality between this IVL and <paramref name="other"/>
        /// </summary>
        /// <remarks>
        /// Two instances of IVL are considered semantically equal when their <see cref="P:Low"/> and <see cref="P:High"/> bounds
        /// are semantically equal with one another. The following rules apply:
        /// <list type="number">
        ///     <item><description>If the <see cref="P:High"/> property is null or positive infinity in both instances of <see cref="T:IVL"/> or if the <see cref="P:Low"/> property
        ///     is null or negative infinity in both IVLs they can be semantically equal</description></item>
        ///     <item><description>Two <see cref="T:IVL"/> instances that are not bound (<see cref="P:Low"/>/<see cref="P:High"/> null) will never be considered equal even if their <see cref="P:Value"/> and <see cref="P:Width"/> properties are semantically equal</description></item>
        ///     
        /// </list>
        /// <para>
        /// Because IVL describes a set, an instance of IVL can be semantically equal to a SET with the same members. For example a <see cref="T:SET"/> instance containing
        /// the numbers {1,2,3,4,5} can be semantically equal to an <see cref="T:IVL{INT}"/> describing {1-5}. Note that when comparing an <see cref="T:IVL"/> to a <see cref="T:SET"/>
        /// the <see cref="F:ToSet()"/> method is called which can be quite costly in terms of CPU resources.
        /// </para>
        /// </remarks>
        public override BL SemanticEquals(IAny other)
        {

            BL semanticEquals = false;

            // Based on set, first, is the other a DSET? 
            if (other is SET<T>)
                return SemanticEqualsInternal(other as SET<T>);
            else if (other is IVL<T>)
            {
                IVL<T> ivlOther = other as IVL<T>;
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
                if(!isOtherUnbound && !isThisUnbound)
                    return ((otherHighInfinite && thisHighInfinite) || (bool)this.High.SemanticEquals(ivlOther.High)) &&
                        ((otherLowInifinite && thisLowInfinite) || (bool)this.Low.SemanticEquals(ivlOther.Low));
                return false; // all others are not equal
                
            }
            return false;
        }

        /// <summary>
        /// Determine if this IVL when represented as a <see cref="T:SET{T}"/>
        /// is semantically equivalent to <paramref name="set"/>
        /// </summary>
        private BL SemanticEqualsInternal(SET<T> set)
        {
            return this.ToSet().SemanticEquals(set);
        }

        #endregion

        /// <summary>
        /// Validates the IVL data type
        /// </summary>
        /// <remarks>
        /// Either the IVL is assigned a null flavor, or one of value, low, high or width is set and
        /// when lowIncluded is set, low is set and
        /// when highIncluded is set, high is set
        /// </remarks>
        /// <returns>True if the item is valid</returns>
        public override bool Validate()
        {
            // Either IVL is null, has a width or has low and/or high. Low/High and width can't be mixed

            // WH + WL + HL + H + V + L     - Original Logic
            // W(H+L) + H(L + 1) + V + L   - Expanded
            // W(H+L) + H + V  + L         - Reduced
            return (NullFlavor != null) ^ ((Low != null || High != null || Width != null || Value != null) &&
                ((LowClosed != null && Low != null) || LowClosed == null) &&
                ((HighClosed != null && High != null) || HighClosed == null));
                
                
                
                //(((Width != null && Low != null) || (Width != null && High != null) ||
                //(Low != null && High != null) || Value != null || High != null)
                //((LowClosed != null && Low != null) || LowClosed == null) &&
                //((HighClosed != null && High != null) || HighClosed == null));
        }

        /// <summary>
        /// Validates the instance of IVL and returns the detected issues
        /// </summary>
        /// <remarks>
        /// An instance of IVL is valid if:
        /// <list type="bullet">
        ///     <item><description>When <see cref="P:NullFlavor"/> is specified <see cref="P:Low"/>, <see cref="P:Width"/>, <see cref="P:High"/> and <see cref="P:Value"/> are null, and</description></item>
        ///     <item><description>When <see cref="P:LowClosed"/> is specified <see cref="P:Low"/> is specified, and </description></item>
        ///     <item><description>When <see cref="P:HighClosed"/> is specified <see cref="P:High"/> is specified, and </description></item>
        /// </list>
        /// </remarks>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = base.ValidateEx() as List<IResultDetail>;

            // Validation
            if (NullFlavor != null && (Low != null || High != null || Width != null || Value != null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "IVL", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (LowClosed != null && Low == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "IVL", String.Format(ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "LowClosed", "Low"), null));
            if (HighClosed != null && High == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "IVL", String.Format(ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "HighClosed", "High"), null));

            return retVal;
        }

        #region IEquatable<IVL<T>> Members

        /// <summary>
        /// Determine if this IVL of T is equal to another IVL of T
        /// </summary>
        public bool Equals(IVL<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((SXCM<T>)other) &&
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
            if (obj is IVL<T>)
                return Equals(obj as IVL<T>);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Determine if the interval described by this IVL
        /// contains the specified member
        /// </summary>
        /// <remarks>This function can only be called on IVL instances
        /// with a low and high</remarks>
        /// <exception cref="T:System.ArgumentException">Thrown when <typeparamref name="T"/> does not implement IComparable</exception>
        /// <exception cref="T:System.InvalidOperationException">Thrown when either the Low or High properties is null and the 
        /// Width property is null or <typeparamref name="T"/> does not implement <see cref="T:MARC.Everest.DataTypes.Interfaces.IPqTranslatable"/>. Basically
        /// the set bounds cannot be determined.</exception>
        public bool Contains(T member)
        {
            if (!(member is IComparable<T>))
                throw new ArgumentException("Must implement IComparable", "member");

            // Low and high references
            T low = this.Low,
                high = this.High;
            
            // Is a width specified?
            if (low == null || high == null)
            {
                // Determine if we can calculate bounds
                if (this.Width != null && member is IPqTranslatable<T>)
                {
                    if (low != null)
                        high = (low as IPqTranslatable<T>).Translate(Width);
                    else if (high != null)
                        low = (high as IPqTranslatable<T>).Translate(-Width);
                }
                else
                    throw new InvalidOperationException("Cannot determine set bounds");
            }

            // Comparable instance
            var membComp = member as IComparable<T>;

            // Determine if the member is within the bounds
            int lb = this.LowClosed == true ? 0 : 1,
                hb = this.HighClosed == true ? 0 : -1;

            return membComp.CompareTo(low) >= lb && membComp.CompareTo(high) <= hb;

        }

        #region IPqTranslatable<IVL<T>> Members

        /// <summary>
        /// Translates this IVL by the specified quantity
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">Thrown when the function cannot determine the
        /// a reliable translation point (ie: either low or high are null)</exception>
        public IVL<T> Translate(PQ translation)
        {
            if (this.IsNull)
                return new IVL<T>() { NullFlavor = this.NullFlavor };

            // Low and high references
            T low = this.Low,
                high = this.High,
                value = this.Value;

            // Is a width specified? If so, then we need a 
            // valid low/high to translate
            if (low == null || high == null)
            {
                // Determine if we can calculate bounds
                if (this.Width != null && (low is IPqTranslatable<T> || high is IPqTranslatable<T>))
                {
                    if (low != null)
                        high = (low as IPqTranslatable<T>).Translate(Width);
                    else if (high != null)
                        low = (high as IPqTranslatable<T>).Translate(-Width);
                }
                else if(value == null)
                    throw new InvalidOperationException("Cannot determine set bounds");
            }

            // Translate
            IPqTranslatable<T> pqLow = low as IPqTranslatable<T>,
                pqHigh = high as IPqTranslatable<T>,
                pqValue = value as IPqTranslatable<T>;
            low = low == null ? default(T) : pqLow.Translate(translation);
            high = pqHigh == null ? default(T) : pqHigh.Translate(translation);
            value = pqValue == null ? default(T) : pqValue.Translate(translation);

            return new IVL<T>(low, high) { Value = value, LowClosed = this.LowClosed, HighClosed = this.HighClosed };
        }

        #endregion

        /// <summary>
        /// Creates a new instance of <see cref="T:IVL{T}"/> with the details of <see cref="P:Low"/> and <see cref="P:High"/> using 
        /// <see cref="P:Low"/> and <see cref="P:High"/> or <see cref="P:Low"/> and <see cref="P:Width"/> or 
        /// <see cref="P:High"/> and <see cref="P:Low"/> from this instance
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="T:IVL{T}"/> containing a low/high pair in place of low/width, high/width
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">When <typeparamref name="T"/> does not implement <see cref="T:MARC.Everest.DataTypes.Interfaces.IPqTranslatable{T}"/></exception>
        public IVL<T> ToBoundIVL()
        {
            if (this.Low != null && this.High != null)
                return new IVL<T>((T)this.Low.Clone(), (T)this.High.Clone())
                {
                    LowClosed = this.LowClosed,
                    HighClosed = this.HighClosed
                };
            else if (this.Low != null && this.Low is IPqTranslatable<T>)
                return new IVL<T>((T)this.Low.Clone(), (this.Low as IPqTranslatable<T>).Translate(this.Width))
                {
                    LowClosed = this.LowClosed
                };
            else if (this.High != null && this.High is IPqTranslatable<T>)
                return new IVL<T>((this.High as IPqTranslatable<T>).Translate(-this.Width), (T)this.High.Clone())
                {
                    HighClosed = this.HighClosed
                };
            else
                throw new InvalidOperationException("Low or High is calculable from given data");
        }

        /// <summary>
        /// Creates a set from a bound IVL
        /// </summary>
        /// <remarks>This function will call <see cref="F:FillInDetails"/> prior to construction of the set as it 
        /// needs to ensure that low and high bounds are known</remarks>
        /// <exception cref="T:System.InvalidOperationException">When <typeparamref name="T"/> does not implement <see cref="T:MARC.Everest.DataTypes.Interfaces.IOrderedDataType{T}"/></exception>
        public SET<T> ToSet()
        {

            var lh = this.ToBoundIVL();
            if (lh.Low is IOrderedDataType<T> && lh.Low is IComparable<T>)
            {
                SET<T> retVal = new SET<T>(10);
                var current = lh.Low as IOrderedDataType<T>;
                while ((current as IComparable<T>).CompareTo(lh.High) <= (lh.HighClosed == true ? 0 : -1))
                {
                    retVal.Add((T)current);
                    current = current.NextValue() as IOrderedDataType<T>;
                }
                return retVal;
            }
            else
                throw new InvalidOperationException(String.Format("Cannot enumerate '{0}' to construct the resultant set", typeof(T).FullName));
        }

        /// <summary>
        /// Represent the set as a string
        /// </summary>
        public override string ToString()
        {
            if (this.Value != null)
                return String.Format("{0}", this.Value);
            else if (this.Low != null && this.High != null)
                return String.Format("{{{0} .. {1}}}", this.Low, this.High);
            else if (this.Low != null)
                return string.Format("{{{0} ..}}", this.Low);
            else if (this.High != null)
                return string.Format("{{.. {0}}}", this.High);
            else return base.ToString();
        }

        /// <summary>
        /// Get parse an instance of IVL from a string representation
        /// </summary>
        internal static IVL<T> FromString(string s)
        {
            return new IVL<T>(MARC.Everest.Connectors.Util.Convert<T>(s));
        }
    }
}
