using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

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
        [XmlElement("doc")]
        public String Documentation { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the template
        /// </summary>
        [XmlElement("templateId")]
        public List<String> Id { get; set; }
    }
}
