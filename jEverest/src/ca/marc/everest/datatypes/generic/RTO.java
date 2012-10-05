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
import ca.marc.everest.datatypes.BL;
import ca.marc.everest.datatypes.EverestValidationMessages;
import ca.marc.everest.datatypes.NullFlavor;
import ca.marc.everest.datatypes.REAL;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IQuantity;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * A quantity constructed as the quotient of a numerator quantity divided by the denominator quantity
 * @param <S> The type which the numerator will have
 * @param <T> The type which the denominator will have
 */
@Structure(name = "RTO", structureType = StructureType.DATATYPE, defaultTemplateType = IQuantity.class)
public class RTO<S extends IQuantity & IAny, T extends IQuantity & IAny> extends QTY<Double> {

	// Backing field for numerator
	private S m_numerator; 
	// Backing field for denominator
	private T m_denominator;
	
	/**
	 * Creates a new instance of the RTO class
	 */
	public RTO() { super(); }
	/**
	 * Creates a new instance of the RTO class with the specified numerator and denominator
	 */
	public RTO(S numerator, T denominator) { 
		super();
		this.m_denominator = denominator;
		this.m_numerator = numerator;
	}

	/**
	 * Get the value of the numerator
	 */
	@Property(name = "numerator", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.NONSTRUCTURAL)
	public S getNumerator() {
		return this.m_numerator;
	}
	
	/**
	 * Sets the value of the numerator
	 */
	public void setNumerator(S value) {
		this.m_numerator = value;
	}
	
	/**
	 * Gets the value of the denominator
	 */
	@Property(name = "denominator", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public T getDenominator() {
		return this.m_denominator;
	}
	/**
	 * Sets the value of the denominator
	 */
	public void setDenominator(T value) {
		this.m_denominator = value;
	}
	@Override
	public Integer toInteger() {
		return this.toDouble().intValue();
	}

	/**
	 * Represent this object as a double
	 */
	@Override
	public Double toDouble() {
		// TODO Auto-generated method stub
		if (this.m_numerator == null || this.m_numerator.isNull())
            throw new UnsupportedOperationException("Numerator must have a value to perform this operation");
        else if (this.m_denominator == null || this.m_denominator.isNull())
            throw new UnsupportedOperationException("Denominator must have a value to perform this operation");
        return this.m_numerator.toDouble() / this.m_denominator.toDouble();
	}
	/** (non-Javadoc)
	 * @see ca.marc.everest.datatypes.generic.QTY#validate()
	 */
	@Override
	public boolean validate() {
		return this.isNull() ^ (this.m_denominator != null || this.m_numerator != null) && 
                ((this.m_numerator != null && this.m_denominator != null && this.m_denominator.validate() && this.m_numerator.validate()) ||
                (this.m_numerator == null && this.m_denominator == null)) &&
                 this.getUncertainRange() == null;
	}
	/** (non-Javadoc)
	 * @see ca.marc.everest.datatypes.generic.PDV#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
        // Cannot use base to validate as Value is not permitted
        List<IResultDetail> retVal = new ArrayList<IResultDetail>();
        if (this.isNull() && (this.m_numerator != null || this.m_denominator != null))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "RTO", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        else if (!this.isNull() && this.m_numerator == null && this.m_denominator == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "RTO", EverestValidationMessages.MSG_NULLFLAVOR_MISSING, null));
        if (this.m_numerator != null && this.m_denominator == null || this.m_denominator != null && this.m_numerator == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "RTO", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Numerator", "Denominator"), null));
        if (this.getUncertainRange() != null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "RTO", String.format(EverestValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "UncertainRange"), null));
        if (this.getUncertainty() != null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.WARNING, "RTO", String.format(EverestValidationMessages.MSG_PROPERTY_SCHEMA_ONLY, "Uncertainty"), null));
        if (this.getUncertaintyType() != null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.WARNING, "RTO", String.format(EverestValidationMessages.MSG_PROPERTY_SCHEMA_ONLY, "UncertaintyType"), null));
        return retVal;
	}
	/**
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		if(this.m_numerator != null && this.m_denominator != null)
			return String.format("%1/%2", this.m_numerator.toString(), this.m_denominator.toString());
		return "";
	}
	/** (non-Javadoc)
	 * @see ca.marc.everest.datatypes.ANY#semanticEquals(ca.marc.everest.datatypes.interfaces.IAny)
	 */
	@SuppressWarnings("unchecked")
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
        RTO<S,T> rtoOther = (RTO<S,T>)other;
        if (rtoOther.m_numerator != null && rtoOther.m_denominator != null && this.m_numerator != null && this.m_denominator != null)
            return rtoOther.m_numerator.semanticEquals(this.m_numerator).and(rtoOther.m_denominator.semanticEquals(this.m_denominator));
        return BL.FALSE;

	}
	
	/**
	 * Represents the RTO as a REAL
	 */
	public REAL toReal()
	{
        REAL retVal = new REAL();
		if (this.m_numerator == null || this.m_denominator == null)
            return null;
        else if (this.m_numerator.isNull() || this.m_denominator.isNull())
        	retVal.setNullFlavor(NullFlavor.NoInformation);
        else
            retVal.setValue(this.m_numerator.toDouble() / this.m_denominator.toDouble());
		return retVal;
	}
	

}
