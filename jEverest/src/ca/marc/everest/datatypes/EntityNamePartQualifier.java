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

import java.util.Arrays;
import java.util.List;

import ca.marc.everest.datatypes.generic.CS;
import ca.marc.everest.interfaces.IEnumeratedVocabulary;

public enum EntityNamePartQualifier implements IEnumeratedVocabulary {
	/**
	 * For organizations indicating legal status
	 */
	LegalStatus("LS", new EntityNamePartType[] { EntityNamePartType.Title }),
	/**
	 * Indicates a prefix like "Dr" or suffix like "MD" is an academic title
	 */
	Academic("AC", new EntityNamePartType[] { EntityNamePartType.Title }),
	/**
	 * In europe and asia there are some individuals with titles of nobility
	 */
	Nobility("NB", new EntityNamePartType[] { EntityNamePartType.Title }),
	/**
	 * Primarily used in British imperial culture, people tend to have an abbreviation of their professional 
	 * organizations as part of credential suffixes (FACP, ESQ, etc..)
	 */
	Professional("PR", new EntityNamePartType[] { EntityNamePartType.Title }),
	/**
	 * An honorific
	 */
	Honorific("HON", new EntityNamePartType[] { EntityNamePartType.Title }),
	/**
	 * A name the person has shortly after being born
	 */
	Birth("BR", new EntityNamePartType[] { EntityNamePartType.Family, EntityNamePartType.Given }),
	/**
	 * A name acquired through adoption or a chosen name
	 */
	Acquired("AD", new EntityNamePartType[] { EntityNamePartType.Family, EntityNamePartType.Given }),
	/**
	 * A name assumed from a partner such as a marital relationship
	 */
	Spouse("SP", new EntityNamePartType[] { EntityNamePartType.Family, EntityNamePartType.Given }),
	/**
	 * A call me name is usually a preferred name (such as Bill for William)
	 */
	CallMe("CL", new EntityNamePartType[] { EntityNamePartType.Family, EntityNamePartType.Given }),
	/**
	 * Indicates that a name is part of an initial
	 */
	Initial("IN", new EntityNamePartType[] { EntityNamePartType.Given, EntityNamePartType.Family }),
	/**
	 * Inidicates a middle name
	 */
	Middle("MID", new EntityNamePartType[] { EntityNamePartType.Given, EntityNamePartType.Family }),
	/**
	 * Identifies a prefix
	 */
	Prefix("PFX", new EntityNamePartType[] { EntityNamePartType.Title, EntityNamePartType.Given, EntityNamePartType.Family }),
	/**
	 * Identifies a suffix
	 */
	Suffix("SFX", new EntityNamePartType[] { EntityNamePartType.Title, EntityNamePartType.Given, EntityNamePartType.Family  })
	;

	// backing field for code
	private String m_code;
	// backing field for types
	private EntityNamePartType[] m_allowedTypes;
	
	/**
	 * Creates a new instance of the entity name part qualifier
	 */
	private EntityNamePartQualifier(String code, EntityNamePartType[] allowedPartTypes)
	{
		this.m_code = code;
		this.m_allowedTypes = allowedPartTypes;
	}
	/**
	 * Determine if this part can qualify the specified type
	 */
	public boolean canQualifyPartType(EntityNamePartType m_type) {
		return Arrays.asList(this.m_allowedTypes).contains(m_type);
	}

	/**
	 * Get allowed part types
	 */
	public List<EntityNamePartType> getAllowedPartTypes() {
		return Arrays.asList(this.m_allowedTypes);
	}

	/**
	 * Get the code mnemonic
	 */
	@Override
	public String getCode() {
		return this.m_code;
	}

	/**
	 * Get the code system for code mnemonics in this enumeration
	 */
	@Override
	public String getCodeSystem() {
		return "2.16.840.1.113883.5.1122";
	}

}
