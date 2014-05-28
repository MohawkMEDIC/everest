/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 6-5-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents the abstract base of all artifacts
    /// </summary>
    [XmlType("ArtifactTemplateBase", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public abstract class ArtifactTemplateBase
    {


        /// <summary>
        /// Tag for the method
        /// </summary>
        [XmlElement("tag")]
        public String Tag { get; set; }

        /// <summary>
        /// Gets or sets basic documentation which describes the template
        /// </summary>
        [XmlElement("documentation")]
        public XmlNode[] Documentation { get; set; }

        /// <summary>
        /// Sample render
        /// </summary>
        [XmlElement("sampleRendering")]
        public XmlNode[] Example { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the template
        /// </summary>
        [XmlElement("id")]
        public List<String> Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [XmlAttribute("name")]
        public String Name { get; set; }

        /// <summary>
        /// Clone this object
        /// </summary>
        public Object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
