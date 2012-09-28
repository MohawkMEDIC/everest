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
import java.util.Arrays;
import java.util.Collection;
import java.util.List;
import java.util.ListIterator;

import ca.marc.everest.annotations.ConformanceType;
import ca.marc.everest.annotations.Property;
import ca.marc.everest.annotations.PropertyType;
import ca.marc.everest.annotations.Structure;
import ca.marc.everest.annotations.StructureType;
import ca.marc.everest.datatypes.BL;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.ISemanticEquals;
import ca.marc.everest.datatypes.interfaces.ISequence;

/**
 * Represents a collection that contains discrete (but not necessarily distinct) values in a defined sequence.
 */
@Structure(name = "LIST", structureType = StructureType.DATATYPE)
public class LIST<T> extends COLL<T> implements List<T>, ISequence<T> {
	
	// List of items
	private List<T> m_list = new ArrayList<T>();
	
	/**
	 * Get the items in the LIST instance
	 */
	@Override
	@Property(name = "item", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.NONSTRUCTURAL)
	public Collection<T> getItems() {
		return this.m_list;
	}
	
	/**
	 * Creates a new instance of the LIST datatype
	 */
	public LIST() { super(); }
	
	/**
	 * Creates a new instance of the LIST datatype with the specified items
	 * @param items The initial set of items to place into the LIST
	 */
	public LIST(Iterable<T> items) 
	{
		this();
		for(T item : items)
			this.add(item);
		
	}
	
	/**
	 * Create a list from items
	 * @param items The items to seed the list with
	 */
	public static <T> LIST<T> createLIST(T... items)
	{
		return new LIST<T>(Arrays.asList(items));
	}
	
	/**
	 * Returns a portion of the list starting from the specified item index
	 * @param start The starting index 
	 * @return A new instance of LIST containing the subsequence
	 */
	public LIST<T> subSequence(int start)
	{
		return this.subSequence(start, this.size() - 1);
	}

	/**
	 * Add an item at the specified index
	 * @param index The index to insert the item
	 * @param element The element to be inserted
	 */
	@Override
	public void add(int index, T element) {
		this.m_list.add(index, element);
	}

	/**
	 * Add all items from c to the LIST at the specified index
	 * @param index The index to insert the items
	 * @param c The collection of items to add
	 */
	@Override
	public boolean addAll(int index, Collection<? extends T> c) {
		return this.m_list.addAll(index, c);
	}

	/**
	 * Get the index of an object within the sequence 
	 */
	@Override
	public int indexOf(Object o) {
		return this.m_list.indexOf(o);
	}

	/**
	 * Get the last index of object o in the LIST
	 */
	@Override
	public int lastIndexOf(Object o) {
		return this.m_list.lastIndexOf(o);
	}

	/**
	 * Get the list iterator for the LIST
	 */
	@Override
	public ListIterator<T> listIterator() {
		return this.m_list.listIterator();
	}

	/**
	 * Get the list iterator starting at the specified index
	 */
	@Override
	public ListIterator<T> listIterator(int index) {
		return this.listIterator(index);
	}

	/**
	 * Remove an item at the specified index
	 */
	@Override
	public T remove(int index) {
		return this.m_list.remove(index);
	}

	/**
	 * Set the item at the specified index to the specified element value
	 */
	@Override
	public T set(int index, T element) {
		return this.m_list.set(index, element);
	}

	/**
	 * Get a sub-list of items (calls the HL7 defined function subSequence)
	 */
	@Override
	public List<T> subList(int fromIndex, int toIndex) {
		return this.m_list.subList(fromIndex, toIndex);
	}
	
	
	/**
	 * Get the first item in the collection
	 */
	public T first()
	{
		return !this.isEmpty() ? this.get(0) : null;
	}
	
	/**
	 * Get the last item in the collection
	 */
	public T last()
	{
		return !this.isEmpty() ? this.get(this.size()) : null;
	}

	/**
	 * Returns a sub-sequence of items from the list from start to end
	 * @param start The index of the first item to extract
	 * @param end The index of the last item to extract
	 */
	@Override
	public LIST<T> subSequence(int start, int end) {
		if(start >= size())
			throw new IllegalArgumentException("Start position is out of range");
		else if(end >= size())
			throw new IllegalArgumentException("End position is out of range");
		else if(start > end)
			throw new IllegalArgumentException("End must be larger than Start");
		
		return new LIST<T>(this.m_list.subList(start, end));
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_list == null) ? 0 : m_list.hashCode());
		return result;
	}

	@SuppressWarnings("rawtypes")
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		LIST other = (LIST) obj;
		if (m_list == null) {
			if (other.m_list != null)
				return false;
		} else if (!m_list.equals(other.m_list))
			return false;
		return true;
	}

	/**
	 * Determine if this LIST instance semantically equals another LIST instance.
	 * <p>Two LIST instances are semantically equal when they contain the same element in the same order</p>
	 */
	@SuppressWarnings("unchecked")
	@Override
	public BL semanticEquals(IAny other) {
		BL baseSem = super.semanticEquals(other);
        if (!baseSem.toBoolean())
            return baseSem;

        // Other is not a LIST
        if(!(other instanceof LIST<?>))
        	return BL.FALSE;
        
        LIST<T> otherList = (LIST<T>)other;
        if (otherList.isEmpty() && this.isEmpty())
            return BL.TRUE;
        else
        {
            boolean isEqual = this.size() == otherList.size();
            for (int i = 0; i < this.size() && isEqual; i++)
                isEqual &= ((ISemanticEquals)otherList.get(i)).semanticEquals((IAny)this.get(i)).toBoolean();
            return BL.fromBoolean(isEqual);
        }
	}

	
}
