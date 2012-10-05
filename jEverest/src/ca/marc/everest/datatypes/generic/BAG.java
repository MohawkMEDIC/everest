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
 * User: Justin Fyfe
 * Date: 10-01-2012
 */
package ca.marc.everest.datatypes.generic;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.ConformanceType;
import ca.marc.everest.annotations.Property;
import ca.marc.everest.annotations.PropertyType;
import ca.marc.everest.annotations.Structure;
import ca.marc.everest.annotations.StructureType;
import ca.marc.everest.datatypes.BL;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IBag;
import ca.marc.everest.datatypes.interfaces.ISemanticEquals;
import ca.marc.everest.interfaces.IGraphable;

/**
 * An unordered collection of values whereby each value can be contained more than once.
 */
@Structure(name = "BAG", structureType = StructureType.DATATYPE)
public class BAG<T extends IGraphable> extends COLL<T> implements IBag<T> {

	// Backing field for items
	private List<T> m_bag = new ArrayList<T>();
	
	/**
	 * Creates a new instance of the BAG datatype
	 */
	public BAG() { super(); }
	
	/**
	 * Creates a new instance of the BAG datatype with the specified items
	 * @param items The initial set of items to place into the BAG
	 */
	public BAG(Iterable<T> items) 
	{
		this();
		for(T item : items)
			this.add(item);
		
	}
	
	/**
	 * Create a bag of items
	 * @param items The items to seed the bag with
	 */
	public static <T extends IGraphable> BAG<T> createBAG(T... items)
	{
		return new BAG<T>(Arrays.asList(items));
	}

	/**
	 * Get items from the bag
	 */
	@Override
	@Property(name = "item", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.NONSTRUCTURAL)
	public Collection<T> getItems() {
		return this.m_bag;
	}

	/**
	 * Determine if this instance of BAG semantically equals another
	 * <p>Two non-null non null-flavored instances of BAG are semantically equal when both instances are empty or
	 * if both instances contain the same items regardless of order</p>
	 */
	@SuppressWarnings("unchecked")
	@Override
	public BL semanticEquals(IAny other) {
		BL baseSem = super.semanticEquals(other);
        if (!baseSem.toBoolean())
            return baseSem;

        
		
        BAG<T> otherBAG = (BAG<T>)other;
        if(otherBAG.isEmpty() && this.isEmpty())
            return BL.TRUE;
        else
        {
            boolean isEqual = this.size() == otherBAG.size();
            for(T itm : this)
            {
        		isEqual &= otherBAG.findAll(            	
        				new Predicate<T>(itm)
            			{
		    				public boolean match(T other)
		    				{
		    					if(other instanceof ISemanticEquals)
		    						return ((ISemanticEquals)other).semanticEquals((IAny)this.getScopeValue()).toBoolean();
		    					else
		    						return other.equals(this.getScopeValue());
		    				}
		    			}).size() == this.findAll(
	    					new Predicate<T>(itm)
    						{
    							public boolean match(T other)
    							{
            						if(other instanceof ISemanticEquals)
            							return ((ISemanticEquals)other).semanticEquals((IAny)this.getScopeValue()).toBoolean();
            						else
            							return other.equals(this.getScopeValue());
    							}
    						}).size();
            }
            return BL.fromBoolean(isEqual);
        }
	}
	
	
}
