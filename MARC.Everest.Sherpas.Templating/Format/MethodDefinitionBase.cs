using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a base class which defines a series of instructions for use in a method (initialization or validation)
    /// </summary>
    [XmlType("MethodDefinitionBase", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public abstract class MethodDefinitionBase 
    {

        /// <summary>
        /// Identifies the steps to take in the method
        /// </summary>
        [XmlElement("set", typeof(AssignmentStatementDefinition))]
        [XmlElement("call", typeof(MethodInvokationStatementDefinition))]
        [XmlElement("when", typeof(ConditionalStatementDefinition))]
        public List<Object> Instruction { get; set; }

    }
}
