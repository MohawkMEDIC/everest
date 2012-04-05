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
import ca.marc.everest.annotations.*;

/**
 * Represents a telecommunications address such as a telephone number, email address,
 * fax, or url that can be contacted
 */
public class TEL extends PDV<String> {

	// backing field for use
	private SET<TelecommunicationsAddressUse> m_use;
	// backing field for capabilities
	private SET<TelecommunicationsCapabilities> m_capabilities;
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
		this(value, new SET<TelecommunicationsAddressUse>(use));
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
		this.m_use = new SET<TelecommunicationsAddressUse>(use);
	}
	/**
	 * Gets a set of TelecommunicationsAddressUse codes that describe the 
	 * circumstances under which the telecommunications address can be used
	 */
	public SET<TelecommunicationsAddressUse> getUse() { return this.m_use; }
	/**
	 * Sets the set of TelecommunicationsAddressUse codes that describe the
	 * circumstances under which the telecommunications address can be used
	 */
	public void setUse(SET<TelecommunicationsAddressUse> value) { this.m_use = value; }
	/**
	 * Gets a set that describes the capabilities of the device
	 * attached to the telecommunications address
	 */
	public SET<TelecommunicationsCapabilities> getCapabilities() { return this.m_capabilities; }
	/**
	 * Sets a set that describes the capabilities of the device
	 * attached to the telecommunications address
	 */
	public void setCapabilities(SET<TelecommunicationsCapabilities> value) { this.m_capabilities = value; }
	/**
	 * Gets a genegral timing specification that describes the segments of
	 * time that a telecommunications address is available
	 */
	public GTS getUsablePeriod() { return this.m_usablePeriod;  }
	/**
	 * Sets a general timing specification that describes the segments
	 * of time that a telecommunications address is available.
	 */
	public void setUsablePeriod(GTS value) { this.m_usablePeriod = value; }
	
	/**
	 * Validate an instance of TEL to TEL.URL
	 */
	@Flavor(name = "TEL.URL")
    public static boolean isUrlFlavor(TEL tel)
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
    public static boolean isUriFlavor(TEL tel)
    {
        return isUrlFlavor(tel);
    }

    /**
     * TEL.Person Validator
     */
    @Flavor(name = "TEL.PERSON")
    public static boolean isPersonFlavor(TEL tel)
    {
        return isPhoneFlavor(tel) || isEMailFlavor(tel);
    }

    /**
     * TEL.Phone validator
     */
    @Flavor(name = "TEL.PHONE")
    public static boolean isPhoneFlavor(TEL tel)
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
    public static boolean isEMailFlavor(TEL tel)
    {
        return tel.getValue() != null && tel.getValue().startsWith("mailto:");
    }

}
