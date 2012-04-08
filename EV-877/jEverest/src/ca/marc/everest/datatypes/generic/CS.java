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
 * Date: 06-29-2011
 */
package ca.marc.everest.datatypes.generic;

import java.util.Comparator;

import ca.marc.everest.interfaces.*;
import ca.marc.everest.datatypes.*;
import ca.marc.everest.annotations.*;

/**
 * Coded data in its simplest form where only the code is not predetermined.
 * <p>
 * The code system and code system version are implied and fixed by the context in which the CS value occurs
 * </p>
 */
@Structure(name = "CS", structureType = StructureType.DATATYPE, defaultTemplateType = java.lang.String.class)
public class CS<T> extends ANY {

	// backing field for code
	private T m_code;
	
	/**
	 * Creates a new instance of the CS class
	 */
	public CS() { super(); }
	/**
	 * Creates a new instance of the CS class with the specified code
	 * @param code The code mnemonic for the CS
	 */
	public CS(T code) { super(); this.setCode(code); }
	
	@Property(name = "code", conformance = ConformanceType.MANDATORY, propertyType = PropertyType.STRUCTURAL)
	public T getCode() { return this.m_code; }
	/**
	 * Sets the code value of this CS.
	 * @param value The new value of the code field
	 */
	public void setCode(T value) {
		this.m_code = value;
	}

	/**
	 * Validates that the code is valid
	 * <p>
	 * The coded simple is valid if:
	 * </p>
	 * <ul>
	 * 	<li>NullFlavor is specified, XOR</li>
	 * <li><ul>
     *   <li>Code is specified, AND</li>
     *   <li>if Code is not an IEnumeratedVocabulary, then CodeSystem is specified</li>
     *  	</ul>
     * </li>
	 * </ul>
	 * 
	 */
	@Override
	public boolean validate()
	{
		if (this instanceof CV<?>) // Special case for non CS
            return super.validate();
        else
            return (this.m_code != null) ^ (this.getNullFlavor() != null) && super.validate();
	}
	
	
	
}
