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
 * Date: 07-26-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MARC.Everest.Attributes;
using MARC.Everest.Connectors;
using System.CodeDom;
using System.Threading;
using MARC.Everest.DataTypes;
using System.CodeDom.Compiler;
using System.Diagnostics;

#if !WINDOWS_PHONE
using Microsoft.CSharp;
using MARC.Everest.Threading;
using System.IO;
#endif

namespace MARC.Everest.Formatters.XML.ITS1.CodeGen
{
    /// <summary>
    /// This class handles the code generation method of formatting 
    /// </summary>
    internal class CodeGenFormatter
    {

        // A dictionary of formatters that have been created in all formatter instances
        private static Dictionary<Type, Type> s_dictFormatters = new Dictionary<Type, Type>(100);
        private static List<Type> s_formatterGenerated = new List<Type>(100);
        // Synchronization object
        private static readonly object m_syncObject = new object();
        private static bool m_codeGenBlocking = false;

        /// <summary>
        /// Get the formatter for the specified type
        /// </summary>
        public ITypeFormatter GetFormatter(Type t)
        {
            Type canditateFormatter = null;
            if (!s_dictFormatters.TryGetValue(t, out canditateFormatter))
                return null;

            // Get constructor 
            ConstructorInfo ctor = canditateFormatter.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
                throw new InvalidOperationException("Cannot find a default constructor for this type");
            return ctor.Invoke(null) as ITypeFormatter;
        }

#if !WINDOWS_PHONE
        /// <summary>
        /// Get unique types that <paramref name="rmimTypes"/> references
        /// </summary>
        /// <param name="rmimType">The rmim type to scan</param>
        /// <param name="current">The list of currently processed types</param>
        private void GetUniqueTypes(Type rmimType, List<Type> current, bool generateDeep)
        {
            var sa = rmimType.GetCustomAttributes(typeof(StructureAttribute), false);
            if (current.Contains(rmimType) || rmimType == null || rmimType.IsAbstract || !rmimType.IsClass ||
                s_dictFormatters.ContainsKey(rmimType) ||
                s_formatterGenerated.Contains(rmimType) ||
                sa.Length == 0 ||
                (sa[0] as StructureAttribute).StructureType == StructureAttribute.StructureAttributeType.DataType) // Already processed?
                return;

            current.Add(rmimType);

            //// Base type?
            //if (rmimType.BaseType != typeof(System.Object) && rmimType.BaseType != null)
            //    this.GetUniqueTypes(rmimType.BaseType, current, generateDeep);

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

                    if(generateDeep)
                        foreach (PropertyAttribute pa in propertyAttribute)
                            if (pa.Type != null)
                                GetUniqueTypes(pa.Type, current, generateDeep);

                    if (!current.Contains(pi.PropertyType))
                        GetUniqueTypes(pi.PropertyType, current, generateDeep);
                }
            }

        }

        /// <summary>
        /// Creates the formatter assembly for the specified types
        /// </summary>
        public Assembly CreateFormatterAssembly(Type[] rmimTypes, List<IStructureFormatter> aides, bool generateDeep)
        {
            // Enter and lock
            lock(m_syncObject)
            {
                while(m_codeGenBlocking)
                    Monitor.Wait(m_syncObject);
                m_codeGenBlocking = true;
            }

            // Create code namespace
            CodeNamespace ns = new CodeNamespace(String.Format("MARC.Everest.Formatters.XML.ITS1.d{0}", Guid.NewGuid().ToString("N")));
            List<Assembly> rmimAssemblies = new List<Assembly>() { rmimTypes[0].Assembly };
            

            try
            {
                // Validate parameters
                if (rmimTypes.Length == 0)
                    throw new ArgumentException("Type array must have at least one element", "t");

                // Scan all classes in any graph aides
                List<string> graphAidesClasses = new List<string>(200);
                foreach (IStructureFormatter isf in aides)
                    graphAidesClasses.AddRange(isf.HandleStructure);

                // Create a list of types (a todo list) that represent the types we want to format
                List<Type> types = new List<Type>(200);
                

                // Iterate through types and create formatter
                if (generateDeep)
                {
                    foreach (var type in Array.FindAll<Type>(rmimAssemblies[0].GetTypes(), o => o.IsClass && !o.IsAbstract && o.GetCustomAttributes(typeof(StructureAttribute), false).Length > 0))
                    {
                        //if (!rmimAssemblies.Contains(type.Assembly))
                        //    rmimAssemblies.Add(type.Assembly);
                        GetUniqueTypes(type, types, true);
                    }
                }
                else
                {
                    // Iterate throgh the types
                    foreach (Type type in rmimTypes)
                    {
                        //if (!rmimAssemblies.Contains(type.Assembly))
                        //    throw new InvalidOperationException("All types must belong to the same revision assembly");
                        GetUniqueTypes(type, types, false);
                    }
                }

               
                // Waith thread pool
                WaitThreadPool wtp = new WaitThreadPool(1);
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

                        // Scan and add base type
                        Type dScan = t.BaseType;
                        while (dScan != null && dScan != typeof(System.Object))
                        {
                            if (!rmimAssemblies.Contains(dScan.Assembly))
                                rmimAssemblies.Add(dScan.Assembly);
                            dScan = dScan.BaseType;
                        }

                        // Structure Attribute
                        StructureAttribute sta = t.GetCustomAttributes(typeof(StructureAttribute), false)[0] as StructureAttribute;

                        // Is this type already handled by a helper formatter?
                        bool hasHelper = graphAidesClasses.Contains(sta.Name);

                        // Compile if helper is not available
                        if (!hasHelper)
                        {

                            // Type formatter creator
                            TypeFormatterCreatorEx crtr = new TypeFormatterCreatorEx();

                            // Reset event
                            crtr.CodeTypeDeclarationCompleted += new CreateTypeFormatterCompletedDelegate(delegate(CodeTypeDeclaration result)
                            {
                                // Add to the code currently created
                                if (result != null)
                                    lock (ns)
                                    {
                                        ns.Types.Add(result);
                                    }
                            });

                            // Helper result
                            wtp.QueueUserWorkItem(crtr.CreateTypeFormatter, t);
                        }

                    }

                    // Wait for final pool to clear
                    wtp.WaitOne();
                }
                finally
                {
                    wtp.Dispose();
                }

                if (ns.Types.Count == 0)
                    return null;
                
                
            }
            finally
            {
                m_codeGenBlocking = false;
                lock(m_syncObject)
                    Monitor.Pulse(m_syncObject);
            }

            // Setup compiler and referenced assemblies
            CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            compileUnit.Namespaces.Add(ns);

            // Was this assembly loaded directly from disk or from a byte array
            foreach(var asm in rmimAssemblies)
                compileUnit.ReferencedAssemblies.Add(asm.Location);

            // Get any other references that might need to be used
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                if (!compileUnit.ReferencedAssemblies.Contains(asm.Location) && asm.FullName.StartsWith("MARC.Everest"))
                    compileUnit.ReferencedAssemblies.Add(Path.GetFileName(asm.Location ?? asm.FullName));

            compileUnit.ReferencedAssemblies.Add("System.dll");
            compileUnit.ReferencedAssemblies.Add("System.Xml.dll");

            // Assembly info
            CodeAttributeDeclaration cadecl = new CodeAttributeDeclaration("System.Reflection.AssemblyVersion", new CodeAttributeArgument[] 
                {
                    new CodeAttributeArgument(new CodePrimitiveExpression("1.0.*"))
                });

            compileUnit.AssemblyCustomAttributes.Add(cadecl);


            // Setup compiler
            CompilerParameters compilerParms = new CompilerParameters();
            compilerParms.GenerateInMemory = !generateDeep;
            compilerParms.WarningLevel = 1;
            compilerParms.TempFiles.KeepFiles = generateDeep;
            compilerParms.IncludeDebugInformation = false;
            using (StreamWriter sw = new StreamWriter("C:\\temp\\csgen.cs"))
                csharpCodeProvider.GenerateCodeFromCompileUnit(compileUnit, sw, null);

            // Compile code dom
            // To see the generated code, set a breakpoint on the next line.
            // Then take a look at the results.TempFiles array to find the 
            // path to the generated C# files            
            CompilerResults results = csharpCodeProvider.CompileAssemblyFromDom(compilerParms, new CodeCompileUnit[] { compileUnit });

            if (results.Errors.HasErrors)
                throw new Exceptions.FormatterCompileException(results);
            else
            {
                Assembly a = !generateDeep ? results.CompiledAssembly : Assembly.LoadFile(results.PathToAssembly);
                AddFormatterAssembly(a);

                return a;
            }
        }
#endif
        /// <summary>
        /// Add a formatter assembly to the dict formatters
        /// </summary>
        internal void AddFormatterAssembly(Assembly assembly)
        {
            // Scan for all types
            foreach (Type t in assembly.GetTypes())
                if (t.GetInterface("MARC.Everest.Formatters.XML.ITS1.ITypeFormatter", true) != null)
                {
                    ITypeFormatter tf = (ITypeFormatter)assembly.CreateInstance(t.FullName);
                    lock (s_dictFormatters)
                    {
                        if (!s_dictFormatters.ContainsKey(tf.HandlesType))
                            s_dictFormatters.Add(tf.HandlesType, t);
                    }
                }
        }

       
    }
}
