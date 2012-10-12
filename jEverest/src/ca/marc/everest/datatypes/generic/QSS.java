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
 * Date: 10-11-2012
 */
package ca.marc.everest.datatypes.generic;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

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
 * Represents a QSET{T} that has been specialized to contain a list of primitive values which
 * are unioned to provide a complete set of data
 */
@Structure(name = "QSS", structureType = StructureType.DATATYPE)
public class QSS<T extends IAny> extends QSET<T> implements IListContainer, Collection<T> {

	// The list of terms
	private List<T> m_terms = new ArrayList<T>();
	
	/**
	 * Creates a new instance of QSS
	 */
	public QSS() { super(); }
	/**
	 * Creates a new instance of QSS with the specified collection of terms
	 */
	public QSS(Collection<T> terms)
	{
		this();
		this.m_terms = new ArrayList<T>(terms);
	}
	/**
	 * Creates an instance of QSS with the specified terms
	 */
	public static <T extends IAny> QSS<T> createQSS(T... terms)
	{
		return new QSS<T>(Arrays.asList(terms));
	}

	/**
	 * Gets the value which represents the terms
	 */
	@Property(name = "term", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public List<T> getTerms() { return this.m_terms; }
	/**
	 * Sets the terms in the collection
	 */
	public void setTerms(List<T> value) { this.m_terms = value; }
	
	/**
	 * Normalize the items
	 */
	@SuppressWarnings("unchecked")
	@Override
	public IGraphable normalize() {
		QSS<T> retVal = (QSS<T>)this.shallowCopy();
		return retVal;
	}

	/**
	 * Get the list contained in this collection
	 */
	@Override
	public List<IGraphable> getContainedList() {
		return new LIST<IGraphable>(this.m_terms);
	}

	/**
	 * Set the contained list
	 */
	@Override
	public void setContainedList(List<IGraphable> value) {
		this.m_terms = new ArrayList<T>(new LIST<T>(value));
	}

	/**
	 * Get the equivalent set operator
	 */
	@Override
	protected SetOperator getEquivalentSetOperator() {
		return SetOperator.Inclusive;
	}
	/**
	 * Add an item to the collection
	 */
	@Override
	public boolean add(T term) {
		return this.m_terms.add(term);
	}
	/**
	 * Add all items from the collection to the list of terms
	 */
	@Override
	public boolean addAll(Collection<? extends T> terms) {
		return this.m_terms.addAll(terms);
	}
	/**
	 * Clears the item from the list
	 */
	@Override
	public void clear() {
		this.m_terms.clear();
	}
	/**
	 * Returns true if the collection contains the specified item
	 */
	@Override
	public boolean contains(Object term) {
		return this.m_terms.contains(term);
	}
	/**
	 * Returns true if the collection of terms contains all the 
	 * terms in the specified array.
	 */
	@Override
	public boolean containsAll(Collection<?> terms) {
		return this.m_terms.containsAll(terms);
	}
	/**
	 * Returns true if the collection is empty
	 */
	@Override
	public boolean isEmpty() {
		return this.m_terms != null && this.m_terms.size() == 0;
	}
	/**
	 * Gets the iterator for the collection
	 */
	@Override
	public Iterator<T> iterator() {
		return this.m_terms.iterator();
	}
	/**
	 * Remove an object from the list of terms
	 */
	@Override
	public boolean remove(Object term) {
		return this.m_terms.remove(term);
	}
	/**
	 * Remove all the items from the list of terms
	 */
	@Override
	public boolean removeAll(Collection<?> terms) {
		return this.m_terms.removeAll(terms);
	}
	/**
	 * Retain all items in the collection
	 */
	@Override
	public boolean retainAll(Collection<?> terms) {
		return this.m_terms.retainAll(terms);
	}
	/**
	 * Gets the size of the collection
	 */
	@Override
	public int size() {
		return this.m_terms.size();
	}
	/**
	 * Conver tthis collection to an array
	 */
	@Override
	public Object[] toArray() {
		return this.m_terms.toArray();
	}
	/**
	 * Convert this collection to an array
	 */
	@Override
	public <T> T[] toArray(T[] typeColl) {
		return this.m_terms.toArray(typeColl);
	}
	/**
	 * Validate extended
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		List<IResultDetail> retVal = new ArrayList<IResultDetail>();

        if (!(this.isNull() ^ !this.isEmpty()))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSS", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        for (T qs : this)
            if (qs == null || qs.isNull())
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSS", EverestValidationMessages.MSG_NULL_COLLECTION_VALUE, null));
        return retVal;
	}
	/**
	 * Validate the QSS instance
	 */
	@Override
	public boolean validate() {
		boolean isValid = this.isNull() ^ !this.isEmpty();
        for (T qs : this)
            isValid &= qs != null && !qs.isNull();
        return isValid;
	}
	/** (non-Javadoc)
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_terms == null) ? 0 : m_terms.hashCode());
		return result;
	}
	/** (non-Javadoc)
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		QSS other = (QSS) obj;
		if (m_terms == null) {
			if (other.m_terms != null)
				return false;
		} else if (!m_terms.equals(other.m_terms))
			return false;
		return true;
	}

}
