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
 * Date: 08-24-2011
 */
package ca.marc.everest.datatypes;

import ca.marc.everest.interfaces.*;

public enum EncapsulatedDataCompression implements IEnumeratedVocabulary {

	/**
	 * Deflate compression algorithm
	 */
	Deflate ("DF"),
	/**
	 * GZip compression algorithm
	 */
	GZip ("GZ"),
	/**
	 * ZLib compression algorithm
	 */
	ZLib ("ZL"),
	/**
	 * Compress compression algorithm
	 */
	Compress ("Z"),
	/**
	 * BZip2 Compression Algorithm
	 */
	BZip ("BZ"),
	/**
	 * 7z Compression from 7zip
	 */
	Zip7 ("Z7");
	
	/**
	 * Creates a new encapsulated data compression
	 */
	EncapsulatedDataCompression(String code)
	{
		this.m_code = code;
	}
	
	// backing field for code
	private final String m_code;
	
	/**
	 * Get the code system of the compression
	 */
	public String getCodeSystem() { return "2.16.840.1.113883.5.1009"; }
	/**
	 * Get the code mnemonic of the compression 
	 */
	public String getCode() { return this.m_code; }
}
