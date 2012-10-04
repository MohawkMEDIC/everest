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
package ca.marc.everest.interfaces;

/**
 * Represents an enumerated vocabulary which is also hierarchical in nature
 */
public interface IHierarchicEnumeratedVocabulary extends IEnumeratedVocabulary {

	/**
	 * Gets the parent of this vocabulary item
	 */
	IHierarchicEnumeratedVocabulary getParent();

	/**
	 * Determine of other is child concept of the current concept
	 */
	boolean isChildConcept(IHierarchicEnumeratedVocabulary other);
}
