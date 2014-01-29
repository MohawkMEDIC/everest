using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a reference to a complete concept domain
    /// </summary>
    [XmlType("ConceptDomainRefDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class ConceptDomainRefDefinition
    {
        /// <summary>
        /// Gets or sets the name of the referenced system
        /// </summary>
        [XmlAttribute("value")]
        public String ReferencedSystem { get; set; }
    }
}
