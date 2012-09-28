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
package ca.marc.everest.datatypes.interfaces;

/**
 * Identifies classes implementing a sequence of items whereby the order of items has meaning
 */
public interface ISequence<T> extends ICollection<T> {

	/**
	 * Retrieves the first item in the sequence
	 */
	public T first();

	/**
	 * Retrieves the last item in the sequence
	 */
	public T last();

	/**
	 * Retrieves a sub-sequence of items from the sequence
	 * @param start The index of the first item to extract
	 * @param end The index of the last item to extract
	 * @return A new sequence containing the selected values
	 */
	public ISequence<T> subSequence(int start, int end);
	
	/**
	 * Retrieves a sub-sequence of items from the sequence from start to the end 
	 * @param start The index of the first item to extract
	 * @return A new sequence containing the selected values from start to the end of the sequence
	 */
	public ISequence<T> subSequence(int start);
}
