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

import java.awt.Event;
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.BL;
import ca.marc.everest.datatypes.ED;
import ca.marc.everest.datatypes.EverestValidationMessages;
import ca.marc.everest.datatypes.NullFlavor;
import ca.marc.everest.datatypes.PQ;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IEncapsulatedData;
import ca.marc.everest.datatypes.interfaces.IInterval;
import ca.marc.everest.datatypes.interfaces.IQuantity;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * A union of the UVP and IVL datatypes. 
 * 
 * @see ca.marc.everest.datatypes.generic.IVL
 * @see ca.marc.everest.datatypes.generic.UVP
 */
@Structure(name = "URG", structureType = StructureType.DATATYPE, defaultTemplateType = IQuantity.class)
public class URG<T extends IAny> extends UVP<T> implements IInterval<T> {

	// backing field for low inclusive attribute
	private Boolean m_lowInclusive = null;
	// backing field for high inclusive
	private Boolean m_highInclusive = null;
	// backing field for the width
	private PQ m_width;
	// backing field for the lower bound of the set
	private T m_low;
	// backing field for the upper bound of the set
	private T m_high;
	// backing field for original text
	private ED m_originalText;
	
	/**
	 * Gets the original text indicating where the interval was derived
	 */
	@Property(name = "originalText", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public ED getOriginalText() { return this.m_originalText; }
	/**
	 * Sets the original text indicating where the interval was derived
	 */
	public void setOriginalText(ED value) { this.m_originalText = value; }
	/**
	 * Sets the original text indicating where the interval was derived
	 */
	@Override
	public void setOriginalText(IEncapsulatedData value) { this.m_originalText = (ED)value; }
	/**
	 * Gets the lower bound of the interval
	 */
	@Property(name = "low", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public T getLow() { return this.m_low; }
	/**
	 * Sets the lower bound of the interval
	 */
	@Override
	public void setLow(T value) { this.m_low = value; }
	/**
	 * Gets a flag indicating if the lower bound of the interval is inclusive
	 */
	@Property(name = "lowClosed", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public Boolean getLowInclusive() {  return this.m_lowInclusive; }
	/**
	 * Sets a flag indicating if the lower bound of the interval is inclusive
	 */
	@Override
	public void setLowInclusive(Boolean value) { this.m_lowInclusive = value; }
	/**
	 * Get the upper bound of the interval
	 */
	@Property(name = "high", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public T getHigh() { return this.m_high; }
	/**
	 * Set the upper bound of the interval
	 */
	@Override
	public void setHigh(T value) { this.m_high = value;  }
	/**
	 * Gets a flag indicating if the upper bound of the interval is inclusive
	 */
	@Property(name = "highClosed", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public Boolean getHighInclusive() { return this.m_highInclusive; }
	/**
	 * Sets a flag indicating if the upper bound of the interval is inclusive
	 */
	@Override
	public void setHighInclusive(Boolean value) { this.m_highInclusive = value; }

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
	 * Validate this instance of URG
	 */
	@Override
	public boolean validate() {
		return (this.getNullFlavor() != null) ^ (this.m_low != null || this.m_width != null || this.m_high != null || this.getValue() != null) &&
				(this.m_lowInclusive == null && this.m_low != null || this.m_lowInclusive != null) &&
				(this.m_highInclusive == null && this.m_high != null || this.m_highInclusive != null) &&
				((this.getProbability() != null) ^ this.isNull());
	}
	/**
	 * Extended validation with detected issues
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		List<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();

        // Validation
        if (this.isNull() && (this.m_low != null || this.m_high != null || this.m_width != null || this.getValue() != null))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "URG", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        if (this.m_lowInclusive != null && this.m_low == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "URG", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "LowInclusive", "Low"), null));
        if (this.m_highInclusive != null && this.m_high == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "URG", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "HighInclusive", "High"), null));

        return retVal;
	}
	/**
	 * Determine semantic equality between this instance of URG and another
	 */
	@Override
	public BL semanticEquals(IAny other) {
        // Based on set, first, is the other a DSET? 
        if (other instanceof URG<?>)
        {
            URG<T> ivlOther = (URG<T>)other;
            // Parameters to semantic equality
            boolean otherHighInfinite = (ivlOther.getHigh() == null || NullFlavor.PositiveInfinity.equals(ivlOther.getHigh().getNullFlavor().getCode())),
                    thisHighInfinite = (this.getHigh() == null || NullFlavor.PositiveInfinity.equals(this.getHigh().getNullFlavor().getCode())),
                    otherLowInifinite = (ivlOther.getLow() == null || NullFlavor.NegativeInfinity.equals(ivlOther.getLow().getNullFlavor().getCode())),
                    thisLowInfinite = (this.getLow() == null || NullFlavor.NegativeInfinity.equals(this.getLow().getNullFlavor().getCode())),
                    isOtherUnbound = (ivlOther.getHigh() == null || ivlOther.getHigh().isNull()) && !otherHighInfinite ||
                        (ivlOther.getLow() == null || ivlOther.getLow().isNull()) && !otherLowInifinite,
                    isThisUnbound = (this.getHigh() == null || this.getHigh().isNull()) && !thisHighInfinite ||
                        (this.getLow() == null || this.getLow().isNull()) && !thisLowInfinite;

                // Case 1 : Both are bound
                if(!isOtherUnbound && !isThisUnbound)
                    return BL.fromBoolean(((otherHighInfinite && thisHighInfinite) || this.getHigh().semanticEquals(ivlOther.getHigh()).toBoolean()) &&
                        ((otherLowInifinite && thisLowInfinite) || this.getLow().semanticEquals(ivlOther.getLow()).toBoolean()) &&
                    (this.getProbability() == null ? ivlOther.getProbability() == null : this.getProbability().equals(ivlOther.getProbability())));
            return BL.FALSE; // all others are not equal

        }
        return BL.FALSE;
	}
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_high == null) ? 0 : m_high.hashCode());
		result = prime * result
				+ ((m_highInclusive == null) ? 0 : m_highInclusive.hashCode());
		result = prime * result + ((m_low == null) ? 0 : m_low.hashCode());
		result = prime * result
				+ ((m_lowInclusive == null) ? 0 : m_lowInclusive.hashCode());
		result = prime * result
				+ ((m_originalText == null) ? 0 : m_originalText.hashCode());
		result = prime * result + ((m_width == null) ? 0 : m_width.hashCode());
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
		URG other = (URG) obj;
		if (m_high == null) {
			if (other.m_high != null)
				return false;
		} else if (!m_high.equals(other.m_high))
			return false;
		if (m_highInclusive == null) {
			if (other.m_highInclusive != null)
				return false;
		} else if (!m_highInclusive.equals(other.m_highInclusive))
			return false;
		if (m_low == null) {
			if (other.m_low != null)
				return false;
		} else if (!m_low.equals(other.m_low))
			return false;
		if (m_lowInclusive == null) {
			if (other.m_lowInclusive != null)
				return false;
		} else if (!m_lowInclusive.equals(other.m_lowInclusive))
			return false;
		if (m_originalText == null) {
			if (other.m_originalText != null)
				return false;
		} else if (!m_originalText.equals(other.m_originalText))
			return false;
		if (m_width == null) {
			if (other.m_width != null)
				return false;
		} else if (!m_width.equals(other.m_width))
			return false;
		return true;
	}
	

}
