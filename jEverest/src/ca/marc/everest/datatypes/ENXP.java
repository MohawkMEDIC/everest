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

import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.CS;
import ca.marc.everest.datatypes.generic.SET;
import ca.marc.everest.formatters.FormatterUtil;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * A character string token representing a part of a name
 */
@Structure(name = "ENXP", structureType = StructureType.DATATYPE)
public class ENXP extends ANY {

	// Backing field for value
	private String m_value;
	// backing field for type
	private CS<EntityNamePartType> m_type;
	// Backing field for code
	private String m_code;
	// backing field for code system
	private String m_codeSystem;
	// Backing field for code system version
	private String m_codeSystemVersion;
	// backing field for qualifier
	private SET<CS<EntityNamePartQualifier>> m_qualifier;
	/**
	 * Creates a new instance of the ENXP class
	 */
	public ENXP() { super(); }
	/**
	 * Creates a new instance of the ENXP class with the specified value
	 * @param value The initial value of the ENXP instance
	 */
	public ENXP(String value) { 
		super();
		this.m_value = value;
	}
	/**
	 * Creates a new instance of the ENXP class with the specified value and type
	 * @param value The value of the name part
	 * @param type The type of component the name part represents
	 */
	public ENXP(String value, EntityNamePartType type)
	{
		this(value);
		this.m_type = new CS<EntityNamePartType>(type);
	}
	/**
	 * Gets the value of the name part
	 */
	@Property(name = "value", propertyType= PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public String getValue() {
		return this.m_value;
	}
	/**
	 * Sets the value of the name part
	 */
	public void setValue(String value) {
		this.m_value = value;
	}
	/**
	 * Gets the type of the name part
	 */
	@Property(name = "type", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public CS<EntityNamePartType> getType() {
		return this.m_type;
	}
	/**
	 * Sets the type of this name part
	 */
	public void setType(CS<EntityNamePartType> value) {
		this.m_type = value;
	}
	/**
	 * Gets the code for this name part
	 */
	@Property(name = "code", propertyType = PropertyType.STRUCTURAL, conformance= ConformanceType.OPTIONAL)
	public String getCode() {
		return this.m_code;
	}
	/**
	 * Sets the code for this name part
	 */
	public void setCode(String value) {
		this.m_code = value;
	}
	/**
	 * Gets the code system for this name part
	 */
	@Property(name = "codeSystem", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public String getCodeSystem() {
		return this.m_codeSystem;
	}
	/**
	 * Sets the code system for this name part
	 */
	public void setM_codeSystem(String value) {
		this.m_codeSystem = value;
	}
	/**
	 * Gets the code system version for this name part
	 */
	@Property(name = "codeSystemVersion", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public String getCodeSystemVersion() {
		return m_codeSystemVersion;
	}
	/**
	 * Sets the code system version for this name part
	 */
	public void setCodeSystemVersion(String value) {
		this.m_codeSystemVersion = value;
	}
	/**
	 * @return the m_qualifier
	 */
	@Property(name = "qualifier", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public SET<CS<EntityNamePartQualifier>> getQualifier() {
		return this.m_qualifier;
	}
	/**
	 * @param m_qualifier the m_qualifier to set
	 */
	public void setQualifier(SET<CS<EntityNamePartQualifier>> value) {
		this.m_qualifier = value;
	}
	/** 
	 * @see ca.marc.everest.datatypes.ANY#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		List<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();

        if (this.isNull() && this.m_value != null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ENXP", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        if (this.m_codeSystem != null && this.m_code == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ENXP", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "CodeSystem", "Code"), null));
        if(this.m_qualifier != null && this.m_type != null)
	        for(CS<EntityNamePartQualifier> q : this.m_qualifier)
	            if (!q.getCode().canQualifyPartType(this.m_type.getCode()))
	                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ENXP", String.format("Qualifier must be one of '%1' when type is populated with '%2'", FormatterUtil.toWireFormat(q.getCode().getAllowedPartTypes()), this.getType().getCode()), null));

        return retVal;        
	}
	/**
	 * @see ca.marc.everest.datatypes.HXIT#validate()
	 */
	@Override
	public boolean validate() {
		boolean retVal = this.isNull() ^ (this.m_value != null && ((this.m_codeSystem != null && this.m_code != null) ^ (this.m_code == null)));
        if(this.m_qualifier != null && this.m_type != null)
			for(CS<EntityNamePartQualifier> qlfr : this.m_qualifier)
	            retVal &= qlfr.getCode().canQualifyPartType(this.m_type.getCode());
        return retVal;
	}
	/**
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		return this.m_value;
	}
	
	
}
