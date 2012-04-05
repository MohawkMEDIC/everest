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
using System.Globalization;
using System.ComponentModel;
using System.Xml.Serialization;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// A quantity specifying a point on the axis of natural time. 
    /// </summary>  
    /// <remarks>A point in time is most often represented 
    /// as a calendar expression.
    /// <para>The TS class also supports arithmetic date operations with other
    /// TS as well as <see cref="T:MARC.Everest.DataTypes.PQ"/>.</para>
    /// </remarks>
    /// <example>
    /// <code lang="cs" title="TS flavors">
    /// <![CDATA[ 
    ///        // TS Sample, Time Zone is EDT
    ///       TS test = DateTime.Parse("March 1, 2009 12:00:00 AM");
    ///        Console.WriteLine(test.ToString()); // output : 20090301000000.000-0500  
    ///        test.Flavor = "DATETIME";
    ///        Console.WriteLine(test.ToString()); // output : 20090301000000-0500
    ///        test.Flavor = "DATE";
    ///        Console.WriteLine(test.ToString()); // output : 20090301
    ///        test.DateValuePrecision = DatePrecision.Year;
    ///        Console.WriteLine(test.ToString()); // output : 2009
    /// ]]>
    /// </code>
    /// <code lang="cs" title="Date Arithmetic">
    ///    <![CDATA[
    ///     
    /// // Two dates
    /// TS now = DateTime.Now, 
    ///     endOfWorld = DateTime.Parse("12-21-2012");
    /// 
    /// // Difference between dates
    /// Console.WriteLine("{0} until {1}",  endOfWorld - now, endOfWorld);
    /// // Sample Output: 44.30293848594 Ms until 20121221000000.000-0500
    /// // Adding time to a date
    /// Console.WriteLine("1 week from now: {0}", now + new PQ(1, "wk"));
    /// // Sample Output: 1 week from now: 20110801153525.358-0500
    ///    ]]>
    /// </code>
    /// </example>
    [Serializable][Structure(Name = "TS", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [DefaultProperty("DateValue")]
    [XmlType("TS", Namespace = "urn:hl7-org:v3")]
    public class TS : QTY<string>, IPointInTime, IEquatable<TS>, IComparable<TS>, IPqTranslatable<TS>, IDistanceable<TS>, IImplicitInterval<TS>, IOrderedDataType<TS>
    {

        // Formats for the various flavors
        private static Dictionary<string, DatePrecision> m_flavorPrecisions = new Dictionary<string, DatePrecision>()
        {
            { "DATETIME", DatePrecision.Second },
            { "TS.DATETIME", DatePrecision.Second },
            { "DATE", DatePrecision.Day },
            { "TS.DATE",  DatePrecision.Day },
            { "", DatePrecision.Full },
            { "FULLDATETIME", DatePrecision.Full },
            { "TS.FULLDATETIME", DatePrecision.Full },
            { "TS.FULLDATEWITHTIME",DatePrecision.Full },
            { "FULLDATEWITHTIME", DatePrecision.Full },
            { "TS.DATE.FULL", DatePrecision.Day },
            { "TS.FULLDATE", DatePrecision.Day },
            { "TS.DATETIME.FULL", DatePrecision.Second }
        };

        /// <summary>
        /// Precision formats
        /// </summary>
        private static Dictionary<DatePrecision, string> m_precisionFormats = new Dictionary<DatePrecision,string>()
        {
            { DatePrecision.Day, "yyyyMMdd" },
            { DatePrecision.Full, "yyyyMMddHHmmss.fffzzzz" },
            { DatePrecision.FullNoTimezone, "yyyyMMddHHmmss.fff" },
            { DatePrecision.Hour, "yyyyMMddHHzzzz" },
            { DatePrecision.HourNoTimezone, "yyyyMMddHH" },
            { DatePrecision.Minute, "yyyyMMddHHmmzzzz" },
            { DatePrecision.MinuteNoTimezone, "yyyyMMddHHmm" }, 
            { DatePrecision.Month, "yyyyMM" },
            { DatePrecision.Second, "yyyyMMddHHmmsszzzz" },
            { DatePrecision.SecondNoTimezone, "yyyyMMddHHmmss" },
            { DatePrecision.Year, "yyyy" }
        };

        /// <summary>
        /// Create a new instance of the timestamp
        /// </summary>
        /// DateTime.Now.ToString("yyyyMMddHHmmss.ffffzzzz").Replace(":", "")
        public TS() : base() { }

        /// <summary>
        /// Create a new instance of TS with the specified value
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TS(DateTime value) : this() { DateValue = value; }

        /// <summary>
        /// Create a new instance of TS with the specified datetime value and the precision specified
        /// </summary>
        /// <param name="value">The value of the timestamp</param>
        /// <param name="precision">The precision of the timestamp</param>
        public TS(DateTime value, DatePrecision precision) : this(value) { this.DateValuePrecision = precision; }
                
        /// <summary>
        /// Gets or sets the string value of this date/time value
        /// </summary>
        /// <remarks>
        /// <para>The output of this property is dependent on the setting of the 
        /// DateValuePrecision property. If the DateValuePrecision property has 
        /// not value when this method is called, then the date is output in
        /// DatePrecision.Full and DateValuePrecision is assigned the value
        /// of Full</para>
        /// </remarks>
        /// <exception cref="T:System.FormatException">When the format of the date string is not in a recognizable form</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)"), Property(Name = "value", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        [Browsable(false)]
        public override string Value
        {
            get
            {
                
                if (DateValue == default(DateTime))
                    return null;

                // Impose flavor formatting
                DatePrecision rDp = DatePrecision.Full;

                if (DateValuePrecision.HasValue)
                    rDp = DateValuePrecision.Value;
                else if (!m_flavorPrecisions.TryGetValue(Flavor ?? "", out rDp))
                    rDp = DatePrecision.Full;
                else // If date precision has no value then it is set to full
                    DateValuePrecision = DatePrecision.Full;

                // Get proper format
                string flavorFormat = m_precisionFormats[rDp];

                // Re-format
                return DateValue.ToString(flavorFormat).Replace(":", "");
            }
            set
            {
                try
                {
                    // Get proper format
                    //string flavorFormat = "";
                    //if (!m_flavorFormats.TryGetValue(Flavor ?? "", out flavorFormat))
                    //    flavorFormat = m_flavorFormats[""];

                    //// Special case, apparently we can have a valid flavor (FULLDATETIME) with no MS... 
                    //// this causes some issues with the parsing of dates
                    //if (flavorFormat.Contains(".") && !value.Contains("."))
                    //    flavorFormat = flavorFormat.Replace(".fff", "");
                    //else if (!flavorFormat.Contains(".") && value.Contains("."))
                    //{
                    //    int fMs = value.IndexOf("."),
                    //        eMs = value.IndexOf("-");

                    //    value = value.Remove(fMs, eMs - fMs);
                    //}
                    //else if (value.Contains("."))
                    //{
                    //    int fMs = value.IndexOf(".") + 1,
                    //        eMs = value.IndexOf("-");

                    //    flavorFormat = flavorFormat.Replace(".fff", "." + new String('f', eMs - fMs));
                    //}
                    
                    //// Flavor
                    //if (value.Contains("-") && !flavorFormat.EndsWith("zzzz"))
                    //    flavorFormat += "zzzz";

                    // Parse a correct precision 
                    try
                    {
                        if (value == null)
                        {
                            this.DateValue = default(DateTime);
                            return;
                        }

                        this.DateValuePrecision = (DatePrecision)(value.Length);

                        // HACK: Correct the milliseonds to be three digits if four are passed into the parse function
                        if ((int)this.DateValuePrecision.Value == 24 || this.DateValuePrecision.Value == DatePrecision.Second && value.Contains("."))
                        {
                            this.DateValuePrecision = (DatePrecision)((int)this.DateValuePrecision.Value - 1);
                            int eMs = value.Contains("-") ? value.IndexOf("-") - 1 : value.Contains("+") ? value.IndexOf("+") - 1 : value.Length - 1;
                            value = value.Remove(eMs, 1);
                        }
                        else if ((int)this.DateValuePrecision.Value > (int)DatePrecision.Second && value.Contains(".")) // HACK: sometimes milliseconds can be one or two digits
                        {
                            int eMs = value.Contains("-") ? value.IndexOf("-") - 1 : value.Contains("+") ? value.IndexOf("+") : value.Length - 1;
                            int sMs = value.IndexOf(".");
                            value = value.Insert(eMs + 1, new string('0', 3 - (eMs - sMs)));
                            this.DateValuePrecision = (DatePrecision)(value.Length);
                        }
                    }
                    catch (Exception) { this.DateValuePrecision = DatePrecision.Full; }

                    string flavorFormat = null;
                    if(!m_precisionFormats.TryGetValue(this.DateValuePrecision.Value, out flavorFormat))
                        flavorFormat = m_precisionFormats[DatePrecision.Full];

                    // Now parse the date string
                    if (value.Length > flavorFormat.Length)
                        DateValue = DateTime.ParseExact(value.Substring(0, flavorFormat.Length + (flavorFormat.Contains("z") ? 1 : 0)), flavorFormat, CultureInfo.InvariantCulture);
                    else
                        DateValue = DateTime.ParseExact(value, flavorFormat.Substring(0, value.Length), CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    throw new FormatException(string.Format("The date string '{0}' is not in the proper format", value), e);
                }
                
            }
        }

        /// <summary>
        /// Gets the precision of the DateValue. This is the precision that a date-time can be trusted to. 
        /// <para>
        /// For example, a date time of January 1, 2009 with a precision of Month means that the dateTime is
        /// only precise to the month (January 2009). 
        /// </para>
        /// </summary>
        /// <example>
        /// Date value precision modifies the precision of the <see cref="DateValue"/> property
        /// <code lang="cs" title="Date Value Precision Example">
        /// <![CDATA[
        /// TS sample = DateTime.Now; // Date value is at full precision
        /// sample.DateValuePrecision = DatePrecision.Year; 
        /// sample.ToString(); // Output is "2009"
        /// ]]>
        /// </code>
        /// </example>
        public DatePrecision? DateValuePrecision { get; set; }

        /// <summary>
        /// JF: Fixes issue with setting flavor then precision
        /// </summary>
        public override string Flavor
        {
            get
            {
                return base.Flavor;
            }
            set
            {
                if (!DateValuePrecision.HasValue)
                {
                    DatePrecision tdprec = DatePrecision.Full;
                    if (m_flavorPrecisions.TryGetValue(value, out tdprec))
                        DateValuePrecision = tdprec;
                }
                base.Flavor = value;
            }
        }

        /// <summary>
        /// Get or set the string as a function of the date value
        /// </summary>
        public virtual DateTime DateValue { get; set; }

        /// <summary>
        /// Represent this TS as a string
        /// </summary>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Convert a string to a datetime
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static explicit operator TS(string s)
        {
            TS retVal = new TS();
            retVal.Value = s;
            return retVal;
        }

        /// <summary>
        /// Convert an <see cref="T:MARC.Everest.DataTypes.ST"/> to a datetime
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static explicit operator TS(ST s)
        {
            TS retVal = new TS();
            retVal.Value = s;
            return retVal;
        }

        /// <summary>
        /// Cast this TS to a datetime
        /// </summary>
        /// <exception cref="T:System.InvalidCastException">When the instance of TS is null or nullFlavored</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static explicit operator DateTime(TS o)
        {
            if (o == null || o.IsNull)
                throw new InvalidCastException("Nullable TS must have a value");
            return o.DateValue;
        }

        /// <summary>
        /// Cast this TS to a datetime
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator DateTime?(TS o)
        {
            return o.DateValue;
        }

        /// <summary>
        /// Cast a DateTime to a TS
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator TS(DateTime o)
        {
            TS retVal = new TS();
            retVal.DateValue = o;
            return retVal;
        }

        /// <summary>
        /// Flavor handler for TS.Date
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "ts"), Flavor(Name = "Date")]
        [Flavor(Name = "TS.DATE")]
        public static bool IsValidDateFlavor(TS ts)
        {
            return ts.DateValuePrecision <= DatePrecision.Day;
        }

        /// <summary>
        /// Flavor handler for TS.DATETIME
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "ts"), Flavor(Name = "DateTime")]
        [Flavor(Name = "TS.DATETIME")]
        public static bool IsValidDateTimeFlavor(TS ts)
        {
            // Correct the date time
            return ts.DateValuePrecision <= DatePrecision.MinuteNoTimezone ||
                ts.Value.Contains("-") && ts.DateValuePrecision <= DatePrecision.Minute;
        }

        /// <summary>
        /// Flavor handler for FullDateTime
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "ts"), Flavor(Name = "FullDateTime")]
        [Flavor(Name = "TS.FULLDATETIME")]
        [Flavor(Name = "TS.DATETIME.FULL")]
        public static bool IsValidFullDateTimeFlavor(TS ts)
        {
            return ts.DateValuePrecision >= DatePrecision.Second;
        }

        /// <summary>
        /// A flavor handler for Instant
        /// </summary>
        [Flavor(Name = "TS.INSTANT")]
        public static bool IsValidInstantFlavor(TS ts)
        {
            return ts.DateValuePrecision == DatePrecision.Full;
        }
        /// <summary>
        /// Flavor handler for FullDate
        /// </summary>
        [Flavor(Name = "TS.FULLDATE")]
        [Flavor(Name = "TS.DATE.FULL")]
        public static bool IsValidFullDateFlavor(TS ts)
        {
            return ts.DateValuePrecision == DatePrecision.Day;
        }

        // TODO: Continue the Date flavors

        #region IPrimitiveDataValue<DateTime> Members

        DateTime IPrimitiveDataValue<DateTime>.Value
        {
            get
            {
                return DateValue;
            }
            set
            {
                DateValue = value;
            }
        }

        #endregion

        /// <summary>
        /// Adds <paramref name="b"/> to <paramref name="a"/>
        /// </summary>
        public static TS operator +(TS a, PQ b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull)
                return new TS() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
            {
                var retVal = new TS(a.DateValue.Add((TimeSpan)b), a.DateValuePrecision.HasValue ? a.DateValuePrecision.Value : DatePrecision.Full);
                // How many leap days are in-between this and the new value?
                var leapDays = TS.GetLeapDays(a, retVal);
                if(leapDays.Value != 0)
                    retVal += leapDays;
                return retVal;
            }
        }

        /// <summary>
        /// Subtracts <paramref name="b"/> from <paramref name="a"/>
        /// </summary>
        public static TS operator -(TS a, PQ b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull)
                return new TS() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
            {
                var retVal = new TS(a.DateValue.Subtract((TimeSpan)b), a.DateValuePrecision.HasValue ? a.DateValuePrecision.Value : DatePrecision.Full);
                // How many leap days are in-between this and the new value?
                var leapDays = TS.GetLeapDays(retVal, a);
                if(leapDays.Value != 0)
                    retVal -= leapDays;
                return retVal;

            }
        }

        /// <summary>
        /// Subtracts <paramref name="b"/> from <paramref name="a"/>
        /// </summary>
        /// <remarks>The result of this operation is always returned in seconds</remarks>
        public static PQ operator -(TS a, TS b)
        {
            if (a == null || b == null)
                return null;
            else if (a.IsNull || b.IsNull)
                return new PQ() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else
                return new PQ(a.DateValue.Subtract((DateTime)b), "s");

        }

        /// <summary>
        /// Convert this timestamp (with precision) to an interval
        /// </summary>
        /// <returns>The converted interval</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)")]
        public IVL<TS> ToIVL()
        {

            // Absolutely precise
            if (this.DateValuePrecision == DatePrecision.Full ||
                this.DateValuePrecision == DatePrecision.FullNoTimezone)
                return new IVL<TS>(this, this) { LowClosed = true, HighClosed = true };

            IVL<TS> retVal = new IVL<TS>();
            
            int maxMonth = DateTime.DaysInMonth(this.DateValue.Year, this.DateValue.Month);

            // Convert
            switch (this.DateValuePrecision)
            {
                case DatePrecision.Year: // Year precision : from Jan 1 YEAR to Dec 31 YEAR
                    return new IVL<TS>(
                        new TS(new DateTime(this.DateValue.Year, 1, 1, 0, 0, 0, 0), DatePrecision.Full),
                        new TS(new DateTime(this.DateValue.Year, 12, 31, 23, 59, 59, 999), DatePrecision.Full)
                    ) { LowClosed = true, HighClosed = true };
                case DatePrecision.Month: // Month precision : from MONTH 1 YEAR to MONTH max YEAR
                    return new IVL<TS>(
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, 1, 0, 0, 0, 0), DatePrecision.Full),
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, maxMonth, 23, 59, 59, 999), DatePrecision.Full)
                    ) { LowClosed = true, HighClosed = true };
                case DatePrecision.Day: // Day precision
                    return new IVL<TS>(
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, this.DateValue.Day, 0, 0, 0, 0), DatePrecision.Full),
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, this.DateValue.Day, 23, 59, 59, 999), DatePrecision.Full)
                    ) { LowClosed = true, HighClosed = true };
                case DatePrecision.Hour: // Hour precision
                case DatePrecision.HourNoTimezone:
                    return new IVL<TS>(
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, this.DateValue.Day, this.DateValue.Hour, 0, 0, 0), DatePrecision.Full),
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, this.DateValue.Day, this.DateValue.Hour, 59, 59, 999), DatePrecision.Full)
                    ) { LowClosed = true, HighClosed = true };
                case DatePrecision.Minute: // Minute precision
                case DatePrecision.MinuteNoTimezone:
                    return new IVL<TS>(
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, this.DateValue.Day, this.DateValue.Hour, this.DateValue.Minute, 0, 0), DatePrecision.Full),
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, this.DateValue.Day, this.DateValue.Hour, this.DateValue.Minute, 59, 999), DatePrecision.Full)
                    ) { LowClosed = true, HighClosed = true };
                case DatePrecision.Second: // Second precision
                case DatePrecision.SecondNoTimezone:
                    return new IVL<TS>(
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, this.DateValue.Day, this.DateValue.Hour, this.DateValue.Minute, this.DateValue.Second, 0), DatePrecision.Full),
                        new TS(new DateTime(this.DateValue.Year, this.DateValue.Month, this.DateValue.Day, this.DateValue.Hour, this.DateValue.Minute, this.DateValue.Second, 999), DatePrecision.Full)
                    ) { LowClosed = true, HighClosed = true } ;
                default:
                    return new IVL<TS>(this, this) { };
            }
        }

        #region IEquatable<TS> Members


        /// <summary>
        /// Determines if this TS is equal to another TS
        /// </summary>
        public bool Equals(TS other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((QTY<String>)other) &&
                    (this.DateValuePrecision ?? DatePrecision.Full) == (other.DateValuePrecision ?? DatePrecision.Full) &&
                    (this.DateValue.ToString(m_precisionFormats[this.DateValuePrecision ?? DatePrecision.Full]) == other.DateValue.ToString(m_precisionFormats[other.DateValuePrecision ?? DatePrecision.Full]));
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is TS)
                return Equals(obj as TS);
            return base.Equals(obj);
        }

        #endregion

        #region IComparable<TS> Members

        /// <summary>
        /// Compares this TS instance to another
        /// </summary>
        public int CompareTo(TS other)
        {
            if (other == null || other.IsNull)
                return 1;
            else if (this.IsNull && !other.IsNull)
                return -1;
            else
                return this.DateValue.CompareTo(other.DateValue);

        }
        #endregion

        #region IPqTranslatable<TS> Members

        /// <summary>
        /// Translates this date by the amount specified in
        /// <paramref name="translation"/>
        /// </summary>
        /// <param name="translation">The amount to translate the date by</param>
        /// <remarks>Results in a new TS instance that contains the translation</remarks>
        public TS Translate(PQ translation)
        {
            return this + translation;
        }

        #endregion


        #region IDistanceable<TS> Members

        /// <summary>
        /// Calculate the distance between this instance and another instance of
        /// TS
        /// </summary>
        public PQ Distance(TS other)
        {
            return this - other;
        }

        #endregion

        /// <summary>
        /// Validates that this TS meets the basic validation criteria
        /// </summary>
        /// <remarks>
        /// <para>Basic validation criteria is considered:</para>
        /// <list type="bullet">
        ///     <item><description>If <see cref="P:NullFlavor"/> is not null, then <see cref="P:DateValue"/> and <see cref="P:UncertainRange"/> cannot be set</description></item>
        ///     <item><description>If <see cref="P:DateValue"/> or <see cref="P:UncertainRange"/> are populated, then <see cref="P:NullFlavor"/> cannot be set</description></item>
        ///     <item><description>Both <see cref="P:DateValue"/> and <see cref="P:UncertainRange"/> cannot be set at the same time</description></item>
        ///     <item><description>Any uncertainty (<see cref="P:Uncertainty"/>, or <see cref="P:UncertainRange"/>) must contain valid <see cref="T:PQ"/>.TIME or <see cref="T:IVL`1"/>of PQ.TIME</description></item>
        /// </list>
        /// </remarks>
        public override bool Validate()
        {
            return (NullFlavor != null) ^ ((DateValue != default(DateTime) || UncertainRange != null) &&
                ((this.Uncertainty != null && this.Uncertainty is PQ && PQ.IsValidTimeFlavor(this.Uncertainty as PQ)) || (this.Uncertainty == null)) &&
                ((this.UncertainRange != null && this.UncertainRange.Low is PQ && this.UncertainRange.High is PQ && PQ.IsValidTimeFlavor(this.UncertainRange.Low as PQ) && PQ.IsValidTimeFlavor(this.UncertainRange.High as PQ)) || this.UncertainRange == null) &&
                (((DateValue != default(DateTime)) ^ (this.UncertainRange != null)) || (this.DateValue == null && this.UncertainRange == null)));

        }

        /// <summary>
        /// Determines if this instance of TS is semantically equal to <paramref name="other"/>
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
            TS tsOther = other as TS;
            if (tsOther == null)
                return false;
            else if (tsOther.DateValuePrecision.HasValue &&
                this.DateValuePrecision.HasValue &&
                tsOther.DateValuePrecision.Value.HasTimeZone() ^
                this.DateValuePrecision.Value.HasTimeZone())
                return new BL() { NullFlavor = DataTypes.NullFlavor.NoInformation };
            else if (tsOther.DateValuePrecision.Equals(this.DateValuePrecision))
                return tsOther.Value.Equals(this.Value);
            else if (tsOther.UncertainRange != null && this.UncertainRange != null &&
                !tsOther.UncertainRange.IsNull && !this.UncertainRange.IsNull)
                return this.UncertainRange.SemanticEquals(tsOther.UncertainRange);
            return false;
        }

        #region IOrderedDataType<TS> Members

        /// <summary>
        /// Gets the next value of TS given this TS' precision
        /// </summary>
        public TS NextValue()
        {
            return TranslateDateInternal(1);
        }

        /// <summary>
        /// Gets the previous value of TS given this TS' precision
        /// </summary>
        public TS PreviousValue()
        {
            return TranslateDateInternal(-1);
        }

        /// <summary>
        /// Translate a date based on the value of <see cref="P:DateValuePrecision"/>
        /// </summary>
        private TS TranslateDateInternal(int value)
        {
            TS retVal = null;
            switch (DateValuePrecision.Value)
            {
                case DatePrecision.Day:
                    retVal = (TS)this.DateValue.AddDays(value);
                    break;
                case DatePrecision.HourNoTimezone:
                case DatePrecision.Hour:
                    retVal = (TS)this.DateValue.AddHours(value);
                    break;
                case DatePrecision.Minute:
                case DatePrecision.MinuteNoTimezone:
                    retVal = (TS)this.DateValue.AddMinutes(value);
                    break;
                case DatePrecision.Month:
                    retVal = (TS)this.DateValue.AddMonths(value);
                    break;
                case DatePrecision.Second:
                case DatePrecision.SecondNoTimezone:
                    retVal = (TS)this.DateValue.AddSeconds(value);
                    break;
                case DatePrecision.Year:
                    retVal = (TS)this.DateValue.AddYears(value);
                    break;
                case DatePrecision.Full:
                case DatePrecision.FullNoTimezone:
                    retVal = (TS)this.DateValue.AddMilliseconds(value);
                    break;
                default:
                    throw new InvalidOperationException("Cannot determine how to translate this date");
            }
            retVal.DateValuePrecision = this.DateValuePrecision;
            return retVal;
        }

        /// <summary>
        /// Gets the number of days (total) between <paramref name="a"/>
        /// and <paramref name="b"/>
        /// </summary>
        internal static PQ GetLeapDays(TS a, TS b)
        {
            if (a == null || b == null)
                return null;
            
            PQ retVal = new PQ(0, "d");
            TS low = a, high = b;
            int val = 1;
            if (a.CompareTo(b) > 0)
            {
                low = b;
                high = a;
                val = -1;
            }
            for (int i = low.DateValue.Year; i < high.DateValue.Year; i++)
                if(DateTime.IsLeapYear(i))
                    retVal += new PQ(val, "d");
            return retVal;
        }


        #endregion
    }
}
