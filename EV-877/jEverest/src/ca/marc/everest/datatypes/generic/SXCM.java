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

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.*;

/**
 * Set component: An individual component belonging to a set
 * <p>See the IVL&lt;T> datatype for an example of a set</p>
 */
@Structure(name = "SXCM", structureType = StructureType.DATATYPE)
public class SXCM<T> extends PDV<T> {

	// backing field for the set operator
	private SetOperator m_setOperator;
	
	/**
	 * Creates a new instance of the SXCM class
	 */
	public SXCM() { super(); }
	/**
	 * Creates a new instance of the SXCM class with the specified value
	 * @param value The initial value of the SXCM
	 */
	public SXCM(T value) { super(value); }
	
	/**
	 * Gets the operator that dictates how the component is included as part of the set
	 */
	public SetOperator getOperator() { return this.m_setOperator; }
	/**
	 * Sets the operator that dictates how the component is included as part of the set
	 * @param value The new operator for the set component
	 */
	public void setOperator(SetOperator value) { this.m_setOperator = value; }
	
	/* (non-Javadoc)
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_setOperator == null) ? 0 : m_setOperator.hashCode());
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
		if (!(obj instanceof SXCM)) {
			return false;
		}
		SXCM other = (SXCM) obj;
		if (m_setOperator != other.m_setOperator) {
			return false;
		}
		return true;
	}
	
	
}
