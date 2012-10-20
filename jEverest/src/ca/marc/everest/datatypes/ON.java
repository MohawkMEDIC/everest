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
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.CS;
import ca.marc.everest.datatypes.generic.Predicate;
import ca.marc.everest.datatypes.generic.SET;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * An organization name
 */
@Structure(name = "ON", structureType = StructureType.DATATYPE)
public class ON extends EN {

	/**
	 * Creates a new instance of the ON type
	 */
	public ON() { super(); }
	/**
	 * Creates a new instance of the ON type with the specified use and parts
	 * @param use The use of the organization name
	 * @param parts The parts of the organization name
	 */
	public ON(EntityNameUse use, Collection<ENXP> parts) { super(use, parts); }
	/**
	 * Creates an ON with the specified use and parts
	 */
	public static ON createON(EntityNameUse use, ENXP...parts)
	{
		return new ON(use, Arrays.asList(parts));

	}
	/**
	 * @see ca.marc.everest.datatypes.EN#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		List<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();
		
        List<EntityNamePartType> disallowedPartTypes = Arrays.asList(new EntityNamePartType[] 
            { 
                EntityNamePartType.Given, 
                EntityNamePartType.Family 
            });
        List<EntityNameUse> disallowedUses = Arrays.asList(new EntityNameUse[]
        {
            EntityNameUse.Indigenous,
            EntityNameUse.Pseudonym,
            EntityNameUse.Anonymous,
            EntityNameUse.Artist,
            EntityNameUse.Religious,
            EntityNameUse.MaidenName
        });

        //object obj = disallowedQualifiers.Find(o => Use.Find(u => u.Code.Equals(o)) != null);
        if(this.getUse() != null)
        	for(EntityNameUse u : disallowedUses)
        		if(this.getUse().findAll(new Predicate<CS<EntityNameUse>>(new CS<EntityNameUse>(u))
        				{
        					public boolean match(CS<EntityNameUse> other)
        					{
        						return other.getCode().equals(this.getScopeValue().getCode());
        					}
        				}).size() > 0)
        			retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ON", String.format(EverestValidationMessages.MSG_INVALID_VALUE, this.getUse(), "Use")));

        // Validate parts
        if(this.getParts() != null)
        	for(ENXP part : this.getParts())
        		for (EntityNamePartType t : disallowedPartTypes)
        			if(part.getType().getCode().equals(t))
        				retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ON", String.format(EverestValidationMessages.MSG_INVALID_VALUE, part.getType().getCode(), "Part.Type")));
        return retVal;
	}
	/* (non-Javadoc)
	 * @see ca.marc.everest.datatypes.EN#validate()
	 */
	@Override
	public boolean validate() {
		boolean isValid = super.validate();
        List<EntityNamePartType> disallowedPartTypes = Arrays.asList(new EntityNamePartType[] 
            { 
                EntityNamePartType.Given, 
                EntityNamePartType.Family 
            });
        List<EntityNameUse> disallowedUses = Arrays.asList(new EntityNameUse[]
        {
            EntityNameUse.Indigenous,
            EntityNameUse.Pseudonym,
            EntityNameUse.Anonymous,
            EntityNameUse.Artist,
            EntityNameUse.Religious,
            EntityNameUse.MaidenName
        });

        //object obj = disallowedQualifiers.Find(o => Use.Find(u => u.Code.Equals(o)) != null);
        if(this.getUse() != null)
        	for(EntityNameUse u : disallowedUses)
        		isValid &= this.getUse().findAll(new Predicate<CS<EntityNameUse>>(new CS<EntityNameUse>(u))
        				{
        					public boolean match(CS<EntityNameUse> other)
        					{
        						return other.getCode().equals(this.getScopeValue().getCode());
        					}
        				}).size() == 0;

        // Validate parts
        if(this.getParts() != null)
        	for(ENXP part : this.getParts())
        		for (EntityNamePartType t : disallowedPartTypes)
        			isValid &= !part.getType().getCode().equals(t);

        return isValid;
	}

	/**
	 * Create an ON instance from an EN instance (up-cast)
	 */
	public static ON fromEN(EN name)
	{
        ON retVal = new ON();
        retVal.getParts().addAll(name.getParts());
        retVal.setControlActExt(name.getControlActExt());
        retVal.setControlActRoot(name.getControlActRoot()) ;
        retVal.setFlavorId(name.getFlavorId());
        retVal.setNullFlavor(name.getNullFlavor() != null ? (CS<NullFlavor>)name.getNullFlavor().shallowCopy() : null);
        retVal.setUpdateMode(name.getNullFlavor() != null ? (CS<UpdateMode>)name.getUpdateMode().shallowCopy() : null);
        retVal.setUse(name.getUse() != null ? new SET<CS<EntityNameUse>>(name.getUse()) : null);
        retVal.setValidTimeHigh(name.getValidTimeHigh());
        retVal.setValidTimeLow(name.getValidTimeLow());
        return retVal;
	}
	
}
