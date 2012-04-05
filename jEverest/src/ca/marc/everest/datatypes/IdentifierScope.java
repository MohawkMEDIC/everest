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
 * Date: 09-09-2011
 */
package ca.marc.everest.datatypes;

import ca.marc.everest.interfaces.*;

/**
 * Identifies the scope to which an II applies
 */
public enum IdentifierScope implements IEnumeratedVocabulary {
	/**
	 * An identifier associated with the object due to business practices
	 */
	BusinessIdentifier("BUSN"),
	/**
	 * An identiifer associated with an object 
	 */
	ObjectIdenifier("OBJ"),
	/**
	 * An identifier that references a paricular version of the object
	 */
	VersionIdentifier("VER"),
	/**
	 * An identifier for the particular snapshot view of the object.
	 */
	ViewSpecificIdentifier("VW");

	
	/**
	 * Creates a new instance of the identifier scope 
	 */
	IdentifierScope(String code)
	{
		this.m_code = code; 
	}
	
	// Code for the identifier scope
	private final String m_code;
	
	/**
	 * Get the code mnemonic for the selected identifier scope
	 */
	@Override
	public String getCode() {
		return this.m_code;
	}

	/**
	 * Get the code system for the identifier scope enumeration
	 */
	@Override
	public String getCodeSystem() {
		return null;
	}
	
	
}
