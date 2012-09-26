/* 
 * Copyright 2008/2011 Mohawk College of Applied Arts and Technology
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
 * User: Jaspinder Singh
 * Date: 06-28-2011
 */

package ca.marc.everest.datatypes;

import ca.marc.everest.annotations.ConformanceType;
import ca.marc.everest.annotations.Property;
import ca.marc.everest.annotations.PropertyType;
import ca.marc.everest.annotations.Structure;
import ca.marc.everest.annotations.StructureType;
import ca.marc.everest.datatypes.generic.CS;
import ca.marc.everest.datatypes.generic.SET;
import ca.marc.everest.datatypes.NullFlavor;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

import java.io.StringWriter;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;

/**
 * Mailing at a home or office addresses is primarily used to communicate data that will allow printing mail labels.
 */
@Structure(name="AD", structureType=StructureType.DATATYPE)
public class AD extends ANY {

	/** A sequence of address parts that makes up an address */
	private List<ADXP> parts;
	
	/** A code that identifies the use for the AD **/
	private SET<CS<PostalAddressUse>> use;
	
	/** Specifies whether or not the order of the address parts is known. **/
	private boolean isNotOrdered;
	
	/** Specifies the useable period for the address **/
	private GTS useablePeriod;
	
	/**
	 * Instantiates a new AD.
	 */
	public AD() {
		super();
		parts = new ArrayList<ADXP>();
	}
	
	/**
	 * Instantiate a new AD using a list of address parts.
	 * @param parts list of address parts to create an AD from.
	 */
	public AD(Collection<ADXP> parts) {
		super();
		this.parts = new ArrayList<ADXP>(parts);
		this.use = new SET<CS<PostalAddressUse>>();
	}
	
	/**
	 * Instantiate a new instance of AD with a specified use and address parts.
	 * @param use the specified use for the address.
	 * @param parts parts that make up the address.
	 */
	public AD(CS<PostalAddressUse> use, Collection<ADXP> parts) {
		super();
		this.parts = new ArrayList<ADXP>(parts);
		this.use = new SET<CS<PostalAddressUse>>(use);
	}
	
	
	@Override
	public CS<NullFlavor> getNullFlavor() {
		// TODO Auto-generated method stub
		return null;
	}
	
	/**
	 * Gets the sequence of address parts that make up the address.
	 * @return the parts that make up the address.
	 */
	@Property(name = "part", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.OPTIONAL, genericSupplier = { ADXP.class })
	public List<ADXP> getPart()
	{
		return this.parts;
	}
	
	/**
	 * Sets the sequence of address parts that make up the address.
	 * @param part the parts that make up the address.
	 */
	public void setPart(List<ADXP> part)
	{
		this.parts = part;
	}
	
	/**
	 * Gets the postal address use codes.
	 * @return the postal address use codes.
	 */
	@Property(name = "use", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public SET<CS<PostalAddressUse>> getUse()
	{
		return this.use;
	}
	
	/**
	 * A boolean value specifying whether or not the order of the address parts is known.
	 * @return true if the order of the address parts is not known, false otherwise.
	 */
	@Property(name = "isNotOrdered", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public boolean isNotOrdered()
	{
		return this.isNotOrdered;
	}
	
	/**
	 * Sets the timing specification indicating the time when this address is valid
	 * @return
	 */
	public GTS getUseablePeriod() { return this.useablePeriod; }
	/**
	 * Gets the timing specification indiciating the time when this address is valid
	 * @param value
	 */
	public void setUseablePeriod(GTS value) { this.useablePeriod = value; }
	
	/**
	 * Determines whether or not <code>n</code> conforms to the AD.Basic flavor.
	 * @param n The AD to check for AD.Basic conformance.
	 * @return true if the AD conforms to the AD.Basic flavor, false otherwise.
	 */
	public static boolean isValidBasicFlavor(AD n)
	{
		boolean isBasic = true;
		
		for(ADXP part : n.getPart())
		{
			if (part.getPartType() != AddressPartType.City || 
					part.getPartType() != AddressPartType.State)
			{
				isBasic = false;
				break;
			}
		}
		
		return isBasic;
	}
	
	@Override
	public String toString()
	{
		StringWriter writer = new StringWriter();
		
		for(ADXP part : parts) 
		{
			if (part.getPartType() == null)
			{
				writer.write(part.getValue());
			}
			else
			{
				switch(part.getPartType())
				{
					case Delimiter:
						if (part.getValue() == null || part.getValue().trim().equals(""))
						{
							writer.write(System.getProperty("line.sperator"));
						}
						else
						{
							writer.write(part.getValue());
						}
						break;
					default:
						writer.write(part.getValue());
						break;
				}
			}
		}
		
		return writer.toString();
	}
	
	/**
	 * Validate the address.
	 * @return True if there is at least one address part or, alternatively, a null flavor is set.
	 */
	public boolean validate()
	{
		return (getNullFlavor()!= null)^(parts.size() > 0);
	}
	
	/**
	 * Extended validation function that returns the detected issues with a particular
	 * instance of the AD data type
	 */
	public Collection<IResultDetail> validateEx()
	{
		ArrayList<IResultDetail> retVal = new ArrayList<IResultDetail>();
		if(!((this.getNullFlavor() != null) ^ (this.parts.size() > 0)))
				retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "AD", "NullFlavor must be specified, or more than one Part must be present", null));
		return retVal;
	}
	
}
