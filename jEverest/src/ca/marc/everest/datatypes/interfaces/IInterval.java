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
package ca.marc.everest.datatypes.interfaces;

/**
 * Identifies an interval 
 */
public interface IInterval<T> extends ISetComponent<T>, IOriginalText {

	/**
	 * Gets the lower bound of the interval
	 */
	T getLow();
	/**
	 * Sets the lower bound of the interval
	 */
	void setLow(T value);
	/**
	 * Gets a flag indicating whether the lower bound of the interval is included in the set
	 */
	Boolean getLowInclusive();
	/**
	 * Sets a flag indicating whether the lower bound of the interval is included in the set
	 */
	void setLowInclusive(Boolean value);
	/**
	 * Gets the upper bound of the interval
	 */
	T getHigh();
	/**
	 * Sets the upper bound of the interval
	 */
	void setHigh(T value);
	/**
	 * Gets a flag indicating whether the upperbound of the interval is included in the set
	 */
	Boolean getHighInclusive();
	/**
	 * Sets a flag indicating whether the upper bound of the interval is included in the set
	 */
	void setHighInclusive(Boolean value);

	
}
