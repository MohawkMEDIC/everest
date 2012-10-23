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
 * Date: 06-29-2011
 */
package ca.marc.everest.datatypes.generic;

import java.util.Collection;
import java.util.List;

import ca.marc.everest.datatypes.*;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.ICodedSimple;
import ca.marc.everest.datatypes.interfaces.IPredicate;
import ca.marc.everest.formatters.FormatterUtil;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;
import ca.marc.everest.annotations.*;

/**
 * Coded data in its simplest form where only the code is not predetermined.
 * <p>
 * The code system and code system version are implied and fixed by the context in which the CS value occurs
 * </p>
 */
@Structure(name = "CS", structureType = StructureType.DATATYPE, defaultTemplateType = java.lang.String.class)
public class CS<T> extends ANY implements ICodedSimple {

	// backing field for code
	private T m_code;
	
	/**
	 * Creates a new instance of the CS class
	 */
	public CS() { super(); }
	/**
	 * Creates a new instance of the CS class with the specified code
	 * @param code The code mnemonic for the CS
	 */
	public CS(T code) { super(); this.setCode(code); }
	
	/**
	 * Gets the code value of the CS
	 */
	@Property(name = "code", conformance = ConformanceType.MANDATORY, propertyType = PropertyType.STRUCTURAL)
	@Override
	public T getCode() { return this.m_code; }
	/**
	 * Sets the code value of this CS.
	 * @param value The new value of the code field
	 */
	@Override
	public void setCode(Object value) {
		this.setCodeEx((T)value);
	}
	/**
	 * Sets the code value of the CS as bound by the generic parameter
	 */
	public void setCodeEx(T value)
	{
		this.m_code = (T)value;
	}
	/**
	 * Validates that the code is valid
	 * <p>
	 * The coded simple is valid if:
	 * </p>
	 * <ul>
	 * 	<li>NullFlavor is specified, XOR</li>
	 * <li><ul>
     *   <li>Code is specified, AND</li>
     *   <li>if Code is not an IEnumeratedVocabulary, then CodeSystem is specified</li>
     *  	</ul>
     * </li>
	 * </ul>
	 * 
	 */
	@Override
	public boolean validate()
	{
		if (this instanceof CV<?>) // Special case for non CS
            return super.validate();
        else
            return (this.m_code != null) ^ (this.getNullFlavor() != null) && super.validate();
	}
	/**
	 * @see ca.marc.everest.datatypes.ANY#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		 List<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();
         if (this instanceof CV<?>)
             return retVal;
         else if (!((this.getCode() == null) ^ (!this.isNull())))
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "CS", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
         return retVal;
	}
	/**
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		return this.isNull() ? "" : this.getCode() == null ? "" : this.getCode().toString();
	}
	
	/**
	 * Convert this type to an enumerated vocabulary
	 */
	public T toEnumeratedVocabulary()
	{
		return this.getCode();
	}

	/**
	 * Get the matcher predicate for the object
	 */
	public IPredicate<CS<T>> getMatcherPredicate(CS<T> scope)
	{
		return new Predicate<CS<T>>(scope) {
			public boolean match(CS<T> other)
			{
				if ((this.getScopeValue().getCode().equals(other.getCode())) ^ (this.getScopeValue().getNullFlavor() == other.getNullFlavor() && this.getScopeValue().getNullFlavor() != null))
	                return true;
	            return false; 
			}
		};
	}
	/**
	 * @see ca.marc.everest.datatypes.ANY#semanticEquals(ca.marc.everest.datatypes.interfaces.IAny)
	 */
	@Override
	public BL semanticEquals(IAny other) {
		BL retVal = new BL();
		if (other == null)
            return null;
        else if (this.isNull() && other.isNull())
            retVal.setNullFlavor(this.getNullFlavor().getCode().getCommonParent(other.getNullFlavor().getCode()));
        else if (this.isNull() ^ other.isNull())
            retVal.setNullFlavor(NullFlavor.NotApplicable);
        else if (!(other instanceof ICodedSimple))
            retVal = BL.FALSE;
        else
        	retVal.setValue(!this.isNull() && !other.isNull() && this.getCode() != null && ((ICodedSimple)other).getCode() != null && FormatterUtil.toWireFormat(this.getCode()).equals(FormatterUtil.toWireFormat(((ICodedSimple)other).getCode())));
		return retVal;
	}
	/**
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_code == null) ? 0 : m_code.hashCode());
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
		CS other = (CS) obj;
		if (m_code == null) {
			if (other.m_code != null)
				return false;
		} else if (!m_code.equals(other.m_code))
			return false;
		return true;
	}
	
	
}
