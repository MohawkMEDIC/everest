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
    /// Integer numbers are precise numbers that are results of counting and enumerating.
    /// </summary>
    /// <remarks>
    /// Integer numbers are discrete, the set of integers is infinite but countable. No arbitrary limit is imposed on the range 
    /// of integer numbers. 
    /// <para>
    /// This implementation of the INT class (since Everest 1.0) contains operators that enable 
    /// us to perform arithmetic operations against the data within.
    /// <example>
    /// <code lang="cs" title="Integer division">
    /// <![CDATA[
    /// INT i1 = 10, input = 0;
    /// do
    /// {
    ///     Console.WriteLine("Enter a positive number to divide 10 by:");
    ///     input = (INT)Console.ReadLine();
    /// } while(input <= 0);
    /// Console.WriteLine("10/{0} = {1}", input, i1 / input);
    /// ]]>
    /// </code>
    /// </example>
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "INT"), Serializable]
    [Structure(Name = "INT", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("INT", Namespace = "urn:hl7-org:v3")]
    public class INT : QTY<Nullable<Int32>>, IIntegerValue, IEquatable<INT>, IComparable<INT>, IOrderedDataType<INT>
    {

        /// <summary>
        /// Creates a new instance of the INT class
        /// </summary>
        public INT() : base() { }
        /// <summary>
        /// Creates a new instance of the INT class with the specified initial value
        /// </summary>
        /// <param name="value">The initial value of the INT instance</param>
        public INT(int value) : base(value) { }
        
        /// <summary>
        /// Validates that <paramref name="i"/> meets criteria for flavor INT.POS
        /// </summary>
        /// <param name="i">The INT to validate</param>
        /// <remarks>i.Value > 0</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "i"), Flavor(Name = "POS")]
        [Flavor(Name = "INT.POS")]
        public static bool IsValidPosFlavor(INT i)
        {
            return i.Value != null && i.Value.HasValue && i.Value.Value > 0;
        }

        /// <summary>
        /// Validates that <paramref name="i"/> meets criteria for flavor INT.NONNEG
        /// </summary>
        /// <remarks>i.Value >= 0</remarks>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "i"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neg"), Flavor(Name = "NONNEG")]
        [Flavor(Name = "INT.NONNEG")]
        public static bool IsValidNonNegFlavor(INT i)
        {
            return i.Value != null && i.Value.HasValue && i.Value.Value >= 0;
        }

        #region Operators
        /// <summary>
        /// Converts an <see cref="INT"/> to an <see cref="int"/>
        /// </summary>
        /// <param name="o">INT to convert</param>
        /// <returns>Converted int</returns>
        /// <exception cref="T:System.InvalidCastException">When the valueof the INT is null</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static explicit operator int(INT o)
        {
            if (o == null || o.IsNull || !o.Value.HasValue)
                throw new InvalidCastException("Nullable type must have a value");
            return (Int32)o.Value;
        }

        /// <summary>
        /// Converts a REAL to INT
        /// </summary>
        public static implicit operator REAL(INT o)
        {
            return new REAL() { Value = o == null ? null : (double?)o };
        }

        /// <summary>
        /// Converts an <see cref="int"/> to an <see cref="INT"/>
        /// </summary>
        /// <param name="o">int to convert</param>
        /// <returns>Converted INT</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator INT(int o)
        {
            INT retVal = new INT();
            retVal.Value = o;
            return retVal;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="INT"/>
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Converted INT</returns>
        /// <remarks>If the string is not in a valid form then the result is populated with a nullflavor of NI</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.Parse(System.String)")]
        public static explicit operator INT(string s)
        {
            INT retVal = new INT();
            int value = 0;
            if (!Int32.TryParse(s, out value))
                retVal.NullFlavor = DataTypes.NullFlavor.NoInformation;
            else
                retVal.Value = value;
            return retVal;
        }

        /// <summary>
        /// Converts an <see cref="INT"/> to a <see cref="int"/>
        /// </summary>
        /// <param name="i">INT to convert</param>
        /// <returns>Converted nullable int</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "i")]
        public static implicit operator int?(INT i)
        {
            if (i == null)
                return null;
            return i.Value;
        }

        /// <summary>
        /// Compares <paramref name="a"/> with <paramref name="b"/> to ensure that
        /// <paramref name="a"/> is less than <paramref name="b"/>.
        /// </summary>
        /// <remarks>If either value is null then no comparison is made and the result is false</remarks>
        /// <param name="a">The value being compared</param>
        /// <param name="b">The value against which <paramref name="a"/> is being compared</param>
        public static bool operator <(INT a, INT b)
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
        public static bool operator >(INT a, INT b)
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
        public static bool operator <=(INT a, INT b)
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
        public static bool operator >=(INT a, INT b)
        {
            // If either parameter is null the result is false
            if (a == null || b == null || !a.Value.HasValue ^ !b.Value.HasValue)
                return false;

            // Determine if either value is set
            return a.Value >= b.Value;
        }
              
        #endregion
        
        #region IEquatable<INT> Members

        /// <summary>
        /// Determine if this INT equals another INT
        /// </summary>
        public bool Equals(INT other)
        {
            bool result = base.Equals((QTY<Nullable<Int32>>)other);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is INT)
                return Equals(obj as INT);
            return base.Equals(obj);
        }

        #endregion

        #region HL7 Operations
        
        /// <summary>
        /// Get the maximum of this integer and <paramref name="other"/>
        /// </summary>
        public INT Max(INT other)
        {
            if (other == null || other.IsNull)
                return this.Clone() as INT;
            else if (this.IsNull)
                return other.Clone() as INT;
            else if (other > this)
                return new INT((int)other);
            else 
                return new INT((int)this);
        }

        /// <summary>
        /// Get the maximum of this integer and <paramref name="other"/>
        /// </summary>
        public INT Min(INT other)
        {
            if (other == null || other.IsNull)
                return this.Clone() as INT;
            else if (this.IsNull)
                return other.Clone() as INT;
            else if (other < this)
                return new INT((int)other);
            else
                return new INT((int)this);
        }


        /// <summary>
        /// Adds <paramref name="a"/> to <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:INT"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:INT"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static INT operator+(INT a, INT b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new INT(a.Value.Value + b.Value.Value);
            else
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };
        }

        /// <summary>
        /// Negates <paramref name="a"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If the operand is null, the result is null</description></item>
        ///     <item><description>If the operand is null flavored, the result is a new instance of <see cref="T:INT"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If the operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:INT"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static INT operator -(INT a)
        {
            if (a == null)
                return null;
            else if (a.IsNull)
                return new INT() { NullFlavor = a.NullFlavor };
            else if (!a.Value.HasValue)
                return new INT() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new INT(-a.Value.Value);
        }

        /// <summary>
        /// Subtracts <paramref name="a"/> to <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:INT"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:INT"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static INT operator-(INT a, INT b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new INT(a.Value.Value - b.Value.Value);
            else
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };
        }

        /// <summary>
        /// Subtracts <paramref name="b"/> from <paramref name="a"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:REAL"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:REAL"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static REAL operator -(INT a, REAL b)
        {
            return (REAL)a - b;
        }

        /// <summary>
        /// Adds <paramref name="b"/> to <paramref name="a"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:REAL"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:REAL"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static REAL operator +(INT a, REAL b)
        {
            return (REAL)a + b;
        }
        /// <summary>
        /// Multiplies <paramref name="a"/> with <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:INT"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:INT"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static INT operator*(INT a, INT b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new INT(a.Value.Value * b.Value.Value);
            else
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };

        }

        /// <summary>
        /// Mutliplies <paramref name="a"/> by <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:REAL"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:REAL"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static REAL operator *(INT a, REAL b)
        {
            return (REAL)a * b;
        }
        /// <summary>
        /// Divides <paramref name="a"/> with <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:INT"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:INT"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static INT operator/(INT a, INT b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (b == 0)
                return new INT() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new INT((int)a.Value.Value / (int)b.Value.Value);
            else
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };
        }

        /// <summary>
        /// Divides <paramref name="a"/> to <paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:REAL"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:REAL"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static REAL operator/(INT a, REAL b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (b == 0)
                return new REAL() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new REAL((double)a.Value.Value / (double)b.Value.Value);
            else
                return new REAL() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };

        }

        /// <summary>
        /// Modulus of <paramref name="a"/> and<paramref name="b"/>
        /// </summary>
        /// <remarks>
        /// The following rules are applied to this arithmetic operation:
        /// <list type="bullet">
        ///     <item><description>If either operand is null, the result is null</description></item>
        ///     <item><description>If either operand is null flavored, the result is a new instance of <see cref="T:INT"/> with the null flavor of NoInformation</description></item>
        ///     <item><description>If either operand does not have a value associated with it (ie: not null and not null flavored with a null value) the result is a new instance of <see cref="T:INT"/> with a null flavor of Other</description></item>
        /// </list>
        /// </remarks>
        public static INT operator %(INT a, INT b)
        {
            if (a == null || b == null)
                return null;
            else if (b.IsNull || a.IsNull)
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NoInformation) };
            else if (b == 0)
                return new INT() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (a.Value.HasValue && b.Value.HasValue)
                return new INT(a.Value.Value % b.Value.Value);
            else
                return new INT() { NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.Other) };
        }

        /// <summary>
        /// Increment Unary operator on <paramref name="i"/>
        /// </summary>
        public static INT operator ++(INT i)
        {
            return i + 1;
        }

        /// <summary>
        /// Decrement unary operator on <paramref name="i"/>
        /// </summary>
        public static INT operator --(INT i)
        {
            return i - 1;
        }
        #endregion

        #region IComparable<INT> Members

        /// <summary>
        /// Compare this integer to another
        /// </summary>
        public int CompareTo(INT other)
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
        /// Determine if <paramref name="other"/> is semantically equal to the current instance
        /// of <see cref="T:INT"/>
        /// </summary>
        /// <param name="other">The other instance of <see cref="T:INT"/> to compare</param>
        /// <returns>A <see cref="T:BL"/> indicating whether the two instance of <see cref="T:INT"/> are equivalent</returns>
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
            INT intOther = other as INT;
            if (intOther == null)
                return false;
            else if (intOther.Value.HasValue && this.Value.HasValue && intOther.Value.Value == this.Value.Value)
                return true;
            else if (intOther.UncertainRange != null && !intOther.UncertainRange.IsNull &&
                this.UncertainRange != null && !this.UncertainRange.IsNull)
                return intOther.UncertainRange.SemanticEquals(this.UncertainRange);
            return false;


        }
        
        #region IOrderedDataType<INT> Members

        /// <summary>
        /// Gets the next value of INT given this value of INT
        /// </summary>
        public INT NextValue()
        {
            return this + 1;
        }

        /// <summary>
        /// Gets the previous value of INT given this value of INT
        /// </summary>
        public INT PreviousValue()
        {
            return this - 1;
        }

        #endregion
    }
}