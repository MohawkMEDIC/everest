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
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.annotations.*;

/**
 * A binary value for use in boolean logic
 */
@Structure(name = "BL", structureType = StructureType.DATATYPE)
public class BL extends PDV<Boolean> {

	// True constant
	public static final BL TRUE = BL.fromBoolean(true);
	// False constant
	public static final BL FALSE = BL.fromBoolean(false);
	/**
	 * Creates a new instance of BL
	 */
	public BL() {}
	/**
	 * Creates a new instance of BL
	 */
	public BL(Boolean value) { super(value); }
	/**
	 * Creates a new instance of BL from the given string value
	 * of boolean.
	 * 
	 * "true", "True", "TRUE" or "1" - Results in BL having true value
	 * "false", "False", "FALSE", or "0" - Results in a BL having a false value
	 * any other value results in a BL with nullFlavor of NotApplicable
	 */
	public BL(String value) {
		String lowerValue = value.toLowerCase();
		if(lowerValue.equals("0") || lowerValue.equals("false"))
			this.setValue(false);
		else if(lowerValue.equals("1") || lowerValue.equals("true"))
			this.setValue(true);
		else 
			this.setNullFlavor(NullFlavor.NotApplicable);
	}
	/**
	 * Validator for the BL.NonNull flavor
	 */
	@Flavor(name = "BL.NONNULL")
	public static Boolean isValidNonNullFlavor(BL o)
	{
		return o.getNullFlavor() == null && o.getValue() != null;
	}
	
	/**
	 * Perform a logical negation
	 */
	public BL not() throws CloneNotSupportedException
	{
		BL retVal = (BL)this.clone();
		if(this.getValue() != null)
		{
			boolean value = !this.getValue();
			retVal.setValue(value);
		}
		return retVal;
	}
	
	/**
	 * Perform an logical exclusive or
	 */
	public BL xor(BL other) throws CloneNotSupportedException
	{
		BL retVal = new BL();
		if(other == null)
			retVal.setNullFlavor(new CS<NullFlavor>(NullFlavor.NoInformation));
		else 
		{
			BL xorResult = this.and(other.not()).or(this.not().and(other));
			if(xorResult.getValue() != null)
				return xorResult;
		}
		return retVal;
	}
	
	/**
	 * Performs a logical or using the HL7v3 defined truth tables
	 */
	public BL or(BL other)
	{
		
		// Truth table
		Boolean[][] truth = {
				{ false, true, null },
				{ true, true, true },
				{ null, true, null }
		};
		// lookup
		int tx = this.getValue() == null ? 2 : this.getValue() ? 1 : 0;
		int ty = this.getValue() == null ? 2 : this.getValue() ? 1 : 0;
		
		// new value
		BL retVal = new BL();
		retVal.setValue(truth[tx][ty]);
		if(retVal.getValue() == null) retVal.setNullFlavor(new CS<NullFlavor>(NullFlavor.NoInformation));
		return retVal;
	}
	
	/**
	 * Perform a logical and
	 */
	public BL and(BL other)
	{
		
		// Truth table
		Boolean[][] truth = {
				{ false, false, false },
				{ false, true, null },
				{ false, null, null }
		};
		// lookup
		int tx = this.getValue() == null ? 2 : this.getValue() ? 1 : 0;
		int ty = this.getValue() == null ? 2 : this.getValue() ? 1 : 0;
		
		// new value
		BL retVal = new BL();
		retVal.setValue(truth[tx][ty]);
		if(retVal.getValue() == null) retVal.setNullFlavor(new CS<NullFlavor>(NullFlavor.NoInformation));
		return retVal;
	}

	/**
	 * Determines if the other implies this instance
	 */
	public BL implies(BL other) throws CloneNotSupportedException
	{
		BL retVal = new BL();
		BL xor = this.xor(other); // o_O .. Seriously... why art thou trying to get in my way?
		if(this == null || other == null)
			retVal.setNullFlavor(new CS<NullFlavor>(NullFlavor.NoInformation));
		else if(this.getValue() != null && other.getValue() != null &&
				(!this.getValue() && !other.getValue() || this.getValue() && other.getValue()))
			retVal.setValue(true);
		else if(xor.getValue() != null && xor.getValue())
			retVal.setValue(false);
		else
			retVal.setNullFlavor(new CS<NullFlavor>(NullFlavor.NoInformation));
		return retVal;
	}

	/**
	 * Represent this object as a string
	 */
	@Override
	public String toString()
	{
		return this.getValue() == null ? null : this.getValue().toString();
	}
	
	/**
	 * Cast operator from boolean
	 */
	public static BL fromBoolean(Boolean b)
	{
		return new BL(b);
	}

	/**
	 * Cast to boolean
	 */
	public Boolean toBoolean()
	{
		return this.getValue();
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
        BL blOther = (BL)other ;
        if (blOther.getValue() != null && this.getValue() != null)
            return BL.fromBoolean(this.getValue().equals(blOther.getValue()));
        return BL.FALSE;
	}
	
}
