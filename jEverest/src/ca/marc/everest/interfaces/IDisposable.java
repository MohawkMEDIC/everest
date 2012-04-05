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
package ca.marc.everest.interfaces;

/**
 * The IDisposable interface marks an object as disposable.
 * 
 *  <p>Being disposable differs from the Finalize call in 
 *  that the dispose method instructs the object instance
 *  to tear down any internal resources (native, thread pools,
 *  etc...) and be prepared to be disposed by the garbage collector.
 *  </p>
 *  <p> As Finalize is not always called before destruction of the object,
 *  Everest needed a way to guarantee that resources are freed. Therefore
 *  any object implementing IDisposable should always have the 
 *  dispose method called like this pattern:
 *  </p>
 *  <code>
 *  IDisposable object = ....
 *  try 
 *  {
 *  	
 *  }
 *  finally
 *  {
 *  	object.dispose();
 *  }
 *  </code>
 */
public interface IDisposable {

	/**
	 * Instructs the object to tear down any resources that
	 * are in use and prepare to be disposed by the garbage
	 * collector
	 */
	void dispose();
}
