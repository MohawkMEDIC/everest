/* 
 * Copyright 2008/2011 Mohawk College of Applied Arts and Technology
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
 * User: Jaspinder Singh
 * Date: 06-28-2011
 */

package ca.marc.everest.datatypes;

import ca.marc.everest.interfaces.*;

/**
 * A code specifying whether the set component is included (union) or excluded (difference) from the set, 
 * or other set operations with the current set component and the set as constructed from the 
 * representation stream up to the current point.
 */
public enum SetOperator implements IEnumeratedVocabulary {
	

	/** Form the convex hull with the value. */
	Hull("H", null),
	
	/** Include the value in the value set (union). */
	Inclusive("I", null),
	
	/** Exclude the value from the set (difference). */
	Exclusive("E", null),
	
	
	/** Intersect: Form the intersection with the value. */
	Intersect("I", null),
	
	/** Form the periodic hull with the value. */
	PeriodicHull("P", null);
	
	/**
	 * Instantiates a new SetOperator.
	 *
	 * @param code the code
	 * @param codeSystem the code system
	 */
	private SetOperator(String code, String codeSystem)
	{
		CODE = code;
		CODE_SYSTEM = codeSystem;
	}
	
	/** The code. */
	private final String CODE;
	
	/** The code system. */
	private final String CODE_SYSTEM;
	
	/**
	 * Gets the code.
	 * @return the code.
	 */
	public String getCode()
	{
		return CODE;
	}
	
	/**
	 * Gets the code system.
	 * @return the code system.
	 */
	public String getCodeSystem()
	{
		return CODE_SYSTEM;
	}
	
}
