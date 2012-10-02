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
package ca.marc.everest.annotations;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * The type map attribute overcomes the change of names of
 * types from R1 to R2 datatypes that have no meaning other
 * than the change of name. For example, a QSI is nothing more than an SXPR 
 * with all components having the operator Intersect.
 *
 */
@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
public @interface TypeMap {

	// HACK: "You can never pass null as a Java annotation parameter value, because, uh, null isn't a ConstantExpression"
	public static final String NULL = "PRETEND I'M NULL";
	
	/**
	 * Identifies the name of the type which the type maps to
	 */
	String name();

	/**
	 * Identifies the argument type that must be supplied to trigger the map
	 */
	String argumentType() default NULL;
	
}
