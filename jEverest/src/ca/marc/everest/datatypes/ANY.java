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
package ca.marc.everest.datatypes;

import ca.marc.everest.interfaces.*;
import ca.marc.everest.datatypes.generic.*;
import ca.marc.everest.annotations.*;

/**
 * Defines the basic properties of every data value. 
 * <p>
 * This is an abstract type meaning that no value can be just a data value without belonging to any concrete type. Every concrete type is a specialization
 * of this general abstract data value type
 * </p>    
 */
public class ANY extends HXIT implements IImplementsNullFlavor, Cloneable {

	// Backing field for null flavor
	private CS<NullFlavor> m_nullFlavor;
	// Backing field for update mode
	private CS<UpdateMode> m_updateMode;
	// Backing field for flavor id
	private String m_flavorId;
	
	/**
	 * Gets the exceptional code that identifies why the contents of the datatype are not 
	 * being passed within a message instance.
	 */
	@Property(name = "nullFlavor", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.STRUCTURAL, genericSupplier = { NullFlavor.class })
	public CS<NullFlavor> getNullFlavor() { return this.m_nullFlavor; }
	/**
	 * Sets the exception code that identifies why the contents of the datatype are not
	 * being passed within a message instance.
	 * @param value The new value of the nullFlavor field
	 */
	public void setNullFlavor(CS<NullFlavor> value) { this.m_nullFlavor = value; }
	/**
	 * Sets the exceptional code that identifies why the contents of the datatype
	 * are not being passed within a message structure
	 */
	public void setNullFlavor(NullFlavor value) { this.m_nullFlavor = new CS<NullFlavor>(value); }
	/**
	 * Gets the flavor identifier for the datatpe. A flavor identifier is used by specializations to validate
	 * the contents of a datatype to an expected rule.
	 * @return
	 */
	public String getFlavorId() { return this.m_flavorId; }
	/**
	 * Sets the flavor identifier for the datatype
	 */
	public void setFlavorId(String value) { this.m_flavorId = value; }

	/**
	 * Gets the update mode of the datatype.
	 * <p>The update mode dictates how a receiver will treat this particular data value
	 * when submitted</p>
	 * @return
	 */
	@Property(name = "updateMode", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.STRUCTURAL, genericSupplier = { UpdateMode.class })
	public CS<UpdateMode> getUpdateMode() { return this.m_updateMode; }
	/**
	 * Sets the update mode of the datatype
	 * @param value
	 */
	public void setUpdateMode(CS<UpdateMode> value) { this.m_updateMode = value; }
	
	/**
	 * Gets the datatype of this class instance
	 */
	public Class getDataType() { return this.getClass(); }

	/**
	 * A predicate indicating that a value is an exceptional or null value
	 */
	public boolean isNull() { return this.m_nullFlavor != null; }
	
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result
				+ ((m_flavorId == null) ? 0 : m_flavorId.hashCode());
		result = prime * result
				+ ((m_nullFlavor == null) ? 0 : m_nullFlavor.hashCode());
		result = prime * result
				+ ((m_updateMode == null) ? 0 : m_updateMode.hashCode());
		return result;
	}
	
	@Override
	public boolean equals(Object obj) {
		if (this == obj) {
			return true;
		}
		if (obj == null) {
			return false;
		}
		if (getClass() != obj.getClass()) {
			return false;
		}
		ANY other = (ANY) obj;
		if (m_flavorId == null) {
			if (other.m_flavorId != null) {
				return false;
			}
		} else if (!m_flavorId.equals(other.m_flavorId)) {
			return false;
		}
		if (m_nullFlavor == null) {
			if (other.m_nullFlavor != null) {
				return false;
			}
		} else if (!m_nullFlavor.equals(other.m_nullFlavor)) {
			return false;
		}
		if (m_updateMode == null) {
			if (other.m_updateMode != null) {
				return false;
			}
		} else if (!m_updateMode.equals(other.m_updateMode)) {
			return false;
		}
		return true;
	}
	
	
}
