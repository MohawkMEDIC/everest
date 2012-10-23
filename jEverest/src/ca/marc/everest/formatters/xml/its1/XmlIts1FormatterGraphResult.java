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
 * Date: 10-22-2012
 */
package ca.marc.everest.formatters.xml.its1;

import ca.marc.everest.formatters.interfaces.IFormatterGraphResult;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultCodeType;

/**
 * Represents a formatter graph result created by the XmlIts1Formatter
 */
public class XmlIts1FormatterGraphResult implements IFormatterGraphResult {


	// Backing field for code
	private ResultCodeType m_code = ResultCodeType.Accepted;
	// Backing field for results
	private IResultDetail[] m_results = null;
	
	/**
	 * Creates a new instance of the XmlIts1FormatterGraphResult 
	 */
	XmlIts1FormatterGraphResult(ResultCodeType code, IResultDetail[] results)
	{
		this.m_code = code;
		this.m_results = results;
	}
	
	/**
	 * Gets the result details of the format operation
	 */
	@Override
	public IResultDetail[] getResults() {
		return this.m_results;
	}

	/**
	 * Gets the result code of the format operation 
	 */
	@Override
	public ResultCodeType getCode() {
		return this.m_code;
	}

	/**
	 * Sets the current value of code
	 */
	public void setCode(ResultCodeType value)
	{
		this.m_code = value;
	}
}
