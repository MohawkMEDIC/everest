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
using MARC.Everest.DataTypes.Interfaces;
using System.Xml.Serialization;

namespace MARC.Everest.DataTypes
{


    /// <summary>
    /// A binary value for use in boolean logic
    /// </summary>
    /// <remarks>
    /// <para>
    /// Business Name : BooleanValue
    /// </para>
    /// <para>
    /// This class provides a wrapper over a nullable boolean and provides three gate logic
    /// </para>
    /// </remarks>
    /// <example>
    /// <code title="Creating a boolean value three ways" lang="cs">
    /// <![CDATA[
    /// BL bool1, bool2, bool3;
    /// bool1 = false; // from a .NET boolean
    /// bool2 = new BL(false); // using constructor
    /// bool3 = coll1.Count > 0; // using logic
    /// ]]>
    /// </code>
    /// <code title="Using the BL class" lang="cs">
    /// <![CDATA[
    /// // In an ASPX page, determing if current user is administrator
    /// BL isAdmin = User.IsInRole("Administrators");
    /// // where response is an Everest message
    /// BL patientFound = response.controlActEvent.Subject.Count > 0;
    /// if(isAdmin && patientFound)
    ///     Console.Write("You may edit this patient");
    /// else if(!patientFound)
    ///     Console.Write("Could not find the patient");
    /// else if(!isAdmin)
    ///     Console.Write("You are not an administrator");
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    [Structure(Name = "BL", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("BL", Namespace = "urn:hl7-org:v3")]
    public class BL : PDV<Boolean?>, IBooleanValue, IEquatable<BL>, IComparable<BL>
    {
        /// <summary>
        /// Create a new instance of BL
        /// </summary>
        public BL() : base() { }
        /// <summary>
        /// Create a new instance of BL with <paramref name="value"/> as initial value
        /// </summary>
        /// <param name="value">The value to set</param>
        public BL(Boolean? value) : base(value) { }

        /// <summary>
        /// Validator for the NONULL flavor of anything that inherits from this class
        /// </summary>
        /// <param name="o">The BL to validate</param>
        /// <returns>True if validation succeeds</returns>
        /// <remarks>A BL meets validation criteria of BL.NONULL if
        /// <list type="bullet">
        /// <item>Null flavor is not assigned, AND</item>
        /// <item>Value is assigned</item>
        /// </list></remarks>
        [Flavor(Name = "NonNull")]
        [Flavor(Name = "BL.NONNULL")]
        public static bool IsValidNonNullFlavor(BL o)
        {
            return o.NullFlavor == null && o.Value != null;
        }

        /// <summary>
        /// Perform logical negation
        /// </summary>
        /// <example>
        /// <code title="Performing a NOT Operation" lang="cs">
        /// BL isTrue = true;
        /// Assert.IsFalse(isTrue.Not());
        /// </code>
        /// </example>
        /// <remarks>
        /// The Not operator as defined by HL7 has some caveats when dealing with nullFlavors in that a 
        /// BL with a nullFlavor that is NOTed will contain a NullFlavor (as per HL7v3 specification), however
        /// when cast to a bool will be false.
        /// </remarks>
        public BL Not()
        {
            // Perform a not
            bool? val = Value == true ? (bool?)false : Value == false ? (bool?)true : null;
            BL retVal = this.MemberwiseClone() as BL;
            retVal.Value = val;
            if (retVal.Value == null) retVal.NullFlavor = DataTypes.NullFlavor.NotApplicable;
            return retVal;
        }

        /// <summary>
        /// Perform a logical Xor
        /// </summary>
        /// <remarks>
        /// XOR returns true if b1 OR this is true but not both
        /// </remarks>
        /// <example>
        /// <code title="Performing an XOR" lang="cs">
        /// <![CDATA[
        /// BL isTrue = true, isFalse = false;
        /// BL xor = isTrue.Xor(isFalse);
        /// BL longCheck = isTrue.Or(isFalse).And(isTrue.And(isFalse).Not());
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b")]
        public BL Xor(BL b1)
        {
            //return (b1.Not().And(this)).Or((this.Not().And(b1)));
            if (null == b1)
                return new BL() { NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation };
            
            // See what operator overloading gives us
            // We could use this:
            // return (this & !b1) | (!this & b1);
            //
            // Instead of :
            return this.And(b1.Not()).Or(this.Not().And(b1));
            
            
        }

        /// <summary>
        /// Perform a logical or
        /// </summary>
        /// <param name="b1">The boolean value to compare to</param>
        /// <returns>The result of the OR operation</returns>
        /// <remarks>
        /// Implements a three gate logical OR, truth table is as follows:
        /// 
        /// <table border="1">
        ///     <tr>
        ///         <th>a/b</th>    
        ///         <th>F</th>
        ///         <th>T</th>
        ///         <th>Null</th>
        ///     </tr>
        ///     <tr>
        ///         <th>F</th>        
        ///         <td>F</td>
        ///         <td>T</td>
        ///         <td>Null</td>
        ///     </tr>
        ///     <tr>
        ///         <th>T</th>
        ///         <td>T</td>
        ///         <td>T</td>
        ///         <td>T</td>
        ///     </tr>
        ///     <tr>
        ///         <th>Null</th>
        ///         <td>Null</td>
        ///         <td>T</td>
        ///         <td>Null</td>
        ///     </tr>
        /// </table>
        /// </remarks>
        /// <example>
        /// <code title="Performing an OR">
        /// <![CDATA[
        /// BL isTrue = true, isFalse = false;
        /// BL result = isTrue.Or(isFalse);
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt16(System.Object)")]
        public BL Or(BL b1)
        {

            if(this.IsNull && b1.IsNull)
                return new BL() { NullFlavor = NullFlavorUtil.GetCommonParent(this.NullFlavor, b1.NullFlavor) };

            // Truth table
            // b1 > f t n
            // f    f t n
            // t    t t t 
            // n    n t n
            bool?[][] truth = new bool?[][] {
                new bool?[] { false, true, null },
                new bool?[] { true, true, true }, 
                new bool?[] { null, true, null }
            };

            int tx = this.Value == null || this.IsNull ? 2 : Math.Abs(Convert.ToInt16(this.Value));
            int ty = b1 == null || b1.Value == null || b1.IsNull ? 2 : Math.Abs(Convert.ToInt16(b1.Value));

            // New value
            BL retVal = new BL();
            retVal.Value = truth[tx][ty];
            if (retVal.Value == null) retVal.NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NotApplicable);
            return retVal;
        }
        /// <summary>
        /// Perform logical and
        /// </summary>
        /// <param name="b1">The value to compare to</param>
        /// <returns>The Result of the AND operation</returns>
        /// <remarks>
        /// Implements a three gate logical AND, truth table is as follows:
        /// 
        /// <table border="1">
        ///     <tr>
        ///         <th>a/b</th>    
        ///         <th>F</th>
        ///         <th>T</th>
        ///         <th>Null</th>
        ///     </tr>
        ///     <tr>
        ///         <th>F</th>        
        ///         <td>F</td>
        ///         <td>F</td>
        ///         <td>F</td>
        ///     </tr>
        ///     <tr>
        ///         <th>T</th>
        ///         <td>F</td>
        ///         <td>T</td>
        ///         <td>Null</td>
        ///     </tr>
        ///     <tr>
        ///         <th>Null</th>
        ///         <td>F</td>
        ///         <td>Null</td>
        ///         <td>Null</td>
        ///     </tr>
        /// </table>
        /// </remarks>
        /// <example>
        /// <code title="Performing an OR">
        /// <![CDATA[
        /// BL isTrue = true, isFalse = false;
        /// BL result = isTrue.And(isFalse);
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt16(System.Object)")]
        public BL And(BL b1)
        {

             if(this.IsNull && b1.IsNull)
                 return new BL() { NullFlavor = NullFlavorUtil.GetCommonParent(this.NullFlavor, b1.NullFlavor) };

            // Truth table
            // b1 > f t n
            // f    f f f 
            // t    f t n
            // n    f n n 
            bool?[][] truth = new bool?[][] {
                new bool?[] { false, false, false }, 
                new bool?[] { false, true, null }, 
                new bool?[] { false, null, null }
            };

            int tx = this.Value == null ? 2 : Math.Abs(Convert.ToInt16(this.Value));
            int ty = b1 == null || b1.Value == null ? 2 : Math.Abs(Convert.ToInt16(b1.Value));

            // New value
            BL retVal = new BL();
            retVal.Value = truth[tx][ty];
            if (retVal.Value == null) retVal.NullFlavor = new CS<NullFlavor>(DataTypes.NullFlavor.NotApplicable);
            return retVal;
        }

        /// <summary>
        /// Determines if <paramref name="other"/> implies this instance
        /// </summary>
        public BL Implies(BL other)
        {
            if(this == null || other == null || this.IsNull || other.IsNull)
                return new BL() { NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation };
            if (this == false && other == false || this == true && other == true)
                return true;
            else if ((bool)this.Xor(other))
                return false;
            else
                return new BL() { NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation };
        }

        /// <summary>
        /// Represents this BL as a string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public override string ToString()
        {
            return Value == null ? null : Value.ToString().ToLower();
        }

        #region Operators
        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="BL"/>
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>Converted BL</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToBoolean(System.String)")]
        public static implicit operator BL(string s)
        {
            if (s.Equals("0"))
                s = "false";
            else if (s.Equals("1"))
                s = "true";
            return new BL(s == null ? null : (bool?)Convert.ToBoolean(s));
        }

        /// <summary>
        /// Converts a <see cref="BL"/> to a <see cref="Boolean"/>
        /// </summary>
        /// <param name="o">BL to convert</param>
        /// <returns>Converted Boolean, false if the original boolean is null or nullFlavored</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static explicit operator Boolean(BL o)
        {
            return o == null || o.IsNull ? false : (Boolean)o.Value;
        }

        /// <summary>
        /// Converts a <see cref="Boolean"/> to a <see cref="BL"/>
        /// </summary>
        /// <param name="o">Boolean to convert</param>
        /// <returns>Converted BL</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator BL(Boolean o)
        {
            BL retVal = new BL();
            retVal.Value = o;
            return retVal;
        }

        /// <summary>
        /// Converts a <see cref="BL"/> to a <see cref="Boolean"/>
        /// </summary>
        /// <param name="o">BL to convert</param>
        /// <returns>Converted nullable Boolean</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator Boolean?(BL o)
        {
            return o == null ? null : o.Value;
        }

        /// <summary>
        /// Converts a <see cref="Boolean"/> to a <see cref="BL"/>
        /// </summary>
        /// <param name="o">Nullable Boolean to convert</param>
        /// <returns>Converted BL</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator BL(Boolean? o)
        {
            BL retVal = new BL();
            retVal.Value = o;
            return retVal;
        }
        #endregion
        /// <summary>
        /// Preforms a AND operation on two BLs.
        /// </summary>
        /// <param name="a">The first BL to AND with the second one.</param>
        /// <param name="b">The second BL to AND with the first BL.</param>
        /// <returns>The result of both input BLs ANDed together.</returns>
        /// <example>
        /// <code title="Using the &amp; Operator" lang="cs">
        /// <![CDATA[
        /// BL isTrue = true, isFalse = false;
        /// Assert.AreEqual(isTrue.And(isFalse), isTrue &amp;&amp; isFalse);
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public static BL operator &(BL a, BL b)
        {
            if (null != a)
                return a.And(b);
            else if (null != b)
                return b.And(a);
            else
                return new BL() { NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation };
        }
        /// <summary>
        /// Preforms a OR operation on two BLs.
        /// </summary>
        /// <param name="a">The first BL to OR with the second one.</param>
        /// <param name="b">The second BL to OR with the first one.</param>
        /// <returns>The result of both input BLs ORed together.</returns>
        /// <example>
        /// <code title="Using the | Operator" lang="cs">
        /// <![CDATA[
        /// BL isTrue = true, isFalse = false;
        /// Assert.AreEqual(isTrue.Or(isFalse), isTrue || isFalse);
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public static BL operator |(BL a, BL b)
        {
            if (null != a)
                return a.Or(b);
            else if (null != b)
                return b.Or(a);
            else
                return new BL() { NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation };
        }
        /// <summary>
        /// Preforms a XOR operation on two BLs.
        /// </summary>
        /// <param name="a">The first BL to XOR with the second one.</param>
        /// <param name="b">The second BL to XOR with the first one.</param>
        /// <returns>The result of both input BLs XORed together.</returns>
        /// <example>
        /// <code title="Using the ^ Operator" lang="cs">
        /// <![CDATA[
        /// BL isTrue = true, isFalse = false;
        /// Assert.AreEqual(isTrue.Xor(isFalse), isTrue ^ isFalse);
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public static BL operator ^(BL a, BL b)
        {
            if (null != a)
                return a.Xor(b);
            else if (null != b)
                return b.Xor(a);
            else
                return new BL() { NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation };
        }
        /// <summary>
        /// Inverts the state of the input BL.
        /// </summary>
        /// <param name="a">A BL to invert.</param>
        /// <returns>The input BL in a state inverted form.</returns>
        /// <example>
        /// <code title="Using the ! Operator" lang="cs">
        /// <![CDATA[
        /// BL isTrue = true;
        /// Assert.AreEqual(isTrue.Not(), !isTrue);
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public static BL operator !(BL a)
        {
            if (null != a)
                return a.Not();
            return null;
        }
        /// <summary>
        /// Test if the Input BL is true
        /// </summary>
        /// /// <param name="a">A BL to test.</param>
        /// <returns>The input BL in a true state, if BL is not null.</returns>
        /// <example>
        /// <code title="Using the True Operator" lang="cs">
        /// <![CDATA[
        /// BL isTrue = true;
        /// if (isTrue)
        /// {
        ///     System.Console.WriteLine("BL is true");
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("BL is false");
        /// }
        /// ]]>
        /// </code>
        /// </example>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        //public static bool operator true(BL a)
        //{
        //    return a != null ? !a.IsNull && a.Value.HasValue && a.Value.Value : false;
        //}

        /// <summary>
        /// Test if the Input BL to false
        /// </summary>
        /// /// <param name="a">A BL to test.</param>
        /// <returns>The input BL in a false state, if BL is not null.</returns>
        /// <example>
        /// <code title="Using the False Operator" lang="cs">
        /// <![CDATA[
        /// BL isTrue = false;
        /// if (isTrue)
        /// {
        ///     System.Console.WriteLine("BL is true");
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("BL is false");
        /// }
        /// ]]>
        /// </code>
        /// </example>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        //public static bool operator false(BL a)
        //{
        //    return a != null ? a.IsNull || a.Value.HasValue && !a.Value.Value : false;
        //}

        #region IEquatable<BL> Members

        /// <summary>
        /// Determine if this BL is equal to another BL
        /// </summary>
        public bool Equals(BL other)
        {
            bool result = base.Equals((PDV<Nullable<Boolean>>)other);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is BL)
                return Equals(obj as BL);
            return base.Equals(obj);
        }

        #endregion

        #region IComparable<BL> Members

        /// <summary>
        /// Compare this boolean to other
        /// </summary>
        public int CompareTo(BL other)
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
        /// Determine if this BL semantically equals another
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
            BL blOther = other as BL;
            if (blOther == null)
                return false;
            else if (blOther.Value != null && this.Value != null)
                return this.Value.Equals(blOther.Value);
            return false;

        }
    }
}
