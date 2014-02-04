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
        [XmlAttribute("id")]
        public String Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [XmlAttribute("name")]
        public String Name { get; set; }
    }
}
