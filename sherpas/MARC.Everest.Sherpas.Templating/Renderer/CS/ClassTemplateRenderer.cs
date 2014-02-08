using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Template.Interface;
using MARC.Everest.Sherpas.Templating.Format;
using System.CodeDom;
using MARC.Everest.Attributes;

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

            foreach(var id in tpl.Id)
                retVal.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(TemplateAttribute)), new CodeAttributeArgument("TemplateId", new CodePrimitiveExpression(id))));

            // Documentation
            if (tpl.Documentation != null)
            {
                retVal.Comments.Add(new CodeCommentStatement(new CodeComment(String.Format("<summary>{0} is a template for <see cref=\"T:{1}\"/></summary>", tpl.Name, tpl.BaseClass.Type.FullName), true)));
                retVal.Comments.Add(new CodeCommentStatement(new CodeComment("<remarks>", true)));
                foreach (var doc in tpl.Documentation)
                    retVal.Comments.Add(new CodeCommentStatement(new CodeComment(doc.OuterXml, true)));
                retVal.Comments.Add(new CodeCommentStatement(new CodeComment("</remarks>", true)));
            }
            if (tpl.Example != null)
            {
                retVal.Comments.Add(new CodeCommentStatement(new CodeComment("<example><code lang=\"xml\"><![CDATA[", true)));
                foreach (var ex in tpl.Example)
                    retVal.Comments.Add(new CodeCommentStatement(new CodeComment(ex.OuterXml, true)));
                retVal.Comments.Add(new CodeCommentStatement(new CodeComment("]]></code></example>", true)));
            }

            // base class
            retVal.BaseTypes.Add(new CodeTypeReference(tpl.BaseClass.Type));

            // Render the methods ... This should be interesting ... yikes!
            

            return new CodeTypeMemberCollection(new CodeTypeMember[] { retVal });
        }

        #endregion
    }
}
