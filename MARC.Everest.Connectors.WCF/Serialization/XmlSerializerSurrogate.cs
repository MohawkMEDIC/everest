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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using MARC.Everest.Interfaces;
using MARC.Everest.Xml;

namespace MARC.Everest.Connectors.WCF.Serialization
{
    /// <summary>
    /// Surrogate that exposes an XML object Serializer to the .NET WCF Framework.
    /// </summary>
    internal class XmlSerializerSurrogate : XmlObjectSerializer
    {

        /// <summary>
        /// Gets or sets the structured formatter to use.
        /// </summary>
        public IXmlStructureFormatter Formatter { get; set; }
        /// <summary>
        /// Gets or sets the Result code.
        /// </summary>
        public ResultCode ResultCode { get; set; }
        /// <summary>
        /// Gets or sets the details of the format operation
        /// </summary>
        public IEnumerable<IResultDetail> Details { get; set; }

        //DOC: Describe the formatter parameter.
        /// <summary>
        /// Create a new instance of the XmlSerializationSurrogate.
        /// </summary>
        /// <param name="formatter"></param>
        public XmlSerializerSurrogate(IXmlStructureFormatter formatter)
        {
            this.Formatter = formatter;
        }

        /// <summary>
        /// Returns true if the current node of <paramref name="reader"/> is suitable to start deserialization.
        /// </summary>
        public override bool IsStartObject(System.Xml.XmlDictionaryReader reader)
        {
            return reader.NamespaceURI == "urn:hl7-org:v3";
        }

        /// <summary>
        /// Deserializes an object from <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The XmlReader to deserialize the object from.</param>
        /// <param name="verifyObjectName">True if the object's name should be verified prior to deserializing.</param>
        /// <returns>The deserialized object or null if deserialization failed.</returns>
        public override object ReadObject(System.Xml.XmlDictionaryReader reader, bool verifyObjectName)
        {
            var result = Formatter.Parse(new XmlStateReader(reader));
            this.Details = result.Details;
            this.ResultCode = result.Code;
            return result.Structure;
        }

        /// <summary>
        /// Not Implemented: The formatter does not make a distinction between the start and end of an object.
        /// </summary>
        public override void WriteEndObject(System.Xml.XmlDictionaryWriter writer)
        {
            // Nothing to do
        }

        /// <summary>
        /// Write the content of <paramref name="graph"/> onto <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer to serialize <paramref name="graph"/>.</param>
        /// <param name="graph">The object to graph to <paramref name="writer"/>.</param>
        public override void WriteObjectContent(System.Xml.XmlDictionaryWriter writer, object graph)
        {
            
            XmlStateWriter xsw = new XmlStateWriter(writer as XmlWriter);
            var result = Formatter.Graph(xsw, (IGraphable)graph);
            this.Details = result.Details;
            this.ResultCode = result.Code;
        }

        /// <summary>
        /// Not Implemented: The formatter does not make a distinction between the start and end of an object.
        /// </summary>
        public override void WriteStartObject(System.Xml.XmlDictionaryWriter writer, object graph)
        {
           // Nothing to do
        }
    }
}