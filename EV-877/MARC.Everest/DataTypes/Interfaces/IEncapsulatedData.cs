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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Attributes;
using System.ComponentModel;

namespace MARC.Everest.DataTypes.Interfaces
{

    /// <summary>
    /// Supported data compression algorithms
    /// </summary>
    /// <remarks>
    /// Only the two formats supported by the system are listed
    /// </remarks>
    [Structure(Name = "CompressionAlgorithm", CodeSystem = "2.16.840.1.113883.5.1009", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    public enum EncapsulatedDataCompression
    {
        /// <summary>
        /// Deflate
        /// </summary>
        DF,
        /// <summary>
        /// GZIP
        /// </summary>
        GZ,
        /// <summary>
        /// ZLib
        /// </summary>
        ZL,
        /// <summary>
        /// Compress
        /// </summary>
        Z,
        /// <summary>
        /// BZIP
        /// </summary>
        BZ,
        /// <summary>
        /// 7z Compression
        /// </summary>
        Z7
    }

    /// <summary>
    /// Representation of the encapsulated data
    /// </summary>
    public enum EncapsulatedDataRepresentation
    {
        /// <summary>
        /// Data should be represented in plain text
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TXT")]
        [Enumeration(Value = "TXT")]
        TXT,
        /// <summary>
        /// Data should be base64 encoded
        /// </summary>
        [Enumeration(Value = "B64")]
        B64,
        /// <summary>
        /// Data is xml encoded
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "XML")]
        [Enumeration(Value = "TXT")]
        XML
    }

    /// <summary>
    /// Representation of how an ED should be checksummed
    /// </summary>
    [Structure(Name = "IntegrityCheckAlgorithm", CodeSystem = "2.16.840.1.113883.5.1010", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    public enum EncapsulatedDataIntegrityAlgorithm
    {
        /// <summary>
        /// Using a SHA1 hash
        /// </summary>
        [Enumeration(Value = "SHA-1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHA")]
        SHA1,
        /// <summary>
        /// Using a SHA256 hash
        /// </summary>
        [Enumeration(Value = "SHA-256")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHA")]
        SHA256
    }

    /// <summary>
    /// Identifies that a class implements (or is compatible with) an IEncapsulatedData 
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IEncapsulatedData
    {
        /// <summary>
        /// Represents the data contained within the encapsulated data
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        byte[] Data { get; set; }

        /// <summary>
        /// Get or set the desired representation
        /// </summary>
        EncapsulatedDataRepresentation Representation { get; set; }

        /// <summary>
        /// Get or set the language
        /// </summary>
        string Language { get; set; }

        /// <summary>
        /// Get or set the media type
        /// </summary>
        string MediaType { get; set; }

        /// <summary>
        /// Get the integrity check
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        byte[] IntegrityCheck { get; }

        /// <summary>
        /// Get or set the integrity check algorithm  to use
        /// </summary>
        EncapsulatedDataIntegrityAlgorithm? IntegrityCheckAlgorithm { get; set; }

        /// <summary>
        /// Get or set a thumbnail represetnation of this ED
        /// </summary>
        IEncapsulatedData Thumbnail { get;}

        /// <summary>
        /// Get or set the reference to the data
        /// </summary>
        ITelecommunicationAddress Reference { get;}

        /// <summary>
        /// Get or set the compression algorithm used for the data
        /// </summary>
        EncapsulatedDataCompression? Compression { get; }

    }
}