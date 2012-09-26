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
 * Identifies that basic validation of a datatype instance has failed
 */
public class DatatypeValidationResultDetail extends ValidationResultDetail {

	
	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;
	// The name of the datatype in error
	private String m_datatypeName;
	
	/**
	 * Creates a new instance of the DatatypeValidationResultDetail pointing to the specified datatypeName
	 * @param datatypeName The name of the datatype in error
	 */
	public DatatypeValidationResultDetail(String datatypeName) 
	{
		this(ResultDetailType.ERROR, datatypeName, null, null);
		
	}
	
	/**
	 * Creates an instance of the DatatypeValidationResultDetail with the specified dataType, type and location
	 * @param type The type of issue being created (error, warning, information)
	 * @param datatypeName The name of the datatype in error
	 * @param location The location of the instance of the datatype in error
	 */
	public DatatypeValidationResultDetail(ResultDetailType type, String datatypeName, String location)
	{
		this(type, datatypeName, null, location);
	}
	
	/**
	 * Creates an instance of the DatatypeValidationResultDetail with the specified type, datatypeName, message and location
	 * @param type The type of issue being constructed (error, warning, information)
	 * @param datatypeName The name of the datatype in error
	 * @param message A textual description of the validation issue
	 * @param location The location where the datatype validation error was found
	 */
	public DatatypeValidationResultDetail(ResultDetailType type, String datatypeName, String message, String location)
	{
		super(type, message, location, null);
		this.m_datatypeName = datatypeName;
	}

	/**
	 * Gets the name of the datatype that is in error
	 */
	public String getDatatypeName() {
		return m_datatypeName;
	}

	/**
	 * Gets the error message
	 */
	@Override
	public String getMessage() {
		if(super.getMessage() == null || super.getMessage().equals(""))
			return String.format("Data type '%1' failed basic validation, please refer to the developer's guide for more detail", this.m_datatypeName);
		else
			return String.format("Data type '%1' failed basic validation. The violation was : %2", this.m_datatypeName, super.getMessage());
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_datatypeName == null) ? 0 : m_datatypeName.hashCode());
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
		DatatypeValidationResultDetail other = (DatatypeValidationResultDetail) obj;
		if (m_datatypeName == null) {
			if (other.m_datatypeName != null)
				return false;
		} else if (!m_datatypeName.equals(other.m_datatypeName))
			return false;
		return true;
	}

	
}
