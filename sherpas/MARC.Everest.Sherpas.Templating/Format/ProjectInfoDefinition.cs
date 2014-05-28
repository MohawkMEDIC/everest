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
 * Date: 4-2-2014
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
    /// Represents template project information
    /// </summary>
    [XmlType("TemplateProjectInfoDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class ProjectInfoDefinition
    {
        /// <summary>
        /// Project information definition
        /// </summary>
        public ProjectInfoDefinition()
        {
            this.OriginalAuthor = new List<string>();
        }

        /// <summary>
        /// Gets or sets the name of the project
        /// </summary>
        [XmlElement("name")]
        public String Name { get; set; }
        /// <summary>
        /// Gets or sets the copyright holder(s) of the project
        /// </summary>
        [XmlElement("copyrightHolder")]
        public XmlElement[] Copyright { get; set; }
        /// <summary>
        /// Gets or sets the original authors of the project
        /// </summary>
        [XmlElement("originalAuthor")]
        public List<String> OriginalAuthor { get; set; }
        /// <summary>
        /// Gets or sets the version 
        /// </summary>
        [XmlElement("version")]
        public String Version { get; set; }
        /// <summary>
        /// Assembly reference
        /// </summary>
        [XmlAttribute("assembly")]
        public String AssemblyRef { get; set; }

        /// <summary>
        /// Gets or sets basic documentation which describes the template
        /// </summary>
        [XmlElement("documentation")]
        public XmlNode[] Documentation { get; set; }

    }
}
