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
 * Date: 08-31-2011
 */
package ca.marc.everest.exceptions;

/**
 * This class is thrown when an exception or fatal error
 * occurs in an Everest Formatter
 */
public class FormatterException extends RuntimeException {

	// Serialization verison unique identifier
	private static final long serialVersionUID = 1L;
	
	/**
	 * Creates a new instance of the FormatterException class
	 */
	public FormatterException() {}
	/**
	 * Creates a new instance of the FormatterException class with the 
	 * specified message
	 */
	public FormatterException(String message) { super(message); }
	/**
	 * Creates a new instance of the FormatterException class with the
	 * specified message and inner exception (cause)
	 * @param message
	 * @param innerException
	 */
	public FormatterException(String message, Exception innerException) { super(message, innerException); }
	
}
