using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.CodeDom;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a formal constraint definition
    /// </summary>
    [XmlType("FormalConstraintDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class FormalConstraintDefinition : MethodDefinitionBase
    {

        /// <summary>
        /// Gets or sets the message to be displayed when the constraint is violated
        /// </summary>
        [XmlAttribute("message")]
        public String Message { get; set; }

        /// <summary>
        /// Render this as a code dom statement
        /// </summary>
        public override System.CodeDom.CodeStatementCollection ToCodeDomStatement(Renderer.RenderContext scope)
        {
            CodeStatementCollection retVal = new CodeStatementCollection();

            retVal.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(Boolean)), "retVal", new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("value"), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null))));
           
            // Define the return value
            CodeBinaryOperatorExpression successCriteria = null;

            // Any statements to get ready for the condition?
            foreach (MethodDefinitionBase itm in this.Instruction.Where(o => !(o is ConditionalStatementDefinition)))
                retVal.AddRange(itm.ToCodeDomStatement(scope));

            var toBeAdded = new List<object>();
            foreach (ConditionalStatementDefinition itm in this.Instruction.Where(o => o is ConditionalStatementDefinition))
            {
                if(itm.VariableName == null)
                    itm.VariableName = "value";
                if (successCriteria == null)
                    successCriteria = itm.BuildConditionExpression(scope);
                else
                    successCriteria = new CodeBinaryOperatorExpression(successCriteria, CodeBinaryOperatorType.BooleanAnd, itm.BuildConditionExpression(scope));

                // Are there any instructions? if so then we need to add them to the master condition as "exists" doesn't do this
                if (itm.Instruction.Count > 0)
                    toBeAdded.AddRange(itm.Instruction.Where(o => !(o is ConditionalStatementDefinition)));
            }
            this.Instruction.AddRange(toBeAdded);
            retVal.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("retVal"), new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("retVal"), CodeBinaryOperatorType.BooleanAnd, successCriteria)));
            retVal.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("retVal")));

            return retVal;
        }
    }
}
