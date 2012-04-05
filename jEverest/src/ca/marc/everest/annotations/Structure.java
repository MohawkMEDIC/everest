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
import java.lang.annotation.*;

/**
 * Identifies a class or structure as belonging to an RMIM structure. Without this attribute, classes
 * and structures are "not" graphable to an ITS
 */
@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
public @interface Structure
{
	// HACK: "You can never pass null as a Java annotation parameter value, because, uh, null isn't a ConstantExpression"
	public static final String NULL = "PRETEND I'M NULL";
	
	/**
	 * Identifies the type of structure annotation represents.
	 */
	StructureType structureType();
	/**
	 * Default code system for the structure (used in Enumerations)
	 */
	String codeSystem() default NULL;
	/**
	 * Default template type, in Everest.NET this allows GPMR to infer 
	 * the default generic type that is used with the class. Used by 
	 * formatters to compensate for type erasure in Java.
	 */
	@SuppressWarnings("rawtypes")
	Class defaultTemplateType() default java.lang.Object.class;
	/**
	 * The name of the structure within the RMIM class
	 */
	String name();
	/**
	 * When set to true, signals to the formatter that the structure name can be used
	 * as an entry point into rendering
	 */
	boolean isEntryPoint() default false;
}
