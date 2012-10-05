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
 * Date: 10-05-2012
 */
package ca.marc.everest.datatypes.generic;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IListContainer;
import ca.marc.everest.datatypes.interfaces.ISetComponent;
import ca.marc.everest.interfaces.IGraphable;

/**
 * Represents a QSET that contains other items in a generic way
 */
abstract class QSC<T extends IAny> extends QSET<T> implements Collection<ISetComponent<T>>, IListContainer {

	// backing field for terms
	protected List<ISetComponent<T>> p_terms = new ArrayList<ISetComponent<T>>();
	
	/**
	 * Creates a new instance of QSC
	 */
	protected QSC() { super(); }

	/**
	 * Gets the list of terms that make up the QSET instance
	 * @return
	 */
	@Property(name = "term", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.MANDATORY)
	public List<ISetComponent<T>> getTerms() {
		return this.p_terms;
	}
	/**
	 * Sets the list of terms that make up this QSET instance
	 */
	public void setTerms(List<ISetComponent<T>> value) {
		this.p_terms = value;
	}
	
	/**
	 * Gets the list of data that is contained in this datatypes
	 */
	@Override
	public List<IGraphable> getContainedList() {
		return new LIST<IGraphable>(this.p_terms);
	}
	/**
	 * Sets the list of data that is contained
	 */
	@Override
	public void setContainedList(List<IGraphable> value) {
		this.p_terms = new ArrayList<ISetComponent<T>>(new LIST<ISetComponent<T>>(value));
	}
	/**
	 * Add a set component expression to the list of terms
	 */
	@Override
	public boolean add(ISetComponent<T> e) {
		return this.p_terms.add(e);
	}
	/**
	 * Adds all the items from the specified collection to the set expression
	 */
	@Override
	public boolean addAll(Collection<? extends ISetComponent<T>> c) {
		return this.p_terms.addAll(c);
	}
	/**
	 * Clear all terms from this set expression
	 */
	@Override
	public void clear() {
		this.p_terms.clear();
	}
	/**
	 * Returns true if the set expression contains the specified object
	 */
	@Override
	public boolean contains(Object o) {
		return this.p_terms.contains(o);
	}
	/**
	 * Returns true if the set expression contains all the specified objects
	 */
	@Override
	public boolean containsAll(Collection<?> c) {
		return this.p_terms.containsAll(c);
	}
	/**
	 * Returns true if this collection is empty
	 */
	@Override
	public boolean isEmpty() {
		return this.p_terms.size() == 0;
	}
	/**
	 * Gets the iterator for this set expression
	 */
	@Override
	public Iterator<ISetComponent<T>> iterator() {
		return this.p_terms.iterator();
	}
	/**
	 * Removes an object from the set expression
	 */
	@Override
	public boolean remove(Object o) {
		return this.p_terms.remove(o);
	}
	/**
	 * Removes all items from the set expression
	 */
	@Override
	public boolean removeAll(Collection<?> c) {
		return this.p_terms.removeAll(c);
	}
	/**
	 * Retains all items in the collection
	 */
	@Override
	public boolean retainAll(Collection<?> c) {
		return this.p_terms.retainAll(c);
	}
	/**
	 * Gets the size of the collection
	 */
	@Override
	public int size() {
		return this.p_terms.size();
	}
	/**
	 * Convert this set expression to an array
	 */
	@Override
	public Object[] toArray() {
		return this.p_terms.toArray();
	}
	/**
	 * Convert this set expression to an array
	 */
	@SuppressWarnings("hiding")
	@Override
	public <T> T[] toArray(T[] a) {
		return this.p_terms.toArray(a);
	}
	/**
	 * Validate
	 */
	@Override
	public boolean validate() {
		 boolean isValid = (this.isNull() && this.size() == 0) ^ (!this.isNull() && this.size() > 1);
         for(ISetComponent<T> qs : this.p_terms)
             isValid &= qs != null && !qs.isNull();
         return isValid;
	}
	/**
	 * Calculate the hash code of this type
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((p_terms == null) ? 0 : p_terms.hashCode());
		return result;
	}
	/**
	 * Determine value equality
	 */
	@SuppressWarnings("unchecked")
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		QSC<T> other = (QSC<T>) obj;
		if (p_terms == null) {
			if (other.p_terms != null)
				return false;
		} else if (!p_terms.equals(other.p_terms))
			return false;
		return true;
	}
	
	
}
