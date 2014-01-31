using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a reenumeration of a value set
    /// </summary>
    [XmlType("EnumerationTemplateDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class EnumerationTemplateDefinition : ArtifactTemplateBase
    {
        /// <summary>
        /// Gets or sets a reference to a concept domain
        /// </summary>
        [XmlElement("conceptDomainRef")]
        public ConceptDomainRefDefinition ConceptDomainReference { get; set; }

        /// <summary>
        /// Gets or sets a literal definition
        /// </summary>
        [XmlElement("literal")]
        public List<EnumerationLiteralDefinition> MyProperty { get; set; }
    }
}
