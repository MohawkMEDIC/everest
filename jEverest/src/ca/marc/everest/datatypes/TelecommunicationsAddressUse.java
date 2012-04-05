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

/**
 * Telecommunications address use
 */
public enum TelecommunicationsAddressUse implements IEnumeratedVocabulary {
	
	/**
	 * Identifies communication address is a home address and
	 * should not be used for business purposes
	 */
	Home ("H"),
	/**
	 * Identifies the telecommunication address is a primary 
	 * address to reach a contact after business hours
	 */
	PrimaryHome ("HP"),
	/**
	 * A vacation home to reach a person while on vacation
	 */
	VacationHome ("HV"),
	/**
	 * An office address, should be used for business communications
	 */
	WorkPlace ("WP"),
	/**
	 * Indicates a workplace address that reaches the person directly without
	 * an intermediary
	 */
	Direct ("DIR"),
	/**
	 * Indicates an address that is a standard address that may be 
	 * subject to a switchboard or operator
	 */
	Public ("PUB"),
	/**
	 * When set, indicates an address is BAD and should not be used
	 */
	BadAddress ("BAD"),
	/**
	 * Indicates a temporary address that may be good for a small amount
	 * of time
	 */
	TemporaryAddress ("TMP"),
	/**
	 * An automatic answering machine that can be used for less urgent contact
	 */
	AnsweringService ("AS"),
	/**
	 * A contact that is designated for contact in case of emergency
	 */
	EmegencyContact ("EC"),
	/**
	 * A telecommunication device that is kept with the entity such 
	 * as a phone
	 */
	MobileContact ("MC"),
	/**
	 * A paging device that can be used to solicit a call
	 */
	Pager ("PG");

	/**
	 * Creates a new instance of the telecommunications address use 
	 */
	TelecommunicationsAddressUse(String code)
	{
		this.m_code = code;
	}
	
	// backing field for code
	private final String m_code;
	
	/**
	 * Gets the code of the telecommunications address use
	 */
	@Override
	public String getCode() {
		return this.m_code;
	}

	/**
	 * Gets the code system for the telecommunications address use
	 */
	@Override
	public String getCodeSystem() {
		return "2.16.840.1.113883.5.1011";
	}

}
