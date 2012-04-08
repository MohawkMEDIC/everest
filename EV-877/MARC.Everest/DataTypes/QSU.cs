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
using System.ComponentModel;
using MARC.Everest.Interfaces;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Represents a <see cref="T:QSET{T}"/> that has been specialized as a union of other sets.
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
    [Structure(Name = "QSU", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("QSU", Namespace = "urn:hl7-org:v3")]
    public class QSU<T> : QSET<T>, ICollection<ISetComponent<T>>, IEquatable<QSU<T>>, IListContainer
        where T : IAny
    {

        /// <summary>
        /// Creates a new instance of the QSET union class
        /// </summary>
        public QSU() : base() { this.Terms = new List<ISetComponent<T>>(); }

        /// <summary>
        /// Creates a new instance of the QSET union class containing the specified sets
        /// </summary>
        /// <param name="terms">The terms contained within the union</param>
        public QSU(params ISetComponent<T>[] terms)
        {
            this.Terms = new List<ISetComponent<T>>(terms);
        }

        /// <summary>
        /// Gets or sets the terms that make up the union of this QSET
        /// </summary>
        [Property(Name = "term", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        [XmlElement("term")]
        public List<ISetComponent<T>> Terms { get; set; }

        /// <summary>
        /// Validate that the QSET is valid. A QSET is valid when it contains at least two sets 
        /// in the <see cref="P:Terms"/> property and contains no-null terms.
        /// </summary>
        public override bool Validate()
        {
            bool isValid = (this.NullFlavor != null) ^ (this.Terms.Count > 1);
            foreach (var qs in Terms ?? new List<ISetComponent<T>>())
                isValid &= qs != null && !qs.IsNull;
            return isValid;
        }

        #region IEquatable<QSU<T>> Members

        /// <summary>
        /// Determine equality between two QSS instances
        /// </summary>
        public bool Equals(QSU<T> other)
        {
            bool result = base.Equals(other);
            result &= this.Terms.Count == other.Terms.Count;
            for (int i = 0; i < other.Terms.Count && result; i++)
                result &= this.Terms[i].Equals(other.Terms[i]);
            return result;
        }

        /// <summary>
        /// Determine equality between two items
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is QSU<T>)
                return Equals((QSU<T>)obj);
            return base.Equals(obj);
        }

        #endregion
        /// <summary>
        /// Get set operator
        /// </summary>
        protected override SetOperator GetEquivalentSetOperator()
        {
            return SetOperator.Inclusive;
        }


        #region ICollection<QSET<T>> Members

        /// <summary>
        /// Add an expression to the term set
        /// </summary>
        public void Add(ISetComponent<T> item)
        {
            this.Terms.Add(item);
        }

        /// <summary>
        /// Clear the terms
        /// </summary>
        public void Clear()
        {
            this.Terms.Clear();
        }

        /// <summary>
        /// Determine if the terms contain the item
        /// </summary>
        public bool Contains(ISetComponent<T> item)
        {
            return this.Terms.Contains(item);
        }

        /// <summary>
        /// Copy the terms to an array
        /// </summary>
        public void CopyTo(ISetComponent<T>[] array, int arrayIndex)
        {
            this.Terms.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Count the items in the terms
        /// </summary>
        public int Count
        {
            get { return this.Terms.Count; }
        }

        /// <summary>
        /// True if the terms are readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove a term from the collection
        /// </summary>
        public bool Remove(ISetComponent<T> item)
        {
            return this.Terms.Remove(item);
        }

        #endregion

        #region IEnumerable<QSET<T>> Members

        /// <summary>
        /// Get the enumerator for the terms
        /// </summary>
        public IEnumerator<ISetComponent<T>> GetEnumerator()
        {
            return this.Terms.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Get the enumerator for the terms
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IListContainer Members

        /// <summary>
        /// IListContainer ContainedList implementation for formatters
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        LIST<MARC.Everest.Interfaces.IGraphable> IListContainer.ContainedList
        {
            get
            {
                return new LIST<IGraphable>(this.Terms);
            }
            set
            {
                this.Terms = new List<ISetComponent<T>>(LIST<ISetComponent<T>>.Parse(value));
            }
        }

        #endregion

        /// <summary>
        /// Normalizes the QSI to include only QS* content
        /// </summary>
        public override IGraphable Normalize()
        {
            var retVal = this.Clone() as QSU<T>;
            for (int i = 0; i < retVal.Terms.Count; i++)
                if (retVal.Terms[i] is SXPR<T>)
                    retVal.Terms[i] = (retVal.Terms[i] as SXPR<T>).TranslateToQSET();
            return retVal;
        }
    }
}
