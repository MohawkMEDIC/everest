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

import ca.marc.everest.annotations.ConformanceType;
import ca.marc.everest.annotations.Property;
import ca.marc.everest.annotations.PropertyType;
import ca.marc.everest.annotations.Structure;
import ca.marc.everest.annotations.StructureType;
import ca.marc.everest.datatypes.BL;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.ISemanticEquals;
import ca.marc.everest.datatypes.interfaces.ISet;
import ca.marc.everest.exceptions.DuplicateItemException;
import ca.marc.everest.formatters.FormatterUtil;
import ca.marc.everest.interfaces.IGraphable;


/**
 * A collection that contains other distinct and discrete values in where the sequence of items has meaning.
 * This class is intended to be a wrapper for the standard Set classes.
 * @param <E> the element type
 */
@Structure(name="SET", structureType=StructureType.DATATYPE)
public class SET<T extends IGraphable> extends COLL<T> implements Set<T>, ISet<T> {

	/**
	 * Represents a default comparator
	 */
	private final Comparator<T> f_defaultComparator = new Comparator<T>() {

		/**
		 * Default comparison works on Semantic Equality (if available) followed
		 * by value comparison
		 */
		@Override
		public int compare(T a, T b) {
			if(a instanceof ISemanticEquals && b instanceof IAny)
				return ((ISemanticEquals)a).semanticEquals((IAny)b).toBoolean() ? 0 : 1;
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
	private Comparator<T> m_comparator = f_defaultComparator;
	
	/**
	 * Gets the comparator that is currently being used by this instance of
	 * SET to determie duplicate entries
	 */
	public Comparator<T> getComparator() { return this.m_comparator; }
	/**
	 * Sets a custom comparator for determining if duplicate entries have been
	 * entered into this type
	 */
	public void setComparator(Comparator<T> value) { this.m_comparator = value; }
	
	/**
	 * Create a new instance of the set
	 */
	public SET() { 
		super();
	}
	/**
	 * Creates a new instance of the set with the specified iterable
	 * instance forming the content of the set
	 */
	public SET(Iterable<? extends IGraphable> collection)
	{
		this(collection, null);
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
	public SET(Comparator<T> comparator)
	{
		this();
		this.m_comparator = comparator;
	}
	/**
	 * Creates a new instance of a set using the custom comparator and the initial set identified
	 * @param comparator The custom comparator to use for detecting duplicates within the set
	 * @param collection The initial collection
	 */
	public SET(Iterable<? extends IGraphable> collection, Comparator<T> comparator)
	{
		this();
		this.m_comparator = comparator == null ? this.f_defaultComparator : comparator;
		for(IGraphable item : collection)
			this.add((T)item);
		
	}

	/**
	 * Create a set helper method
	 */
	public static <T extends IGraphable> SET<T> createSET(T... items)
	{
		return new SET<T>(Arrays.asList(items));
	}
	
	/**
	 * Get items collection
	 */
	@Override
	@Property(name = "item", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.NONSTRUCTURAL)
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
	 * <p>Note: Uses the comparator for the compare operation</p>
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
	
	/**
	 * Calculate hash code
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_set == null) ? 0 : m_set.hashCode());
		return result;
	}
	/**
	 * Determine equality between this instance of SET and another
	 */
	@SuppressWarnings("rawtypes")
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		SET other = (SET) obj;
		if (m_set == null) {
			if (other.m_set != null)
				return false;
		} else if (!m_set.equals(other.m_set))
			return false;
		return true;
	}
	
	/**
	 * Determines semantic equality between this instance of SET and another datatype
	 * <p>Two instances of SET are considered semantically equal when both contain the same items in the sequence</p>
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
        
        SET<T> otherList = (SET<T>)other;
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
