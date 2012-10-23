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
package ca.marc.everest.interfaces;

/**
 * Represents the outcomes of a parse, format or connector 
 * operation
 */
public enum ResultCodeType {

	/**
	 * The operation succeeded and the message or structure as 
	 * accepted without error
	 */
	Accepted,
	/**
	 * The operation succeeded, however the message generated
	 * may not be conformant.
	 */
	AcceptedNonConformant,
	/**
	 * The operation failed because the message structure or
	 * operation was rejected given the current state
	 */
	Rejected,
	/**
	 * The operation could not be completed because the necessary
	 * information to complete the request is was not available
	 */
	NotAvailable,
	/**
	 * The operation could not be completed because a runtime error
	 * occurred that prohibited the operation from succeeding
	 */
	Error
}
