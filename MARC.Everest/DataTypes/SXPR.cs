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
 * User: fyfej
 * Date: 4/27/2010 12:14:39 PM
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes.Interfaces;
using System.Xml.Serialization;
using System.ComponentModel;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// A non generic implementation of the SXPR&lt;T&gt; class.
    /// </summary>
    /// <remarks>Supports several functions in GPMR</remarks>
    //[Obsolete("SXPR_TS is obsolete, consider using SXPR<IVL<TS>>", true)]
    //public class SXPR_TS : SXPR<IVL<TS>>
    //{
    //}

    /// <summary>
    /// A set component that is itself made up of set components that are evaluated as one value
    /// </summary>
    /// <remarks>
    /// <para>The set expression data type is a concept defined in Data Types R1. In order to maintain compatibility
    /// between data types versions, R2 concepts based on <see cref="T:MARC.Everest.DataTypes.QSET`1"/> are permitted for use in an SXPR and
    /// will be represented appropriately when formatting using an R1 formatter</para>
    /// <para>
    /// However, because of the nature of SXPR and <see cref="T:MARC.Everest.DataTypes.SXCM`1"/> R2 formatters will not render SXPR or SXCM 
    /// components appropriately and will ignore their content (throwing warnings). This is important because although
    /// it is possible to represent R2's QSET constructs using SXPR, it is very difficult to represent R1 SXPR concepts
    /// as R2 QSETs.
    /// </para>
    /// <para>
    /// The rules for mapping concepts based on <see cref="T:MARC.Everest.DataTypes.QSET`1"/> are as follows:
    /// </para>
    /// <list type="table">
    ///     <listheader><term>R2 Type</term>
    ///     <description>R1 Representation</description></listheader>
    ///     <item>
    ///         <term>QSI</term>    
    ///         <description>Mapped to SXPR with all components operators set to Intersect</description>
    ///     </item>
    ///     <item>
    ///         <term>QSU</term>    
    ///         <description>Mapped to SXPR with all components operators set to Inclusive</description>
    ///     </item>
    ///     <item>
    ///         <term>QSD</term>
    ///         <description>Mapped to SXPR with all components operators set to Exclusive. May only contain two components</description>
    ///     </item>
    ///     <item>
    ///         <term>QSS</term>    
    ///         <description>Mapped to SXPR with all components operators set to Convex Hull</description>
    ///     </item>
    ///     <item>
    ///         <term>QSP</term>
    ///         <description>Mapped to SXPR with all components operators set to Periodic Hull. May only contain two components</description>
    ///     </item>
    /// </list>
    /// <para>
    /// Of course, these rules mean that if the developer creates an SXPR using <see cref="T:MARC.Everest.DataTypes.QSET`1"/> classes and parses the
    /// instance back in, the resulting structure will be an equivalent to the original structure (substituting SXCM and SXPR for QSET, QSI, QSD, etc...)
    /// </para>
    /// </remarks>
    /// <seealso cref="T:MARC.Everest.DataTypes.QSET`1"/>
    /// <seealso cref="T:MARC.Everest.DataTypes.SET`1"/>
    [Structure(Name = "SXPR", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(IVL<TS>))]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class SXPR<T> : SXCM<T>, IListContainer, IList<SXCM<T>>, IEquatable<SXPR<T>>
        where T : IAny
    {

        /// <summary>
        /// Overriden value property
        /// </summary>
        /// <remarks>We never want the developer to see the SXCM value as it is not permitted in the SXPR context</remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [XmlIgnore]
        public override T Value { get; set; }

        /// <summary>
        /// Represents the components of this parenthetic set expression
        /// </summary>
        [Property(Name = "comp")]
        [XmlElement(ElementName = "comp")]
        public LIST<SXCM<T>> Terms { get; set; }

        /// <summary>
        /// Creates a new instance of the set Expression
        /// </summary>
        public SXPR() : base() 
        {
            Terms = new LIST<SXCM<T>>();
        }

        /// <summary>
        /// Creates a set expression with an initial list of terms specified by <paramref name="terms"/>
        /// </summary>
        /// <param name="terms">An initial set of terms that this SXPR contains</param>
        public SXPR(IEnumerable<SXCM<T>> terms)
            : base()
        {
            Terms = new LIST<SXCM<T>>(terms);
        }

        /// <summary>
        /// Constructs a new instance of the SXPR data type with the specified <paramref name="terms"/>
        /// </summary>
        public static SXPR<T> CreateSXPR(params SXCM<T>[] terms)
        {
            return new SXPR<T>(terms);
        }

        /// <summary>
        /// Validate this data structure
        /// </summary>
        /// <remarks>
        /// An instance of SXPR is considered valid when it carries no null flavor and contains
        /// more than one term, or carries and null flavor and has no terms
        /// </remarks>
        public override bool Validate()
        {
            return (NullFlavor != null && (Terms == null || Terms.Count == 0)) ^ (NullFlavor == null && Terms != null && Terms.Count > 1);
        }

        /// <summary>
        /// Extended validation function that returns detected issues
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());
            if (this.NullFlavor != null && this.Terms.Count > 0)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "SXPR", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (this.NullFlavor == null && (this.Terms == null || this.Terms.IsNull || this.Terms.Count < 2))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "SXPR", ValidationMessages.MSG_INSUFFICIENT_TERMS, null));
            return retVal;
        }

        #region IListContainer Members

        /// <summary>
        /// Get the contained list as a generic list of IGraphable
        /// </summary>
        LIST<IGraphable> IListContainer.ContainedList
        {
            get
            {
                return new LIST<IGraphable>(this.Terms);
            }
            set
            {
                this.Terms = LIST<SXCM<T>>.Parse(value);
            }
        }

        #endregion

        #region IList<SXCM<T>> Members

        /// <summary>
        /// Index of an item in the component collection
        /// </summary>
        public int IndexOf(SXCM<T> item)
        {
            return Terms.IndexOf(item);
        }

        /// <summary>
        /// Insert an item
        /// </summary>
        public void Insert(int index, SXCM<T> item)
        {
            Terms.Insert(index, item);
        }

        /// <summary>
        /// Remove at a particular index
        /// </summary>
        public void RemoveAt(int index)
        {
            Terms.RemoveAt(index);
        }

        /// <summary>
        /// Property indexer
        /// </summary>
        public SXCM<T> this[int index]
        {
            get
            {
                return Terms[index];
            }
            set
            {
                Terms[index] = value;
            }
        }

        #endregion

        #region ICollection<SXCM<T>> Members

        /// <summary>
        /// Add an item to the component collection
        /// </summary>
        public void Add(SXCM<T> item)
        {
            Terms.Add(item);
        }

        /// <summary>
        /// Clear this set expression
        /// </summary>
        public void Clear()
        {
            Terms.Clear();
        }

        /// <summary>
        /// Returns if the component collection contains the specified item
        /// </summary>
        public bool Contains(SXCM<T> item)
        {
            return Terms.Contains(item);
        }

        /// <summary>
        /// Copy to an array
        /// </summary>
        public void CopyTo(SXCM<T>[] array, int arrayIndex)
        {
            Terms.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get the count of items in the set expression
        /// </summary>
        public int Count
        {
            get { return Terms.Count; }
        }

        /// <summary>
        /// Returns true if the collection is read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return Terms.IsReadOnly; }
        }

        /// <summary>
        /// Remove an item from the collection
        /// </summary>
        public bool Remove(SXCM<T> item)
        {
            return Terms.Remove(item);
        }

        #endregion

        #region IEnumerable<SXCM<T>> Members

        /// <summary>
        /// Get an enumerator for the item
        /// </summary>
        public IEnumerator<SXCM<T>> GetEnumerator()
        {
            return Terms.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Get an enumerator for the set
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IEquatable<SXPR<T>> Members

        /// <summary>
        /// Determines if this SXPR of T is equal to another SXPR of T
        /// </summary>
        /// <remarks>
        /// This equality method checks to ensure that not only are the core data elements of the 
        /// two instanes the same, but each of the <see cref="P:Component"/> elements are equal.
        /// </remarks>
        public bool Equals(SXPR<T> other)
        {
            if (other != null)
            {
                bool result = base.Equals((SXCM<T>)other) &&
                    other.Count == this.Count;
                for (int i = 0; i < (result ? this.Count : 0); i++)
                    result &= this.Terms[i] != null ? this.Terms[i].Equals(other.Terms[i]) : other.Terms[i] == null;
                return result;
            }
            return false;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is SXPR<T>)
                return Equals(obj as SXPR<T>);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Translate to a QSET
        /// </summary>
        public QSET<T> TranslateToQSET()
        {

            // A little more trickily done... 
            // Basically we need to gather like terms and express
            // them as an QSI, QSU, QSP, QSS or QSD
            QSET<T> currentQset = null;

            List<SXCM<T>> likeTerms = new List<SXCM<T>>(10);
            SetOperator? lastTerm = SetOperator.Inclusive;
            int count = 0;
            foreach (var term in this)
            {
                // Usually the first we may have to default to inclusive
                SetOperator thisTerm = term.Operator ?? SetOperator.Inclusive;

                // same operator or is first one
                if (thisTerm.Equals(lastTerm) || count == 0 || currentQset == null && likeTerms.Count == 1)
                    likeTerms.Add(term);
                else
                {
                    currentQset = TranslateToQSETInternal(lastTerm.Value, likeTerms, currentQset);
                    likeTerms.Clear();
                    likeTerms.Add(term);
                }

                // prepare for next iteration
                lastTerm = thisTerm;
                count++;
            }

            currentQset = TranslateToQSETInternal(lastTerm.Value, likeTerms, currentQset);
            
            return currentQset;
        }

        /// <summary>
        /// Translate to a QSET data internally
        /// </summary>
        private QSET<T> TranslateToQSETInternal(SetOperator operation, List<SXCM<T>> collectedTerms, QSET<T> context)
        {
            // We need to construct the appropriate structure
            switch (operation)
            {
                case SetOperator.Exclusive: // difference
                    {
                        // QSD must be broken into groups of two
                        int iofs = 0;
                        QSD<T> diffSet = null;
                        if (context == null) // Null, then the minuend is our first term
                        {
                            diffSet = new QSD<T>(collectedTerms[0].TranslateToQSETComponent(), null);
                            iofs = 1;
                        }
                        else // Not, then the current QSET is the minuend
                            diffSet = new QSD<T>(context, null);
                        for (int i = iofs; i < collectedTerms.Count; i++)
                        {
                            diffSet.Subtrahend = collectedTerms[i].TranslateToQSETComponent();
                            if (i + 1 < collectedTerms.Count) // We need to make a new QSD where this is the minuend
                                diffSet = new QSD<T>(diffSet, null);
                        }
                        context = diffSet;
                        break;
                    }
                case SetOperator.Inclusive: // union is pretty easy
                    {
                        QSU<T> unionSet = new QSU<T>();
                        bool isQssCandidate = true;
                        if (context != null)
                        {
                            isQssCandidate &= (context is QSS<T>) && (context as QSS<T>).Count() == 1;
                            unionSet.Add(context);
                        }
                        foreach (var itm in collectedTerms)
                        {
                            var qsuItem = itm.TranslateToQSETComponent();
                            isQssCandidate &= (qsuItem is QSS<T>) && (qsuItem as QSS<T>).Count() == 1;
                            unionSet.Add(qsuItem);
                        }

                        // Is the union set composed entirely of QSS instances with one item?
                        if (isQssCandidate)
                        {
                            var qssSet = new QSS<T>();
                            foreach (var itm in unionSet)
                                qssSet.Add((itm as QSS<T>).First());
                            context = qssSet;
                        }
                        else
                            context = unionSet;
                        break;
                    }
                case SetOperator.Intersect: // intersect, also pretty easy
                    {
                        QSI<T> intersectSet = new QSI<T>();
                        if (context != null)
                            intersectSet.Add(context);
                        foreach (var itm in collectedTerms)
                            intersectSet.Add(itm.TranslateToQSETComponent());
                        context = intersectSet;
                        break;
                    }
                case SetOperator.PeriodicHull: // periodic hull, same as difference
                    {
                        // QSP must be broken into groups of two
                        int iofs = 0;
                        QSP<T> periodSet = null;
                        if (context == null) // Null, then the minuend is our first term
                        {
                            periodSet = new QSP<T>(collectedTerms[0].TranslateToQSETComponent(), null);
                            iofs = 1;
                        }
                        else // Not, then the current QSET is the minuend
                            periodSet = new QSP<T>(context, null);
                        for (int i = iofs; i < collectedTerms.Count; i++)
                        {
                            periodSet.High = collectedTerms[i].TranslateToQSETComponent();
                            if (i + 1 < collectedTerms.Count) // We need to make a new QSD where this is the minuend
                                periodSet = new QSP<T>(periodSet, null);
                        }
                        context = periodSet;
                        break;
                    }
                case SetOperator.Hull: // convex hull
                    {
                        throw new InvalidOperationException("Cannot represent a HULL value");
                    }
            }

            return context;
        }

        
        
    }
}
