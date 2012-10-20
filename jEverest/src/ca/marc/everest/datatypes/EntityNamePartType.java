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
 * Date: 10-19-2012
 */
package ca.marc.everest.datatypes;

import ca.marc.everest.interfaces.IEnumeratedVocabulary;

public enum EntityNamePartType implements IEnumeratedVocabulary {
	/**
	 * Part type is the family name portion of the name
	 */
	Family("FAM"),
	/**
	 * Part type is the given name portion of the name
	 */
	Given("GIV"),
	/**
	 * Part type is the a prefix (Mr)
	 */
	Prefix("PFX"),
	/**
	 * Part type is a suffix (for example: ESQ, or III)
	 */
	Suffix("SFX"),
	/**
	 * Part type is a name because of academic, legal, employment or certification (Example: Sir, Hon) 
	 */
	Title("TITLE"),
	/**
	 * Part type is a delimiter having no other meaning except for use in printing or display
	 */
	Delimiter("DEL")
	;

	/**
	 * Creates a new EntityNamePartType instance
	 */
	private EntityNamePartType(String code)
	{ 
		this.m_code= code;
	}
	
	// backing field for code
	private final String m_code;
	
	/**
	 * Get the code mnemonic
	 */
	@Override
	public String getCode() {
		return this.m_code;
	}

	/**
	 * Gets the code systemfor the specified code
	 */
	@Override
	public String getCodeSystem() {
		return "2.16.840.1.113883.5.1121";
	}

}
