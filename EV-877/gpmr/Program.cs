/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 * Program.cs created with MonoDevelop
 * User: justin at 8:51 PM 11/13/2008
 */

using System;
using MohawkCollege.Util.Console.MessageWriter;
using MohawkCollege.Util.Console.Parameters;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using MohawkCollege.EHR.gpmr.Pipeline;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace gpmr
{
	/// <summary>
	/// The general purpose mif renderer is responsible for rendering MIF
	/// files into output formats using "rendering pipeline"
	/// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
    public class Program
	{
		
		/// <summary>
		/// Mif parameters
		/// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static MifConverterParameters parameters;

        /// <summary>
        /// Load assemblies and extensions
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "w"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private static void loadAssemblies(MessageWriter w)
        {
            // Hello 
            // Load all assemblies in the app dir
            foreach (string file in Directory.GetFiles(Application.StartupPath, "*.dll"))
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Loading {0}...", file), "debug");
                Assembly a = Assembly.LoadFile(file);
                AppDomain.CurrentDomain.Load(a.FullName);
            }

            // Load extensions
            if(parameters.RenderExtensions != null)
                foreach (string file in parameters.RenderExtensions)
                {
                    try
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("Loading {0}...", file), "debug");
                        Assembly a = Assembly.LoadFile(file);
                        AppDomain.CurrentDomain.Load(a.FullName);
                    }
                    catch (Exception) // Try to load from GAC
                    {
                        try
                        {
                            AppDomain.CurrentDomain.Load(file);
                        }
                        catch (Exception ie)
                        {
                            System.Diagnostics.Trace.WriteLine(ie.ToString(), "error");
                        }
                    }
                }
        }

        /// <summary>
        /// Process all sources passed to the program on the command line. MIF files can be
        /// processed individually : 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private static void processTranformation()
        {
           
            Pipeline processPipeline = new Pipeline();


            // Load all pipeline components and triggers from available assemblies
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type t in a.GetTypes())
                {
                    if (t.GetInterface("MohawkCollege.EHR.gpmr.Pipeline.IPipelineComponent") != null
                        && !t.IsAbstract)
                    {
                        processPipeline.Components.Add((IPipelineComponent)a.CreateInstance(t.FullName));
                        System.Diagnostics.Trace.WriteLine(string.Format("Found pipeline component {0}", t.FullName), "debug");
                    }
                    else if (t.GetInterface("MohawkCollege.EHR.gpmr.Pipeline.IPipelineTrigger") != null
                        && !t.IsAbstract)
                    {
                        processPipeline.Triggers.Add((IPipelineTrigger)a.CreateInstance(t.FullName));
                        System.Diagnostics.Trace.WriteLine(string.Format("Found pipeline trigger {0}", t.FullName), "debug");
                    }
                }

            // Initialize the process pipeline
            processPipeline.Initialize();

            // Set data segment
            processPipeline.Data.Add("EnabledRenderers", new DumpableStringCollection(parameters.Renderers));
            processPipeline.Data.Add("CommandParameters", parameters.Extensions);

            if (parameters.StrictMode && parameters.QuirksMode)
            {
                Console.WriteLine("The --quirks and --strict parameters are exclusive and cannot be combined");
                return;
            }

            processPipeline.Mode = parameters.StrictMode ? Pipeline.OperationModeType.Strict : parameters.QuirksMode ? Pipeline.OperationModeType.Quirks : Pipeline.OperationModeType.Normal;


            if (processPipeline.Mode == Pipeline.OperationModeType.Quirks)
                Console.WriteLine("--- WARNING ---\r\n You are executing GPMR in Quirks mode, GPMR will continue to process models that cannot be verified to be correct\r\n--- WARNING ---");

            processPipeline.Output = parameters.OutputDirectory;

            // Saturate the pipeline file list
            foreach (string filePattern in parameters.Sources)
            {
                // See what we are including
                if (filePattern.Contains("*") || filePattern.Contains("?"))
                {
                    string path = Path.GetDirectoryName(filePattern);
                    foreach (string file in Directory.GetFiles(path, Path.GetFileName(filePattern)))
                        processPipeline.InputFiles.Push(file);
                }
                else
                    processPipeline.InputFiles.Push(filePattern);

            }

            // Begin Pipeline Execution
            try
            {
                processPipeline.Execute();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine("The execution of the pipeline has failed. No processing can continue", "fatal");
                System.Diagnostics.Trace.WriteLine(e.Message, "fatal");
            }
        }

		/// <summary>
		/// Dump help contents to the console
		/// </summary>
		/// <param name="consoleWriter">The console writer to write version information to</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)")]
        private static void dumpHelp()
		{
			new ParameterParser<MifConverterParameters>().WriteHelp(System.Console.Out);
			Console.WriteLine("\r\n---- Available Renderers ---\r\n");
			
			// Attempt to print the pipeline components available
			#region Print pipeline components that are loaded to the console stream
			try
			{
				AppDomain d = AppDomain.CurrentDomain;
				foreach(Assembly a in d.GetAssemblies())
					foreach(Type t in a.GetTypes())
                        if (t.BaseType != null && t.BaseType.FullName == "MohawkCollege.EHR.gpmr.Pipeline.RendererBase")
					    {
						    RendererBase r = (RendererBase)a.CreateInstance(t.FullName);
						    Console.WriteLine(string.Format("{0}{2}-\t{1}", r.Identifier, r.Name, new string(' ', 10 - r.Identifier.Length)));
					    }

                Console.WriteLine("\r\n");

                // Print HELP for renderers
                foreach (Assembly a in d.GetAssemblies())
                    foreach (Type t in a.GetTypes())
                        if (t.BaseType != null && t.BaseType.FullName == "MohawkCollege.EHR.gpmr.Pipeline.RendererBase")
                        {
                            RendererBase r = (RendererBase)a.CreateInstance(t.FullName);
                            Console.WriteLine(string.Format("{0}\r\n{1}\r\n{2}", r.Name, new string('-', r.Name.Length), r.Help));
                        }

            }
			catch(Exception e)
			{
				throw new ApplicationException("Could not load pipeline component help", e);
			}
			#endregion
		}
		
		/// <summary>
		/// Dump versions to the screen
		/// </summary>
		/// <param name="consoleWriter">The console writer to write version information to</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)")]
        private static void dumpVersions()
		{
			try
			{

				AppDomain cd = AppDomain.CurrentDomain;
				
				foreach(Assembly a in cd.GetAssemblies())
					Console.WriteLine(string.Format("{0}{1}v{2}", a.GetName().Name, 
					                                  new string(' ', 60 - a.GetName().Name.Length),
					                                  a.GetName().Version.ToString()));
			}
			catch(Exception e)
			{
				throw new ApplicationException("Could not generate assembly versions", e);
			}
		}
		
		/// <summary>
		/// Main entry point into the Mif Converter application
		/// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt32(System.String)")]
        [STAThread]
        public static void Main(string[] args)
		{



			ConsoleTraceWriter consoleWriter = null;
			
			try
			{
				// Create a parser
				ParameterParser<MifConverterParameters> parser = new ParameterParser<MifConverterParameters>();
				parameters = parser.Parse(args);
				
				// Create the console writer
                consoleWriter = new ConsoleTraceWriter(MessageWriter.VerbosityType.Information | MessageWriter.VerbosityType.Fatal | MessageWriter.VerbosityType.Error);
                
                // Are there any shortcuts
                if (parameters.Quiet)
                    consoleWriter.Verbosity = MessageWriter.VerbosityType.None;
                else if (parameters.ErrorsOnly)
                    consoleWriter.Verbosity = MessageWriter.VerbosityType.Error | MessageWriter.VerbosityType.Fatal;
                else if (parameters.Debug)
                    consoleWriter.Verbosity = (MessageWriter.VerbosityType)15;
                else if (parameters.Chatty)
                    consoleWriter.Verbosity = (MessageWriter.VerbosityType)31;
                else if(parameters.Verbosity != null)
                    consoleWriter.Verbosity = (MessageWriter.VerbosityType)Convert.ToInt32(parameters.Verbosity);


                // Are we doing anything?
                bool noAction = !parameters.Help && !parameters.ShowVersion && (args.Length == 0 || parameters.Sources == null || parameters.Sources.Count == 0 ||
                    parameters.Renderers == null || parameters.Renderers.Count == 0); // IF no parameters, then help will be displayed
                

                // Now default
                System.Diagnostics.Trace.Listeners.Add(consoleWriter);

				// Display information
				Console.WriteLine("General Purpose MIF Converter & Render Tool v{0}", 
                    Assembly.GetEntryAssembly().GetName().Version);
                Console.WriteLine("Copyright (C) 2008-2012 Mohawk College of Applied Arts and Technology");

                Console.WriteLine("All rights reserved");

                // Load extensions
                loadAssemblies(consoleWriter);
				
				// Help being displayed?
                if (noAction)
                {
                    Console.WriteLine("\r\nNothing to do! \r\nUsage:");
                    Console.WriteLine("gpmr --renderer=xxxx --source=source.mif\r\ngpmr --renderer=xxxx source.mif\r\ngpmr --renderer=xxxx --output-dir=out_dir\r\n");
                    Console.WriteLine("For parameter listing use : gpmr --help");
                }
                else if (parameters.Help)
                    dumpHelp();
                else if (parameters.ShowVersion)
                    dumpVersions();
                else
                    processTranformation();

                // Write out stats
                Console.WriteLine("\r\nOperation completed, following messages were generated:");
                foreach (KeyValuePair<string, Int32> kv in consoleWriter.MessageCount)
                    Console.WriteLine("{0} -> {1}", kv.Key, kv.Value);

                Environment.Exit(0);
			}
			catch(Exception e)
			{
                Console.WriteLine(e.Message);
				System.Diagnostics.Debug.Print(e.ToString());
                Environment.Exit(1);
			}


#if DEBUG
            System.Console.ReadKey();
#endif

		}
	}
}
