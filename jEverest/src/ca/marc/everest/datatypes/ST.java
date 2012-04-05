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

import java.security.InvalidParameterException;
import java.util.Locale;

import org.w3c.dom.ranges.RangeException;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.*;

/**
 * The character string data type is used to encapsulate text data that 
 * is primarily intended for machine use such as sorting, indexing and searching 
 */
@Structure(name = "ST", structureType = StructureType.DATATYPE)
public class ST extends PDV<String> {
	
	// backing field for language property
	private String m_language;
	// backing field for translations
	private SET<ST> m_translation;
	
	/**
	 * Creates a new empty instance of the ST class with the default language code
	 */
	public ST() { 
		super();
		this.m_language = Locale.getDefault().getLanguage() + "-" + Locale.getDefault().getCountry();
	}
	/**
	 * Creates a new instance of the ST class with the specified text in the 
	 * current locale's language code
	 */
	public ST(String data) {
		this();
		this.setValue(data);
	}
	/**
	 * Creates a new instance of the ST class with the specified string data 
	 * and language code
	 */
	public ST(String data, String language)
	{
		this(data);
		this.m_language = language;
	}
	
	/**
	 * Gets the current language of the character string data
	 */
	public String getLanguage() { return this.m_language; }
	/**
	 * Sets the language of the character string data
	 */
	public void setLanguage(String value) { this.m_language = value; }
	/**
	 * Gets a set that contains translations of this string instance to other languages
	 */
	public SET<ST> getTranslation() { return this.m_translation; }
	/**
	 * Sets a set that contains translations of this string instance to other languages.
	 */
	public void setTranslation(SET<ST> value) { this.m_translation = value; }
	/**
	 * Gets the length of this character string. 0 if the value has not been set
	 */
	public int length() { return this.getValue() == null ? 0 : this.getValue().length(); }
	/**
	 * Concatenate this string with another
	 */
	public ST concat(ST other)
	{
		ST retVal = new ST();
		if(other == null)
			return null;
		else if(other.isNull() || this.isNull())
			retVal.setNullFlavor(NullFlavor.NoInformation);
		else
			retVal.setValue(this.getValue() + other.getValue());
		return retVal;
	}
	/**
	 * Return a sub string of this character string instance
	 */
	public ST subString(INT start, INT end)
	{
		ST retVal = new ST();
		if(start == null || start.isNull())
			throw new IllegalArgumentException("start is null or null flavored");
		else if(end == null || end.isNull())
			throw new IllegalArgumentException("end is null or null flavored");
		else if(start.getValue() > this.length())
			throw new ArrayIndexOutOfBoundsException("start must be less than length of string");
		else if(end.getValue() > this.length())
			throw new ArrayIndexOutOfBoundsException("end must be less than length of the string");
		else if(start.compareTo(end) > 0)
			throw new IllegalArgumentException("start must be less than end");
		else if(this.getNullFlavor() != null)
			retVal.setNullFlavor(this.getNullFlavor());
		else
			retVal.setValue(this.getValue() == null ? null : this.getValue().substring(start.getValue(), end.getValue()));
		return retVal;
	}
	
	/**
	 * Validates this instance of ST meets basic validation criteria
	 */
	@Override
	public boolean validate()
	{
		boolean isValid = ((this.getValue() != null) ^ (this.getNullFlavor() != null));
		if(this.getTranslation() != null)
			for(ST trans : this.getTranslation())
				isValid &= trans.getTranslation() == null;
		return isValid;
	}
	
	/**
	 * Validates this ST instance meets validation criteria for flavor
	 * NoTranslations
	 */
	@Flavor(name = "ST.NT")
	public boolean isNoTranslationsFlavor(ST st)
	{
		return st.getTranslation() == null || st.getTranslation().isEmpty();
	}
	
	/**
	 * Validates that this ST instance meets validation criteria for flavor
	 * Simple
	 */
	@Flavor(name = "ST.SIMPLE")
	public boolean isSimpleFlavor(ST st)
	{
		String defaultLanguage = Locale.getDefault().getLanguage() + "-" + Locale.getDefault().getCountry();
		// Language is usually set to the default locale
        if (st.getLanguage().equals(defaultLanguage))
            st.setLanguage(null);
        return st.getLanguage() == null;
	}
}
