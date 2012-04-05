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
 * Identifies the type of result detail that has been found
 */
public enum ResultDetailType {

	/**
	 * The result detail represents an Error
	 */
	ERROR,
	/**
	 * The result detail represents a Warning condition
	 */
	WARNING,
	/**
	 * The result detail is raised for Informational purposes only
	 */
	INFORMATION
}
