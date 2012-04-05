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
 * Date: 08-31-2011
 */
package ca.marc.everest.exceptions;

import ca.marc.everest.interfaces.IDisposable;

/**
 * This exception indicates that an object that has already
 * been disposed was accessed.
 * 
 *  Disposing an object instructs the object to tear down
 *  all of the internal resources that are being used and
 *  prepare for disposal by the garbage collector.
 */
public final class ObjectDisposedException extends RuntimeException {

	// backing field for object name
	private String m_objectName;

	/**
	 * Creates a new instance of the ObjectDisposedException
	 */
	public ObjectDisposedException() { }
	/**
	 * Creates a new instance of the ObjectDisposedException
	 * with the name of the object (class) that was disposed.
	 * @param objectName The object name
	 */
	public ObjectDisposedException(String objectName) {
		super("Attempt to access disposed object " + objectName);
		this.m_objectName = objectName;
	}
}
