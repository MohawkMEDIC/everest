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
    /// Method invokation statement definition
    /// </summary>
    [XmlType("MethodInvokationStatementDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class MethodInvokationStatementDefinition : AssignmentStatementDefinition, IMethodInstruction
    {

        /// <summary>
        /// Gets or sets the name of the method which this invokation statement executes
        /// </summary>
        [XmlAttribute("method")]
        public String MethodNameRef { get; set; }

        /// <summary>
        /// Gets or sets the parameters to the method invokcation
        /// </summary>
        [XmlElement("param")]
        public List<AssignmentStatementDefinition> Params { get; set; }

        /// <summary>
        /// Represent as a code statement collection
        /// </summary>
        /// <returns></returns>
        public override CodeStatementCollection ToCodeDomStatement(RenderContext context)
        {
            // Invoke the method
            var retVal = new CodeStatementCollection();

            CodeExpression scope = base.GetScopeStatement(context);

            // Now we have scope try to invoke
            CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(
                new CodeMethodReferenceExpression(scope ?? null, this.MethodNameRef)
            );


            foreach (var parm in this.Params)
            {
                // Create the declareation
                String variableName = String.Format("d{0:n}", Guid.NewGuid());

                parm.VariableName = variableName;
                retVal.AddRange(parm.ToCodeDomStatement(context, true));
                invoke.Parameters.Add(new CodeVariableReferenceExpression(parm.VariableName));
            }
            retVal.Add(invoke);
            // Return value
            return retVal;
        }

    }
}
