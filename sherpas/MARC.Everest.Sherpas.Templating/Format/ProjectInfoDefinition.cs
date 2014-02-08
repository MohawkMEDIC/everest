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
    }
}
