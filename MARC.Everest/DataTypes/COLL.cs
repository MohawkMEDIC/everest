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
 * Date: 09-13-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// An abstract type intended to collect common attributes related to collections
    /// </summary>
    [Structure(Name= "COLL", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [Serializable]
    public abstract class COLL<T> : ANY, IColl<T>, IEquatable<COLL<T>>
    {
        #region IColl<T> Members

        /// <summary>
        /// Gets the contents of the collection
        /// </summary>
        public abstract List<T> Items { get; set;  }

        /// <summary>
        /// Returns true if <paramref name="other"/> contains all the items 
        /// in this list
        /// </summary>
        public BL IncludesAll(IColl<T> other)
        {
            bool includesAll = true;
            foreach (var item in this)
                includesAll &= other.Contains(item);
            return includesAll;
        }

        /// <summary>
        /// Returns true if <paramref name="other"/> does not contain any of the 
        /// items in this list
        /// </summary>
        public BL ExcludesAll(IColl<T> other)
        {
            bool includesOne = false;
            foreach (var item in this)
                includesOne |= other.Contains(item);
            return !includesOne;
        }


        /// <summary>
        /// Validate the collection. 
        /// </summary>
        /// <remarks>
        /// A collection is considered valid when <see cref="P:NullFlavor"/> is set xor
        /// the collection is not empty.
        /// </remarks>
        public override bool Validate()
        {
            return (this.NullFlavor != null) ^ (!this.IsEmpty);
        }


        /// <summary>
        /// Validate the data type returning the validation errors that occur
        /// </summary>
        public override IEnumerable<IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());
            if (this.NullFlavor != null && this.Items.Count > 0)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "COLL", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (this.NullFlavor == null && this.Items.Count == 0)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "COLL", ValidationMessages.MSG_NULLFLAVOR_MISSING, null));
            return retVal;
        }
        #endregion

        #region IList<T> Members

        /// <summary>
        /// Gets the index of the specified <paramref name="item"/> 
        /// within the collection.
        /// </summary>
        public abstract int IndexOf(T item);

        /// <summary>
        /// Inserts <paramref name="item"/> at the specified 
        /// <paramref name="index"/>
        /// </summary>
        public abstract void Insert(int index, T item);

        /// <summary>
        /// Removes the item at the specified <paramref name="index"/>
        /// </summary>
        public abstract void RemoveAt(int index);

        /// <summary>
        /// Indexer property for the collection
        /// </summary>
        public abstract T this[int index]
        {
            get;
            set;
        }

        #endregion

        #region ICollection<T> Members

        /// <summary>
        /// Adds <paramref name="item"/> to the collection
        /// </summary>
        public abstract void Add(T item);

        /// <summary>
        /// Clears the collection
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Returns true if the current collection contains <paramref name="item"/>
        /// </summary>
        public abstract bool Contains(T item);

        /// <summary>
        /// Copies the contents of this collection to <paramref name="array"/>
        /// </summary>
        public abstract void CopyTo(T[] array, int arrayIndex);

        /// <summary>
        /// Gets the number of items that are in the collection
        /// </summary>
        public abstract int Count
        {
            get;
        }
        
        /// <summary>
        /// Gets a value indicating if the collection is in a read only state
        /// </summary>
        public abstract bool IsReadOnly
        {
            get;
        }

        /// <summary>
        /// Removes the specified <paramref name="item"/> from the collection
        /// </summary>
        public abstract bool Remove(T item);

        #endregion

        #region IEnumerable<T> Members
        /// <summary>
        /// Gets the enumerator for the collection
        /// </summary>
        public abstract IEnumerator<T> GetEnumerator();

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Get the enumerator for the collection
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IColl Members

        /// <summary>
        /// Returns a value that indicates if the item is empty
        /// </summary>
        public abstract bool IsEmpty { get; }

        #endregion

        #region IEquatable<COLL<T>> Members

        /// <summary>
        /// Determine if this SET of T equals another SET of T
        /// </summary>
        /// <remarks>
        /// This equality method differs from the <see cref="T:MARC.Everest.DataTypes.BAG{T}"/> and <see cref="T:MARC.Everest.DataTypes.LIST{T}"/>
        /// in that it not only compares the contents of each set to ensure that all data is present in both sets, it uses the comparator of 
        /// this set to determine if the "item" is the same (rather than the equality method)
        /// </remarks>
        public bool Equals(COLL<T> other)
        {
            if (other != null)
            {
                bool result = base.Equals((ANY)other) &&
                    other.Count == this.Count;
                for (int i = 0; i < (result ? this.Items.Count : 0); i++)
                    result &= Items[i].Equals(other.Items[i]);
                return result;
            }
            return false;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is SET<T>)
                return Equals(obj as SET<T>);
            return base.Equals(obj);
        }

        #endregion
    }
}
