/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: fyfej
 * Date: 20-4-2014
 */
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
