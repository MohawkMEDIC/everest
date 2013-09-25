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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.DynamicModel;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Sterotype of an interface
    /// </summary>
    /// <remarks>
    /// Graphical elements have been removed from the model
    /// </remarks>
    [XmlType(TypeName = "EntryPointBase", Namespace = "urn:hl7-org:v3/mif2")]
    public class EntryPointBase : Interface
    {

        private StaticModelUseKind useKind;
        private string id;
        private string name;
        private Annotation annotations;

        /// <summary>
        /// Descriptive information about the entry point
        /// </summary>
        [XmlElement(ElementName = "annotations")]
        public Annotation Annotations
        {
            get { return annotations; }
            set { annotations = value; }
        }
	

        /// <summary>
        /// The descriptive name associated with the entry point
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
	

        /// <summary>
        /// The old style identifier associated with the model that is tied to the entry point
        /// </summary>
        [XmlAttribute("id")]
        //[Obsolete("The 'old'-style identifier associated with the model tied to the entry point")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
	

        /// <summary>
        /// Identifies the type of content represented by the model when entered from this entry
        /// point. The contentType determines whether the model is legitamite content for classStub from 
        /// another model.
        /// </summary>
        [XmlAttribute("useKind")]
        public StaticModelUseKind UseKind
        {
            get { return useKind; }
            set { useKind = value; }
        }
	
    }
}