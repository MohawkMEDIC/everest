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
 * Date: 09-27-2012
 */
package ca.marc.everest.exceptions;

/**
 * An exception indicating that a duplicate item has been added to a collection
 * where duplicates are not permitted
 */
public class DuplicateItemException extends RuntimeException {

	// Serialization version identifier
	private static final long serialVersionUID = 1L;

	/**
	 * Creates a new instance of the DuplicateItemException
	 */
    public DuplicateItemException() { super(); }
    /**
     * Creates a new instance of the DusplicateItemException with the specified message
     */
    public DuplicateItemException(String message) { super(message); }
    /**
     * Creates a new instance of the DuplicateItemException with the specified exception and inner exception
     */
    public DuplicateItemException(String message, Exception innerException) { super(message, innerException); }
}
