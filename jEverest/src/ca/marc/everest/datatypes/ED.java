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

import ca.marc.everest.datatypes.generic.*;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IEncapsulatedData;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;
import ca.marc.everest.annotations.*;
import java.io.*;
import java.util.*;

import org.w3c.dom.*;

import javax.xml.bind.DatatypeConverter;
import javax.xml.transform.dom.*;
import javax.xml.transform.stream.*;
import javax.xml.transform.*;
import java.util.zip.*;
import java.security.*;

/**
 * Encapsulated data is used to transport larger portions of text, binary and XML data
 * within an instance
 */
@Structure(name = "ED", structureType = StructureType.DATATYPE)
public class ED extends ANY implements IEncapsulatedData {

	// backing field for data
	private byte[] m_data;
	// backing field for media type
	private String m_mediaType;
	// Backing field for language
	private String m_language;
	// Backing field for reference
	private TEL m_reference;
	// Backing field for compression
	private EncapsulatedDataCompression m_compression;
	// Backing field for representation
	private EncapsulatedDataRepresentation m_representation;
	// Backing field for description
	private ST m_description;
	// backing field for integrity check
	private byte[] m_integrityCheck;
	// backing field for integrity check algo
	private EncapsulatedDataIntegrityAlgorithm m_integrityCheckAlgorithm;
	// backing field for thumbnail
	private ED m_thumbnail;
	// backing field for translations
	private SET<ED> m_translation;
	
	/**
	 * Creates a new instance of the encapsulated data class
	 */
	public ED() { 
		super(); 
		this.m_language = Locale.getDefault().getLanguage() + "-" + Locale.getDefault().getCountry();	
	}
	/**
	 * Creates a new instance of the encapsulated data class with
	 * the specified binary data and media type
	 * @param data The initial set of data to populate within the ED
	 * @param mediaType The mediate type of the data in the ED
	 */
	public ED(byte[] data, String mediaType)
	{
		this();
		this.m_data = data;
		this.m_mediaType = mediaType;
	}
	/**
	 * Creates a new instance of the encapsulated data class with
	 * the specified string data. Media type is set to text/plain
	 */
	public ED(String data) throws UnsupportedEncodingException 
	{
		this();
		this.setData(data);
	}
	/**
	 * Creates a new instance of the encapsulated data clas
	 * @param data
	 * @param language
	 */
	public ED(String data, String language) throws UnsupportedEncodingException
	{
		this(data);
		this.m_language = language;
	}
	/**
	 * Creates an instance of an ED that links to a reference somewhere else in the instance
	 * or on the internet
	 */
	public ED(TEL reference)
	{
		this.m_reference = reference;
	}
	
	/**
	 * Gets the data that is contained within the ED instance
	 */
	public byte[] getData() { return this.m_data; }
	/**
	 * Sets the data that is contained within the ED instance
	 * @param value The byte array that represents the data to be set in the ED instance
	 */
	public void setData(byte[] value) { this.m_data = value; }
	/**
	 * Sets the data that is contained within the ED instance to the specified string
	 * @param value The string value of the data
	 */
	public void setData(String value) throws UnsupportedEncodingException
	{
		if(value == null)
			this.m_data = null;
		else
			this.m_data = value.getBytes("UTF8");
		this.m_representation = EncapsulatedDataRepresentation.Text;
	}
	/**
	 * Sets the data that is contained within the encapsulated data to a document
	 * Permits the nesting of data
	 * @param value The DOM instance of the XML document to be nested
	 */
	public void setData(Document value) throws TransformerConfigurationException, TransformerException
	{
		if(value == null)
		{
			this.m_data = null;
			return;
		}
		
		Source source = new DOMSource(value);
		ByteArrayOutputStream out = new ByteArrayOutputStream();
		Result result = new StreamResult(out);
		TransformerFactory factory = TransformerFactory.newInstance();
		Transformer transformer = factory.newTransformer();
		transformer.transform(source, result);
		this.m_data = out.toByteArray();
	}
	
