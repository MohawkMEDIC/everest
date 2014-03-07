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
    /// Represents a constructor invocation statement
    /// </summary>
    /// <remarks>A constructor is usually called and then a series of properties used on the variable that was just created</remarks>
    [XmlType("ConstructorInvokationStatementDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class ConstructorInvokationStatementDefinition : MethodDefinitionBase, IMethodInstruction
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


        /// <summary>
        /// Convert the statement to code dom
        /// </summary>
        public override CodeStatementCollection ToCodeDomStatement(RenderContext context)
        {
            if (this.VariableName == null)
                this.VariableName = String.Format("d{0:n}", Guid.NewGuid());

            var retVal = new System.CodeDom.CodeStatementCollection()
            {
                new CodeVariableDeclarationStatement(new CodeTypeReference(this.TypeName), this.VariableName, new CodeObjectCreateExpression(new CodeTypeReference(this.TypeName)))
            };
            foreach (AssignmentStatementDefinition itm in this.Instruction.Where(i => i is AssignmentStatementDefinition))
            {
                itm.VariableName = this.VariableName;
                retVal.AddRange(itm.ToCodeDomStatement(context));
            }
            return retVal;
        }
    }
}
