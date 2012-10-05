/* 
 * Copyright 2008-2011 Mohawk College of Applied Arts and Technology
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
 * Date: 10-03-2012
 */
package ca.marc.everest.datatypes.generic;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.EverestValidationMessages;
import ca.marc.everest.datatypes.SetOperator;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IListContainer;
import ca.marc.everest.datatypes.interfaces.ISetComponent;
import ca.marc.everest.interfaces.IGraphable;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Represents set component expression which describes a continuous set of values 
 * of an ordered base type.
 */
@Structure(name = "SXPR", structureType = StructureType.DATATYPE, defaultTemplateType = IVL.class)
public class SXPR<T extends IAny> extends SXCM<T> implements List<SXCM<T>>, IListContainer {

	// Backing field for terms
	private LIST<SXCM<T>> m_terms = new LIST<SXCM<T>>();

	/**
	 * Creates a new instance of the SXPR data type
	 */
	public SXPR() { super(); }
	/**
	 * Creates a new instance of the SXPR data type with the specified terms
	 * @param terms The initial list of terms to seed the SXPR instance
	 */
	public SXPR(Iterable<SXCM<T>> terms)
	{
		super();
		this.m_terms = new LIST<SXCM<T>>(terms);
	}
	/**
	 * Creates a new instance of SXPR with the specified terms
	 * @param terms
	 * @return
	 */
	public static <T extends IAny> SXPR<T> createSXPR(SXCM<T>... terms)
	{
		return new SXPR<T>(Arrays.asList(terms));
	}
	
	/**
	 * Adds an item to the set expression
	 */
	@Override
	public boolean add(SXCM<T> e) {
		return this.m_terms.add(e);
	}

	/**
	 * Adds an item to the set expression at the specified index
	 */
	@Override
	public void add(int i, SXCM<T> e) {
		this.m_terms.add(i, e);
	}

	/**
	 * Adds all items from the specified collection to the set expression
	 */
	@Override
	public boolean addAll(Collection<? extends SXCM<T>> c) {
		return this.m_terms.addAll(c);
	}

	/**
	 * Inserts all items from the specified collection at the specified index
	 * in the set expression 
	 */
	@Override
	public boolean addAll(int i, Collection<? extends SXCM<T>> c) {
		return this.addAll(i, c);
	}

	/**
	 * Clears all terms from this set expression
	 */
	@Override
	public void clear() {
		this.m_terms.clear();
	}

	/**
	 * Determines if the set expression contains the specified term
	 */
	@Override
	public boolean contains(Object term) {
		return this.m_terms.contains(term);
	}

	/**
	 * Determines if the set expression contains all the specified terms
	 */
	@Override
	public boolean containsAll(Collection<?> term) {
		return this.m_terms.containsAll(term);
	}

	/**
	 * Gets a term at the specified index
	 */
	@Override
	public SXCM<T> get(int i) {
		return this.m_terms.get(i);
	}

	/**
	 * Gets the index of the specified term
	 */
	@Override
	public int indexOf(Object term) {
		return this.m_terms.indexOf(term);
	}

	/**
	 * Returns true if the set expression is empty
	 */
	@Override
	public boolean isEmpty() {
		return this.isNull() ? true : this.m_terms.size() > 0;
	}

	/**
	 * Get the iterator for this SXCM
	 */
	@Override
	public Iterator<SXCM<T>> iterator() {
		return this.m_terms.iterator();
	}

	/**
	 * Gets the last index of the specified term
	 */
	@Override
	public int lastIndexOf(Object term) {
		return this.m_terms.lastIndexOf(term);
	}

	/**
	 * Get the listiterator for the terms in this set expression
	 */
	@Override
	public ListIterator<SXCM<T>> listIterator() {
		return this.m_terms.listIterator();
	}

	/**
	 * Gets the list iterator for the terms in the set expression after the specified index
	 */
	@Override
	public ListIterator<SXCM<T>> listIterator(int index) {
		return this.m_terms.listIterator(index);
	}

