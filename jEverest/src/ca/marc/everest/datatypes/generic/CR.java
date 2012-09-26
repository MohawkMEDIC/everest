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
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.datatypes.*;
import ca.marc.everest.annotations.*;

/**
 * A concept qualifier code with optionally named role
 */
public class CR<T> extends ANY {

	// Backing field for name
	private CV<T> m_name;
	// Backing field for value
	private CD<T> m_value;
	
	/**
	 * Creates a new instance of the CR class
	 */
	public CR() {}
	/**
	 * Creates a new instance of the CR class with the specified name and
	 * value
	 * @param name The name of the code
	 * @param value The value of the code
	 */
	public CR(CV<T> name, CD<T> value)
	{
		this.m_name = name;
		this.m_value = value;
	}
	
	/**
	 * Gets a value specifying the manner in which the concept role value contributes to the concept descriptor
	 */
	@Property(name = "name", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.NONSTRUCTURAL)
	public CV<T> getName() {
		return m_name;
	}
	/**
	 * Sets a value specifying the manner in which the concept role value contributes to the concept descriptor
	 * @param value The new value of the concept qualifier name
	 */
	public void setName(CV<T> value) {
		this.m_name = value;
	}
	/**
	 * Gets a value specifying the concept that modifies the primary code phrase
	 */
	@Property(name = "value", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.NONSTRUCTURAL)
	public CD<T> getValue() {
		return m_value;
	}
	/**
	 * Sets a value specifying the concept that modifies the primary code phrase
	 */
	public void setValue(CD<T> value) {
		this.m_value = value;
	}
	
	
}
