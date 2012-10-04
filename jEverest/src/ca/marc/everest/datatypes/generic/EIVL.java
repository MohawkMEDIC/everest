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
 * Date: 10-04-2012
 */
package ca.marc.everest.datatypes.generic;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.*;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IEncapsulatedData;
import ca.marc.everest.datatypes.interfaces.IOriginalText;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Specifies a periodic interval of time centered around some event. 
 */
@Structure(name = "EIVL", structureType = StructureType.DATATYPE, defaultTemplateType = TS.class)
public class EIVL<T extends IAny> extends SXCM<T> implements IOriginalText {

	// backing field for event
	private CS<DomainTimingEvent> m_event;
	// backing field for offset
	private IVL<PQ> m_offset;
	// backing field for original text
	private ED m_originalText;
	
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
		this.m_event = new CS<DomainTimingEvent>(event);
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
	 * Get the event around which this EIVL is bound
	 */
	@Property(name = "event", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public CS<DomainTimingEvent> getEvent() { return this.m_event; }
	
	/**
	 * Set the event around which this EIVL is bound
	 */
	public void setEvent(DomainTimingEvent value) { this.m_event = new CS<DomainTimingEvent>(value); }
	
	/**
	 * Set the event around which this EIVL is bound
	 */
	public void setEvent(CS<DomainTimingEvent> value) { this.m_event = value; }

	/**
	 * Gets the offset that specifies how long after (or before) the bound event the 
	 * interval begins
	 */
	@Property(name = "offset", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public IVL<PQ> getOffset() { return this.m_offset; }
	/**
	 * Sets the offset that specifies how long after (or before) the bound event the
	 * interval begins
	 */
	public void setOffset(IVL<PQ> value) { this.m_offset = value; }
	
	/**
	 * Gets a value which specifies the reasoning behind the selection of this particular representation of the EIVL
	 */
	@Property(name = "originalText", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public ED getOriginalText() {
		return this.m_originalText;
	}
	/**
	 * Sets a value which specifies the reasoning behind the selection of this particular representation of the EIVL
	 */
	@Override
	public void setOriginalText(IEncapsulatedData value) {
		this.m_originalText = (ED)value;
	}
	/* (non-Javadoc)
	 * @see ca.marc.everest.datatypes.generic.SXCM#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		List<IResultDetail> retVal = new ArrayList<IResultDetail>(super.validateEx());
        if (this.m_offset != null)
        {
            if (this.m_offset.getLow() != null && !this.m_offset.getLow().isNull() && !PQ.isValidTimeFlavor(this.m_offset.getLow()))
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "EIVL", "When populated, the Offset.Low property must contain a valid PQ.TIME instance", null));
            if (this.m_offset.getHigh() != null && !this.m_offset.getHigh().isNull() && !PQ.isValidTimeFlavor(this.m_offset.getHigh()))
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "EIVL", "When populated, the Offset.High property must contain a valid PQ.TIME instance", null));
            if (this.m_offset.getWidth() != null && !this.m_offset.getWidth().isNull() && !PQ.isValidTimeFlavor(this.m_offset.getWidth()))
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "EIVL", "When populated, the Offset.Width property must contain a valid PQ.TIME instance", null));
            if (this.m_offset.getValue() != null && !this.m_offset.getValue().isNull() && !PQ.isValidTimeFlavor(this.m_offset.getValue()))
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "EIVL", "When populated, the Offset.Value property must contain a valid PQ.TIME instance", null));
        }
        
        if (!((this.m_event.getCode().isChildConcept(DomainTimingEvent.AfterMeal) || 
        		this.m_event.getCode().isChildConcept(DomainTimingEvent.BetweenMeals) || 
        		this.m_event.getCode().isChildConcept(DomainTimingEvent.BeforeMeal)) ^ (this.m_offset != null)))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "EIVL", "When the Event property implies before, after or between meals the Offset property must not be populated", null));
        return retVal;
        
	}
	/**
	 * @see ca.marc.everest.datatypes.generic.PDV#validate()
	 */
	@Override
	public boolean validate() {
		boolean valid = this.isNull() ^ (this.m_offset != null || this.m_event != null);

        if (this.m_offset != null)
        {
            valid &= this.m_offset.getLow() == null || this.m_offset.getLow().isNull() || PQ.isValidTimeFlavor(this.m_offset.getLow());
            valid &= this.m_offset.getHigh() == null || this.m_offset.getHigh().isNull() || PQ.isValidTimeFlavor(this.m_offset.getHigh());
            valid &= this.m_offset.getWidth() == null || this.m_offset.getWidth().isNull() || PQ.isValidTimeFlavor(this.m_offset.getWidth());
            valid &= this.m_offset.getValue() == null || this.m_offset.getValue().isNull() || PQ.isValidTimeFlavor(this.m_offset.getValue());
        }
        valid &= (this.m_event.getCode().isChildConcept(DomainTimingEvent.AfterMeal) || 
        		this.m_event.getCode().isChildConcept(DomainTimingEvent.BetweenMeals) || 
        		this.m_event.getCode().isChildConcept(DomainTimingEvent.BeforeMeal)) ^ (this.m_offset != null);

        return valid;
	}
	/**
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_event == null) ? 0 : m_event.hashCode());
		result = prime * result
				+ ((m_offset == null) ? 0 : m_offset.hashCode());
		result = prime * result
				+ ((m_originalText == null) ? 0 : m_originalText.hashCode());
		return result;
	}
	/**
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		EIVL other = (EIVL) obj;
		if (m_event != other.m_event)
			return false;
		if (m_offset == null) {
			if (other.m_offset != null)
				return false;
		} else if (!m_offset.equals(other.m_offset))
			return false;
		if (m_originalText == null) {
			if (other.m_originalText != null)
				return false;
		} else if (!m_originalText.equals(other.m_originalText))
			return false;
		return true;
	}
	/**
	 * @see ca.marc.everest.datatypes.ANY#semanticEquals(ca.marc.everest.datatypes.interfaces.IAny)
	 */
	@Override
	public BL semanticEquals(IAny other) {
		BL baseEq = super.semanticEquals(other);
        if (!baseEq.toBoolean())
            return baseEq;

        // Null-flavored
        if (this.isNull() && other.isNull())
            return BL.TRUE;
        else if (this.isNull() ^ other.isNull())
            return BL.FALSE;

        EIVL<T> eivlOther = (EIVL<T>)other;
        if (eivlOther == null)
            return BL.FALSE;
        else
            return BL.fromBoolean(
                (eivlOther.m_event == null ? this.m_event == null : eivlOther.m_event.semanticEquals(this.m_event).toBoolean())
                &&
                (eivlOther.m_offset == null ? this.m_offset == null : eivlOther.m_offset.semanticEquals(this.m_offset).toBoolean())
                );
	}

	
	
	
}
