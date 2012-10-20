package ca.marc.everest.datatypes;

import java.util.Arrays;
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.Structure;
import ca.marc.everest.annotations.StructureType;
import ca.marc.everest.datatypes.generic.CS;
import ca.marc.everest.datatypes.generic.SET;
import ca.marc.everest.datatypes.interfaces.IPredicate;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Represents a name for a person
 */
@Structure(name = "PN", structureType = StructureType.DATATYPE)
public class PN extends EN {
	
	/**
	 * Creates a new instance of the PN class
	 */
	public PN() {
		super();
	}

	/**
	 * Creates a new instance of the PN class with the specified  parts
	 */
	public PN(Collection<ENXP> parts) {
		super(parts);
	}
	
	/**
	 * Creates a new instance of the PN class with the specified use and parts
	 */
	public PN(EntityNameUse use, Collection<ENXP> parts) {
		super(use, parts);
	}

	/**
	 * Creates a name with simple content
	 */
	public PN(String simpleContent)
	{
		this(Arrays.asList(new ENXP[] { new ENXP(simpleContent) }));
	}
	
	/**
	 * Creates a name with the specified use and parts
	 */
	public PN createPN(EntityNameUse use, ENXP...parts)
	{
		return new PN(use, Arrays.asList(parts));
	}
	
	/**
	 * Create an PN instance from an EN instance (up-cast)
	 */
	public static PN fromEN(EN name)
	{
        PN retVal = new PN();
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

	/* (non-Javadoc)
	 * @see ca.marc.everest.datatypes.EN#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		List<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();
        for (ENXP part : this.getParts())
            if (part.getQualifier() != null && part.getQualifier().find(
            		new IPredicate<CS<EntityNamePartQualifier>>()
            		{
            			public boolean match(CS<EntityNamePartQualifier> other)
            			{
            				return other.getCode().equals(EntityNamePartQualifier.LegalStatus);
            			}
            		}) != null)
                retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "PN", String.format(EverestValidationMessages.MSG_INVALID_VALUE, "LegalStatus", "Qualifier"), null));
        return retVal;
	}
	/**
	 * @see ca.marc.everest.datatypes.EN#validate()
	 */
	@Override
	public boolean validate() {
		boolean isValid = this.isNull() ^ (this.getParts().size() > 0);
        for(ENXP part : this.getParts())
            isValid &= part.getQualifier() == null || part.getQualifier().find(
            		new IPredicate<CS<EntityNamePartQualifier>>()
            		{
            			public boolean match(CS<EntityNamePartQualifier> other)
            			{
            				return other.getCode().equals(EntityNamePartQualifier.LegalStatus);
            			}
            		}) == null;
        return isValid;
	}
	
	
}
