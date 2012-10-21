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
 * Date: 10-21-2012
 */
package ca.marc.everest.datatypes;

import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.CD;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Represents a string with an option code attached. 
 * <p>
 * The text must always be present, however the code is optional, and often the code specified is a local code
 * </p>
 */
@Structure(name = "SC", structureType = StructureType.DATATYPE)
public class SC extends ST {

	private CD<String> m_code;
	
	/**
	 * Creates a new instance of the SC type
	 */
	public SC() { super(); }
	/**
	 * Creates a new instance of the SC type with the specified value
	 * @param value The initial string value the SC should carry
	 */
	public SC(String value) { super(value); }
	/**
	 * Creates a new instance of the SC type with the specified value in the specified language
	 * @param value THe initial string value of the SC type
	 * @param language The ISO 639 language code the SC type should carry
	 */
	public SC(String value, String language) { super(value, language); }
	/**
	 * Creates a new instance of the SC type with the specified value in the specified language having the 
	 * specified 
	 * @param value The string value of the text
	 * @param language The language of the text
	 * @param code A codified concept describing the string value
	 */
	public SC(String value, String language, CD<String> code) {
		super(value, language);
		this.m_code = code;
	}
	
	/**
	 * Gets the contained code attached to the string
	 */
	@Property(name="code", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public CD<String> getCode() {
		return this.m_code;
	}
	/**
	 * Sets the contained code attached to the string
	 */
	public void setCode(CD<String> value) {
		this.m_code = value;
	}
	
	/**
	 * Determine if the specified instance of SC is a valid NT flavor
	 */
	@Flavor(name="SC.NT")
	public static boolean isValidNtFlavor(SC sc)
	{
		return sc.getTranslation() == null || sc.getTranslation().isNull() || sc.getTranslation().isEmpty();
	}
	/* (non-Javadoc)
	 * @see ca.marc.everest.datatypes.ST#validate()
	 */
	@Override
	public boolean validate() {
		// If code is present there must be a value otherwise a null flavor must be specified
        boolean isValid = super.validate();
        // If a code is specified, a value must also be specified and code cannot have a nullFlavor
        isValid &= this.m_code != null && this.getValue() != null && !this.m_code.isNull() || this.m_code == null;
        // No code is specified when a null flavor is specified
        isValid &= this.m_code == null && this.isNull() || !this.isNull();
        // Code is not permitted to have an original text
        isValid &= this.m_code != null && this.m_code.getOriginalText() == null || this.m_code == null;
        isValid &= this.m_code != null && this.m_code.validate() || this.m_code == null;
        return isValid;
	}
	/* (non-Javadoc)
	 * @see ca.marc.everest.datatypes.ST#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		 List<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();

         if (this.m_code != null && this.getValue() == null)
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "SC", "When Code is specified, Value must also be specified", null));
         if (this.m_code != null && this.m_code.isNull())
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "SC", String.format(EverestValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "NullFlavor", "Code"), null));
         if (this.isNull() && (this.m_code != null || this.getValue() != null))
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "SC", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
         if (this.m_code != null && this.m_code.getOriginalText() != null)
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "SC", String.format(EverestValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "OriginalText", "Code"), null));
         if(this.m_code != null)
             retVal.addAll(this.m_code.validateEx());
         return retVal;
	}
	/*
	 * (non-Javadoc)
	 * @see ca.marc.everest.datatypes.ST#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_code == null) ? 0 : m_code.hashCode());
		return result;
	}
	/*
	 * (non-Javadoc)
	 * @see ca.marc.everest.datatypes.ST#equals(java.lang.Object)
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		SC other = (SC) obj;
		if (m_code == null) {
			if (other.m_code != null)
				return false;
		} else if (!m_code.equals(other.m_code))
			return false;
		return true;
	}
	
	
	
}
