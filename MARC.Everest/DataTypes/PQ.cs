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
using System.ComponentModel;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// A dimensioned quantity expressing the result of measuring
    /// </summary>
    /// <remarks>
    /// <para>
    /// A physical quantity is used primarily for the storage of decimal values
    /// with UCUM (<a href="http://unitsofmeasure.org/">http://unitsofmeasure.org/</a>) units describing
    /// their unit of measure.
    /// </para>
    /// <para><b>Conversion of Units</b></para>
    /// <para>The PQ type only contains builtin support for the conversion of time units (min, mo, s, hr, etc...). However
    /// the PQ type can utilize unit converters via <see cref="T:MARC.Everest.DataTypes.Interfaces.IUnitConverter"/> instances 
    /// attached to the <b>UnitConverters</b> array. A default implementation exists for
    /// SI units called <see cref="T:MARC.Everest.DataTypes.Converters.SimpleSiUnitConverter"/>.</para>
    /// <example>
    /// <code lang="cs" name="Attaching an SI unit converter">
    /// <![CDATA[
    /// PQ.UnitConverters.Add(new SimpleSiUnitConverter());
    /// PQ length = new PQ(1, "km");
    /// length -= new PQ(1, "m");
    /// // results in 
    /// // length = 0.999 km
    /// ]]>
    /// </code>
    /// </example>
    /// <para><b>PQ with Time</b></para>
    /// <para>
    /// The physical quantity class contained within the Everest Framework contains several useful functions
    /// for the handling of date values and the addition of time spans with time stamps (TS + PQ). The calculation
    /// these provisioned quantities is derived from the <see cref="T:System.TimeSpan"/> class. To calculate these
    /// Quantites, the PQ datatype will take the system ticks (100 nanosecond units) and convert them
    /// using the following unit conversion table (where <i>S</i> is 1 second or 0x989680 ticks)
    /// </para>
    /// <list type="table">
    /// <listheader>
    ///     <term>UCUM Measure</term>
    ///     <description>Ticks / Notes</description>
    /// </listheader>
    /// <item><term>us (Microseconds)</term><description>0xA = <i>S</i> / 1,000,000</description></item>
    /// <item><term>ms (Milliseconds)</term><description>0x2710 = <i>S</i> / 1,000</description></item>
    /// <item><term>s (Seconds)</term><description>0x989680 = <i>S</i></description></item>
    /// <item><term>ks (Killoseconds)</term><description>0x2540be400 = <i>S</i> * 1,000 : used when attempting to reduce extremely large second values from cast operators (approx 3 hr)</description></item>
    /// <item><term>Ms (Megaseconds)</term><description>0x9184e72a000 = <i>S</i> * 1,000,000 : used when attempting to reduce extremely large killosecond values from cast operators (approx 2 mo)</description></item>
    /// <item><term>Gs (Gigaseconds)</term><description>0x2386f26fc1000 = <i>S</i> * 1,000,000,000 : used when attempting to reduce extremely large megasecond values from cast operators (approx 31 a)</description></item>
    /// <item><term>min (Minutes)</term><description>0x23c34600 = <i>S</i> * 60</description></item>
    /// <item><term>hr (Hours)</term><description>0x861c46800 = <i>S</i> * 3,600</description></item>
    /// <item><term>d (Days)</term><description>0xc92a69c000 = <i>S</i> * 3,600 * 24</description></item>
    /// <item><term>wk (Weeks)</term><description>0x58028e44000 = <i>S</i> * 3,600 * 24 * 7</description></item>
    /// <item><term>mo (Months)</term><description>0x17e6ca109000 = <i>S</i> * 3,600 * 24 * 30.416 : since month duration vaires, and since given just a timespan it is impossible to calculate which months were used to get the PQ, we take 365 / 12 to be the length of a month</description></item>
    /// <item><term>a (Annum)</term><description>0x11ed178C6C000 = <i>S</i> * 3,600 * 24 * 365</description> or 31,536,000 seconds. Note there is some drifting associated with leap years, this can be corrected with <see cref="M:TS.GetLeapDays"/></item>
    /// </list>
    /// </remarks>
    /// <seealso cref="T:MARC.Everest.DataTypes.QTY"/>
    [Serializable][Structure(Name = "PQ", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("PQ", Namespace = "urn:hl7-org:v3")]
    public class PQ : QTY<Nullable<Decimal>>, IEquatable<PQ>, IComparable<PQ>, IPqTranslatable<PQ>, IPqScalar<PQ>, IDistanceable<PQ>, IImplicitInterval<PQ>
    {

        // A constant representing the number of ticks in a second
        private const long SECOND = TimeSpan.TicksPerSecond;

        /// <summary>
        /// Units of measure to tick map
        /// </summary>
        private static readonly Dictionary<string, long> s_tickMap = new Dictionary<string, long>() {
                { "us", 0xaL },
                { "ms", 0x2710L },
                { "s",  SECOND},
                { "ks", 0x2540be400L },
                { "Ms", 0x9184e72a000L },
                { "Gs", 0x2386f26fc10000L },
                { "min", 0x23c34600L },
                { "h", 0x861c46800L },
                { "d", 0xc92a69c000L },
                { "wk", 0x58028e44000L },
                { "mo", 0x17e6ca109000L },
                { "a", 0x11ed178C6C000L }
            };

        /// <summary>
        /// Gets a list of unit converters that can be used for 
        /// converting units
        /// </summary>
        public static List<IUnitConverter> UnitConverters { get; private set; }

        // Precision
        private int m_precision = 0;

        /// <summary>
        /// Static constructor
        /// </summary>
        static PQ()
        {
            PQ.UnitConverters = new List<IUnitConverter>();
        }

        /// <summary>
        /// Default ctor for PQ
        /// </summary>
        public PQ() : base() {
        }
        /// <summary>
        /// Create new instance of PQ using supplied values
        /// </summary>
        /// <param name="value">The value of the PQ</param>
        /// <param name="units">The units of the value</param>
        public PQ(Decimal value, string units) : base(value) { 
            this.Unit = units;
        }
        /// <summary>
        /// Create a new instance of PQ using the supplied timestamp and unit of time
        /// </summary>
        /// <param name="value">The value of the provisioned quantity</param>
        /// <param name="unit">A valid unit of time to use from the timespan</param>
        /// <remarks>This is different from using the cast operator from a timestamp as it allows
        /// the developer to specify the desired unit measure to use. 
        /// <para>
        /// Note: Since it is impossible to reliably calculate the value when using months (ie unit "mo") a month is considered to be 30.375 days
        /// </para>
        /// </remarks>
        /// <exception cref="T:System.InvalidOperationException">If <paramref name="unit"/> is not a valid unit of time</exception>
        public PQ(TimeSpan value, string unit)
        {
            long ticks = 0;
            if (!s_tickMap.TryGetValue(unit, out ticks))
                throw new InvalidOperationException(String.Format("'{0}' not recognized as valid unit of time", unit));
            this.Value = (decimal)(value.Ticks / (double)ticks);
            this.Unit = unit;
        }

        /// <summary>
        /// The unit of measure specified in a UCUM code. 
        /// </summary>
        [Property(Name = "unit", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public string Unit { get; set; }

        /// <summary>
        /// The number of significant digits of the decimal representation.
        /// </summary>
        /// <remarks>
        /// <para>Setting the precision has an effect on the graphing of the instance and is populated in the R1 formatter by 
        /// the processing of parsing.</para>
        /// </remarks>
        /// <seealso cref="P:MARC.Everest.Datatypes.REAL.Precision"/>
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
        /// The reason that this quantity was provided
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Property(Name = "codingRationale", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public SET<CodingRationale> CodingRationale { get; set; }
        /// <summary>
        /// An alternative representation of the physcial quantity
        /// </summary>
        /// <example>PQ with metric and imperial measures
        /// <code lang="cs" title="PQ translation">
        /// <![CDATA[
        /// // 2.1 Meters
        /// PQ h = new PQ((decimal)2.1f, "m");
        /// // Translates to 6.8897 ft
        /// h.Translation = new SET<PQR>(new PQR(6.8897f, "ft_i", "2.16.840.1.113883.6.8"), PQR.Comparator);
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Property(Name = "translation", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public SET<PQR> Translation { get; set; }

        /// <summary>
        /// Repraesentatione ad translatus
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public SPQR Translatus { get; set; }

        /// <summary>
        /// Determine if this PQ validates to the PQ.TIME flavor
        /// </summary>
        /// <param name="a">The PQ to validate</param>
        [Flavor("PQ.TIME")]
        public static bool IsValidTimeFlavor(PQ a)
        {
            return s_tickMap.ContainsKey(a.Unit);
        }

        /// <summary>
        /// Parse PQ from string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToDouble(System.String)")]
        public static implicit operator PQ(string s)
        {
            if (!s.Contains(" "))
            {
                return new PQ(System.Convert.ToDecimal(s), null);
            }
            else
            {
                string valPart = s.Substring(0, s.IndexOf(" "));

                string unitPart = s.Substring(s.IndexOf(" ") + 1);
                PQ retVal = new PQ(System.Convert.ToDecimal(valPart), unitPart);

                // Precision? 
                if (valPart.Contains("."))
                    retVal.Precision = valPart.Length - valPart.IndexOf(".") - 1;

                return retVal;
            }
        }

        /// <summary>
        /// Casts a timespan to a provisioned quantity
        /// </summary>
        /// <remarks>
        /// This operation will always return a PQ with the value consisting of the number of
        /// seconds in the timespan object. You can convert this value to other units of measure
        /// using the <see cref="M:Convert(System.String)"/> method.
        /// </remarks>
        public static implicit operator PQ(TimeSpan tsp)
        {
            decimal rValue = 2000;
            string rUnit = String.Empty;

            return new PQ((decimal)(tsp.Ticks / (float)s_tickMap["s"]), "s");
        }

        /// <summary>
        /// Casts the <paramref name="pq"/> to  <see cref="T:System.TimeSpan"/>
        /// </summary>
        /// <exception cref="T:System.InvalidCastException">When <paramref name="pq"/> is null, nullFlavored or has no value or when <paramref name="pq"/> does not have a time unit</exception>
        public static explicit operator TimeSpan(PQ pq)
        {
            if (pq == null || pq.IsNull || String.IsNullOrEmpty(pq.Unit) || !pq.Value.HasValue)
                throw new InvalidCastException("Nullable type must have a value for cast");
            long ticks = 0;
            if (s_tickMap.TryGetValue(pq.Unit, out ticks))
                return new TimeSpan((long)(pq.Value * ticks));
            throw new InvalidCastException(String.Format("Unit '{0}' is not understood as a reliable unit of time. Understood units are {{'us','ms','s','ks','Ms','min','h','d','wk','a'}}", pq.Unit));

        }

        /// <summary>
        /// Convert PQ to a double
        /// </summary>
        /// <exception cref="T:System.InvalidCastException">When the value if this PQ is null</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static explicit operator decimal(PQ s)
        {
            if (s == null)
                throw new InvalidCastException("Nullable type must have a value");
            return (Decimal)s.Value;
        }

        /// <summary>
        /// Converts <paramref name="s"/> to a nullable <see cref="System.Decimal"/>
        /// </summary>
        /// <param name="s">The <see cref="T:MARC.Everest.DataTypes.PQ"/> to cast</param>
        public static implicit operator decimal?(PQ s)
        {
            return s.Value;
        }

        /// <summary>
        /// Converts <paramref name="i"/> to a <see cref="T:MARC.Everest.DataTypes.PQ"/>
        /// </summary>
        /// <param name="d">The double to cast</param>
        /// <returns>The cast PQ</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "d")]
        public static implicit operator PQ(decimal d)
        {
            return new PQ(d, null);
        }

        /// <summary>
        /// Converts <paramref name="i"/> to a <paramref name="T:MARC.Everest.Datatypes.PQ"/>
        /// </summary>
        /// <param name="i">The integer to convert</param>
        /// <returns>The cast PQ</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "i")]
        public static implicit operator PQ(int i)
        {
            return new PQ((decimal)i, null);
        }

        /// <summary>
        /// Represent PQ as a string
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public override string  ToString()
        {
            if (this.Value.HasValue)
            {
                String strValue = this.Value.ToString();
                if (strValue.Contains(".") && this.Precision != 0)
                    strValue = this.Value.Value.ToString(String.Format("0.{0}", new String('0', this.Precision)));

                return String.Format("{0}{1}{2}", strValue, this.Value != null && !String.IsNullOrEmpty(this.Unit) ? " " : "", this.Unit);
            }
            else
                return "";
        }

        /// <summary>
        /// Validate this class
        /// </summary>
        public override bool Validate()
        {
            return (this.Value != null || this.UncertainRange != null) ^ (this.NullFlavor != null) &&
                ((this.Value != null || this.UncertainRange != null && ((this.Value != null) ^ (this.UncertainRange != null))) || (this.Value == null && this.UncertainRange == null)) &&
                ((this.Uncertainty != null && this.Uncertainty is PQ && (this.Uncertainty as PQ).IsUnitComparable(this.Unit)) || this.Uncertainty == null) &&
                ((this.UncertainRange != null && this.UncertainRange.Low is PQ && this.UncertainRange.High is PQ) || (this.UncertainRange == null)) &&
                ((this.UncertainRange != null && this.UncertainRange.Low != null && this.UncertainRange.High != null && (this.UncertainRange.Low as PQ).IsUnitComparable((this.UncertainRange.High as PQ).Unit)) || (this.UncertainRange == null));
        }

        /// <summary>
        /// Negates <paramref name="a"/>
        /// </summary>
        public static PQ operator -(PQ a)
        {
            if (a == null)
                return null;
            else if (a.IsNull)
                return new PQ() { NullFlavor = a.NullFlavor };
            else if (!a.Value.HasValue)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new PQ((decimal)-a.Value, a.Unit);
        }

        /// <summary>
        /// Subtracts <paramref name="b"/> from <paramref name="a"/>
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">When the unit of <paramref name="a"/> and <paramref name="b"/> are not comparable (ie: can't be converted by PQ)</exception>
        public static PQ operator -(PQ a, PQ b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (!a.IsUnitComparable(b.Unit))
                throw new InvalidOperationException("Both quantities must have the comparable units for this operation");
            else
                return new PQ((decimal)(a.Value - b.Convert(a.Unit).Value), a.Unit);
        }

        /// <summary>
        /// Adds <paramref name="b"/> to <paramref name="a"/>
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">When the unit of <paramref name="a"/> and <paramref name="b"/> are not comparable (ie: can't be converted by PQ)</exception>
        public static PQ operator +(PQ a, PQ b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (!a.IsUnitComparable(b.Unit))
                throw new InvalidOperationException("Both quantities must have the comparable units for this operation");
            else
                return new PQ((decimal)(a.Value + b.Convert(a.Unit).Value), a.Unit);
        }

        /// <summary>
        /// Multiplies <paramref name="b"/> with <paramref name="a"/>
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">When the unit of <paramref name="a"/> and <paramref name="b"/> are not comparable (ie: can't be converted by PQ)</exception>
        public static PQ operator *(PQ a, PQ b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (!a.IsUnitComparable(b.Unit))
                throw new InvalidOperationException("Both quantities must have the comparable units for this operation");
            else
                return new PQ((decimal)(a.Value * b.Convert(a.Unit).Value), a.Unit);
        }

        /// <summary>
        /// Multiplies <paramref name="b"/> by <paramref name="a"/>
        /// </summary>
        public static PQ operator *(REAL a, PQ b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new PQ((decimal)(a.Value * (double)b.Value), b.Unit);
        }

        /// <summary>
        /// Multiplies <paramref name="b"/> by <paramref name="a"/>
        /// </summary>
        public static PQ operator *(PQ a, REAL b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new PQ((decimal)((double)a.Value * b.Value), a.Unit);
        }

        /// <summary>
        /// Multiplies <paramref name="b"/> with <paramref name="a"/>
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">When the unit of <paramref name="a"/> and <paramref name="b"/> are not comparable (ie: can't be converted by PQ)</exception>
        public static PQ operator /(PQ a, PQ b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (!a.IsUnitComparable(b.Unit))
                throw new InvalidOperationException("Both quantities must have the comparable units for this operation");
            else if (b.Value == 0)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new PQ((decimal)(a.Value / b.Convert(a.Unit).Value), a.Unit);
        }

        /// <summary>
        /// Divides <paramref name="a"/> by <paramref name="b"/>
        /// </summary>
        public static PQ operator /(REAL a, PQ b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (b.Value == 0)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new PQ((decimal)(a.Value / (double)b.Value), b.Unit);
        }

        /// <summary>
        /// Divides <paramref name="a"/> by <paramref name="b"/>
        /// </summary>
        public static PQ operator /(PQ a, REAL b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull || !a.Value.HasValue || !b.Value.HasValue)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (b.Value == 0)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new PQ((decimal)((double)a.Value / b.Value), a.Unit);
        }

        #region IEquatable<PQ> Members

        /// <summary>
        /// Determine if this PQ equals another instance of PQ
        /// </summary>
        public bool Equals(PQ other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((QTY<Nullable<Decimal>>)other) &&
                    (other.CodingRationale != null ? other.CodingRationale.Equals(this.CodingRationale) : this.CodingRationale == null) &&
                    other.Precision == this.Precision &&
                    (other.Translation != null ? other.Translation.Equals(this.Translation) : this.Translation == null) &&
                    other.Unit == this.Unit;
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is PQ)
                return Equals(obj as PQ);
            return base.Equals(obj);
        }

        #endregion

        #region IComparable<PQ> Members

        /// <summary>
        /// Compares this PQ to another PQ
        /// </summary>
        /// <exception cref="T:System.ArgumentException">When the units of both PQ instances do not match</exception>
        public int CompareTo(PQ other)
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
            else if (!this.IsUnitComparable(other.Unit))
                throw new ArgumentException("Units must match to compare PQ");
            else
                return this.Value.Value.CompareTo(other.Convert(this.Unit).Value.Value);
        }

        #endregion

        #region IPqTranslatable<PQ> Members

        /// <summary>
        /// Translates this PQ by a factor of <paramref name="translation"/>
        /// by applying the addition operator
        /// </summary>
        /// <param name="translation">The quantity to translate (or shift) this PQ by</param>
        /// <remarks>Results in a new PQ containing the result of the translation</remarks>
        /// <exception cref="T:System.ArgumentException">If the units of the PQ do not match</exception>
        public PQ Translate(PQ translation)
        {
            return this + translation;
        }

        #endregion

        #region IPqScalar<PQ> Members

        /// <summary>
        /// Scales this PQ by <paramref name="scale"/>
        /// </summary>
        /// <param name="scale">The scale by which this instance should be scaled</param>
        /// <remarks>Results in a new PQ with the result</remarks>
        public PQ Scale(PQ scale)
        {
            return this * scale;
        }

        #endregion

        #region IDistanceable<PQ> Members

        /// <summary>
        /// Calculate the distance between this PQ and 
        /// another PQ with the same unit of measure
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">Thrown units of this and <paramref name="other"/> are not comparable</exception>
        public PQ Distance(PQ other)
        {
            return this - other;
        }

        #endregion

        #region Unit Conversion Functions

        /// <summary>
        /// Determines if this instance of PQ unit is comparable
        /// to the specified <paramref name="unit"/>
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException">When <paramref name="unit"/> is null</exception>
        private bool IsUnitComparable(string unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            if(unit == this.Unit) return true;
            
            bool isTimeMeasure = PQ.s_tickMap.ContainsKey(unit) && PQ.s_tickMap.ContainsKey(this.Unit);
            bool hasConverter = PQ.UnitConverters.Exists(o => o.CanConvert(this, unit)); ;
            
            return isTimeMeasure || hasConverter;
        }

        /// <summary>
        /// Convert this PQ to another unit of measure
        /// and return the result in a new PQ
        /// </summary>
        /// <exception cref="T:System.ArgumentException">When there is no method to convert between this PQ and <paramref name="unit"/></exception>
        /// <exception cref="T:System.InvalidOperationException">When this instance of PQ has no value to convert</exception>
        /// <exception cref="T:System.ArgumentNullException">When the unit argument is null</exception>
        public PQ Convert(string unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");
            else if (unit.Equals(this.Unit))
                return (PQ)this.Clone();
            else if (!this.IsUnitComparable(unit))
                throw new ArgumentException(String.Format("Cannot convert '{0}' to '{1}' as no map exists", this.Unit, unit));
            else if (!this.Value.HasValue)
                throw new InvalidOperationException("Cannot translate a null value");

            decimal newValue = this.Value.Value;
            var uc = PQ.UnitConverters.Find(o => o.CanConvert(this, unit));
            // Translation map
            long thisBase = 0,
                scaleBase = 0;
            if(uc != null)
                return uc.Convert(this, unit);
            else if (s_tickMap.TryGetValue(this.Unit, out thisBase) &&
                s_tickMap.TryGetValue(unit, out scaleBase))
            {
                thisBase *= (long)this.Value.Value;
                newValue = (decimal)(thisBase / (double)scaleBase);
            }
            else
            {
                unit = this.Unit;
            }

            return new PQ(newValue, unit);
        }

        #endregion

        /// <summary>
        /// Determine if this instance of PQ semantically equals another
        /// instance of a data type.
        /// </summary>
        public override BL SemanticEquals(IAny other)
        {
            // Base equality
            var baseEq = base.SemanticEquals(other);
            if (!(bool)baseEq)
                return baseEq;

            // Values are equal?
            PQ pqOther = other as PQ;
            if (pqOther == null)
                return false;
            else if (this.Value.HasValue && pqOther.Value.HasValue)
                return pqOther.Convert(this.Unit).Value.Value == this.Value.Value;
            else if (this.UncertainRange != null && !this.UncertainRange.IsNull &&
                pqOther.UncertainRange != null && !pqOther.UncertainRange.IsNull)
                return this.UncertainRange.Equals(pqOther.UncertainRange);
            return false;
        }


        #region IImplicitInterval<PQ> Members

        /// <summary>
        /// Represents the PQ as an interval range represented by the precision
        /// </summary>
        /// <remarks>
        /// This function requires the <see cref="P:Precision"/> property to be set 
        /// in order to be of any use. When calling with the precision it will 
        /// return the appropriate Interval
        /// <example>
        /// <code lang="cs" title="Range of 1/3">
        /// PQ oneThirdFoot = new PQ(1.0f/3.0f, "[ft_i]");
        /// oneThird.Precision = 3;
        /// IVL&lt;PQ> ivl = oneThird.ToIVL();
        /// Console.WriteLine("1/3 [ft_i] is between {0} and {1}", ivl.Low, ivl.High);
        /// // Output is: 
        /// // 1/3 [ft_i] is between 0.333 [ft_i] and 0.333999999999 [ft_i]
        /// </code>
        /// </example>
        /// </remarks>
        public IVL<PQ> ToIVL()
        {
            // For example, if we have 43.20399485
            // with a precision of 4, our interval should be 
            // 43.20390000 to 43.20399999

            if (this.IsNull || !this.Value.HasValue)
                return new IVL<PQ>() { NullFlavor = this.NullFlavor ?? DataTypes.NullFlavor.NoInformation };

            // Precision
            if(this.Precision == 0)
                return new IVL<PQ>(this, this);

            // First, we multiply the value to get store the number
            decimal tValue = this.Value.Value * (decimal)Math.Pow(10, Precision),
                min = Math.Floor(tValue),
                max = Math.Ceiling(tValue + (decimal)Math.Pow(10, -(20 - Precision))) - (decimal)Math.Pow(10, -(20 - Precision));
            
            return new IVL<PQ>(
                new PQ(min / (decimal)Math.Pow(10, Precision), this.Unit),
                new PQ(max / (decimal)Math.Pow(10, Precision), this.Unit)
               ) { LowClosed = true, HighClosed = true };


        }

        #endregion
    }
}