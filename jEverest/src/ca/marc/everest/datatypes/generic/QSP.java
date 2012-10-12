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
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.EverestValidationMessages;
import ca.marc.everest.datatypes.SetOperator;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.ISetComponent;
import ca.marc.everest.interfaces.IGraphable;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Represents a QSET{T} that has been specialized as a difference between two other 
 * expressions
 */
@Structure(name = "QSP", structureType = StructureType.DATATYPE)
public class QSP<T extends IAny> extends QSET<T> implements Iterable<ISetComponent<T>> {

	// The minuend of this difference
	private ISetComponent<T> m_low;
	// The subtrahend of this difference
	private ISetComponent<T> m_high;
	
	/**
	 * Creates a new instance of the QSP{T} class
	 */
	public QSP() { super(); }
	/**
	 * Creates a new instance of the QSP class with the specified low and high
	 * @param low The initial value of the low
	 * @param high The initial value of the high
	 */
	public QSP(ISetComponent<T> low, ISetComponent<T> high)
	{
		this.m_low = low;
		this.m_high = high;
	}
	
	/**
	 * Gets the value representing the low of the periodic hull expression
	 */
	@Property(name = "low", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.MANDATORY)
	public ISetComponent<T> getLow() {
		return this.m_low;
	}
	/**
	 * Sets the value representing the low of the hull expression
	 */
	public void setLow(ISetComponent<T> value) {
		this.m_low = value;
	}
	/**
	 * Gets the value representing the hull expression
	 */
	@Property(name = "high", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.MANDATORY)
	public ISetComponent<T> getHigh() {
		return this.m_high;
	}
	/**
	 * Sets a value representing the hull expression
	 */
	public void setHigh(ISetComponent<T> value) {
		this.m_high = value;
	}
	/**
	 * Normalize the expression
	 */
	@SuppressWarnings("unchecked")
	@Override
	public IGraphable normalize() {
		QSP<T> retVal = (QSP<T>)this.shallowCopy();
        if (retVal.m_low instanceof SXPR<?>)
            retVal.m_low = ((SXPR<T>)retVal.m_low).translateToQSET();
        if (retVal.m_high instanceof SXPR<?>)
            retVal.m_high = ((SXPR<T>)retVal.m_high).translateToQSET();
        return retVal;
	}

	/**
	 * Get the iterator
	 */
	@Override
	public Iterator<ISetComponent<T>> iterator() {
		ArrayList<ISetComponent<T>> iterable = new ArrayList<ISetComponent<T>>();
		iterable.add(this.m_low);
		iterable.add(this.m_high);
		return iterable.iterator();
	}

	/**
	 * Get the equivalent set operator
	 */
	@Override
	protected SetOperator getEquivalentSetOperator() {
		return SetOperator.PeriodicHull;
	}
	/** (non-Javadoc)
	 * @see ca.marc.everest.datatypes.ANY#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		List<IResultDetail> retVal = new ArrayList<IResultDetail>();
        if (!(this.isNull() ^ (this.m_low != null && !this.m_low.isNull() && this.m_high != null && !this.m_high.isNull())))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSP", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        if (this.m_low == null || this.m_low.isNull())
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSP", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Minuend", String.format("ISetComponent<{0}>", "?")), null));
        if (this.m_high == null || this.m_high.isNull())
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSP", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Subtrahend", String.format("ISetComponent<{0}>", "?")), null));
        return retVal;
	}
	/** (non-Javadoc)
	 * @see ca.marc.everest.datatypes.HXIT#validate()
	 */
	@Override
	public boolean validate() {
		boolean isValid = this.isNull() ^ (this.m_low != null && !this.m_low.isNull() && this.m_high != null && !this.m_high.isNull());
        return isValid;
	}
	/** (non-Javadoc)
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_low == null) ? 0 : m_low.hashCode());
		result = prime * result
				+ ((m_high == null) ? 0 : m_high.hashCode());
		return result;
	}
	/** (non-Javadoc)
	 * @see java.lang.Object#equals(java.lang.Object)
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
		QSP<T> other = (QSP<T>) obj;
		if (m_low == null) {
			if (other.m_low != null)
				return false;
		} else if (!m_low.equals(other.m_low))
			return false;
		if (m_high == null) {
			if (other.m_high != null)
				return false;
		} else if (!m_high.equals(other.m_high))
			return false;
		return true;
	}
	

}
