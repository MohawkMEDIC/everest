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
 * Date: 06-24-2011
 */
package ca.marc.everest.datatypes.interfaces;

/**
 * Represents the structure of a coded simple value 
 */
public interface ICodedSimple<T> {

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
	 * Get the code mnemonic value of the coded simple
	 */
	T getCode();
	/**
	 * Set the code mnemonic value of the coded simple
	 */
	void setCode(T value);
	
}
