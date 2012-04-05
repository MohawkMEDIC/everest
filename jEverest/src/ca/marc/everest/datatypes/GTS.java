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
 * Date: 09-02-2011
 */
package ca.marc.everest.datatypes;

import java.util.*;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.*;

/**
 * A general timing specification represents a set of points in time specifying
 * the timing of events and actions.
 */
@Structure(name = "GTS", structureType = StructureType.DATATYPE)
public class GTS extends ANY {

	// backing field for hull
	private SXCM<TS> m_hull;
	
	/**
	 * Creates a new instance of the GTS class
	 */
	public GTS() {}
	/**
	 * Creates a new instance of the GTS class with the specified
	 * EIVL as its hull
	 */
	public GTS(EIVL<TS> hull)
	{
		super();
		this.m_hull = hull;
	}
	/**
	 * Creates a new instance of the GTS class with the specified
	 * SXPR as its hull
	 */
	public GTS(SXPR<TS> hull)
	{
		super();
		this.m_hull = hull;
	}
	/**
	 * Creates a new instance of the GTS class with the specified
	 * IVL as its hull
	 */
	public GTS(IVL<TS> hull)
	{
		super();
		this.m_hull = hull;
	}
	/**
	 * Creates a new instance of the GTS class with the specified
	 * PIVL as its hull
	 */
	public GTS(PIVL<TS> hull)
	{
		super();
		this.m_hull = hull;
	}
	/**
	 * Creates a new instance of the GTS class with the specified
	 * SXCM as its hull
	 */
	public GTS(SXCM<TS> hull)
	{
		super();
		this.m_hull = hull;
	}
	
	/**
	 * Gets the Hull value of this GTS
	 */
	@Property(name = "hull", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public SXCM<TS> getHull() { return this.m_hull; }
	/**
	 * Sets the Hull value of this GTS
	 */
	public void setHull(SXCM<TS> hull) { this.m_hull = hull; }

	/**
	 * Flavor validator for the bounded PIVL class
	 */
	@Flavor(name = "GTS.BOUNDEDPIVL")
	public static boolean IsBoundedPivlFlavor(GTS gts)
	{
		return (gts.getHull() instanceof PIVL<TS>) ^ (gts.getNullFlavor() != null);
	}
	
	/**
	 * Validate this instance of GTS
	 */
	@Override
	public boolean validate()
	{
		return (this.getHull() != null) ^ (this.getNullFlavor() != null) &&
			((this.getHull() != null && this.getHull().validate()) || this.getHull() == null);
	}
	
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_hull == null) ? 0 : m_hull.hashCode());
		return result;
	}
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		GTS other = (GTS) obj;
		if (m_hull == null) {
			if (other.m_hull != null)
				return false;
		} else if (!m_hull.equals(other.m_hull))
			return false;
		return true;
	}

}
