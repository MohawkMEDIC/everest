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
import ca.marc.everest.datatypes.generic.CS;
import ca.marc.everest.datatypes.generic.SET;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * A trivial name is a restriction of the EN whereby it may carry only one part
 */
@Structure(name = "TN", structureType = StructureType.DATATYPE)
public class TN extends EN {

	/**
	 * Creates a new instance of the TN datatype
	 */
	public TN() { super(); }
	/**
	 * Creates a new instance of the TN datatype with the specified value
	 * @param value The value of the TN
	 */
	public TN(String value)
	{
		super();
		this.getParts().add(new ENXP(value));
	}
	
	/**
	 * Create an ON instance from an EN instance (up-cast)
	 */
	public static TN fromEN(EN name)
	{
        TN retVal = new TN();
        retVal.getParts().addAll(name.getParts());
        retVal.setControlActExt(name.getControlActExt());
        retVal.setControlActRoot(name.getControlActRoot()) ;
        retVal.setFlavorId(name.getFlavorId());
        retVal.setNullFlavor(name.getNullFlavor() != null ? (CS<NullFlavor>)name.getNullFlavor().shallowCopy() : null);
        retVal.setUpdateMode(name.getNullFlavor() != null ? (CS<UpdateMode>)name.getUpdateMode().shallowCopy() : null);
        retVal.setValidTimeHigh(name.getValidTimeHigh());
        retVal.setValidTimeLow(name.getValidTimeLow());
        return retVal;
	}
	
	/**
	 * Validate this TN returning detected issues
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
        List<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();
        if (!this.isNull())
        {
            if (this.getParts().size() != 1)
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "TN", EverestValidationMessages.MSG_INSUFFICIENT_TERMS, null));
            if (this.getPart(0).getType() != null)
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "TN" , String.format(EverestValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "Type", "Part"), null));
            if(this.getPart(0).getQualifier() != null)
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "TN" , String.format(EverestValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "Qualifier", "Part"), null));
        }
        return retVal;
	}
	/**
	 * Validate this TN
	 */
	@Override
	public boolean validate() {
        boolean isBasicValid = super.validate();
        if (!this.isNull())
            return isBasicValid && (this.getParts().size() == 1 && this.getPart(0).getType() == null && this.getPart(0).getQualifier() == null);
        return isBasicValid;
	}
	
	
}
