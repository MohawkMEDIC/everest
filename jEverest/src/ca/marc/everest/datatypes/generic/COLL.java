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
 * Date: 09-27-2012
 */
package ca.marc.everest.datatypes.generic;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;

import ca.marc.everest.annotations.ConformanceType;
import ca.marc.everest.annotations.Property;
import ca.marc.everest.annotations.PropertyType;
import ca.marc.everest.annotations.Structure;
import ca.marc.everest.annotations.StructureType;
import ca.marc.everest.datatypes.ANY;
import ca.marc.everest.datatypes.BL;
import ca.marc.everest.datatypes.EverestValidationMessages;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.ICollection;
import ca.marc.everest.datatypes.interfaces.IPredicate;
import ca.marc.everest.datatypes.interfaces.ISemanticEquals;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * An abstract type intended to collect common functionality related
 * to collections
 */
@Structure(name = "COLL", structureType = StructureType.DATATYPE)
public abstract class COLL<T> extends ANY implements ICollection<T> {

	/**
	 * Gets the list of items
	 */
	@Property(name = "item", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.NONSTRUCTURAL)
	public abstract Collection<T> getItems();
	
	/**
	 * Determines if this set contains all 
	 */
	@Override
	public BL includesAll(ICollection<T> other) {
		return new BL(this.containsAll(other));
	}
	
	/** 
	 * Get the iterator for the set
	 */
	@Override
	public Iterator<T> iterator() {
		return this.iterator();
	}


	/**
	 * Determines if this set contains none of the other set
	 */
	@Override
	public BL excludesAll(ICollection<T> other) {
		boolean includesOne = false;
        for (T item : other)
            includesOne |= this.contains(item);
        return new BL(!includesOne);
	}

	/**
	 * Returns true if this collection is empty
	 */
	@Override
	public boolean isEmpty() {
		return this.size() == 0;
	}
	
	/**
	 * Gets an item from the collection
	 */
	@Override
	public T get(int index) throws IndexOutOfBoundsException {
		if(index >= this.size())
			throw new IndexOutOfBoundsException();
		Iterator<T> iterator = this.iterator();
		T retItem = null;
		for(int i = 0; i <= index; i++)
			retItem = iterator.next();
		return retItem;
	}

	/**
	 * Add an item to the collection
	 */
	@Override
	public boolean add(T e) {
		return this.getItems().add(e);
	}

	/**
	 * Add all items from the collection to the set
	 */
	@Override
	public boolean addAll(Collection<? extends T> c) {
		return this.getItems().addAll(c);
	}

	/**
	 * Clears the set
	 */
	@Override
	public void clear() {
		this.getItems().clear();
		
	}

	/**
	 * Returns true if the item specified is contained within this set.
	 * Uses the SemanticallyEqual definition of equality for determining 
	 * containment
	 */
	@Override
	public boolean contains(Object o) {
			
		boolean contains = false;
		for(T item : this)
			if(o instanceof ISemanticEquals)
				contains |= ((ISemanticEquals)o).semanticEquals((IAny)item).toBoolean();
			else
				contains |= (o != null && o.equals(item) ||
					item != null && item.equals(o) ||
					o == null && item == null);
		return contains;
	}

	/**
	 * Determines if this instance contains all of the objects in the target collection
	 */
	@Override
	public boolean containsAll(Collection<?> c) {
		boolean includesAll = true;
        for(Object item : c)
            includesAll &= this.contains(item);
        return includesAll;
	}

	/**
	 * Removes an item from the collection
	 */
	@Override
	public boolean remove(Object o) {
		return this.getItems().remove(o);
	}

	/**
	 * Removes all items in the specified collection from this collection
	 */
	@Override
	public boolean removeAll(Collection<?> c) {
		return this.getItems().removeAll(c);
	}

	/**
	 * Retains all items in the specified colleciton
	 */
	@Override
	public boolean retainAll(Collection<?> c) {
		return this.getItems().retainAll(c);
	}

	/**
	 * Gets the current size of the collection
	 */
	@Override
	public int size() {
		return this.getItems().size();
	}

	/**
	 * Convert this collection to an array
	 */
	@Override
	public Object[] toArray() {
		return this.getItems().toArray();
	}

	/**
	 * Convert this collection to an array
	 */
	@SuppressWarnings("hiding")
	@Override
	public <T> T[] toArray(T[] a) {
		return this.getItems().toArray(a);
	}
	
	/**
	 * Extended validation routine returning all identified problems
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		Collection<IResultDetail> retVal = new ArrayList<IResultDetail>();
		if(this.getNullFlavor() != null && !this.isEmpty())
			retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "COLL", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE));
		else if(this.getNullFlavor() == null && this.isEmpty())
			retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "COLL" ,EverestValidationMessages.MSG_NULLFLAVOR_MISSING));
		return retVal;
	}
	
	/**
	 * Validate the instance of COLL
	 */
	@Override
	public boolean validate() {
		return (this.getNullFlavor() != null) ^ (!this.isEmpty());
	}

	/**
	 * Finds all items within the set that matches the given predicate.
	 * 
	 * <pre>
	 * COLL&lt;TEL> emails = new LIST&lt;TEL>();
	 * emails.add(new TEL("mailto:f@m.com"));
	 * emails.add(new TEL("mailto:m@f.com"));
	 * emails.find(new IPredicate&lt;TEL>() {
	 * 	public boolean match(TEL item)
	 *  {
	 *  	return item.getValue().equals("mailto:f@m.com");
	 *  }
	 * });
	 * </pre>
	 * @param match
	 * @return
	 */
	public Collection<T> findAll(IPredicate<T> match)
	{
		Collection<T> retVal = new ArrayList<T>();
		for(T item : this)
			if(match.match(item))
				retVal.add(item);
		return retVal;
	}

	/**
	 * Find an item within the set that matches the given predicate, null if it is not found.
	 */
	public T find(IPredicate<T> match)
	{
		for(T item : this)
			if(match.match(item))
				return item;
		return null;
	}

}
