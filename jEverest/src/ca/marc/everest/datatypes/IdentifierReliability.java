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

import ca.marc.everest.annotations.*;
import ca.marc.everest.interfaces.*;

/**
 * Represents the reliability of an assigned II
 */
@Structure(name = "IdentifierScope", structureType = StructureType.CONCEPTDOMAIN)
public enum IdentifierReliability implements IEnumeratedVocabulary {
	/**
	 * The identiifer was issued by the system responsible for constructing
	 * the instnace
	 */
	IssuedBySystem("ISS"),
	/**
	 * The identifier was not issued by the system that created the 
	 * instance but has been verified.
	 */
	VerifiedBySystem("VRF"),
	/**
	 * The identifier was provided to the system that constructed the
	 * instance.
	 */
	UsedBySystem("USE");
	
	/**
	 * Creates a new instance of the identifier reliability
	 */
	IdentifierReliability(String code)
	{
		this.m_code = code;
	}

	private final String m_code;
	
	/**
	 * Gets the code mnemonic of the reliability
	 */
	@Override
	public String getCode() {
		return this.m_code;
	}

	/**
	 * Gets the code system oid for the reliability
	 */
	@Override
	public String getCodeSystem() {
		return null;
	}

	
}
