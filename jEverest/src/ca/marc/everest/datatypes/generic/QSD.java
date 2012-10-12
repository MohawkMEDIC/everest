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
@Structure(name = "QSD", structureType = StructureType.DATATYPE)
public class QSD<T extends IAny> extends QSET<T> implements Iterable<ISetComponent<T>> {

	// The minuend of this difference
	private ISetComponent<T> m_minuend;
	// The subtrahend of this difference
	private ISetComponent<T> m_subtrahend;
	
	/**
	 * Creates a new instance of the QSD{T} class
	 */
	public QSD() { super(); }
	/**
	 * Creates a new instance of the QSD class with the specified minuend and subtrahend
	 * @param minuend The initial value of the minuend
	 * @param subtrahend The initial value of the subtrahend
	 */
	public QSD(ISetComponent<T> minuend, ISetComponent<T> subtrahend)
	{
		this.m_minuend = minuend;
		this.m_subtrahend = subtrahend;
	}
	
	/**
	 * Gets the value representing the minuend of the difference expression
	 */
	@Property(name = "minuend", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.MANDATORY)
	public ISetComponent<T> getMinuend() {
		return this.m_minuend;
	}
	/**
	 * Sets the value representing the minuend of the difference expression
	 */
	public void setMinuend(ISetComponent<T> value) {
		this.m_minuend = value;
	}
	/**
	 * Gets the value representing the subtrahend
	 */
	@Property(name = "subtrahend", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.MANDATORY)
	public ISetComponent<T> getSubtrahend() {
		return this.m_subtrahend;
	}
	/**
	 * Sets a value representing the subtrahend
	 */
	public void setSubtrahend(ISetComponent<T> value) {
		this.m_subtrahend = value;
	}
	/**
	 * Normalize the expression
	 */
	@SuppressWarnings("unchecked")
	@Override
	public IGraphable normalize() {
		QSD<T> retVal = (QSD<T>)this.shallowCopy();
        if (retVal.m_minuend instanceof SXPR<?>)
            retVal.m_minuend = ((SXPR<T>)retVal.m_minuend).translateToQSET();
        if (retVal.m_subtrahend instanceof SXPR<?>)
            retVal.m_subtrahend = ((SXPR<T>)retVal.m_subtrahend).translateToQSET();
        return retVal;
	}

	/**
	 * Get the iterator
	 */
	@Override
	public Iterator<ISetComponent<T>> iterator() {
		ArrayList<ISetComponent<T>> iterable = new ArrayList<ISetComponent<T>>();
		iterable.add(this.m_minuend);
		iterable.add(this.m_subtrahend);
		return iterable.iterator();
	}

	/**
	 * Get the equivalent set operator
	 */
	@Override
	protected SetOperator getEquivalentSetOperator() {
		return SetOperator.Exclusive;
	}
	/** (non-Javadoc)
	 * @see ca.marc.everest.datatypes.ANY#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		List<IResultDetail> retVal = new ArrayList<IResultDetail>();
        if (!(this.isNull() ^ (this.m_minuend != null && !this.m_minuend.isNull() && this.m_subtrahend != null && !this.m_subtrahend.isNull())))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSD", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        if (this.m_minuend == null || this.m_minuend.isNull())
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSD", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Minuend", String.format("ISetComponent<{0}>", "?")), null));
        if (this.m_subtrahend == null || this.m_subtrahend.isNull())
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSD", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Subtrahend", String.format("ISetComponent<{0}>", "?")), null));
        return retVal;
	}
	/** (non-Javadoc)
	 * @see ca.marc.everest.datatypes.HXIT#validate()
	 */
	@Override
	public boolean validate() {
		boolean isValid = this.isNull() ^ (this.m_minuend != null && !this.m_minuend.isNull() && this.m_subtrahend != null && !this.m_subtrahend.isNull());
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
				+ ((m_minuend == null) ? 0 : m_minuend.hashCode());
		result = prime * result
				+ ((m_subtrahend == null) ? 0 : m_subtrahend.hashCode());
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
		QSD<T> other = (QSD<T>) obj;
		if (m_minuend == null) {
			if (other.m_minuend != null)
				return false;
		} else if (!m_minuend.equals(other.m_minuend))
			return false;
		if (m_subtrahend == null) {
			if (other.m_subtrahend != null)
				return false;
		} else if (!m_subtrahend.equals(other.m_subtrahend))
			return false;
		return true;
	}
	

}
