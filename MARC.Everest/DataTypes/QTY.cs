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

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// Provides a convenient wrapper around <see cref="T:QTY{T}"/>
    /// </summary>
    /// <remarks>Included in order to process HL7v3 standard MIF files with no type parameter for the QTY class</remarks>
    //[Serializable]
    //[Obsolete("QTY is obsolete, consider using QTY<INT>", true)]
    //public class QTY : QTY<INT>
    //{
    //    /// <summary>
    //    /// Create a new instance of QTY
    //    /// </summary>
    //    public QTY() : base() {}
        
    //    /// <summary>
    //    /// Create a new instance of QTY using the specified <paramref name="value"/>
    //    /// </summary>
    //    /// <param name="value">The value of the QTY</param>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    //    public QTY(INT value)
    //        : base(value)
    //    { }

    //    /// <summary>
    //    /// Implicit operator can be used to parse a <see cref="T:MARC.Everest.DataTypes.QTY{T}"/> to a <see cref="QTY"/>
    //    /// </summary>
    //    public static QTY Parse(QTY<INT> qty)
    //    {
    //        return new QTY(qty.Value)
    //        {
    //            ControlActExt = qty.ControlActExt,
    //            ControlActRoot = qty.ControlActRoot,
    //            Expression = qty.Expression != null ? qty.Expression.Clone() as ED : null,
    //            ExpressionLanguage = qty.ExpressionLanguage,
    //            Flavor = qty.Flavor,
    //            NullFlavor = qty.NullFlavor,
    //            OriginalText = qty.OriginalText != null ? qty.OriginalText.Clone() as ED : null,
    //            Uncertainty = qty.Uncertainty != null ? qty.Uncertainty.Clone() as QTY<INT> : null,
    //            UncertaintyType = qty.UncertaintyType,
    //            UpdateMode = qty.UpdateMode != null ? qty.UpdateMode.Clone() as CS<UpdateMode> : null,
    //            ValidTimeHigh = qty.ValidTimeHigh,
    //            ValidTimeLow = qty.ValidTimeLow
    //        };
            
    //    }
    //}

    /// <summary>
    /// The quantity data type is an abstract generalization for all data types whose value set has an order 
    /// relation and where difference is defined in all of the data type's totally ordered value subsets. A
    /// quantity type abstraction is needed in defining certain other types such as the interval and 
    /// probability distribution
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "QTY")]
    [Structure(Name = "QTY", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("QTY", Namespace = "urn:hl7-org:v3")]
    #if !WINDOWS_PHONE
    [Serializable]
    #endif
    public abstract class QTY<T> : PDV<T>, IQuantity, IRealValue, IGraphable, IEquatable<QTY<T>>
        //where T : IComparable<T>
    {

        /// <summary>
        /// Create a new instance of QTY
        /// </summary>
        public QTY() : base() {}
        /// <summary>
        /// Create a new instance of QTY using the value specified
        /// </summary>
        /// <param name="value">The value of the QTY</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QTY(T value) { this.Value = value; }

        /// <summary>
        /// Identifies an expression that represents the quantity
        /// </summary>
        [Property(Name = "expression", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public ED Expression { get; set; }

        /// <summary>
        /// Represents the original text that was used to determine the quantity
        /// </summary>
        [Property(Name = "originalText", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public ED OriginalText { get; set; }

        /// <summary>
        /// Specifies the uncertainty of the quantity using a distribution function and its parameters
        /// </summary>
        [Property(Name = "uncertainty", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public virtual IQuantity Uncertainty { get; set; }

        /// <summary>
        /// A code specifying the type of probability distribution in uncertainty. Possible values are shown in
        /// the specified enumeration
        /// </summary>
        [Property(Name = "uncertaintyType", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public virtual QuantityUncertaintyType? UncertaintyType { get; set; }

        /// <summary>
        /// Indicates that the value comes from a range of possible values
        /// </summary>
        /// <remarks>
        /// <para>The uncertain range is used where the actual value is not known,
        /// but a range of possible values are.</para>
        /// <para>
        /// The uncertain range indicates a range of uncertain values (not explicitly
        /// equal to) whereas a value with uncertainty indicates that there is a 
        /// single value along with a distribution of uncertainty for the value.
        /// </para>
        /// </remarks>
        [Property(Name = "uncertainRange", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Required)]
        public virtual IVL<IQuantity> UncertainRange { get; set; }

        /// <summary>
        /// Convert QTY to Int32
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt32(System.Object)")]
        public virtual Int32 ToInt()
        {
            return Convert.ToInt32(Value);
        }

        /// <summary>
        /// Convert QTY to double
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToDouble(System.Object)")]
        public virtual Double ToDouble()
        {
            return Convert.ToDouble(Value);
        }

        /// <summary>
        /// Precision
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public virtual int Precision { get; set; }

        /// <summary>
        /// Validate this structure
        /// </summary>
        /// <remarks>
        /// A QTY is considered valid when:
        /// <list type="list">
        ///     <item><term>When <see cref="F:Expression"/> is specified, <see cref="F:ExpressionLanguage"/> is specified</term></item>
        ///     <item><term>Either <see cref="F:Uncertainty"/> or <see cref="F:UncertainRange"/> are specified (but not both), or neither is set</term></item>
        /// </list>
        /// </remarks>
        public override bool Validate()
        {
            bool valid = (Value == null && NullFlavor != null) || (NullFlavor == null && Value != null);

            // When 
            // Uncertainty or uncertainty range must be used exclusively
            valid &= ((UncertainRange != null) ^ (Uncertainty != null)) || UncertainRange == null && Uncertainty == null;

            // When Uncertainty is populated, the Uncertainty type must be populated
            valid &= ((Uncertainty != null && UncertaintyType != null) || Uncertainty == null);

            // When the uncertain range is populated it cannot have a width or value
            valid &= ((UncertainRange != null && UncertainRange.Width == null && UncertainRange.Value == null) || UncertainRange == null);

            return valid;

        }

        /// <summary>
        /// Advanced validation with details
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            List<IResultDetail> retVal = new List<IResultDetail>();
            
            // Either null Flavor or Value or Uncertain range
            if ((Value != null) && NullFlavor != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QTY", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if(Value == null && NullFlavor == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QTY", ValidationMessages.MSG_NULLFLAVOR_MISSING, null));
            if(!((UncertainRange != null) ^ (Uncertainty != null)|| UncertainRange == null && Uncertainty == null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QTY", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_INDEPENDENT_VALUE, "Uncertainty", "UncertainRange"), null));
            if (!((Uncertainty != null && UncertaintyType != null) || Uncertainty == null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QTY", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Uncertainty", "UncertaintyType"), null));
            if(!((UncertainRange != null && UncertainRange.Width == null && UncertainRange.Value == null) || UncertainRange == null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QTY", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_PERMITTED_ON_PROPERTY, "Width or Value", "UncertainRange"), null));
            return retVal;
        }

        /// <summary>
        /// Casts a <typeparamref name="T"/> into a <see cref="T:QTY{T}"/>
        /// </summary>
        /// <param name="value">The <typeparamref name="T"/> to cast</param>
        //public static implicit operator QTY<T>(T value)
        //{
        //    return new QTY<T>(value);
        //}

        /// <summary>
        /// Casts a <see cref="T:QTY{T}"/> to a <typeparamref name="T"/>
        /// </summary>
        /// <param name="value">The <see cref="T:QTY{T}"/> that is to be cast</param>
        public static implicit operator T(QTY<T> value)
        {
            return value.Value;
        }

        #region IQuantity<T> Members

        /// <summary>
        /// Expression of the quantity
        /// </summary>
        IEncapsulatedData IQuantity.Expression
        {
            get
            {
                return (IEncapsulatedData)Expression;
            }
            set
            {
                this.Expression = value as ED;
            }
        }

        /// <summary>
        /// Original text of the quantity
        /// </summary>
        IEncapsulatedData IQuantity.OriginalText
        {
            get
            {
                return (IEncapsulatedData)OriginalText;
            }
            set
            {
                OriginalText = value as ED;
            }
        }

        /// <summary>
        /// Gets or sets the uncertainty of the quantity
        /// </summary>
        IQuantity IQuantity.Uncertainty
        {
            get
            {
                return (IQuantity)Uncertainty;
            }
            set
            {
                this.Uncertainty = value;
            }
        }

        /// <summary>
        /// Gets or sets the uncertainty of the quantity
        /// </summary>
        IVL<IQuantity> IQuantity.UncertainRange
        {
            get
            {
                return this.UncertainRange;
            }
            set
            {
                this.UncertainRange = value;
            }
        }

        #endregion

        #region IEquatable<QTY<T>> Members

        /// <summary>
        /// Determine if this QTY of T is equal to another QTY of T
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(QTY<T> other)
        {
            bool result = false;
            if (other != null)
                result = (base.Equals((PDV<T>)other) &&
                    (other.Expression != null ? other.Expression.Equals(this.Expression) : this.Expression == null) &&
                    (other.OriginalText != null ? other.OriginalText.Equals(this.OriginalText) : this.OriginalText == null) &&
                    (other.Uncertainty != null ? other.Uncertainty.Equals(this.Uncertainty) : this.Uncertainty == null) &&
                    (other.UncertainRange != null ? other.UncertainRange.Equals(this.UncertainRange) : this.UncertainRange == null) &&
                    other.UncertaintyType == this.UncertaintyType) ||
                    (base.Equals((ANY)other) &&
                    this.Value == null && other.Value == null &&
                    (other.UncertainRange != null ? other.UncertainRange.Equals(this.UncertainRange) : this.UncertainRange == null));
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is QTY<T>)
                return Equals(obj as QTY<T>);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Determine if this instance of QTY semantically equals <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other datatype to compare for semantic equality</param>
        /// <remarks>There are not specific restrictions on QTY and semantic equality however 
        /// because QTY is described as a generic type in Everest, the semantic equality check 
        /// must be performed to ensure the type of encapsulated data matches</remarks>
        /// <returns></returns>
        public override BL SemanticEquals(IAny other)
        {
            return base.SemanticEquals(other);
        }

    }
}