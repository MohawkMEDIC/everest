using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a restriction on a class
    /// </summary>
    [XmlType("ClassTemplate", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class ClassTemplateDefinition : ArtifactTemplateBase
    {

        /// <summary>
        /// Gets or sets the base class for the template definition
        /// </summary>
        [XmlIgnore]
        public Type BaseClass { get; private set; }

        /// <summary>
        /// Gets or sets the fully qualified name of the base class
        /// </summary>
        [XmlAttribute("baseClass")]
        public String BaseClassName
        {
            get
            {
                return this.BaseClass == null ? null : this.BaseClass.AssemblyQualifiedName;
            }
            set
            {
                this.BaseClass = Type.GetType(value);
            }
        }

        /// <summary>
        /// Template instructions applied to the class
        /// </summary>
        [XmlElement("propertyTemplate", typeof(PropertyTemplateDefinition))]
        public List<ArtifactTemplateBase> Templates { get; set; }

        /// <summary>
        /// Gets or sets the validation rules
        /// </summary>
        [XmlElement("validationRule")]
        public List<ValidationRule> Validation { get; set; }
    }
}
