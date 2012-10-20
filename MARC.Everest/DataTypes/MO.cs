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
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// An MO is a quantity expressing the amount of money in some currency
    /// </summary>
    /// <remarks>
    /// The monetary amount class in Everest 1.0 contains methods that allow
    /// for arithmetic operations to be performed against instances for example:
    /// <example>
    /// <code lang="cs" title="Change Problem">
    ///Console.Write("Enter an Amount:$");
    ///MO amount = new MO(decimal.Parse(Console.ReadLine()), "CAD");
    ///MO loonies = new MO((int)(amount / 1), "CAD");
    ///amount -= loonies;
    ///MO quarters = new MO((decimal)(0.25 * (int)(amount / 0.25)), "CAD");
    ///amount -= quarters;
    ///MO dimes = new MO((decimal)(0.10 * (int)(amount / 0.10)), "CAD");
    ///amount -= dimes;
    ///MO nickels = new MO((decimal)(0.05 * (int)(amount / 0.05)), "CAD");
    ///amount -= nickels;
    ///Console.WriteLine("Total Change:\r\n{0} in loonies\r\n{1} in quarters\r\n{2} in dimes\r\n{3} in nickels\r\n{4} in pennies",
    ///    loonies, quarters, dimes, nickels, amount);
    /// </code>
    /// </example>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance"), Serializable]
    [Structure(Name = "MO", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("MO", Namespace = "urn:hl7-org:v3")]
    public class MO : QTY<Nullable<Decimal>>, IEquatable<MO>, IComparable<MO>, IRealValue
    {


        //((a, b) => a.Value.Equals(b.Value) && a.Currency.Equals(b.Currency) ? 0 : 1);
        // Precision for the MO
        private int m_precision = 0;

        /// <summary>
        /// Creates a new instance of the MO class
        /// </summary>
        public MO() : base() { }
        /// <summary>
        /// Creates a new instance of the MO class with the specified value
        /// </summary>
        /// <param name="value">The initial value of the MO</param>
        public MO(decimal value) : base(value) { }
        /// <summary>
        /// Creates a new instance of the MO class with the specified <paramref name="value"/> in the specified <paramref name="currency"/>
        /// </summary>
        /// <param name="value">The initial value of the MO</param>
        /// <param name="currency">The currency of the MO</param>
        public MO(decimal value, string currency) : this(value) { this.Currency = currency; }

        /// <summary>
        /// The currency code as defined by ISO 4217
        /// </summary>
        [Property(Name = "currency", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Required)]
        public string Currency { get; set; }

        /// <summary>
        /// The number of significant digits of the decimal representation.
        /// </summary>
        [Property(Name = "precision", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public override int Precision
        {
            get
            {
                return m_precision;
            }
            set
            {
                m_precision = value;
            }
        }

        /// <summary>
        /// Represent this MO as a string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)")]
        public override string ToString()
        {
            return String.Format("{0}{1}{2}", Value, Value != null && !String.IsNullOrEmpty(Currency) ? " " : "", Currency);
        }

        /// <summary>
        /// Validate
        /// </summary>
        public override bool Validate()
        {
            return ((Value != null || UncertainRange != null) ^ (NullFlavor != null)) &&
                (((Value != null || UncertainRange != null) && Currency != null) || (Value == null && UncertainRange == null)) &&
                ((Value != null) ^ (UncertainRange != null) || (Value == null && UncertainRange == null));
        }

        /// <summary>
        /// Validate returning a list of detected issues
        /// </summary>
        /// <remarks>
        /// An instance of MO is considered valid when:
        /// <list type="number">
        ///     <item><description>If the <see cref="P:NullFlavor"/> property has a value and the <see cref="P:Value"/> and <see cref="P:UncertainRange"/> properties are not set, and</description></item>
        ///     <item><description>If the <see cref="P:Value"/> property is set, the <see cref="P:NullFlavor"/> and <see cref="P:UncertainRange"/> properties have no value, and</description></item>
        ///     <item><description>If the <see cref="P:UncertainRange"/> property is set, the <see cref="P:NullFlavor"/> and <see cref="P:Value"/> properties are not set, and</description></item>
        ///     <item><description>If the <see cref="P:NullFlavor"/> property is not set then <see cref="P:Currency"/> property is set</description></item>
        /// </list>
        /// </remarks>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            List<IResultDetail> retVal = base.ValidateEx() as List<IResultDetail>;
            if(this.NullFlavor != null && (this.Value != null || this.UncertainRange != null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "MO", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (this.Value == null && this.NullFlavor == null && this.UncertainRange == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "MO", ValidationMessages.MSG_NULLFLAVOR_MISSING, null));
            if (!((this.Value != null) ^ (this.UncertainRange != null)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "MO", String.Format(ValidationMessages.MSG_INDEPENDENT_VALUE, "UncertainRange", "Value")));
            if (this.NullFlavor == null && String.IsNullOrEmpty(this.Currency))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "MO", String.Format(ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Value or UncertainRange", "Currency"), null));
            return retVal;
        }
        #region IEquatable<MO> Members

        /// <summary>
        /// Determine if this MO is equal to another MO
        /// </summary>
        public bool Equals(MO other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((QTY<Nullable<Decimal>>)other) &&
                    this.Currency == other.Currency &&
                    this.Precision == other.Precision;
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is MO)
                return Equals(obj as MO);
            return base.Equals(obj);
        }
        #endregion
        #region Operators

        /// <summary>
        /// Negates <paramref name="a"/>
        /// </summary>
        public static MO operator -(MO a)
        {
            if (a == null)
                return null;
            else if (a.IsNull)
                return new MO() { NullFlavor = a.NullFlavor };
            else if (!a.Value.HasValue)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new MO((decimal)-a.Value, a.Currency);
        }

        /// <summary>
        /// Subtracts <paramref name="b"/> from <paramref name="a"/>
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">When the currency of <paramref name="a"/> does not match the currency of <paramref name="b"/></exception>
        public static MO operator -(MO a, MO b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (a.Currency != b.Currency)
                throw new InvalidOperationException("Both amounts must have the same currency for this operation");
            else
                return new MO((decimal)(a.Value - b.Value), a.Currency);
        }

        /// <summary>
        /// Adds <paramref name="b"/> to <paramref name="a"/>
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">When the currency of <paramref name="a"/> does not match the currency of <paramref name="b"/></exception>
        public static MO operator +(MO a, MO b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (a.Currency != b.Currency)
                throw new InvalidOperationException("Both amounts must have the same currency for this operation");
            else
                return new MO((decimal)(a.Value + b.Value), a.Currency);
        }

        /// <summary>
        /// Multiplies <paramref name="b"/> with <paramref name="a"/>
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">When the currency of <paramref name="a"/> does not match the currency of <paramref name="b"/></exception>
        public static MO operator *(MO a, MO b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (a.Currency != b.Currency)
                throw new InvalidOperationException("Both amounts must be in he same currency for this operation");
            else
                return new MO((decimal)(a.Value * b.Value), a.Currency);
        }

        /// <summary>
        /// Multiplies <paramref name="b"/> by <paramref name="a"/>
        /// </summary>
        public static MO operator *(REAL a, MO b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new MO((decimal)(a.Value * (double)b.Value), b.Currency);
        }

        /// <summary>
        /// Multiplies <paramref name="b"/> by <paramref name="a"/>
        /// </summary>
        public static MO operator *(MO a, REAL b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new MO((decimal)((double)a.Value * b.Value), a.Currency);
        }

        /// <summary>
        /// Divides <paramref name="b"/> with <paramref name="a"/>
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">When the currency of <paramref name="a"/> does not match the currency of <paramref name="b"/></exception>
        public static MO operator /(MO a, MO b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (a.Currency != b.Currency)
                throw new InvalidOperationException("Both amounts must be in the same currency for this operation");
            else if (b.Value == 0)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new MO((decimal)(a.Value / b.Value), a.Currency);
        }

        /// <summary>
        /// Divides <paramref name="a"/> by <paramref name="b"/>
        /// </summary>
        public static MO operator /(REAL a, MO b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (b.Value == 0)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new MO((decimal)(a.Value / (double)b.Value), b.Currency);
        }

        /// <summary>
        /// Divides <paramref name="a"/> by <paramref name="b"/>
        /// </summary>
        public static MO operator /(MO a, REAL b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (b.Value == 0)
                return new MO() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new MO((decimal)((double)a.Value / b.Value), a.Currency);
        }

        #endregion



        #region ISemanticEquatable<MO> Members

        /// <summary>
        /// Determines if this instance of MO semantically equals <paramref name="other"/>
        /// </summary>
        /// <remarks>Two non-null non-null flavored instance of MO are considered equal when their currencies and values are equal or if their uncertain range properties
        /// are non-null, not null-flavored and equal.</remarks>
        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;
            var otherMo = other as MO;
            if (otherMo == null)
                return false;
            if ((this.Value == null && otherMo.Value == null || this.Value.Equals(otherMo.Value)) &&
                           (this.Currency == null && otherMo.Currency == null || this.Currency.Equals(otherMo.Currency)))
                return true;
            else if (this.UncertainRange != null && otherMo.UncertainRange != null &&
                           this.UncertainRange.Equals(otherMo.UncertainRange))
                return true;
            else
                return false;
            
        }

        #endregion

        #region IComparable<MO> Members

        /// <summary>
        /// Compare this MO to another MO
        /// </summary>
        /// <exception cref="T:System.ArgumentException">If the currencies of both MO instances don't match</exception>
        public int CompareTo(MO other)
        {
            if (other == null || other.IsNull)
                return 1;
            else if (this.IsNull && !other.IsNull)
                return -1;
            else if (this.Value.HasValue && !other.Value.HasValue)
                return 1;
            else if (other.Value.HasValue && !this.Value.HasValue)
                return -1;
            else if (!other.Value.HasValue && !this.Value.HasValue)
                return 0;
            else if (!this.Currency.Equals(other.Currency))
                throw new ArgumentException("Currencies must match to compare MO");
            else
                return this.Value.Value.CompareTo(other.Value.Value);
        }

        #endregion
    }
}