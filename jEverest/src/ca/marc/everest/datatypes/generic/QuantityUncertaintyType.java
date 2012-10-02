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
 * Date: 10-02-2012
 */
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.interfaces.IEnumeratedVocabulary;

/**
 * Uncertainty type
 */
public enum QuantityUncertaintyType implements IEnumeratedVocabulary {

	/**
	 * Assigned constant probability over the entire list of possible outcomes
	 */
	Uniform("U"),
	/**
	 * The well-known bell-shaped normal distribution
	 */
	Normal("N"),
	/**
	 * Logarithmic normal distribution
	 */
	LogNormal("LN"),
	/**
	 * The gamma-distribution used for data that is skewed right
	 */
	Gama("G"),
	/**
	 * Used for data that describes extinction
	 */
	Exponential("E"),
	/**
	 * Used to describe the sum of squares of random variables
	 */
	X2("X2"),
	/**
	 * Used to describe the quotient of a normal random vairable and the square root
	 */
	TDistribution("T"),
	/**
	 * Used to describe the quotient of two X^2 random variables
	 */
	F("F"),
	/**
	 * The beta distribution used for data that is bound on both sides 
	 */
	Beta("B");
	
	/**
	 * Encapsulated data mnemonic
	 */
	QuantityUncertaintyType(String mnemonic)
	{
		this.m_code = mnemonic;
	}
	
	// Backing field for code
	private final String m_code;
	
	/**
	 * Gets the code mnemonic
	 */
	@Override
	public String getCode() {
		return this.m_code; 
	}

	/**
	 * Gets the code system
	 */
	@Override
	public String getCodeSystem() {
		return "2.16.840.1.113883.5.1020";
	}

}
