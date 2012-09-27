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
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.ISemanticEquals;
import ca.marc.everest.exceptions.DuplicateItemException;


/**
 * A collection that contains other distinct and discrete values in no particular order.
 * This class is intended to be a wrapper for the standard Set classes.
 * @param <E> the element type
 */
@Structure(name="SET", structureType=StructureType.DATATYPE)
public class SET<T> extends COLL<T> implements Set<T> {

	/**
	 * Represents a default comparator
	 */
	private static final Comparator<Object> f_defaultComparator = new Comparator<Object>() {

		/**
		 * Default comparison works on Semantic Equality (if available) followed
		 * by value comparison
		 */
		@Override
		public int compare(Object a, Object b) {
			if(a instanceof ISemanticEquals && b instanceof ANY)
				return ((ISemanticEquals)a).semanticEquals((ANY)b).toBoolean() ? 0 : 1;
			else if(a != null && a.equals(b) || 
					b != null && b.equals(a) ||
					a == null && b == null)
				return 0;
			else
				return 1;
		}
			
	};

	// Backing set
	private Set<T> m_set = new HashSet<T>();
	// Backing comparator
	private Comparator<Object> m_comparator = f_defaultComparator;
	
	/**
	 * Gets the comparator that is currently being used by this instance of
	 * SET to determie duplicate entries
	 */
	public Comparator<Object> getComparator() { return this.m_comparator; }
	/**
	 * Sets a custom comparator for determining if duplicate entries have been
	 * entered into this type
	 */
	public void setComparator(Comparator<Object> value) { this.m_comparator = value; }
	
	/**
	 * Create a new instance of the set
	 */
	public SET() { 
		super();
	}
	/**
	 * Create a new instance of the set with the specified capacity
	 */
	public SET(int capacity) {
		this();
	}
	/**
	 * Creates a new instance of the set with the specified iterable
	 * instance forming the content of the set
	 */
	public SET(Iterable<T> collection)
	{
		this(collection, f_defaultComparator);
	}
	
	/**
	 * Creates a new instance of the set containing only the specified
	 * first item
	 */
	public SET(T firstItem)
	{
		this();
		this.add(firstItem);
		
	}
	/**
	 * Creates a new instance of a set using the custom comparator
	 * @param comparator The custom comparator to use for detecting duplicates within the set
	 */
	public SET(Comparator<Object> comparator)
	{
		this();
		this.m_comparator = comparator;
	}
	/**
	 * Creates a new instance of a set using the custom comparator and the initial set identified
	 * @param comparator The custom comparator to use for detecting duplicates within the set
	 * @param collection The initial collection
	 */
	public SET(Iterable<T> collection, Comparator<Object> comparator)
	{
		this();
		this.m_comparator = comparator;
		for(T item : collection)
			this.add(item);
	}
	/**
	 * Get items collection
	 */
	@Override
	public Collection<T> getItems() {
		return this.m_set;
	}
	
	/**
	 * Add an item to the collection.
	 */
	@Override
	public boolean add(T e) throws DuplicateItemException, IllegalArgumentException {
		if(e == null)
			throw new IllegalArgumentException("e");
		else if(this.contains(e))
			throw new DuplicateItemException("Item already exists in the SET");
		return super.add(e);
	}
	/**
	 * Add all items to the collection
	 */
	@Override
	public boolean addAll(Collection<? extends T> c) throws DuplicateItemException, IllegalArgumentException {
		if(c == null)
			throw new IllegalArgumentException("c");
		
		for(T o : c)
			if(this.contains(o))
				throw new DuplicateItemException("Item already exists in the SET");
		
		return super.addAll(c);
	}
	
	/**
	 * Determines if this set contains the specified object
	 */
	@SuppressWarnings({ "unchecked" })
	@Override
	public boolean contains(Object o) throws NullPointerException {
		if(this.getComparator() == null)
			throw new NullPointerException("Comparator is null");
		for(T i : this)
			if(this.getComparator().compare(i, (T)o) == 0)
				return true;
		return false;
	}

	/**
	 * Returns all the items in this set except the items in the otherSet
	 */
	public SET<T> except(SET<T> otherSet)
	{
		SET<T> retVal = new SET<T>();
        for(T item : this)
            if(!otherSet.contains(item))
                retVal.add(item);
        return retVal;
	}
		
	/**
	 * Returns all the items in this set except the specified element
	 */
	public SET<T> except(T element)
	{
		SET<T> retVal = new SET<T>();
        for(T item : this)
            if(this.getComparator().compare(element, item) != 0)
                retVal.add(item);
        return retVal;
	}
	
	/**
	 * Return an intersection of all items in this set with the items in the otherSet
	 */
	public SET<T> intersect(SET<T> otherSet)
	{
		SET<T> retVal = new SET<T>(this.m_comparator);
        for(T item : this)
            if(otherSet.contains(item))
                retVal.add(item);
        return retVal;
	}
	
	/**
	 * Return an intersection of all items in this set with the element item
	 */
	public SET<T> intersect(T element)
	{
		SET<T> retVal = new SET<T>(this.m_comparator);
        for(T item : this)
            if(this.m_comparator.compare(item, element) == 0)
                retVal.add(item);
        return retVal;
	}
	
	/**
	 * Return an union of all items in this set with the items in the otherSet
	 */
	public SET<T> union(SET<T> otherSet)
	{
        SET<T> retVal = new SET<T>();
        for(T item : this)
            retVal.add(item);
        for(T item : otherSet)
            if(!retVal.contains(item))
                retVal.add(item);
        return retVal;
	}
	
	/**
	 * Return an union of all items in this set with the element item
	 */
	public SET<T> union(T element)
	{
        SET<T> retVal = new SET<T>();
        for(T item : this)
            retVal.add(item);
        if(!retVal.contains(element))
            retVal.add(element);
        return retVal;
	}
}
