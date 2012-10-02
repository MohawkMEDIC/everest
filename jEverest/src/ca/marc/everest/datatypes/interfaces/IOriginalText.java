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
 * Represents objects which carry original text data
 */
public interface IOriginalText {
	/**
	 * Gets the original text of the coded value
	 */
	IEncapsulatedData getOriginalText();
	/**
	 * Sets the original text of the coded value
	 */
	void setOriginalText(IEncapsulatedData value);
}
