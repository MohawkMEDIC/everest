/* 
 * Copyright 2008/2011 Mohawk College of Applied Arts and Technology
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
 * User: Jaspinder Singh
 * Date: 06-28-2011
 */

package ca.marc.everest.datatypes;

import ca.marc.everest.annotations.ConformanceType;
import ca.marc.everest.annotations.Property;
import ca.marc.everest.annotations.PropertyType;
import ca.marc.everest.annotations.Structure;
import ca.marc.everest.annotations.StructureType;


/**
 * A character string that may have a type-tag signifying its role in the address.
 */
@Structure(name="ADXP", structureType=StructureType.DATATYPE)
public class ADXP extends ANY {

	/** Specified whether an address part names the street, city, country, etc.. If not specified, the
        address will appear on the label as is */
	private AddressPartType partType;
	
	/** The string representation of the address part. */
	private String value;
	
	/** A code assigned to the part (if applicable). */
	private String code;
	
	/** The code system for which the code belongs to (if the code is specified). */
	private String codeSystem;
	
	/** A version for the code system. */
	private String codeSystemVersion;
	
	/**
	 * Instantiates a new ADXP.
	 */
	public ADXP()
	{
		super();
	}
	
	/**
	 * Instantiates a new ADXP.
	 *
	 * @param value the value of the ADXP
	 * @param type the address part type.
	 */
	public ADXP(String value, AddressPartType type)
	{
		super();
		partType = type;
		this.value = value;
	}
	
	
	/**
	 * Instantiates a new ADXP with AddressPartType.AddressLine as the default type.
	 *
	 * @param value the value of the ADXP.
	 */
	public ADXP(String value)
	{
		this.value = value;
		partType = AddressPartType.AddressLine;
	}
	
	/**
	 * Gets the part type.
	 *
	 * @return the part type
	 */
	@Property(name = "type", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public AddressPartType getPartType() {
		return partType;
	}


	/**
	 * Sets the part type.
	 *
	 * @param partType the new part type
	 */
	public void setPartType(AddressPartType partType) {
		this.partType = partType;
	}


	/**
	 * Gets the string representation of the address part.
	 *
	 * @return the string representation of the address part.
	 */
	@Property(name = "value", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public String getValue() {
		return value;
	}

	
	/**
	 * Sets the string representation of the address part.
	 *
	 * @param value the string representation of the address part.
	 */
	public void setValue(String value) {
		this.value = value;
	}

	
	/**
	 * Gets the code.
	 *
	 * @return the code
	 */
	@Property(name = "code", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public String getCode() {
		return code;
	}

	/**
	 * Sets the code.
	 *
	 * @param code the new code
	 */
	public void setCode(String code) {
		this.code = code;
	}

	/**
	 * Gets the code system.
	 *
	 * @return the code system
	 */
	@Property(name = "codeSystem", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public String getCodeSystem() {
		return codeSystem;
	}

	/**
	 * Sets the code system.
	 *
	 * @param codeSystem the new code system
	 */
	public void setCodeSystem(String codeSystem) {
		this.codeSystem = codeSystem;
	}

	/**
	 * Gets the code system version.
	 *
	 * @return the code system version
	 */
	@Property(name = "codeSystemVersion", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public String getCodeSystemVersion() {
		return codeSystemVersion;
	}

	/**
	 * Sets the code system version.
	 *
	 * @param codeSystemVersion the new code system version
	 */
	public void setCodeSystemVersion(String codeSystemVersion) {
		this.codeSystemVersion = codeSystemVersion;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((code == null) ? 0 : code.hashCode());
		result = prime * result
				+ ((codeSystem == null) ? 0 : codeSystem.hashCode());
		result = prime
				* result
				+ ((codeSystemVersion == null) ? 0 : codeSystemVersion
						.hashCode());
		result = prime * result
				+ ((partType == null) ? 0 : partType.hashCode());
		result = prime * result + ((value == null) ? 0 : value.hashCode());
		return result;
	}
	
	@Override
	public boolean equals(Object obj) {
		if (this == obj) {
			return true;
		}
		if (!super.equals(obj)) {
			return false;
		}
		if (getClass() != obj.getClass()) {
			return false;
		}
		ADXP other = (ADXP) obj;
		if (code == null) {
			if (other.code != null) {
				return false;
			}
		} else if (!code.equals(other.code)) {
			return false;
		}
		if (codeSystem == null) {
			if (other.codeSystem != null) {
				return false;
			}
		} else if (!codeSystem.equals(other.codeSystem)) {
			return false;
		}
		if (codeSystemVersion == null) {
			if (other.codeSystemVersion != null) {
				return false;
			}
		} else if (!codeSystemVersion.equals(other.codeSystemVersion)) {
			return false;
		}
		if (partType != other.partType) {
			return false;
		}
		if (value == null) {
			if (other.value != null) {
				return false;
			}
		} else if (!value.equals(other.value)) {
			return false;
		}
		return true;
	}
	
	@Override
	public String toString()
	{
		return this.getValue();
	}

}
