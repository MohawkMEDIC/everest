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
 * Date: 08-16-2011
 */
package ca.marc.everest.datatypes;

import java.math.BigDecimal;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.*;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IImplicitInterval;
import ca.marc.everest.datatypes.interfaces.IInterval;
import ca.marc.everest.datatypes.interfaces.IRealValue;

/**
 * Represent fractional numbers.
 * 
 * Typically used whenever quantities are measured with real numbers.
 */
@Structure(name = "REAL", structureType = StructureType.DATATYPE)
public class REAL extends QTY<Double> implements IRealValue<Double>, IImplicitInterval<REAL>  {

	// Precision of the float
	private int m_precision = 0;
	
	/**
	 * Creates a new instance of the REAL class
	 */
	public REAL() {	}
	/**
	 * Creates a new instance of the REAL class with the specified value
	 */
	public REAL(Double value) {	super(value); }

	/**
	 * Gets the precision of this REAL value
	 */
	@Property(name = "precision", conformance=ConformanceType.REQUIRED, propertyType = PropertyType.STRUCTURAL)
	public int getPrecision() { return this.m_precision; }
	/**
	 * Sets the precision of this real value to the specified value.
	 * 
	 * Note that this will have an affect on the manner in which values are compared. For example,
	 * consider the following scenario:
	 * 
	 * REAL r1 = new REAL(0.333333333333333333333),
	 * 	r2 = new REAL(1/3.0f);
	 * 
	 * r1.equals(r2); // Returns false as r2 is stored as 0.333333333333333332
	 * r2.setPrecision(10);
	 * r1.equals(r2); // Returns true as equality is checked to precision
	 * @param value
	 */
	public void setPrecision(int value)
	{
		this.m_precision = value;
		if(this.m_precision == 0)
			this.p_floatingPointEqualityTolerance = 1e-15;
		else
			this.p_floatingPointEqualityTolerance = 1 / Math.pow(10, this.m_precision);
	}
	
	/**
	 * Calculate hashcode
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + m_precision;
		return result;
	}
	/**
	 * Determine value equality
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		REAL other = (REAL) obj;
		if (m_precision != other.m_precision)
			return false;
		return true;
	}
	
	/**
	 * Parse a REAL instance from a string
	 */
	public static REAL fromString(String s)
	{
		try
		{		
			REAL retVal = new REAL(Double.parseDouble(s));
			int precision = 0;
			if (s.contains("."))
				precision = s.length() - s.indexOf(".") - 1;
			retVal.setPrecision(precision);
			return retVal;
		}
		catch(NumberFormatException e)
		{
			REAL retVal = new REAL();
			retVal.setNullFlavor(NullFlavor.Other);
			return retVal;
		}
	}

	/**
	 * Get the maximum of this integer an another
	 */
	public REAL max(REAL other) throws CloneNotSupportedException {
		
		if(other == null || other.isNull())
			return (REAL)this.clone();
		else if(this.isNull())
			return (REAL)other.clone();
		else if(other.getValue() > this.getValue())
			return new REAL(other.getValue());
		else
			return new REAL(this.getValue()); 
	}
	
	/**
	 * Gets the minimum value of this integer and another
	 */
	public REAL min(REAL other) throws CloneNotSupportedException {

		if(other == null || other.isNull())
			return (REAL)this.clone();
		else if(this.isNull())
			return (REAL)other.clone();
		else if(other.getValue() < this.getValue())
			return new REAL(other.getValue());
		else 
			return new REAL(this.getValue());
	}
	
	/**
	 * Adds the value of this REAL with another integer
	 */
	public REAL add(INT other) {
		
		REAL retVal = new REAL();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() + other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
	}
	
	/**
	 * Adds the value of this REAL with another REAL
	 */
	public REAL add(REAL other)	{
		
		REAL retVal = new REAL();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() + other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
		
	}
	
	/**
	 * Subract the value of another integer from this REAL
	 */
	public REAL subtract(INT other)	{
		
		REAL retVal = new REAL();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() - other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
	}
	
	/**
	 * Subtract the value of another REAL from this REAL
	 */
	public REAL subtract(REAL other) {
		
		REAL retVal = new REAL();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() - other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
	}
	
