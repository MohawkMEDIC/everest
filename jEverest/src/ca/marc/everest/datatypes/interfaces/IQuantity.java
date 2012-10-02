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
 * Date: 06-24-2011
 */
package ca.marc.everest.datatypes.interfaces;

import ca.marc.everest.annotations.TypeMap;
import ca.marc.everest.datatypes.generic.IVL;
import ca.marc.everest.datatypes.generic.QuantityUncertaintyType;

/**
 * Identifies a class that implements the necessary data to qualify as a Quantity of something
 */
@TypeMap(name = "QTY")
public interface IQuantity extends IAny {

	/**
	 * Gets the expression that represents the quantity
	 */
	IEncapsulatedData getExpression();
	/**
	 * Sets a value representing the expression of the quantity
	 */
	void setExpression(IEncapsulatedData value);
	
	/**
	 * Get the uncertainty
	 */
	IQuantity getUncertainty();
	/**
	 * Set the uncertainty
	 */
	void setUncertainty(IQuantity value);
	
	/**
	 * Get the uncertainty type
	 */
	QuantityUncertaintyType getUncertaintyType();

	/**
	 * Sets the type of uncertainty
	 */
	void setUncertaintyType(QuantityUncertaintyType value);
	
	/**
	 * Get the uncertain range
	 */
	IInterval<IQuantity> getUncertainRange();

	
	/**
	 * Gets the value of the quantity as an integer
	 */
	Integer toInteger();
	
	/**
	 * Gets the value of the quantity as a double
	 */
	Double toDouble();
	
}
