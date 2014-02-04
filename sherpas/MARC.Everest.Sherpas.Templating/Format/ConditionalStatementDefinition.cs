using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Indicates a condition if statement
    /// </summary>
    [XmlType("ConditionalStatementDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class ConditionalStatementDefinition : MethodDefinitionBase
    {
        /// <summary>
        /// Gets or sets the name of the property that this value is testing
        /// </summary>
        [XmlAttribute("propertyName")]
        public String PropertyName { get; set; }
        /// <summary>
        /// Gets or sets the name of the local variable this is testing
        /// </summary>
        [XmlAttribute("variableName")]
        public String VariableName { get; set; }
        /// <summary>
        /// Gets or sets the name of the operator that this is testing
        /// </summary>
        [XmlAttribute("operator")]
        public OperatorType Operator { get; set; }
        /// <summary>
        /// Gets or sets the value being compared to
        /// </summary>
        [XmlAttribute("value")]
        public String Value { get; set; }
        /// <summary>
        /// Gets or sets a programmatic reference to the comparison
        /// </summary>
        [XmlAttribute("valueRef")]
        public String ValueRef { get; set; }

    }
}
