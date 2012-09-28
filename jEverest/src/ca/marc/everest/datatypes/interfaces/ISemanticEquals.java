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
 * Date: 08-24-2011
 */
package ca.marc.everest.datatypes.interfaces;

import ca.marc.everest.datatypes.*;

/**
 * Defines a structure for determining if one instance of a datatype is
 * semantically equal to another
 */
public interface ISemanticEquals {

	/**
	 * Returns true if this instance is semantically equal to another
	 */
	public BL semanticEquals(IAny other);
}
