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
 * Date: 07-19-2011
 */
package ca.marc.everest.interfaces;

import ca.marc.everest.datatypes.*;
import ca.marc.everest.datatypes.generic.*;
import ca.marc.everest.datatypes.interfaces.ICodedSimple;

/**
 * Identifies a class as implementing the basic functionality
 * as necessary to implement Interaction functionality
 */
public interface IInteraction extends IGraphable {

	/**
	 * Gets the time that the particular interaction instance was created
	 */
	TS getCreationTime();
	/**
	 * Gets the version code of the interaction instance 
	 */
	ICodedSimple getVersionCode();
	/**
	 * Gets the interaction identifier for the instance
	 */
	II getInteractionId();
	/**
	 * Gets the processing mode code for the instance 
	 */
	ICodedSimple getProcessingModeCode();
	/**
	 * Gets the profile identifier for the instance 
	 */
	LIST<II> getProfileId();
	
}
