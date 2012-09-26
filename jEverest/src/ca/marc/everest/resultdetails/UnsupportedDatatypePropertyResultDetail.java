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
 * Identifies that a property populated within a datatype can't be rendered to the output stream 
 * by the currently executing datatype formatter
 * <p>
 * Because the Everest data type library is a combination of R1 and R2 concepts 
 * (to support write-once render both) some concepts cannot be rendered within 
 * either instance of a message
 * </p>
 * <p>This result detail signals that a value populated in memory may not 
 * have been rendered on the wire
 * </p>
 * <p>This class is abstract, it is expected that each datatype formatter will 
 * extend this class and implement formatter specified enhancements</p>
 */
public abstract class UnsupportedDatatypePropertyResultDetail extends NotImplementedElementResultDetail {

	// Serialization version unique identifier 
	private static final long serialVersionUID = 1L;
	// The name of the property that is not supported
	private String m_propertyName;
	// The name of the datatype where the property is defined
	private String m_datatypeName;
	
	/**
	 * Creates an empty instance of the property result detail
	 */
	protected UnsupportedDatatypePropertyResultDetail() 
	{
		super(null, null);
	}
	
	/**
	 * Creates a new instance of the UnsupportedDatatypePropertyResultDetail
	 * @param type The type of result being constructed (error, warning, information)
	 * @param datatypeName The name of the datatype where the property is defined
	 * @param propertyName The name of the property which is not supported
	 * @param location The location within the message instance (or model) where the property is defined
	 */
	protected UnsupportedDatatypePropertyResultDetail(ResultDetailType type, String datatypeName, String propertyName, String location)
	{
		super(type, String.format("Property '%1' in '%2' is not supported by this formatter", propertyName, datatypeName), location, null);
		this.m_propertyName = propertyName;
		this.m_datatypeName = datatypeName;
	}

	/**
	 * Get the name of the property which is not supported
	 */
	public String getPropertyName() {
		return m_propertyName;
	}

	/**
	 * Get the name of the datatype in which the property is defined
	 */
	public String getDatatypeName() {
		return m_datatypeName;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_datatypeName == null) ? 0 : m_datatypeName.hashCode());
		result = prime * result
				+ ((m_propertyName == null) ? 0 : m_propertyName.hashCode());
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
		UnsupportedDatatypePropertyResultDetail other = (UnsupportedDatatypePropertyResultDetail) obj;
		if (m_datatypeName == null) {
			if (other.m_datatypeName != null)
				return false;
		} else if (!m_datatypeName.equals(other.m_datatypeName))
			return false;
		if (m_propertyName == null) {
			if (other.m_propertyName != null)
				return false;
		} else if (!m_propertyName.equals(other.m_propertyName))
			return false;
		return true;
	}

	
	
}
