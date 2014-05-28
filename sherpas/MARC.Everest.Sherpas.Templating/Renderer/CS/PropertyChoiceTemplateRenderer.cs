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
 * Date: 6-5-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Template.Interface;
using MARC.Everest.Sherpas.Templating.Format;
using System.Diagnostics;
using System.CodeDom;
using MARC.Everest.Attributes;

namespace MARC.Everest.Sherpas.Templating.Renderer.CS
{
    /// <summary>
    /// Represents a property choice renderer
    /// </summary>
    public class PropertyChoiceTemplateRenderer : IArtifactRenderer
    {
        /// <summary>
        /// Artifact template type
        /// </summary>
        public Type ArtifactTemplateType
        {
            get { return typeof(PropertyChoiceTemplateDefinition); }
        }

        /// <summary>
        /// Render the choice 
        /// </summary>
        public System.CodeDom.CodeTypeMemberCollection Render(RenderContext context)
        {

            var choiceTemplate = context.Artifact as PropertyChoiceTemplateDefinition;

            // Was this bound? If not then don't render
            if (choiceTemplate.Property == null)
            {
                Trace.TraceInformation("Property {0} was never bound!", choiceTemplate.TraversalName);
                return new CodeTypeMemberCollection();
            }

            Trace.TraceInformation("Entering binder for choice template '{0}'", choiceTemplate.TraversalName);
            var tFakeClass = new CodeTypeDeclaration();

            // Render the sub-properties
            foreach (PropertyTemplateDefinition subProperty in choiceTemplate.Templates)
            {
                RenderContext childContext = new RenderContext(context.Parent, subProperty, tFakeClass);
                var renderer = childContext.GetRenderer();
                if (renderer == null)
                    Trace.TraceError("Could not find renderer for type {0}", subProperty.GetType().FullName);
                else
                    tFakeClass.Members.AddRange(renderer.Render(childContext));


            
            }

            // Clean up the contains relationship stuff
            foreach(CodeAttributeDeclaration attr in tFakeClass.CustomAttributes)
                if (attr.AttributeType.BaseType == typeof(FormalConstraintAttribute).FullName)
                {
                    var methName = (attr.Arguments.OfType<CodeAttributeArgument>().FirstOrDefault(a => a.Name == "CheckConstraintMethod").Value as CodePrimitiveExpression).Value.ToString();
                    tFakeClass.Members.Remove(tFakeClass.Members.OfType<CodeTypeMember>().First(o => o.Name == methName));
                }

            Trace.TraceInformation("Exiting binder for choice template '{0}'", choiceTemplate.TraversalName);

            // TODO: Generate parameters
            return tFakeClass.Members;
        }
    }
}
