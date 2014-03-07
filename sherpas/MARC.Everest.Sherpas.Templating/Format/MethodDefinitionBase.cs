using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.CodeDom;
using MARC.Everest.Sherpas.Templating.Renderer;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a base class which defines a series of instructions for use in a method (initialization or validation)
    /// </summary>
    [XmlType("MethodDefinitionBase", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class MethodDefinitionBase : IMethodInstruction
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MethodDefinitionBase()
        {
            this.Instruction = new List<Object>();
        }


        /// <summary>
        /// Identifies the steps to take in the method
        /// </summary>
        [XmlElement("set", typeof(AssignmentStatementDefinition))]
        [XmlElement("call", typeof(MethodInvokationStatementDefinition))]
        [XmlElement("where", typeof(ConditionalStatementDefinition))]
        [XmlElement("construct", typeof(ConstructorInvokationStatementDefinition))]
        public virtual List<Object> Instruction { get; set; }

        /// <summary>
        /// To code dom statemnet
        /// </summary>
        public virtual CodeStatementCollection ToCodeDomStatement(RenderContext scope)
        {
            var retVal = new CodeStatementCollection();
            foreach (IMethodInstruction ins in this.Instruction)
            {
                retVal.AddRange(ins.ToCodeDomStatement(scope));
            }
            return retVal;
        }
    }
}
