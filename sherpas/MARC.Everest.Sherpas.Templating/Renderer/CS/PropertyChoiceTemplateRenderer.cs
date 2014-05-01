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