	/**
	 * Divide this REAL number by an integer number 
	 */
	public REAL divide(INT other) {

		REAL retVal = new REAL();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull() || other.getValue() == 0)
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() / other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
		
	}
	
	/**
	 * Perform floating point division between this REAL and the specified REAL value
	 */
	public REAL divide(REAL other) {
		
		REAL retVal = new REAL();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull() || other.getValue() == 0)
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() / other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
	}
	
	/**
	 * Perform floating point division between this REAL and an instance of MO
	 */
	public MO divide(MO other)
	{
		MO retVal = new MO();
		
		if (other == null)
            return null;
        else if (this.isNull() || other.isNull() || other.getValue().equals(0))
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else
        {
            retVal.setValue(BigDecimal.valueOf(this.getValue() / other.getValue().doubleValue()));
            retVal.setCurrency(other.getCurrency());
        }
		return retVal;
	}
	
	/**
	 * Multiply this REAL by the specified integer
	 */
	public REAL multiply(INT other) {
		
		REAL retVal = new REAL();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() * other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
		
	}
	
	/**
	 * Multiply this REAL by the specified REAL
	 */
	public REAL multiply(REAL other) {
		
		REAL retVal = new REAL();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() * other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
		
	}

	/**
	 * Multiply this REAL by the specified MO
	 */
	public MO multiply(MO other) {
		
		MO retVal = new MO();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
		{
			retVal.setValue(BigDecimal.valueOf(this.getValue() * other.getValue().doubleValue()));
			retVal.setCurrency(other.getCurrency());
		}
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
		
	}
	
	/**
	 * Converts an INT instance to a REAL instance
	 */
	public static REAL fromInt(INT i)
	{
		return new REAL(i.getValue().doubleValue());
	}
	
	/**
	 * Converts this REAL to an INT
	 */
	public INT toInt()
	{
		return new INT(this.getValue().intValue());
	}
	/**
	 * Represent as an integer
	 */
	@Override
	public Integer toInteger() {
		return this.getValue().intValue();
	}
	/**
	 * Represent as double
	 */
	@Override
	public Double toDouble() {
		return this.getValue();
	}
    /**
	* Represents the REAL as an interval range represented by the precision
	* <p>
	* This function requires the <see cref="P:Precision"/> property to be set 
	* in order to be of any use. When calling with the precision it will 
	* return the appropriate Interval
	* </p>
	* REAL oneThird = new REAL(1.0f).divide(new REAL(3.0f));
	* oneThird.setPrecision(3);
	* IVL&lt;REAL> ivl = oneThird.toIVL();
	* // Output is: 
	* // 1/3 is between 0.333 and 0.333999999999 
	*/
	@Override
	public IInterval<REAL> toIvl() {
        // For example, if we have 43.20399485
        // with a precision of 4, our interval should be 
        // 43.20390000 to 43.20399999
		IVL<REAL> retVal = new IVL<REAL>();
        if (this.isNull() || this.getValue() == null)
            retVal.setNullFlavor(NullFlavor.NoInformation);
        else
        {
	        // First, we multiply the value to get store the number
	        double tValue = this.getValue().doubleValue() * Math.pow(10, this.getPrecision()),
	            min = Math.floor(tValue),
	            max = Math.ceil(tValue) - Math.pow(10, -(14 - this.getPrecision()));
	
	        retVal.setLow(new REAL(min / Math.pow(10, this.getPrecision())));
	        retVal.setHigh(new REAL(max / Math.pow(10, this.getPrecision())));
	        retVal.setLowInclusive(true);
	        retVal.setHighInclusive(true);
        }
        return retVal;
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

        // Values are equal?
        REAL realOther = (REAL)other;
        if (realOther.getValue() != null && this.getValue() != null && Math.abs(realOther.getValue().doubleValue() - this.getValue().doubleValue()) <= Math.abs(realOther.getValue().doubleValue() * this.p_floatingPointEqualityTolerance))
            return BL.TRUE;
        else if (realOther.getUncertainRange() != null && !realOther.getUncertainRange().isNull() &&
            this.getUncertainRange() != null && !this.getUncertainRange().isNull())
            return realOther.getUncertainRange().semanticEquals(this.getUncertainRange());
        return BL.FALSE;
	}
	
	
}
