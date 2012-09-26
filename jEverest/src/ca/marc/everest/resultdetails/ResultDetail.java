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

import java.io.PrintWriter;
import java.io.Serializable;
import java.io.StringWriter;

import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;

/**
 * Represents a diagnostic detail about a validation, formatting or connection
 * operation
 */
public class ResultDetail implements IResultDetail, Serializable {

	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;
	
	// Type of result detail
	private ResultDetailType m_type;
	// The descriptive message related to the detail
	private String m_message;
	// The location where the issue was detected
	private String m_location;
	// The exception that caused the issue to be raised
	private Exception m_exception;
	
	/**
	 * Gets the type of detail encapsulated in this ResultDetail instance
	 */
	@Override
	public ResultDetailType getType() {
		return this.m_type;
	}

	/**
	 * Gets the textual message which caused the result detail to be reported
	 */
	@Override
	public String getMessage() {
		return this.m_message;
	}

	/**
	 * Get the location (stack trace or message path) where the issue occurred
	 */
	@Override
	public String getLocation() {
		if(this.m_location == null && this.m_exception != null)
		{
			StringWriter sw = new StringWriter();
			PrintWriter pw = new PrintWriter(sw);
			this.m_exception.printStackTrace(pw);
			this.m_location = sw.toString();
		}
		return this.m_location;
	}
	
	/**
	 * Sets the location of the result detail to the value parameter
	 * @param value The value which the location property should be set to
	 */
	public void setLocation(String value)
	{
		this.m_location = value;
	}

	/**
	 * Gets the exception that caused the issue to be raised
	 */
	@Override
	public Exception getException() {
		return this.m_exception;
	}

	/**
	 * Creates a new instance of the result detail with the specified type and message
	 * @param type The type of result detail being constructed (error, warning, information)
	 * @param message The textual message describing the issue that caused the detail to be raised
	 */
	public ResultDetail(ResultDetailType type, String message) {
		super();
		this.m_type = type;
		this.m_message = message;
	}

	/**
	 * Creates a new instance of the result detail with the specified type, message, location and exception
	 * @param type The type of result detail being constructed (error, warning, information)
	 * @param message The textual message describing the issue that caused the detail to be raised
	 * @param location The location (message path or stack trace) where the exception occurred
	 * @param exception The exception that caused the result detail to be raised
	 */
	public ResultDetail(ResultDetailType type, String message,
			String location, Exception exception) {
		super();
		this.m_type = type;
		this.m_message = message;
		this.m_location = location;
		this.m_exception = exception;
	}

	/**
	 * Creates a new instance of the result detail with the specified type, message, and exception
	 * @param type The type of result detail being constructed (error, warning, information)
	 * @param message The textual message describing the issue that caused the detail to be raised
	 * @param exception The exception that caused the result detail to be raised
	 */
	public ResultDetail(ResultDetailType type, String message,
			Exception exception) {
		super();
		this.m_type = type;
		this.m_message = message;
		this.m_exception = exception;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result
				+ ((m_location == null) ? 0 : m_location.hashCode());
		result = prime * result
				+ ((m_message == null) ? 0 : m_message.hashCode());
		result = prime * result + ((m_type == null) ? 0 : m_type.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		ResultDetail other = (ResultDetail) obj;
		if (m_location == null) {
			if (other.m_location != null)
				return false;
		} else if (!m_location.equals(other.m_location))
			return false;
		if (m_message == null) {
			if (other.m_message != null)
				return false;
		} else if (!m_message.equals(other.m_message))
			return false;
		if (m_type != other.m_type)
			return false;
		return true;
	}

	
	
}
