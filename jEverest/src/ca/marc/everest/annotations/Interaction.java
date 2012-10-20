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
 * Date: 10-19-2012
 */
package ca.marc.everest.annotations;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * Identifies a structure which represents a complete HL7v3 interaction with transport, controlAct and payload wrappers.
 */
@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
public @interface Interaction {

	// HACK: See Structure.java
	public final String NULL = "PRETEND I'M NULL";
	
	/**
	 * Identifies the trigger event code for the interaction
	 */
	String triggerEvent() default NULL;
	
	/**
	 * Gets the name of the interaction, used for responses
	 */
	String name() default NULL;
	
}
