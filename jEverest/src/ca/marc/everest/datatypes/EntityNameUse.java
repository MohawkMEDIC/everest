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

import ca.marc.everest.annotations.*;
import ca.marc.everest.interfaces.IEnumeratedVocabulary;

/**
 * Identifies the intended use of an entity name
 */
@Structure(name = "EntityNameUse", structureType = StructureType.DATATYPE)
public enum EntityNameUse implements IEnumeratedVocabulary {
	/**
	 * The name to be used in a legal context
	 */
	Legal("L"),
	/**
	 * The formal name as registered in an official registry
	 */
	OfficialRecord("OR"),
	/**
	 * The name as recorded on a license or certificate
	 */
	License("C"),
	/**
	 * A name used prior to marriage
	 */
	MaidenName("M"),
	/**
	 * Name as used by indigenous  settings
	 */
	Indigenous("I"),
	/**
	 * A self asserted name that the personn is using
	 */
	Pseudonym("P"),
	/**
	 * Includes writer's pseudonym/stage name
	 */
	Artist("A"),
	/**
	 * A name when used in a religious context (ex: Sister Mary Francis)
	 */
	Religious("R"),
	/**
	 * A name assigned to a person
	 */
	Assigned("ASGN"),
	/**
	 * Alphabetic transcription of a name (ex: Romaji in Japan)
	 */
	Alphabetic("ABC"),
	/**
	 * Ideographic representation (ex: Kanji in Japan)
	 */
	Ideographic("IDE"),
	/**
	 * Syllabic transcription (ex: Hangul)
	 */
	Syllabic("SYL"),
	/**
	 * A name as defined by the soundex algorithm
	 */
	Soundex("SNDX"),
	/**
	 * The name as understood by a datacentre
	 */
	Phonetic("PHON"),
	/**
	 * An anonymously assigned name
	 */
	Anonymous("ANON"),
	/**
	 * A name intended to be used in searches
	 */
	Search("SRCH");

	/**
	 * Construct a new code mnemonic 
	 */
	private EntityNameUse(String code) { this.m_code = code; }
	
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
	 * Get the code system for the item
	 */
	@Override
	public String getCodeSystem() {
		// TODO Auto-generated method stub
		return "2.16.840.1.113883.5.1120";
	}

	
	
}
