using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Property template container which contains other property templates
    /// </summary>
    [XmlType("PropertyTemplateContainer", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public abstract class PropertyTemplateContainer : ArtifactTemplateBase
    {
        /// <summary>
        /// Template instructions applied to the class
        /// </summary>
        [XmlElement("propertyTemplate", typeof(PropertyTemplateDefinition))]
        [XmlElement("propertyChoiceTemplate", typeof(PropertyChoiceTemplateDefinition))]
        public List<PropertyTemplateContainer> Templates { get; set; }

        /// <summary>
        /// Gets or sets the validation rules
        /// </summary>
        [XmlElement("validationInstruction")]
        public List<ValidationInstructionDefinition> Validation { get; set; }

        /// <summary>
        /// Gets or sets the validation rules
        /// </summary>
        //[XmlElement("initialize")]
        //public List<InitializationInstructionDefinition> Initialize { get; set; }
    }
}
