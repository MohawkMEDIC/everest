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
 * 		
 * 						    ()
 * 							/\
 * 						   /  \
 *  					  /    \
 *  					 /      \
 *  					/ 		 \
 *  				   /		  \
 *  				  /============\
 *  				 |				|
 *  				 |	/\		/\	|
 *  				 |	\/		\/	|
 *  				 |				|
 *  				 \ |		  | /
 *  				  \	\--------/ /
 *  				   ------------
 *			Thanks for a great 3rd Birthday Party
 * 
 * User: Justin Fyfe
 * Date: 06-28-2011
 */
package ca.marc.everest.interfaces;

/**
 * This interface is implemented by enumerations that contain literals
 * within a value set. Formatters use this to serialize 
 */
public interface IEnumeratedVocabulary {

	/**
	 * Get the code for the enumerated value
	 */
	String getCode();
	
	/**
	 * Get the code system for the enumerated vocabulary
	 */
	String getCodeSystem();
}
