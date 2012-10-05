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
using MARC.Everest.Interfaces;
using System.Xml.Serialization;
using System.ComponentModel;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// Specifies how the repetitions of the Periodic Interval are aligned to the cycles of the underlying calendar. A non-aligned periodic interval recurs independently from the calendar(e.g every 30 days). An aligned periodic interval is synchronized with the calendar(e.g the 5th day of every month).
    /// </summary>
    [XmlType("CalendarCycle", Namespace = "urn:hl7-org:v3")]
    [Structure(Name = "CalendarCycle", CodeSystem= "2.16.840.1.113883.5.9", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    public enum CalendarCycle
    {        
        /// <summary>
        /// The calendar cycle is aligned to years continuosly.
        /// </summary>
        [Enumeration(Value = "CY")]
        [XmlEnum("CY")]
        Year,
        /// <summary>
        /// The calendar cycle is aligned to each month of the year.
        /// </summary>
        [Enumeration(Value = "MY")]
        [XmlEnum("MY")]
        MonthOfYear,
        /// <summary>
        /// The calendar cycle is aligned to months continuously.
        /// </summary>
        [Enumeration(Value = "CM")]
        [XmlEnum("CM")]
        Month,
        /// <summary>
        /// The calendar cycle is aligned to weeks continuously.
        /// </summary>
        [Enumeration(Value = "CW")]
        [XmlEnum("CW")]
        Week,
        /// <summary>
        /// The calendar cycle is aligned to each week of the year.
        /// </summary>
        [Enumeration(Value = "WY")]
        [XmlEnum("WY")]
        WeekOfYear,
        /// <summary>
        /// The calendar cycle is aligned to each day of the month.
        /// </summary>
        [Enumeration(Value = "DM")]
        [XmlEnum("DM")]
        DayOfMonth,
        /// <summary>
        /// The calendar cycle is aligned to days continuously.
        /// </summary>
        [Enumeration(Value = "CD")]
        [XmlEnum("CD")]
        Day,
        /// <summary>
        /// The calendar cycle is aligned to each day of the year.
        /// </summary>
        [Enumeration(Value = "DY")]
        [XmlEnum("DY")]
        DayOfYear,
        /// <summary>
        /// The calendar cycle is aligned to each day of the week.
        /// </summary>
        [Enumeration(Value = "DW")]
        [XmlEnum("DW")]
        DayOfWeek,
        /// <summary>
        /// The calendar cycle is aligned to each hour of the day.
        /// </summary>
        [Enumeration(Value = "HD")]
        [XmlEnum("HD")]
        HourOfDay,
        /// <summary>
        /// The calendar cycle is aligned to hours continuously.
        /// </summary>
        [Enumeration(Value = "CH")]
        [XmlEnum("CH")]
        Hour,
        /// <summary>
        /// The calendar cycle is aligned to each minute of the hour.
        /// </summary>
        [Enumeration(Value = "NH")]
        [XmlEnum("NH")]
        MinuteOfHour,
        /// <summary>
        /// The calendar cycle is aligned to minutes continuously.
        /// </summary>
        [Enumeration(Value = "CN")]
        [XmlEnum("CN")]
        Minute,
        /// <summary>
        /// The calendar cycle is aligned to each second of the minute.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Minue")]
        [Enumeration(Value = "SN")]
        [XmlEnum("SN")]
        SecondOfMinute,
        /// <summary>
        /// The calendar cycle is aligned to seconds continuously.
        /// </summary>
        [Enumeration(Value = "CS")]
        [XmlEnum("CS")]
        Second
    }

    ///// <summary>
    ///// Link to PIVL for timestamp, can't figure out any other "good" way to do this
    ///// </summary>
    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
    //[XmlType("PIVL_TS", Namespace = "urn:hl7-org:v3")]
    //[Obsolete("Consider using PIVL<TS> rather than PIVL_TS", true)]
    //public class PIVL_TS : PIVL<TS>
    //{
    //    /// <summary>
    //    /// Create a new instance of the PIVL type
    //    /// </summary>
    //    public PIVL_TS() : base() { }
    //    /// <summary>
    //    /// Create a new instance of the PIVL type using the phase and period specified
    //    /// </summary>
    //    /// <param name="phase">The phase of the PIVL</param>
    //    /// <param name="period">The period of the PIVL</param>
    //    public PIVL_TS(IVL<TS> phase, PQ period) : base(phase, period) { }

    //}

    /// <summary>
    /// An interval of time that recurs periodically. PIVL has two properties phase and period.
    /// </summary>
    /// <example>
    /// <code lang="cs" title="Repeat Oct 1 2009 - Nov 15 2009 every year">
    /// <![CDATA[
    ///     //Set the interval width, the "phase".
    ///     IVL<TS> phase = new IVL<TS> 
    ///     ( 
    ///     DateTime.Parse("October 1, 2009"),  
    ///     DateTime.Parse("November 15, 2009") 
    ///     ); 
    ///     phase.Operator = SetOperator.Inclusive;      
    ///     //Repeat the interval every year, the "period".
    ///     PQ period = new PQ(1.0f, "y");
    ///     //Create the periodic interval using the phase and period.
    ///     PIVL<TS> pInterval = new PIVL<TS>(phase, period); 
    ///     //Align the interval with the calendar year
    ///     pInterval.Alignment = CalendarCycle.Year;
    /// ]]>
    /// </code>
    /// </example>
    /// <seealso cref="T:MARC.Everest.DataTypes.SXCM{T}"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIVL"), Serializable]
    [Structure(Name = "PIVL", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("PIVL", Namespace = "urn:hl7-org:v3")]
    public class PIVL<T> : SXCM<T>, IEquatable<PIVL<T>>, IOriginalText
        where T : IAny
    {
        /// <summary>
        /// A prototype of the repeating interval
        /// </summary>
        [Property(Name = "phase", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Mandatory)]
        public IVL<T> Phase { get; set; }
        /// <summary>
        /// A time duration specifying as a reciprocal measure of the frequency at which the PIVL
        /// repeats
        /// </summary>
        /// <remarks>
        /// When specified, should be a reciprocal of the what the <see cref="P:Frequency"/> would have been. For example, 
        /// if the frequency would have been 2/10 mo (2 per 10 months) then the period is 10 mo/2 (or 5 mo). Alternatively if
        /// the frequency is 3/1 d, then the period is 1 d/3 (or 0.333... d). 
        /// <para>Only <see cref="P:Frequency"/> OR <see cref="P:Period"/> should be specified</para>
        /// </remarks>
        [Property(Name = "period", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural,
            Conformance = PropertyAttribute.AttributeConformanceType.Mandatory)]
        public PQ Period { get; set; }
        /// <summary>
        /// Specifies if and how the repetitions are aligned to the cycles of the underlying calendar
        /// </summary>
        /// <remarks>CalendarCycles starting with "C" are continuous.</remarks>
        [Property(Name = "alignment", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public CalendarCycle? Alignment { get; set; }
        /// <summary>
        /// Indicates whether the exact timing is up to the party executing the schedule.
        /// </summary>
        [Property(Name = "institutionSpecified", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public bool? InstitutionSpecified { get; set; }
        /// <summary>
        /// Indicates the value of the PIVL
        /// </summary>
        [Property(Name = "value", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override T Value { get; set; }

        /// <summary>
        /// Indicates the maximum number of times the interval can occur. Must be a valid INT.POS
        /// </summary>
        [Property(Name = "count", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public INT Count { get; set; }
        /// <summary>
        /// Indicates the frequency that the period repeats. 
        /// </summary>
        /// <remarks>Only <see cref="P:Frequency"/> OR <see cref="P:Period"/> should be specified
        /// <para>
        /// <see cref="P:Period"/> should be used in cases where it is easier for humans to read.</para>
        /// <example>Repeat every 5 days:
        /// <code lang="cs" title="Preferred">
        /// var pivl = new PIVL&lt;TS>();
        /// pivl.Period = new PQ(5, "d");
        /// </code>
        /// instead of :
        /// <code lang="cs" title="Correct, but not preferable">
        /// var pivl = new PIVL&lt;TS>();
        /// pivl.Frequency = new RTO&lt;INT, PQ>(1, new PQ(5, "d"));
        /// </code>
        /// </para>
        /// </example>
        /// <example>Repeat twice per day:
        /// <code lang="cs" title="Preferred">
        /// var pivl = new PIVL&lt;TS>();
        /// pivl.Frequency = new RTO&lt;INT, PQ>(2, new PQ(1, "d"));
        /// </code>
        /// instead of :
        /// <code lang="cs" title="Correct, but not preferable">
        /// var pivl = new PIVL&lt;TS>();
        /// pivl.Period = new PQ(0.5, "d");
        /// </code>
        /// </example>
        /// </remarks>
        /// 
        public RTO<INT, PQ> Frequency { get; set; }
        /// <summary>
        /// Gets or sets the reason why the specified interval was supplied
        /// </summary>
        [Property(Name = "originalText", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public ED OriginalText { get; set; }
        /// <summary>
        /// Create a new instance of the PIVL type
        /// </summary>
        public PIVL() : base() { }
        /// <summary>
        /// Create a new instance of the PIVL type using the phase and period specified
        /// </summary>
        /// <param name="phase">The phase of the PIVL</param>
        /// <param name="period">The period of the PIVL</param>
        public PIVL(IVL<T> phase, PQ period) { this.Phase = phase; this.Period = period; }
        /// <summary>
        /// Create a new instance of the PIVL type using the phase and period specified
        /// </summary>
        /// <param name="phase">The phase of the PIVL</param>
        /// <param name="frequency">The period of the PIVL</param>
        public PIVL(IVL<T> phase, RTO<INT,PQ> frequency) { this.Phase = phase; this.Frequency = frequency; }
        /// <summary>
        /// Create a new instance of the PIVL type using the phase and period specified
        /// </summary>
        /// <param name="phase">The phase of the PIVL</param>
        /// <param name="frequency">The period of the PIVL</param>
        /// <param name="count">The maximum number of times the PIVL can repreat</param>
        public PIVL(IVL<T> phase, RTO<INT, PQ> frequency, INT count) : this(phase, frequency) { this.Count = count; }
        /// <summary>
        /// Create a new instance of the PIVL type using the phase and period specified
        /// </summary>
        /// <param name="phase">The phase of the PIVL</param>
        /// <param name="period">The period of the PIVL</param>
        /// <param name="count">The maximum number of times the PIVL can repreat</param>
        public PIVL(IVL<T> phase, PQ period, INT count) : this(phase, period) { this.Count = count; }

        /// <summary>
        /// Validate the PIVL
        /// </summary>
        public override bool Validate()
        {
            return
                ((NullFlavor != null && Period == null && Phase == null && Frequency == null) || (NullFlavor == null)) &&
                ((NullFlavor == null && (Period != null) ^ (Frequency != null)) &&
                 ((this.Phase != null && this.Phase.Width != null && this.Period != null && this.Period.CompareTo(this.Phase.Width) >= 0) || (this.Phase == null || this.Period == null || this.Phase.Width == null)) ||
                 (NullFlavor != null));
        }

        #region IEquatable<PIVL<T>> Members

        /// <summary>
        /// Determine if this PIVL of T equals another PIVL of T
        /// </summary>
        public bool Equals(PIVL<T> other)
        {
            bool result = false;

            if (other != null)
                result = base.Equals((SXCM<T>)other) &&
                    (other.Phase != null ? other.Phase.Equals(this.Phase) : this.Phase == null) &&
                    (other.Period != null ? other.Period.Equals(this.Period) : this.Period == null) &&
                    (other.Count != null ? other.Count.Equals(this.Count) : this.Count == null) &&
                    other.Alignment == this.Alignment &&
                    other.InstitutionSpecified == this.InstitutionSpecified;
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is PIVL<T>)
                return Equals(obj as PIVL<T>);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Determine if this instance of PIVL contains <paramref name="member"/> according 
        /// to the PIVL's definition
        /// </summary>
        /// <remarks>
        /// <para>Please note that the contains method does not adhere to the <see cref="P:Alignment"/> property and
        /// the determination of containment is defined strictly on <see cref="P:Phase"/> and <see cref="P:Period"/>.</para>
        /// <para>The algorithm for determining containment is as follows:</para>
        /// <list type="number">
        ///     <item><description>Distance <paramref name="member"/> from the low bound of <see cref="P:Phase"/></description></item>
        ///     <item><description>Divide the result of the previous operation by <see cref="P:Period"/></description></item>
        ///     <item><description>Round the result of the previous operation to a whole number based on the unit specified in <see cref="P:Period"/></description></item>
        ///     <item><description>Multiply the result of the previous operation by the <see cref="P:Period"/></description></item>
        ///     <item><description>Translate <see cref="P:Phase"/> by the result of the previous operation</description></item>
        ///     <item><description>Determine if <paramref name="member"/> resides in the translated <see cref="P:Phase"/> from the previous operation</description></item>
        /// </list>
        /// </remarks>
        /// <param name="member">The proposed member of the set</param>
        /// <returns>True if the PIVL contains <paramref name="member"/></returns>
        /// <exception cref="T:System.ArgumentException">When the distance between two instances of <typeparamref name="T"/> is not calculable. This occurs when
        /// <typeparamref name="T"/> does not implement <see cref="T:MARC.Everest.DataTypes.Interfaces.IDistanceable{T}"/></exception>
        /// <exception cref="T:System.InvalidOperationException">When the <see cref="P:Phase"/> property is not populated</exception>
        public bool Contains(T member)
        {
            // Algorithm:
            //  phase.translate((int)((member - low) / period) * period)
            // 

            if(!(member is IDistanceable<T>))
                throw new ArgumentException("Unable to calculate the distance between objects of specified type", "member");
            if (this.Phase == null)
                throw new InvalidOperationException("Cannot determine containment of a PIVL that is not bound with a Phase");

            var distMemb = member as IDistanceable<T>;
            var period = this.Period;

            if (period == null && this.Frequency != null) // If frequency is specified, then make period the reciprocal
                period = this.Frequency.Denominator / this.Frequency.Numerator;

            // Get the distance between the target iteration and the phase
            PQ desiredTranslation = distMemb.Distance(this.Phase.Low);

            // Get the desired translation in repeats
            desiredTranslation /= period;
            desiredTranslation.Value = Math.Round(desiredTranslation.Value.Value, 0);
            
            if (this.Count != null && this.Count.CompareTo((int)desiredTranslation.Value) > 0) // number of iterations exceeds limit
                return false;
            else if (desiredTranslation.Value < 0) // Happens before offset, meaning first 
                return false;

            // Correct to periods we need to translate by
            desiredTranslation *= period;

            // Translate phase
            var translatedPhase = this.Phase.Translate(desiredTranslation);
            return translatedPhase.Contains(member);
        }

        /// <summary>
        /// Extended validation routine which returns the detected issues with the
        /// validated data type
        /// </summary>
        /// <remarks>
        /// An instance of PIVL is considered valid when :
        /// <list type="number">
        ///     <item>
        ///         <description>When the <see cref="P:NullFlavor"/> property is populated, neither <see cref="P:Period"/>, <see cref="P:Frequency"/> nor <see cref="P:Phase"/> are populated</description>
        ///     </item>
        ///     <item>
        ///         <description>When <see cref="P:NullFlavor"/> is not populated <see cref="P:Frequency"/> or <see cref="P:Period"/> must be set
        ///     </item>
        ///     <item>
        ///         <description>When <see cref="P:Frequency"/> is set <see cref="P:Period"/> must not be set
        ///     </item>
        ///     <item>
        ///         <description>When <see cref="P:Period"/> is set <see cref="P:Frequency"/> must not be set</description>
        ///     </item>
        ///     <item>
        ///         <description>If <see cref="P:Phase"/> is set and the <see cref="P:IVL.Width"/> property of <see cref="P:Phase"/> is populated, then it must be less than or equal to to value of the <see cref="P:Period"/> property</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());

            if(this.NullFlavor != null && (this.Period != null || this.Frequency != null || this.Phase != null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "PIVL", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            else if(this.NullFlavor == null && this.Period == null && this.Period == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "PIVL", ValidationMessages.MSG_NULLFLAVOR_MISSING, null));
            if((this.Frequency != null) ^ (this.Period != null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "PIVL", String.Format(ValidationMessages.MSG_INDEPENDENT_VALUE, "Frequency", "Period"), null));
            if(this.Phase != null && (this.Phase.Width == null || this.Phase.Width.IsNull || this.Phase.Width > this.Period))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "PIVL", "Width property of Phase must be less than the Period property", null));
            return retVal;
        }

        /// <summary>
        /// Determine semantic equality between this and <paramref name="other"/>
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

            PIVL<T> eivlOther = (PIVL<T>)other;
            if (eivlOther == null)
                return false;
            else
                return
                    (eivlOther.Period == null ? this.Period == null : (bool)eivlOther.Period.SemanticEquals(this.Period))
                    &&
                    (eivlOther.Frequency == null ? this.Frequency == null : (bool)eivlOther.Frequency.SemanticEquals(this.Frequency)) 
                    &&
                    (eivlOther.Phase == null ? this.Phase == null : (bool)eivlOther.Phase.SemanticEquals(this.Phase));
        }
    }
}
