/* 
 * Copyright 2008/2011 Mohawk College of Applied Arts and Technology
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
 * User: Jaspinder Singh
 * Date: 06-28-2011
 */
package ca.marc.everest.datatypes;

import ca.marc.everest.interfaces.*;

/**
 * Identifies how an address can be used.
 */
public enum PostalAddressUse implements IEnumeratedVocabulary
{
	/**
	 * A communication address at a home, attempted contacts for business purposes.
	 */
	HomeAddress("H", null),
	
	/**
	 * The primary home to reach a person after business hours.
	 */
	PrimaryHome("HP", null),
	
	/**
	 * A vacation home.
	 */
	VacationHome("HV", null),
	
	/**
	 * An office address.
	 */
	WorkPlace("WP", null),
	
	/**
	 * Indicates a work place address or a telecommunication address.
	 */
	Direct("DIR", null),
	
	/**
	 * Indicates a work place address or telecommunication address that is a standard.
	 */
	Public("PUB", null),
	
	/**
	 * A flag indicating that the address is bad.
	 */
	BadAddress("BAD", null),
	
	/**
	 * Used primarily to visit an address.
	 */
	PhysicalVisit("PHYS", null),
	
	/**
	 * Used to send mail.
	 */
	PostalAddress("PST", null),
	
	/**
	 * A temporary address may be good for visit or mailing.
	 */
	TemporaryAddress("TMP", null),
	
	/**
	 * Alphabetic transcription.
	 */
	Alphabetic("ABC", null),
	
	/**
	 * Address as understood by the datacentre.
	 */
	Ideographic("IDE", null),
	
	/**
	 * Syllabic translation of the address.
	 */
	Syllabic("SYL", null),
	
	/**
	 * An address spelled according to the soundex algorithm.
	 */
	Soundex("SNDX", null),
	
	/** The address as understood by the datacentre. 
	 */
	Phonetic("PHON", null);
	
	
	/**
	 * Instantiates a new postal address use.
	 *
	 * @param code the code
	 * @param codeSystem the code system
	 */
	private PostalAddressUse(String code, String codeSystem)
	{
		CODE = code;
		CODE_SYSTEM = codeSystem;
	}
	
	/**
	 * Code.
	 */
	private final String CODE;
	
	/**
	 * Code System.
	 */
	private final String CODE_SYSTEM;
	
	/**
	 * Gets the code.
	 *
	 * @return the string
	 */
	public String getCode()
	{
		return CODE;
	}
	
	/**
	 * Gets the code system.
	 *
	 * @return the code system
	 */
	public String getCodeSystem()
	{
		return CODE_SYSTEM;
	}
	
	
}
