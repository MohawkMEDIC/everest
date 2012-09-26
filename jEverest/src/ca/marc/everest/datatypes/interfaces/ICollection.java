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
 * Date: 08-24-2011
 */
package ca.marc.everest.datatypes.interfaces;

import java.util.Collection;
import ca.marc.everest.datatypes.*;

public interface ICollection<T> extends Iterable<T>, Collection<T> {

	/**
	 * Gets the item at the specified index
	 */
	T getItem(int index);
	
	/**
	 * Returns a BL indicating whether this collection includes
	 * all of the items in the other collection
	 */
	BL includesAll(ICollection<T> other);
	
	/**
	 * Returns a BL indicating whether this collection excludes
	 * all of the items in the other collection
	 */
	BL excludesAll(ICollection<T> other);

	/**
	 * Returns true if the collection is empty
	 */
	boolean isEmpty();
	
}
