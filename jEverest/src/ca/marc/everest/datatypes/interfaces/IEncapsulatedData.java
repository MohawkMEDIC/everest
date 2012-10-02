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
package ca.marc.everest.datatypes.interfaces;

import ca.marc.everest.datatypes.EncapsulatedDataCompression;
import ca.marc.everest.datatypes.EncapsulatedDataIntegrityAlgorithm;
import ca.marc.everest.datatypes.EncapsulatedDataRepresentation;

/**
 * Represents a type that encapsulates data in a message
 */
public interface IEncapsulatedData {

	/**
	 * Gets the data within the encapsulated data object
	 */
	byte[] getData();
	/**
	 * Sets the data carried in the encapsulated data object
	 */
	void setData(byte[] value);

	/**
	 * Gets the representation that the encapsulated data should carry on the wire
	 */
	EncapsulatedDataRepresentation getRepresentation();
	/**
	 * Sets a value instructing the formatter how to represent the data carried in 
	 * the encapsulated data
	 */
	void setRepresentation(EncapsulatedDataRepresentation value);

	/**
	 * Gets the language code representing the language of the content within the encapsulated
	 * data instance
	 */
	String getLanguage();
	/**
	 * Sets the language code representing the language of the content within the encapsulated 
	 * data instance
	 */
	void setLanguage(String value);

	/**
	 * Gets the content-type of the content carried within the encapsulated data
	 */
	String getMediaType();
	/**
	 * Sets the content-type of the content carried within the encapsulated data instance
	 */
	void setMediaType(String value);
	
	/**
	 * Gets a series of byte representing the integrity check of the data carried in the encapsulated
	 * data instance.
	 */
	byte[] getIntegrityCheck();

	/**
	 * Gets a value representing the algorithm used to calculate the integrity check
	 */
	EncapsulatedDataIntegrityAlgorithm getIntegrityCheckAlgorithm();
	/**
	 * Sets a value representing the algorithm used to calculate the integrity check property
	 * of the encapsulated data instance
	 */
	void setIntegrityCheckAlgorithm(EncapsulatedDataIntegrityAlgorithm value);

	/**
	 * Gets an encapsulated data instance which is a thumbnail representation of the encapsulated data
	 * instance
	 */
	IEncapsulatedData getThumbnail();

	/**
	 * Gets a telecommunications address which points to the contents of the encapsulated data instance
	 */
	ITelecommunicationsAddress getReference();
	
	/**
	 * Gets a value representing the compression algorithm used to compress the data encapsulated in this instance
	 */
	EncapsulatedDataCompression getCompression();
}
