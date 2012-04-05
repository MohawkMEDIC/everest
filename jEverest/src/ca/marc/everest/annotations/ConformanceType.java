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
 * Date: 06-26-2011
 */
package ca.marc.everest.annotations;

/**
 * Identifies the levels of conformance that a field can employ
 */
public enum ConformanceType {

	/**
	 * A value must be supplied and must not be null (ie: no nullflavor is permitted)
	 */
	MANDATORY,
	/**
	 * The property must be supported, but data is supplied only when available
	 */
	REQUIRED,
	/**
	 * A value must be supplied, however a nullflavor can be used when no data is available
	 */
	POPULATED,
	/**
	 * Implementers may choose to not support this concept.
	 */
	OPTIONAL
}
