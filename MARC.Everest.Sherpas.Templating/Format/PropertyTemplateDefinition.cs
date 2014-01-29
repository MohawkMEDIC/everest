using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MARC.Everest.Attributes;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Property template container
    /// </summary>
    [XmlType("PropertyTemplateDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class PropertyTemplateDefinition : PropertyTemplateContainer
    {
        /// <summary>
        /// Gets or sets the type that this particular property should take
        /// </summary>
        [XmlElement("type")]
        public List<TypeDefinition> Type { get; set; }

        /// <summary>
        /// Indicates the conformance of the property
        /// </summary>
        [XmlAttribute("conformance")]
        public PropertyAttribute.AttributeConformanceType Conformance { get; set; }

        /// <summary>
        /// Indicates the minimum occurance of an element
        /// </summary>
        [XmlAttribute("minOccurs")]
        public String MinOccurs { get; set; }

        /// <summary>
        /// Indicates the maximum occurance for an element
        /// </summary>
        [XmlAttribute("maxOccurs")]
        public String MaxOccurs { get; set; }

        /// <summary>
        /// Indicates that the value of this property should be drawn form the specified type
        /// </summary>
        [XmlAttribute("ref")]
        public String TemplateReference { get; set; }
    }
}
