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
public enum DomainTimingEvent implements IHierarchicEnumeratedVocabulary {
	/**
	 * Before a meal
	 */
	BeforeMeal("AC", null),
	/**
	 * Before Lunch
	 */
	BeforeLunch("ACD", BeforeMeal),
	/**
	 * Before breakfast
	 */
	BeforeBreakfast("ACM", BeforeMeal),
	/**
	 * Before Dinner
	 */
	BeforeDinner("ACV", BeforeMeal),
	/**
	 * One hour before sleeping
	 */
	HourOfSleep("HS", null),
	/**
	 * BEtween two meals
	 */
	BetweenMeals("IC", null),
	/**
	 * Between lunch and dinner
	 */
	BetweenLunchAndDinner("ICD", BetweenMeals),
	/**
	 * Between breakfast and lunch
	 */
	BetweenBreakfastAndLunch("ICM", BetweenMeals),
	/**
	 * Between dinner and the hour of sleep
	 */
	BetweenDinnerAndSleep("ICV", BetweenMeals),
	/**
	 * After a meal
	 */
	AfterMeal("PC", null),
	/**
	 * After lunch
	 */
	AfterLunch("PCD", AfterMeal),
	/**
	 * After breakfast
	 */
	AfterBreakfast("PCM", AfterMeal),
	/**
	 * After dinner
	 */
	AfterDinner("PCV", AfterMeal);

	private final String m_code;
	private final DomainTimingEvent m_parent;
	
	/**
	 * Creates a new instance of the DomainTimingEvent enumeration
	 */
	DomainTimingEvent(String code, DomainTimingEvent parent) { this.m_code = code; this.m_parent = parent; }
	
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

	/**
	 * Gets the parent of this code
	 */
	@Override
	public IHierarchicEnumeratedVocabulary getParent() {
		return this.m_parent;
	}

	/**
	 * Returns true if the specified code is a child concept of this code.
	 */
	@Override
	public boolean isChildConcept(IHierarchicEnumeratedVocabulary other) {
		IHierarchicEnumeratedVocabulary parentAttempt = this;
		do
		{
			if(parentAttempt.equals(other))
				return true;
			parentAttempt = parentAttempt.getParent();
		} while(parentAttempt != null);
		return false;
	}

}
