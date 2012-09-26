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
 * An issue was detected with the codified concepts in a message
 * <p>
 * This issue is raised whenever codified data is encountered whereby the supplied value 
 * cannot be used in the context, or if the value is unknown / invalid.
 * </p>
 *
 */
public class VocabularyIssueResultDetail extends ValidationResultDetail {

	
	// Serialization version unique identifier
	private static final long serialVersionUID = 1L;

	/**
	 * Creates a new instance of the VocabularyIssueResultDetail with the specified type, message and exception
	 * @param type The type of issue being constructed (error, warning, information)
	 * @param message A textual message describing the vocabulary/codification error
	 * @param exception An exception that caused the issue to be raised
	 */
	public VocabularyIssueResultDetail(ResultDetailType type, String message,
			Exception exception) {
		super(type, message, exception);
	}

	/**
	 * Creates a new instance of the VocabularyIssueResultDetail with the specified type, message, exception and lcoation
	 * @param type The type of issue being constructed (error, warning, information)
	 * @param message A textual message describing the vocabulary/codification error
	 * @param location The location where the vocabulary issue was encountered
	 * @param exception An exception that caused this issue to be raised
	 */
	public VocabularyIssueResultDetail(ResultDetailType type, String message,
			String location, Exception exception) {
		super(type, message, location, exception);
	}

	/**
	 * Creates a new instance of the VocabularyIssueResultDetail with the specified type, and message
	 * @param type The type of issue being constructed (error, warning, information)
	 * @param message A textual message describing the vocabulary/codification error
	 */
	public VocabularyIssueResultDetail(ResultDetailType type, String message) {
		super(type, message);
	}

}
