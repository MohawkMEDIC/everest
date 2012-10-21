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
 * Date: 10-19-2012
 */
package ca.marc.everest.datatypes;

import java.math.BigDecimal;
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.PDV;
import ca.marc.everest.datatypes.generic.QTY;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Represents a quantity expressing an amount of money in some currency.
 */
@Structure(name = "MO", structureType = StructureType.DATATYPE)
public class MO extends QTY<BigDecimal> implements Comparable<PDV<BigDecimal>> {

	private int m_precision = 0;
	private String m_currency;
	
	/**
	 * Creates a new instance of the MO class
	 */
	public MO() { super(); }
	/**
	 * Creates a new instance of the MO class with the specified value
	 */
	public MO(BigDecimal value) { super(value); }
	/**
	 * Creates a new instance of the MO class with the specfied value
	 */
	public MO(BigDecimal value, String currency) { this(value); this.m_currency = currency; }
	/**
	 * Gets a number of significant digits of the decimal representation
	 */
	@Property(name = "precision", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public int getPrecision() {
		return this.m_precision;
	}
	/**
	 * Sets the number of significant digits of the decimal representation
	 */
	public void setPrecision(int value) {
		this.m_precision = value;
	}
	/**
	 * Gets the currency that this amount is represented. ISO 4217 currency codes should be used
	 */
	@Property(name = "currency", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public String getCurrency() {
		return this.m_currency;
	}
	/**
	 * Sets the  ISO 4217 currency code representing the currency of the value
	 */
	public void setCurrency(String value) {
		this.m_currency = value;
	}
	/** 
	 * @see cthis.marc.everest.datatypes.interfaces.IQuantity#toInteger()
	 */
	@Override
	public Integer toInteger() {
		return this.getValue().intValue();
	}
	/**
	 * @see cthis.marc.everest.datatypes.interfaces.IQuantity#toDouble()
	 */
	@Override
	public Double toDouble() {
		return this.getValue().doubleValue();
	}
	/**
	 * @see javthis.lang.Comparable#compareTo(javthis.lang.Object)
	 */
	@Override
	public int compareTo(PDV<BigDecimal> o) {
		
		if(o instanceof MO)
		{
			MO other = (MO)o;
			if (other == null || other.isNull())
	            return 1;
	        else if (this.isNull() && !other.isNull())
	            return -1;
	        else if (this.getValue() != null && other.getValue() == null)
	            return 1;
	        else if (other.getValue() != null && this.getValue() == null)
	            return -1;
	        else if (other.getValue() == null && this.getValue() == null)
	            return 0;
	        else if (!this.m_currency.equals(other.m_currency))
	            throw new UnsupportedOperationException("Currencies must match to compare MO");
	        else
	            return this.getValue().compareTo(other.getValue());
		}
		else
			return super.compareTo(o);
	}
	/**
	 * @see cthis.marc.everest.datatypes.generic.QTY#validate()
	 */
	@Override
	public boolean validate() {
		return ((this.getValue() != null || this.getUncertainRange() != null) ^ this.isNull()) &&
                (((this.getValue() != null || this.getUncertainRange() != null) && this.m_currency != null) || (this.getValue() == null && this.getUncertainRange() == null)) &&
                ((this.getValue() != null) ^ (this.getUncertainRange() != null) || (this.getValue() == null && this.getUncertainRange() == null));
	}
	/**
	 * @see cthis.marc.everest.datatypes.generic.PDV#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
        List<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();
        if(this.isNull() && (this.getValue() != null || this.getUncertainRange() != null))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "MO", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        if (this.getValue() == null && !this.isNull() && this.getUncertainRange() == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "MO", EverestValidationMessages.MSG_NULLFLAVOR_MISSING, null));
        if (!((this.getValue() != null) ^ (this.getUncertainRange() != null)))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "MO", String.format(EverestValidationMessages.MSG_INDEPENDENT_VALUE, "UncertainRange", "Value")));
        if (!this.isNull() && this.m_currency == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "MO", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Value or UncertainRange", "Currency"), null));
        return retVal;

	}
	/**
	 * @see cthis.marc.everest.datatypes.ANY#semanticEquals(cthis.marc.everest.datatypes.interfaces.IAny)
	 */
	@Override
	public BL semanticEquals(IAny other) {
		BL baseSem = super.semanticEquals(other);
        if (!baseSem.toBoolean())
            return baseSem;
        MO otherMo = (MO)other;
        if ((this.getValue() == null && otherMo.getValue() == null || this.getValue().equals(otherMo.getValue())) &&
                       (this.m_currency == null && otherMo.m_currency == null || this.m_currency.equals(otherMo.m_currency)))
            return BL.TRUE;
        else if (this.getUncertainRange() != null && otherMo.getUncertainRange() != null &&
                       this.getUncertainRange().equals(otherMo.getUncertainRange()))
            return BL.TRUE;
        else
            return BL.FALSE;
	}
	/**
	 * @see javthis.lang.Object#toString()
	 */
	@Override
	public String toString() {
		return String.format("%1%2%3", this.getValue(), this.getValue() != null && this.m_currency != null ? " " : "", this.m_currency);
	}
	
	/**
	 * Negate this MO
	 * @return
	 */
	public MO negate()
	{
		MO retVal = new MO();
        if (this.isNull())
            retVal.setNullFlavor(this.getNullFlavor());
        else if (this.getValue() == null)
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else
        {
            retVal.setValue(this.getValue());
            retVal.setCurrency(this.getCurrency());
        }
        return retVal;
	}
	
	/**
	 * Subtract this MO with another
	 */
	public MO subtract(MO other)
	{
		MO retVal = new MO();
		if (other == null)
            return null;
        else if (this.isNull() || other.isNull() || this.getValue() == null || other.getValue() == null)
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else if (!this.getCurrency().equals(other.getCurrency()))
            throw new UnsupportedOperationException("Both amounts must have the same currency for this operation");
        else
        {
        	retVal.setValue(this.getValue().subtract(other.getValue()));
        	retVal.setCurrency(this.getCurrency());
        }
		return retVal;
	}
	
	/**
	 * Add this MO with another
	 */
	public MO add(MO other)
	{
		MO retVal = new MO();
		if (other == null)
            return null;
        else if (this.isNull() || other.isNull() || this.getValue() == null || other.getValue() == null)
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else if (!this.getCurrency().equals(other.getCurrency()))
            throw new UnsupportedOperationException("Both amounts must have the same currency for this operation");
        else
        {
        	retVal.setValue(this.getValue().add(other.getValue()));
            retVal.setCurrency(this.getCurrency());
        }
		return retVal;
	}
	
	/**
	 * Multiply two MO instances together
	 */
	public MO multiply(MO other)
	{
		MO retVal = new MO();
		if (other == null)
            return null;
        else if (this.isNull() || other.isNull() || this.getValue() == null || other.getValue() == null)
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else if (!this.getCurrency().equals(other.getCurrency()))
            throw new UnsupportedOperationException("Both amounts must be in he same currency for this operation");
        else
        {
        	retVal.setValue(this.getValue().multiply(other.getValue()));
        	retVal.setCurrency(this.getCurrency());
        }
		return retVal;
	}
	
	/**
	 * Multiply this MO with a REAL number
	 */
	public MO multiply(REAL other)
	{
		MO retVal = new MO();
		if (other == null)
            return null;
        else if (this.isNull() || other.isNull() || this.getValue() == null || other.getValue() == null)
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else
        {
        	retVal.setValue(BigDecimal.valueOf(this.getValue().doubleValue() * other.getValue()));
        	retVal.setCurrency(this.getCurrency());
        }
        return retVal;
	}

	/**
	 * Divide this MO by another
	 */
	public MO divide(MO other)
	{
		MO retVal = new MO();
		if (other == null)
            return null;
        else if (this.isNull() || other.isNull() || this.getValue() == null || other.getValue() == null)
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else if (!this.getCurrency().equals(other.getCurrency()))
            throw new UnsupportedOperationException("Both amounts must be in the same currency for this operation");
        else if (other.getValue().equals(0))
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else
        {
        	retVal.setValue(this.getValue().divide(other.getValue()));
            retVal.setCurrency(this.getCurrency());
        }
		return retVal;
	}
	
	/**
	 * Divide this MO by a REAL instance
	 */
	public MO divide(REAL other)
	{
		MO retVal = new MO();
		if (other == null)
            return null;
        else if (this.isNull() || other.isNull() || this.getValue() == null || other.getValue() == null)
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else if (other.getValue() == 0)
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else
        {
        	retVal.setValue(BigDecimal.valueOf(this.getValue().doubleValue() / other.getValue()));
        	retVal.setCurrency(this.getCurrency());
        }
		return retVal;
	}
	/**
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_currency == null) ? 0 : m_currency.hashCode());
		result = prime * result + m_precision;
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
		MO other = (MO) obj;
		if (m_currency == null) {
			if (other.m_currency != null)
				return false;
		} else if (!m_currency.equals(other.m_currency))
			return false;
		if (m_precision != other.m_precision)
			return false;
		return true;
	}
	
}
