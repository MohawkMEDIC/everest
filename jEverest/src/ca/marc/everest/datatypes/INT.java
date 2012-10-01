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
 * Date: 08-02-2011
 */

package ca.marc.everest.datatypes;

import ca.marc.everest.datatypes.generic.*;
import ca.marc.everest.annotations.*;

/**
 * Integer numbers are precise numbers that are results of counting and enumerating
 */
@Structure(name = "INT", structureType = StructureType.DATATYPE)
public class INT extends QTY<Integer> {

	/**
	 * Creates a new instance of the INT class
	 */
	public INT() {}
	/**
	 * Creates a new instance of the INT class with the specified value
	 */
	public INT(Integer value) { super(value); }
	
	/**
	 * Validates that the specified instance meets the validation criteria of POS
	 */
	@Flavor(name = "INT.POS")
	public static Boolean pos(INT i)
	{
		return i.getValue() != null && i.getValue() > 0;
	}
	
	/**
	 * Validates that the specified instance meets the validation criteria of nonneg
	 */
	@Flavor(name = "INT.NONNEG")
	public static Boolean nonNeg(INT i)
	{
		return i.getValue() != null && i.getValue() >= 0;
	}
	
	/**
	 * Casts this INT as a real 
	 */
	public REAL toReal()
	{
		REAL retVal = new REAL();
		if(this.getValue() != null)
			retVal.setValue((double)this.getValue()); // This is a performance hit, but there is no way around it in the jaba land
		else
			retVal.setNullFlavor(this.getNullFlavor());
		return retVal;
	}
	
	/**
	 * Casts the specified REAL to an integer
	 */
	public static INT fromReal(REAL r)
	{
		INT retVal = new INT();
		if(r.getValue() != null)
			retVal.setValue(r.getValue().intValue());
		else
			retVal.setNullFlavor(r.getNullFlavor());
		
		return retVal;
	}
	
	/**
	 * Get the maximum of this integer an another
	 */
	public INT max(INT other) {
		try
		{
			if(other == null || other.isNull())
				return (INT)this.clone();
			else if(this.isNull())
				return (INT)other.clone();
			else if(other.getValue() > this.getValue())
				return new INT(other.getValue());
			else
				return new INT(this.getValue());
		}
		catch(CloneNotSupportedException e)
		{
			INT in = new INT();
			in.setNullFlavor(NullFlavor.NoInformation);
			return in;
		}
	}
	
	/**
	 * Gets the minimum value of this integer and another
	 */
	public INT min(INT other) { 

		try
		{
			if(other == null || other.isNull())
				return (INT)this.clone();
			else if(this.isNull())
				return (INT)other.clone();
			else if(other.getValue() < this.getValue())
				return new INT(other.getValue());
			else 
				return new INT(this.getValue());
		}
		catch(CloneNotSupportedException e)
		{
			INT in = new INT();
			in.setNullFlavor(NullFlavor.NoInformation);
			return in;
		}
	}
	
	/**
	 * Adds the value of this integer with another
	 */
	public INT add(INT other) {
		
		INT retVal = new INT();
		
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
	 * Adds the value of this integer with a real
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
	 * Subract the value of another integer from this integer
	 */
	public INT subtract(INT other)	{
		
		INT retVal = new INT();
		
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
	 * Subtract the value of another REAL from this integer
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
	 * Integer divide another number into this number 
	 */
	public INT divide(INT other) {

		INT retVal = new INT();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() / other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
		
	}
	
	/**
	 * Perform floating point division between this integer and the specified REAL value
	 */
	public REAL divide(REAL other) {
		
		REAL retVal = new REAL();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() / other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
	}
	
	/**
	 * Multiply this number by the specified integer
	 */
	public INT multiply(INT other) {
		
		INT retVal = new INT();
		
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
	 * Multiply this number by the specified REAL
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
	 * Perform a modulus 
	 */
	public INT mod(INT other) {
		
		INT retVal = new INT();
		
		if(other == null)
			return null; // This differs from standard Java Integer + null as no boxing is performed
		else if(this.isNull() || other.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else if(other.getValue() != null && this.getValue() != null)
			retVal.setValue(this.getValue() % other.getValue());
		else 
			retVal.setNullFlavor(NullFlavor.Other);
		return retVal;
		
	}
	
	/**
	 * Increment this integer and return a new integer with the incremented value
	 */
	public INT increment() {

		return this.add(new INT(1));
		
	}
	
	/**
	 * Decrement this integer and return a new integer with the decremented value
	 */
	public INT decrement() {

		return this.subtract(new INT(1));

	}
	
	
}
