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

import ca.marc.everest.formatters.FormatterUtil;
import ca.marc.everest.interfaces.ResultDetailType;

/**
 * Identifies that a value in a datatype has been propagated as the traversal
 * on which the original value was set is not rendered by a particular formatter
 * <p>
 * This result detail is added whenever a value (such as a NullFlavor, Flavor, etc.) is
 * set on a datatype instance which is transparent on the wire (i.e. not rendered such as GTS.Hull). 
 * The result detail is used to record the original property name, destination property name,
 * and value of propagation. 
 * </p>
 */
public class ValuePropagationResultDetail extends ResultDetail {

	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;
	// The value that was propagated
	private Object m_propagatedValue;
	// The original path where the property value resided 
	private String m_originalPropertyPath;
	// The destination path where the property value now resides
	private String m_destinationPropertyPath;

	/**
	 * Creates an instance of the ValuePropagationRestulDetail class with the specified paramters
	 * @param type The type of result being created (error, warning, information)
	 * @param originalPath The original path where the property value resided
	 * @param destinationPath The path where the property value was propagated to
	 * @param value The value that was propagated
	 * @param location The location where the propagation occurred
	 */
	public ValuePropagationResultDetail(ResultDetailType type, String originalPath, String destinationPath, Object value, String location)
	{
		super(type, String.format("Value '%1' set at '%2' has been propagated to '%3'", FormatterUtil.toWireFormat(value), originalPath, destinationPath), location, null);
		this.m_propagatedValue = value;
		this.m_originalPropertyPath = originalPath;
		this.m_destinationPropertyPath = destinationPath;
	}

	/**
	 * Get the value that was propagated 
	 */
	public Object getPropagatedValue() {
		return m_propagatedValue;
	}

	/**
	 * Gets the path where the value originally resided
	 */
	public String getOriginalPropertyPath() {
		return m_originalPropertyPath;
	}

	/**
	 * Gets the path where the value now resides
	 */
	public String getDestinationPropertyPath() {
		return m_destinationPropertyPath;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime
				* result
				+ ((m_destinationPropertyPath == null) ? 0
						: m_destinationPropertyPath.hashCode());
		result = prime
				* result
				+ ((m_originalPropertyPath == null) ? 0
						: m_originalPropertyPath.hashCode());
		result = prime
				* result
				+ ((m_propagatedValue == null) ? 0 : m_propagatedValue
						.hashCode());
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
		ValuePropagationResultDetail other = (ValuePropagationResultDetail) obj;
		if (m_destinationPropertyPath == null) {
			if (other.m_destinationPropertyPath != null)
				return false;
		} else if (!m_destinationPropertyPath
				.equals(other.m_destinationPropertyPath))
			return false;
		if (m_originalPropertyPath == null) {
			if (other.m_originalPropertyPath != null)
				return false;
		} else if (!m_originalPropertyPath.equals(other.m_originalPropertyPath))
			return false;
		if (m_propagatedValue == null) {
			if (other.m_propagatedValue != null)
				return false;
		} else if (!m_propagatedValue.equals(other.m_propagatedValue))
			return false;
		return true;
	}
	
	
}
