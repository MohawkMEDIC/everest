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

import ca.marc.everest.datatypes.generic.*;
import ca.marc.everest.datatypes.interfaces.IPointInTime;
import ca.marc.everest.datatypes.interfaces.ISet;
import ca.marc.everest.datatypes.interfaces.ISetComponent;
import ca.marc.everest.datatypes.interfaces.ITelecommunicationsAddress;
import ca.marc.everest.annotations.*;

/**
 * Represents a telecommunications address such as a telephone number, email address,
 * fax, or url that can be contacted
 */
public class TEL extends PDV<String> implements ITelecommunicationsAddress {

	// backing field for use
	private SET<CS<TelecommunicationsAddressUse>> m_use;
	// backing field for capabilities
	private SET<CS<TelecommunicationsCapabilities>> m_capabilities;
	// backing field for usable period
	private GTS m_usablePeriod;
	
	/**
	 * Creates a new instance of the telecommunications address use 
	 */
	public TEL() { super(); }
	/**
	 * Creates a new instance of the telecommunications address with the
	 * specified value
	 * @param value The value of the telecommunications address
	 */
	public TEL(String value) { super(value); }
	/**
	 * Creates a new instance of the telecommunications address with the
	 * specified value and use
	 * @param value The value of the telecommunications address
	 * @param use The use of the telecommunications address
	 */
	public TEL(String value, TelecommunicationsAddressUse use) {
		this(value);
		this.m_use = new SET<CS<TelecommunicationsAddressUse>>(new CS<TelecommunicationsAddressUse>(use));
	}
	/**
	 * Creates a new instance of the telecommunications address with the
	 * specified value and use
	 * @param value The value of the telecommunications address
	 * @param use The uses of the telecommunications address
	 */
	public TEL(String value, Iterable<TelecommunicationsAddressUse> use)
	{
		this(value);
		this.m_use = new SET<CS<TelecommunicationsAddressUse>>();
		for(TelecommunicationsAddressUse u : use)
			this.m_use.add(new CS<TelecommunicationsAddressUse>(u));
	}
	/**
	 * Gets a set of TelecommunicationsAddressUse codes that describe the 
	 * circumstances under which the telecommunications address can be used
	 */
	@Override
	public ISet<CS<TelecommunicationsAddressUse>> getUse() { return this.m_use; }
	/**
	 * Sets the set of TelecommunicationsAddressUse codes that describe the
	 * circumstances under which the telecommunications address can be used
	 */
	public void setUse(SET<CS<TelecommunicationsAddressUse>> value) { this.m_use = value; }
	/**
	 * Populates the use based on a list of TelecommunicationsAddressUse codes
	 * @param use
	 */
	public void setUse(TelecommunicationsAddressUse... use) {
		this.m_use = new SET<CS<TelecommunicationsAddressUse>>();
		for(TelecommunicationsAddressUse u : use)
			this.m_use.add(new CS<TelecommunicationsAddressUse>(u));
	}
	/**
	 * Gets a set that describes the capabilities of the device
	 * attached to the telecommunications address
	 */
	public SET<CS<TelecommunicationsCapabilities>> getCapabilities() { return this.m_capabilities; }
	/**
	 * Sets a set that describes the capabilities of the device
	 * attached to the telecommunications address
	 */
	public void setCapabilities(SET<CS<TelecommunicationsCapabilities>> value) { this.m_capabilities = value; }
	/**
	 * Sets a set that describes the capabilities of the device
	 * attached to the telecommunications address
	 */
	public void setCapabilities(TelecommunicationsCapabilities... value) {
		this.m_capabilities = new SET<CS<TelecommunicationsCapabilities>>();
		for(TelecommunicationsCapabilities u : value)
			this.m_capabilities.add(new CS<TelecommunicationsCapabilities>(u));	
	}
	/**
	 * Gets a genegral timing specification that describes the segments of
	 * time that a telecommunications address is available
	 */
	@Override
	public ISetComponent<IPointInTime> getUseablePeriod() { return this.m_usablePeriod;  }
	/**
	 * Sets a general timing specification that describes the segments
	 * of time that a telecommunications address is available.
	 */
	public void setUsablePeriod(GTS value) { this.m_usablePeriod = value; }
	
	/**
	 * Validate an instance of TEL to TEL.URL
	 */
	@Flavor(name = "TEL.URL")
    public static boolean isValidUrlFlavor(TEL tel)
    {
        // Nothing to validate
        if (tel.getValue() == null) return true;

        String[] allowedSchemes = { "file:", "nfs://", "ftp://", "cid://", "http://", "https://" };
        boolean valid = tel.getUse() == null;
        for (String s : allowedSchemes)
            if (tel.getValue() != null && tel.getValue().startsWith(s))
                return valid;
        return false;
    }

    /**
     * Validate an instance of TEL to TEL.URI
     */
    @Flavor(name = "TEL.URI")
    public static boolean isValidUriFlavor(TEL tel)
    {
        return isValidUrlFlavor(tel);
    }

    /**
     * TEL.Person Validator
     */
    @Flavor(name = "TEL.PERSON")
    public static boolean isValidPersonFlavor(TEL tel)
    {
        return isValidPhoneFlavor(tel) || isValidEMailFlavor(tel);
    }

    /**
     * TEL.Phone validator
     */
    @Flavor(name = "TEL.PHONE")
    public static boolean isValidPhoneFlavor(TEL tel)
    {
        String[] validSchemes = { "tel", "x-text-fax", "x-text-tel" };
        for (String s : validSchemes)
            if (tel.getValue() != null && tel.getValue().startsWith(s))
                return true;
        return false;
    }

    /**
     * TEL.Email validator
     */
    @Flavor(name = "TEL.EMAIL")
    public static boolean isValidEMailFlavor(TEL tel)
    {
        return tel.getValue() != null && tel.getValue().startsWith("mailto:");
    }
	


}
