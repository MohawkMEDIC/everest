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
 * Date: 09-27-2012
 */
package ca.marc.everest.datatypes.interfaces;

import java.util.Comparator;

/**
 * An interface which identifies sets
 */
public interface ISet<T> extends ICollection<T> {

	/**
	 * Gets the current comparator
	 */
	public Comparator<T> getComparator();
	
	/**
	 * Sets the comparator for the SET
	 */
	public void setComparator(Comparator<T> value);
}
