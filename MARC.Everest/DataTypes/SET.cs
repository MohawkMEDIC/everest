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
using MARC.Everest.Exceptions;
using MARC.Everest.Attributes;
using MARC.Everest.Interfaces;
using System.Collections;
using MARC.Everest.Connectors;
using System.Xml.Serialization;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// SPQR is an initialism from a Latin phrase, Senatus Populusque 
    /// Romanus ("The Senate and People of Rome")
    /// </summary>
    /// <remarks>
    /// <para>
    /// SPQR refers to the government of the ancient Roman Republic, and 
    /// used as an official emblem of the modern day comune (municipality) 
    /// of Rome. It appears on coins, at the end of documents made public 
    /// by inscription in stone or metal, in dedications of monuments and 
    /// public works, and was emblazoned on the standards of the Roman 
    /// legions. The phrase appears many hundreds of times in Roman 
    /// political, legal and historical literature, including the speeches 
    /// of Marcus Tullius Cicero and the history of Titus Livius. Since 
    /// the meaning and the words never vary, except for the spelling and 
    /// inflection of populus in literature, Latin dictionaries classify 
    /// it as a formula.
    /// <img src="http://upload.wikimedia.org/wikipedia/commons/thumb/9/98/Roman_SPQR_banner.svg/150px-Roman_SPQR_banner.svg.png"/>
    /// </para>
    /// <para>From : http://en.wikipedia.org/wiki/SPQR</para>
    /// </remarks>
    [Serializable]
    [Obsolete("",false)]
    public class SPQR : SET<PQR>
    {
    }

    /// <summary>
    /// A collection that contains other distinct and discrete values in no particular order
    /// </summary>
    /// <example>
    /// <code title="Creating a new SET" lang="cs">
    /// <![CDATA[
    /// // Create sets using two different constructors
    ///    SET<II> set1 = new SET<II>(new II("1.1.1.1", "1234"), II.Comparator);
    ///    SET<II> set2 = new SET<II>(new II[]{new II("1.1.1.1", "12345"), 
    ///                                            new II("1.1.1.2", "23456")} , II.Comparator);
    ///    SET<II> set3 = set1.Union(set2); // set3 has two items
    /// ]]>
    /// </code>
    /// </example>
    /// <remarks>
    /// When creating a set, it is recommended that you use a comparator as the business rules surrounding a set are that
    /// not items can be duplicated. Since it is impossible to determine if an item will equal another without a comparator
    /// this rule cannot be enforced without this comparator parameter.
    /// <para>
    /// When creating a new SET without a comparator, the default comparator is used
    /// <code lang="cs" title="Default Comparator">
    /// delegate(T a, T b)
    /// {
    ///     if (a is ISemanticEquatable<T>)
    ///         return (a as ISemanticEquatable<T>).SemanticEquals(b) ? 0 : 1;
    ///     if (a != null && a.Equals(b) ||
    ///         b != null && b.Equals(a) ||
    ///         a == null && b == null)
    ///         return 0;
    ///     else
    ///         return 1;
    /// }
    /// 
    /// </code>
    /// 
    /// </para>
    /// <para>
    /// The option to use custom comparators has been made to support the following scenarios:
    /// <list type="bullet">
    ///     <item>Creating SET instances with types that do not implement IEquatable</item>
    ///     <item>Creating SET instances with types where only portions of the members are considered duplicate (ie: an II is considered the same if only the Root and Extension are the same, no matter what the rest of the 
    ///     instance's content is)</item>
    /// </list>
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SET"), Serializable]
    [Structure(Name = "SET", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("SET", Namespace = "urn:hl7-org:v3")]
    [TypeMap(Name = "DSET")]
    public class SET<T> : COLL<T>, ISet<T>, IColl<T> , IEquatable<SET<T>>
    {

        /// <summary>
        /// Default comparator
        /// </summary>
        public static Comparison<T> DefaultComparator = delegate(T a, T b)
        {
            ANY anyB = b as ANY;
            if (a is ISemanticEquatable && anyB != null)
                return (bool)(a as ISemanticEquatable).SemanticEquals(anyB) ? 0 : 1;
            else if (a != null && a.Equals(b) ||
                b != null && b.Equals(a) ||
                a == null && b == null)
                return 0;
            else
                return 1;
        };

        /// <summary>
        /// Create a new instance of the DSET class
        /// </summary>
        public SET() : base() 
        {
            this.Comparator = DefaultComparator;
        }

        /// <summary>
        /// Createa a new instance of the DSET class using <paramref name="comparator"/> as the 
        /// comparison function
        /// </summary>
        /// <param name="comparator">The comparator to use for determining an items entry into the set</param>
        public SET(Comparison<T> comparator) : base()
        {
            this.Comparator = comparator;
        }

        /// <summary>
        /// Create a new instance of the DSET class with the capacity specified
        /// </summary>
        /// <param name="capacity">The capacity</param>
        public SET(int capacity) : this()
        {
            this.items = new List<T>(capacity);
        }

        /// <summary>
        /// Create a new instance of the DSET class with the specified initial capacity and <paramref name="comparator"/>
        /// </summary>
        /// <param name="capacity">The initial capacity of the set</param>
        /// <param name="comparator">The comparator to use for determining an item's entry into the sset</param>
        public SET(int capacity, Comparison<T> comparator)
            : this(capacity)
        {
            this.Comparator = comparator;
        }

        /// <summary>
        /// Create a new instance of the DSET class using the specified collection and comparator
        /// </summary>
        /// <param name="collection">The initial collection</param>
        /// <param name="comparator">The comparator to use</param>
        public SET(IEnumerable<T> collection, Comparison<T> comparator)
            : this(comparator)
        {
            foreach (T item in collection)
                this.Add(item);
        }

        /// <summary>
        /// Create a new instance of the SET class using the specified first item
        /// </summary>
        /// <param name="firstItem">The first item in the collection</param>
        /// <param name="comparator">The comparator to use to determine if an item belongs to the set</param>
        public SET(T firstItem, Comparison<T> comparator) 
            : this(comparator)
        {
            this.Add(firstItem);
        }

        /// <summary>
        /// Create a new instance of the SET class using the specified items
        /// </summary>
        /// <param name="collection">A collection of items to add</param>
        /// <param name="comparator">The comparator to assign</param>
        public SET(IEnumerable collection, Comparison<T> comparator)
            : this(comparator)
        {
            items = new List<T>();
            // Add items
            foreach (object o in collection)
            {
                if (o is T)
                    items.Add((T)o);
                else
                {
                    T converted = (T)Util.FromWireFormat(o, typeof(T));
                    if (converted != null) items.Add(converted);
                }
            }
        }

        /// <summary>
        /// Create a set with one item
        /// </summary>
        public SET(T firstItem) : this(firstItem, SET<T>.DefaultComparator) { }

        /// <summary>
        /// Creates a new instance of the set collection with the default comparator
        /// </summary>
        public static SET<T> CreateSET(params T[] item) 
        {
            return new SET<T>(item);
        }

        public SET(IEnumerable collection)
            : this(collection, SET<T>.DefaultComparator)
        { }


        // Used for items in the set
        private List<T> items = new List<T>();

        /// <summary>
        /// Get or set the predicate that will determine an object's involvement into the set
        /// </summary>
        public Comparison<T> Comparator { get; set; }

        /// <summary>
        /// Determine if the set is empty
        /// </summary>
        public override bool IsEmpty
        {
            get { return items.Count == 0; }
        }

        /// <summary>
        /// Find all items that match the given predicate
        /// </summary>
        /// <param name="match">The predicate that dictates how matching should be performed</param>
        /// <example>
        /// <code lang="cs" title="Finding items in a set">
        /// <![CDATA[
        /// // Create set 
        /// SET<II> set1 = new SET<II>(new II[] 
        /// { 
        /// new II("1.1.1.1","1"),  
        /// new II("1.1.1.1","2"), 
        /// new II("1.1.1.1","3") 
        /// }, II.Comparator); 
        ///              
        /// // .NET 3.5 and Mono 
        /// II ii2 = set1.Find(ii => ii.Extension == "2" && ii.Root == "1.1.1.1"); 
        /// // .NET 2.0 
        /// ii2 = set1.Find(delegate(II ii) 
        /// { 
        /// return ii.Extension == "1" && ii.Root == "1.1.1.1"; 
        /// }); 
        /// ]]>
        /// </code>
        /// </example>
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
        /// Union this set with another set
        /// </summary>
        /// <param name="otherset">The set to union with</param>
        /// <returns>The new unioned set</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "otherset")]
        public SET<T> Union(SET<T> otherset)
        {
            SET<T> retVal = new SET<T>();
            foreach(T item in this)
                retVal.Add(item);
            foreach(T item in otherset)
                if(!retVal.Contains(item))
                    retVal.Add(item);
            return retVal;
        }

        /// <summary>
        /// Create a union of this set and another set
        /// </summary>
        public SET<T> Union(T element)
        {
            SET<T> retVal = new SET<T>();
            foreach(T item in this)
                retVal.Add(item);
            if(!retVal.Contains(element))
                retVal.Add(element);
            return retVal;
        }

        /// <summary>
        /// Return a new DSET with all contents of this DSET except the items in the other set
        /// </summary>
        /// <param name="otherset">The set of items to exclude</param>
        /// <example>
        /// Except function with two sets
        /// <code title="Except Function" lang="cs">
        /// <![CDATA[
        /// // Create sets 
        /// SET<II> set1 = new SET<II>(new II[] 
        /// { 
        /// new II("1.1.1.1","1"),  
        /// new II("1.1.1.1","2"), 
        /// new II("1.1.1.1","3") 
        /// }, II.Comparator);
        /// SET<II> set2 = new SET<II>(new II[]  
        /// { 
        /// new II("1.1.1.1", "3"), 
        /// new II("1.1.1.1", "4") 
        /// }, II.Comparator);
        /// SET<II> set3 = set1.Except(set2); // set3 has two items (1,2) 
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "otherset")]
        public SET<T> Except(SET<T> otherset)
        {
            SET<T> retVal = new SET<T>();
            foreach(T item in this)
                if(!otherset.Contains(item))
                    retVal.Add(item);
            return retVal;
        }

        /// <summary>
        /// Return a new DSET with all the contents of this DSET except the element specified
        /// </summary>
        public SET<T> Except(T element)
        {
            SET<T> retVal = new SET<T>();
            foreach(T item in this)
                if(Comparator(item, element) != 0)
                    retVal.Add(item);
            return retVal;        
        }

        /// <summary>
        /// Intersect this DSET and another DSET
        /// </summary>
        /// <param name="otherset">The other DSET to instersect with</param>
        /// <example>
        /// <code lang="cs" title="Intersecting two sets">
        /// <![CDATA[
        /// // Create sets 
        /// SET<II> set1 = new SET<II>(new II[] 
        /// { 
        ///         new II("1.1.1.1","1"),  
        ///         new II("1.1.1.1","2"), 
        ///         new II("1.1.1.1","3") 
        /// }, II.Comparator);
        /// SET<II> set2 = new SET<II>(new II[]  
        /// { 
        ///         new II("1.1.1.1", "3"), 
        ///         new II("1.1.1.1", "4") 
        /// }, II.Comparator);
        /// SET<II> set3 = (SET<II>)set1.Intersection(set2); // set3 has one item ("3") 
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "otherset")]
        public SET<T> Intersection(SET<T> otherset)
        {
            SET<T> retVal = new SET<T>(this.Comparator);
            foreach(T item in this)
                if(otherset.Contains(item))
                    retVal.Add(item);
            return retVal;
        }

        /// <summary>
        /// Get the index of an item
        /// </summary>
        /// <exception cref="T:System.NullReferenceException">When the comparator of this SET is null</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public override int IndexOf(T item)
        {
            if (Comparator == null)
                throw new NullReferenceException("Comparator must be set before performing set functions");

            for(int i = 0; i < items.Count; i++)
                if(Comparator(item, items[i]) == 0)
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
        /// Indexer property
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
        /// Add an item to the set
        /// </summary>
        /// <exception cref="T:MARC.Everest.Exceptions.DuplicateItemException">When <paramref name="item"/> is semantically equal to an item already contained within the set</exception>
        /// <exception cref="T:System.ArgumentNullException">When <paramref name="item"/> is null</exception>
        public override void Add(T item)
        {
            // Make sure the item is not in the collection
            if (item == null)
                throw new ArgumentNullException("item");
            if(this.Contains(item))
                throw new DuplicateItemException("Item already exists in the SET, items are only allowed to appear once in any given SET");
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
            catch(Exception)
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
        /// Get or set the items that make up this set
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), Property(Name = "item", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public override List<T> Items
        {
            get { return items; }
            set { items = value; }
        }

        #region IColl<T> Members

        bool IColl.IsEmpty
        {
            get { return Items.Count == 0; }
        }

        List<T> IColl<T>.Items
        {
            get { return Items; }
        }

        #endregion

        /// <summary>
        /// Validate set
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return (items.Count > 0) ^ (NullFlavor != null);
        }

        /// <summary>
        /// Determine if the two items are semantically equal
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;

            var otherList = other as SET<T>;
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
        /// <param name="o">The array of objects to add</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static explicit operator SET<T>(LIST<T> o)
        {
            SET<T> retVal = new SET<T>();
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
        /// Create a set of T from Array of graphable (shallow copy)
        /// </summary>
        /// <remarks>Usually, this is called when formatting</remarks>
        /// <param name="o">The array of objects to add</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        internal static SET<T> Parse(LIST<IGraphable> o)
        {
            SET<T> retVal = new SET<T>(o);
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
        /// Parse a SET of T from the string <paramref name="s"/>
        /// </summary>
        /// <param name="s">The string to parse</param>
        /// <returns>The parsed set</returns>
        internal static SET<T> FromString(string s)
        {
            string[] values = s.Split(' ');
            SET<T> retVal = new SET<T>((Comparison<T>)((a,b)=>a.Equals(b) ? 0 : -1));
            foreach (string value in values)
                retVal.Add((T)Util.FromWireFormat(value, typeof(T)));
            return retVal;
        }

        /// <summary>
        /// Convert this set to a string
        /// </summary>
        public override string ToString()
        {
            StringBuilder rv = new StringBuilder();
            foreach (T itm in items)
                rv.AppendFormat("{0} ", Util.ToWireFormat(itm));
            if(rv.Length > 0) rv.Remove(rv.Length - 1,1);
            return rv.ToString();
        }

        #region IEquatable<SET<T>> Members

        /// <summary>
        /// Determine if this SET of T equals another SET of T
        /// </summary>
        /// <remarks>
        /// This equality method differs from the <see cref="T:MARC.Everest.DataTypes.BAG{T}"/> and <see cref="T:MARC.Everest.DataTypes.LIST{T}"/>
        /// in that it not only compares the contents of each set to ensure that all data is present in both sets, it uses the comparator of 
        /// this set to determine if the "item" is the same (rather than the equality method)
        /// </remarks>
        public bool Equals(SET<T> other)
        {
            if (other != null)
            {
                bool result = base.Equals((ANY)other) &&
                    other.Count == this.Count;
                for (int i = 0; i < (result ? items.Count : 0); i++)
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
