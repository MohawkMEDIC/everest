using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents an assignment statement
    /// </summary>
    [XmlType("AssignmentStatementDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class AssignmentStatementDefinition : MethodDefinitionBase
    {
        /// <summary>
        /// Scoped property
        /// </summary>
        [XmlAttribute("scope")]
        public String ScopedProperty { get; set; }
        /// <summary>
        /// Gets or sets the name of the property that this set operation works upon
        /// </summary>
        [XmlAttribute("propertyName")]
        public String PropertyName { get; set; }
        /// <summary>
        /// Variable name
        /// </summary>
        [XmlAttribute("variableName")]
        public String VariableName { get; set; }
        /// <summary>
        /// Gets or sets the string value of the statement
        /// </summary>
        [XmlAttribute("value")]
        public String Value { get; set; }
        /// <summary>
        /// Gets or sets the value reference (programmatic statement) that will set the value
        /// </summary>
        [XmlAttribute("valueRef")]
        public String ValueRef { get; set; }

    }
}
