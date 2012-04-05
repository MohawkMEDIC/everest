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
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.interfaces.*;

/**
 * Identifies the type of uncertainty distribution
 */
public enum QuantityUncertaintyType implements IEnumeratedVocabulary {
	/** The The uniform distribution assigns a constant probability over the entire interval 
     * of possible outcomes */
	Uniform ("U"),
	/** This is the well-known bell-shaped normal distribution */
	Normal ("N"),
	/** The logarithmic normal distribution */
	LogNormal("LN"),
	/** The gamma-distribution */
	Gamma("G"),
	/** Used for data that describes extinction */
	Exponential("E"),
	/** Used to describe the sum of squares */
	X2("X2"),
	/** Used to describe the quotient of a normal random variable and the square root */
	TDistribution("T"),
	/** sed to describe the quotient of two X^2 random variables */
	F("F"),
	/** The beta distribution */
	Beta("B");

	// backing field for code
	private final String m_code;
	
	/**
	 * Creates a new uncertainty type code
	 */
	QuantityUncertaintyType(String code) { this.m_code = code; }
	@Override
	public String getCode() {
		return this.m_code;
	}

	@Override
	public String getCodeSystem() {
		return "2.16.840.1.113883.5.1020";
	}

}
