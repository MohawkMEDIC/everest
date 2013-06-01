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
    /// Represents fractional numbers.
    /// </summary>
    /// <remarks>
    ///  Typically used whenever quantities are measured, estimated or computed from other
    /// real numbers. The typical representation is decimal where the number of significant decimal digits is
    /// known as the precision
    /// </remarks>
    /// <seealso cref="T:MARC.Everest.DataTypes.QTY"/>
    /// <example> An example of a REAL
    /// <code lang="cs" title="Example of REAL Operations">
    /// <![CDATA[
    /// REAL radius = 3.0f,
    ///     parimeter = 2 * Math.PI * radius;
    /// parimeter.Precision = 4;
    /// IVL<REAL> ivl = parimeter.ToIVL();
    /// Console.WriteLine("Parimeter is between {0} and {1}", ivl.Low, ivl.High);
    /// // Output: Parimeter is between 18.4895 and 18.4896
    /// ]]>
    /// </code>
    /// </example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "REAL")]
    [Structure(Name = "REAL", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("REAL", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class REAL : QTY<Nullable<Double>>, IRealValue, IEquatable<REAL>, IComparable<REAL>, IImplicitInterval<REAL>
    {

        // Precision
        private int m_precision = 0;

        /// <summary>
        /// Create a new instance of the REAL class
        /// </summary>
        public REAL() : base() {}

        /// <summary>
        /// Create a new instance of REAL with an initial value of <paramref name="value"/>
        /// </summary>
        /// <param name="value">The initial value</param>
        public REAL(double value) : base(value) {  }

        /// <summary>
        /// The number of significant digits of the decimal representation. Null and a value of 0 means nothing 
        /// is known or stated about the precision
        /// </summary>
        /// <remarks>
        /// Modifying this property will affect the manner in which it is compared. Since floating point numbers will
        /// lose some precision after deserializing, this property has an effect on the tolerance of difference when determining
        /// equality. For example:
        /// <example>
        /// <code title="Precision Effects" lang="cs">
        /// REAL r1 = 0.333333333333333,
        ///     r2 = 1/3.0f;
        /// Console.WriteLine(r1.Equals(r2)); // Displays false
        /// 
        /// r2.Precision = 10;
        /// Console.WriteLine(r1.Equals(r2)); // Displays true
        /// </code>
        /// </example>
        /// <para>
        /// Setting the precision will also have an effect on the graphing of the value. By default a precision of 15 digits is used, so 
        /// "0.36" will be formatted by the R1 formatter as "0.360000000000000", if precision is changed to 1 (ie: 1 significant digit) then 
        /// the value is rounded to one decimal place and formatted as "0.4".
        /// </para>
        /// <para>
        /// After parsing a message from the wire, precision is set based on the value read from the wire. If a wire value had 4 digits then the 
        /// Precision property is set as 4.
        /// </para>
        /// </remarks>
        [Property(Name = "precision", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("precision")]
        public override int Precision
        {
            get { return this.m_precision; }
            set
            {
                this.m_precision = value;
                if (this.m_precision == 0)
                    this.p_floatingPointEqualityTolerance = 1e-15;
                else
                    this.p_floatingPointEqualityTolerance = 1 / Math.Pow(10, m_precision);
            }
        }
            
        #region Operators

        /// <summary>
        /// Converts a <see cref="T:MARC.Everest.DataTypes.REAL"/> to a <see cref="T:System.Double"/>
        /// </summary>
        /// <param name="o">REAL to convert</param>
        /// <returns>Converted Double</returns>
        /// <exception cref="T:System.InvalidCastException">When the value of <paramref name="o"/> is null</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static explicit operator Double(REAL o)
        {
            if (o == null || o.IsNull || !o.Value.HasValue)
                throw new InvalidCastException("Nullable REAL must have a value");
            return (Double)o.Value;
        }


        /// <summary>
        /// Converts a <see cref="Double"/> to a <see cref="REAL"/>
        /// </summary>
        /// <param name="o">Double</param>
        /// <returns>Converted REAL</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator REAL(Double o)
        {
            REAL retVal = new REAL();
            retVal.Value = o;
            return retVal;
        }

        /// <summary>
        /// Converts a <see cref="T:System.String"/> to a <see cref="T:MARC.Everest.DataTypes.REAL"/>
        /// </summary>
        /// <param name="o">string to convert</param>
        /// <returns>Converted REAL</returns>
        /// <remarks>The resulting REAL will have a nullflavor of NoInformation if the cast was not possible. 
        /// <para>The result will also have a precision set based on the location of the decimal point in the string representation</para></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static explicit operator REAL(string o)
        {
            Double d = 0;
            if (!Double.TryParse(o, out d))
                return new REAL() { NullFlavor = DataTypes.NullFlavor.NoInformation };

            int precision = 0;
            if (o.Contains(EverestFrameworkContext.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                precision = o.Length - o.IndexOf(EverestFrameworkContext.CurrentCulture.NumberFormat.NumberDecimalSeparator) - 1;

            return new REAL(d)
            {
                Precision = precision
            };
        }
        /// <summary>
        /// Converts a REAL to INT
        /// </summary>
        public static explicit operator INT(REAL o)
        {
            return new INT() { Value = o == null ? null : (int?)o };
        }

        /// <summary>
        /// Converts a <see cref="REAL"/> to a <see cref="Double"/>
        /// </summary>
        /// <param name="o">REAL</param>
        /// <returns>Converted nullable Double</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator Double?(REAL o)
        {
            if (o == null)
                return null;
            return o.Value;
        }


        /// <summary>
        /// Compares <paramref name="a"/> with <paramref name="b"/> to ensure that
        /// <paramref name="a"/> is less than <paramref name="b"/>.
        /// </summary>
        /// <remarks>If either value is null then no comparison is made and the result is false</remarks>
        /// <param name="a">The value being compared</param>
        /// <param name="b">The value against which <paramref name="a"/> is being compared</param>
        public static bool operator <(REAL a, REAL b)
        {
            // If either parameter is null the result is false
            if (a == null || b == null || !a.Value.HasValue ^ !b.Value.HasValue)
                return false;

            // Determine if either value is set
            return a.Value < b.Value;
        }

        /// <summary>
        /// Compares <paramref name="a"/> with <paramref name="b"/> to ensure that
        /// <paramref name="a"/> is greater than <paramref name="b"/>.
        /// </summary>
        /// <remarks>If either value is null then no comparison is made and the result is false</remarks>
        /// <param name="a">The value being compared</param>
        /// <param name="b">The value against which <paramref name="a"/> is being compared</param>
        public static bool operator >(REAL a, REAL b)
        {
            // If either parameter is null the result is false
            if (a == null || b == null || !a.Value.HasValue ^ !b.Value.HasValue)
                return false;

            // Determine if either value is set
            return a.Value > b.Value;
        }


        /// <summary>
        /// Compares <paramref name="a"/> with <paramref name="b"/> to ensure that
        /// <paramref name="a"/> is less than or equal to <paramref name="b"/>.
        /// </summary>
        /// <remarks>If either value is null then no comparison is made and the result is false</remarks>
        /// <param name="a">The value being compared</param>
        /// <param name="b">The value against which <paramref name="a"/> is being compared</param>
        public static bool operator <=(REAL a, REAL b)
        {
            // If either parameter is null the result is false
            if (a == null || b == null || !a.Value.HasValue ^ !b.Value.HasValue)
                return false;

            // Determine if either value is set
            return a.Value <= b.Value;
        }



        /// <summary>
        /// Compares <paramref name="a"/> with <paramref name="b"/> to ensure that
        /// <paramref name="a"/> is greater than or equal to <paramref name="b"/>.
        /// </summary>
        /// <remarks>If either value is null then no comparison is made and the result is false</remarks>
        /// <param name="a">The value being compared</param>
        /// <param name="b">The value against which <paramref name="a"/> is being compared</param>
        public static bool operator >=(REAL a, REAL b)
        {
            // If either parameter is null the result is false
            if (a == null || b == null || !a.Value.HasValue ^ !b.Value.HasValue)
                return false;

            // Determine if either value is set
            return a.Value >= b.Value;
        }
        #endregion

        #region IEquatable<REAL> Members

        /// <summary>
        /// Determine if this REAL equals another REAL
        /// </summary>
        public bool Equals(REAL other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((QTY<Nullable<Double>>)other) &&
                    (other.Precision == 0 ? this.Precision : other.Precision) == (this.Precision == 0 ? other.Precision : this.Precision); // NB: 0 means nothing is stated about precision so it can be equivalent
            return result;

        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is REAL)
                return Equals(obj as REAL);
            return base.Equals(obj);
        }

        #endregion

        #region HL7 Operations

        /// <summary>
        /// REturns the maximum of the two REAL numbers
        /// </summary>
        public REAL Max(REAL other)
        {
            if (other == null || other.IsNull)
                return this.Clone() as REAL;
            else if (this.IsNull)
                return other.Clone() as REAL;
            else if (other > this)
                return new REAL((int)other);
            else
                return new REAL((int)this);
        }

        /// <summary>
        /// REturns the minimum of the two REAL numbers
        /// </summary>
        public REAL Min(REAL other)
        {
            if (other == null || other.IsNull)
                return this.Clone() as REAL;
            else if (this.IsNull)
                return other.Clone() as REAL;
            else if (other < this)
                return new REAL((int)other);
            else
                return new REAL((int)this);
        }

        /// <summary>
        /// Represents the REAL as an interval range represented by the precision
        /// </summary>
        /// <remarks>
        /// This function requires the <see cref="P:Precision"/> property to be set 
        /// in order to be of any use. When calling with the precision it will 
        /// return the appropriate Interval
        /// 
        /// <example>
        /// <code lang="cs" title="Range of 1/3">
        /// REAL oneThird = 1.0f/3.0f;
        /// oneThird.Precision = 3;
        /// IVL&lt;REAL> ivl = oneThird.ToIVL();
        /// Console.WriteLine("1/3 is between {0} and {1}", ivl.Low, ivl.High);
        /// // Output is: 
        /// // 1/3 is between 0.333 and 0.333999999999 
        /// </code>
        /// </example>
        /// </remarks>
        public IVL<REAL> ToIVL()
        {
            // For example, if we have 43.20399485
            // with a precision of 4, our interval should be 
            // 43.20390000 to 43.20399999

            if (this.IsNull || !this.Value.HasValue)
                return new IVL<REAL>() { NullFlavor = this.NullFlavor ?? DataTypes.NullFlavor.NoInformation };

            // First, we multiply the value to get store the number
            double tValue = this.Value.Value * Math.Pow(10, Precision),
                min = Math.Floor(tValue),
                max = Math.Ceiling(tValue) - Math.Pow(10, -(14 - Precision));

            return new IVL<REAL>(
                min / Math.Pow(10, Precision),
                max / Math.Pow(10, Precision)
               ) { LowClosed = true, HighClosed = true };
            

        }

        /// <summary>
        /// Adds <paramref name="a"/> to <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:REAL"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:REAL"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static REAL operator +(REAL a, REAL b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new REAL(a.Value.Value + b.Value.Value);
            else
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };
        }

        /// <summary>
        /// Negates <paramref name="a"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If the operand is null, the result is null</description></item>
        ///     <item><description>If the operand is null flavored, the result is a new instance of <see cref="T:REAL"/> with the null flavor of the operand</description></item>
        ///     <item><description>If the operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:REAL"/> with a null flavor of NoInformation</description></item>
        /// </list>
        /// </remarks>
        public static REAL operator -(REAL a)
        {
            if (a == null)
                return null;
            else if (a.IsNull)
                return new REAL() { NullFlavor = a.NullFlavor };
            else if (!a.Value.HasValue)
                return new REAL() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new REAL(-a.Value.Value);
        }

        /// <summary>
        /// Subtracts <paramref name="a"/> to <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:REAL"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:REAL"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static REAL operator -(REAL a, REAL b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new REAL(a.Value.Value - b.Value.Value);
            else
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };
        }

        ///// <summary>
        ///// Adds <paramref name="a"/> to <paramref name="b"/>
        ///// </summary>
        //public static REAL operator +(REAL a, INT b)
        //{
        //    if (a == null || a.IsNull || b == null || b.IsNull)
        //        return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
        //    else if (a.Value.HasValue && b.Value.HasValue)
        //        return new REAL(a.Value.Value + b.Value.Value);
        //    else
        //        return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };
        //}

        /// <summary>
        /// Subtract <paramref name="a"/> to <paramref name="b"/>
        /// </summary>
        //public static REAL operator -(REAL a, INT b)
        //{
        //    if (a == null || b == null)
        //        return null;
        //    else if (b.IsNull || a.IsNull)
        //        return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
        //    else if (a.Value.HasValue && b.Value.HasValue)
        //        return new REAL(a.Value.Value - b.Value.Value);
        //    else
        //        return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };
        //}

        /// <summary>
        /// Multiplies <paramref name="a"/> with <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:REAL"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:REAL"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static REAL operator *(REAL a, REAL b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new REAL(a.Value.Value * b.Value.Value);
            else
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };

        }

        /// <summary>
        /// Divides <paramref name="a"/> with <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:REAL"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:REAL"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static REAL operator /(REAL a, REAL b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (b == 0)
                return new REAL() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new REAL((int)a.Value.Value / (int)b.Value.Value);
            else
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };
        }

        ///// <summary>
        ///// Divides <paramref name="a"/> to <paramref name="b"/>
        ///// </summary>
        //public static REAL operator /(REAL a, INT b)
        //{
        //    if (a == null || a.IsNull || b == null || b.IsNull)
        //        return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
        //    else if (b == 0)
        //        return new REAL() { NullFlavor = DataTypes.NullFlavor.NoInformation };
        //    else if (a.Value.HasValue && b.Value.HasValue)
        //        return new REAL((double)a.Value.Value / (double)b.Value.Value);
        //    else
        //        return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };

        //}

        ///// <summary>
        ///// Multiplies <paramref name="a"/> to <paramref name="b"/>
        ///// </summary>
        //public static REAL operator *(REAL a, INT b)
        //{
        //    if (a == null || a.IsNull || b == null || b.IsNull)
        //        return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
        //    else if (a.Value.HasValue && b.Value.HasValue)
        //        return new REAL((double)a.Value.Value * (double)b.Value.Value);
        //    else
        //        return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };

        //}


        #endregion

        #region IComparable<REAL> Members

        /// <summary>
        /// Compares this REAL instance to another
        /// </summary>
        public int CompareTo(REAL other)
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
            else
                return this.Value.Value.CompareTo(other.Value.Value);

        }

        #endregion

        /// <summary>
        /// Determine semantic equality between this REAL and another REAL instance
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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
            REAL realOther = other as REAL;
            if (realOther == null)
                return false;
            else if (realOther.Value.HasValue && this.Value.HasValue && Math.Abs(realOther.Value.Value - this.Value.Value) <= Math.Abs(realOther.Value.Value * this.p_floatingPointEqualityTolerance))
                return true;
            else if (realOther.UncertainRange != null && !realOther.UncertainRange.IsNull &&
                this.UncertainRange != null && !this.UncertainRange.IsNull)
                return realOther.UncertainRange.SemanticEquals(this.UncertainRange);
            return false;
        }
      
    }
}