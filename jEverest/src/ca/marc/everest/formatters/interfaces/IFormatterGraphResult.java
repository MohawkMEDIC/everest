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
 * Date: 07-21-2011
 */
package ca.marc.everest.formatters.interfaces;

import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultCodeType;

/**
 * Represents the outcome of a graph operation from a formatter
 */
public interface IFormatterGraphResult {

	/**
	 * Gets an array of result details that represent the issues 
	 * detected from the parse operation
	 */
	IResultDetail[] getResults();
	/**
	 * Get the code that represents the overall outcome of the
	 * formatting parse result.
	 */
	ResultCodeType getCode();
	
}
