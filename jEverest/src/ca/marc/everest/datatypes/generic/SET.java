/* 
 * Copyright 2008/2011 Mohawk College of Applied Arts and Technology
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
 * User: Jaspinder Singh
 * Date: 06-28-2011
 */
package ca.marc.everest.datatypes.generic;

import java.util.*;

import ca.marc.everest.annotations.Structure;
import ca.marc.everest.annotations.StructureType;
import ca.marc.everest.datatypes.ANY;
import ca.marc.everest.datatypes.BL;
import ca.marc.everest.datatypes.interfaces.ICollection;
import ca.marc.everest.datatypes.interfaces.ISemanticEquals;


/**
 * A collection that contains other distinct and discrete values in no particular order.
 * This class is intended to be a wrapper for the standard Set classes.
 * @param <E> the element type
 */
@Structure(name="SET", structureType=StructureType.DATATYPE)
public class SET<T> extends ANY implements ICollection<T>, Set<T> {

	// Backing field for set items
	private Set<T> m_items = null;
	
	/**
	 * Create a new instance of the set
	 */
	public SET() { 
		super();
		this.m_items = new HashSet();
	}
	/**
	 * Create a new instance of the set with the specified capacity
	 */
	public SET(int capacity) {
		this();
		this.m_items = new HashSet(capacity);
	}
	/**
	 * Creates a new instance of the set with the specified iterable
	 * instance forming the content of the set
	 */
	public SET(Iterable<T> collection)
	{
		this();
		for(T item : collection)
			this.m_items.add(item);
	}
	/**
	 * Creates a new instance of the set containing only the specified
	 * first item
	 */
	public SET(T firstItem)
	{
		this();
		this.m_items.add(firstItem);
		
	}
	
	/** 
	 * Get the iterator for the set
	 */
	@Override
	public Iterator<T> iterator() {
		return this.m_items.iterator();
	}

	/**
	 * Determines if this set contains all 
	 */
	@Override
	public BL includesAll(ICollection<T> other) {
		return new BL(this.containsAll(other));
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
		return this.m_items.size() == 0;
	}

	/**
	 * Gets an item from the collection
	 */
	@Override
	public T getItem(int index) throws IndexOutOfBoundsException {
		if(index >= this.m_items.size())
			throw new IndexOutOfBoundsException();
		Iterator<T> iterator = this.m_items.iterator();
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
		return this.m_items.add(e);
	}

	/**
	 * Add all items from the collection to the set
	 */
	@Override
	public boolean addAll(Collection<? extends T> c) {
		return this.m_items.addAll(c);
	}

	/**
	 * Clears the set
	 */
	@Override
	public void clear() {
		this.m_items.clear();
		
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
				contains |= ((ISemanticEquals)o).semanticEquals((ANY)item);
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

	@Override
	public boolean remove(Object o) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean removeAll(Collection<?> c) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean retainAll(Collection<?> c) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public int size() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public Object[] toArray() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public <T> T[] toArray(T[] a) {
		// TODO Auto-generated method stub
		return null;
	}

	
	
	
	
}
