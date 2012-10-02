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
 * Date: 07-25-2011
 */
package ca.marc.everest.datatypes;

import java.math.BigDecimal;

import ca.marc.everest.datatypes.generic.*;
import ca.marc.everest.datatypes.interfaces.*;
import ca.marc.everest.annotations.*;
/**
 * Represents data where coded values are associated with a specific order
 */
@Structure(name = "CO", structureType = StructureType.DATATYPE)
public class CO extends QTY<BigDecimal> {

	// Backing code
	private CD<String> m_code;
	
	/**
	 * Creates a new instance of the CO type
	 */
	public CO() {}
	/**
	 * Creates a new instance of the CO class with the specified value
	 * @param value The ordinal value of the code
	 */
	public CO(BigDecimal value) { 
		super(value); 
	}
	/**
	 * Creates a new instance of the CO class with the specified code
	 * @param code The code of the CO
	 */
	public CO(CD<String> code) { 
		super();
		this.m_code = code;
	}
	/**
	 * Creates a new instance of the CO class with the specified value and code
	 * @param value The ordinal value of the CO
	 * @param code The code mnemonic of the CO
	 */
	public CO(BigDecimal value, CD<String> code) {
		super(value);
		this.m_code = code;
	}
	
	/**
	 * Gets the code of the CO
	 * @return The current code of the CO
	 */
	@Property(name = "code", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.NONSTRUCTURAL)
	public CD<String> getCode() { return this.m_code; }
	/**
	 * Sets the value of the CO
	 * @param value The new code of the CO
	 */
	public void setCode(CD<String> value) { this.m_code = value; }
	
	/**
	 * Validate the CO
	 */
	@Override
	public boolean validate()
	{
		return (this.getNullFlavor() != null) ^ (this.getCode() != null || this.getValue() != null) &&
        ((this.getCode() != null && this.getCode().validate()) || this.getCode() == null);
	}
	/**
	 * Represent the CO as an integer
	 */
	@Override
	public Integer toInteger() {
		return this.getValue().intValue();
	}
	/**
	 * Represent the CO as a double
	 */
	@Override
	public Double toDouble() {
		// TODO Auto-generated method stub
		return this.getValue().doubleValue();
	}

}
