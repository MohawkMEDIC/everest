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
 * Date: 07-01-2011
 */
package ca.marc.everest.datatypes.generic;

import java.io.UnsupportedEncodingException;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.*;
import ca.marc.everest.interfaces.*;

/**
 * Represents any kind of concept usually by giving a code defined in a code system.
 * A CD can contain the original text or phrase that served as the basis of the coding 
 * and one or more translations into other coding systems. The CD also contains qualifiers
 * that can be used to describe the concept of laterality or severity to further refine
 * the selected code
 */
@Structure(name = "CD", structureType = StructureType.DATATYPE, defaultTemplateType = java.lang.String.class)
public class CD<T> extends CE<T>  {

	
	// Backing field for qualifier property
	private LIST<CR<T>> m_qualifier;
	
	/**
	 * Creates a new instance of the CD class
	 */
	public CD() { 
		super();
	}
	/**
	 * Creates a new instance of the CD class with the specified code
	 * @param code The code mnemonic for the concept descriptor
	 */
	public CD(T code) { 
		super(code); 
	}
	/**
	 * Creates a new instance of the CD class with the specified code and codeSystem
	 * @param code The code mnemonic of the CD
	 * @param codeSystem The code system to which the mnemonic belongs
	 */
	public CD(T code, String codeSystem) { 
		super(code, codeSystem); 
	}
	/**
	 * Creates a new instance of the CD class with the specified code, codeSystem, codeSystemName and codeSystemVersion
	 * @param code The code mnemonic of the CD
	 * @param codeSystem The code system from which the mnemonic was selected
	 * @param codeSystemName The human readable name of the code system to which the mnemonic belongs
	 * @param codeSystemVersion The version of the code system from which the mnemonic was selected
	 */
	public CD(T code, String codeSystem, String codeSystemName, String codeSystemVersion) {
		super(code, codeSystem, codeSystemName, codeSystemVersion);
	}
	/**
	 * Creates a new instance of the CD class with the specified parameters
	 * @param code The code mnemonic for the CD
	 * @param codeSystem The code system from which the mnemonic was selected
	 * @param codeSystemName The human readable name of the code system from which the mnemonic was selected
	 * @param codeSystemVersion The version of the code system to which the mnemonic belongs
	 * @param displayName A human readable display name for the code mnemonic
	 * @param originalText The text that the user used to select the mnemonic
	 */
	public CD(T code, String codeSystem, String codeSystemName, String codeSystemVersion, String displayName, String originalText)  throws UnsupportedEncodingException
	{
		super(code, codeSystem, codeSystemName, codeSystemVersion, displayName, originalText);
	}
	/**
	 * Creates a new instance of the CD class with the specified translation
	 * @param code The code mnemonic for the CD
	 * @param codeSystem The code system to which the mnemonic belongs
	 * @param translation A list of translations that represent the CD in different code systems, or provide synonyms for the code mnemonic
	 */
	public CD(T code, String codeSystem, Iterable<CD<T>> translation) {
		super(code, codeSystem, translation);
	}
	/**
	 * Creates a new instance of the CD class with the specified code, codeSystem 
	 * @param code The code mnemonic
	 * @param codeSystem The code system from which the mnemonic was chosen
	 * @param translation A list of translations that represent the CD in different code systems or provides synonyms for the code
	 * @param qualifier A list of additional code/value pairs that serve to qualify the selected code
	 */
	public CD(T code, String codeSystem, Iterable<CD<T>> translation, Iterable<CR<T>> qualifier) {
		super(code, codeSystem, translation);
		this.m_qualifier = new LIST<CR<T>>(qualifier);
	}
	
	/** 
	 * Gets a list of codes that qualify the parent code phrase
	 */
	@Property(name = "qualifier", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public LIST<CR<T>> getQualifier() { return this.m_qualifier; }
	/**
	 * Sets a list of codes that qualify the parent code phrase.
	 */
	public void setQualifier(LIST<CR<T>> value) { this.m_qualifier = value; }
	
	/**
	 * Validates that the CD is valid. A CD is valid when:
	 * 
	 * 	The base type (CE) is valid and
	 *  when a qualifier is specified, a code is also specified
	 */
	@Override
	public boolean validate()
	{
		boolean isValid = ((this.m_qualifier != null && this.getCode() != null) || (this.m_qualifier == null));
		return isValid && super.validate();
	}
}
