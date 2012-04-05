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
 * Date: 06-25-2011
 */
package ca.marc.everest.annotations;
import java.lang.annotation.*;

/**
 * Indicates how a formatter should treat the name, conformance and serialization
 * of the attached field.
 */
@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.METHOD)
public @interface Property {

	// HACK: See Structure.java
	public final String NULL = "PRETEND I'M NULL";
	

	/**
	 * Gets the name of the property when serialized to the wire using the formatter 
	 */
	String name();
	
	/**
	 * Identifies the conformance of the property as mandatory, populated, optional or required
	 */
	ConformanceType conformance();
	
	/**
	 * Identifies the type of property. This is used by the formatter (ie: ITS) to determine
	 * how the property should be serialized on the wire.
	 */
	PropertyType propertyType();
	
	/**
	 * Gets an identifier of the value set to which valid codes are members
	 */
	String supplierDomain() default NULL;

	/**
	 * Identifies the type that is to be used when choosing which property annotation
	 * is to be used on serialization. This is used when conditional serialization is 
	 * desired.
	 */
	Class type() default java.lang.Object.class;

	/**
	 * Identifies which property annotation should be used when alternate traversal names are dependent
	 * upon the interaction type.  
	 */
	Class interactionOwner() default java.lang.Object.class;

	/**
	 * Identifies the generic suppliers for the type to get around Java's lack
	 * of reified generics
	 */
	Class[] genericSupplier() default {};
	
	/**
	 * Identifies the minimum occurrence of the property instances in order for a property
	 * to be considered "conformant". This parameter is typically seen on collection types 
	 */
	int minOccurs() default 0;

	/**
	 * Identifies the maximum occurrence of the property instances in order for the property to 
	 * be considered "conformant". This parameter is typically seen on collection types. 
	 */
	int maxOccurs() default 1;

	/**
	 * When set, this parameter forces a flavor onto the rendered property. This means that 
	 * a particular flavor can be imposed on a property.
	 */
	String imposeFlavorId() default NULL;
	
	/**
	 * When true, instructs formatters to ignore the traversal of this property. IE: The formatter
	 * should treat the child elements of the instance of the property as children of the parent
	 * of the contained class.
	 */
	boolean ignoreTraversal() default false;
	
}
