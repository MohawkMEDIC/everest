/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 6-5-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.Util.Console.Parameters;
using MARC.Everest.Sherpas.Templating.Loader;
using System.Globalization;
using MARC.Everest.Sherpas.Templating.Format;
using System.IO;
using System.Reflection;
using MARC.Everest.Sherpas.Templating.Binder;
using System.Xml.Serialization;
using System.CodeDom;
using MARC.Everest.Sherpas.Templating.Renderer.CS;
using MARC.Everest.Threading;

namespace sherptc
{
    public class Program
    {

        /// <summary>
        /// The main entry point to the program
        /// </summary>
        public static void Main(String[] args)
        {

            // Parser for parameters
            ParameterParser<ConsoleParameters> parser = new ParameterParser<ConsoleParameters>();

            try
            {

                Console.WriteLine("SHERPAS CDA Template Compiler for MARC-HI Everest Framework");
                Console.WriteLine("Copyright (C) 2014, Mohawk College of Applied Arts and Technology");

                // Parse parameters
                var parameters = parser.Parse(args);

                if (parameters.Help)
                {
                    parser.WriteHelp(Console.Out);
                    return;
                }

                // First, load the specified loader
                XsltBasedLoader loader = new XsltBasedLoader();
                loader.Locale = parameters.Locale ?? CultureInfo.CurrentCulture.Name;

                if (String.IsNullOrEmpty(parameters.EverestAssembly) || !File.Exists(parameters.EverestAssembly))
                    throw new FileNotFoundException(String.Format("Couldn't find the specified Everest assembly file '{0}'...", parameters.EverestAssembly));
                var asm = Assembly.LoadFile(parameters.EverestAssembly);

                // Now load
                Console.Out.WriteLineIf(parameters.Verbose, "Loading template files into template project...");
                TemplateProjectDefinition compileTemplate = new TemplateProjectDefinition();

                foreach (var s in parameters.Template)
                {
                    Console.Out.WriteLineIf(parameters.Verbose, "\t{0}", Path.GetFileNameWithoutExtension(s));
                    compileTemplate.Merge(loader.LoadTemplate(s));
                }
                compileTemplate.ProjectInfo.AssemblyRef = asm.FullName;

                // Now compile or normalize the template ... I.e. bind them
                Console.Out.WriteLineIf(parameters.Verbose, "Binding template project to assembly '{0}'...", asm.GetName().Name);
                for (int i = 0; i < compileTemplate.Templates.Count; i++)
                {
                    var tpl = compileTemplate.Templates[i];
                    Console.Out.WriteLineIf(parameters.Verbose, "\t[{1} %] Binding template '{0}' ", tpl.Name, (int)(((float)i / compileTemplate.Templates.Count) * 100));

                    var ctx = new BindingContext(asm, tpl, compileTemplate);
                    var binder = ctx.GetBinder();
                    if (binder == null)
                        Console.Error.WriteLineIf(parameters.Verbose, "Cannot find a valid Binder for template of type '{0}'", tpl.GetType().Name);
                    else
                        binder.Bind(ctx);
                }

                // Save bound template?

                if (!String.IsNullOrEmpty(parameters.SaveTpl))
                {
                    Console.Out.WriteLineIf(parameters.Verbose, "Saving bound template to '{0}'...", parameters.SaveTpl);
                    compileTemplate.Save(parameters.SaveTpl);
                }

                // Render
                if (String.IsNullOrEmpty(parameters.OutputFile))
                    parameters.OutputFile = Path.GetFileNameWithoutExtension(parameters.Template[0]) + ".cs";

                Console.Out.WriteLineIf(parameters.Verbose, "Generating code to '{0}'...", parameters.OutputFile);
                CodeDomRenderer renderer = new CodeDomRenderer();
                renderer.Render(compileTemplate, parameters.OutputFile);
            }
            catch (Exception e)
            {

#if DEBUG
                Console.WriteLine(e.ToString());
#else
                Console.WriteLine(e.Message);

#endif
            }
            finally
            {
#if DEBUG
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
#endif
            }


        }

    }
}
