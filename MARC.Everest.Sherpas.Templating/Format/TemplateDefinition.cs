using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a template definition file
    /// </summary>
    [XmlType("TemplateDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    [XmlRoot("Template", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class TemplateDefinition
    {
        /// <summary>
        /// Get the templated classes
        /// </summary>
        [XmlElement("classTemplate", typeof(ClassTemplateDefinition))]
        [XmlElement("enumerationTemplate", typeof(EnumerationTemplateDefinition))]
        public List<ArtifactTemplateBase> Templates { get; set; }

    }
}
