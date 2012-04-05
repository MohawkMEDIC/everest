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
 * Date: 06-29-2011
 */
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.datatypes.*;
import ca.marc.everest.annotations.*;
import ca.marc.everest.interfaces.*;

/**
 * A set of consecutive values of an ordered base datatype. Any ordered type can be the basis of an IVL
 */
public class IVL<T extends ANY> extends SXCM<T>  {

	// backing field for low inclusive attribute
	private boolean m_lowInclusive;
	private boolean m_lowInclusiveSet;
	// backing field for high inclusive attribute
	private boolean m_highInclusive;
	private boolean m_highInclusiveSet;
	// backing field for the width
	private PQ m_width;
	// backing field for the lower bound of the set
	private T m_low;
	// backing field for the upper bound of the set
	private T m_high;
	// backing field for original text
	private ED m_originalText;
	
	/**
	 * Creates a new instance of the interval type
	 */
	public IVL() { super(); }
	/**
	 * Creates a new instance of the interval type with the specified value
	 * @param value The value of the interval type
	 */
	public IVL(T value) { super(value); }
	/**
	 * Creates a new instance of the interval type with the specified upper and lower bounds
	 * @param low The initial value of the lower bound of the interval
	 * @param high The initial value of the upper bound of the interval
	 */
	public IVL(T low, T high) { this.m_low = low; this.m_high = high; }
	/**
	 * Creates a new instance of the interval type with the specified upper and lower bounds with
	 * the specified set operator applied 
	 * @param low The initial value of the lower bound of the interval
	 * @param high The value of the upper bound of the interval
	 * @param operator The manner in which this IVL instance participates in a containing SET expression
	 */
	public IVL(T low, T high, SetOperator operator) { this(low, high); this.setOperator(operator); }
	
	/**
	 * Gets the original text indicating where the interval was derived
	 */
	@Property(name = "originalText", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public ED getOriginalText() { return this.m_originalText; }
	/**
	 * Sets the original text indicating where the interval was derived
	 */
	public void setOriginalText(ED value) { this.m_originalText = value; }
	/**
	 * Gets the lower bound of the interval
	 */
	@Property(name = "low", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public T getLow() { return this.m_low; }
	/**
	 * Sets the lower bound of the interval
	 */
	public void setLow(T value) { this.m_low = value; }
	/**
	 * Gets a flag indicating if the lower bound of the interval is inclusive
	 */
	@Property(name = "lowClosed", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public boolean getLowInclusive() { return this.m_lowInclusive; }
	/**
	 * Sets a flag indicating if the lower bound of the interval is inclusive
	 */
	public void setLowInclusive(boolean value) { this.m_lowInclusive = value; this.m_lowInclusiveSet = true; }
	/**
	 * Gets a value indicating if the lower bound inclusivity flag has been set or is the default value
	 */
	public boolean getLowInclusiveSet() { return this.m_lowInclusiveSet; }
	/**
	 * Get the upper bound of the interval
	 */
	@Property(name = "high", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public T getHigh() { return this.m_high; }
	/**
	 * Set the upper bound of the interval
	 */
	public void setHigh(T value) { this.m_high = value;  }
	/**
	 * Gets a flag indicating if the upper bound of the interval is inclusive
	 */
	@Property(name = "highClosed", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public boolean getHighInclusive() { return this.m_highInclusive; }
	/**
	 * Sets a flag indicating if the upper bound of the interval is inclusive
	 */
	public void setHighInclusive(boolean value) { this.m_highInclusive = value; this.m_highInclusiveSet = true; }
	/**
	 * Gets a value indicating if the upper bound inclusivity flag has been set or is the default value
	 */
	public boolean getHighInclusiveSet() { return this.m_highInclusiveSet; }
	/**
	 * Gets the width of the interval
	 */
	@Property(name = "width", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public PQ getWidth() { return this.m_width; }
	/**
	 * Sets the width of the interval
	 */
	public void setWidth(PQ value) { this.m_width = value; }
	
	/**
	 * Identifies if this instance of an IVL is valid
	 * <p>
	 * Either the IVL is assigned a null flavor, or one of value, low, high or width is set and
     * when lowIncluded is set, low is set and
     * when highIncluded is set, high is set
     * </p>
	 */
	@Override
	public boolean validate()
	{
		return (this.getNullFlavor() != null) ^ (this.m_low != null || this.m_width != null || this.m_high != null || this.getValue() != null) &&
			(this.m_lowInclusiveSet && this.m_low != null || !this.m_lowInclusiveSet) &&
			(this.m_highInclusiveSet && this.m_high != null || !this.m_highInclusiveSet) &&
			super.validate();
		
	}
	

	

	
}
