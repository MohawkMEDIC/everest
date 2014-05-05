using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Format;
using System.CodeDom;
using System.Text.RegularExpressions;
using MARC.Everest.Attributes;
using System.ComponentModel;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using System.Xml;
using System.IO;
using MARC.Everest.Sherpas.Formatter.XML.ITS1;

namespace MARC.Everest.Sherpas.Templating.Renderer.CS
{
    /// <summary>
    /// Rendering utilities for all the other renderers
    /// </summary>
    public static class RenderUtils
    {

        /// <summary>
        /// Enumerable class name
        /// </summary>
        public static readonly List<String> EnumerableClassNames = new List<string>()
        {
            typeof(SET<>).FullName,
            typeof(COLL<>).FullName,
            typeof(LIST<>).FullName,
            typeof(List<>).FullName,
            typeof(BAG<>).FullName,
            typeof(HIST<>).FullName,
            typeof(SLIST<>).FullName
        };

        /// <summary>
        /// Render comments from the template
        /// </summary>
        public static CodeCommentStatementCollection RenderComments(ArtifactTemplateBase tpl, string summary)
        {
            CodeCommentStatementCollection retVal = new CodeCommentStatementCollection();

            if(summary != null)
                retVal.Add(new CodeCommentStatement(new CodeComment(String.Format("<summary>{0}</summary>", summary), true)));
            else if(tpl.Documentation != null)
                foreach(var doc in tpl.Documentation)
                    if (doc.OuterXml.Length > 0)
                    {
                        retVal.Add(new CodeCommentStatement(new CodeComment(String.Format("<summary>{0}</summary>", doc.OuterXml), true)));
                        break;
                    }

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
                foreach (var ex in tpl.Example)
                    using (StringWriter sw = new StringWriter())
                    {
                        using (XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true }))
                            ex.WriteTo(xw);
                        retVal.Add(new CodeCommentStatement(new CodeComment("<example><code lang=\"xml\"><![CDATA[", true)));
                        foreach (var lin in sw.ToString().Split('\n'))
                            retVal.Add(new CodeCommentStatement(new CodeComment(lin.Replace("\r",""), true)));
                        retVal.Add(new CodeCommentStatement(new CodeComment("]]></code></example>", true)));
                    }
            }

            return retVal;
        }

        /// <summary>
        /// Create a string type reference to the specified code
        /// </summary>
        public static CodeTypeReference CreateTypeReference(BasicTypeReference templateType, string maxOccurs, TemplateProjectDefinition project)
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
                    List<CodeTypeReference> tplParms = new List<CodeTypeReference>();
                    foreach (var t in td.TemplateParameter)
                        if (t.Name != null || t.Type != null)
                        {
                            var crt = CreateTypeReference(t, "1", project);
                            if ((t.Type != null || project.Templates.Exists(p => p.Name == crt.BaseType)) && crt.BaseType != null && crt.BaseType != typeof(void).ToString())
                            {
                                var tr = CreateTypeReference(t, "1", project);
                                if(templateType.Type.GetInterface(typeof(ICodedSimple).FullName) != null && t.Type == null)
                                    tr.BaseType = "Vocabulary." + tr.BaseType;
                                tplParms.Add(tr);
                            }
                        }


                    if (tplParms.Count == retVal.TypeArguments.Count)
                    {
                        retVal.TypeArguments.Clear();
                        retVal.TypeArguments.AddRange(tplParms.ToArray());
                    }
                }
                
            }

            // Correct any defaults
            if (templateType.Type != null &&
                templateType.Type.IsGenericType)
            {
                if(retVal.TypeArguments.Count != templateType.Type.GetGenericArguments().Length)
                    foreach (var itm in templateType.Type.GetGenericArguments())
                        retVal.TypeArguments.Add(new CodeTypeReference(itm));
                
            }

            // Correct multiples if we auto-gen'd this
            if (templateType.Type == null && maxOccurs != null && (maxOccurs == "*" || Int32.Parse(maxOccurs) > 1) && retVal.BaseType != typeof(List<>).FullName)
            {
                retVal = new CodeTypeReference(typeof(List<>).AssemblyQualifiedName, retVal);
            }

            
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

        /// <summary>
        /// Negate all conditions in the list adding them back to the list
        /// </summary>
        internal static List<object> NegateConditions(List<object> instructions)
        {

            foreach (var itm in instructions)
            {
                ConditionalStatementDefinition cond = itm as ConditionalStatementDefinition;
                if (cond != null)
                {
                    if ((cond.Operator & OperatorType.Not) != null) // negate a not
                        cond.Operator &= (OperatorType)~(int)OperatorType.Not;
                    else
                        cond.Operator |= OperatorType.Not;
                }
            }

            return instructions;
        }

        /// <summary>
        /// Create a PascalCasedName
        /// </summary>
        public static String PascalCaseName(String original)
        {

            if (original == null || original.Length == 0) return original;
            original = original.Trim();
            string retVal = "";
            foreach (string s in original.Split(' ', '/'))
            {
                if (s.Length > 1)
                    retVal += s.ToUpper().Substring(0, 1) + s.Substring(1);
                else
                    retVal += s.ToUpper() + "_";
            }
            return MakeFriendlyName(retVal);
        }

        /// <summary>
        /// Make a friendly C# name
        /// </summary>
        public static String MakeFriendlyName(String original)
        {
            if (original == null || original.Length == 0) return original;

            string retVal = original;
            foreach (char c in original)
            {
                Regex validChars = new Regex("[A-Za-z0-9_]");
                if (!validChars.IsMatch(c.ToString()))
                {
                    switch (c)
                    {
                        case '*':
                            retVal = retVal.Replace(c.ToString(), "Star");
                            break;
                        case '^':
                            retVal = retVal.Replace(c.ToString(), "Caret");
                            break;
                        default:
                            retVal = retVal.Replace(c.ToString(), "_");
                            break;
                    }
                }
            }

            // Remove non-code chars
            //foreach (char c in nonCodeChars)
            //    retVal = retVal.Replace(c.ToString(), "");
            //retVal = retVal.Replace("-", "_");


            // Check for numeric start
            Regex re = new Regex("^[0-9]");
            if (re.Match(retVal).Success)
                retVal = "_" + retVal;

            return retVal.Length == 0 ? null : retVal;
        }

        /// <summary>
        /// Generate the code member method for the check
        /// </summary>
        /// <param name="fcd"></param>
        /// <param name="constraintName"></param>
        /// <param name="scanType"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static CodeMemberMethod RenderFormalConstraintValidator(
            FormalConstraintDefinition fcd,
            String constraintName,
            CodeTypeReference scanType, 
            RenderContext context)
        {
            // Name the constraint
            fcd.Tag = String.Format("{0}Check", constraintName);
            int uniqueMethodId = 0;
            var cdt = context.ContainerObject as CodeTypeDeclaration;
            if (cdt == null)
                cdt = context.CurrentObject as CodeTypeDeclaration;
            while (cdt.Members.OfType<CodeMemberMethod>().Count(m => m.Name == fcd.Tag) > 0)
                fcd.Tag = String.Format("{0}Check{1}", constraintName, uniqueMethodId++);

            

            // Construct the validate method
            var method = new CodeMemberMethod()
            {
                Name = fcd.Tag,
                Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final,
                ReturnType = new CodeTypeReference(typeof(bool))
            };

            var type = scanType;
            if (EnumerableClassNames.Contains(type.BaseType))
                type = scanType.TypeArguments[0];
            method.Parameters.Add(new CodeParameterDeclarationExpression(type, "value"));
            // Add attributes so it is not visible
            method.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(EditorBrowsableAttribute)),
                new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(EditorBrowsableState)), "Never"))));
            method.Statements.AddRange(fcd.ToCodeDomStatement(context));
            method.Comments.Add(new CodeCommentStatement(String.Format("<summary>Checks formal constraint '{0}'</summary>", fcd.Message), true));
            return method;
        }

        /// <summary>
        /// Render root namespace documentation
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Foo</description></item>
        /// </list></remarks>
        internal static CodeTypeDeclaration RenderNamespaceDoc(TemplateProjectDefinition project, String type)
        {

            CodeTypeDeclaration namespaceDoc = new CodeTypeDeclaration()
            {
                Name = "NamespaceDoc",
                IsClass = true,
                Attributes = MemberAttributes.Public
            };

            namespaceDoc.Comments.Add(new CodeCommentStatement(new CodeComment(String.Format("<summary>Sherpas Generated {2} for {0} version {1}</summary>", project.ProjectInfo.Name, project.ProjectInfo.Version, type), true)));
            namespaceDoc.Comments.Add(new CodeCommentStatement(new CodeComment("<remarks>", true)));
            namespaceDoc.Comments.Add(new CodeCommentStatement(new CodeComment("<para>Copyright:</para>", true)));
            foreach (var itm in project.ProjectInfo.Copyright)
                namespaceDoc.Comments.Add(new CodeCommentStatement(new CodeComment(itm.OuterXml, true)));
            namespaceDoc.Comments.Add(new CodeCommentStatement(new CodeComment("<para>Template Authors:</para><list type=\"bullet\">", true)));
            foreach (var aut in project.ProjectInfo.OriginalAuthor) 
                namespaceDoc.Comments.Add(new CodeCommentStatement(new CodeComment(String.Format("<item><description>{0}</description></item>" , aut), true)));
            namespaceDoc.Comments.Add(new CodeCommentStatement(new CodeComment("</list>", true)));
            namespaceDoc.Comments.Add(new CodeCommentStatement(new CodeComment("</remarks>", true)));

            return namespaceDoc;
        }

        /// <summary>
        /// Generates the extension class that initializes a formatter
        /// </summary>
        internal static CodeTypeDeclaration GenerateRegisterTemplatesMethod(TemplateProjectDefinition project, CodeNamespace nsCode)
        {
            // extension class
            CodeTypeDeclaration typeFormatterExtensions = new CodeTypeDeclaration()
            {
                Attributes = MemberAttributes.Public,
                Name = "SherpasUtilities",
                IsClass = true
            };
            typeFormatterExtensions.Comments.Add(new CodeCommentStatement(String.Format("<summary>Initializes formatter to handle templates from {0}</summary>", project.ProjectInfo.Name), true));
            typeFormatterExtensions.StartDirectives.Add(
                new CodeRegionDirective(CodeRegionMode.Start, "\nstatic"));
            typeFormatterExtensions.EndDirectives.Add(
                new CodeRegionDirective(CodeRegionMode.End, String.Empty));

            // Extension method for Initialize 
            CodeMemberMethod registerMethod = new CodeMemberMethod()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Static,
                Name = "RegisterTemplates",
                ReturnType = new CodeTypeReference(typeof(void))
            };

            var parmMe = new CodeParameterDeclarationExpression(String.Format("{0}", typeof(ClinicalDocumentFormatter).FullName), "me");
            registerMethod.Parameters.Add(parmMe);

            // Now register for the types
            foreach (var itm in project.Templates.OfType<ClassTemplateDefinition>().Where(o => o.Id != null && o.Id.Count > 0))
                registerMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("me"), "RegisterTemplate", new CodeTypeOfExpression(String.Format("{0}.{1}", nsCode.Name, itm.Name))));
            // Register helper classes
            foreach (var itm in project.Templates.OfType<ClassTemplateDefinition>().Where(o => o.Templates.Exists(t => t.Tag != null && t.Tag.StartsWith("ContFor:"))))
                registerMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("me"), "RegisterContainer", new CodeTypeOfExpression(String.Format("{0}.{1}", nsCode.Name, itm.Name))));

            typeFormatterExtensions.Members.Add(registerMethod);

            // For each template type generate an extension method on the base which does a AsX
            foreach (var tpl in project.Templates.OfType<ClassTemplateDefinition>())
            {
                // Extension method for Initialize 
                CodeMemberMethod asMethod = new CodeMemberMethod()
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Static,
                    Name = String.Format("As{0}", tpl.Name),
                    ReturnType = new CodeTypeReference(tpl.Name)
                };

                // This method
                asMethod.Parameters.Add(new CodeParameterDeclarationExpression(String.Format("this {0}", tpl.BaseClass.Type.FullName), "me"));
                asMethod.Statements.Add(new CodeMethodReturnStatement(new CodeObjectCreateExpression(tpl.Name, new CodeVariableReferenceExpression("me"))));
                typeFormatterExtensions.Members.Add(asMethod);
            }
            return typeFormatterExtensions;
        }
    }
}