	/**
	 * Removes the specified term from the set expression
	 */
	@Override
	public boolean remove(Object term) {
		return this.m_terms.remove(term);
	}

	/**
	 * Removes the item at the specified index
	 */
	@Override
	public SXCM<T> remove(int index) {
		return this.m_terms.remove(index);
	}

	/**
	 * Remove all terms in the specified collection from the set expression
	 */
	@Override
	public boolean removeAll(Collection<?> terms) {
		return this.m_terms.removeAll(terms);
	}

	/**
	 * Performs a retainall function on the specified terms
	 */
	@Override
	public boolean retainAll(Collection<?> terms) {
		return this.m_terms.retainAll(terms);
	}

	/**
	 * Sets the term at the specified index
	 */
	@Override
	public SXCM<T> set(int index, SXCM<T> term) {
		return this.m_terms.set(index, term);
	}

	/**
	 * Gets the number of terms in the set expression
	 */
	@Override
	public int size() {
		return this.m_terms.size();
	}

	/**
	 * Gets a sub-list of the terms in the set expression
	 */
	@Override
	public List<SXCM<T>> subList(int start, int end) {
		return this.m_terms.subList(start, end);
	}

	/**
	 * Convert this set expression to an array contianing all the terms
	 */
	@Override
	public Object[] toArray() {
		return this.m_terms.toArray();
	}

	/**
	 * Convert this set expression to an array containing all the terms
	 */
	@SuppressWarnings("unchecked")
	@Override
	public <T> T[] toArray(T[] arg) {
		return (T[])this.m_terms.toArray(arg);
	}
	
	/**
	 * Validate this instance of the set expression
	 * <p>An instance of SXPR is considered valid when it is not null flavored and contains more than
	 * one term or carries a null-flavor</p>
	 */
	@Override
	public boolean validate() {
		return (this.getNullFlavor() != null && (this.m_terms == null || this.m_terms.size() == 0) ) ^ (this.m_terms != null && this.m_terms.size() > 1);
	}
	
	/**
	 * Extended validation function returning a list of detected issues
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
        Collection<IResultDetail> retVal = new ArrayList<IResultDetail>(super.validateEx());
        if (this.getNullFlavor() != null && this.m_terms.size() > 0)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "SXPR", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        if (this.getNullFlavor() == null && (this.m_terms == null || this.m_terms.isNull() || this.m_terms.size() < 2))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "SXPR", EverestValidationMessages.MSG_INSUFFICIENT_TERMS, null));
        return retVal;
	}
	
	/**
	 * Calculate the hash code of the set expression
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_terms == null) ? 0 : m_terms.hashCode());
		return result;
	}
	/**
	 * Determine value equality
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		@SuppressWarnings("unchecked")
		SXPR<T> other = (SXPR<T>) obj;
		if (m_terms == null) {
			if (other.m_terms != null)
				return false;
		} else if (!m_terms.equals(other.m_terms))
			return false;
		return true;
	}
	
	/**
	 * Translate the SXPR instance to a QSET
	 */
	public QSET<T> translateToQSET()
	{
		// Ported from .NET implementation
		QSET<T> currentQset = null;
		
		// Basically the algorithm groups like terms and nests the resultant items into
		// the QSET equivalent expression tree using QSI, QSU, QSP, and QSD
		List<SXCM<T>> likeTerms = new ArrayList<SXCM<T>>(10);
		SetOperator lastTerm = SetOperator.Inclusive;
		int count = 0;
		for(SXCM<T> term : this)
		{
			
			// First, get the operator
			SetOperator thisTerm = term.getOperator();
			if(thisTerm == null) thisTerm = SetOperator.Inclusive;
			
			// Same operator or is the first?
			if(thisTerm.equals(lastTerm) || count == 0 || currentQset == null && likeTerms.size() == 1)
				likeTerms.add(term);
			else
			{
				currentQset = translateToQSETInternal(lastTerm, likeTerms, currentQset);
				likeTerms.clear();
				likeTerms.add(term);
			}
			
			// prep next iteration
			lastTerm = thisTerm;
			count++;
		}
		
		currentQset = translateToQSETInternal(lastTerm, likeTerms, currentQset);
		
		return currentQset;
	}
	
