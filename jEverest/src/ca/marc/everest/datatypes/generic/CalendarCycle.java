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
 * Date: 10-04-2012
 */
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.datatypes.NullFlavor;
import ca.marc.everest.interfaces.IEnumeratedVocabulary;

/**
 * Specifies how the repetitions of a periodic interval are aligned to cycles of the calendar
 */
public enum CalendarCycle implements IEnumeratedVocabulary {
	
	/**
	 * Cycle is aligned to years continuously
	 */
	Year("CY"),
	/**
	 * Cycle is aligned to each month of the year
	 */
	MonthOfYear("MY"),
	/**
	 * Cycle is aligned to month continuously
	 */
	Month("CM"),
	/**
	 * Cycle is aligned to weeks continuously
	 */
	Week("CW"),
	/**
	 * Calendar cycle is aligned to each week of the year
	 */
	WeekOfYear("WY"),
	/**
	 * Calendar cycle is aligned to each day of the mont
	 */
	DayOfMonth("DM"),
	/**
	 * Calendar cycle is aligned to days continuously
	 */
	Day("CD"),
	/**
	 * Calendar cycle is aligned to each day of the year
	 */
	DayOfYear("DY"),
	/**
	 * Cycle is algined to each day of the week
	 */
	DayOfWeek("DW"),
	/**
	 * Cycle is aligned to each our of each day
	 */
	HourOfDay("HD"),
	/**
	 * Cycle is aligned to each hour continuously
	 */
	Hour("CH"),
	/**
	 * Cycle is aligned to each minute of each hour
	 */
	MinuteOfHour("NH"),
	/**
	 * Cycle is aligned to minutes continuously
	 */
	Minute("CN"),
	/**
	 * Cycle is aligned to each second of each minute
	 */
	SecondOfMinute("SN"),
	/**
	 * Cycle is aligned to seconds continuously
	 */
	Second("CS");

	/**
	 * Creates a new CalendarCycle enumeration value
	 */
	CalendarCycle(String code)
	{
		this.m_code = code;
	}

	// backing field for code
	private final String m_code;
	
	
	/**
	 * @see ca.marc.everest.interfaces.IEnumeratedVocabulary#getCode()
	 */
	@Override
	public String getCode() {
		return this.m_code;
	}

	/**
	 * @see ca.marc.everest.interfaces.IEnumeratedVocabulary#getCodeSystem()
	 */
	@Override
	public String getCodeSystem() {
		return "2.16.840.1.113883.5.9";
	}

	

}
