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
 * A choice element that was used is not supported
 * <p>
 * This detail is usually raised whenever a property is populated with a valid value
 * from a Java perspective, however the RMIM model does not support the choice. This is
 * commonly raised when java.lang.Object is used as a property's type.
 * </p>
 */
public class NotSupportedChoiceResultDetail extends FormalConstraintViolationResultDetail {

	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;

	/**
	 * Creates a new instance of the NotSupportedChoiceResultDetail with the specified type, message and exception
	 * @param type The type of result being constructed (error, warning, information)
	 * @param message A textual message which describes the choice element which is not supported
	 * @param exception The exception that caused this issue to be raised
	 */
	public NotSupportedChoiceResultDetail(ResultDetailType type,
			String message, Exception exception) {
		super(type, message, exception);
	}

	/**
	 * Creates a new instance of the NotSupportedChoiceResultDetail with the specified type, message, exception and location
	 * @param type The type of result detail being constructed (error, warning, information)
	 * @param message A textual message which describes the choice element which is not supported
	 * @param location The location where the invalid choice element was detected
	 * @param exception The exception that caused this issue to be raised
	 */
	public NotSupportedChoiceResultDetail(ResultDetailType type,
			String message, String location, Exception exception) {
		super(type, message, location, exception);
	}

	/**
	 * Creates a new instance of the NotSupportedChoiceResultDetail with the specified type and message
	 * @param type The type of result detail being constructed (error, information, warning)
	 * @param message A textual message which describes the choice element which is not supported
	 */
	public NotSupportedChoiceResultDetail(ResultDetailType type, String message) {
		super(type, message);
	}

}

