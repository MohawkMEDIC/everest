using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Template.Interface;
using MARC.Everest.Sherpas.Templating.Format;
using System.CodeDom;
using MARC.Everest.Attributes;
using System.Diagnostics;
using MARC.Everest.Sherpas.Interface;
using MARC.Everest.Connectors;

namespace MARC.Everest.Sherpas.Templating.Renderer.CS
{
    /// <summary>
    /// Class template renderer
    /// </summary>
    public class ClassTemplateRenderer : IArtifactRenderer
    {
        #region IArtifactRenderer Members

        /// <summary>
        /// Artifact type this renderer renders
        /// </summary>
        public Type ArtifactTemplateType
        {
            get { return typeof(ClassTemplateDefinition); }
        }

        /// <summary>
        /// Render the artifact
        /// </summary>
        public System.CodeDom.CodeTypeMemberCollection Render(RenderContext context)
        {
            var tpl = context.Artifact as ClassTemplateDefinition;

            // emit the enum
            CodeTypeDeclaration retVal = new CodeTypeDeclaration(tpl.Name);
            context.CurrentObject = retVal;
            retVal.IsClass = true;
            retVal.Attributes = MemberAttributes.Public;

            // Get the base class
            var structureAttribute = tpl.BaseClass.Type.GetCustomAttributes(typeof(StructureAttribute), false)[0] as StructureAttribute;
            
            // Add structure attribute
            retVal.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(StructureAttribute)),
                new CodeAttributeArgument("Name", new CodePrimitiveExpression(structureAttribute.Name)),
                new CodeAttributeArgument("StructureType", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(MARC.Everest.Attributes.StructureAttribute.StructureAttributeType)), "MessageType")),
                new CodeAttributeArgument("Model", new CodePrimitiveExpression(structureAttribute.Model)),
                new CodeAttributeArgument("Publisher", new CodePrimitiveExpression(structureAttribute.Publisher))
            ));

            if(tpl.Id != null)
                foreach(var id in tpl.Id)
                    retVal.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(TemplateAttribute)), new CodeAttributeArgument("TemplateId", new CodePrimitiveExpression(id))));

            retVal.Comments.AddRange(RenderUtils.RenderComments(tpl, String.Format("{0} is a template for <see cref=\"T:{1}\"/>", tpl.Name, tpl.BaseClass.Type.FullName)));

            // base class
            retVal.BaseTypes.Add(new CodeTypeReference(tpl.BaseClass.Type));

            // Initialization method
            CodeMemberMethod initializeInstanceMethod = new CodeMemberMethod()
            {
                Name = "InitializeInstance",
                ReturnType = new CodeTypeReference(typeof(void)),
                Attributes = MemberAttributes.Public
            },
            validateMethod = new CodeMemberMethod()
            {
                Name = "ValidateEx",
                ReturnType = new CodeTypeReference(typeof(IEnumerable<IResultDetail>)),
                Attributes = MemberAttributes.Public | MemberAttributes.Override
            };

            // Validate the method setup
            validateMethod.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(List<IResultDetail>)), "retVal", new CodeCastExpression(new CodeTypeReference(typeof(List<IResultDetail>)),  new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeBaseReferenceExpression(), "ValidateEx")))));
            foreach (var itm in tpl.Validation)
                validateMethod.Statements.AddRange(itm.ToCodeDomStatement(context));

            // Initialize method setup
            foreach (var itm in tpl.Initialize)
                initializeInstanceMethod.Statements.AddRange(itm.ToCodeDomStatement(context));

            retVal.Members.Add(initializeInstanceMethod);
            retVal.Members.Add(validateMethod);

            // Render the methods ... This should be interesting ... yikes!
            foreach (var itm in tpl.Templates)
            {
                var childContext = new RenderContext(context, itm, retVal);
                var renderer = childContext.GetRenderer();
                if (renderer == null)
                    Trace.TraceError("Could not find renderer for type '{0}'...", itm.GetType().Name);
                else
                    retVal.Members.AddRange(renderer.Render(childContext));
            }

            validateMethod.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("retVal")));

            // Add the interface for the IMessageTypeTemplate interface
            retVal.BaseTypes.Add(new CodeTypeReference(typeof(IMessageTypeTemplate)));

            return new CodeTypeMemberCollection(new CodeTypeMember[] { retVal });
        }

        #endregion
    }
}
