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
 * Date: 09-22-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Attributes;
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Represents a <see cref="T:QSET{T}"/> that has been specialized as a periodic hull between <see cref="P:First"/> and <see cref="P:Second"/>
    /// </summary>
    /// <seealso cref="T:QSET{T}"/>
    /// <seealso cref="T:QSD{T}"/>
    /// <seealso cref="T:QSI{T}"/>
    /// <seealso cref="T:QSP{T}"/>
    /// <seealso cref="T:QSS{T}"/>
    /// <seealso cref="T:QSU{T}"/>
    /// <seealso cref="T:SXPR{T}"/>
    /// <seealso cref="T:SXCM{T}"/>
    /// <seealso cref="T:GTS"/>
    [Serializable]
    [Structure(Name = "QSP", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("QSP", Namespace = "urn:hl7-org:v3")]
    public class QSP<T> : QSET<T>, IEnumerable<ISetComponent<T>>, IEquatable<QSP<T>>
        where T : IAny
    {

        /// <summary>
        /// Creates a new instance of the QSET periodic hull class
        /// </summary>
        public QSP() : base() { }

        /// <summary>
        /// Creates a new instance of the QSET periodic hull class with the specified sets
        /// </summary>
        /// <param name="low">The set used as the basis for the periodic hull operation</param>
        /// <param name="high">The set used for the parameter of the periodic hull function</param>
        public QSP(ISetComponent<T> low, ISetComponent<T> high)
        {
            this.Low = low;
            this.High = high;
        }

        /// <summary>
        /// The set that forms the basis of the hull operation
        /// </summary>
        [Property(Name = "low", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        [XmlElement("low")]
        public ISetComponent<T> Low { get; set; }

        /// <summary>
        /// The set used as a parameter to the hull operation
        /// </summary>
        [Property(Name = "high", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        [XmlElement("high")]
        public ISetComponent<T> High { get; set; }

        /// <summary>
        /// Validate that the QSET is valid. A QSET is valid when it contains <see cref="P:First"/> and
        /// <paramref name="P:Second"/> and contains no-null terms.
        /// </summary>
        public override bool Validate()
        {
            bool isValid = (this.NullFlavor != null) ^ (this.Low != null && !this.Low.IsNull && this.High != null && !this.High.IsNull);
            return isValid;
        }

        /// <summary>
        /// Validate that the QSET is valid. A QSET is valid when it contains <see cref="P:First"/> and
        /// <paramref name="P:Second"/> and contains no-null terms.
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            List<IResultDetail> retVal = new List<IResultDetail>();
            if(!((this.NullFlavor != null) ^ (this.Low != null && !this.Low.IsNull && this.High != null && !this.High.IsNull)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QSP", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (this.Low == null || this.Low.IsNull)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QSP", String.Format(ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Low", String.Format("ISetComponent<{0}>", typeof(T).Name)), null));
            if (this.High == null || this.High.IsNull)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QSP", String.Format(ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "High", String.Format("ISetComponent<{0}>", typeof(T).Name)), null));
            return retVal;
        }

        /// <summary>
        /// Periodic hull
        /// </summary>
        protected override SetOperator GetEquivalentSetOperator()
        {
            return SetOperator.PeriodicHull;
        }

        #region IEnumerable<ISetComponent<T>> Members

        /// <summary>
        /// Get the enumerator
        /// </summary>
        public IEnumerator<ISetComponent<T>> GetEnumerator()
        {
            return new List<ISetComponent<T>> { this.Low, this.High }.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Get the enumerator
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
        
        #region IEquatable<QSP<T>> Members

        /// <summary>
        /// Determine equality between two QSS instances
        /// </summary>
        public bool Equals(QSP<T> other)
        {
            bool result = base.Equals(other);
            result &= this.High == null ? other.High == null : this.High.Equals(other.High);
            result &= this.Low == null ? other.Low == null : this.Low.Equals(other.Low);
            return result;
        }

        /// <summary>
        /// Determine equality between two items
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is QSP<T>)
                return Equals((QSP<T>)obj);
            return base.Equals(obj);
        }

        #endregion    

        /// <summary>
        /// Normalize
        /// </summary>
        public override MARC.Everest.Interfaces.IGraphable Normalize()
        {
            QSP<T> retVal = this.Clone() as QSP<T>;
            if (retVal.Low is SXPR<T>)
                retVal.Low = (retVal.Low as SXPR<T>).TranslateToQSET();
            if (retVal.High is SXPR<T>)
                retVal.High = (retVal.High as SXPR<T>).TranslateToQSET();
            return retVal;
        }
    }
}
