using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MARC.Everest.Attributes;
using System.IO;
using System.Diagnostics;

namespace MARC.Everest.Test.Generator
{
    internal class InstanceGenerator
    {


        /// <summary>
        /// Generate interaction instance tests
        /// </summary>
        internal static string GenerateInstanceTests(string asmFile)
        {
            StringWriter retVal = new StringWriter();
            Assembly asm = Assembly.LoadFile(asmFile);
            foreach (var type in asm.GetTypes())
            {
                object[] structureAttributes = type.GetCustomAttributes(typeof(StructureAttribute), false);
                if (structureAttributes.Length > 0 &&
                    (structureAttributes[0] as StructureAttribute).StructureType == StructureAttribute.StructureAttributeType.Interaction)
                {
                    Trace.WriteLine(String.Format("Generating Instance Test {0}...", type.Name), "information");
                    // Generate the instance
                    string template = Template.InstanceTest;
                    string[][] parameters = {
                                                new string[] { "instanceClass", type.FullName },
                                                new string[] { "instanceName", (structureAttributes[0] as StructureAttribute).Name },
                                                new string[] { "className", Path.GetFileNameWithoutExtension(Program.FileName) }
                                            };
                    foreach (var parm in parameters)
                        template = template.Replace(String.Format("${0}$", parm[0]), parm[1]);
                    retVal.WriteLine(template);
                }

            }
            return retVal.ToString();
        }

        internal static bool ShouldRender(Type t)
        {
            if (!t.IsGenericType && t.BaseType != null && t.BaseType.IsGenericType) return ShouldRender(t.BaseType);
            else if (!t.IsGenericType) return true;

            bool render = true;
            foreach (Type arg in t.GetGenericArguments())
                render &= arg != typeof(System.Object) && ShouldRender(arg);
            return render;
        }

        /// <summary>
        /// Generate formatter tests
        /// </summary>
        internal static string GenerateFormatterTests(string asmFile)
        {
            List<String> generated = new List<string>(10);
            StringWriter retVal = new StringWriter();
            Assembly asm = Assembly.LoadFile(asmFile);
            string[] settings = { "DefaultMultiprocessor", "DefaultUniprocessor", "DefaultLegacy" };
            
            foreach(string currentSetting in settings)
                foreach (var type in asm.GetTypes())
                {
                    object[] structureAttributes = type.GetCustomAttributes(typeof(StructureAttribute), false);
                    if (structureAttributes.Length > 0 && (structureAttributes[0] as StructureAttribute).StructureType == StructureAttribute.StructureAttributeType.Interaction
                   )
                    {
                        Trace.WriteLine(String.Format("Generating Formatter Test {0}...", type.Name), "information");
                        // Generate the instance
                        string template = Template.FormatterTest;
                        string[][] parameters = {
                                                    new string[] { "instanceClass", type.FullName },
                                                    new string[] { "instanceName", (structureAttributes[0] as StructureAttribute).StructureType == StructureAttribute.StructureAttributeType.Interaction ? (structureAttributes[0] as StructureAttribute).Name : type.FullName.Replace(".","") },
                                                    new string[] { "className", Path.GetFileNameWithoutExtension(Program.FileName) },
                                                    new string[] { "formatterSettings", currentSetting }
                                                };
                        foreach (var parm in parameters)
                            template = template.Replace(String.Format("${0}$", parm[0]), parm[1]);

                        // Build assertions
                        StringWriter assertions = new StringWriter();

                        foreach (var pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                            if((!pi.PropertyType.IsGenericType || pi.PropertyType.GetGenericTypeDefinition() != typeof(List<>)) &&
                                ShouldRender(pi.PropertyType) && pi.PropertyType != typeof(System.Object))
                                assertions.WriteLine("\t\tAssert.AreEqual(original.{0}, parsed.{0});", pi.Name);

                        retVal.WriteLine(template.Replace("$assertions$",assertions.ToString()));
                    }

                }
            return retVal.ToString();

        }
    }
}
