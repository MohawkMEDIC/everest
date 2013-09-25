/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * Date: 10-05-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Attributes;
using System.Xml.Serialization;
using System.ComponentModel;
using MARC.Everest.Interfaces;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Represents a genericized QSET which contains a collection of terms
    /// </summary>
    public abstract class QSC<T> : QSET<T>, ICollection<ISetComponent<T>>, IEquatable<QSC<T>>, IListContainer
        where T : IAny
    {
        
        /// <summary>
        /// Creates a new instance of the QSET intersection class
        /// </summary>
        internal QSC() : base() { this.Terms = new List<ISetComponent<T>>(); }

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
            bool isValid = (this.NullFlavor != null && (this.Terms == null || this.Terms.Count == 0)) ^ (this.NullFlavor == null && this.Terms.Count > 1);            foreach (var qs in Terms ?? new List<ISetComponent<T>>())
                isValid &= qs != null && !qs.IsNull;
            return isValid;
        }


        #region IEquatable<QSI<T>> Members

        /// <summary>
        /// Determine equality between two QSS instances
        /// </summary>
        public bool Equals(QSC<T> other)
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
            if (obj is QSC<T>)
                return Equals((QSC<T>)obj);
            return base.Equals(obj);
        }
        #endregion

        #region ICollection<ISetComponent<T>> Members

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

        #region IEnumerable<ISetComponent<T>> Members

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
        /// Contained list implementation for the IListContainer class used by 
        /// formatters
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        LIST<IGraphable> IListContainer.ContainedList
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

        
    }
}
