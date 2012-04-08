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
package ca.marc.everest.datatypes;

import ca.marc.everest.interfaces.*;

/**
 * Identifies type of encapsulated data representations
 */
public enum EncapsulatedDataRepresentation implements IEnumeratedVocabulary {

	/**
	 * Data should be represented as plain text, and escaped if necessary
	 */
	Text ("TXT"),
	/**
	 * Data should be encoded using the Base64 algorithm
	 */
	Base64 ("B64"),
	/**
	 * Data should be represented as embedded XML
	 */
	Xml ("TXT");

	/**
	 * Encapsulated data mnemonic
	 */
	EncapsulatedDataRepresentation(String mnemonic)
	{
		this.m_code = mnemonic;
	}
	
	// Backing field for code
	private final String m_code;
	
	/**
	 * Gets the code mnemonic
	 */
	@Override
	public String getCode() {
		return this.m_code; 
	}

	/**
	 * Gets the code system
	 */
	@Override
	public String getCodeSystem() {
		return "";
	}

	
}
