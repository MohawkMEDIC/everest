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

import ca.marc.everest.datatypes.interfaces.ICodedExtended;
import ca.marc.everest.annotations.*;

/**
 * Represents a codified value with translations to other possible code systems
 * <h3>Remarks</h3>
 * This class implements the CD.CE flavor from datatypes r2
 */
public class CE<T> extends CV<T> implements ICodedExtended<T> {

	// backing field for translation
	private SET<CD<T>> m_translation;
	
	/**
	 * Creates a new instance of the CE class
	 */
	public CE() { super(); }
	/**
	 * Creates a new instance of the CE class with the specified code
	 * @param code The code mnemonic of the CE instance
	 */
	public CE(T code) { super(code); }
	/**
	 * Creates a new instance of the CE class with the specified code and code system
	 * @param code The code mnemonic of the CE instance
	 * @param codeSystem The code system to which the CE instance belongs
	 */
	public CE(T code, String codeSystem) { super(code, codeSystem); }
	/**
	 * Creates a new instance of the CE class with the specified parameters
	 * @param code The code mnemonic of the CE instance
	 * @param codeSystem The code system to which the mnemonic belongs
	 * @param codeSystemName The name of the code system to which the mnemonic belongs
	 * @param codeSystemVersion The name version of the code system from which the mnemonic was selected
	 */
	public CE(T code, String codeSystem, String codeSystemName, String codeSystemVersion) { super(code, codeSystem, codeSystemName, codeSystemVersion); }
	/**
	 * Creates a new instance of the CE class with the specified code, codeSystem and translations
	 * @param code The code mnemonic for the CE instance
	 * @param codeSystem The code system to which the mnemonic belongs
	 * @param translation The code translated to other code systems
	 */
	public CE(T code, String codeSystem, Iterable<CD<T>> translation)
	{
		super(code, codeSystem);
		this.m_translation = new SET<CD<T>>(translation);
	}
	/**
	 * Creates a new instance of the CE class with the specified parametes 
	 * @param code The code mnemonic of the CE instance
	 * @param codeSystem The code system to which the mnemonic belongs
	 * @param codeSystemName The human readable name of the code system to which the mnemonic belongs
	 * @param codeSystemVersion The version ofthe code system from which the mnemonic was drawn
	 * @param displayName A human readable verison of the code mnemonic
	 * @param originalText The original text that represents the display name of the code that the user selected
	 * @throws UnsupportedEncodingException 
	 */
	public CE(T code, String codeSystem, String codeSystemName, String codeSystemVersion, String displayName, String originalText) throws UnsupportedEncodingException
	{
		super(code, codeSystem, codeSystemName, codeSystemVersion, displayName, originalText);
	}

	/**
	 * Gets the set of other concept descriptors that provide a translation of this concept descriptor to other
	 * code systems or a synonym to the code
	 */
	@Override
	@Property(name = "translation", propertyType = PropertyType.NONSTRUCTURAL ,conformance = ConformanceType.REQUIRED)
	public SET<CD<T>> getTranslation() { return this.m_translation; }
	
	/**
	 * Sets the set of other concept descriptors that provide a translation of this concept descriptor to other
	 * code systems or a synonym to the code
	 */
	@Override
	public void setTranslation(SET<CD<T>> value) { this.m_translation = value; }

	/**
	 * Validate the the CE is valid.
	 * 
	 * A CE is valid when a translation is specified a code is specified
	 * and all CD within the translation do not contain an original text or 
	 * a translation.
	 */
	@Override
	public boolean validate()
	{
		boolean isValid = super.validate();
		isValid &= (this.m_translation != null && this.getCode() != null) || this.m_translation == null;
		for(CD<T> translation : this.m_translation)
			isValid &= translation.validate() && translation.getOriginalText() == null && translation.getTranslation() == null;
		return isValid;
	}

}
