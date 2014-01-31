using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a type definition
    /// </summary>
    [XmlType("TypeDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class TypeDefinition
    {
        /// <summary>
        /// Gets the name of the type definition
        /// </summary>
        [XmlAttribute("name")]
        public String Name { get; set; }

        /// <summary>
        /// Gets the template parameters (typically value sets) for the type
        /// </summary>
        [XmlElement("type")]
        public List<TypeDefinition> TemplateParameter { get; set; }

    }
}
