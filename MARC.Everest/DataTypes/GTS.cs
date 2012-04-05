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
    /// A set of points in time, specifying the timing of events and actions and the cyclical validity patterns that may exist
    /// for certain kinds of information
    /// </summary>
    /// <example>
    /// <code title="A GTS representing Mondays between September 1 and September 8 every other year" lang="cs">
    /// <![CDATA[
    /// GTS test = new GTS();
    /// SXPR<TS> labourDayDates = new SXPR<TS>();
    /// // Labour day can occur in between September 1 and September 8 of every year
    /// PIVL_TS labourDayWeek = new PIVL_TS(
    ///    // this is the week between September 1 and September 8
    ///    new IVL<TS>(DateTime.Parse("09/01/1987"), DateTime.Parse("09/08/1987")),
    ///    new PQ(1.0f, "a")); 
    /// labourDayWeek.Phase.LowClosed = false; // Including 09/01/1987
    /// labourDayWeek.Phase.HighClosed = true; // Not including 09/08/1987
    ///
    ///    
    /// // Labour day must occur on the Monday of the week above
    /// PIVL_TS mondayDayOfTheWeek = new PIVL_TS(
    ///    // this date is a Monday
    ///    new IVL<TS>(DateTime.Parse("01/04/2010"), DateTime.Parse("01/05/2010")),
    ///    new PQ(1.0f, "wk"));
    /// // set Monday to intersect with labour day week
    /// mondayDayOfTheWeek.Operator = SetOperator.Intersect; // intersect every Monday with the LabourDayWeek period
    /// mondayDayOfTheWeek.Phase.LowClosed = false;
    /// mondayDayOfTheWeek.Phase.HighClosed = true;
    ///
    /// // Find Labour Day in alternate years 
    /// PIVL_TS alternateYear = new PIVL_TS(
    ///    // this is every other year
    ///    new IVL<TS>(DateTime.Parse("01/01/2010"), DateTime.Parse("01/01/2011")),
    ///    new PQ(2.0f, "a"));
    /// alternateYear.Operator = SetOperator.Intersect; // intersect the Labour Day Monday with every other year
    /// alternateYear.Phase.LowClosed = false;
    /// alternateYear.Phase.HighClosed = true;
    ///
    /// labourDayDates.Add(labourDayWeek);
    /// labourDayDates.Add(mondayDayOfTheWeek);
    /// labourDayDates.Add(alternateYear);
    ///
    /// test.Hull = labourDayDates;
    /// ]]>
    /// </code>
    /// 
    /// </example>
    /// <seealso cref="T:MARC.Everest.DataType.EIVL&lt;>"/>
    /// <seealso cref="T:MARC.Everest.DataType.IVL_TS"/>
    /// <seealso cref="T:MARC.Everest.DataType.SXCM&lt;>"/>
    /// <seealso cref="T:MARC.Everest.DataType.PIVL_TS"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GTS")]
    [Serializable][Structure(Name = "GTS", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("GTS", Namespace = "urn:hl7-org:v3")]
    [TypeMap(Name = "QSI", ArgumentType = "TS")]
    [TypeMap(Name = "QSET", ArgumentType= "TS")]
    [TypeMap(Name = "QSU", ArgumentType = "TS")]
    [TypeMap(Name = "QSP", ArgumentType = "TS")]
    [TypeMap(Name = "QSD", ArgumentType = "TS")]
    [TypeMap(Name = "SXPR", ArgumentType = "TS")]
    [TypeMap(Name = "SXCM", ArgumentType = "TS")]
    public class GTS : ANY, IEquatable<GTS>
    {

        /// <summary>
        /// Represents the "hull" of the General Timing Specification object
        /// </summary>
        [XmlIgnore()]
        [Property(Name = "hull", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Mandatory)]
        public ISetComponent<TS> Hull { get; set; }

        /// <summary>
        /// Create a new instance of this GTS
        /// </summary>
        public GTS() : base() { }

        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        public GTS(EIVL<TS> hull)
            : base()
        {
            this.Hull = hull;
        }

        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        /// <param name="hull">The SXPR&lt;TS> to set as the hull</param>
        public GTS(QSET<TS> hull)
            : base()
        {
            this.Hull = hull;
        }
        
        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        /// <param name="hull">The IVL&lt;TS> to set as the hull</param>
        public GTS(IVL<TS> hull)
            : base()
        {
            this.Hull = hull;
        }

        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        /// <param name="hull">A PIVL&lt;TS> to set as the hull</param>
        public GTS(PIVL<TS> hull)
            : base()
        {
            this.Hull = hull;
        }

        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        /// <param name="hull"></param>
        public GTS(QSS<TS> hull) : base()
        {
            this.Hull = hull;
        }

        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        /// <param name="hull"></param>
        public GTS(QSI<TS> hull)
            : base()
        {
            this.Hull = hull;
        }

        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        /// <param name="hull"></param>
        public GTS(QSD<TS> hull)
            : base()
        {
            this.Hull = hull;
        }

        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        /// <param name="hull"></param>
        public GTS(QSU<TS> hull)
            : base()
        {
            this.Hull = hull;
        }

        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        /// <param name="hull"></param>
        public GTS(SXCM<TS> hull)
            : base()
        {
            this.Hull = hull;
        }

        /// <summary>
        /// Create a new instance of the GTS class with the specified <paramref name="hull"/>
        /// </summary>
        /// <param name="hull"></param>
        public GTS(SXPR<TS> hull)
            : base()
        {
            this.Hull = hull;
        }


        /// <summary>
        /// Explicitly casts QSET of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(QSET<TS> a)
        {
            return new GTS(a);
        }

        /// <summary>
        /// Explicitly casts SXCM of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(SXCM<TS> a)
        {
            return new GTS(a);
        }

        /// <summary>
        /// Explicitly casts SXPR of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(SXPR<TS> a)
        {
            return new GTS(a);
        }

        /// <summary>
        /// Explicitly casts IVL of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(IVL<TS> a)
        {
            return new GTS(a);
        }

        /// <summary>
        /// Explicitly casts EIVL of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(EIVL<TS> a)
        {
            return new GTS(a);
        }

        /// <summary>
        /// Explicitly casts QSI of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(QSI<TS> a)
        {
            return new GTS(a);
        }

        /// <summary>
        /// Explicitly casts QSD of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(QSD<TS> a)
        {
            return new GTS(a);
        }
        /// <summary>
        /// Explicitly casts QSP of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(QSP<TS> a)
        {
            return new GTS(a);
        }
        /// <summary>
        /// Explicitly casts QSS of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(QSS<TS> a)
        {
            return new GTS(a);
        }
        /// <summary>
        /// Explicitly casts QSU of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(QSU<TS> a)
        {
            return new GTS(a);
        }
        /// <summary>
        /// Explicitly casts PIVL of TS <paramref name="a"/> to a GTS
        /// </summary>
        public static implicit operator GTS(PIVL<TS> a)
        {
            return new GTS(a);
        }

        /// <summary>
        /// Flavor validator for the GTS.BOUNDEDPIVL
        /// </summary>
        [Flavor(Name = "GTS.BOUNDEDPIVL")]
        public static bool IsValidBoundedPivlFlavor(GTS gts)
        {
            // Hull variable
            var hull = gts.Hull;
            if (hull is SXPR<TS>)
                hull = (hull as SXPR<TS>).TranslateToQSET();

            // GTS is a null flavor
            if (gts.NullFlavor == null)
                return true;

            // Not a QSI, then bail
            if (!(hull is QSI<TS>))
                return false;

            bool isValid = true;
            foreach (var itm in hull as QSI<TS>)
                isValid &= itm is IVL<TS> || itm is PIVL<TS> && (itm as PIVL<TS>).Phase != null;
            return isValid;
        }

        /// <summary>
        /// Validate this GTS
        /// </summary>
        public override bool Validate()
        {
            return (this.Hull != null) ^ (this.NullFlavor != null) &&
                ((this.Hull != null && this.Hull.Validate()) || this.Hull == null);
        }

        /// <summary>
        /// Represent this object as a string
        /// </summary>
        public override string ToString()
        {
            return Hull == null ? null : Hull.ToString();
        }

        #region IEquatable<GTS> Members

        /// <summary>
        /// Determine if this GTS equals another instance of GTS
        /// </summary>
        public bool Equals(GTS other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((ANY)other) &&
                    (other.Hull != null ? other.Hull.Equals(this.Hull) : this.Hull == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is GTS)
                return Equals(obj as GTS);
            return base.Equals(obj);
        }

        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;

            GTS otherGts = other as GTS;
            if (other == null)
                return false;

            // Return hull equality
            return otherGts.Hull == null ? this.Hull == null : otherGts.Hull.SemanticEquals(this.Hull);
        }
        #endregion
    }
}