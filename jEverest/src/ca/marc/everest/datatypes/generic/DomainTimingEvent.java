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
import ca.marc.everest.annotations.*;
/**
 * Domain timing event types
 */
@Structure(name = "DomainTimingEvent", structureType = StructureType.CONCEPTDOMAIN)
public enum DomainTimingEvent implements IEnumeratedVocabulary {
	/**
	 * Before a meal
	 */
	BeforeMeal("AC"),
	/**
	 * Before Lunch
	 */
	BeforeLunch("ACD"),
	/**
	 * Before breakfast
	 */
	BeforeBreakfast("ACM"),
	/**
	 * Before Dinner
	 */
	BeforeDinner("ACV"),
	/**
	 * One hour before sleeping
	 */
	HourOfSleep("HS"),
	/**
	 * BEtween two meals
	 */
	BetweenMeals("IC"),
	/**
	 * Between lunch and dinner
	 */
	BetweenLunchAndDinner("ICD"),
	/**
	 * Between breakfast and lunch
	 */
	BetweenBreakfastAndLunch("ICM"),
	/**
	 * Between dinner and the hour of sleep
	 */
	BetweenDinnerAndSleep("ICV"),
	/**
	 * After a meal
	 */
	AfterMeal("PC"),
	/**
	 * After lunch
	 */
	AfterLunch("PCD"),
	/**
	 * After breakfast
	 */
	AfterBreakfast("PCM"),
	/**
	 * After dinner
	 */
	AfterDinner("PCV");

	private final String m_code;
	
	/**
	 * Creates a new instance of the DomainTimingEvent enumeration
	 */
	DomainTimingEvent(String code) { this.m_code = code; }
	
	/**
	 * Get the code of the domain timing event
	 */
	@Override
	public String getCode() {
		return this.m_code;
	}

	/**
	 * Get code system for the domain timing event
	 */
	@Override
	public String getCodeSystem() {
		return "2.16.840.1.113883.5.139";
	}

}
