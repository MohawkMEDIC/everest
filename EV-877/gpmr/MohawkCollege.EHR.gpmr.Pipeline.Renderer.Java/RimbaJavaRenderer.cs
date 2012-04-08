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
                new string[] {"--rimbapi-jdk", "Specifies the path to the JDK\r\n\t\t\twith which to compile the project"}
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
        private string m_javaccpath = Environment.GetEnvironmentVariable("JAVA_HOME");


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

            #region Validate all parameters
            // Validate parameters
            if (!parameters.ContainsKey("rimbapi-api-ns"))
            {
                parameters.Add("rimbapi-api-ns", new StringCollection());
                parameters["rimbapi-api-ns"].Add("ca.marc.everest");
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
            //if (parameters.ContainsKey("rimbapi-profileid"))
            //    InteractionRenderer.profileId = parameters["rimbapi-profileid"][0];
            //if (parameters.ContainsKey("rimbapi-oid-profileid"))
            //    InteractionRenderer.profileIdOid = parameters["rimbapi-oid-profileid"][0];
            //if (parameters.ContainsKey("rimbapi-oid-interactionid"))
            //    InteractionRenderer.interactionIdOid = parameters["rimbapi-oid-interactionid"][0];
            //if (parameters.ContainsKey("rimbapi-oid-triggerevent"))
            //    InteractionRenderer.triggerEventOid = parameters["rimbapi-oid-triggerevent"][0];
            if (parameters.ContainsKey("rimbapi-gen-its"))
                genFormatters = parameters["rimbapi-gen-its"];
            if (parameters.ContainsKey("rimbapi-jdk"))
                m_javaccpath = parameters["rimbapi-jdk"][0];

            if (string.IsNullOrEmpty(m_javaccpath))
                throw new ArgumentException("Cannot find JDK, specify location in JAVA_HOME or with --rimbapi-jdk", "rimbapi-jdk");

            #endregion

            // Validate the jdk path
            // TODO: Find a better way of doing this
            String javacPath = Util.JabaUtils.GetJavaCPath(m_javaccpath);
            if (string.IsNullOrEmpty(javacPath))
                throw new InvalidOperationException("Could not find javac on the path specified by --rimbapi-jdk parameter");

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

            // Core directories
            Directory.CreateDirectory(Path.Combine(hostContext.Output, "src"));
            Directory.CreateDirectory(Path.Combine(hostContext.Output, "bin"));
            Directory.CreateDirectory(Path.Combine(hostContext.Output, "lib"));
            Directory.CreateDirectory(Path.Combine(hostContext.Output, ".settings"));
            string[] subPackages = { projectName, 
                                       String.Format("{0}.{1}", projectName, "interaction"), 
                                       String.Format("{0}.{1}", projectName, "vocabulary"),
                                       String.Format("{0}.{1}", projectName, "rim")
                                   };
            foreach(var subPkg in subPackages)
                Directory.CreateDirectory(Path.Combine(Path.Combine(hostContext.Output, "src"), JabaUtils.PackageNameToDirectory(subPkg)));

            // Copy JAR to output directory
            string jarFile = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "lib"), "ca.marc.everest.jar");
            if (!File.Exists(jarFile))
                throw new FileNotFoundException("Cannot find the Everest JAR file");
            File.Copy(jarFile, Path.Combine(Path.Combine(hostContext.Output, "lib"), Path.GetFileName(jarFile)), true);
            #endregion
            // Core files
            #region Assembly Info
            GenerateFile(Path.Combine(Path.Combine(Path.Combine(hostContext.Output, "src"), JabaUtils.PackageNameToDirectory(projectName)), "JarInfo.java"), Template.AssemblyInfo, parameters);
            GenerateFile(Path.Combine(hostContext.Output, ".project"), Template.ProjectFile, parameters);
            GenerateFile(Path.Combine(hostContext.Output, ".classpath"), Template.ClassPath, parameters);
            GenerateFile(Path.Combine(Path.Combine(hostContext.Output, ".settings"), "org.eclipse.jdt.core.prefs"), Template.Preferences, parameters);
            #endregion

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

            // Scan the class repo and start processing
            foreach (Feature f in features)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Rendering Java for '{0}'...", f.Name), "debug");

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
                        file = fr.Value.CreateFile(f, Path.Combine(Path.Combine(hostContext.Output, "src"), JabaUtils.PackageNameToDirectory(projectName)));

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

                        }
                    }
                    catch (NotSupportedException)
                    {
                        if (!String.IsNullOrEmpty(file)) File.Delete(file);
                    }
                    catch (Exception e)
                    {
                        if (!String.IsNullOrEmpty(file)) File.Delete(file);
                        System.Diagnostics.Trace.WriteLine(String.Format("Could not write file '{0}', {1}", file, e.Message), "error");
                    }
                }
            }


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
        /// Output help
        /// </summary>
        public override string Help
        {
            get 
            {
                StringBuilder helpString = new StringBuilder();
                foreach (String[] helpData in helpText)
                    helpString.AppendFormat("{0}\t{1}\r\n", helpData[0], helpData[1]);

                helpString.Append("\r\nExample:\r\ngpmr -v 7 -s mif/*.mif -r RIMBA_JA -o .\\output --rimbapi-target-ns=ca.marc.everest.rmim.ca.\r\n");
                return helpString.ToString();
            
            }
        }
    }
}
