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
 * Date: 07-26-2012
 */
package ca.marc.everest.datatypes.interfaces;

import java.util.Collection;

import ca.marc.everest.interfaces.*;

/**
 * Identifies a type as implementing the ANY attributes (a data type)
 */
public interface IAny extends ISemanticEquals, Cloneable, IImplementsNullFlavor, IGraphable {

	/**
	 * Validates the instance
	 */
	boolean validate();
	
	/**
	 * Returns true if the instance is null
	 */
	boolean isNull();
	
	/**
	 * Gets the datatype class of the instance
	 */
	@SuppressWarnings("rawtypes")
	Class getDataType();
	
	/**
	 * Gets the flavor of the instance
	 */
	String getFlavorId();
	
	/**
	 * Validate with details
	 */
	Collection<IResultDetail> validateEx();
}
