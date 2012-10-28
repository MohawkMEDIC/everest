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
using System.Xml.Serialization;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes.Interfaces;
using System.ComponentModel;
using MARC.Everest.Connectors;
using System.Collections.Generic;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// Shortcut class
    /// </summary>
    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RTO"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    //[XmlType("RTO_PQ_PQ", Namespace = "urn:hl7-org:v3")]
    //[Obsolete("RTO is obsolete, consider using RTO<PQ,PQ>", true)]
    //public class RTO : RTO<PQ, PQ>
    //{
    //    /// <summary>
    //    /// Creates a new instance of the RTO class
    //    /// </summary>
    //    public RTO() : base() { }
    //    /// <summary>
    //    /// Creates a new instance of the RTO class with the specified <paramref name="numerator"/> and <paramref name="denominator"/>
    //    /// </summary>
    //    /// <param name="denominator">The <see cref="T:MARC.Everest.DataTypes.PQ"/> that contains the value of the denominator</param>
    //    /// <param name="numerator">The <see cref="T:MARC.Everest.DataTypes.PQ"/> that contains the value of the numerator</param>
    //    public RTO(PQ numerator, PQ denominator) : base(numerator, denominator) { }

    //    /// <summary>
    //    /// Convert ratio to a double
    //    /// </summary>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rto")]
    //    public static implicit operator double(RTO rto)
    //    {
    //        return rto.Numerator.ToDouble() / rto.Denominator.ToDouble();
    //    }
    //}

    /// <summary>
    /// A quantity constructed as the quotient of a numerator quantity divided by the denominator
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ratio class is not defined to represent rational numbers (although certainly possible). Common factors in the numerator and denominator never 
    /// cancel each other and RTO's are not automatically converted to a REAL number. 
    /// </para>
    /// <para>
    /// Everest .NET varies from the HL7v3 data types specification in that it uses generics to determine the types of 
    /// quantities used for the numerator and denominator. Since .NET supports reified generics, the constraint on the type
    /// used for numerator and denominator is expressed via generic parameters.
    /// </para>
    /// </remarks>
    /// <example>Create a new RTO
    /// <code lang="cs" title="Expressing pay using RTO $1,304 per week">
    /// <![CDATA[
    /// RTO<MO,PQ> pay = new RTO<MO,PQ>(
    ///     new MO(1304,"CAD"),
    ///     new PQ(1, "wk")
    /// );
    /// ]]>
    /// </code>
    /// </example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", MessageId = "T"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RTO"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "S")]
    [Structure(Name = "RTO", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(IQuantity))]
    [XmlType("RTO", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class RTO<S, T> : QTY<Nullable<Double>>, IEquatable<RTO<S, T>>
        where S : IAny, IQuantity
        where T : IAny, IQuantity
    {
        /// <summary>
        /// Creates a new instance of an RTO
        /// </summary>
        public RTO() { }
        /// <summary>
        /// Creates a new instance of an RTO with the specified <paramref name="numerator"/> and <paramref name="denominator"/>
        /// </summary>
        /// <param name="numerator">The numerator</param>
        /// <param name="denominator">The denominator</param>
        public RTO(S numerator, T denominator) { this.Numerator = numerator; this.Denominator = denominator; }

        /// <summary>
        /// The numerator. The quantity being divided in this ratio
        /// </summary>
        [Property(Name = "numerator", Conformance = PropertyAttribute.AttributeConformanceType.Optional,
            PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        public S Numerator { get; set; }
        /// <summary>
        /// The denominator. The quantity that divides the <see cref="Numerator"/>
        /// </summary>
        [Property(Name = "denominator", Conformance = PropertyAttribute.AttributeConformanceType.Optional,
            PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        public T Denominator { get; set; }

        /// <summary>
        /// Precision is not supported on the RTO instance but may be applied to the 
        /// <see cref="P:Numerator"/> and <see cref="P:Denominator"/> seperately.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int Precision
        {
            get
            {
                return base.Precision;
            }
            set
            {
                base.Precision = value;
            }
        }

        /// <summary>
        /// Uncertainty is not directly supported on the RTO instance but may be applied seperately
        /// for the <see cref="P:Numerator"/> and <see cref="P:Denominator"/>
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override IQuantity Uncertainty
        {
            get
            {
                return base.Uncertainty;
            }
            set
            {
                base.Uncertainty = value;
            }
        }

        /// <summary>
        /// Uncertainty type is not directly supported on the RTO instance but may be applied seperately
        /// for the <see cref="P:Numerator"/> and <see cref="P:Denominator"/>
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override QuantityUncertaintyType? UncertaintyType
        {
            get
            {
                return base.UncertaintyType;
            }
            set
            {
                base.UncertaintyType = value;
            }
        }

        /// <summary>
        /// Gets the value of this ratio
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override double? Value
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
        /// Convert ratio to a double
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">If either <see cref="P:Numerator"/> or <see cref="P:Denominator"/> are null or null flavored</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rto")]
        public static explicit operator double(RTO<S, T> rto)
        {
            if (rto.Numerator == null || rto.Numerator.IsNull)
                throw new InvalidOperationException("Numerator must have a value to perform this operation");
            else if (rto.Denominator == null || rto.Denominator.IsNull)
                throw new InvalidOperationException("Denominator must have a value to perform this operation");
            return rto.Numerator.ToDouble() / rto.Denominator.ToDouble();
        }

        /// <summary>
        /// Convert this ratio to a <see cref="T:REAL"/> instance.
        /// </summary>
        public static explicit operator REAL(RTO<S, T> rto)
        {
            if (rto.Numerator == null || rto.Denominator == null)
                return null;
            else if (rto.Numerator.IsNull || rto.Denominator.IsNull)
                return new REAL() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new REAL(rto.Numerator.ToDouble() / rto.Denominator.ToDouble());
        }

        /// <summary>
        /// To double
        /// </summary>
        public override double ToDouble()
        {
            return (double)this;
        }

        /// <summary>
        /// Convert this RTO into an integer if possible
        /// </summary>
        public override int ToInt()
        {
            return base.ToInt();
        }

        /// <summary>
        /// Represent this RTO as a string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public override string ToString()
        {
            if (Numerator != null && Denominator != null)
                return String.Format("{0}/{1}", Numerator.ToString(), Denominator.ToString());
            return String.Empty;
        }

        /// <summary>
        /// Validate that this RTO meets minimum conformance criteria
        /// </summary>
        public override bool Validate()
        {
            return (NullFlavor != null) ^ (Denominator != null || Numerator != null) && 
                ((Numerator != null && Denominator != null && Denominator.Validate() && Numerator.Validate()) ||
                (Numerator == null && Denominator == null)) &&
                 UncertainRange == null;
        }

        /// <summary>
        /// Validate the RTO returning detected issues
        /// </summary>
        /// <remarks>An instance of RTO is considered valid when :
        /// <list type="number">
        ///     <item><description>When the <see cref="P:NullFlavor"/> property is set, neither <see cref="P:Numerator"/> or <see cref="P:Denominator"/> may have a value</description></item>
        ///     <item><description>When the <see cref="P:Numerator"/> and <see cref="P:Denominator"/> properties are set, the <see cref="P:NullFlavor"/> property is not set</description></item>
        ///     <item><description>Whenever a <see cref="P:Numerator"/> is set, the <see cref="P:Denominator"/> must also be set</description></item>
        ///     <item><description><see cref="P:UncertainRange"/> must never be set</description></item>
        /// </list>
        /// </remarks>
        public override System.Collections.Generic.IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            // Cannot use base to validate as Value is not permitted
            var retVal = new List<IResultDetail>();
            if (this.NullFlavor != null && (this.Numerator != null || this.Denominator != null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "RTO", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            else if (this.NullFlavor == null && this.Numerator == null && this.Denominator == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "RTO", ValidationMessages.MSG_NULLFLAVOR_MISSING, null));
            if (this.Numerator != null && this.Denominator == null || this.Denominator != null && this.Numerator == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "RTO", String.Format(ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Numerator", "Denominator"), null));
            if (this.UncertainRange != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "RTO", String.Format(ValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "UncertainRange"), null));
            if (this.Uncertainty != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Warning, "RTO", String.Format(ValidationMessages.MSG_PROPERTY_SCHEMA_ONLY, "Uncertainty"), null));
            if (this.UncertaintyType != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Warning, "RTO", String.Format(ValidationMessages.MSG_PROPERTY_SCHEMA_ONLY, "UncertaintyType"), null));
            return retVal;
        }
        // <summary>
        // Parse a generic RTO from a serialization surrogate RTO
        // </summary>
        //public static RTO<S, T> FromSurrogate(Surrogates.RTO o)
        //{
        //    RTO<S, T> retVal = new RTO<S, T>();
        //    retVal.Denominator = (T)Util.FromWireFormat(o, typeof(T));
        //    retVal.Numerator = (S)Util.FromWireFormat(o, typeof(S));
        //    retVal.ControlActExt = o.ControlActExt;
        //    retVal.ControlActRoot = o.ControlActRoot;
        //    retVal.Expression = o.Expression.Clone() as ED;
        //    retVal.ExpressionLanguage = o.ExpressionLanguage;
        //    retVal.Flavor = o.Flavor;
        //    retVal.NullFlavor = o.NullFlavor.Clone() as CS<NullFlavor>;
        //    retVal.OriginalText = o.OriginalText.Clone() as ED;
        //    retVal.Uncertainty = o.Uncertainty;
        //    retVal.UncertaintyType = o.UncertaintyType;
        //    retVal.UpdateMode = o.UpdateMode.Clone() as CS<UpdateMode>;
        //    retVal.ValidTimeHigh = o.ValidTimeHigh;
        //    retVal.ValidTimeLow = o.ValidTimeLow;
        //    retVal.Value = retVal.Value;
        //    return retVal;
        //}


        #region IEquatable<RTO<S,T>> Members

        /// <summary>
        /// Determine if RTO of S/T is the same as another RTO of S/T
        /// </summary>
        public bool Equals(RTO<S, T> other)
        {
            bool result = false;
            if (other != null)
                result = (base.Equals((PDV<Double?>)other) &&
                    (other.Expression != null ? other.Expression.Equals(this.Expression) : this.Expression == null) &&
                    (other.OriginalText != null ? other.OriginalText.Equals(this.OriginalText) : this.OriginalText == null) &&
                    (other.Uncertainty != null ? other.Uncertainty.Equals(this.Uncertainty) : this.Uncertainty == null) &&
                    other.UncertaintyType == this.UncertaintyType) &&
                    (other.Denominator != null ? other.Denominator.Equals(this.Denominator) : this.Denominator == null) &&
                    (other.Numerator != null ? other.Numerator.Equals(this.Numerator) : this.Numerator == null) ||
                    (base.Equals((ANY)other) &&
                    this.Numerator == null && other.Numerator == null &&
                    this.Denominator == null && other.Denominator == null &&
                    (other.UncertainRange != null ? other.UncertainRange.Equals(this.UncertainRange) : this.UncertainRange == null));
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is RTO<S,T>)
                return Equals(obj as RTO<S,T>);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Determine if this RTO equals another
        /// </summary>
        public override BL SemanticEquals(IAny other)
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
            RTO<S,T> rtoOther = other as RTO<S,T>;
            if (rtoOther == null)
                return false;
            else if (rtoOther.Numerator != null && rtoOther.Denominator != null && this.Numerator != null && this.Denominator != null)
                return rtoOther.Numerator.SemanticEquals(this.Numerator) & rtoOther.Denominator.SemanticEquals(this.Denominator);
            return false;

        }
    }
}