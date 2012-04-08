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
 * Represents the details of a particular error or operation
 */
public interface IResultDetail {

	/**
	 * Gets the type of detail represented by the structure
	 */
	ResultDetailType getType();
	/**
	 * Gets the message related to the detail
	 */
	String getMessage();
	/**
	 * Gets the location of the element, or condition that caused
	 * the particular condition
	 */
	String getLocation();
	/**
	 * Gets the exception that caused the condition to be raised
	 */
	Exception getException();
	
}
