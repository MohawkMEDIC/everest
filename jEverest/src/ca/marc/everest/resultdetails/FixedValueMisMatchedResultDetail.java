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
 * The fixed value in the RMIM message definition does not match the value supplied
 * <p>
 * This violation occurs during the parsing of an instance property whereby the value supplied in the instance
 * does not match the fixed value in the model. Since fixed property 
 * </p>
 */
public class FixedValueMisMatchedResultDetail extends ValidationResultDetail {

	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;
	// True if the supplied value overwrote the fixed value
	private boolean m_didOverwrite = false;
	// Identifies the supplied value 
	private String m_suppliedValue;
	
	/**
	 * Creates a new instance of the FixedValueMisMatchedResultDetail with the specified suppliedValue, fixedValue, location
	 * @param suppliedValue The value that was supplied in the message instance
	 * @param fixedValue The fixed value in the model
	 * @param location The location of the property/element
	 */
	public FixedValueMisMatchedResultDetail(String suppliedValue, String fixedValue, String location)
	{
		super(ResultDetailType.ERROR, String.format("The supplied value of '%1' doesn't match the fixed value of '%2'", suppliedValue, fixedValue), location, null);
		this.m_suppliedValue = suppliedValue;
	}
	
	/**
	 * Creates a new instance of the FixedValueMisMatchedResultDetail with the specified suppliedValue, fixedValue, location and a 
	 * flag indicating whether the fixed value in the deserialized instance was replaced with the fixed
	 * @param suppliedValue The value that was supplied in the message instance
	 * @param fixedValue The fixed value in the model
	 * @param wasOverwritten True if the suppliedValue overwrote the fixedValue in the parsed instance 
	 * @param location The location of the property/element
	 */
	public FixedValueMisMatchedResultDetail(String suppliedValue, String fixedValue, boolean wasOverwritten, String location)
	{
		super(ResultDetailType.WARNING, String.format("The supplied value of '%1' doesn't match the fixed value of '%2'. %3",  suppliedValue, fixedValue, wasOverwritten ? "The supplied value has been used in place of the fixed" : "The supplied value was ignored"), location, null);
		this.m_didOverwrite = wasOverwritten;
		this.m_suppliedValue = suppliedValue;
	}

	/**
	 * Gets a value indicating whether the fixed value identified in this result detail was replaced with the supplied value
	 */
	public boolean isOverwritten() {
		return m_didOverwrite;
	}

	/**
	 * Gets the value that was supplied 
	 */
	public String getSuppliedValue() {
		return m_suppliedValue;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + (m_didOverwrite ? 1231 : 1237);
		result = prime * result
				+ ((m_suppliedValue == null) ? 0 : m_suppliedValue.hashCode());
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
		FixedValueMisMatchedResultDetail other = (FixedValueMisMatchedResultDetail) obj;
		if (m_didOverwrite != other.m_didOverwrite)
			return false;
		if (m_suppliedValue == null) {
			if (other.m_suppliedValue != null)
				return false;
		} else if (!m_suppliedValue.equals(other.m_suppliedValue))
			return false;
		return true;
	}
	
	
	
}
