/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Interfaces;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Attributes;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Renderer;
using System.Diagnostics;
using MARC.Everest.Connectors;
using System.ComponentModel;
using Microsoft.Build.BuildEngine;
using MARC.Everest.Attributes;
using System.Xml;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS
{
    /// <summary>
    /// Rim based C# class generator
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Cs"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Rimba"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Renderer")]
    public class RimbaCsRenderer : MohawkCollege.EHR.gpmr.Pipeline.RendererBase
    {
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Vocab")]
        public static bool GenerateVocab = false;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static string RootClass = "RIM.InfrastructureRoot";
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static bool GenerateRim = false;

        /// <summary>
        /// When true, render  partials
        /// </summary>
        public static bool RenderPartials = false;
        /// <summary>
        /// Suppress documentation
        /// </summary>
        public static bool SuppressDoc = false;

        /// <summary>
        /// The maximum number of literals that can be rendered using this renderer
        /// </summary>
        public static int MaxLiterals = 100;

        /// <summary>
        /// Preferred rendering realm when there is a conflict
        /// </summary>
        public static string prefRealm = "UV";

        private string[][] helpText = new string[][] 
            { new string[] { "--rimbapi-root-class\t", "Sets the name of root class \r\n\t\t\t\taka the RIM.infrastructureRoot" } ,
                new string [] {"--rimbapi-target-ns\t", "Set the target namespace of the generated files" },
                new string[] {"--rimbapi-compile\t","Compile the resultant csproj file" },
                new string[] {"--rimbapi-dllonly\t", "Only output an assembly (clean\r\n\t\t\t\tcode files)" },
                new string[] {"--rimbapi-license\t","The license that should be appended to the\r\n\t\t\t\tgenerated files (BSD, MIT, or file)"}, 
                new string[] {"--rimbapi-org\t","The organization the generated code belongs to"},
                new string[] {"--rimbapi-gen-vocab\t", "Generate all vocabulary into C# classes"},
                new string[] {"--rimbapi-gen-rim\t","Generate classes in the RIM package"},
                new string[] {"--rimbapi-oid-profileid", "Set default OID of ProfileId"}, 
                new string[] {"--rimbapi-oid-interaction", "Set the default OID for InteractionID"}, 
                new string[] {"--rimbapi-oid-triggerevent", "Set the default OID for the TriggerEvent"}, 
                new string[] {"--rimbapi-profileid","Set the default ProfileId value"},
                new string[] {"--rimbapi-gen-its","Specifies a formatter assembly for which\r\n\t\t\t a type cache should be created."},
                new string[] {"--rimbapi-realm-pref", "When there is a conflict of names\r\n\t\t\tdetermines which 'realm' is preferred" },
                new string[] {"--rimbapi-max-literals", "Specifies the maximum size of an\r\n\t\t\tenumeration. Default is 100" },
                new string[] {"--rimbapi-partials", "When true, specifies that GPMR should\r\n\t\t\tenumerate partial vocabularies" },
                new string[] {"--rimbapi-suppress-doc", "Supresses documentation generation" },
                new string[] {"--rimbapi-phone","Generate Windows Phone project" }
            };

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get { return "RIM Based C# Generator"; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string Identifier
        {
            get { return "RIMBA_CS"; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override int ExecutionOrder
        {
            get { return 0; }
        }

        /// <summary>
        /// Execute the pipleine component
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToBoolean(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
        public override void Execute()
        {
            if (!(hostContext.Data["EnabledRenderers"] as StringCollection).Contains(this.Identifier)) return;

            // Get parameters
            Dictionary<String, StringCollection> parameters = hostContext.Data["CommandParameters"] as Dictionary<String, StringCollection>;
            System.Diagnostics.Trace.WriteLine("\r\nStarting RIMBA Renderer", "information");
            StringCollection genFormatters = new StringCollection();
            bool makeWP7Proj = false;

            if (hostContext.Mode == Pipeline.OperationModeType.Quirks)
                System.Diagnostics.Trace.WriteLine("--- WARNING ---\r\n Host context is operating in Quirks mode, GPMR cannot guarantee output will be accurate\r\n--- WARNING ---");

            #region Validate all parameters
            // Validate parameters
            if (!parameters.ContainsKey("rimbapi-api-ns"))
            {
                parameters.Add("rimbapi-api-ns", new StringCollection());
                parameters["rimbapi-api-ns"].Add("MARC.Everest");
            }
            if (!parameters.ContainsKey("rimbapi-target-ns"))
            {
                parameters.Add("rimbapi-target-ns", new StringCollection());
                parameters["rimbapi-target-ns"].Add("output");
            }
            if (parameters.ContainsKey("rimbapi-root-class"))
                RootClass = parameters["rimbapi-root-class"][0];
            if (parameters.ContainsKey("rimbapi-gen-vocab"))
                GenerateVocab = Convert.ToBoolean(parameters["rimbapi-gen-vocab"][0]);
            if(parameters.ContainsKey("rimbapi-gen-rim"))
                GenerateRim = Convert.ToBoolean(parameters["rimbapi-gen-rim"][0]);
            if (parameters.ContainsKey("rimbapi-profileid"))
                InteractionRenderer.profileId = parameters["rimbapi-profileid"][0];
            if (parameters.ContainsKey("rimbapi-oid-profileid"))
                InteractionRenderer.profileIdOid = parameters["rimbapi-oid-profileid"][0];
            if (parameters.ContainsKey("rimbapi-oid-interactionid"))
                InteractionRenderer.interactionIdOid = parameters["rimbapi-oid-interactionid"][0];
            if (parameters.ContainsKey("rimbapi-oid-triggerevent"))
                InteractionRenderer.triggerEventOid = parameters["rimbapi-oid-triggerevent"][0];
            if (parameters.ContainsKey("rimbapi-gen-its"))
                genFormatters = parameters["rimbapi-gen-its"];
            if (parameters.ContainsKey("rimbapi-phone"))
                makeWP7Proj = bool.Parse(parameters["rimbapi-phone"][0]);

            #endregion

            // Initialize Heuristics
            MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.HeuristicEngine.Datatypes.Initialize(parameters["rimbapi-api-ns"][0]);
            MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.HeuristicEngine.Interfaces.Initialize(parameters["rimbapi-api-ns"][0]);

            // Get our repository ready
            ClassRepository classRep = hostContext.Data["SourceCR"] as ClassRepository;

            string ProjectFileName = "output.csproj"; // Set the output file name
            if (parameters.ContainsKey("rimbapi-target-ns"))
                ProjectFileName = parameters["rimbapi-target-ns"][0];
            if(parameters.ContainsKey("rimbapi-partials"))
                RenderPartials = Boolean.Parse(parameters["rimbapi-partials"][0]);
            if (parameters.ContainsKey("rimbapi-realm-pref"))
                prefRealm = parameters["rimbapi-realm-pref"][0];
            if (parameters.ContainsKey("rimbapi-max-literals"))
                MaxLiterals = Int32.Parse(parameters["rimbapi-max-literals"][0]);
            if (parameters.ContainsKey("rimbapi-suppress-doc"))
                SuppressDoc = Boolean.Parse(parameters["rimbapi-suppress-doc"][0]);
            // Setup the template parameters
            string[][] templateFields = new string[][] 
            {
                new string[] { "$license$", parameters.ContainsKey("rimbapi-license") ? Licenses.ResourceManager.GetString(parameters["rimbapi-license"][0].ToUpper()) : "" },
                new string[] { "$org$", parameters.ContainsKey("rimbapi-org") ? parameters["rimbapi-org"][0] : "" },
                new string[] { "$date$", DateTime.Now.ToString("yyyy-MM-dd") },
                new string[] { "$clrversion$", Environment.Version.ToString() },
                new string[] { "$time$", DateTime.Now.ToString("HH:mm:ss") },
                new string[] { "$author$", SystemInformation.UserName },
                new string[] { "$year$", DateTime.Now.Year.ToString() },
                new string[] { "$version$", Assembly.GetEntryAssembly().GetName().Version.ToString() },
                new string[] { "$guid$", Guid.NewGuid().ToString() }, 
                new string[] { "$name$", ProjectFileName },
                new string[] { "$mrversion$", InteractionRenderer.profileId ?? "" }
            };

            // Now we want to scan our assembly for FeatureRenderers
            List<KeyValuePair<FeatureRendererAttribute, IFeatureRenderer>> renderers = new List<KeyValuePair<FeatureRendererAttribute, IFeatureRenderer>>();
            foreach(Type t in this.GetType().Assembly.GetTypes())
                if (t.GetInterface("MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Interfaces.IFeatureRenderer") != null &&
                    t.GetCustomAttributes(typeof(FeatureRendererAttribute), true).Length > 0)
                {
                    foreach(FeatureRendererAttribute feature in (t.GetCustomAttributes(typeof(FeatureRendererAttribute), true)))
                    {
                            // Only one feature renderer per feature, so if the dictionary throws an exception
                            // on the add it is ok
                            renderers.Add(new KeyValuePair<FeatureRendererAttribute,IFeatureRenderer>(feature, (IFeatureRenderer)t.Assembly.CreateInstance(t.FullName)));
                    }
                }

            #region Setup the project
            // Create engine reference
            Microsoft.Build.BuildEngine.Engine engine = new Microsoft.Build.BuildEngine.Engine(
                Path.Combine(Path.Combine(Path.Combine(System.Environment.SystemDirectory, "..\\Microsoft.NET"), "Framework"), "v3.5")),
                phoneEngine = new Microsoft.Build.BuildEngine.Engine(
                Path.Combine(Path.Combine(Path.Combine(System.Environment.SystemDirectory, "..\\Microsoft.NET"), "Framework"), "v4.0.30319"));
            
            // Create MSPROJ
            Microsoft.Build.BuildEngine.Project project = new Microsoft.Build.BuildEngine.Project(engine),
                phoneProj = new Project(phoneEngine, "4.0");

            
            phoneProj.DefaultTargets = project.DefaultTargets = "Build";
            

            // Setup project attributes
            Microsoft.Build.BuildEngine.BuildPropertyGroup pg = project.AddNewPropertyGroup(false);

            Microsoft.Build.BuildEngine.BuildProperty property = pg.AddNewProperty("Configuration", "Release");

            property.Condition = "'$(Configuration)' == ''";
            property = pg.AddNewProperty("Platform", "AnyCPU");
            property.Condition = "'$(Platform)' == ''";
            pg.AddNewProperty("ProductVersion", "10.0.20506");
            pg.AddNewProperty("SchemaVersion", "2.0");
            pg.AddNewProperty("ProjectGuid", Guid.NewGuid().ToString());
            pg.AddNewProperty("OutputType", "Library");
            pg.AddNewProperty("AppDesignerFolder", "Properties");
            pg.AddNewProperty("RootNamespace", parameters["rimbapi-target-ns"][0]);
            pg.AddNewProperty("AssemblyName", parameters["rimbapi-target-ns"][0]);
            
            // Release AnyCPU
            pg = project.AddNewPropertyGroup(false);
            pg.Condition = "'$(Configuration)|$(Platform)' == 'Release|AnyCPU'";
            pg.AddNewProperty("DebugType", "pdbonly");
            pg.AddNewProperty("Optimize", "true");
            pg.AddNewProperty("OutputPath", "bin\\release");
            pg.AddNewProperty("DefineConstants", "TRACE");
            pg.AddNewProperty("ErrorReport", "prompt");
            pg.AddNewProperty("WarningLevel", "4");
            pg.AddNewProperty("DocumentationFile", "bin\\release\\" + parameters["rimbapi-target-ns"][0] + ".xml");

            // Create Dir Structure
            Directory.CreateDirectory(Path.Combine(hostContext.Output, "bin"));
            Directory.CreateDirectory(Path.Combine(hostContext.Output, "lib"));
            Directory.CreateDirectory(Path.Combine(hostContext.Output, "Properties"));
            Directory.CreateDirectory(Path.Combine(hostContext.Output, "Vocabulary"));
            Directory.CreateDirectory(Path.Combine(hostContext.Output, "Interaction"));

            // Add reference structure
            Microsoft.Build.BuildEngine.BuildItemGroup refItemGroup = project.AddNewItemGroup();

            // Add References
            File.Copy(Path.Combine(System.Windows.Forms.Application.StartupPath, "MARC.Everest.dll"), Path.Combine(Path.Combine(hostContext.Output, "lib"), "MARC.Everest.dll"), true);

            if(makeWP7Proj)
                File.Copy(Path.Combine(Path.Combine(System.Windows.Forms.Application.StartupPath, "lib"), "MARC.Everest.Phone.dll"), Path.Combine(Path.Combine(hostContext.Output, "lib"), "MARC.Everest.Phone.dll"), true);

            File.Copy(Path.Combine(System.Windows.Forms.Application.StartupPath, "MARC.Everest.xml"), Path.Combine(Path.Combine(hostContext.Output, "lib"), "MARC.Everest.xml"), true);
            refItemGroup.AddNewItem("Reference", "System");
            refItemGroup.AddNewItem("Reference", "System.Drawing");
            refItemGroup.AddNewItem("Reference", "System.Xml");
            Microsoft.Build.BuildEngine.BuildItem buildItem = refItemGroup.AddNewItem("Reference", @"MARC.Everest");
            buildItem.SetMetadata("SpecificVersion", "false");
            buildItem.SetMetadata("HintPath", "lib\\MARC.Everest.dll");

            project.AddNewImport("$(MSBuildBinPath)\\Microsoft.CSharp.targets", null);

            Microsoft.Build.BuildEngine.BuildItemGroup fileItemGroup = project.AddNewItemGroup(),
                phoneFileItemGroup = phoneProj.AddNewItemGroup();

            #region Assembly Info
            try
            {
                TextWriter tw = File.CreateText(Path.Combine(Path.Combine(hostContext.Output, "Properties"), "AssemblyInfo.cs"));
                try
                {
                    string Header = Template.AssemblyInfo; // Set the header to the default

                    // Populate template fields
                    foreach (String[] st in templateFields)
                        Header = Header.Replace(st[0], st[1]);

                    // Write header
                    tw.Write(Header);
                }
                finally
                {
                    tw.Close();
                }
                fileItemGroup.AddNewItem("Compile", Path.Combine("Properties", "AssemblyInfo.cs"));
                phoneFileItemGroup.AddNewItem("Compile", Path.Combine("Properties", "AssemblyInfo.cs"));
            }
            catch(Exception)
            {
                System.Diagnostics.Trace.WriteLine("Couldn't generate the AssemblyInfo.cs file for this project", "warn");
            }
            #endregion
            #endregion

            #region Code Create
            // Convert class rep to list
            List<Feature> features = new List<Feature>();
            foreach (KeyValuePair<String, Feature> kv in classRep)
                features.Add(kv.Value);
            // Sort so classes are processed first
            features.Sort(delegate(Feature a, Feature b)
            {
                if ((a is SubSystem) && !(b is SubSystem)) return -1;
                else if ((b is SubSystem) && !(a is SubSystem)) return 1;
                else return a.GetType().Name.CompareTo(b.GetType().Name);
            });

            RenderFeatureList(features, templateFields, renderers, fileItemGroup, phoneFileItemGroup, parameters);

            // Any added features?
            // HACK: This should be fixed soon, but meh... I'll get around to it
            List<Feature> addlFeatures = new List<Feature>();
            foreach (KeyValuePair<String, Feature> kv in classRep)
                if(!features.Contains(kv.Value))
                    addlFeatures.Add(kv.Value);
            RenderFeatureList(addlFeatures, templateFields, renderers, fileItemGroup, phoneFileItemGroup, parameters);

            // Save the project
            project.Save(Path.Combine(hostContext.Output, ProjectFileName) + ".csproj");
            #endregion

            // Compile?
            #region Compile this project

            // Does the user want to compile?
            if (parameters.ContainsKey("rimbapi-compile") && Convert.ToBoolean(parameters["rimbapi-compile"][0]))
            {
                string logPath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName()); // Create log
                Microsoft.Build.BuildEngine.FileLogger logger = new Microsoft.Build.BuildEngine.FileLogger();
                logger.Parameters = "logfile=" + logPath;
                engine.RegisterLogger(logger);

                System.Diagnostics.Trace.Write(String.Format("Compiling project (Build log {0})...", logPath), "information");

                // Compile
                if (engine.BuildProject(project))
                    System.Diagnostics.Trace.WriteLine("Success!", "information");
                else
                {
                    System.Diagnostics.Trace.WriteLine("Fail", "information");
                    throw new InvalidOperationException("Failed compilation, operation cannot continue");

                }
                engine.UnregisterAllLoggers();
                
            }
            #endregion

            #region Windows Phone

            if (makeWP7Proj)
            {

                // Setup project attributes
                pg = phoneProj.AddNewPropertyGroup(false);
                property = pg.AddNewProperty("Configuration", "Release");
                property.Condition = "'$(Configuration)' == ''";
                property = pg.AddNewProperty("Platform", "AnyCPU");
                property.Condition = "'$(Platform)' == ''";
                pg.AddNewProperty("ProductVersion", "10.0.20506");
                pg.AddNewProperty("SchemaVersion", "2.0");
                pg.AddNewProperty("ProjectGuid", Guid.NewGuid().ToString());
                pg.AddNewProperty("OutputType", "Library");
                pg.AddNewProperty("AppDesignerFolder", "Properties");
                pg.AddNewProperty("RootNamespace", parameters["rimbapi-target-ns"][0]);
                pg.AddNewProperty("AssemblyName", parameters["rimbapi-target-ns"][0] + ".Phone");
                pg.AddNewProperty("ProjectTypeGuids", "{C089C8C0-30E0-4E22-80C0-CE093F111A43};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}");
                pg.AddNewProperty("TargetFrameworkVersion", "v4.0");
                pg.AddNewProperty("SilverlightVersion", "$(TargetFrameworkVersion)");
                pg.AddNewProperty("TargetFrameworkProfile", "WindowsPhone71");
                pg.AddNewProperty("TargetFrameworkIdentifier", "Silverlight");
                pg.AddNewProperty("SilverlightApplication", "false");
                pg.AddNewProperty("ValidateXaml", "true");
                pg.AddNewProperty("ThrowErrorsInValidation", "true");

                // Release AnyCPU
                pg = phoneProj.AddNewPropertyGroup(false);
                pg.Condition = "'$(Configuration)|$(Platform)' == 'Release|AnyCPU'";
                pg.AddNewProperty("DebugType", "pdbonly");
                pg.AddNewProperty("Optimize", "true");
                pg.AddNewProperty("OutputPath", "bin\\release");
                pg.AddNewProperty("DefineConstants", "TRACE;SILVERLIGHT;WINDOWS_PHONE");
                pg.AddNewProperty("ErrorReport", "prompt");
                pg.AddNewProperty("NoStdLib", "true");
                pg.AddNewProperty("NoConfig", "true");
                pg.AddNewProperty("WarningLevel", "4");
                pg.AddNewProperty("DocumentationFile", "bin\\release\\" + parameters["rimbapi-target-ns"][0] + ".Phone.xml");

                // Add reference structure
                refItemGroup = phoneProj.AddNewItemGroup();
                refItemGroup.AddNewItem("Reference", "System");
                refItemGroup.AddNewItem("Reference", "System.Xml");

                BuildItem evReference = refItemGroup.AddNewItem("Reference", @"MARC.Everest.Phone");
                evReference.SetMetadata("SpecificVersion", "false");
                evReference.SetMetadata("HintPath", "lib\\MARC.Everest.Phone.dll");

                // Add WP7 Imports
                phoneProj.AddNewImport(@"$(MSBuildExtensionsPath)\Microsoft\Silverlight for Phone\$(TargetFrameworkVersion)\Microsoft.Silverlight.$(TargetFrameworkProfile).Overrides.targets", null);
                phoneProj.AddNewImport(@"$(MSBuildExtensionsPath)\Microsoft\Silverlight for Phone\$(TargetFrameworkVersion)\Microsoft.Silverlight.CSharp.targets", null);

                // HACK: Add tools version
                string fileName = Path.Combine(hostContext.Output, ProjectFileName) + ".Phone.csproj";
                phoneProj.Save(fileName);
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                doc.DocumentElement.Attributes.Append(doc.CreateAttribute("ToolsVersion"));
                doc.DocumentElement.Attributes["ToolsVersion"].Value = "4.0";
                doc.Save(fileName);

                if (parameters.ContainsKey("rimbapi-compile") && Convert.ToBoolean(parameters["rimbapi-compile"][0]))
                {
                    System.Diagnostics.Trace.Write(String.Format("Compiling phone project..."), "information");

                    // Compile
                    if (phoneEngine.BuildProjectFile(fileName))
                        System.Diagnostics.Trace.WriteLine("Success!", "information");
                    else
                    {
                        System.Diagnostics.Trace.WriteLine("Fail", "information");
                        throw new InvalidOperationException("Failed compilation, operation cannot continue");

                    }
                }
            }

            #endregion

            #region Generate Formatter Assemblies

            // Generate the formatter assemblies
            if (genFormatters.Count > 0 && parameters.ContainsKey("rimbapi-compile") && Convert.ToBoolean(parameters["rimbapi-compile"][0]))
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                Trace.WriteLine("Generating ITS Formatter Types:", "information");

                // Load the assembly
                Assembly genAsm = Assembly.LoadFile(Path.Combine(Path.Combine(Path.Combine(hostContext.Output, "bin"), "release"), ProjectFileName + ".dll"));
                foreach (string s in genFormatters)
                    GenerateFormatterAssembly(s, genAsm, InteractionRenderer.profileId ?? "formatter");
                
                // Assembly resolve
                AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            }
            else if (genFormatters.Count > 0)
                Trace.WriteLine("Can't use --rimbapi-gen-its when --rimbapi-compile is not true, skipping ITS generation", "warn");
            #endregion

            // Does the user only want asm?
            #region dllonly
            if (parameters.ContainsKey("rimbapi-dllonly") && parameters.ContainsKey("rimbapi-compile") && Convert.ToBoolean(parameters["rimbapi-dllonly"][0]))
                try
                {
                    // Move the assemblies up to root
                    foreach (string file in Directory.GetFiles(Path.Combine(Path.Combine(hostContext.Output, "bin"), "release")))
                    {
                        if (File.Exists(Path.Combine(hostContext.Output, Path.GetFileName(file))))
                            File.Delete(Path.Combine(hostContext.Output, Path.GetFileName(file)));
                        File.Move(file, Path.Combine(hostContext.Output, Path.GetFileName(file)));
                    }

                    // Clean all in the projects and remove all directories
                    List<String> directories = new List<string>(new string[] {
                        Path.Combine(Path.Combine(hostContext.Output, "bin"), "release"), 
                        Path.Combine(hostContext.Output, "bin"), 
                        Path.Combine(hostContext.Output, "lib"),
                        Path.Combine(hostContext.Output, "Vocabulary"), 
                        Path.Combine(hostContext.Output, "Interaction"),
                        Path.Combine(hostContext.Output, "obj")
                    });

                    // Gather files and clean
                    foreach (Microsoft.Build.BuildEngine.BuildItem fi in fileItemGroup)
                    {
                        // Add directory on the "to be deleted"
                        string dirName = Path.GetDirectoryName(Path.Combine(hostContext.Output, fi.Include));
                        if (!directories.Contains(dirName))
                            directories.Add(dirName);

                        Trace.WriteLine(String.Format("Deleting {0}...", fi.Include), "debug");
                        File.Delete(Path.Combine(hostContext.Output, fi.Include));
                    }
                    // Clean dirs
                    foreach (string s in directories)
                        Directory.Delete(s, true);
                    File.Delete(project.FullFileName);
                }
                catch(Exception)
                {
                    System.Diagnostics.Trace.WriteLine("Could not clean working files!", "warn");
                }
            #endregion

        }

        /// <summary>
        /// Render feature list
        /// </summary>
        private void RenderFeatureList(List<Feature> features, string[][] templateFields, List<KeyValuePair<FeatureRendererAttribute, IFeatureRenderer>> renderers, BuildItemGroup fileItemGroup, BuildItemGroup mobileItemGroup, Dictionary<String, StringCollection> parameters)
        {
            // Scan the class repo and start processing
            foreach (Feature f in features)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Rendering C# for '{0}'...", f.Name), "debug");

                // Is there a renderer for this feature
                KeyValuePair<FeatureRendererAttribute, IFeatureRenderer> fr = renderers.Find(o => o.Key.Feature == f.GetType());

                // Was a renderer found?
                if (fr.Key == null)
                    System.Diagnostics.Trace.WriteLine(String.Format("can't find renderer for {0}", f.GetType().Name), "warn");
                else
                {
                    string file = String.Empty;
                    try // To write the file
                    {
                        // Start the rendering
                        file = fr.Value.CreateFile(f, hostContext.Output);

                        // Is the renderer for a file
                        if (fr.Key.IsFile)
                        {

                            TextWriter tw = File.CreateText(file);
                            try // Render the file
                            {

                                string Header = Template.Default; // Set the header to the default

                                // Populate template fields
                                foreach (String[] st in templateFields)
                                    Header = Header.Replace(st[0], st[1]);

                                // Write header
                                tw.Write(Header);

                                // Render the template out
                                fr.Value.Render(parameters["rimbapi-target-ns"][0], parameters["rimbapi-api-ns"][0], f, tw);
                            }
                            finally
                            {
                                tw.Close();
                            }

                            // Add to the files in the project
                            string projName = file.Replace(hostContext.Output, "");
                            if (projName.StartsWith(Path.DirectorySeparatorChar.ToString()))
                                projName = projName.Substring(1);
                            if (Array.Find<BuildItem>(fileItemGroup.ToArray(), itm => itm.Include.Equals(projName)) == null)
                            {
                                fileItemGroup.AddNewItem("Compile", projName);
                                mobileItemGroup.AddNewItem("Compile", projName);
                            }
                            else
                                Trace.WriteLine(String.Format("Class '{0}' is defined more than once, second include is ignored", projName), "warn");

                        }
                    }
                    catch (NotSupportedException)
                    {
                        if (!String.IsNullOrEmpty(file)) File.Delete(file);
                    }
                    catch (Exception e)
                    {
                        if (!String.IsNullOrEmpty(file)) File.Delete(file);
                        System.Diagnostics.Trace.WriteLine(e.Message, "error");
                    }
                }
            }
        }

        /// <summary>
        /// Assembly resolution
        /// </summary>
        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                if (args.Name == asm.FullName)
                    return asm;

            /// Try for an non-same number Version
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                if (args.Name.Substring(0, args.Name.IndexOf(",")) == asm.GetName().Name)
                    return asm;

            return null;
        }

        /// <summary>
        /// Generate a formatter assembly
        /// </summary>
        private void GenerateFormatterAssembly(string itsDllFile, Assembly genAsm, string profileId)
        {
            // Attempt to load the its DLL file
            try
            {
                Assembly asm = Assembly.LoadFile(itsDllFile);

                // Now find the formatter
                Type formatterType = Array.Find<Type>(asm.GetTypes(), t => t.GetInterface("MARC.Everest.Connectors.ICodeDomStructureFormatter") != null);
                if(formatterType == null)
                    throw new InvalidOperationException(String.Format("Could not find a formatter in the specified assembly that implmenets ICodeDomStructureFormatter"));

                // Get the description for the formatter
                string formatterDescription = formatterType.FullName;
                object[] descriptionAttributes = formatterType.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptionAttributes.Length > 0)
                    formatterDescription = (descriptionAttributes[0] as DescriptionAttribute).Description;

                // Create the formatter type
                ConstructorInfo ci = formatterType.GetConstructor(Type.EmptyTypes);
                if (ci == null)
                    throw new InvalidOperationException(string.Format("The formatter '{0}' must provide a parameterless constructor", formatterType.FullName));
                
                // Create the code dom structure formatter
                ICodeDomStructureFormatter cdsf = ci.Invoke(null) as ICodeDomStructureFormatter;
                cdsf.GenerateInMemory = false;
                
                // Build the types
                Trace.Write(formatterDescription, "information");
                //var interactionTypes = Array.FindAll<Type>(genAsm.GetTypes(), o => o.GetInterface("MARC.Everest.Interfaces.IInteraction") != null);
                //var entryTypes = Array.FindAll<Type>(genAsm.GetTypes(), o => o.GetCustomAttributes(typeof(StructureAttribute), false).Length > 0 && (o.GetCustomAttributes(typeof(StructureAttribute), false)[0] as StructureAttribute).IsEntryPoint);


                cdsf.BuildCache(Array.FindAll<Type>(genAsm.GetTypes(), o=>o.IsClass && !o.IsAbstract && o.GetCustomAttributes(typeof(StructureAttribute), false).Length > 0));
                Trace.WriteLine(".. ok", "information");
                
                // Now copy the assemblies
                foreach (Assembly fmtrAsm in cdsf.GeneratedAssemblies)
                {
                    string destFile = String.Format("{0}.dll", Path.Combine(Path.GetDirectoryName(genAsm.Location), Path.GetFileNameWithoutExtension(itsDllFile) + "." + profileId));
                    Trace.WriteLine(String.Format("\tCopying '{0}' to '{1}'", fmtrAsm.Location, destFile), "debug");
                    
                    File.Copy(fmtrAsm.Location, destFile, true);
                }
                
            }
            catch (Exception e)
            {
                Trace.WriteLine(String.Format("Couldn't generate formatters for '{0}', error was:\r\n{1}", itsDllFile, e), "error");
            }
        }

        /// <summary>
        /// Display help
        /// </summary>
        public override string Help
        {
            get 
            {

                StringBuilder helpString = new StringBuilder();
                foreach(String[] helpData in helpText)
                    helpString.AppendFormat("{0}\t{1}\r\n", helpData[0], helpData[1]);

                helpString.Append("\r\nExample:\r\ngpmr -v 7 -s mif/*.mif -r RIMBA_CS -o .\\output --rimbapi-target-ns=MARC.Everest.CMETS\r\n");
                return helpString.ToString();
            }
        }
    }
}
