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
 * Date: 06-24-2011
 */
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.*;

/**
	* This implementation of the HXIT type is used for R1 data types compatibility. 
	* <p>
	* In the MIF for R1 data types, HXIT&lt;<typeparamref name="T"/>&gt; is referenced when a data type can 
	* use the HXIT members. In the R1 formatter, no HXIT data types are serialized or parsed by 
	* default (as all DTs in the class lib have HXIT attributes, but not all DTs in R1 can 
	* use HXIT attributes). When a HXIT&lt;T&gt; is used, it marks the core DT as being allowed
	* to use the HXIT properties at the base of the inheritence tree.
	* </p>
	* <p>
	* R1 can't represent the ValidTimeLow property of the CS because R1 doesn't allow all DTs to 
	* carry history data. When this CS is formatted, only the Code attribute will be serialized. By
	* wrapping the CS class in an HXIT the formatter is instructed to use the HXIT members of the CS
	* </p><p>
	* The serialization will include the code and valid time low property
	* </p>
*/
@Structure(name = "HXIT", structureType = StructureType.DATATYPE)
public abstract class HXIT<T extends ANY> extends ca.marc.everest.datatypes.HXIT 
{
	/**
	 * Backing field for value
	 */
	private T m_value;
	
	/**
	 * Gets the value of the HXIT
	 */
	@Property(name = "value", conformance = ConformanceType.MANDATORY, propertyType = PropertyType.NONSTRUCTURAL, ignoreTraversal = true)
	public T getValue() { return this.m_value; }
	/**
	 * Set the value of the HXIT
	 */
	public void setValue(T value) { this.m_value = value; }

	/**
	 * Creates a new instance of the HXIT class
	 */
	public HXIT() { super(); }
	/**
	 * Creates a new instance of HXIT class with the specified value 
	 */
	public HXIT(T value) { this.m_value = value; } 
	
	/**
	 * Validates that this HXIT is valid
	 */
	@Override
	public boolean validate()
	{
		return this.m_value != null && super.validate();
	}
	/* (non-Javadoc)
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_value == null) ? 0 : m_value.hashCode());
		return result;
	}
	/* (non-Javadoc)
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj) {
			return true;
		}
		if (!super.equals(obj)) {
			return false;
		}
		if (!(obj instanceof HXIT)) {
			return false;
		}
		HXIT<?> other = (HXIT<?>) obj;
		if (m_value == null) {
			if (other.m_value != null) {
				return false;
			}
		} else if (!m_value.equals(other.m_value)) {
			return false;
		}
		return true;
	}
	
}
