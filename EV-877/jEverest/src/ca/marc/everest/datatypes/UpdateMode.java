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
 * Date: 06-29-2011
 */
package ca.marc.everest.datatypes;

import ca.marc.everest.interfaces.*;
import ca.marc.everest.annotations.*;
/**
 * An enumeration of the allowed update modes. See Members for modes.
 */
@Structure(name = "UpdateMode", structureType = StructureType.CONCEPTDOMAIN)
public enum UpdateMode implements IEnumeratedVocabulary  {

	/**
	 * The item was (or is to be) added, having not been present before
	 */
	Add("A"),
	/**
	 *  The item was(or is to be) removed
	 */
	Remove("D"),
	/**
	 * The item existed previously and has been (or is to be) revised
	 */
	Replace("R"),
	/**
	 * The item was (or is to be) either added or replaced
	 */
	AddOrReplace("AR"),
	/**
	 * There was (or is to be) no change to the item
	 */
	NoChange("N"),
	/**
	 * It is not specified whether or what kind of change has occurred to the item
	 */
	Unknown("U"),
	/**
	 * The item is part of the identifying information for the object that contains it
	 */
	Key("K");
	
	/**
	 * Creates a new instance of the UpdateMode enumeration
	 */
	private UpdateMode(String code)
	{
		this.m_code = code;
	}

	// Backing field for code
	private final String m_code;
	
	/**
	 * Get the code system of the UpdateMode valueset
	 */
	public String getCodeSystem() { return "2.16.840.1.113883.5.57"; }
	/**
	 * Get the code of the update mode
	 */
	public String getCode() { return this.m_code; }
}
