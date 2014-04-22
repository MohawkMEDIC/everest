using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.CodeDom;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Defines a code segment that repeats over the course of the specified property
    /// </summary>
    [XmlType("CodeIterationStatementDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class IterationStatementDefinition : PropertyReferenceInstructionDefinition, IMethodInstruction
    {

        /// <summary>
        /// Return a code dom statement
        /// </summary>
        public override System.CodeDom.CodeStatementCollection ToCodeDomStatement(Renderer.RenderContext context)
        {
            CodeStatementCollection retVal = new CodeStatementCollection();
            var scope = base.GetScopeStatement(context);
            
            var forLoop = new CodeIterationStatement(new CodeVariableDeclarationStatement(typeof(int), "idx", new CodePrimitiveExpression(0)), 
                new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("idx"), CodeBinaryOperatorType.LessThan, new CodePropertyReferenceExpression(scope, "Count")),
                new CodeSnippetStatement("idx++"));

            foreach (IMethodInstruction instr in this.Instruction)
            {
                forLoop.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference("var"), "instance", new CodeIndexerExpression(scope, new CodeVariableReferenceExpression("idx"))));
                var prid = instr as PropertyReferenceInstructionDefinition;
                if (prid != null)
                    prid.VariableName = "instance";
                forLoop.Statements.AddRange(instr.ToCodeDomStatement(context));
            }

            retVal.Add(new CodeConditionStatement(new CodeBinaryOperatorExpression(scope, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), forLoop));

            // Create the scoping
            return retVal;
        }
    }
}
