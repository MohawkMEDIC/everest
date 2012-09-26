/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 * Date: 09-25-2012
 */
package ca.marc.everest.resultdetails;

import ca.marc.everest.interfaces.ResultDetailType;

/**
 * Identifies that an instance of a datatype failed to meet the criteria of a scoped flavor in the model
 */
public class DatatypeFlavorValidationResultDetail extends DatatypeValidationResultDetail {

	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;
	// The name of the flavor the datatype instance failed to meet
	private String m_flavorName;
	
	/**
	 * Creates a new instance of the DatatypeFlavorValidationResultDetail having the specified datatypeName and flavorName	
	 * @param datatypeName The name of the datatype that was validated
	 * @param flavorName The flavor that the datatype instance failed to meet criteria
	 */
	public DatatypeFlavorValidationResultDetail(String datatypeName, String flavorName)
	{
		this(ResultDetailType.ERROR, datatypeName, flavorName, null);
	}
	
	/**
	 * Creates a new instance of the DatatypeFlavorValidationResultDetail having the specified datatypeName, flavorName, type and location
	 * @param type The type of issue being constructed (error, warning, information)
	 * @param datatypeName The name of the datatype that is in violation
	 * @param flavorName The name of the flavor that the datatype instance failed to meet
	 * @param location The location of the datatype instance in a message
	 */
	public DatatypeFlavorValidationResultDetail(ResultDetailType type, String datatypeName, String flavorName, String location)
	{
		super(type, datatypeName, location);
		this.m_flavorName = flavorName;
	}

	/**
	 * Gets a descriptive message
	 */
	@Override
	public String getMessage() {
		return String.format("Datatype '%1' failed validation criteria for flavor '%2'. Please refer to development guide for more information", this.getDatatypeName(), this.m_flavorName);
	}

	/**
	 * Get the name of the flavor that the datatype instance failed to meet 
	 */
	public String getFlavorName() {
		return m_flavorName;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_flavorName == null) ? 0 : m_flavorName.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		DatatypeFlavorValidationResultDetail other = (DatatypeFlavorValidationResultDetail) obj;
		if (m_flavorName == null) {
			if (other.m_flavorName != null)
				return false;
		} else if (!m_flavorName.equals(other.m_flavorName))
			return false;
		return true;
	}
	
}
