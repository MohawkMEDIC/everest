using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a constructor invocation statement
    /// </summary>
    /// <remarks>A constructor is usually called and then a series of properties used on the variable that was just created</remarks>
    [XmlType("ConstructorInvokationStatementDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class ConstructorInvokationStatementDefinition : MethodDefinitionBase
    {
        /// <summary>
        /// Gets or sets the name of the type to be constructed
        /// </summary>
        [XmlAttribute("type")]
        public String TypeName { get; set; }
        /// <summary>
        /// An optional alias for the variable in the context of the method
        /// </summary>
        [XmlAttribute("variableName")]
        public String VariableName { get; set; }

    }
}
