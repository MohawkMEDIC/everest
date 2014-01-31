using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a choice of items that can be selected. This is typically used to suppress the conformance statement for a particular property 
    /// as it will be forced in the validation routine
    /// </summary>
    [XmlType("PropertyChoiceTemplateDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class PropertyChoiceTemplateDefinition : PropertyTemplateContainer
    {
    }
}
