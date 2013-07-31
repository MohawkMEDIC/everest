/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Attributes;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using MARC.Everest.Exceptions;
using System.Xml.Serialization;
using MARC.Everest.Connectors;

#if WINDOWS_PHONE
using MARC.Everest.Phone;
#else
using MARC.Everest.Design;
using System.Drawing.Design;
using System.IO.Compression;
#endif

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Encapsulated data is used to transport larger portions of text, binary and XML data within an instance. 
    /// </summary>
    /// <example>Loading a PDF into an ED
    /// <code lang="cs" title="ED datatype">
    /// <![CDATA[
    /// // Using constructor
    /// ED t = new ED(File.ReadAllBytes(@"test.pdf"),"application/pdf");
    /// t.Language = "en-US";
    /// t.MediaType = "text/plain";
    /// t.Reference = "http://www.google.com";
    /// ]]>
    /// </code>
    /// <code lang="cs" title="Compressing an ED">
    /// <![CDATA[
    /// // The ED data type provides an easy mechanism to automatically compute checksums for the 
    /// // data contained within. Computing an integrity check is simple. All the developer needs to do,
    /// // is set the IntegrityCheckAlgorithm property, and set the integrity check to the result of the 
    /// // ComputeHash function. If an integrity check algorithm is specified, and no integrity check is 
    /// // assigned, it will be automatically generated when the “IntegrityCheck” value is retrieved.
    /// ED test = new ED(File.ReadAllBytes("C:\\output.pdf"), "text/xml"); 
    /// Console.WriteLine("Original: {0}", test.Data.Length); 
    ///  
    /// // Setup compression 
    /// test.Compression = EncapsulatedDataCompression.GZ; 
    /// test.Data = test.Compress(); 
    /// Console.WriteLine("Compressed: {0}", test.Data.Length); 
    ///  
    /// // Setup integrity check 
    /// test.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA256; 
    /// test.IntegrityCheck = test.ComputeIntegrityCheck(); 
    ///  
    /// // Validate 
    /// if (!test.ValidateIntegrityCheck()) 
    /// Console.WriteLine("Integrity check doesn't match!"); 
    /// else 
    /// { 
    ///     test.Data = test.UnCompress(); 
    ///     Console.WriteLine("UnCompressed: {0}", test.Data.Length); 
    /// }
    /// ]]>
    /// </code>
    /// </example>
    /// <remarks>
    /// <para>The ED data type can be used to encapsulate any binary data within an HL7v3 instance. The ED data type
    /// within Everest will be formatted appropriately for whatever particular ITS is used (for example: Binary data in 
    /// XML would be formatted as Base64)</para>
    /// <para>
    /// The key to the ED data type representation on the wire is the <see cref="P:Representation"/> property. This property
    /// is set automatically based on the method in which data within the ED is set. For example, if you set the content
    /// of the ED as a string, then the Representation property is automatically set to "Text" (which is represented in 
    /// RAW format in XML). However, if you set the contents of the ED to a byte array, then the representation is set
    /// to Base64
    /// </para>
    /// <para>
    /// The ED datatype provides many mechanisms of populating data within an instace. For example, one could use 
    /// the byte array property of Data, however for convenience, using the string property Value may work better if
    /// you are populating your ED to a string.
    /// </para>
    /// </remarks>
    [Structure(Name = "ED", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("ED", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif

    public class ED : ANY, IEncapsulatedData, IEquatable<ED>
    {

        /// <summary>
        /// Create a new instance of ED
        /// </summary>
        public ED() : base() {
            //this.Language = CultureInfo.CurrentCulture.Name;
        }
        /// <summary>
        /// Create a new instance of ED with initial value of <paramref name="Data"/>. Sets representation to B64
        /// </summary>
        /// <param name="data">Initial data</param>
        /// <param name="mediaType">The media type of the binary data</param>
        public ED(byte[] data, string mediaType) : this() { this.Data = data; Representation = EncapsulatedDataRepresentation.B64; this.MediaType = mediaType; }
        /// <summary>
        /// Create a new instance of ED with an initial value of <paramref name="Data"/>. Sets representation to TXT
        /// </summary>
        /// <param name="data">The data string to contain</param>
        public ED(string data) : this() { this.Value = data; this.Representation = EncapsulatedDataRepresentation.TXT; this.MediaType = "text/plain"; }
        /// <summary>
        /// Create a new instance of ED with initial value of <paramref name="data"/> in the language <paramref name="language"/>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="language"></param>
        public ED(string data, string language) : this(data) { this.Language = language; }
        /// <summary>
        /// Create an instance of ED that links to a real location <paramref name="reference"/>
        /// </summary>
        /// <param name="reference">The reference to the real data</param>
        public ED(TEL reference) : this() { this.Reference = reference; }

        /// <summary>
        /// Get or set the data that is encapsulated by this object. 
        /// </summary>
        /// <remarks>Not included in formatted output</remarks>
        public byte[] Data { get; set; }

        /// <summary>
        /// Identifies how data should be compressed
        /// </summary>
        /// <remarks>
        /// Note that setting this property does not compress the data, it merely informs
        /// receiving systems that the data is to be compressed.
        /// <para>
        /// When compressing data using the compress method, it is recommended that this value be set
        /// via the compression parameter. The only reason this property is read/write is to allow
        /// specification of compression on referenced data.
        /// </para>
        /// </remarks>
        [Property(Name = "compression", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public EncapsulatedDataCompression? Compression { get; set; }

        /// <summary>
        /// Compress the data in this object according to the set parameters
        /// </summary>
        /// <returns>A new instance of ED with compressed data</returns>
        /// <example>ED with compressed data
        /// <code lang="cs" title="ED compression">
        /// <![CDATA[
        /// // Create instance
        /// ED test = new ED(File.ReadAllBytes("C:\\output.pdf"), "text/xml");
        /// Console.WriteLine("Original: {0}", test.Data.Length);
        ///
        /// // Setup compression
        /// compressed = test.Compress(EncapsulatedDataCompression.GZ);
        /// Console.WriteLine("Compressed: {0}", compressed.Data.Length);
        /// // Setup integrity check
        /// compressed.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA256;
        /// compressed.IntegrityCheck = compressed.ComputeIntegrityCheck();
        /// // Validate
        /// if (!compressed.ValidateIntegrityCheck())
        /// Console.WriteLine("Integrity check doesn't match!");
        /// else
        /// {
        /// test = compressed.UnCompress();
        /// Console.WriteLine("UnCompressed: {0}", test.Data.Length);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="T:System.InvalidOperationException">When <paramref name="compressionMethod"/> is not supported by this function</exception>
        public ED Compress(EncapsulatedDataCompression compressionMethod)
        {
            #if WINDOWS_PHONE
            throw new NotSupportedException("Compression is not supported by the Windows Phone Version of Everest");
            #else
            ED retVal = this.Clone() as ED;
            retVal.Compression = compressionMethod;
            retVal.Data = retVal.CompressInternal();

            if (retVal.IntegrityCheckAlgorithm != null)
                retVal.IntegrityCheck = retVal.ComputeIntegrityCheck();

            return retVal;
            #endif
        }

        /// <summary>
        /// Decompresses the data from the this ED and places the result in a 
        /// new ED.
        /// </summary>
        /// <remarks>
        /// If an integrity check algorithm is specified, then this method will update
        /// the IntegrityCheck property to contain the value of integrity check.
        /// </remarks>
        /// <returns>A new ED instance containing uncompressed data.</returns>
        /// <exception cref="T:System.InvalidOperationException">When the compression algorithm for the instance is not supported by this method</exception>
        public ED UnCompress()
        {
            #if WINDOWS_PHONE
            throw new NotSupportedException("De-Compression is not supported by the Windows Phone Version of Everest");
            #else
            ED retVal = this.Clone() as ED;
            retVal.Data = retVal.UnCompressInternal();
            retVal.Compression = null;

            if (retVal.IntegrityCheckAlgorithm != null)
                retVal.IntegrityCheck = retVal.ComputeIntegrityCheck();

            return retVal;
            #endif

        }

        /// <summary>
        /// Gets or sets an alternative description of the media where the context is
        /// not suitable for rendering the media
        /// </summary>
        [Property(Name = "description", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)] 
        public ST Description { get; set; }

        // Encapsulated data representation
        private EncapsulatedDataRepresentation _representation = EncapsulatedDataRepresentation.TXT;

        /// <summary>
        /// Identifies how the encapsulated data is represented when formatted
        /// </summary>
        /// <remarks>The default value for this property is always to encode in Base64</remarks>
        /// <example>
        /// <code title="XML Representation" lang="cs">
        /// <![CDATA[
        ///
        ///    // Encapsulated Data 
        ///    ED xmlEd = new ED();
        ///
        ///
        ///     XmlDocument doc = new XmlDocument();
        ///     XmlElement xmlElem = doc.CreateElement("ROOT");
        ///     xmlElem.InnerText = "This is the text of the root element";
        ///     doc.AppendChild(xmlElem);
        ///
        ///    // We want the encapsulated data to be represented as XML
        ///
        ///    
        ///    // When formatted, the output of this ed will be
        ///    // <text representation="XML" mediaType="text/xml"><ROOT>This is the text of the root element</ROOT></text>
        ///    xmlEd.Representation = EncapsulatedDataRepresentation.XML;
        ///    xmlEd.MediaType = "text/xml";
        /// ]]>
        /// </code>
        /// <code title="Text Representation" lang="cs">
        /// <![CDATA[
        ///    // When formatted, the output of this ed will be
        ///    // <text representation="TXT" mediaType="text/xml">This is my text&lt;/ROOT></text>
        ///    ED txtEd = "This is my text";
        ///    txtEd.MediaType = "text/plain";
        ///    txtEd.Representation = EncapsulatedDataRepresentation.TXT;
        /// ]]>
        /// </code>
        /// <code title="Base64 Encoding" lang="cs">
        /// <![CDATA[
        ///    // When formatted, the output of this ed will be
        ///    // <text representation="B64" mediaType="text/xml">VGhpcyBpcyBteSB0ZXh0</text>
        ///    ED b64Ed = "This is my text";
        ///    b64Ed.Representation = EncapsulatedDataRepresentation.B64;
        /// ]]>
        /// </code>        
        /// </example>
        [Property(Name = "representation", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public EncapsulatedDataRepresentation Representation 
        {
            get
            {
                // Must be B64 for compressed data
                return Compression == null ? _representation : EncapsulatedDataRepresentation.B64;
            }
            set
            {
                _representation = value;
            }
        }

        /// <summary>
        /// The human language of the content. Valid codes are taken from the IETF. 
        /// </summary>
        [Property(Name = "language", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public string Language { get; set; }

        /// <summary>
        /// Alternative renditions of the same content translated into a different language
        /// </summary>
        /// <remarks>Included to help assist with DataTypes R2 implementation. Note that this property will not appear in datatypes r1</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Property(Name = "translation", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public SET<ED> Translation { get; set; }

        /// <summary>
        /// Identifies the type of encapsulated data and identifies a method to interpret or render the content. 
        /// The IANA defined domain of media types is established by the IETF RFC 2045 and 2046
        /// </summary>
        [Property(Name = "mediaType", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public string MediaType { get; set; }

#if !WINDOWS_PHONE
        /// <summary>
        /// Compute hash for the data in this object and set the hash and integrity check algorithm used
        /// </summary>
        /// <returns>The computed hash</returns>
        public byte[] ComputeIntegrityCheck()
        {
            return ComputeIntegrityCheck(this.Data);
        }

        /// <summary>
        /// Compute integrity check for the specified data
        /// </summary>
        /// <param name="data">The data to calculate integrity check</param>
        /// <returns>The calculated integrity check for <paramref name="data"/></returns>
        /// <exception cref="T:System.InvalidOperationException">When integrityCheckAlgorithm is not set</exception>
        public byte[] ComputeIntegrityCheck(byte[] data)
        {
            if (!this.IntegrityCheckAlgorithm.HasValue)
                throw new InvalidOperationException("Must set IntegrityCheckAlgorithm for this call");
            return ComputeIntegrityCheck(data, this.IntegrityCheckAlgorithm.Value);
        }

        /// <summary>
        /// Compute integrity check for the specified <paramref name="data"/> using the specified <paramref name="algorithm"/>
        /// </summary>
        /// <param name="data">The data which is to have its checksum calculated</param>
        /// <param name="algorithm">The algorithm to use for calculating the integrity check</param>
        /// <returns>A byte array containing the computed integrity check</returns>
        /// <exception cref="T:System.ArgumentNullException">When data is null</exception>
        public byte[] ComputeIntegrityCheck(byte[] data, EncapsulatedDataIntegrityAlgorithm algorithm)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            
            switch (algorithm)
            {
                case EncapsulatedDataIntegrityAlgorithm.SHA256:
                    SHA256Managed sha256 = new SHA256Managed();
                    return sha256.ComputeHash(data);

                case EncapsulatedDataIntegrityAlgorithm.SHA1:
                    SHA1Managed sha1 = new SHA1Managed();
                    return sha1.ComputeHash(data);
            }
            return null;
        }
#endif

        private byte[] integrityCheck;
        /// <summary>
        /// The Integrity check is a short binary value representing a cryptographically strong checksum of the data
        /// </summary>
        /// <remakrs>
        /// If data is populated and a check algorithm is subsequently populated, and no value is stored in <see cref="IntegrityCheck"/>, then
        /// the IntegrityCheck property will be computed and assigned automatically
        /// </remakrs>
        /// <example>Using integrity checks
        /// <code lang="cs" title="ED integrity check">
        /// <![CDATA[
        /// ED edTest = new ED(File.ReadAllBytes("test.xml"),"text/xml");
        /// edTest.Representation = EncapsulatedDataRepresentation.XML;
        /// edTest.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA1;
        /// // This will be auto-generated if you don't explicity call it
        /// edTest.IntegrityCheck = edTest.ComputeIntegrityCheck();
        /// Console.WriteLine(Convert.ToBase64String(edTest.IntegrityCheck));
        /// ]]>
        /// </code>
        /// </example>
        [Property(Name = "integrityCheck", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
#if !WINDOWS_PHONE
        [ReadOnly(true)]
#endif
        public byte[] IntegrityCheck
        {
            get
            {
#if !WINDOWS_PHONE
                if (integrityCheck == null && IntegrityCheckAlgorithm != null &&
                    Data != null)
                    integrityCheck = ComputeIntegrityCheck();
#endif
                return integrityCheck;
            }
            set
            {
                integrityCheck = value;
            }
        }

#if !WINDOWS_PHONE
        /// <summary>
        /// Uncompress the data in this object according to the set parameters
        /// </summary>
        /// <returns>The decompressed data</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Un"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "UnCompress")]
        private byte[] UnCompressInternal()
        {
            if (Compression == null || Data == null)
                return Data;

            // Decompress the data
            MemoryStream ms = new MemoryStream(Data);
            Stream compressionStream = null;

            // Create stream
            switch ((EncapsulatedDataCompression)Compression)
            {
                case EncapsulatedDataCompression.DF:
                    compressionStream = new DeflateStream(ms, CompressionMode.Decompress);
                    break;
                case EncapsulatedDataCompression.GZ:
                    compressionStream = new GZipStream(ms, CompressionMode.Decompress);
                    break;
                default:
                    throw new InvalidOperationException("The compression algorithm required to compress the data is not implemented in this version of .NET");
                    break;

            }
                    
            // Decompress into a memory stream
            byte[] buffer = new byte[1024]; // 1k buffer
            MemoryStream output = new MemoryStream();
            int readBytes = 0;
            while ((readBytes = compressionStream.Read(buffer, 0, buffer.Length)) > 0) 
                output.Write(buffer, 0, readBytes); // Read the stream
            compressionStream.Close();

            // Write a buffer
            buffer = new byte[output.Length];
            output.Seek(0, SeekOrigin.Begin);
            output.Read(buffer, 0, buffer.Length);
            return buffer; // Return output
        }

        /// <summary>
        /// Compression utility to be used internally
        /// </summary>
        /// <returns></returns>
        private byte[] CompressInternal()
        {
            if (Compression == null || Data == null)
                return null;

            // Decompress the data
            MemoryStream output = new MemoryStream();
            Stream compressionStream = null;
            
            // Create stream
            switch ((EncapsulatedDataCompression)Compression)
            {
                case EncapsulatedDataCompression.DF:
                    compressionStream = new DeflateStream(output, CompressionMode.Compress, true);
                    break;
                case EncapsulatedDataCompression.GZ:
                    compressionStream = new GZipStream(output, CompressionMode.Compress, true);
                    break;
                default:
                    throw new InvalidOperationException("The compression algorithm required to compress the data is not implemented in this version of .NET");
                    break;
            }

            // Decompress into a memory stream
            byte[] buffer = new byte[Data.Length]; // 1k buffer
            compressionStream.Write(Data, 0, Data.Length);
            compressionStream.Close();
            buffer = new byte[output.Length];
            output.Seek(0, SeekOrigin.Begin);
            output.Read(buffer, 0, buffer.Length);
            return buffer; // Return output

        }


        /// <summary>
        /// Validate the hash that the data within this ED is valid according to the set
        /// integrity check algorithm and integrity check
        /// </summary>
        /// <remarks>If Data is not set (ie: ED is a reference), then the result of this function is true</remarks>
        public bool ValidateIntegrityCheck()
        {
            if (IntegrityCheckAlgorithm == null || Data == null)
                return true;

            byte[] valueHash = IntegrityCheck;
            byte[] dataHash = ComputeIntegrityCheck();

            // Compare the hash
            return ByteEquality(valueHash, dataHash);
        }
#endif
        /// <summary>
        /// Specifies the algorithm used to compute the checksum
        /// </summary>
        [Property(Name = "integrityCheckAlgorithm", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public EncapsulatedDataIntegrityAlgorithm? IntegrityCheckAlgorithm { get; set; }

        /// <summary>
        /// An abbreviated rendition of the full content.
        /// </summary>
        /// <example>Reference with thumbnail
        /// <code lang="cs" title="ED thumbnail">
        /// <![CDATA[
        /// ED t = new ED((TEL)"http://1.2.12.32/images/big.tif");
        /// t.Thumbnail = new ED(File.ReadAllBytes(@"C:\small.png"),"image/png");
        /// ]]>
        /// </code>
        /// </example>
        [Property(Name = "thumbnail", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
#if !WINDOWS_PHONE
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NewInstanceTypeEditor), typeof(UITypeEditor))]        
#endif
        public ED Thumbnail { get; set; }

        /// <summary>
        /// A telecommunications address such as a url for HTTP or FTP which resolve to precisely the same binary
        /// content that could as well have been provided inline.
        /// </summary>
        [Property(Name = "reference", ImposeFlavorId = "URL", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
#if !WINDOWS_PHONE
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NewInstanceTypeEditor), typeof(UITypeEditor))]        
#endif
        public TEL Reference { get; set; }

        /// <summary>
        /// ED.Text validator
        /// </summary>
        /// <remarks>
        /// Ensures that the specified ED is properly setup to be represented as Text. Changes
        /// Representation to TXT, sets compression and integritycheck to null.
        /// </remarks>
        [Flavor(Name = "Text")]
        [Flavor(Name = "ED.TEXT")]
        public static bool IsValidTextFlavor(ED ed)
        {
            // Ensure that instance will be rendered as text, ie: clean programmer mistakes
            return ed.Representation == EncapsulatedDataRepresentation.TXT &&
                ed.Compression == null &&
                ed.IntegrityCheckAlgorithm == null &&
                ed.MediaType == "text/plain";
        }

        /// <summary>
        /// ED.Image validator
        /// </summary>
        [Flavor(Name = "Image")]
        [Flavor(Name = "ED.IMAGE")]
        public static bool IsValidImageFlavor(ED ed)
        {
            return ed.MediaType != null && ed.MediaType.StartsWith("image/") &&
                (ed.Data != null && ed.Representation == EncapsulatedDataRepresentation.B64 || 
                ed.Data == null);
        }

        /// <summary>
        /// ED.SIGNATURE validator
        /// </summary>
        [Flavor("Signature")]
        [Flavor("ED.SIGNATURE")]
        public static bool IsValidSignatureFlavor(ED ed)
        {
            return ed.Representation == EncapsulatedDataRepresentation.XML &&
                ed.IntegrityCheck == null && ed.Thumbnail == null &&
                ed.Compression == null && ed.Language == null &&
                ed.Translation == null && ed.MediaType != null &&
                ed.MediaType == "text/xml";
        }

        /// <summary>
        /// Get the string representation of the value
        /// </summary>
        /// <remarks>By setting this property, the Representation property is updated to TXT</remarks>
        /// <seealso cref="P:Representation"/>
        [EditorBrowsable(EditorBrowsableState.Never)]
#if !WINDOWS_PHONE
        [Browsable(false)]
#endif
        [Property(Name = "value", PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public string Value
        {
            get
            {
                if (Representation == EncapsulatedDataRepresentation.TXT && Data != null)
                {
                    byte[] data = Data ?? new byte[0];
                    return System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
                }
                else
                    return null;
            }
            set
            {
                
                Representation = EncapsulatedDataRepresentation.TXT;
                if (value != null)
                    Data = System.Text.Encoding.UTF8.GetBytes(value ?? "");
                else
                    Data = null;
            }
        }

        /// <summary>
        /// Base64 representation of the value
        /// </summary>
        /// <remarks>By setting this property, the Representation property is updated to B64</remarks>
        /// <seealso cref="P:Representation"/>
        [EditorBrowsable(EditorBrowsableState.Never)]
#if !WINDOWS_PHONE
        [Browsable(false)]
#endif
        [Property(Name = "data", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        public string Base64Data
        {
            get
            {
                if (Representation == EncapsulatedDataRepresentation.B64 && this.Data != null)
                    return Convert.ToBase64String(Data);
                else
                    return null;
            }
            set
            {
                Representation = EncapsulatedDataRepresentation.B64;
                Data = Convert.FromBase64String(value);
            }
        }

#if !WINDOWS_PHONE
        /// <summary>
        /// XML Representation of the data
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [Property(Name = "xml", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        public XmlElement XmlData
        {
            get
            {
                try
                {
                    if (Representation == EncapsulatedDataRepresentation.XML && Data != null)
                    {
                        MemoryStream ms = new MemoryStream(Data);
                        XmlDocument xd = new XmlDocument();
                        xd.Load(ms);
                        return xd.DocumentElement;
                    }
                    else
                        return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                // Value is null, so reset data
                if (value == null) { Data = null; return; }

                MemoryStream ms = new MemoryStream();
                try
                {
                    XmlWriter xw = XmlWriter.Create(ms, new XmlWriterSettings() { OmitXmlDeclaration = true});
                    value.WriteTo(xw);
                    xw.Close();

                    // Seek to start and read
                    ms.Seek(0, SeekOrigin.Begin);
                    Data = new byte[ms.Length];
                    ms.Read(Data, 0, (int)ms.Length);
                    ms.Dispose();
                    Representation = EncapsulatedDataRepresentation.XML;
                }
                finally
                {
                    ms.Dispose();
                }
            }
        }
#endif

        #region IEncapsulatedData Members


        IEncapsulatedData IEncapsulatedData.Thumbnail
        {
            get { return (IEncapsulatedData)Thumbnail; }
        }

        ITelecommunicationAddress IEncapsulatedData.Reference
        {
            get { return (ITelecommunicationAddress)Reference; }
        }

        #endregion

        /// <summary>
        /// Validate ED
        /// </summary>
        /// <remarks>
        /// An ED is valid if
        /// <list type="bullet">
        ///     <item>Data is set, XOR</item>
        ///     <item>NullFlavor is provided</item>
        /// </list>
        /// </remarks>
        public override bool Validate()
        {
            return ((((this.Data != null) ^ (this.Reference != null)) ) ^ (this.NullFlavor != null)) &&
                (((this.Data != null || this.Reference != null)) || (this.Data == null && this.Reference == null)) &&
                (this.Translation != null && this.Translation.FindAll(o=>o.Translation == null).Count ==this.Translation.Count ||
                this.Translation == null) &&
                (this.Reference == null || TEL.IsValidUrlFlavor(this.Reference)) &&
                (this.Thumbnail == null || this.Thumbnail.Thumbnail == null && this.Thumbnail.Reference == null);
        }

        /// <summary>
        /// Validatethe data type returning the validation errors that occurred
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());

            if (!((this.Data != null) ^ (this.Reference != null)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ED", "The Data and Reference properties must be used exclusive of each other", null));
            if (this.NullFlavor != null && (this.Data != null || this.Reference != null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ED", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE));
            else if (this.NullFlavor == null && this.Data == null && this.Reference == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ED", ValidationMessages.MSG_NULLFLAVOR_MISSING));
            if (this.Translation != null && this.Translation.FindAll(o => o.Translation != null).Count > 0)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ED", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_PERMITTED_ON_PROPERTY, "Translation", "Translation"), null));
            if (this.Reference != null && !TEL.IsValidUrlFlavor(this.Reference))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ED", "When populated, Reference must be a valid instance of TEL.URL", null));
            if (this.Thumbnail != null && this.Thumbnail.Thumbnail != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ED", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_PERMITTED_ON_PROPERTY, "Thumbnail", "Thumbnail"), null));
            if (this.Thumbnail != null && this.Thumbnail.Reference != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ED", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_PERMITTED_ON_PROPERTY, "Thumbnail", "Reference"), null));
            
            return retVal;
        }

        #region Operators
        /// <summary>
        /// Convert this string into an ED
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator string(ED o)
        {
            if (o == null || o.IsNull)
                return null;
            else
                return o.Value;
        }

        
        /// <summary>
        /// Convert an ED into a byte array
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator byte[](ED o)
        {

            return o == null || o.IsNull ? null : o.Data;
        }

        /// <summary>
        /// Convert the string into an ED
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator ED(string o)
        {
            return new ED(o)
            {
                Language = CultureInfo.CurrentCulture.Name
            };
        }


        #endregion

        /// <summary>
        /// Convert this ED to a string
        /// </summary>
        public override string ToString()
        {
            if (Value != null)
                return Value;
            else if (Base64Data != null)
                return Base64Data;
            #if !WINDOWS_PHONE
            else if (XmlData != null)
                return XmlData.OuterXml;
            #endif
            return "";
        }

        #region IEquatable<ED> Members

        /// <summary>
        /// Determines if data in <paramref name="a"/> equals <paramref name="b"/>
        /// </summary>
        private bool ByteEquality(byte[] a, byte[] b)
        {
            if (a == null && b == null)
                return true;
            else if ((a == null) ^ (b == null))
                return false;

            bool valid = a.Length == b.Length;
            int i = 0;
            if (valid)  // Compare data
                while (i < b.Length && a[i] == b[i])
                    i++;
            valid &= i == a.Length; // ensure that data is equal

            return valid;

        }

        /// <summary>
        /// Determine if this ED is equal to another ED
        /// </summary>
        public bool Equals(ED other)
        {
            
            bool result = false;
            if (other != null)
                result = base.Equals((ANY)other) &&
                    other.Compression == this.Compression &&
                    other.IntegrityCheckAlgorithm == this.IntegrityCheckAlgorithm &&
                    ByteEquality(other.Data, this.Data) &&
                    ByteEquality(other.IntegrityCheck, this.IntegrityCheck) &&
                    other.Language == this.Language &&
                    other.MediaType == this.MediaType &&
                    (other.Reference != null ? other.Reference.Equals(this.Reference) : this.Reference == null) &&
                    other.Representation == this.Representation &&
                    (other.Thumbnail != null ? other.Thumbnail.Equals(this.Thumbnail) : this.Thumbnail == null) &&
                    (other.Translation != null ? other.Translation.Equals(this.Translation) : this.Translation == null) &&
                    (other.Description != null ? other.Description.Equals(this.Description) : this.Description == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is ED)
                return Equals(obj as ED);
            return base.Equals(obj);
        }

        /// <summary>
	    /// Determine semantic equality between this instance of ED and other
	    /// </summary>
	    /// <remarks>Two non-null flavored instances of ED are semantically equal when their MediaType and raw data properties are equal. 
	    ///When performing semantic equality between compressed instances of ED, the equality will be performed on the uncompressed data.
	    ///<para>
        /// Instances of ED can be semantically equal to instances of ST or SC if the mediaType of the ED it "text/plain" and 
	    /// the binary contents of the ED and ST/SC match.
	    /// </para>
        /// </remarks>
        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;

            // Null-flavored
            if (this.IsNull && other.IsNull)
                return true;
            else if (this.IsNull ^ other.IsNull)
                return false;

            ED thisEd, otherEd;

            // Other is ST?
            if (this.MediaType == "text/plain" && other is ST)
                return (other as ST).SemanticEquals(this);

            // Get other as an ED
            otherEd = other as ED;
            if (otherEd == null)
                return false;
            else if (otherEd.Data != null && otherEd.Compression != null)
                otherEd = otherEd.UnCompress();

            // Compressed data for this reference
            if (this.Data != null && this.Compression != null)
                thisEd = this.UnCompress();
            else
                thisEd = this;

            if (thisEd.Data != null && otherEd.Data != null)
                return thisEd.ByteEquality(thisEd.Data, otherEd.Data) && thisEd.MediaType.Equals(otherEd.MediaType);
            else
                return false;
        }
        #endregion

    }
}
