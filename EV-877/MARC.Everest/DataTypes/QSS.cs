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
using MARC.Everest.Interfaces;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Represents a <see cref="T:QSET{T}"/> that has been specialized as containing a set.
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
    [Structure(Name = "QSS", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("QSS", Namespace = "urn:hl7-org:v3")]
    public class QSS<T> : QSET<T>, ICollection<T>, IEquatable<QSS<T>>, IListContainer
        where T : IAny
    {

        /// <summary>
        /// Creates a new instance of the QSET set class
        /// </summary>
        public QSS() : base() {
            this.Terms = new List<T>();
        }

        /// <summary>
        /// Creates a new instance of the QSET set class containing the specified <paramref name="collection"/>
        /// </summary>
        public QSS(List<T> terms)
        {
            this.Terms = terms;
        }

        /// <summary>
        /// Creates a new instance of the QSET set class containing the specified <paramref name="collection"/>
        /// </summary>
        public QSS(params T[] terms)
        {
            this.Terms = new List<T>(terms);
        }

        /// <summary>
        /// Equivalent set operator
        /// </summary>
        protected override SetOperator GetEquivalentSetOperator()
        {
            return SetOperator.Inclusive;
        }

        /// <summary>
        /// Gets or sets the terms that make up the union of this QSET
        /// </summary>
        [Property(Name = "term", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        [XmlElement("term")]
        public List<T> Terms { get; set; }

        /// <summary>
        /// Validate that the QSET is valid. A QSET is valid when it contains at least two sets 
        /// in the <see cref="P:Terms"/> property and contains no-null terms.
        /// </summary>
        public override bool Validate()
        {
            bool isValid = (this.NullFlavor != null) ^ (this.Terms != null && this.Terms.Count > 0);
            foreach (var qs in Terms ?? new List<T>())
                isValid &= qs != null && !qs.IsNull;
            return isValid;
        }


        #region IEnumerable<T> Members

        /// <summary>
        /// Get the enumerator for this QSET SET
        /// </summary>
        /// <exception cref="T:System.NullReferenceException">When the value of <see cref="P:Terms"/> has not been set</exception>
        public IEnumerator<T> GetEnumerator()
        {
            return this.Terms.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Get the non-generic enumerator
        /// </summary>
        /// <exception cref="T:System.NullReferenceException">When the value of <see cref="P:Terms"/> has not been set</exception>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region ICollection<T> Members

        /// <summary>
        /// Add an expression to the term set
        /// </summary>
        public void Add(T item)
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
        public bool Contains(T item)
        {
            return this.Terms.Contains(item);
        }

        /// <summary>
        /// Copy the terms to an array
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex)
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
        public bool Remove(T item)
        {
            return this.Terms.Remove(item);
        }

        #endregion

        #region IEquatable<QSS<T>> Members

        /// <summary>
        /// Determine equality between two QSS instances
        /// </summary>
        public bool Equals(QSS<T> other)
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
            if (obj is QSS<T>)
                return Equals((QSS<T>)obj);
            return base.Equals(obj);
        }
        #endregion

        #region IListContainer Members

        /// <summary>
        /// Gets or sets the contents of the list contained by the object
        /// </summary>
        LIST<MARC.Everest.Interfaces.IGraphable> IListContainer.ContainedList
        {
            get
            {
                return new LIST<IGraphable>(this.Terms);
            }
            set
            {
                this.Terms = new List<T>(LIST<T>.Parse(value));
            }
        }

        #endregion

        /// <summary>
        /// No normalization needed
        /// </summary>
        public override IGraphable Normalize()
        {
            return this.Clone() as IGraphable;   
        }
    }
}
