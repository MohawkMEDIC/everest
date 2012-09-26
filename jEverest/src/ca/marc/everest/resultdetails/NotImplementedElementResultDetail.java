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
 * Date: 09-25-2012
 */
package ca.marc.everest.resultdetails;

import ca.marc.everest.interfaces.ResultDetailType;

/**
 * An element or attribute encountered is not understood
 * <p>
 * This result detail occurs during the parsing of an instance whereby a formatter cannot understand
 * or parse an element or attribute because there is no backing property in the object model
 * </p>
 *
 */
public class NotImplementedElementResultDetail extends NotImplementedResultDetail {

	
	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;

	/**
	 * Creates a new instance of the NotImplementedElementResultDetail with the specified type, exception, elementName and namespace 
	 * @param type The type of result being issued (error, warning, information)
	 * @param elementName The name of the element or attribute that is not implemented
	 * @param elementNamespace The namespace to which the unimplemented element belongs
	 * @param exception The exception that caused the issue to be raised
	 */
	public NotImplementedElementResultDetail(ResultDetailType type, String elementName, String elementNamespace, Exception exception)
	{
		this(type, elementName, elementNamespace, null, exception);
	}

	/**
	 * Creates a new instance of the NotImplementedElementResultDetail with the specified type, exception, elementName, elementNamespace and location
	 * @param type The type of result being created (error, warning, information)
	 * @param elementName The name of the element or attribute which is not implemented
	 * @param elementNamespace The namespace to which the element belongs
	 * @param location The message location where the element was encountered
	 * @param exception The exception that caused the issue to be raised
	 */
	public NotImplementedElementResultDetail(ResultDetailType type, String elementName, String elementNamespace, String location, Exception exception)
	{
		super(type, String.format("Element %1#%2 is not supported and was not interpreted", elementNamespace, elementName), location, exception);
	}
	
	/**
	 * Creates a new instance of the NotImplementedElementResultDetail with the specified elementName and elementNamespace parameters
	 * @param elementName The name of the element or attribute that is not understood
	 * @param elementNamespace The namespace to which the elementName belongs
	 */
	public NotImplementedElementResultDetail(String elementName, String elementNamespace)
	{
		this(ResultDetailType.WARNING, elementName, elementNamespace, null, null);
	}
}
