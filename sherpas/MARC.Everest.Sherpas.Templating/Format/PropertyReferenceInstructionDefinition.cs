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
using MARC.Everest.Sherpas.Templating.Renderer;
using System.CodeDom;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents an instruction definition that can reference a property or a variable
    /// </summary>
    [XmlType("PropertyReferenceInstructionDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public abstract class PropertyReferenceInstructionDefinition : MethodDefinitionBase, IMethodInstruction
    {

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
        /// Gets the variable name
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public CodeExpression GetScopeStatement(RenderContext context, CodeStatementCollection ctorStatements = null, CodeExpressionCollection conditionalStatements = null)
        {

            Stack<PropertyTemplateDefinition> scopeStack = new Stack<PropertyTemplateDefinition>();
            while (!(context.Artifact is ClassTemplateDefinition))
            {
                scopeStack.Push(context.Artifact as PropertyTemplateDefinition);
                context = context.Parent;
            }

            CodeExpression scope = new CodeThisReferenceExpression();
            if (this.VariableName != null)
            {
                scope = new CodeVariableReferenceExpression(this.VariableName);
            }
            else while (scopeStack.Count > 0)
                {
                    var tpl = scopeStack.Pop();
                    scope = new CodePropertyReferenceExpression(scope, tpl.Name);
                    if (ctorStatements != null && !tpl.Property.PropertyType.IsAbstract)
                    {

                        // Do we want a conversion or just a assignment?
                        var currentObjectGen = context.Artifact as ClassTemplateDefinition;
                        var trueStatements = new CodeStatementCollection() {
                            new CodeAssignStatement(scope, new CodeObjectCreateExpression(new CodeTypeReference(tpl.Type != null ? tpl.Type.Type : tpl.Property.PropertyType)))
                        };

                        var ifNullStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(scope, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)));
                        if (tpl.Property.PropertyType.GetInterface(typeof(IList<>).FullName) != null)
                        {
                            trueStatements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(scope, "Add"), new CodeObjectCreateExpression(new CodeTypeReference(tpl.Property.PropertyType.GetGenericArguments()[0]))));
                            scope = new CodeArrayIndexerExpression(scope, new CodePrimitiveExpression(0));
                        }
                        ifNullStatement.TrueStatements.AddRange(trueStatements);

                        ctorStatements.Add(ifNullStatement);
                    }
                    else if (conditionalStatements != null)
                    {
                        conditionalStatements.Add(new CodeBinaryOperatorExpression(scope, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)));

                    }
                }
            if (this.PropertyName != null)
                scope = new CodePropertyReferenceExpression(scope, this.PropertyName);
            return scope;
        }

    }
}