	/**
	 * Internal method that does the dirty work of translating the SXCM components to QSET
	 * @param operation the current set operation
	 * @param collectedTerms The terms that are grouped with the current term
	 * @param context The context of the operation 
	 */
	private QSET<T> translateToQSETInternal(SetOperator operation, List<SXCM<T>> collectedTerms, QSET<T> context) 
	{
		// Translate based on operation
		switch(operation)
		{
			case Exclusive: // > Difference or QSD
			{
				// Must be broken into groups of two (minuend and subtrahend)
				int iofs = 0;
				QSD<T> diffSet = null;
				if(context == null) // Minuend is the first term
				{
					diffSet = new QSD<T>(collectedTerms.get(0).translateToQSETComponent(), null);
					iofs = 1;
				}
				else // Current is QSET is minuend
					diffSet = new QSD<T>(context, null);
				
				// Rest of collected terms, nesting if needed
				for(int i = iofs; i < collectedTerms.size(); i++)
				{
					diffSet.setSubtrahend(collectedTerms.get(i).translateToQSETComponent());
					if(i + 1 < collectedTerms.size())
						diffSet = new QSD<T>(diffSet, null);
				}
				context = diffSet;
				break;
			}
			case Inclusive: // > Union ... This one is pretty easy
			{
				QSU<T> unionSet = new QSU<T>();
                boolean isQssCandidate = true;
                if (context != null)
                {
                    isQssCandidate &= (context instanceof QSS<T>) && ((QSS<T>)context).size() == 1;
                    unionSet.add(context);
                }
                for(SXCM<T> itm : collectedTerms)
                {
                    ISetComponent<T> qsuItem = itm.translateToQSETComponent();
                    isQssCandidate &= (qsuItem instanceof QSS<T>) && ((QSS<T>)qsuItem).size() == 1;
                    unionSet.add(qsuItem);
                }

                // Is the union set composed entirely of QSS instances with one item?
                if (isQssCandidate)
                {
                    QSS<T> qssSet = new QSS<T>();
                    for(ISetComponent<T> itm : unionSet)
                        qssSet.add(((QSS<T>)itm).First());
                    context = qssSet;
                }
                else
                    context = unionSet;
                break;
			}
			case Intersect: // > Intersect also quite simple
			{
				 QSI<T> intersectSet = new QSI<T>();
                 if (context != null)
                     intersectSet.add(context);
                 for (SXCM<T> itm : collectedTerms)
                     intersectSet.Add(itm.translateToQSETComponent());
                 context = intersectSet;
                 break;
			}
			case PeriodicHull: // periodic hull, same as difference
			{
                // QSP must be broken into groups of two
                int iofs = 0;
                QSP<T> periodSet = null;
                if (context == null) // Null, then the minuend is our first term
                {
                    periodSet = new QSP<T>(collectedTerms[0].translateToQSETComponent(), null);
                    iofs = 1;
                }
                else // Not, then the current QSET is the minuend
                    periodSet = new QSP<T>(context, null);
                for (int i = iofs; i < collectedTerms.size(); i++)
                {
                    periodSet.setHigh(collectedTerms.get(i).translateToQSETComponent());
                    if (i + 1 < collectedTerms.size()) // We need to make a new QSD where this is the minuend
                        periodSet = new QSP<T>(periodSet, null);
                }
                context = periodSet;
                break;
			}
			default:
				throw new UnsupportedOperationException("Cannot represent this set component in QSET");
				break;
		}
		
		return context;
	}
	/**
	 * Gets the contained list
	 */
	@Override
	public List<IGraphable> getContainedList() {
		return new LIST<IGraphable>(this.m_terms);
	}
	/**
	 * Sets the contained list
	 */
	@Override
	public void setContainedList(List<IGraphable> value) {
		this.m_terms = new LIST<SXCM<T>>(value);
	}
	
}
