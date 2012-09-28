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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Attributes;
using System.Collections;
using MARC.Everest.Interfaces;
using System.Xml.Serialization;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{


    /// <summary>
    /// A collection of items that are ordered based on their history
    /// </summary>
    [Serializable]
    [Structure(Name = "HIST", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("HIST", Namespace = "urn:hl7-org:v3")]
    public class HIST<T> : LIST<T>
    {

        /// <summary>
        /// Create a new instance of a list
        /// </summary>
        public HIST() : base() { }

        /// <summary>
        /// Create a new instance of the list with a specified capacity
        /// </summary>
        /// <param name="capacity">The initial capacity of the LIST</param>
        public HIST(int capacity)
            : base(capacity) { }

        /// <summary>
        /// Create a new list of items using values from another collection
        /// </summary>
        /// <param name="collection">The collection of items to add. Note: If an item does not match type <typeparamref name="T"/> it won't be added</param>
        public HIST(IEnumerable collection)
            : base(collection){}

        /// <summary>
        /// Create a new list of items using values from another collection
        /// </summary>
        /// <param name="collection">The collection to add from</param>
        public HIST(IEnumerable<T> collection)
            : base(collection) { }


        /// <summary>
        /// Create a new list of items using the specified items
        /// </summary>
        /// <param name="item">The collection to add from</param>
        public static HIST<T> CreateHIST(params T[] item)
        {
            return new HIST<T>(item);
        }

        /// <summary>
        /// Create a hist of T from Array of graphable (shallow copy)
        /// </summary>
        /// <remarks>Usually, this is called when formatting</remarks>
        /// <param name="o">The array of objects to add</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        internal static HIST<T> Parse(LIST<IGraphable> o)
        {
            HIST<T> retVal = new HIST<T>(o);
            retVal.NullFlavor = o.NullFlavor;
            retVal.ControlActExt = o.ControlActExt;
            retVal.ControlActRoot = o.ControlActRoot;
            retVal.Flavor = o.Flavor;
            retVal.UpdateMode = o.UpdateMode;
            retVal.ValidTimeHigh = o.ValidTimeHigh;
            retVal.ValidTimeLow = o.ValidTimeLow;
            return retVal;
        }
    }

    /// <summary>
    /// A collection that contains other discrete (but not necessarily distinct) values in a defined sequence.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST"), Serializable]
    [Structure(Name = "LIST", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("LIST", Namespace = "urn:hl7-org:v3")]
    public class LIST<T> : COLL<T>, ISequence<T>, IEquatable<LIST<T>>
    {
        /// <summary>
        /// Create a new instance of a list
        /// </summary>
        public LIST() : base() { }

        /// <summary>
        /// Create a new instance of the list with a specified capacity
        /// </summary>
        /// <param name="capacity">The initial capacity of the LIST</param>
        public LIST(int capacity)
            : base()
        {
            items = new List<T>(capacity);
        }

        /// <summary>
        /// Create a new list of items using values from another collection
        /// </summary>
        /// <param name="collection">The collection of items to add. Note: If an item does not match type <typeparamref name="T"/> it won't be added</param>
        public LIST(IEnumerable collection)
            : base()
        {
            items = new List<T>();
            // Add items
            foreach (object o in collection ?? new ArrayList())
                if (o is T)
                    items.Add((T)o);
                else
                {
                    T converted = (T)Util.FromWireFormat(o, typeof(T));
                    if(converted != null) items.Add(converted);
                }

        }

        /// <summary>
        /// Create a new list of items using values from another collection
        /// </summary>
        /// <param name="collection">The collection to add from</param>
        public LIST(IEnumerable<T> collection)
            : base()
        {
            items = new List<T>(collection);
        }

        /// <summary>
        /// Creates a new LIST containing the specified <paramref name="items"/>
        /// </summary>
        public static LIST<T> CreateList(params T[] items)
        {
            return new LIST<T>(items);
        }

        /// <summary>
        /// Find all items that match the given predicate
        /// </summary>
        public T Find(Predicate<T> match)
        {
            return items.Find(match);
        }

        /// <summary>
        /// Find all occurences of <paramref name="match"/>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<T> FindAll(Predicate<T> match)
        {
            return items.FindAll(match);
        }

        /// <summary>
        /// Cast any IEnumerable to this list
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static implicit operator LIST<T>(List<T> o)
        {
            return new LIST<T>(o);
        }

        // Items
        private List<T> items = new List<T>();

        /// <summary>
        /// Return a portion of the list
        /// </summary>
        /// <param name="Start">The first item to include</param>
        /// <param name="End">The last item to include</param>
        /// <example>
        /// <code title="Using sub-sequence" lang="cs">
        /// <![CDATA[
        /// // Create list of identifiers (Person, Place, or Thing)
        /// // The first Item in the list has a zero Index
        /// LIST<II> set1 = new LIST<II>(new II[] 
        /// { 
        ///     new II("1.1.1.1","1"), // set1[0] 
        ///     new II("1.1.1.1","2"), 
        ///     new II("1.1.1.1","3"), 
        ///     new II("1.1.1.1","3"), 
        ///     new II("1.1.1.1","4") 
        /// }); 
        /// LIST<II> set2 = (LIST<II>)set1.SubSequence(1, 2); // contains 1 items 
        /// set2 = (LIST<II>)set1.SubSequence(1); // set2 will hold the last four identifiers in the list       
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="T:System.IndexOutOfRangeException">Thrown when either <paramref name="start"/> or <paramref name="end"/> are outside the bounds of the array</exception>
        /// <exception cref="T:System.ArgumentException">Thrown when <paramref name="start"/> is greater than <paramref name="end"/></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Start"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "End"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SubSequence")]
        public ISequence<T> SubSequence(int start, int end)
        {
            if (start >= Count)
                throw new System.IndexOutOfRangeException(String.Format("Start position {0} is outside bounds of the LIST", start));
            else if (end >= Count)
                throw new System.IndexOutOfRangeException(String.Format("End position {0} is outside the bounds of the LIST", end));
            else if (start > end)
                throw new ArgumentException("Start parameter must be less than the End parameter");

            LIST<T> retVal = new LIST<T>();
            for(int i = start; i <= end; i++)
                retVal.Add(items[i]);
            return retVal;
        }

        /// <summary>
        /// Return a portion of the list starting from the specified item
        /// </summary>
        /// <param name="start">The start index</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Start"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SubSequence")]
        public ISequence<T> SubSequence(int start)
        {
            return SubSequence(start, this.Count - 1);
        }

        /// <summary>
        /// Determine if the collection is empty
        /// </summary>
        public override bool IsEmpty
        {
            get { return items.Count == 0; }
        }

        /// <summary>
        /// Get the index of an item
        /// </summary>
        public override int IndexOf(T item)
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].Equals(item))
                    return i;
            return -1;
        }

        /// <summary>
        /// Insert an item at a specified index
        /// </summary>
        /// <param name="index">The index to add <paramref name="item"/> at</param>
        /// <param name="item">The item to add</param>
        public override void Insert(int index, T item)
        {
            items.Insert(index, item);
        }

        /// <summary>
        /// Remove a specific item
        /// </summary>
        /// <param name="index">The index to remove at</param>
        public override void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        /// <summary>
        /// Indexer properter
        /// </summary>
        /// <param name="index">The index of the item to retrieve</param>
        public override T this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                items[index] = value;
            }
        }

        /// <summary>
        /// Add a range of values to this list
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "otherlist")]
        public void AddRange(LIST<T> otherlist)
        {
            Items.AddRange(otherlist);
        }

        /// <summary>
        /// Add an item to the set
        /// </summary>
        public override void Add(T item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Clear this set
        /// </summary>
        public override void Clear()
        {
            items.Clear();
        }

        /// <summary>
        /// Determine if this set contains another item
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns></returns>
        public override bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">The destination array</param>
        /// <param name="arrayIndex">The array index</param>
        public override void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Return the number of items in the set
        /// </summary>
        public override int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// Return if the set is read only
        /// </summary>
        public override bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove an item from the set
        /// </summary>
        /// <param name="item">The item to remove</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override bool Remove(T item)
        {
            try
            {
                RemoveAt(IndexOf(item)); // Try to reuse the remove at
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get this set's enumerator
        /// </summary>
        public override IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }
    
        /// <summary>
        /// Items contained in this set
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), Property(Name = "item", Conformance = PropertyAttribute.AttributeConformanceType.Optional,
            PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        [Marker(MarkerType = MarkerAttribute.MarkerAttributeType.Data)]
        public override List<T> Items
        {
	        get { return items; }
            set { items = value; }
        }

        #region Explicit Members

        T ISequence<T>.First
        {
            get { return this.First; }
        }

        T ISequence<T>.Last
        {
            get { return this.Last; }
        }

        ISequence<T> ISequence<T>.SubSequence(int Start, int End)
        {
            return this.SubSequence(Start, End);
        }

        ISequence<T> ISequence<T>.SubSequence(int Start)
        {
            return this.SubSequence(Start);
        }

        bool IColl.IsEmpty
        {
            get { return this.IsEmpty; }
        }

        List<T> IColl<T>.Items
        {
            get { return this.Items; }
        }

        #endregion


        /// <summary>
        /// Determine if the two items are semantically equal
        /// </summary>
        /// <remarks>Two instances of LIST are semantically equal when they contain the same items in the same sequence</remarks>
        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;

            var otherList = other as LIST<T>;
            if (otherList.IsEmpty && this.IsEmpty)
                return true;
            else
            {
                bool isEqual = this.Count == otherList.Count;
                for (int i = 0; i < this.Count && isEqual; i++)
                    isEqual &= (bool)(otherList[i] as IAny).SemanticEquals(this[i] as IAny);
                return isEqual;
            }
        }

        /// <summary>
        /// Create a set of T from Array o (shallow copy)
        /// </summary>
        /// <remarks>Usually called from the formatter</remarks>
        /// <param name="o">The array of objects to add</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        internal static LIST<T> Parse(LIST<IGraphable> o)
        {
            LIST<T> retVal = new LIST<T>(o);
            retVal.NullFlavor = o.NullFlavor;
            retVal.ControlActExt = o.ControlActExt;
            retVal.ControlActRoot = o.ControlActRoot;
            retVal.Flavor = o.Flavor;
            retVal.UpdateMode = o.UpdateMode;
            retVal.ValidTimeHigh = o.ValidTimeHigh;
            retVal.ValidTimeLow = o.ValidTimeLow;
            return retVal;
        }


        /// <summary>
        /// Get the first item from the collection
        /// </summary>
        public T First
        {
            get { return Items.Count > 0 ? Items[0] : default(T); }
        }

        /// <summary>
        /// Get the last item from the collection
        /// </summary>
        public T Last
        {
            get { return Items.Count > 0 ? Items[Items.Count - 1] : default(T); }
        }

        #region IEquatable<LIST<T>> Members

        /// <summary>
        /// Determine if this LIST of T is the same as another LIST of T
        /// </summary>
        /// <remarks>
        /// This equals method differs form the equals method in <see cref="T:MARC.Everest.DataTypes.BAG{T}"/> as it ensures that not only
        /// every element within the <paramref name="other"/> list appears in the current list, but it appears in the same order. So
        /// <para>
        ///     { 2, 3, 0, 1 }
        /// </para>
        /// <para>
        /// is equal to { 2, 3, 0, 1 }
        /// </para>
        /// <para>
        /// but not to { 2, 0, 1, 3 }
        /// </para>
        /// <para>Also, this method ensures that both lists are the same size</para>
        /// </remarks>
        public bool Equals(LIST<T> other)
        {
            if (other != null)
            {
                bool result = base.Equals((ANY)other) &&
                    other.Count == this.Count;
                for (int i = 0; i < (result ? this.Items.Count : 0); i++)
                    result &= this.Items[i] != null ? this.Items[i].Equals(other.Items[i]) : other.Items[i] == null;
                return result;
            }
            return false;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is LIST<T>)
                return Equals(obj as LIST<T>);
            return base.Equals(obj);
        }

        #endregion

        #region IColl<T> Members

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

        #endregion
    }
}