	/**
	 * Gets the reference to the content of this ED
	 */
	@Property(name = "reference", conformance = ConformanceType.REQUIRED, propertyType = PropertyType.NONSTRUCTURAL)
	@Override
	public TEL getReference() { return this.m_reference; }
	/**
	 * Sets the reference to the content of this ED
	 */
	public void setReference(TEL value) { this.m_reference = value; }
	/**
	 * Gets the compression method that was used to compress the data
	 */
	@Property(name = "compression", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public EncapsulatedDataCompression getCompression() { return this.m_compression; }
	/**
	 * Sets the compression method that the target data is compressed with. 
	 * Note that setting this property does not compress inline data. You should
	 * use the @see Compress method. 
	 * @param value The selected compression value
	 */
	public void setCompression(EncapsulatedDataCompression value) { this.m_compression = value; }
	/**
	 * Compresses the data contained in this instance of ED and returns a new 
	 * ED with the specified compression and compressed data
	 * @param compressionAlgorithm The compression algorithm to use for compressing
	 * @return A new instance of ED that has been compressed using the desired algorithm
	 * @throws IOException 
	 */
	public ED compress(EncapsulatedDataCompression compressionAlgorithm) throws IOException
	{
		return compress(compressionAlgorithm, this.getData());
	}
	/**
	 * Compresses the specified data with the specified compression algorithm and returns a new
	 * ED instance with the resultant data.
	 * @param compressionAlgorithm The algorithm to use for compression
	 * @param data The data that is to be compressed
	 * @return A new instance of ED that contains all the data found in this instance with the compressed data
	 */
	public ED compress(EncapsulatedDataCompression compressionAlgorithm, byte[] data) throws IOException
	{
		
		// Validate parameters
		if(compressionAlgorithm == null)
			throw new IllegalArgumentException("compressionAlgorithm is null");
		else if(data == null)
			throw new IllegalArgumentException("data is null");
		
		ED retVal = null;
		try
		{
			retVal = (ED)this.clone();
		}
		catch(CloneNotSupportedException e)
		{
			// Shouldn't reach this
		}
		
		// Create byte array output stream
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		
		// Deflater stream
		DeflaterOutputStream compressionStream = null;
		
		// Determine compression algorithm
		switch(compressionAlgorithm)
		{
			case Deflate:
				compressionStream = new DeflaterOutputStream(bos);
				break;
			case GZip:
				compressionStream = new GZIPOutputStream(bos);
				break;
			default:
				throw new IllegalArgumentException("unsupported compressionAlgorithm");
		}
		
		try
		{
			compressionStream.write(data);
			compressionStream.close();
		}
		catch(IOException e) { // ingore
		}
		
		retVal.setData(bos.toByteArray());
		retVal.setCompression(compressionAlgorithm);
		
		return retVal;
	}
	/**
	 * Uncompresses the data in this ED instance and returns a new ED instance with
	 * the uncompressed data
	 * @throws IOException 
	 * @throws NoSuchAlgorithmException 
	 */
	public ED unCompress() throws IOException, NoSuchAlgorithmException
	{
		if(this.getCompression() == null)
			throw new IllegalStateException("no compression algorithm is set on this instance");
		
		ED retVal = null;
		try
		{
			retVal = (ED)this.clone();
		}
		catch(CloneNotSupportedException e)
		{
			// Shouldn't reach this
		}
		
		// Data stream
		ByteArrayInputStream bin = new ByteArrayInputStream(this.getData());
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		// Deflate output stream
		InflaterInputStream decompressionStream = null;
		switch(this.getCompression())
		{
			case Deflate:
				decompressionStream = new InflaterInputStream(bin);
				break;
			case GZip:
				decompressionStream = new GZIPInputStream(bin);
				break;
			default:
				throw new NoSuchAlgorithmException();
		}
		
		// Decompress
		try
		{
			byte buffer[] = new byte[1024];
			int read = 0;
			do
			{
				read = decompressionStream.read(buffer, 0, buffer.length);
				if(read > 0)
					bos.write(buffer, 0, read);
			} while(read >= 0);
			
		}
		catch(IOException e)
		{
			// Ignore
		}
		
		retVal.setData(bos.toByteArray());
		retVal.setCompression(null);
		
		if(retVal.getIntegrityCheckAlgorithm() != null && retVal.getIntegrityCheck() != null)
			retVal.setIntegrityCheck(retVal.computeIntegrityCheck());
		return retVal;
	}
	
	/**
	 * Gets the description of the data contained in the ED
	 */
	@Property(name = "description", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public ST getDescription() { return this.m_description; }
	/**
	 * Sets the description of the data contained in this ED
	 */
	public void setDescription(ST value) { this.m_description = value; }

	/**
	 * Gets a code specifying how this instance will be represented in
	 * this instance of ED
	 */
	@Property(name = "representation", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public EncapsulatedDataRepresentation getRepresentation() 
	{
		// Must be B64 for compress
		return this.m_compression == null ? this.m_representation : EncapsulatedDataRepresentation.Base64; 
	}
	/**
	 * Sets the representation of the instance of ED
	 */
	public void setRepresentation(EncapsulatedDataRepresentation value) { this.m_representation = value; }
	/**
	 * Gets the language in which the content of the ED is represented.
	 */
	@Property(name = "language", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public String getLanguage() { return this.m_language; }
	/**
	 * Sets the language in which the content of the ED is represented
	 * @param value
	 */
	public void setLanguage(String value) { this.m_language = value; }
	/**
	 * Gets alternate representations of this encapsulated data object in 
	 * other languages.
	 */
	@Property(name = "translation", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public SET<ED> getTranslation() { return this.m_translation; }
	/**
	 * Sets the alternate representations of this encapsulated data object
	 * in other language
	 */
	public void setTranslation(SET<ED> value) { this.m_translation = value; }
	/**
	 * Gets the internet media type (MIME) of this ED instance
	 */
	@Property(name = "mediaType", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public String getMediaType() { return this.m_mediaType; }
	/**
	 * Sets the internet media type (MIME) of this ED instance
	 */
	public void setMediaType(String value) { this.m_mediaType = value; }

	/** 
	 * Gets the integrity check for this instance of the ED class.
	 */
	@Property(name = "integrityCheck", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public byte[] getIntegrityCheck() { return this.m_integrityCheck; }
	/**
	 * Sets the integrity check data for this instance of the ED class
	 */
	public void setIntegrityCheck(byte[] value) { this.m_integrityCheck = value; }
	/**
	 * Gets the integrity check algorithm
	 */
	@Property(name = "integrityCheckAlgorithm", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public EncapsulatedDataIntegrityAlgorithm getIntegrityCheckAlgorithm() { return this.m_integrityCheckAlgorithm; }
	/**
	 * Sets the integrity check algorithm to the specified value
	 */
	public void setIntegrityCheckAlgorithm(EncapsulatedDataIntegrityAlgorithm value) { this.m_integrityCheckAlgorithm = value; }
	/**
	 * Computes the integrity check of the data
	 * @throws NoSuchAlgorithmException 
	 */
	public byte[] computeIntegrityCheck() throws NoSuchAlgorithmException {
		return computeIntegrityCheck(this.m_data);
	}
	/**
	 * Compute integrity check for the specified data using this instance's
	 * integrity check algorithm value
	 * @param data The data to calculate integrity check 
	 * @return The integrity check value
	 */
	public byte[] computeIntegrityCheck(byte[] data) throws NoSuchAlgorithmException {
		return computeIntegrityCheck(data, this.m_integrityCheckAlgorithm);
	}
	/**
	 * Compute integrity check for the specified data using the specified integrity check algorithm
	 * @param data The data to calculate integrity
	 * @param algorithm The algorithm to use to compute integrity
	 * @return The integrity check data
	 */
	public byte[] computeIntegrityCheck(byte[] data, EncapsulatedDataIntegrityAlgorithm algorithm) throws NoSuchAlgorithmException {
		
		MessageDigest calcDigest = null;
		
		// Determine the algorithm
		switch(algorithm)
		{
			case Sha1:
				calcDigest = MessageDigest.getInstance("SHA-1");
				break;
			case Sha256:
				calcDigest = MessageDigest.getInstance("SHA-256");
				break;
		}
		
		calcDigest.update(data, 0, data.length);
		return calcDigest.digest();
	}
	/**
	 * Validates that the data contained within the ED matches it's integrity check
	 * If
	 * @throws NoSuchAlgorithmException 
	 */
	public boolean validateIntegrityCheck() throws NoSuchAlgorithmException
	{
		if(this.getIntegrityCheckAlgorithm() == null || this.getData() == null)
			return true;
		
		byte[] valueHash = this.getIntegrityCheck();
		byte[] dataHash = this.computeIntegrityCheck();
		return Arrays.equals(valueHash, dataHash);
	}
	/**
	 * Gets the thumbnail representation of this ED instance
	 */
	@Property(name = "thumbnail", conformance = ConformanceType.MANDATORY, propertyType = PropertyType.NONSTRUCTURAL)
	public ED getThumbnail() { return this.m_thumbnail; }
	/**
	 * Sets the thumbnail representation of this ED instance
	 */
	public void setThumbnail(ED value) { this.m_thumbnail = value; }
	
	/**
	 * Flavor validation for ED.TEXT
	 */
	@Flavor(name = "ED.TEXT")
	public static boolean isValidTextFlavor(ED ed)
	{
		return ed.getRepresentation() == EncapsulatedDataRepresentation.Text &&
			ed.getCompression() == null &&
			ed.getIntegrityCheckAlgorithm() == null &&
			ed.getMediaType().equals("text/plain");
	}
	/**
	 * Flavor validation for ED.IMAGE
	 */
	@Flavor(name = "ED.IMAGE")
	public static boolean isValidImageFlavor(ED ed)
	{
		return ed.getMediaType() != null && 
			ed.getMediaType().startsWith("image/");
	}
	/**
	 * Flavor validator for ED.SIGNATURE
	 */
	@Flavor(name = "ED.SIGNATURE")
	public static boolean isValidSignatureFlavor(ED ed)
	{
		return ed.getRepresentation() == EncapsulatedDataRepresentation.Xml &&
			ed.getIntegrityCheck() == null && ed.getThumbnail() == null &&
			ed.getCompression() == null && ed.getLanguage() == null &&
			ed.getTranslation() == null && ed.getMediaType() != null &&
			ed.getMediaType().equals("text/xml");
	}
	
	/**
	 * validate that this instance of ED meets basic validation criteria
	 */
	@Override
	public boolean validate()
	{
		boolean isValid = (((this.getData() != null || this.getReference() != null) ) ^ (this.getNullFlavor() != null)) &&
	        (((this.getData() != null || this.getReference() != null)) || (this.getData() == null && this.getReference() == null)) &&
	        (this.getReference() == null || TEL.isValidUrlFlavor(this.getReference())) &&
	        (this.getThumbnail() == null || this.getThumbnail().getThumbnail() == null && this.getThumbnail().getReference() == null);

	     // Validate the no translation has a translation
	     if(this.getTranslation() != null)
	    	 for(ED trans : this.getTranslation())
	    		 isValid &= trans.getTranslation() == null;
	        
	     return isValid;
	}
	
	/**
	 * Gets the hash code
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_compression == null) ? 0 : m_compression.hashCode());
		result = prime * result + Arrays.hashCode(m_data);
		result = prime * result + Arrays.hashCode(m_integrityCheck);
		result = prime
				* result
				+ ((m_integrityCheckAlgorithm == null) ? 0
						: m_integrityCheckAlgorithm.hashCode());
		result = prime * result
				+ ((m_language == null) ? 0 : m_language.hashCode());
		result = prime * result
				+ ((m_mediaType == null) ? 0 : m_mediaType.hashCode());
		result = prime
				* result
				+ ((m_representation == null) ? 0 : m_representation.hashCode());
		result = prime * result
				+ ((m_thumbnail == null) ? 0 : m_thumbnail.hashCode());
		return result;
	}
	
	/**
	 * Determines if this instance of ED equals the other instance of
	 * ED
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		ED other = (ED) obj;
		if (m_compression != other.m_compression)
			return false;
		if (!Arrays.equals(m_data, other.m_data))
			return false;
		if (!Arrays.equals(m_integrityCheck, other.m_integrityCheck))
			return false;
		if (m_integrityCheckAlgorithm != other.m_integrityCheckAlgorithm)
			return false;
		if (m_language == null) {
			if (other.m_language != null)
				return false;
		} else if (!m_language.equals(other.m_language))
			return false;
		if (m_mediaType == null) {
			if (other.m_mediaType != null)
				return false;
		} else if (!m_mediaType.equals(other.m_mediaType))
			return false;
		if (m_representation != other.m_representation)
			return false;
		if (m_thumbnail == null) {
			if (other.m_thumbnail != null)
				return false;
		} else if (!m_thumbnail.equals(other.m_thumbnail))
			return false;
		return true;
	}
	
	/**
	 * Cast operator to byte array
	 */
	public byte[] toByteArray()
	{
		return this.m_data;
	}
	
	/**
	 * Convert this ED instance to an ST
	 */
	public ST toST()
	{
		String value = this.toString();

        ST retVal = new ST(value);
        retVal.setControlActRoot(this.getControlActRoot());
        retVal.setControlActExt(this.getControlActExt());
        retVal.setFlavorId(this.getFlavorId());
        retVal.setLanguage(this.getLanguage());
        retVal.setNullFlavor(this.getNullFlavor());
        retVal.setUpdateMode(this.getUpdateMode());
        retVal.setValidTimeHigh(this.getValidTimeHigh());
        retVal.setValidTimeLow(this.getValidTimeLow());
        return retVal;
	}
	
	/**
	 * Represent this as a string
	 */
	@Override
	public String toString() {
		if (this.m_representation == EncapsulatedDataRepresentation.Text && this.m_data != null)
            return new String(this.m_data);
        else if (this.m_representation == EncapsulatedDataRepresentation.Base64 && this.m_data != null)
            return DatatypeConverter.printBase64Binary(this.m_data);
        else if(this.m_representation == EncapsulatedDataRepresentation.Xml && this.m_data != null)
        	return new String(this.m_data);
        return "";
	}
	
	/**
	 * Extended validation method returning the details of the validation
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		Collection<IResultDetail> retVal = new ArrayList<IResultDetail>(super.validateEx());

        if (!((this.m_data != null) ^ (this.m_reference != null)))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ED", "The Data and Reference properties must be used exclusive of each other", null));
        if (this.getNullFlavor() != null && (this.m_data != null || this.m_reference != null))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ED", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE));
        else if (this.getNullFlavor() == null && this.m_data == null && this.m_reference == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ED", EverestValidationMessages.MSG_NULLFLAVOR_MISSING));
        if (this.m_translation != null)
        {
        	for(ED trans : this.m_translation)
        		if(trans.getTranslation() != null)
        			retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ED", String.format(EverestValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "Translation", "Translation"), null));
        }
        if (this.m_reference != null && !TEL.isValidUrlFlavor(this.m_reference))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ED", "When populated, Reference must be a valid instance of TEL.URL", null));
        if (this.m_thumbnail != null && this.m_thumbnail.m_thumbnail != null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ED", String.format(EverestValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "Thumbnail", "Thumbnail"), null));
        if (this.m_thumbnail != null && this.m_thumbnail.m_reference != null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "ED", String.format(EverestValidationMessages.MSG_PROPERTY_NOT_PERMITTED, "Thumbnail", "Reference"), null));
        
        return retVal;
	}
	
	/**
	 * Determines if a is equal to b on a byte level
	 */
	private static boolean byteEquals(byte[] a, byte[] b)
	{
		if (a == null && b == null)
            return true;
        else if ((a == null) ^ (b == null))
            return false;

        boolean valid = a.length == b.length;
        int i = 0;
        if (valid)  // Compare data
            while (i < b.length && a[i] == b[i])
                i++;
        valid &= i == a.length; // ensure that data is equal

        return valid;
	}
	
	/** 
	 * Determine semantic equality between this instance of ED and other
	 * <p>
	 * Two non-null flavored instances of ED are semantically equal when their MediaType and raw data properties are equal. 
	 * When performing semantic equality between compressed instances of ED, the equality will be performed on the uncompressed data.
	 * </p>
	 * <p>
	 * Instances of ED can be semantically equal to instances of ST or SC if the mediaType of the ED it "text/plain" and 
	 * the binary contents of the ED and ST/SC match.
	 * </p>
	 */
	@Override
	public BL semanticEquals(IAny other) {
		BL baseSem = super.semanticEquals(other);
        if (!baseSem.toBoolean())
            return baseSem;

        // Null-flavored
        if (this.isNull() && other.isNull())
            return BL.TRUE;
        else if (this.isNull() ^ other.isNull())
            return BL.FALSE;

        ED thisEd, otherEd;

        // Other is ST?
        if (this.m_mediaType == "text/plain" && other instanceof ST)
            return ((ST)other).semanticEquals((IAny)this);

        // Get other as an ED
        try
        {
	        if (!(other instanceof ED))
	            return BL.FALSE;
	        else
        	{
	        	otherEd = (ED)other;
	        	if (otherEd.m_data != null && otherEd.m_compression != null)
	        	otherEd = otherEd.unCompress();
        	}
	        
	        // Compressed data for this reference
	        if (this.m_data != null && this.m_compression != null)
	            thisEd = this.unCompress();
	        else
	            thisEd = this;
	
	        if (thisEd.m_data != null && otherEd.m_data != null)
	            return BL.fromBoolean(ED.byteEquals(thisEd.m_data, otherEd.m_data) && thisEd.m_mediaType.equals(otherEd.m_mediaType));
	        else
	            return BL.FALSE;
        }
        catch(NoSuchAlgorithmException e)
        {
        	BL retVal = new BL();
        	retVal.setNullFlavor(NullFlavor.NoInformation);
        	return retVal;
        } catch (IOException e) {
        	BL retVal = new BL();
        	retVal.setNullFlavor(NullFlavor.NoInformation);
        	return retVal;
		}
	}

	
	
}
