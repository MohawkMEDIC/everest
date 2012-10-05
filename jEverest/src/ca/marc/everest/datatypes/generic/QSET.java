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
 * Date: 10-03-2012
 */
package ca.marc.everest.datatypes.generic;

import java.util.Iterator;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.ANY;
import ca.marc.everest.datatypes.ED;
import ca.marc.everest.datatypes.SetOperator;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IEncapsulatedData;
import ca.marc.everest.datatypes.interfaces.INormalizable;
import ca.marc.everest.datatypes.interfaces.IOriginalText;
import ca.marc.everest.datatypes.interfaces.ISetComponent;
import ca.marc.everest.datatypes.interfaces.IImplicitInterval;

/**
 * Specifies a set of consecutive values of an ordered base type.
 * <p>QSET and its derivatives are concepts defined in HL7v3 data types R2 and 
 * should be used when communicating with R2 systems (though there are translation
 * methods to/from QSET and the R1 SXCM data types).</p>
 * @see QSI QSI (Intersection)
 * @see QSD QSD (Difference)
 * @see QSP QSP (Periodic Hull)
 * @see QSU QSU (Union)
 * @see SXPR SXPR (Set Expression)
 * @see SXCM SXCM (Set Components)
 * @see GTS GTS (General Timing Specification)
 */
@Structure(name = "QSET", structureType = StructureType.DATATYPE )
public abstract class QSET<T extends IAny> extends ANY implements ISetComponent<T>, IOriginalText, INormalizable
{

	// backing field for original text
	private ED m_originalText;
	
	/**
	 * Gets the equivalent SXCM set operator for the particular type of QSET
	 */
	protected abstract SetOperator getEquivalentSetOperator();
	
	/**
	 * Default constructor for the abstract type
	 */
	public QSET() { super(); } 

	/**
	 * Gets the reasoning behind the selected set expression
	 */
	@Property(name = "originalText", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public IEncapsulatedData getOriginalText() {
		return this.m_originalText;
	}

	/**
	 * Sets the reasoning behind the selected set expression
	 */
	@Override
	public void setOriginalText(IEncapsulatedData value) {
		this.m_originalText = (ED)value;
	}

	/**
	 * Sets the reasoning behind the selected set expression
	 * @param value
	 */
	public void setOriginalText(ED value)
	{
		this.m_originalText = value;
	}

	/**
	 * Calculate the hashcode of the QSET
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_originalText == null) ? 0 : m_originalText.hashCode());
		return result;
	}

	/**
	 * Determine if this instance of the object value equals another
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
		QSET<T> other = (QSET<T>) obj;
		if (m_originalText == null) {
			if (other.m_originalText != null)
				return false;
		} else if (!m_originalText.equals(other.m_originalText))
			return false;
		return true;
	}
	
	/**
	 * Translates this QSET to an instance of SXPR
	 * @throws CloneNotSupportedException, UnsupportedOperationException 
	 */
	@SuppressWarnings("unchecked")
	public SXPR<T> translateToSXPR() throws CloneNotSupportedException, UnsupportedOperationException
	{

		SXPR<T> retVal = new SXPR<T>();
		
		if(this.isNull())
			retVal.setNullFlavor(this.getNullFlavor());
		else if(this instanceof Iterable<?>)
		{
			Iterator<T> iterator = ((Iterable<T>)this).iterator();
			while(iterator.hasNext())
			{
				IAny current = iterator.next();
				if(current instanceof QSET<?>)
				{
					SXPR<T> sxpr = ((QSET<T>)current).translateToSXPR();
					sxpr.setOperator(this.getEquivalentSetOperator());
					retVal.add(sxpr);
				}
				else if(current instanceof SXCM<?>) // Already an SXCM in this QSET so just copy
				{
					SXCM<T> sxcm = new SXCM<T>((SXCM<T>)current);
					sxcm.setOperator(this.getEquivalentSetOperator());
					retVal.add(sxcm);
				}
				else if(current instanceof IAny)
				{
					SXCM<T> sxcm = null;
					if(current instanceof IImplicitInterval<?>)
						sxcm = (SXCM<T>)((IImplicitInterval<T>)current).toIVL();
					else
						throw new UnsupportedOperationException("Cannot interpret set member");
					sxcm.setOperator(this.getEquivalentSetOperator());
					retVal.add(sxcm);
				}
			}
		}
		
		return retVal;
	}
	
}
