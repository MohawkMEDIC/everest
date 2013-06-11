using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using MohawkCollege.EHR.gpmr.COR;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Attributes;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Util;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Interfaces;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java
{
    /// <summary>
    /// Java renderer
    /// </summary>
    public class RimbaJavaRenderer : RendererBase
    {

        /// <summary>
        /// Help text
        /// </summary>
        private string[][] helpText = new string[][] 
            { new string[] { "--rimbapi-root-class\t", "Sets the name of root class \r\n\t\t\t\taka the RIM.infrastructureRoot" } ,
                new string [] {"--rimbapi-target-ns\t", "Set the target namespace of the generated files" },
                new string[] {"--rimbapi-compile\t","Compile the resultant java files" },
                new string[] {"--rimbapi-jaronly\t", "Only output a jar file (clean\r\n\t\t\t\tcode files)" },
                new string[] {"--rimbapi-jdoc\t", "True if javadoc should be generated" },
                new string[] {"--rimbapi-license\t","The license that should be appended to the\r\n\t\t\t\tgenerated files (BSD, MIT, or file)"}, 
                new string[] {"--rimbapi-org\t","The organization the generated code belongs to"},
                new string[] {"--rimbapi-gen-vocab\t", "Generate all vocabulary into Java enumerations"},
                new string[] {"--rimbapi-gen-rim\t","Generate classes in the RIM package"},
                new string[] {"--rimbapi-oid-profileid", "Set default OID of ProfileId"}, 
                new string[] {"--rimbapi-oid-interaction", "Set the default OID for InteractionID"}, 
                new string[] {"--rimbapi-oid-triggerevent", "Set the default OID for the TriggerEvent"}, 
                new string[] {"--rimbapi-profileid","Set the default ProfileId value"},
                new string[] {"--rimbapi-realm-pref", "When there is a conflict of names\r\n\t\t\tdetermines which 'realm' is preferred" },
                new string[] {"--rimbapi-max-literals", "Specifies the maximum size of an\r\n\t\t\tenumeration. Default is 100" },
                new string[] {"--rimbapi-jdk", "Specifies the path to the JDK\r\n\t\t\twith which to compile the project"},
                new string[] {"--rimbapi-partials", "When true, specifies that GPMR should\r\n\t\t\tenumerate partial vocabularies" },
                new string[] {"--rimbapi-suppress-doc", "Supresses documentation generation" },
                new string[] {"--rimbapi-jopt", "Pass the specified parameter to the java tools" },
                new string[] {"--rimbapi-maven", "Generate a maven directory structure" }

            };

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        internal static bool GenerateVocab = false;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        internal static string RootClass = "rim.InfrastructureRoot";
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        internal static bool GenerateRim = false;
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
        internal static int MaxLiterals = 100;

        /// <summary>
        /// Preferred rendering realm when there is a conflict
        /// </summary>
        internal static string prefRealm = "UV";

        /// <summary>
        /// Path to the java cc program
        /// </summary>
        private string m_javaccpath = Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.Machine);


        /// <summary>
        /// Gets the name of the renderer
        /// </summary>
        public override string Name
        {
            get { return "RIM Based Java Renderer"; }
        }

        /// <summary>
        /// Get the identifier of the java
        /// </summary>
        public override string Identifier
        {
            get { return "RIMBA_JA"; }
        }

        /// <summary>
        /// Execution order
        /// </summary>
        public override int ExecutionOrder
        {
            get { return 0; }
        }

        /// <summary>
        /// Execute the pipeline
        /// </summary>
        public override void Execute()
        {
            if (!(hostContext.Data["EnabledRenderers"] as StringCollection).Contains(this.Identifier)) return;

            
            // Get parameters
            Dictionary<String, StringCollection> parameters = hostContext.Data["CommandParameters"] as Dictionary<String, StringCollection>;
            System.Diagnostics.Trace.WriteLine("\r\nStarting RIMBA Renderer", "information");
            StringCollection genFormatters = new StringCollection();

            if (hostContext.Mode == Pipeline.OperationModeType.Quirks)
                System.Diagnostics.Trace.WriteLine("--- WARNING ---\r\n Host context is operating in Quirks mode, GPMR cannot guarantee output will be accurate\r\n--- WARNING ---");

            bool generateMaven = false;

            #region Validate all parameters
            // Validate parameters
            if (!parameters.ContainsKey("rimbapi-api-ns"))
            {
                parameters.Add("rimbapi-api-ns", new StringCollection());
                parameters["rimbapi-api-ns"].Add("org.marc.everest");
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
            if (parameters.ContainsKey("rimbapi-gen-rim"))
                GenerateRim = Convert.ToBoolean(parameters["rimbapi-gen-rim"][0]);
            if (parameters.ContainsKey("rimbapi-profileid"))
                InteractionRenderer.profileId = parameters["rimbapi-profileid"][0];
            if (parameters.ContainsKey("rimbapi-oid-profileid"))
                InteractionRenderer.profileIdOid = parameters["rimbapi-oid-profileid"][0];
            if (parameters.ContainsKey("rimbapi-oid-interactionid"))
                InteractionRenderer.interactionIdOid = parameters["rimbapi-oid-interactionid"][0];
            if (parameters.ContainsKey("rimbapi-oid-triggerevent"))
                InteractionRenderer.triggerEventOid = parameters["rimbapi-oid-triggerevent"][0];
            if (parameters.ContainsKey("rimbapi-suppress-doc"))
                SuppressDoc = Boolean.Parse(parameters["rimbapi-suppress-doc"][0]);
            if (parameters.ContainsKey("rimbapi-gen-its"))
                genFormatters = parameters["rimbapi-gen-its"];
            if (parameters.ContainsKey("rimbapi-jdk"))
                m_javaccpath = parameters["rimbapi-jdk"][0];
            if (parameters.ContainsKey("rimbapi-partials"))
                RenderPartials = Boolean.Parse(parameters["rimbapi-partials"][0]);
            if (parameters.ContainsKey("rimbapi-maven"))
                generateMaven = Boolean.Parse(parameters["rimbapi-maven"][0]);

            if (string.IsNullOrEmpty(m_javaccpath))
                throw new ArgumentException("Cannot find JDK, specify location in JAVA_HOME or with --rimbapi-jdk", "rimbapi-jdk");

            #endregion

            // Validate the jdk path
            // TODO: Find a better way of doing this
            String javacPath = Util.JabaUtils.GetJavaTool(m_javaccpath, "javac"),
                jarPath = Util.JabaUtils.GetJavaTool(m_javaccpath, "jar"),
                javaDocPath = Util.JabaUtils.GetJavaTool(m_javaccpath, "javadoc");
            if (string.IsNullOrEmpty(javacPath) || string.IsNullOrEmpty(jarPath))
                throw new InvalidOperationException("Could not find javac or jar on the path specified by --rimbapi-jdk parameter");

            string heuristicFile = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "data"), "JavaHeuristicData.xml");
            // Initialize Heuristics
            MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.HeuristicEngine.Datatypes.Initialize(parameters["rimbapi-api-ns"][0], heuristicFile);
            MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.HeuristicEngine.Interfaces.Initialize(parameters["rimbapi-api-ns"][0], heuristicFile);

            // Get our repository ready
            ClassRepository classRep = hostContext.Data["SourceCR"] as ClassRepository;
            string projectName = "output";
            if (parameters.ContainsKey("rimbapi-target-ns"))
                projectName = parameters["rimbapi-target-ns"][0];
            if (parameters.ContainsKey("rimbapi-realm-pref"))
                prefRealm = parameters["rimbapi-realm-pref"][0];
            if (parameters.ContainsKey("rimbapi-max-literals"))
                MaxLiterals = Int32.Parse(parameters["rimbapi-max-literals"][0]);

            RootClass = string.Format("{0}.{1}", projectName, RootClass);


            #region Scan for feature renderlets
            // Now we want to scan our assembly for FeatureRenderers
            List<KeyValuePair<FeatureRendererAttribute, IFeatureRenderer>> renderers = new List<KeyValuePair<FeatureRendererAttribute, IFeatureRenderer>>();
            foreach (Type t in this.GetType().Assembly.GetTypes())
                if (t.GetInterface("MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Interfaces.IFeatureRenderer") != null &&
                    t.GetCustomAttributes(typeof(FeatureRendererAttribute), true).Length > 0)
                {
                    foreach (FeatureRendererAttribute feature in (t.GetCustomAttributes(typeof(FeatureRendererAttribute), true)))
                    {
                        // Only one feature renderer per feature, so if the dictionary throws an exception
                        // on the add it is ok
                        renderers.Add(new KeyValuePair<FeatureRendererAttribute, IFeatureRenderer>(feature, (IFeatureRenderer)t.Assembly.CreateInstance(t.FullName)));
                    }
                }
            #endregion

            #region Create Project Structure

            string sourcePath = Path.Combine(hostContext.Output, "src");

            // Copy JAR to output directory
            string jarFile = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "lib"), "org.marc.everest.jar");
            if (!File.Exists(jarFile))
                throw new FileNotFoundException("Cannot find the Everest JAR file");
            jarFile = Path.Combine(Path.Combine(hostContext.Output, "lib"), Path.GetFileName(jarFile));

            // Core directories
            if (generateMaven)
            {
                sourcePath = Path.Combine(Path.Combine(Path.Combine(hostContext.Output, "src"), "main"), "java");
                Directory.CreateDirectory(Path.Combine(hostContext.Output, "src"));
                Directory.CreateDirectory(Path.Combine(Path.Combine(hostContext.Output, "src"), "main"));
                Directory.CreateDirectory(sourcePath);
                Directory.CreateDirectory(Path.Combine(hostContext.Output, "target"));
                Directory.CreateDirectory(Path.Combine(hostContext.Output, ".settings"));
            }
            else
            {
                Directory.CreateDirectory(sourcePath);
                Directory.CreateDirectory(Path.Combine(hostContext.Output, "bin"));
                Directory.CreateDirectory(Path.Combine(hostContext.Output, "lib"));
                Directory.CreateDirectory(Path.Combine(hostContext.Output, "doc"));
                Directory.CreateDirectory(Path.Combine(hostContext.Output, ".settings"));

                // Copy jar file to output directory
                File.Copy(jarFile, Path.Combine(Path.Combine(hostContext.Output, "lib"), Path.GetFileName(jarFile)), true);
                
            }

            // Create directory structure
            string[] subPackages = { projectName, 
                                       String.Format("{0}.{1}", projectName, "interaction"), 
                                       String.Format("{0}.{1}", projectName, "vocabulary"),
                                       String.Format("{0}.{1}", projectName, "rim")
                                   };
            foreach (var subPkg in subPackages)
                Directory.CreateDirectory(Path.Combine(sourcePath, JabaUtils.PackageNameToDirectory(subPkg)));

            #endregion
            // Core files
            #region Assembly Info
            GenerateFile(Path.Combine(Path.Combine(sourcePath, JabaUtils.PackageNameToDirectory(projectName)), "JarInfo.java"), Template.AssemblyInfo, parameters);

            if (generateMaven)
            {
                GenerateFile(Path.Combine(hostContext.Output, ".project"), Template.ProjectFileMaven, parameters);
                GenerateFile(Path.Combine(hostContext.Output, ".classpath"), Template.ClassPathMaven, parameters);
                GenerateFile(Path.Combine(hostContext.Output, "pom.xml"), Template.Pom, parameters);
            }
            else
            {
                GenerateFile(Path.Combine(hostContext.Output, ".project"), Template.ProjectFile, parameters);
                GenerateFile(Path.Combine(hostContext.Output, ".classpath"), Template.ClassPath, parameters);
            }
            
            GenerateFile(Path.Combine(Path.Combine(hostContext.Output, ".settings"), "org.eclipse.jdt.core.prefs"), Template.Preferences, parameters);
            #endregion

            #region Generate Sources
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


            // Setup the template parameters
            string[][] templateFields = new string[][] 
            {
                new string[] { "$license$", parameters.ContainsKey("rimbapi-license") ? Licenses.ResourceManager.GetString(parameters["rimbapi-license"][0].ToUpper()) : "" },
                new string[] { "$org$", parameters.ContainsKey("rimbapi-org") ? parameters["rimbapi-org"][0] : "" },
                new string[] { "$date$", DateTime.Now.ToString("yyyy-MM-dd") },
                new string[] { "$time$", DateTime.Now.ToString("HH:mm:ss") },
                new string[] { "$author$", Environment.UserName },
                new string[] { "$year$", DateTime.Now.Year.ToString() },
                new string[] { "$version$", Assembly.GetEntryAssembly().GetName().Version.ToString() },
                new string[] { "$guid$", Guid.NewGuid().ToString() }, 
                new string[] { "$name$", projectName },
                new string[] { "$apins$", parameters["rimbapi-api-ns"][0] },
                new string[] { "$clrversion$", Environment.Version.ToString()},
                new string[] { "$mrversion$", InteractionRenderer.profileId ?? "" }
            };

            TextWriter jFileList = null;
            try
            {
                jFileList = File.CreateText(Path.Combine(hostContext.Output, "sources.index"));

                jFileList.WriteLine(Path.Combine(Path.Combine(sourcePath, JabaUtils.PackageNameToDirectory(projectName)), "JarInfo.java"), Template.AssemblyInfo, parameters);

                // Render initial feature list
                RenderFeatureList(features, templateFields, renderers, jFileList, parameters, projectName, sourcePath);

                // Any added features?
                // HACK: This should be fixed soon, but meh... I'll get around to it
                List<Feature> addlFeatures = new List<Feature>();
                foreach (KeyValuePair<String, Feature> kv in classRep)
                    if (!features.Contains(kv.Value))
                        addlFeatures.Add(kv.Value);
                RenderFeatureList(addlFeatures, templateFields, renderers, jFileList, parameters, projectName, sourcePath);

            }
            finally
            {
                if (jFileList != null)
                    jFileList.Close();
            }
            #endregion

            // Compile?
            #region Compile this project

            // Does the user want to compile?
            if (parameters.ContainsKey("rimbapi-compile") && Convert.ToBoolean(parameters["rimbapi-compile"][0]))
            {
                if (generateMaven)
                {
                    Trace.WriteLine("ERROR: Cannot compile when --rimbapi-maven is specified", "error");
                    return;
                }
                Trace.WriteLine(String.Format("Output will be logged to: {0}", hostContext.Output), "information");

                // Generate compile arguments
                StringBuilder compileArgs = new StringBuilder(),
                    jarArgs = new StringBuilder(),
                    jdocArgs = new StringBuilder();

                // First, the classpath 
                compileArgs.AppendFormat("-encoding UTF8 -nowarn -classpath \"{0}\" ", jarFile);
                // Output
                compileArgs.AppendFormat("-d \"{0}\" ", Path.Combine(hostContext.Output, "bin"));
                // Sources
                compileArgs.AppendFormat("-sourcepath \"{0}\" ", Path.Combine(hostContext.Output, "src"));
                compileArgs.Append("-g:none ");
                // source files
                compileArgs.AppendFormat("@{0}", Path.Combine(hostContext.Output, "sources.index"));

                // Jar options
                jarArgs.AppendFormat("cf \"{0}\" ", Path.Combine(hostContext.Output, String.Format("{0}.jar", projectName)));
                jarArgs.AppendFormat("@\"{0}\" ", Path.Combine(hostContext.Output, "classes.index"));

                // Javadoc args
                jdocArgs.AppendFormat("-docencoding UTF-8 -charset UTF-8 -encoding UTF-8 -sourcepath \"{0}\" ", Path.Combine(hostContext.Output, "src"));
                jdocArgs.AppendFormat("-classpath \"{0}\" ", jarFile);
                jdocArgs.AppendFormat("-d \"{0}\" ", Path.Combine(hostContext.Output, "doc"));
                jdocArgs.AppendFormat(" {0} @{1}", projectName, Path.Combine(hostContext.Output, "sources.index"));

                // Additional stuffs
                if(parameters.ContainsKey("rimbapi-jopt"))
                    foreach (var parm in parameters["rimbapi-jopt"])
                    {
                        jdocArgs.AppendFormat(" {0} ", parm);
                        compileArgs.AppendFormat(" {0} ", parm);
                        jarArgs.AppendFormat(" {0} ", parm);
                    }
                // Create process start info
                ProcessStartInfo psiJavac = new ProcessStartInfo(javacPath, compileArgs.ToString()),
                    psiJar = new ProcessStartInfo(jarPath, jarArgs.ToString()),
                    psiJavaDoc = new ProcessStartInfo(javaDocPath, jdocArgs.ToString());
                psiJavaDoc.UseShellExecute = psiJar.UseShellExecute = psiJavac.UseShellExecute = false;
                //psiJavac.RedirectStandardOutput = psiJar.RedirectStandardOutput = psiJar.RedirectStandardError = psiJavac.RedirectStandardError = true;

                // Setup processes
                Process javacProc = new Process(),
                    jarProc = new Process(),
                    jdocProc = new Process();

                javacProc.StartInfo = psiJavac;
                jarProc.StartInfo = psiJar;
                jdocProc.StartInfo = psiJavaDoc;

                // Compile
                System.Diagnostics.Trace.Write(String.Format("Compiling project:\r\n\t{0}\r\n\t{1}\r\nWait...", javacPath, psiJavac.Arguments), "information");
        
                // Start compile
                javacProc.Start();
                //logWriter.Write(javacProc.StandardOutput.ReadToEnd());
                javacProc.WaitForExit();
                if (javacProc.ExitCode == 0)
                {
                    Trace.WriteLine("Success!", "information");

                    // Generate documentation?
                    bool jd = false;
                    if (parameters.ContainsKey("rimbapi-jdoc"))
                        jd = Boolean.Parse(parameters["rimbapi-jdoc"][0]);
                    if (jd)
                    {
                        System.Diagnostics.Trace.Write(String.Format("Creating JavaDocs:\r\n\t{0}\r\n\t{1}\r\nWait...", jdocProc, psiJavaDoc.Arguments), "information");
                        jdocProc.Start();
                        // logWriter.Write(jarProc.StandardOutput.ReadToEnd());
                        jdocProc.WaitForExit();
                        if (jdocProc.ExitCode == 0)
                            Trace.WriteLine("Success!", "information");
                        else
                        {
                            Trace.WriteLine("Fail!", "information");
                        }
                    }

                    // Enumerate classes
                    var classList = JabaUtils.GenerateClassIndex(Path.Combine(hostContext.Output, "bin"));
                    try
                    {
                        jFileList = File.CreateText(Path.Combine(hostContext.Output, "classes.index"));
                        foreach (var classFile in classList)
                            jFileList.WriteLine(String.Format("-C \"{0}\" \"{1}\"",
                                Path.Combine(hostContext.Output, "bin"),
                                classFile.Replace(String.Format("{0}{1}", Path.Combine(hostContext.Output, "bin"), Path.DirectorySeparatorChar), "")).Replace('\\', '/')
                            );
                    }
                    finally
                    {
                        if (jFileList != null)
                            jFileList.Close();
                    }
                    // Jar it up
                    System.Diagnostics.Trace.Write(String.Format("Creating JAR:\r\n\t{0}\r\n\t{1}\r\nWait...", jarPath, psiJar.Arguments), "information");
                    jarProc.Start();
                    // logWriter.Write(jarProc.StandardOutput.ReadToEnd());
                    jarProc.WaitForExit();
                    if (jarProc.ExitCode == 0)
                        Trace.WriteLine("Success!", "information");
                    else
                    {
                        Trace.WriteLine("Fail!", "information");
                        throw new InvalidOperationException("Can't continue, compile failed!");
                    }

                }
                else
                {
                    Trace.WriteLine("Fail!", "information");
                    throw new InvalidOperationException("Can't continue, compile failed!");
                }

            }
            #endregion

            // Does the user only want asm?
            #region jaronly
            if (parameters.ContainsKey("rimbapi-jaronly") && parameters.ContainsKey("rimbapi-compile") && Convert.ToBoolean(parameters["rimbapi-jaronly"][0]))
                try
                {
                    // Move the everest jar to root
                    File.Move(jarFile, Path.Combine(hostContext.Output, Path.GetFileName(jarFile)));

                    // Clean all in the projects and remove all directories
                    List<String> directories = new List<string>(new string[] {
                        Path.Combine(hostContext.Output, "bin"), 
                        Path.Combine(hostContext.Output, "lib"), 
                        Path.Combine(hostContext.Output, "src"),
                        Path.Combine(hostContext.Output, ".settings"),
                    });

                    // Clean dirs
                    foreach (string s in directories)
                        Directory.Delete(s, true);
                    foreach (string f in Directory.GetFiles(hostContext.Output))
                        if (Path.GetExtension(f) != ".jar")
                            File.Delete(f);
                }
                catch (Exception)
                {
                    System.Diagnostics.Trace.WriteLine("Could not clean working files!", "warn");
                }
            #endregion

        }

        /// <summary>
        /// Generate a templatized file
        /// </summary>
        private void GenerateFile(string outputFile, string templateContents, Dictionary<String,StringCollection> parameters)
        {
            string projectName = "output";
            if (parameters.ContainsKey("rimbapi-target-ns"))
                projectName = parameters["rimbapi-target-ns"][0];

            // Setup the template parameters
            string[][] templateFields = new string[][] 
            {
                new string[] { "$license$", parameters.ContainsKey("rimbapi-license") ? Licenses.ResourceManager.GetString(parameters["rimbapi-license"][0].ToUpper()) : "" },
                new string[] { "$org$", parameters.ContainsKey("rimbapi-org") ? parameters["rimbapi-org"][0] : "" },
                new string[] { "$date$", DateTime.Now.ToString("yyyy-MM-dd") },
                new string[] { "$time$", DateTime.Now.ToString("HH:mm:ss") },
                new string[] { "$author$", Environment.UserName },
                new string[] { "$year$", DateTime.Now.Year.ToString() },
                new string[] { "$version$", Assembly.GetEntryAssembly().GetName().Version.ToString() },
                new string[] { "$guid$", Guid.NewGuid().ToString() }, 
                new string[] { "$name$", projectName },
                new string[] { "$apins$", parameters["rimbapi-api-ns"][0] },
                new string[] { "$clrversion$", Environment.Version.ToString()},
                new string[] { "$mrversion$", InteractionRenderer.profileId ?? "" }
            };

            try
            {
                TextWriter tw = File.CreateText(outputFile);
                try
                {
                    string Header = templateContents; // Set the header to the default

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

            }
            catch (Exception)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Couldn't generate the {0} for this project", Path.GetFileName(outputFile)), "warn");
            }
        }

        /// <summary>
        /// Render feature list
        /// </summary>
        private void RenderFeatureList(List<Feature> features, string[][] templateFields, List<KeyValuePair<FeatureRendererAttribute, IFeatureRenderer>> renderers, TextWriter jFileList, Dictionary<String, StringCollection> parameters, string projectName, string sourcePath)
        {

            // Scan the class repo and start processing
            foreach (Feature f in features)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Rendering Java for '{0}'...", f.Name), "debug");

                // Is there a renderer for this feature
                KeyValuePair<FeatureRendererAttribute, IFeatureRenderer> fr = renderers.Find(o => o.Key.Feature == f.GetType() && !o.Key.IsFactory);
                KeyValuePair<FeatureRendererAttribute, IFeatureRenderer> factoryRenderer = renderers.Find(o => o.Key.Feature == f.GetType() && o.Key.IsFactory);

                // Was a renderer found?
                if (fr.Key == null)
                    System.Diagnostics.Trace.WriteLine(String.Format("can't find renderer for {0}", f.GetType().Name), "warn");
                else
                    if (GenerateFeature(fr, f, templateFields, projectName, jFileList, parameters, sourcePath) && factoryRenderer.Key != null)
                        GenerateFeature(factoryRenderer, f, templateFields, projectName, jFileList, parameters, sourcePath);
            }

        }

        /// <summary>
        /// Generate feature
        /// </summary>
        private bool GenerateFeature(KeyValuePair<FeatureRendererAttribute, IFeatureRenderer> fr, Feature f, string[][] templateFields, string projectName, TextWriter jFileList, Dictionary<String, StringCollection> parameters, string sourcePath)
        {
            string file = String.Empty;
            try // To write the file
            {
                // Start the rendering
                file = fr.Value.CreateFile(f, Path.Combine(sourcePath, JabaUtils.PackageNameToDirectory(projectName)));

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

                        jFileList.WriteLine(file);
                    }
                    finally
                    {
                        tw.Close();
                    }
                    return true;
                }
                return true;
            }
            catch (NotSupportedException)
            {
                if (!String.IsNullOrEmpty(file)) File.Delete(file);
                return false;
            }
            catch (Exception e)
            {
                if (!String.IsNullOrEmpty(file)) File.Delete(file);
                System.Diagnostics.Trace.WriteLine(String.Format("Could not write file '{0}', {1}", file, e.Message), "error");
                return false;
            }
        }

        /// <summary>
        /// Output help
        /// </summary>
        public override string Help
        {
            get 
            {
                StringBuilder helpString = new StringBuilder();
                foreach (String[] helpData in helpText)
                    helpString.AppendFormat("{0}\t{1}\r\n", helpData[0], helpData[1]);

                helpString.Append("\r\nExample:\r\ngpmr -v 7 -s mif/*.mif -r RIMBA_JA -o .\\output --rimbapi-target-ns=org.marc.everest.rmim.ca.\r\n");
                return helpString.ToString();
            
            }
        }
    }
}
