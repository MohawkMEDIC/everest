using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Formatters.XML.ITS1;
using System.Reflection;
using MohawkCollege.Util.Console.Parameters;
using System.CodeDom;
using MARC.Everest.Connectors;
using MARC.Everest.Attributes;
using MARC.Everest.Threading;
using MARC.Everest.Formatters.XML.ITS1.CodeGen;
using Microsoft.CSharp;
using System.IO;
using MARC.Everest.DataTypes;

namespace FormatterUtil
{
    class Program
    {

        // Types already generated
        private static List<Type> s_formatterGenerated = new List<Type>(100);

        static void Main(string[] args)
        {

            Console.WriteLine("XML ITS1 Formatter Pregenerator Utility");
            Console.WriteLine("Copyright (C) 2012, Mohawk College of Applied Arts and Technology");

            ParameterParser<Parameters> parser = new ParameterParser<Parameters>();

            try
            {

                var arguments = parser.Parse(args);

                if (arguments.ShowHelp)
                {
                    ShowHelp();
                    return;
                }
                // Generate formatter utility
                MARC.Everest.Formatters.XML.ITS1.CodeGen.TypeFormatterCreatorEx creator = new MARC.Everest.Formatters.XML.ITS1.CodeGen.TypeFormatterCreatorEx();

                // Create code namespace
                CodeNamespace ns = new CodeNamespace(arguments.TargetNs);
                // Load assembly
                Assembly rmimAssembly = Assembly.LoadFile(arguments.AssemblyFile);

                List<Type> rmimTypes = new List<Type>();

                if (arguments.Interactions != null)
                    foreach (var s in arguments.Interactions)
                        rmimTypes.Add(rmimAssembly.GetTypes().First(o => o.Name == s));
                else
                    rmimTypes.AddRange(rmimAssembly.GetTypes());

                // Validate parameters
                if (rmimTypes.Count == 0)
                    throw new ArgumentException("Type array must have at least one element", "t");

                // Create a list of types (a todo list) that represent the types we want to format
                List<Type> types = new List<Type>(200);

                // Iterate through types and create formatter
                // Iterate throgh the types
                foreach (Type type in rmimTypes)
                {
                    if (type.Assembly != rmimAssembly)
                        throw new InvalidOperationException("All types must belong to the same revision assembly");
                    GetUniqueTypes(type, types, true);
                }


                // Waith thread pool
                WaitThreadPool wtp = new WaitThreadPool();
                try
                {
                    // Create type definitions
                    foreach (Type t in types)
                    {
                        // Check if we need to gen this type
                        if (t.GetCustomAttributes(typeof(StructureAttribute), false).Length == 0 ||
                            s_formatterGenerated.Contains(t))
                            continue;

                        s_formatterGenerated.Add(t);

                        // Structure Attribute
                        StructureAttribute sta = t.GetCustomAttributes(typeof(StructureAttribute), false)[0] as StructureAttribute;

                        // Type formatter creator
                        TypeFormatterCreatorEx crtr = new TypeFormatterCreatorEx();

                        // Reset event
                        crtr.CodeTypeDeclarationCompleted += new CreateTypeFormatterCompletedDelegate(delegate(CodeTypeDeclaration result)
                        {
                            // Add to the code currently created
                            if (result != null)
                                lock (ns)
                                    ns.Types.Add(result);
                        });

                        // Helper result
                        wtp.QueueUserWorkItem(crtr.CreateTypeFormatter, t);

                    }

                    // Wait for final pool to clear
                    wtp.WaitOne();
                }
                finally
                {
                    wtp.Dispose();
                }

                if (ns.Types.Count == 0)
                {
                    Console.WriteLine("Didn't create any formatter helpers...");
                    return;
                }




                // Setup compiler and referenced assemblies
                CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();


                using (TextWriter tw = File.CreateText(arguments.Output ?? "output.cs"))
                    csharpCodeProvider.GenerateCodeFromNamespace(ns, tw, new System.CodeDom.Compiler.CodeGeneratorOptions()
                    {
                        IndentString = "\t"
                    });
            }
            catch (ArgumentNullException)
            {
                ShowHelp();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
            finally
            {
            }

#if DEBUG
            Console.ReadKey();
#endif
        }

        /// <summary>
        /// Show help
        /// </summary>
        private static void ShowHelp()
        {
            Console.WriteLine("\r\nCreates XML ITS1 formatter helper classes and saves the result to a C# file which can be imported into either an Everest or" +
                "Everest for Windows Phone project. Performing this operation before an application is compiled will greatly increase formatter performance");

            ParameterParser<Parameters> parser = new ParameterParser<Parameters>();
            parser.WriteHelp(Console.Out);

        }

        /// <summary>
        /// Get unique types that <paramref name="rmimTypes"/> references
        /// </summary>
        /// <param name="rmimType">The rmim type to scan</param>
        /// <param name="current">The list of currently processed types</param>
        private static void GetUniqueTypes(Type rmimType, List<Type> current, bool generateDeep)
        {
            var sa = rmimType.GetCustomAttributes(typeof(StructureAttribute), false);
            if (current.Contains(rmimType) || rmimType == null || rmimType.IsAbstract || !rmimType.IsClass ||
                s_formatterGenerated.Contains(rmimType) ||
                sa.Length == 0 ||
                (sa[0] as StructureAttribute).StructureType == StructureAttribute.StructureAttributeType.DataType) // Already processed?
                return;

            current.Add(rmimType);

            // Scan types
            foreach (PropertyInfo pi in rmimType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object[] propertyAttribute = pi.GetCustomAttributes(typeof(PropertyAttribute), true);
                if (propertyAttribute.Length > 0)
                {
                    // Generic parameters!
                    foreach (Type t in pi.PropertyType.GetGenericArguments())
                        if (!current.Contains(t))
                            GetUniqueTypes(t, current, generateDeep);

                    if (generateDeep)
                        foreach (PropertyAttribute pa in propertyAttribute)
                            if (pa.Type != null)
                                GetUniqueTypes(pa.Type, current, generateDeep);

                    if (!current.Contains(pi.PropertyType))
                        GetUniqueTypes(pi.PropertyType, current, generateDeep);
                }
            }

            
        }
    }
}
