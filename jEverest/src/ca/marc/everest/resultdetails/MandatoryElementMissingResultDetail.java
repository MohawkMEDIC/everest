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
 * Mandatory element is missing 
 * <p>This formal constraint violation indicates that an instance has a property set to null or
 * an instance containing a NullFlavor where the conformance is  Mandatory</p>
 */
public class MandatoryElementMissingResultDetail extends ResultDetail {
	
	// Serialization version unique identifier
		private static final long serialVersionUID = 1L;

		/**
		 * Creates a new instance of the MandatoryElementMissingResultDetail with the specified type, message and exception 
		 * @param type The type of result detail being constructed (error, warning, information)
		 * @param message A textual message which describes the missing element
		 * @param exception The exception that caused the mandatory element to not be interpreted (set to null)
		 */
		public MandatoryElementMissingResultDetail(ResultDetailType type, String message,
				Exception exception) {
			super(type, message, exception);
		}

		/**
		 * Creates a new instance of the MandatoryElementMissingResultDetail with the specified type, message, location and exception 
		 * @param type The type of result detail being constructed (error, warning, information)
		 * @param message A textual message which describes the missing element
		 * @param exception The exception that caused the mandatory element to not be interpreted (set to null)
		 * @param location The message path to the missing mandatory element
		 */
		public MandatoryElementMissingResultDetail(ResultDetailType type, String message,
				String location, Exception exception) {
			super(type, message, location, exception);
		}

		/**
		 * Creates a new instance of the MandatoryElementMissingResultDetail with the specified message and type
		 * @param type The type of result detail being constructed (error, warning, information)
		 * @param message A textual message which describes the missing element
		 */
		public MandatoryElementMissingResultDetail(ResultDetailType type, String message) {
			super(type, message);
		}
}
