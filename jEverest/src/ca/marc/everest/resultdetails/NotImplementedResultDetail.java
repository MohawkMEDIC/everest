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
 * The requested feature has not been implemented
 * <p>This result detail is usually triggered when functionality has not been fully implemented
 * or a feature cannot be reliably parsed due to missing functionality. It is also possible that a 
 * requested feature cannot be implemented in Everest</p>
 */
public class NotImplementedResultDetail extends ResultDetail {

	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;

	/**
	 * Creates a new instance of the NotImplementedResultDetail with the specified type, message and exception
	 * @param type Identifies the severity of the detail (error, warning, information)
	 * @param message A textual message describing the feature that is not implemented
	 * @param exception The inner exception that caused the message to be generated
	 */
	public NotImplementedResultDetail(ResultDetailType type, String message,
			Exception exception) {
		super(type, message, exception);
	}

	/**
	 * Creates a new instance of the NotImplementedResultDetail with the specified type, message, exception and location.
	 * @param type The type of result detail being constructed (error, warning, information)
	 * @param message A textual message describing the feature that is not implemented
	 * @param location A location that triggered the result detail the be raised (usually a message path requiring the feature not implemented)
	 * @param exception The exception that caused the result detail the be raised
	 */
	public NotImplementedResultDetail(ResultDetailType type, String message,
			String location, Exception exception) {
		super(type, message, location, exception);
	}

	/**
	 * Creates a new instance of the NotImplementedResultDetail with the specified type and message 
	 * @param type The type of result detail being constructed (error, warning, information)
	 * @param message A textual message describing the feature that was not implemented
	 */
	public NotImplementedResultDetail(ResultDetailType type, String message) {
		super(type, message);
	}

}
