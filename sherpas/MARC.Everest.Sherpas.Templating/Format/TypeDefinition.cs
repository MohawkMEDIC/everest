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
    public class TypeDefinition : BasicTypeReference
    {

        /// <summary>
        /// Gets the template parameters (typically value sets) for the type
        /// </summary>
        [XmlElement("type", typeof(TypeDefinition))]
        [XmlElement("enum", typeof(BasicTypeReference))]
        public List<BasicTypeReference> TemplateParameter { get; set; }

        /// <summary>
        /// Override to set the template parameter
        /// </summary>
        [XmlIgnore]
        public override Type Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
                if (value.IsGenericType)
                {
                    this.TemplateParameter = new List<BasicTypeReference>();
                    foreach (var t in value.GetGenericArguments())
                        this.TemplateParameter.Add(new BasicTypeReference() { Type = t });
                }
            }
        }
    }
}
