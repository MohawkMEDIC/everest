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
 * Date: 10-01-2012
 */
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.ANY;

/**
 * Represents a list of sampled values with each new term 
 * scaled and translated from a list of previous samples. Used
 * to specify sampled biosignals
 */
@Structure(name = "SLIST", structureType = StructureType.DATATYPE)
public class SLIST<T extends IQuantity> extends ANY implements ISequence<INT>, ISampledList {

}
