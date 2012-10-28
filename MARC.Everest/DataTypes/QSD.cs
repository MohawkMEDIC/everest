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
    /// Represents a <see cref="T:QSET{T}"/> that has been specialized as a difference between <see cref="P:First"/> and <see cref="P:Second"/>
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
    [Structure(Name = "QSD", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("QSD", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class QSD<T> : QSET<T>, IEnumerable<ISetComponent<T>>, IEquatable<QSD<T>>
        where T : IAny
    {

        /// <summary>
        /// Creates a new instance of the QSET difference class
        /// </summary>
        public QSD() : base() { }

        /// <summary>
        /// Creates a new instance of the QSET difference class with the specified sets
        /// </summary>
        /// <param name="minuend">The first set from which <paramref name="subtrahend"/> should be subtracted</param>
        /// <param name="subtrahend">The set by which <paramref name="minuend"/> should be subtracted</param>
        public QSD(ISetComponent<T> minuend, ISetComponent<T> subtrahend)
        {
            this.Minuend = minuend;
            this.Subtrahend = subtrahend;
        }

        /// <summary>
        /// The set from which <see cref="P:Subtrahend"/> is subtracted
        /// </summary>
        [Property(Name = "first", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        [XmlElement("first")]
        public ISetComponent<T> Minuend { get; set; }

        /// <summary>
        /// The set by which <see cref="P:Minuend"/> is to be subtracted
        /// </summary>
        [Property(Name = "second", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        [XmlElement("second")]
        public ISetComponent<T> Subtrahend { get; set; }

        /// <summary>
        /// Validate that the QSET is valid. A QSET is valid when it contains <see cref="P:First"/> and
        /// <paramref name="P:Second"/> and contains no-null terms.
        /// </summary>
        public override bool Validate()
        {
            bool isValid = (this.NullFlavor != null) ^ (this.Minuend != null && !this.Minuend.IsNull && this.Subtrahend != null && !this.Subtrahend.IsNull);
            return isValid;
        }

        /// <summary>
        /// Validate that the QSET is valid. A QSET is valid when it contains <see cref="P:First"/> and
        /// <paramref name="P:Second"/> and contains no-null terms.
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            List<IResultDetail> retVal = new List<IResultDetail>();
            if (!((this.NullFlavor != null) ^ (this.Minuend != null && !this.Minuend.IsNull && this.Subtrahend != null && !this.Subtrahend.IsNull)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QSD", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (this.Minuend == null || this.Minuend.IsNull)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QSD", String.Format(ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Minuend", String.Format("ISetComponent<{0}>", typeof(T).Name)), null));
            if (this.Subtrahend == null || this.Subtrahend.IsNull)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QSD", String.Format(ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Subtrahend", String.Format("ISetComponent<{0}>", typeof(T).Name)), null));
            return retVal;
        }
        /// <summary>
        /// Get equivalent set operator
        /// </summary>
        protected override SetOperator GetEquivalentSetOperator()
        {
            return SetOperator.Exclusive;
        }

        #region IEnumerable<ISetComponent<T>> Members

        /// <summary>
        /// Get the enumerator
        /// </summary>
        public IEnumerator<ISetComponent<T>> GetEnumerator()
        {
            return new List<ISetComponent<T>> { this.Minuend, this.Subtrahend }.GetEnumerator();
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

        #region IEquatable<QSD<T>> Members

        /// <summary>
        /// Determine equality between two QSS instances
        /// </summary>
        public bool Equals(QSD<T> other)
        {
            bool result = base.Equals(other);
            result &= this.Minuend == null ? other.Minuend == null : this.Minuend.Equals(other.Minuend);
            result &= this.Subtrahend == null ? other.Subtrahend == null : this.Subtrahend.Equals(other.Subtrahend);
            return result;
        }

        /// <summary>
        /// Determine equality between two items
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is QSD<T>)
                return Equals((QSD<T>)obj);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Normalize
        /// </summary>
        public override MARC.Everest.Interfaces.IGraphable Normalize()
        {
            QSD<T> retVal = this.Clone() as QSD<T>;
            if (retVal.Minuend is SXPR<T>)
                retVal.Minuend = (retVal.Minuend as SXPR<T>).TranslateToQSET();
            if (retVal.Minuend is SXPR<T>)
                retVal.Minuend = (retVal.Minuend as SXPR<T>).TranslateToQSET();
            return retVal;
        }
    }
}
