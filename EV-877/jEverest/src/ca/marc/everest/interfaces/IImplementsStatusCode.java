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
 * Date: 07-19-2011
 */
package ca.marc.everest.interfaces;

import ca.marc.everest.datatypes.generic.*;

/**
 * Identifies a class that implements a status code property
 */
public interface IImplementsStatusCode<T> {

	/**
	 * Gets the value of the status code
	 */
	CS<T> getStatusCode();
	
}
