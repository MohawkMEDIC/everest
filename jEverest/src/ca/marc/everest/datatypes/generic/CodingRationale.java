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
package ca.marc.everest.datatypes.generic;
import ca.marc.everest.annotations.*;
import ca.marc.everest.interfaces.*;

/**
 * Represents the rationale for codification
 */
@Structure(name = "CodingRationale", structureType = StructureType.CONCEPTDOMAIN)
public enum CodingRationale implements IEnumeratedVocabulary {
	
	/**
	 * Originally produced code
	 */
	Original ("O"),
	/**
	 * Post coding from a free text source
	 */
	PostCoding ("P"),
	/**
	 * Required by the specification describing the code
	 */
	Required ("R"),
	/**
	 * The source of a required code
	 */
	Source ("S");

	/**
	 * Creates a new instance of the coding rational enumeration
	 */
	CodingRationale(String code) { this.m_code = code; }
	
	// Backing field for code
	private final String m_code;
	
	/**
	 * Gets the code for the code instance
	 */
	@Override
	public String getCode() {
		return this.m_code;
	}

	/**
	 * Gets the code system of the code instance
	 */
	@Override
	public String getCodeSystem() {
		return "2.16.840.1.113883.5.1074";
	}

	
	
}
