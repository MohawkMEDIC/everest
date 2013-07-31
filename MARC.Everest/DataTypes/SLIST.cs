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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Attributes;
using System.Collections;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// A sequence of sampled values scaled and translated from a 
    /// list of integer values. Used to specify sampled biosignals
    /// </summary>
    /// <remarks>
    /// <para>
    /// The type of <see cref="P:Scale"/> is dictated by <typeparamref name="T"/>. For example, when
    /// a SLIST&lt;PQ> is specified, the <see cref="P:Scale"/> should be an instance of PQ.
    /// </para>
    /// <para>
    /// SLIST was introduced in DataTypes R2 and should not be used when formatting instances using 
    /// DataTypes R1 formatters.
    /// </para>
    /// </remarks>
    [Structure(Name = "SLIST", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(PQ))]
    public class SLIST<T> : ANY, ISequence<INT>, IEquatable<SLIST<T>>, ISampledList
        where T : IQuantity, new()
    {

        /// <summary>
        /// Digits
        /// </summary>
        private LIST<INT> m_digits = new LIST<INT>();

        /// <summary>
        /// Creates a new instance of SLIST
        /// </summary>
        public SLIST()
        {
        }
        /// <summary>
        /// Creates a new instance of SLIST with the specified <paramref name="origin"/> and
        /// <paramref name="scale"/>
        /// </summary>
        public SLIST(T origin, IQuantity scale)
        {
            this.Origin = origin;
            this.Scale = scale;
        }
        /// <summary>
        /// Creates a new instance of SLIST with the <paramref name="origin"/>, <paramref name="scale"/>
        /// and <paramref name="digits"/>
        /// </summary>
        public SLIST(T origin, IQuantity scale, IEnumerable<INT> items) : this(origin, scale)
        {
            this.m_digits = new LIST<INT>(items);
        }

        /// <summary>
        /// Creates an instance of the SLIST with the specified <paramref name="items"/>
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static SLIST<T> CreateSLIST(T origin, IQuantity scale, params INT[] items)
        {
            return new SLIST<T>(origin, scale, items);
        }

        /// <summary>
        /// The origin of the first reading in the list. The physical
        /// quantity that a zero-digit in the sequence would represent
        /// </summary>
        [Property(Name = "origin", Conformance = PropertyAttribute.AttributeConformanceType.Required, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public T Origin { get; set; }

        /// <summary>
        /// Gets or sets the a ratio-scale quantity that is factored out of the digit
        /// sequence
        /// </summary>
        [Property(Name = "scale", Conformance = PropertyAttribute.AttributeConformanceType.Required, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public IQuantity Scale { get; set; }

        #region IEnumerable<INT> Members

        /// <summary>
        /// Get the enumerator from the digits
        /// </summary>
        public IEnumerator<INT> GetEnumerator()
        {
            if (this.m_digits != null)
                return this.m_digits.GetEnumerator();
            return null;
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Get the enumerator from the digits
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region ISequence<INT> Members

        /// <summary>
        /// Gets the first item in the list
        /// </summary>
        public INT First
        {
            get { return this.m_digits.First; }
        }

        /// <summary>
        /// Gets the last item in the list
        /// </summary>
        public INT Last
        {
            get { return this.m_digits.Last; }
        }

        /// <summary>
        /// Returns a sub-sequence from the <see cref="P:Digits"/>
        /// property
        /// </summary>
        /// <param name="start">The first item to retrieve</param>
        /// <param name="end">The last item in the list to retrieve</param>
        /// <returns>A new instance of <see cref="T:LIST"/> containing the results</returns>
        public ISequence<INT> SubSequence(int start, int end)
        {
            return this.m_digits.SubSequence(start, end);
        }

        /// <summary>
        /// Returns a sub-sequence from the <see cref="P:Digits"/>
        /// property starting at <paramref name="start"/> and 
        /// ending with the length of the list
        /// </summary>
        /// <param name="start"></param>
        /// <returns>A new instance of <see cref="T:LIST"/> containing the results</returns>
        public ISequence<INT> SubSequence(int start)
        {
            return this.m_digits.SubSequence(start);
        }

        #endregion

        #region IColl<INT> Members

        /// <summary>
        /// Gets a list of all items in the SLIST
        /// </summary>
        [Property(Name = "digit", Conformance = PropertyAttribute.AttributeConformanceType.Required, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        public List<INT> Items
        {
            get { return this.m_digits.Items; }
        }

        /// <summary>
        /// Returns a new instance of <see cref="T:BL"/> indicating if
        /// this SLIST contains all the items from <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other collection to detect inclusion</param>
        public BL IncludesAll(IColl<INT> other)
        {
            return this.m_digits.IncludesAll(other);
        }

        /// <summary>
        /// Returns  a new instance of <see cref="T:BL"/> indicating whether
        /// the SLIST excludes all the items from <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other collection to detect exclusion</param>
        public BL ExcludesAll(IColl<INT> other)
        {
            return this.m_digits.ExcludesAll(other);
        }

        #endregion

        #region IList<INT> Members

        /// <summary>
        /// Return the index of <paramref name="item"/> within this SLIST
        /// </summary>
        /// <param name="item">The item for which a search should be conducted</param>
        public int IndexOf(INT item)
        {
            return this.m_digits.IndexOf(item);
        }

        /// <summary>
        /// Inserts <paramref name="item"/> at the specified <paramref name="index"/>
        /// </summary>
        public void Insert(int index, INT item)
        {
            this.m_digits.Insert(index, item);
        }

        /// <summary>
        /// Remove the object at <paramref name="index"/>
        /// </summary>
        public void RemoveAt(int index)
        {
            this.m_digits.RemoveAt(index);
        }

        /// <summary>
        /// Indexer property for the SLIST
        /// </summary>
        public INT this[int index]
        {
            get
            {
                return this.m_digits[index];
            }
            set
            {
                this.m_digits[index] = value;
            }
        }

        #endregion

        #region ICollection<INT> Members

        /// <summary>
        /// Adds <paramref name="item"/> to the SLIST
        /// </summary>
        public void Add(INT item)
        {
            this.m_digits.Add(item);
        }

        /// <summary>
        /// Clears the current instance of SLIST of all readings
        /// </summary>
        public void Clear()
        {
            this.m_digits.Clear();
        }

        /// <summary>
        /// Indicates whether this instance of SLIST contains <paramref name="item"/>
        /// </summary>
        public bool Contains(INT item)
        {
            return this.m_digits.Contains(item);
        }

        /// <summary>
        /// Copy the contents of this SLIST into an array of <see cref="T:INT"/>
        /// </summary>
        /// <param name="array">The target array of the copy operation</param>
        /// <param name="arrayIndex">The index to start the copy operation</param>
        public void CopyTo(INT[] array, int arrayIndex)
        {
            this.m_digits.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of readings in the SLIST
        /// </summary>
        public int Count
        {
            get { return this.m_digits.Count; }
        }

        /// <summary>
        /// Indicates whether this SLIST is read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes <paramref name="item"/> from the SLIST instance
        /// </summary>
        public bool Remove(INT item)
        {
            return this.m_digits.Remove(item);
        }

        #endregion

        #region IColl Members

        /// <summary>
        /// Returns true if the SLIST is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return this.m_digits.IsEmpty; }
        }

        #endregion

        #region IEquatable<SLIST<T>> Members

        /// <summary>
        /// Determine equality
        /// </summary>
        public bool Equals(SLIST<T> other)
        {
            bool result = base.Equals(other);
            result &= other.Origin == null ? this.Origin == null : other.Origin.Equals(this.Origin);
            result &= other.Scale == null ? this.Scale == null : other.Scale.Equals(this.Scale);
            result &= this.m_digits == null ? other.m_digits == null : this.m_digits.Equals(other.m_digits);
            return result;
        }

        /// <summary>
        /// Determine equality
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is SLIST<T>)
                return Equals((SLIST<T>)obj);
            return base.Equals(obj);
        }

        #endregion

        #region ISampledList Members

        /// <summary>
        /// Gets or sets the origin of the slist
        /// </summary>
        IQuantity ISampledList.Origin
        {
            get
            {
                return this.Origin;
            }
            set
            {
                this.Origin = (T)value;
            }
        }
     
        #endregion

      
    }
}
