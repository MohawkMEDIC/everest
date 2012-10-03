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
 * User: Justin Fyfe
 * Date: 10-01-2012
 */
package ca.marc.everest.datatypes.generic;

import java.util.Arrays;
import java.util.Collection;
import java.util.Iterator;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.*;
import ca.marc.everest.datatypes.interfaces.*;

/**
 * Represents a list of sampled values with each new term 
 * scaled and translated from a list of previous samples. Used
 * to specify sampled biosignals
 */
@Structure(name = "SLIST", structureType = StructureType.DATATYPE)
public class SLIST<T extends IQuantity> extends ANY implements ISequence<INT>, ISampledList {

	// backing field for digits
	private LIST<INT> m_digits = new LIST<INT>();
	// backing field for the origin property
	private T m_origin;
	// backing field for the scale property
	private IQuantity m_scale;
	
	/**
	 * Creates a new instance of the sampled list class
	 */
	public SLIST() { super(); }
	/**
	 * Creates a new instance of the sampled list class with the specified
	 * origin and scale
	 * @param origin The origin (first reading in the list) a physical quantity which a zero-digit in the sequence would represent
	 * @param scale The ratio-scale quantity that is factored out of the digit sequence
	 */
	public SLIST(T origin, IQuantity scale) { 
		super();
		this.m_origin = origin;
		this.m_scale = scale;
	}
	/**
	 * Creates a new instance of the sampled list class with the specified origin, scale
	 * and sequence items.
	 * @param origin The origin (first reading in the list) which represents a zero-digit in the sequence
	 * @param scale The ratio-scale quantity that is factored out of the digit sequence
	 * @param items The digits representing the readings or samples
	 */
	public SLIST(T origin, IQuantity scale, Collection<INT> items)
	{
		this(origin, scale);
		this.m_digits = new LIST<INT>(items);
	}
	/**
	 * Creates a new sampled list class
	 * @param origin The origin (first reading in the list) which represents a zero-digit in the sequence
	 * @param scale The ratio-scale quantity that is factored out of the digit sequence
	 * @param items The digits representing the readings or samples
	 * @return A new instance of SLIST
	 */
	public static <T extends IQuantity> SLIST<T> CreateSLIST(T origin, IQuantity scale, INT... items)
	{
		return new SLIST<T>(origin, scale, Arrays.asList(items));
	}

	/**
	 * Gets the origin of the sampled list
	 */
	@Property(name = "origin", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public IQuantity getOrigin() {
		return this.m_origin;
	}

	/**
	 * Sets the origin of the sampled list
	 */
	@SuppressWarnings("unchecked")
	@Override
	public void setOrigin(IQuantity value) {
		this.m_origin = (T)value;
	}

	/**
	 * Gets the ratio-scale quantity that is factored out of the digit
	 */
	@Property(name = "scale", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public IQuantity getScale() {
		return this.m_scale;
	}

	/**
	 * Sets the ratio-scale quantity that is factored out of the digit
	 */
	@Override
	public void setScale(IQuantity value) {
		this.m_scale = value;
	}

	/**
	 * Gets the items in the list
	 */
	@Property(name = "digit", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.STRUCTURAL)
	@Override
	public Collection<INT> getItems() {
		return this.m_digits.getItems();
	}

	/**
	 * Gets the first sample in the list
	 */
	@Override
	public INT first() {
		return this.m_digits.first();
	}

	/**
	 * Gets the last sample from the list
	 */
	@Override
	public INT last() {
		return this.m_digits.last();
	}

	/**
	 * Selects a sub-sequence from the sampled list
	 */
	@Override
	public ISequence<INT> subSequence(int start, int end) {
		return this.m_digits.subSequence(start, end);
	}

	/**
	 * Selects a sub=sequence from the sampled list
	 */
	@Override
	public ISequence<INT> subSequence(int start) {
		return this.subSequence(start);
	}
	
	/**
	 * Get the specified item at the index
	 */
	@Override
	public INT get(int index) {
		return this.m_digits.get(index);
	}
	/**
	 * Returns true if this sampled list contains all the samples in other
	 */
	@Override
	public BL includesAll(ICollection<INT> other) {
		return this.m_digits.includesAll(other);
	}
	/**
	 * Returns true if this sampled list contains none of the samples in other
	 */
	@Override
	public BL excludesAll(ICollection<INT> other) {
		return this.m_digits.excludesAll(other);
	}
	/**
	 * Returns true if the sampled list contains no samples
	 */
	@Override
	public boolean isEmpty() {
		return this.m_digits.isEmpty();
	}
	/**
	 * Gets the iterator for this item
	 */
	@Override
	public Iterator<INT> iterator() {
		return this.m_digits.iterator();
	}
	/**
	 * Add a sample to this SLIST
	 */
	@Override
	public boolean add(INT e) {
		return this.m_digits.add(e);
	}
	/**
	 * Adds all items in the collection to this sampled list
	 */
	@Override
	public boolean addAll(Collection<? extends INT> c) {
		return this.m_digits.addAll(c);
	}
	/**
	 * Clears all items from this list
	 */
	@Override
	public void clear() {
		this.m_digits.clear();
	}
	/**
	 * Returns true if the sampled list contains the object
	 */
	@Override
	public boolean contains(Object o) {
		return this.m_digits.contains(o);
	}
	/**
	 * Returns true if the sampled list contains all the items in the specified collection
	 */
	@Override
	public boolean containsAll(Collection<?> c) {
		return this.m_digits.containsAll(c);
	}
	/**
	 * Removes the specified sample from the list of samples
	 */
	@Override
	public boolean remove(Object o) {
		return this.m_digits.remove(o);
	}
	/**
	 * Removes all the samples contained in c from the list of samples
	 */
	@Override
	public boolean removeAll(Collection<?> c) {
		return this.m_digits.removeAll(c);
	}
	/**
	 * Performs the retainAll function against the collection of samples
	 */
	@Override
	public boolean retainAll(Collection<?> c) {
		return this.m_digits.retainAll(c);
	}
	/**
	 * Gets the number of samples in the sample list
	 */
	@Override
	public int size() {
		return this.m_digits.size();
	}
	/**
	 * Converts this list of samples to an array
	 */
	@Override
	public Object[] toArray() {
		return this.m_digits.toArray();
	}
	/**
	 * Converts the list of samples to an array
	 */
	@SuppressWarnings({ "unchecked", "hiding" })
	@Override
	public <T> T[] toArray(T[] a) {
		return (T[])this.m_digits.toArray();
	}

	/** 
	 * Calculates the hash code of this sampled list
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_digits == null) ? 0 : m_digits.hashCode());
		result = prime * result
				+ ((m_origin == null) ? 0 : m_origin.hashCode());
		result = prime * result + ((m_scale == null) ? 0 : m_scale.hashCode());
		return result;
	}
	/**
	 * Determines the value equality of this sampled list
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		SLIST other = (SLIST) obj;
		if (m_digits == null) {
			if (other.m_digits != null)
				return false;
		} else if (!m_digits.equals(other.m_digits))
			return false;
		if (m_origin == null) {
			if (other.m_origin != null)
				return false;
		} else if (!m_origin.equals(other.m_origin))
			return false;
		if (m_scale == null) {
			if (other.m_scale != null)
				return false;
		} else if (!m_scale.equals(other.m_scale))
			return false;
		return true;
	}
}
