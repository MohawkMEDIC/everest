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
using MARC.Everest.Threading;
using System.Xml;
using MARC.Everest.Formatters.XML.ITS1;

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
            renderUnit.ReferencedAssemblies.Add(typeof(XmlIts1Formatter).Assembly.Location);
            renderUnit.ReferencedAssemblies.Add("System.dll");
            renderUnit.ReferencedAssemblies.Add("System.Linq.dll");
            renderUnit.ReferencedAssemblies.Add("System.Core.dll");
            renderUnit.ReferencedAssemblies.Add("System.Data.dll");
            renderUnit.ReferencedAssemblies.Add("System.Xml.dll");
            renderUnit.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            renderUnit.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            renderUnit.ReferencedAssemblies.Add("System.Data.dll");
            renderUnit.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            // Assembly info
            renderUnit.AssemblyCustomAttributes.AddRange(
                new CodeAttributeDeclaration[] {
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyVersionAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression("1.0.*"))),
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyInformationalVersionAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(project.ProjectInfo.Version))),
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyCopyrightAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(project.ProjectInfo.Copyright[0].InnerText))),
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyDescriptionAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(project.ProjectInfo.Name)))
                }
            );

            object synclock = new object();
            var nsRoot = new CodeNamespace(Path.GetFileNameWithoutExtension(outputFile));
            var nsVocab = new CodeNamespace(String.Format("{0}.Vocabulary", Path.GetFileNameWithoutExtension(outputFile)));

            nsRoot.Imports.Add(new CodeNamespaceImport("System.Linq"));
            nsVocab.Imports.Add(new CodeNamespaceImport("System.Linq"));
            CodeTypeDeclarationCollection collection = new CodeTypeDeclarationCollection();

            // Start rendering the project
            using (WaitThreadPool wtp = new WaitThreadPool())
            {
                foreach (var itm in project.Templates)
                {
                    RenderContext context = new RenderContext(itm, project, collection);

                    wtp.QueueUserWorkItem((o) =>
                    {


                        var renderer = (o as RenderContext).GetRenderer();
                        if (renderer == null)
                            Trace.TraceError("Cannot find a valid Renderer for template of type '{0}'", itm.GetType().Name);
                        else
                        {
                            foreach (CodeTypeDeclaration ctm in renderer.Render((o as RenderContext)))
                                lock (synclock)
                                {
                                    collection.Add(ctm);
                                    if (ctm.IsEnum)
                                        nsVocab.Types.Add(ctm);
                                    else
                                        nsRoot.Types.Add(ctm);
                                }
                        }
                    }, context);
                }

                wtp.WaitOne();
            }

            nsRoot.Types.Add(RenderUtils.RenderNamespaceDoc(project, "Classes"));
            nsVocab.Types.Add(RenderUtils.RenderNamespaceDoc(project, "Vocabulary"));
            nsRoot.Types.Add(RenderUtils.GenerateRegisterTemplatesMethod(project, nsRoot));
            renderUnit.Namespaces.Add(nsVocab);
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
                parameters.CompilerOptions = String.Format("/doc:{0}", Path.ChangeExtension(outputFile, "xml"));
                parameters.GenerateInMemory = false;
                parameters.IncludeDebugInformation = false;
                parameters.OutputAssembly = outputFile;
                parameters.TempFiles.KeepFiles = true;
                parameters.WarningLevel = 4;

                var result = csharpCodeProvider.CompileAssemblyFromDom(parameters, renderUnit);
                if (result.Errors.HasErrors)
                {
                    File.Copy(result.Errors[0].FileName, Path.ChangeExtension(outputFile, ".cs"), true);

                    foreach (CompilerError rs in result.Errors)
                            Console.WriteLine(rs.ToString());
                }


                Console.WriteLine("Verifying documentation links...");
                // Now post-process comments to clean them up
                XmlDocument xd = new XmlDocument();
                xd.Load(Path.ChangeExtension(outputFile, "xml"));
                var seeNodes = xd.SelectNodes("//see");
                List<String> nameCache = new List<string>();

                int i = 0;
                foreach (XmlNode xel in seeNodes)
                {
                    i++;
                    if(i % 10 == 0)
                        Console.Write("{0:##}% Complete ({1} of {2})", (i / (float)seeNodes.Count) * 100.0, i, seeNodes.Count);

                    Console.CursorLeft = 0;
                    var cref = xel.Attributes["cref"];
                    
                    // Clean up the reference
                    if (nameCache.Contains(cref.Value) || xd.SelectSingleNode(String.Format("//member[@name = '{0}']", cref.Value)) != null)
                    {
                        nameCache.Add(cref.Value);
                        continue; // already resolves
                    }
                    // Try to resolve
                    string newCref = cref.Value;
                    if (cref.Value.StartsWith("T:")) // type
                        newCref = String.Format("T:{0}.{1}", Path.GetFileNameWithoutExtension(outputFile), cref.Value.Substring(2));
                    else if (cref.Value.StartsWith("P:")) // property
                        newCref = String.Format("P:{0}.{1}", Path.GetFileNameWithoutExtension(outputFile), cref.Value.Substring(2));
                    if (nameCache.Contains(newCref) || xd.SelectSingleNode(String.Format("//member[@name = '{0}']", newCref)) != null)
                    {
                        nameCache.Add(newCref);
                        cref.Value = newCref;
                    }
                }
                xd.Save(Path.ChangeExtension(outputFile, "xml"));

                Console.WriteLine("Compile finished with {0} errors", result.Errors.OfType<CompilerError>().Count(o=>!o.IsWarning));
            }

        }
    }
}
