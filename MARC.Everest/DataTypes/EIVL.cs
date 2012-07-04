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
 * User: fyfej
 * Date: 4/27/2010 12:14:39 PM
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Interfaces;
using MARC.Everest.Attributes;
using System.Xml.Serialization;
using System.ComponentModel;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// Domain timing event type
    /// </summary>
    [Structure(Name = "DomainTimingEvent", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain, CodeSystem = "2.16.840.1.113883.5.139")]
    public enum DomainTimingEventType
    {
        /// <summary>
        /// before meal (from lat. ante cibus)
        /// </summary>
        [Enumeration(Value = "AC")]
        BeforeMeal,
        /// <summary>
        /// before lunch (from lat. ante cibus diurnus)
        /// </summary>
        [Enumeration(Value = "ACD")]
        BeforeLunch,
        /// <summary>
        /// before breakfast (from lat. ante cibus matutinus)
        /// </summary>
        [Enumeration(Value = "ACM")]
        BeforeBreakfast,
        /// <summary>
        /// before dinner (from lat. ante cibus vespertinus)
        /// </summary>
        [Enumeration(Value = "ACV")]
        BeforeDinner,
        /// <summary>
        /// the hour of sleep
        /// </summary>
        [Enumeration(Value = "HS")]
        HourOfSleep,
        /// <summary>
        /// between meals (from lat. inter cibus)
        /// </summary>
        [Enumeration(Value = "IC")]
        BetweenMeals,
        /// <summary>
        /// between lunch and dinner
        /// </summary>
        [Enumeration(Value = "ICD")]
        BetweenLunchAndDinner,
        /// <summary>
        /// between breakfast and lunch
        /// </summary>
        [Enumeration(Value = "ICM")]
        BetweenBreakfastAndLunch,
        /// <summary>
        /// between dinner and the hour of sleep
        /// </summary>
        [Enumeration(Value = "ICV")]
        BetweenDinnerAndSleep,
        /// <summary>
        /// after meal (from lat. post cibus)
        /// </summary>
        [Enumeration(Value = "PC")]
        AfterMeal,
        /// <summary>
        /// after lunch (from lat. post cibus diurnus)
        /// </summary>
        [Enumeration(Value = "PCD")]
        AfterLunch,
        /// <summary>
        /// after breakfast (from lat. post cibus matutinus)
        /// </summary>
        [Enumeration(Value = "PCM")]
        AfterBreakfast,
        /// <summary>
        /// after dinner (from lat. post cibus vespertinus)
        /// </summary>
        [Enumeration(Value = "PCV")]
        AfterDinner,
        /// <summary>
        /// The meal of breakfast
        /// </summary>
        [Enumeration(Value = "CM")]
        Breakfast,
        /// <summary>
        /// The meal of lunch
        /// </summary>
        [Enumeration(Value = "CD")]
        Lunch,
        /// <summary>
        /// The meal of dinner
        /// </summary>
        [Enumeration(Value = "CV")]
        Dinner,
        /// <summary>
        /// Any meal
        /// </summary>
        [Enumeration(Value = "C")]
        Meal, 
        /// <summary>
        /// upon waking
        /// </summary>
        [Enumeration(Value = "WAKE")]
        UponWaking
    }

    /// <summary>
    /// Specifies a periodic interval of time where the recurrence is based on the activities of daily living
    /// or other important events that are time related but not fully determined by time
    /// </summary>
    /// <remarks>
    /// <para>Represents an Event Related Periodic Interval of Time</para>
    /// <para>EIVL has been implemented as a generic as the XSD for the HL7 data types defines
    /// EIVL as either EIVL&lt;TS> (EIVL_TS) or EIVL&lt;NPPD&lt;TS>> (EIVL_NPPD_TS).</para></remarks>
    /// <example>
    /// <code title="XML Representation" lang="cs">
    /// <![CDATA[
    /// // Event driven interval, in this case after breakfast
    /// // Specifies a periodic interval: At most 12 hours
    ///    IVL<PQ> effectiveTime1 = new IVL<PQ>() { High = new PQ(12, "h") };
    ///
    ///    // the recurrence is based on this activity of daily living
    ///    CS<DomainTimingEventType> event1 = new CS<DomainTimingEventType>(DomainTimingEventType.BeforeBreakfast);
    ///    
    ///    // patient must perform action within 12 hours of eating breakfast
    ///    EIVL<TS> id = new EIVL<TS>();
    ///    id.Offset = effectiveTime1;
    ///    id.Event = event1;
    /// ]]>
    /// </code>        
    /// </example>
    [Structure(Name = "EIVL", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(TS))]
    [Serializable]
    public sealed class EIVL<T> : SXCM<T>, IEquatable<EIVL<T>>, IOriginalText
        where T : IAny
    {

        /// <summary>
        /// Create a new instance of the EIVL class
        /// </summary>
        public EIVL() : base() { }

        /// <summary>
        /// Create a new instance of the EIVL class with the specified <paramref name="operatorType"/>, <paramref name="eventType"/> and <paramref name="offset"/>
        /// </summary>
        /// <param name="eventType">The type of event that this interval relates to</param>
        /// <param name="offset">The offset (time interval)</param>
        public EIVL(DomainTimingEventType eventType, IVL<PQ> offset)
        {
            this.Offset = offset;
            this.Event = eventType;
        }

        /// <summary>
        /// Override of the value so that developers don't see it
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [XmlIgnore()]
        public override T Value { get; set; }

        /// <summary>
        /// A code for a common, periodical activity of daily living
        /// </summary>
        [Property(Name = "event", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, SupplierDomain = "2.16.840.1.113883.5.139")]
        public CS<DomainTimingEventType> Event { get; set; }

        /// <summary>
        /// An interval of elapsed time (duration, not absolute point in time) that marks the offsets for the 
        /// beginning width and end of the event related periodic interval
        /// </summary>
        [Property(Name = "offset", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public IVL<PQ> Offset { get; set; }

        /// <summary>
        /// Gets or sets the textual description of why the particular EIVL was conveyed
        /// </summary>
        [Property(Name = "originalText", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public ED OriginalText { get; set; }

        /// <summary>
        /// Validate this EIVL
        /// </summary>
        public override bool Validate()
        {
            bool valid = (NullFlavor != null) ^ (Offset != null || Event != null);

            if (this.Offset != null)
            {
                valid &= this.Offset.Low == null || this.Offset.Low.IsNull || PQ.IsValidTimeFlavor(this.Offset.Low);
                valid &= this.Offset.High == null || this.Offset.High.IsNull || PQ.IsValidTimeFlavor(this.Offset.High);
                valid &= this.Offset.Width == null || this.Offset.Width.IsNull || PQ.IsValidTimeFlavor(this.Offset.Width);
                valid &= this.Offset.Value == null || this.Offset.Value.IsNull || PQ.IsValidTimeFlavor(this.Offset.Value);
            }
            string cc = Util.ToWireFormat(this.Event);
            valid &= (cc.StartsWith("IC") || cc.StartsWith("AC") || cc.StartsWith("PC")) ^ (this.Offset != null);

            return valid;
        }

        /// <summary>
        /// Validate the data type and return the validation errors detected
        /// </summary>
        public override IEnumerable<IResultDetail> ValidateEx()
        {
            List<IResultDetail> retVal = new List<IResultDetail>(base.ValidateEx());
            if (this.Offset != null)
            {
                if (this.Offset.Low != null && !this.Offset.Low.IsNull && !PQ.IsValidTimeFlavor(this.Offset.Low))
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "EIVL", "When populated, the Offset.Low property must contain a valid PQ.TIME instance", null));
                if (this.Offset.High != null && !this.Offset.High.IsNull && !PQ.IsValidTimeFlavor(this.Offset.High))
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "EIVL", "When populated, the Offset.High property must contain a valid PQ.TIME instance", null));
                if (this.Offset.Width != null && !this.Offset.Width.IsNull && !PQ.IsValidTimeFlavor(this.Offset.Width))
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "EIVL", "When populated, the Offset.Width property must contain a valid PQ.TIME instance", null));
                if (this.Offset.Value != null && !this.Offset.Value.IsNull && !PQ.IsValidTimeFlavor(this.Offset.Value))
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "EIVL", "When populated, the Offset.Value property must contain a valid PQ.TIME instance", null));
            }
            string cc = Util.ToWireFormat(this.Event);
            if (!((cc.StartsWith("IC") || cc.StartsWith("AC") || cc.StartsWith("PC")) ^ (this.Offset != null)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "EIVL", "When the Event property implies before, after or between meals the Offset property must not be populated", null));
            return retVal;
            
        }

        #region IEquatable<EIVL<T>> Members

        /// <summary>
        /// Determines if this instance of EIVL of T is equal to another instance of EIVL of T
        /// </summary>
        public bool Equals(EIVL<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((SXCM<T>)other) &&
                    (other.Event != null ? other.Event.Equals(this.Event) : this.Event == null )&&
                    (other.Offset != null ? other.Offset.Equals(this.Offset) : this.Offset == null) &&
                    (other.Value != null ? other.Value.Equals(this.Value) : this.Value == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is EIVL<T>)
                return Equals(obj as EIVL<T>);
            return base.Equals(obj);
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

            EIVL<T> eivlOther = (EIVL<T>)other;
            if (eivlOther == null)
                return false;
            else
                return
                    (eivlOther.Event == null ? this.Event == null : (bool)eivlOther.Event.SemanticEquals(this.Event))
                    &&
                    (eivlOther.Offset == null ? this.Offset == null : (bool)eivlOther.Offset.SemanticEquals(this.Offset));
        }
        #endregion
    }
}
