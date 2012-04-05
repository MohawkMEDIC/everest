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
 * Date: 09-02-2011
 */
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.datatypes.*;

/**
 * Specifies a periodic interval of time centered around some event.
 * Note that VALUE cannot be set on this class 
 */
public class EIVL<T> extends SXCM<T> {

	// backing field for event
	private DomainTimingEvent m_event;
	// backing field for offset
	private IVL<PQ> m_offset;
	
	/**
	 * Creates a new instance of the event based interval class
	 */
	public EIVL() { super(); }
	/**
	 * Creates a new instance of the event based interval class with the
	 * specified event and offsets
	 * @param event The event around which this EIVL is bound
	 * @param offset The offset that marks the beginning and end time from the event 
	 */
	public EIVL(DomainTimingEvent event, IVL<PQ> offset)
	{
		super();
		this.m_event = event;
		this.m_offset = offset;
	}
	/**
	 * Creates a new instance of the event based interval class with the
	 * specified event, offset and set operator
	 * @param event The event around which this EIVL is bound
	 * @param offset The offset that marks the beginning and end time from the event 
	 * @param operator The manner in which this EIVL participates in the contained set expression
	 */
	public EIVL(DomainTimingEvent event, IVL<PQ> offset, SetOperator operator)
	{
		this(event, offset);
		this.setOperator(operator);
	}

	/**
	 * Setting value on EIVL is not supported
	 */
	@Override
	public void setValue(T value) { throw new UnsupportedOperationException("Setting value is not permitted on EIVL"); }
	
	/**
	 * Get the event around which this EIVL is bound
	 */
	public DomainTimingEvent getEvent() { return this.m_event; }
	/**
	 * Set the event around which this EIVL is bound
	 */
	public void setEvent(DomainTimingEvent value) { this.m_event = value; }
	/**
	 * Gets the offset that specifies how long after (or before) the bound event the 
	 * interval begins
	 */
	public IVL<PQ> getOffset() { return this.m_offset; }
	/**
	 * Sets the offset that specifies how long after (or before) the bound event the
	 * interval begins
	 */
	public void setOffset(IVL<PQ> value) { this.m_offset = value; }
	
}
