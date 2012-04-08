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
 * Date: 08-30-2011
 */
package ca.marc.everest.datatypes;

import ca.marc.everest.interfaces.*;
import ca.marc.everest.annotations.*;

/**
 * Telecommunications capabilities 
 */
@Structure(name = "TelecommunicationCapability", structureType = StructureType.CONCEPTDOMAIN)
public enum TelecommunicationsCapabilities implements IEnumeratedVocabulary {
	/**
	 * The device can receive voice calls (ie: IVR, or TAM)
	 */
	Voice("voice"),
	/**
	 * The device can receive faxes
	 */
	Fax("fax"),
	/**
	 * The device can receive data streams (ie: a modem)
	 */
	Data("data"),
	/**
	 * The device can receive text telephone data (ie: TTY)
	 */
	Text("text"), 
	/**
	 * The device can receive SMS messages
	 */
	SMS("sms");

	// Code
	private final String m_code;
	
	/**
	 * Creates a new instance of the enumeration
	 */
	TelecommunicationsCapabilities(String code) { this.m_code = code; }
	
	/**
	 * Gets the code
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
		return "2.16.840.1.113883.5.1118";
	}

}
