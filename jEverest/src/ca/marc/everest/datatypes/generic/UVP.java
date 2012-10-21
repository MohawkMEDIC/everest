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
 * Date: 10-21-2012
 */
package ca.marc.everest.datatypes.generic;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.ANY;
import ca.marc.everest.datatypes.BL;
import ca.marc.everest.datatypes.EverestValidationMessages;
import ca.marc.everest.datatypes.NullFlavor;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IProbability;
import ca.marc.everest.datatypes.interfaces.IQuantity;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * A generic datatype extension used to specify the probability (percentage from 0 to 1) of the value
 */
@Structure(name = "UVP", structureType = StructureType.DATATYPE, defaultTemplateType = IQuantity.class)
public class UVP<T extends IAny> extends ANY implements IProbability<T> {

	// backing field for probability
	private Float m_probability = null;
	// backing field for value
	private T m_value = null;
	
	/**
	 * Creates a new instance of the UVP class
	 */
	public UVP() { super(); }
	/**
	 * Creates a new instance of the UVP datatype with the specified value and probability 
	 * @param value The value the UVP instance shall carry
	 * @param probability The probability that the value is accurate expressed as a decimal between 0 and 1
	 */
	public UVP(T value, Float probability) {
		super();
		this.m_value = value;
		this.m_probability = probability;
	}
	
	/**
	 * Gets the value representing the probability
	 */
	@Property(name = "probability", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.MANDATORY)
	@Override
	public Float getProbability() {
		return this.m_probability;
	}
	/**
	 * Sets the value representing the probability
	 */
	@Override
	public void setProbability(Float value) {
		this.m_probability = value;
	}
	
	/**
	 * Gets the value of the UVP
	 */
	@Property(name = "value", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.MANDATORY)
	@Override
	public T getValue() {
		// TODO Auto-generated method stub
		return this.m_value;
	}
	/**
	 * Sets the value of the UVP
	 */
	@Override
	public void setValue(T value) {
		this.m_value = value;
	}

	/**
	 * Validate the UVP
	 */
	@Override
	public boolean validate() {
		return this.isNull() ^ ( this.getValue() != null && this.getProbability() != null && (this.getProbability() >= 0 && this.getProbability() <= 1));
	}
	/**
	 * Extended validation routine returning errors with datatype validation
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		List<IResultDetail> retVal = new ArrayList<IResultDetail>();
        if (this.isNull() && (this.getValue() != null || this.getProbability() != null))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "UVP", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        else if (!this.isNull())
        {
            if (this.getValue() == null && !(this instanceof URG<?>))
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "UVP", String.format(EverestValidationMessages.MSG_PROPERTY_NOT_POPULATED, "Value", "T"), null));
            if (this.getProbability() == null)
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "UVP", String.format(EverestValidationMessages.MSG_PROPERTY_NOT_POPULATED, "Probability", "Decimal"), null));
            else if (this.getProbability() < 0 || this.getProbability() > 1)
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "UVP", "Probabiliy must carry a value between 0 and 1", null));
        }
        return retVal;
	}
	/**
	 * Determine semantic equality between this and another UVP 
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

        // Values are equal?
        UVP<T> uvOther = (UVP<T>)other;
        return BL.fromBoolean((this.getValue() == null ? uvOther.getValue() == null : this.getValue().equals(uvOther.getValue())) &&
            (this.getProbability() == null ? uvOther.getProbability() == null : this.getProbability().equals(uvOther.getProbability())));
	}
	/**
	 * Represents the UVP as a string
	 */
	@Override
	public String toString() {
		// TODO Auto-generated method stub
		return this.getValue() == null || this.getProbability() == null ? super.toString() : String.format("~%1 (%2$.2f %%)", this.getValue(), this.getProbability() * 100.0);
	}
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_probability == null) ? 0 : m_probability.hashCode());
		return result;
	}
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		UVP other = (UVP) obj;
		if (m_probability == null) {
			if (other.m_probability != null)
				return false;
		} else if (!m_probability.equals(other.m_probability))
			return false;
		return true;
	}

	
	
	
	
}
