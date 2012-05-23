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
using System.Xml.Serialization;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;
using System.Collections;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// An unordered list of collection values where each value can be contained more than once in the collection. 
    /// </summary>
    /// <remarks>
    /// <para>
    ///     This class differs from a <see cref="T:MARC.Everest.DataTypes.LIST()"/> in that no meaning can be inferred from
    ///     the order in which items appear in the BAG.
    /// </para>
    /// <para>
    /// This class behaves like the built in .NET <see cref="T:System.Collections.Generic.List()"/> class
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BAG"), Serializable]
    [Structure(StructureType = StructureAttribute.StructureAttributeType.DataType, Name = "BAG")]
    [XmlType("BAG", Namespace = "urn:hl7-org:v3")]
    public class BAG<T> : COLL<T>, IBag<T>, IEquatable<BAG<T>>
    {

        /// <summary>
        /// Create a new instance of the BAG class
        /// </summary>
        /// 
        public BAG() : base() { }
        /// <summary>
        /// Create a new instance of the BAG class with the capacity specified
        /// </summary>
        /// <param name="capacity">The capacity</param>
        public BAG(int capacity)
        {
            this.items = new List<T>(capacity);
        }
        
        /// <summary>
        /// Create a new instance of the BAG class using the specified collection and comparator
        /// </summary>
        /// <example>
        /// <code title="Create a BAG object with an enumerable collection." lang="cs">
        /// <![CDATA[
        /// // This Bag contains an enumerable collection of Identifiers
        /// BAG<II> bag = new BAG<II>(new II[] {
        ///        new II("1.1.1.1", "1"),
        ///        new II(),
        ///        });
        /// // Inserts an Identifier at position one.       
        /// bag.Insert(1, new II("1.2.1.2", "1234")); 
        /// ]]>
        /// </code>
        /// </example>
        public BAG(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                this.Add(item);
        }

        /// <summary>
        /// Create a new bag of items using values from another collection
        /// </summary>
        /// <param name="collection">The collection of items to add. Note: If an item does not match type <typeparamref name="T"/> it won't be added</param>
        public BAG(IEnumerable collection)
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
        /// Create a new instance of the BAG class using the specified <paramref name="item"/>
        /// </summary>
        /// <param name="items">The items to add to the collection</param>
        public static BAG<T> CreateBAG(params T[] items)
        {
            return new BAG<T>(items);
        }

        // Items
        private List<T> items = new List<T>();

        /// <summary>
        /// Find item in the collection that matches <paramref name="match"/>
        /// </summary>
        /// <param name="match">The predicate that specifies the criteria of the object to </param>
        /// <returns>The found object</returns>
        public T Find(Predicate<T> match)
        {
            return items.Find(match);
        }

        /// <summary>
        /// Find all items in the collection that match <paramref name="match"/>
        /// </summary>
        /// <param name="match">The predicate that specifies the criteria to match</param>
        /// <returns>An enumerable collection of all found items</returns>
        public List<T> FindAll(Predicate<T> match)
        {
            return items.FindAll(match);
        }

        #region IColl<T> Members

        /// <summary>
        /// Determine if the bag is empty
        /// </summary>
        public override bool IsEmpty
        {
            get { return items.Count == 0; }
        }

        /// <summary>
        /// Get the items in the collection
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), Property(Name = "item", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public override List<T> Items
        {
            get { return items; }
            set { this.items = value; }
        }

        #endregion

        #region IList<T> Members

        /// <summary>
        /// Get the index of an item
        /// </summary>
        public override int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        /// <summary>
        /// Insert an item into the collection
        /// </summary>
        public override void Insert(int index, T item)
        {
            items.Insert(index, item);
        }

        /// <summary>
        /// Remove an item from the collection
        /// </summary>
        public override void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        /// <summary>
        /// Indexor
        /// </summary>
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

        #endregion

        #region ICollection<T> Members

        /// <summary>
        /// Add an item
        /// </summary>
        public override void Add(T item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Clear the items
        /// </summary>
        public override void Clear()
        {
            items.Clear();
        }

        /// <summary>
        /// Determine if the list contains an item
        /// </summary>
        public override bool Contains(T item)
        {
            return items.Contains(item);
        }

        /// <summary>
        /// Copy the contents of this array to an array
        /// </summary>
        public override void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get the number of items in the collection
        /// </summary>
        public override int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// Determine if the collection is read only
        /// </summary>
        public override bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove an item from the collection
        /// </summary>
        public override bool Remove(T item)
        {
            return items.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Get the enumerator
        /// </summary>
        /// <returns></returns>
        public override IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Validate the Bag
        /// </summary>
        public override bool Validate()
        {
            return (this.NullFlavor != null) ^ (Items.Count > 0);
        }


        /// <summary>
        /// Create a bag of T from Array o (shallow copy)
        /// </summary>
        /// <param name="o">The array of objects to add</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static explicit operator BAG<T>(LIST<T> o)
        {
            BAG<T> retVal = new BAG<T>();
            retVal.NullFlavor = o.NullFlavor;
            retVal.ControlActExt = o.ControlActExt;
            retVal.ControlActRoot = o.ControlActRoot;
            retVal.Flavor = o.Flavor;
            retVal.UpdateMode = o.UpdateMode;
            retVal.ValidTimeHigh = o.ValidTimeHigh;
            retVal.ValidTimeLow = o.ValidTimeLow;
            foreach (T item in o)
                retVal.Add(item);
            return retVal;
        }

        /// <summary>
        /// Create a set of T from Array o (shallow copy)
        /// </summary>
        /// <remarks>Usually called from the formatter</remarks>
        /// <param name="o">The array of objects to add</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        internal static BAG<T> Parse(LIST<IGraphable> o)
        {
            BAG<T> retVal = new BAG<T>(o);
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
        /// Determines if the current instance of BAG is semantically equal to <paramref name="other"/>
        /// </summary>
        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;

            var otherBAG = other as BAG<T>;
            if(otherBAG.IsEmpty && this.IsEmpty)
                return true;
            else
            {
                bool isEqual = this.Count == otherBAG.Count;
                foreach(ISemanticEquatable itm in this)
                    isEqual &= otherBAG.FindAll(o=> itm.SemanticEquals(o as IAny) == true).Count == this.FindAll(o=>itm.SemanticEquals(o as IAny) == true).Count;
                return isEqual;
            }

        }
        
        #region IEquatable<BAG<T>> Members

        /// <summary>
        /// Determine if this BAG of T equals another BAG of T
        /// </summary>
        /// <remarks>
        /// In this class, you will notice that we use the Find method for each item in this
        /// BAG within the other (ie: we search for an item matching each item in this bag to appear
        /// in the other). This is done because the BAG collection is unordered, so in theory
        /// the following BAGs are equal:
        /// <para>
        ///     { 0, 2, 3, 1 }
        /// </para>
        /// <para>
        /// is equal to { 0, 1, 2, 3 }
        /// </para>
        /// <para>
        /// However two BAGs are not considered equal of the count of items are different, for example:
        /// </para>
        /// <para>
        ///     { 0, 2, 3, 1 }
        /// </para>
        /// <para>
        /// is not equal to { 0, 1, 2, 3, 1 }
        /// </para>
        /// </remarks>
        public bool Equals(BAG<T> other)
        {
            if (other != null)
            {
                bool result = base.Equals((ANY)other);
                foreach (var itm in items)
                    result &= itm != null ? other.Find(o => o.Equals(itm)) != null : other.Find(o => o == null) != null;
                result &= other.Count == this.Count;
                return result;
            }
            return false;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is BAG<T>)
                return Equals(obj as BAG<T>);
            return base.Equals(obj);
        }

        #endregion
    }
}