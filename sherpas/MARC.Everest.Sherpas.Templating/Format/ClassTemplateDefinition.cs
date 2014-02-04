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
    public class ClassTemplateDefinition : PropertyTemplateContainer
    {

        /// <summary>
        /// Gets or sets the fully qualified name of the base class
        /// </summary>
        [XmlElement("baseClass")]
        public BasicTypeReference BaseClass
        {
            get;
            set;
        }

        
       
    }
}
