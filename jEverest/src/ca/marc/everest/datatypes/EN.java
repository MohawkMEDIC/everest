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

import java.io.StringWriter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.CS;
import ca.marc.everest.datatypes.generic.SET;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Represents a name for an entity (person, organization, place or thing)
 */
@Structure(name = "EN", structureType = StructureType.DATATYPE)
public class EN extends ANY {

	// backing field for parts
	private List<ENXP> m_parts = new ArrayList<ENXP>();
	// backing field for use
	private SET<CS<EntityNameUse>> m_use;
	
	/**
	 * Creates a new instance of EN
	 */
	public EN() { super(); }
	/**
	 * Creates a new instance of EN with the given use and parts
	 * @param use The intended use of the entity name
	 * @param parts The parts which compose the EN
	 */
	public EN(EntityNameUse use, Collection<ENXP> parts) { 
		this(parts);
		this.m_use = new SET<CS<EntityNameUse>>(new CS<EntityNameUse>(use));
	}
	/**
	 * Creates a new instance of EN with the specified parts
	 */
	public EN(Collection<ENXP> parts)
	{
		this.m_parts.addAll(parts);
	}
	/**
	 * Creates an EN instance with the specified use and parts
	 */
	public static EN createEN(EntityNameUse use, ENXP... parts)
	{
		return new EN(use, Arrays.asList(parts));
	}
	/**
	 * Gets a list of parts that make up this entity name
	 */
	@Property(name = "part", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.NONSTRUCTURAL, minOccurs = 0, maxOccurs = -1)
	public List<ENXP> getParts() {
		return this.m_parts;
	}
	/**
	 * Sets the list of parts that make up this entity name
	 */
	public void setParts(List<ENXP> value) {
		this.m_parts = value;
	}
	/**
	 * Gets the valid uses of this entity name
	 */
	@Property(name = "use", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.NONSTRUCTURAL)
	public SET<CS<EntityNameUse>> getUse() {
		return this.m_use;
	}
	/**
	 * Sets the valid uses of this entity name
	 */
	public void setUse(SET<CS<EntityNameUse>> value) {
		this.m_use = value;
	}
	/**
	 * @see ca.marc.everest.datatypes.ANY#semanticEquals(ca.marc.everest.datatypes.interfaces.IAny)
	 */
	@Override
	public BL semanticEquals(IAny other) {
		BL baseSem = super.semanticEquals(other);
        if (!baseSem.toBoolean())
            return baseSem;

        boolean result = true;
        EN otherEn = (EN)other;
        if ((this.m_parts == null) ^ (otherEn.m_parts == null))
            return BL.FALSE;
        else if (this.m_parts == otherEn.m_parts)
            return BL.TRUE;
        else
        {
            for (int i = 0; i < this.m_parts.size(); i++)
                result &= otherEn.m_parts.get(i).equals(this.m_parts.get(i));
            return BL.fromBoolean(result && this.m_parts.size() == otherEn.m_parts.size());
        }
	}
	/**
	 * @see ca.marc.everest.datatypes.ANY#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
        List<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();
        if (this.isNull() && this.m_parts.size() > 0)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "EN", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        else if (this.m_parts.size() == 0 && !this.isNull())
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "EN", EverestValidationMessages.MSG_NULLFLAVOR_MISSING, null));

        // Validate parts
        for(ENXP pt : this.m_parts)
            retVal.addAll(pt.validateEx());

        return retVal;
	}
	/**
	 * @see ca.marc.everest.datatypes.HXIT#validate()
	 */
	@Override
	public boolean validate() {
		return (this.m_parts.size() > 0) ^ this.isNull();
	}
	
	/**
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		StringWriter sw = new StringWriter();

        // Iterate through the sequence
        for (ENXP p : this.m_parts)
        {
            if (p.getType() == null)
                sw.write(p.getValue());
            else
                switch (p.getType().getCode())
                {
                    case Delimiter:
                        sw.write(p.getValue() == null || p.getValue() == "" ? "\n" : p.getValue());
                        break;
                    default:
                        sw.write(p.getValue());
                        break;
                }
        }

        return sw.toString();	
	}
	
	
}
