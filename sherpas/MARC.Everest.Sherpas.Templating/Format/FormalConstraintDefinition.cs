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
