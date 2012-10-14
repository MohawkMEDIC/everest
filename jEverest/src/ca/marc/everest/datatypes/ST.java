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

import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Locale;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.*;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IPredicate;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

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
	@Property(name = "language", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public String getLanguage() { return this.m_language; }
	/**
	 * Sets the language of the character string data
	 */
	public void setLanguage(String value) { this.m_language = value; }
	/**
	 * Gets a set that contains translations of this string instance to other languages
	 */
	@Property(name = "translation", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
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
	 * Convert this ST to an ED
	 * @return
	 * @throws UnsupportedEncodingException 
	 */
	public ED toED() throws UnsupportedEncodingException
	{
        // Copy
        ED retVal = new ED(this.getValue(), this.getLanguage());
        retVal.setControlActExt(this.getControlActExt());
        retVal.setControlActRoot(this.getControlActRoot());
        retVal.setFlavorId(this.getFlavorId());
        retVal.setNullFlavor(this.getNullFlavor());
        retVal.setTranslation(new SET<ED>(this.getTranslation()));
        retVal.setUpdateMode(this.getUpdateMode());
        retVal.setValidTimeHigh(this.getValidTimeHigh());
        retVal.setValidTimeLow(this.getValidTimeLow());
        retVal.setMediaType("text/plain");
        retVal.setRepresentation(EncapsulatedDataRepresentation.Text);
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
	public boolean isValidNoTranslationsFlavor(ST st)
	{
		return st.getTranslation() == null || st.getTranslation().isEmpty();
	}
	
	/**
	 * Validates that this ST instance meets validation criteria for flavor
	 * Simple
	 */
	@Flavor(name = "ST.SIMPLE")
	public boolean isValidSimpleFlavor(ST st)
	{
		String defaultLanguage = Locale.getDefault().getLanguage() + "-" + Locale.getDefault().getCountry();
		// Language is usually set to the default locale
        if (st.getLanguage().equals(defaultLanguage))
            st.setLanguage(null);
        return st.getLanguage() == null;
	}
	/**
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_language == null) ? 0 : m_language.hashCode());
		result = prime * result
				+ ((m_translation == null) ? 0 : m_translation.hashCode());
		return result;
	}
	/**
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		ST other = (ST) obj;
		if (m_language == null) {
			if (other.m_language != null)
				return false;
		} else if (!m_language.equals(other.m_language))
			return false;
		if (m_translation == null) {
			if (other.m_translation != null)
				return false;
		} else if (!m_translation.equals(other.m_translation))
			return false;
		return true;
	}
	/**
	 * @see ca.marc.everest.datatypes.generic.PDV#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		 List<IResultDetail> retVal = (ArrayList<IResultDetail>)super.validateEx();

		 // A predicate which can detect if a ST has a null translation
		 IPredicate<ST> nullTranslationPredicate = new IPredicate<ST>()
         {
			 public boolean match(ST o)
			 {
				 return o.getTranslation() != null;
			 }
		 };
		 
         if(this.isNull() && this.getValue() != null)
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ST", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
         else if(this.getValue() == null && !this.isNull())
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ST", EverestValidationMessages.MSG_NULLFLAVOR_MISSING, null));
         if (this.getTranslation() != null && this.getTranslation().findAll(nullTranslationPredicate).size() == this.getTranslation().size())
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ST", String.format(EverestValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "Translation", "Translation"), null));
         return retVal;
	}
	/**
	 * @see ca.marc.everest.datatypes.ANY#semanticEquals(ca.marc.everest.datatypes.interfaces.IAny)
	 */
	@Override
	public BL semanticEquals(IAny other) {
		
		BL retVal = new BL(false);
		if (other == null)
            return null;
        else if (this.isNull() && other.isNull())
            retVal.setNullFlavor(this.getNullFlavor().getCode().getCommonParent(other.getNullFlavor().getCode()));
        else if (this.isNull() ^ other.isNull())
            retVal.setNullFlavor(NullFlavor.NotApplicable);
        else
        {
        	if(other instanceof ST)
        	{
        		ST otherSt = (ST)other;
        		retVal.setValue(otherSt.getValue() != null ? otherSt.getValue().equals(this.getValue()) : this.getValue() == null);
        	}
        	else if(other instanceof ED)
        	{
        		ED otherEd = (ED)other; // ST may also be equal to ED when value is text/plain
        		if (otherEd.getMediaType() == "text/plain")
        			retVal.setValue(otherEd.getData() != null ? otherEd.getData().equals(this.getValue().getBytes()) : this.getValue() == null);
        		else
        			retVal.setValue(false);
        	}
        }
        return retVal;
	}
	
	
}
