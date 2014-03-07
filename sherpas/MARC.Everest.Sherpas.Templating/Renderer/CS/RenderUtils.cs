using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Format;
using System.CodeDom;

namespace MARC.Everest.Sherpas.Templating.Renderer.CS
{
    /// <summary>
    /// Rendering utilities for all the other renderers
    /// </summary>
    public static class RenderUtils
    {
        /// <summary>
        /// Render comments from the template
        /// </summary>
        public static CodeCommentStatementCollection RenderComments(ArtifactTemplateBase tpl, string summary)
        {
            CodeCommentStatementCollection retVal = new CodeCommentStatementCollection();

            retVal.Add(new CodeCommentStatement(new CodeComment(String.Format("<summary>{0}</summary>", summary), true)));

            // Documentation
            if (tpl.Documentation != null)
            {
                retVal.Add(new CodeCommentStatement(new CodeComment("<remarks>", true)));
                foreach (var doc in tpl.Documentation)
                    retVal.Add(new CodeCommentStatement(new CodeComment(doc.OuterXml, true)));
                retVal.Add(new CodeCommentStatement(new CodeComment("</remarks>", true)));
            }
            if (tpl.Example != null)
            {
                retVal.Add(new CodeCommentStatement(new CodeComment("<example><code lang=\"xml\"><![CDATA[", true)));
                foreach (var ex in tpl.Example)
                    retVal.Add(new CodeCommentStatement(new CodeComment(ex.OuterXml, true)));
                retVal.Add(new CodeCommentStatement(new CodeComment("]]></code></example>", true)));
            }

            return retVal;
        }

        /// <summary>
        /// Create a string type reference to the specified code
        /// </summary>
        public static CodeTypeReference CreateTypeReference(BasicTypeReference templateType)
        {
            CodeTypeReference retVal = null;
            if (templateType.Type != null)
                retVal = new CodeTypeReference(templateType.Type);
            else
                retVal = new CodeTypeReference(templateType.Name);
            if (templateType is TypeDefinition)
            {
                var td = templateType as TypeDefinition;
                if(td.TemplateParameter != null)
                {
                    retVal.TypeArguments.Clear();
                    foreach (var t in td.TemplateParameter)
                        retVal.TypeArguments.Add(CreateTypeReference(t));
                }
                
            }

            // Correct any defaults
            if (templateType.Type != null &&
                templateType.Type.IsGenericType &&
                retVal.TypeArguments.Count != templateType.Type.GetGenericArguments().Length)
                foreach (var itm in templateType.Type.GetGenericArguments())
                    retVal.TypeArguments.Add(new CodeTypeReference(itm));
            return retVal;
        }

        /// <summary>
        /// Type reference equals each other
        /// </summary>
        internal static bool TypeReferenceEquals(CodeTypeReference a, CodeTypeReference b)
        {
            bool equals = a.BaseType == b.BaseType && a.TypeArguments.Count == b.TypeArguments.Count;
            for (int i = 0; i < a.TypeArguments.Count && equals; i++)
                equals &= TypeReferenceEquals(a.TypeArguments[i], b.TypeArguments[i]);
            return equals;
        }
    }
}
