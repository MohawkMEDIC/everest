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
 * Date: 09-27-2012
 */
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.datatypes.interfaces.IPredicate;

/**
 * An abstract predicate implementation that allows anonymous classes to pass a scoped filter variable
 * to the match function. For example, to iterate over myStrings and use the current value in myString
 * as a predicate filter, a developer could do this: 
 * <pre>
 * BAG&lt;ST> strings = new BAG&lt;ST>();
 * ...
 * for(ST instance : myStrings)
 * 	ST result = BAG.find(            	
 *		new Predicate&lt;ST>(instance)
 *		{
 *			public boolean match(ST other)
 *			{
 *				return other.getValue().equals(this.getScopeValue());
 *			}
 *		}
 *	);
 * </pre>
 */
// HACK: Workaround for Java's lack of closures
public abstract class Predicate<T> implements IPredicate<T> {

	// Reference to the base object of comparison
	private T m_self;
	
	/**
	 * Get the value from the calling scope
	 */
	protected T getScopeValue() { return this.m_self; }
	
	/**
	 * Constructs a new predicate with a reference to self
	 */
	public Predicate(T scopedValue) { this.m_self = scopedValue; }
	
	/**
	 * Matching code
	 */
	public abstract boolean match(T i);
	
}
