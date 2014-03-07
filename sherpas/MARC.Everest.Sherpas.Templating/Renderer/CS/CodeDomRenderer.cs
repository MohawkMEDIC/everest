using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Interface;
using System.CodeDom;
using System.IO;
using Microsoft.CSharp;
using MARC.Everest.DataTypes;
using System.Reflection;
using MARC.Everest.Attributes;
using System.Diagnostics;
using System.CodeDom.Compiler;

namespace MARC.Everest.Sherpas.Templating.Renderer.CS
{
    /// <summary>
    /// Represents a renderer that uses code dom
    /// </summary>
    public class CodeDomRenderer : ITemplateRenderer
    {
        /// <summary>
        /// Gets the name of the renderer
        /// </summary>
        public string Name
        {
            get { return "NET"; }
        }

        /// <summary>
        /// Render the project to the specified directory
        /// </summary>
        public void Render(Format.TemplateProjectDefinition project, string outputFile)
        {

            // Render unit
            CodeCompileUnit renderUnit = new CodeCompileUnit();
            renderUnit.ReferencedAssemblies.Add(AppDomain.CurrentDomain.GetAssemblies().First(a=>a.FullName == project.ProjectInfo.AssemblyRef).Location);
            renderUnit.ReferencedAssemblies.Add(typeof(II).Assembly.Location);
            renderUnit.ReferencedAssemblies.Add(typeof(TemplateAttribute).Assembly.Location);
            renderUnit.ReferencedAssemblies.Add("System.dll");

            // Assembly info
            renderUnit.AssemblyCustomAttributes.AddRange(
                new CodeAttributeDeclaration[] {
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyVersionAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression("1.0.*"))),
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyInformationalVersionAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(project.ProjectInfo.Version))),
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyCopyrightAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(project.ProjectInfo.Copyright[0].InnerText))),
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyDescriptionAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(project.ProjectInfo.Name)))
                }
            );


            var nsRoot = new CodeNamespace(Path.GetFileNameWithoutExtension(outputFile));
            // Start rendering the project
            foreach (var itm in project.Templates)
            {
                RenderContext context = new RenderContext(itm, project, nsRoot);
                var renderer = context.GetRenderer();
                if (renderer == null)
                    Trace.TraceError("Cannot find a valid Renderer for template of type '{0}'", itm.GetType().Name);
                else
                    foreach (var ctm in renderer.Render(context))
                        nsRoot.Types.Add(ctm as CodeTypeDeclaration);

            }

            renderUnit.Namespaces.Add(nsRoot);

            // Generate the C# file or DLL
            CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();
            if (Path.GetExtension(outputFile) == ".cs")
            {
                using (TextWriter tw = new StreamWriter(File.Create(outputFile)))
                {
                    csharpCodeProvider.GenerateCodeFromCompileUnit(renderUnit, tw, new System.CodeDom.Compiler.CodeGeneratorOptions()
                    {
                        BlankLinesBetweenMembers = true,
                        VerbatimOrder = true
                    });
                }
            }
            else if (Path.GetExtension(outputFile) == ".dll")
            {
                CompilerParameters parameters = new CompilerParameters();
                parameters.GenerateInMemory = false;
                parameters.IncludeDebugInformation = false;
                parameters.OutputAssembly = outputFile;
                parameters.TempFiles.KeepFiles = true;
                parameters.WarningLevel = 4;

                var result = csharpCodeProvider.CompileAssemblyFromDom(parameters, renderUnit);
                foreach (CompilerError rs in result.Errors)
                {
                    Trace.TraceError(rs.ToString());
                    if (File.Exists(rs.FileName))
                        File.Copy(rs.FileName, Path.ChangeExtension(outputFile, ".cs"), true);
                }
                Trace.TraceInformation("Compile finished with {0} errors", result.Errors.Count);
            }

        }
    }
}
