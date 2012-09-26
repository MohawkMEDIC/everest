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
 * Represents a result detail whereby a formal constraint was violated
 */
public class FormalConstraintViolationResultDetail extends ResultDetail {

	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;

	/**
	 * Constructs a new instance of the FormalConstraintViolationResultDetail with the specified type, message, and exception 
	 * @param type The type of validation result (error, warning, information)
	 * @param message A textual description of the condition that caused the detail to be raised 
	 * @param exception The exception that caused the detail to be raised
	 */
	public FormalConstraintViolationResultDetail(ResultDetailType type,
			String message, Exception exception) {
		super(type, message, exception);
	}

	/**
	 * Constructs a new instance of the FormalConstraintViolationResultDetail with the specified type, message, location and exception 
	 * @param type The type of validation result (error, warning, information)
	 * @param message A textual description of the condition that caused the detail to be raised 
	 * @param exception The exception that caused the detail to be raised
	 * @param location The location where the formal constraint violation was detected
	 */
	public FormalConstraintViolationResultDetail(ResultDetailType type,
			String message, String location, Exception exception) {
		super(type, message, location, exception);
		// TODO Auto-generated constructor stub
	}

	/**
	 * Constructs a new instance of the FormalConstraintViolationResultDetail with the specified type and message 
	 * @param type The type of validation result (error, warning, information)
	 * @param message A textual description of the condition that caused the detail to be raised 
	 */
	public FormalConstraintViolationResultDetail(ResultDetailType type,
			String message) {
		super(type, message);
		// TODO Auto-generated constructor stub
	}

}
