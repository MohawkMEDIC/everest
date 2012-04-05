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
 * Date: 07-21-2011
 */
package ca.marc.everest.formatters;

import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;

/**
 * The ResultDetail class is a general implementation of 
 * an IResultDetail interface. It is recommended to not
 * use this class, rather to derive new classes, similar to 
 * the manner in which Exceptions are used. 
 * @author fyfej
 *
 */
public class ResultDetail implements IResultDetail {

	// Type of error backing field
	private ResultDetailType m_type;
	// Message backing field
	private String m_message;
	// Exception backing field
	private Exception m_exception;
	// Location backing field
	private String m_location;
	
	/**
	 * Gets the type of result detail
	 */
	public ResultDetailType getType() { return this.m_type; }
	
	/**
	 * Gets the message of the result detail
	 */
	public String getMessage() { return this.m_message; }

	/**
	 * Gets the location where the detail was raised
	 */
	public String getLocation() { return this.m_location; }

	/**
	 * Get the exception that caused the detail
	 */
	public Exception getException() { return this.m_exception; }

	/**
	 * Creates a new instance of a result detail
	 */
	public ResultDetail() { }
	/**
	 * Creates a new instance of the result detail with the specified message
	 * @param message The textual message that describes the result detail
	 */
	public ResultDetail(String message) { 
		this.m_message = message;
	}
	/**
	 * Creates a new instance of the result detail with the specified
	 * type and message
	 * @param type The type of result detail
	 * @param message The textual message that describes the result detail
	 */
	public ResultDetail(ResultDetailType type, String message) {
		this(message);
		this.m_type = type;
	}
	/**
	 * Creates a new instance of the result detail with the specified
	 * type, message and location of error
	 * @param type The type of result detail
	 * @param message The textual message that describes the result detail
	 * @param location The location within the source that caused the result detail to occur
	 */
	public ResultDetail(ResultDetailType type, String message, String location) {
		this(type, message);
		this.m_location = location;
	}
	/**
	 * Creates a new instance of the result with the specified type,
	 * message, and exception
	 * @param type The type of the result detail
	 * @param message The textual message that describes the result detail
	 * @param exception The exception that caused the result detail to be raised
	 */
	public ResultDetail(ResultDetailType type, String message, Exception exception) {
		this(type, message);
		this.m_exception = exception;
	}
	/**
	 * Creates a new instance of the result detail with the specified type,
	 * message, exception and location
	 * @param type The type of the result detail
	 * @param message The textual message that describes the result detail
	 * @param exception The exception that caused the result detail to be raised
	 * @param location The location within the source that caused the result detail to occur
	 */
	public ResultDetail(ResultDetailType type, String message, Exception exception, String location) {
		this(type, message, exception);
		this.m_location = location;
	}
}
