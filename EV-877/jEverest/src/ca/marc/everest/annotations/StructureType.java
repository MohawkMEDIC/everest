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
 * Date: 06-24-2011
 */
package ca.marc.everest.annotations;

/**
 * Identifies the type of structures that can be represented in the RMIM class
 */
public enum StructureType {
	/**
	 * The structure represents a message structure
	 */
	MESSAGETYPE,
	/**
	 * The structure represents a data type
	 */
	DATATYPE,
	/**
	 * The structure represents an interaction, which can be used as an entry point for formatting.
	 */
	INTERACTION,
	/**
	 * Structure is a concept domain
	 */
	CONCEPTDOMAIN,
	/**
	 * Structure is a value set
	 */
	VALUESET,
	/**
	 * Structure is a code system
	 */
	CODESYSTEM
}
