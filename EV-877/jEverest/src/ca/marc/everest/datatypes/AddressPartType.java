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
 * Identifies how an address part contributes to the address.
 */
public enum AddressPartType implements IEnumeratedVocabulary {

	/** An address line is for either an additional locator, a delivery address or a street address. */
	AddressLine("AL", null),
	
	/** This can be a unit designator such as apartment number, suite, etc... */
	AdditionalLocator("ADL", null),
	
	/** The number or name of a specific unit contained within a building. */
	UnitIdentifier("UNID", null),
	
	/** Indicates the type of specific unit contained within a building */
	UnitDesignator("UNIT", null),
	
	/** A delivery address line is frequently used instead of breaking out delivery mode, delivery installation, etc... */
	DeliveryAddressLine("DAL", null),
	
	/** Indicates the type of delivery installation (facility to which the mail will be delivered prior to final shipping). */
	DeliveryInstallationType("DINST", null),
	
	/** The location of the delivery location, usually a town or city. */
	DeliveryInstallationArea("DINSTA", null),
	
	/** A number, letter or name identifying a delivery location (eg: Station A). */
	DeliveryInstallationQualifier("DINSTQ", null),
	
	/** Indicates the type of service offered, method of delivery. Example: A PO box. */
	DeliveryMode("DMOD", null),
	
	/** Represents the routing information such as a letter carrier route number. */
	DeliveryModeIdentifier("DMODID", null),
	
	/** A full street address line, including number details and the street name and type as appropriate. */
	StreetAddressLine("SAL", null),
	
	/** The number of a building, house or lot alongside the street. */
	BuildingNumber("BNR", null),
	
	/** The numeric portion of a building number. */
	BuildingNumberNumeric("BNN", null),
	
	/** Any alphabetic character, fraction or other text that may appear after the numeric portion of a building number. */
	BuildingNumberSuffix("BNS", null),
	
	/** The name of the street including the type. */
	StreetName("STR", null),
	
	/** The base name of a roadway, or artery recognized by a municipality. */
	StreetNameBase("STB", null),
	
	/** The designation given to the street (avenue, crescent, street, etc). */
	StreetType("STTYP", null),
	
	/** The direction (N,W,S,E). */
	Direction("DIR", null),
	
	/** The name of the party who will take receipt at the specified address. */
	CareOf("CAR", null),
	
	/** A geographic sub-unit delineated for demographic purposes. */
	CensusTract("CEN", null),
	
	/** Country. */
	Country("CNT", null),
	
	/** A sub-unit of a state or province (49 of the United States of America uses county while Louisiana uses the term "parish"). */
	County("CPA", null),
	
	/** The name of the city, town, village, etc... */
	City("CTY", null),
	
	/** Delimiters are printed without framing white space. */
	Delimiter("DEL", null),
	
	/** A numbered box located in a post station. */
	PostBox("POB", null),
	
	/** A subsection of a municipality. */
	Precinct("PRE", null),
	
	/** A sub-unit of a country. */
	State("STA", null),
	
	/** A postal code designating a region defined by the post service. */
	PostalCode("ZIP", null);
	
	
	/**
	 * Instantiates a new address part type.
	 *
	 * @param code the code
	 * @param codeSystem the code system
	 */
	private AddressPartType(String code, String codeSystem)
	{
		CODE = code;
		CODE_SYSTEM = codeSystem;
	}
	
	/**
	 * Gets the code.
	 *
	 * @return the code
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
	
	/** The code. */
	private final String CODE;
	
	/** The code system. */
	private final String CODE_SYSTEM;
	
	
}
