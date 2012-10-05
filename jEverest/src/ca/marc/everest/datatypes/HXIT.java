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
import java.util.ArrayList;
import java.util.Collection;

import ca.marc.everest.interfaces.*;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;
import ca.marc.everest.annotations.*;

/**
 * The HistoryItem or HXIT is a generic data type extension that tags a time range and control
 * act event to any datatype. The time range is the time in which the information represented
 * by the value is (was) valid and which control act event modified the value
 */
@Structure(name = "HXIT", structureType = StructureType.DATATYPE)
public abstract class HXIT implements IGraphable
{

	// Backing field for valid time low
	private TS m_validTimeLow;
	// Backing field for valid time high
	private TS m_validTimeHigh;
	// Backing field for control act root
	private String m_controlActRoot;
	

	// Backing field for control act ext
	private String m_controlActExt;
	
	/**
	 * Identifies the time that the given information has or will become valid
	 */
	@Property(name = "validTimeLow", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	public TS getValidTimeLow() { return this.m_validTimeLow; }
	/**
	 * Identifies the time that the given information has or will become valid
	 * @param value The value to set the valid time
	 */
	public void setValidTimeLow(TS value) { this.m_validTimeLow = value; }
	/**
	 * Identifies the time that the given information has or will no longer be valid
	 */
	@Property(name = "validTimeHigh", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	public TS getValidTimeHigh() { return this.m_validTimeHigh; }
	/**
	 * Identifies the time that the given information has or will no longer be valid
	 * @param value The value to set the valid time
	 */
	public void setValidTimeHigh(TS value) { this.m_validTimeHigh = value; }
	/**
	 * Identifies the root of the identifier of the event associated with the setting of the data type to the value
	 */
	@Property(name = "controlActRoot", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	public String getControlActRoot() { return this.m_controlActRoot; }
	/**
	 * Identifies the root of the identifier of the event associated with the setting of the data type to the value
	 * @param value The value to set the control act root 
	 */
	public void setControlActRoot(String value) { this.m_controlActRoot = value; }
	/**
	 * Identifies the extension of the identifier of the event associated with the setting of the data type to the value
	 */
	@Property(name = "controlActExt", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	public String getControlActExt() { return this.m_controlActExt; }
	/**
	 * Identifies the extension of the identifier of the event associated with the setting of the data type to the value
	 * @param value The value to set the control act extension to
	 */
	public void setControlActExt(String value) { this.m_controlActExt = value; }
	
	/**
	 * When overridden in a derived class this method validates that the datatype contents are valid.
	 * @return True if the datatype is valid
	 */
	public boolean validate()
	{
		return this.m_controlActExt == null && this.m_controlActRoot == null || 
			this.m_controlActRoot != null && this.m_controlActExt != null;
	}
	
	
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result
				+ ((m_controlActExt == null) ? 0 : m_controlActExt.hashCode());
		result = prime
				* result
				+ ((m_controlActRoot == null) ? 0 : m_controlActRoot.hashCode());
		result = prime * result
				+ ((m_validTimeHigh == null) ? 0 : m_validTimeHigh.hashCode());
		result = prime * result
				+ ((m_validTimeLow == null) ? 0 : m_validTimeLow.hashCode());
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
		HXIT other = (HXIT) obj;
		if (m_controlActExt == null) {
			if (other.m_controlActExt != null) {
				return false;
			}
		} else if (!m_controlActExt.equals(other.m_controlActExt)) {
			return false;
		}
		if (m_controlActRoot == null) {
			if (other.m_controlActRoot != null) {
				return false;
			}
		} else if (!m_controlActRoot.equals(other.m_controlActRoot)) {
			return false;
		}
		if (m_validTimeHigh == null) {
			if (other.m_validTimeHigh != null) {
				return false;
			}
		} else if (!m_validTimeHigh.equals(other.m_validTimeHigh)) {
			return false;
		}
		if (m_validTimeLow == null) {
			if (other.m_validTimeLow != null) {
				return false;
			}
		} else if (!m_validTimeLow.equals(other.m_validTimeLow)) {
			return false;
		}
		return true;
	}
	
	/**
	 * Extended validation which returns details about the validation
	 */
	public Collection<IResultDetail> validateEx()
	{
		Collection<IResultDetail> retVal = new ArrayList<IResultDetail>();
        if ((this.m_controlActRoot == null) ^ (this.m_controlActExt == null))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "HXIT",
                this.m_controlActExt == null ? "ControlActExt must be populated when ControlActRoot is populated" :
                "ControlActRoot must be populated when ControlActExt is populated", null));
        return retVal;
	}
	
	
}
