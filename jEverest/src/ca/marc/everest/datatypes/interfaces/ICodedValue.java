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
 * Date: 07-02-2011
 */
package ca.marc.everest.datatypes.interfaces;

import ca.marc.everest.datatypes.generic.*;

/**
 * An interface that represents the structure of a codified value
 */
public interface ICodedValue<T> extends ICodedSimple, IOriginalText {

	/**
	 * Get the code system of the coded simple
	 */
	String getCodeSystem();
	/**
	 * Set the code system of the coded simple
	 */
	void setCodeSystem(String value);
	/**
	 * Get the code system name of the coded simple
	 */
	String getCodeSystemName();
	/**
	 * Set the code system name of the coded simple
	 */
	void setCodeSystemName(String value);
	/**
	 * Get the code system version of the coded simple
	 */
	String getCodeSystemVersion();
	/**
	 * Set the code system version of the coded simple
	 */
	void setCodeSystemVersion(String value);
	/**
	 * Gets the display name of the coded value
	 */
	String getDisplayName();
	/**
	 * Sets the display name of the coded value
	 */
	void setDisplayName(String value);

	/**
	 * Gets the coding rationale for the coded value
	 */
	ISet<CS<CodingRationale>> getCodingRationale();
	/**
	 * Sets the coding rationale for the coded value
	 */
	void setCodingRationale(ISet<CS<CodingRationale>> value);
	
}
