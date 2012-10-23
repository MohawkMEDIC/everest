using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Interfaces;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Attributes;
using MohawkCollege.EHR.gpmr.COR;
using System.IO;
using System.Text.RegularExpressions;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer
{
    /// <summary>
    /// Factory renderer for factory classes
    /// </summary>
    [FeatureRenderer(Feature = typeof(Class), IsFile = true, IsFactory = true)]
    class ClassFactoryRenderer : IFeatureRenderer
    {
        #region IFeatureRenderer Members

        // Method signatures that have been decleared 
        internal List<String> s_methodDeclarations = new List<string>();
        // Factory methods for each type. The dictionary is used to keep track of which factory methods
        // create which types. Example
        // KEY                              Value
        // MCCI_MTxxxxxxCA.Patient3         COCT_MTxxxxxxxCA.CreatePatient(x,y,z);
        static Dictionary<string, List<MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer.ClassRenderer.FactoryMethodInfo>> factoryMethods = new Dictionary<string, List<MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer.ClassRenderer.FactoryMethodInfo>>();



        /// <summary>
        /// Render the factory
        /// </summary>
        public void Render(string ownerPackage, string apiNs, Feature f, System.IO.TextWriter tw)
        {
            ClassRenderer.s_imports.Clear();
            // Validate arguments
            if (String.IsNullOrEmpty(ownerPackage))
                throw new ArgumentNullException("ownerPackage");
            if (String.IsNullOrEmpty(apiNs))
                throw new ArgumentNullException("apiNs");
            if (f == null || !(f is Class))
                throw new ArgumentException("Parameter must be of type Class", "f");

            // Create a local copy of the class
            Class cls = f as Class;

            StringWriter sw = new StringWriter();

            tw.WriteLine("package {0}.{1};", ownerPackage, cls.ContainerName.ToLower());


            #region Render Class Signature

            // Documentation
            if (DocumentationRenderer.Render(cls.Documentation, 0).Length == 0)
                sw.WriteLine("\t/** No Summary Documentation Found */");
            else
                sw.Write(DocumentationRenderer.Render(cls.Documentation, 0));

            // Create class signature
            sw.Write("public final class {0}Factory", Util.Util.PascalCase(cls.Name));

            // If class is generic class
            string genericString = String.Empty;
            foreach (TypeParameter tp in cls.TypeParameters ?? new List<TypeParameter>())
                genericString += tp + ",";
            if (!String.IsNullOrEmpty(genericString))
                genericString = String.Format("<{0}>", genericString.Substring(0, genericString.Length - 1)); // get rid of trailing ,

            sw.WriteLine("{");

            #endregion

            sw.WriteLine("\tprivate {0}Factory() {{ super(); }}\r\n", Util.Util.PascalCase(cls.Name));


            // Create newInstanceMethod
            sw.WriteLine("\tpublic static final {0} {1}Factory newInstance() {{", cls.TypeParameters != null && cls.TypeParameters.Count > 0 ? string.Format("<{0}>", genericString) : "", Util.Util.PascalCase(cls.Name));
            sw.WriteLine("\t\treturn new {0}Factory{1}();", Util.Util.PascalCase(cls.Name), genericString);
            sw.WriteLine("\t}");

            // Move to a factory
            // Is this an emtpy class that facilitates a choice?
            if (cls.SpecializedBy != null && cls.SpecializedBy.Count > 0 &&
                cls.IsAbstract)
            {
                //// NB: In Java apparently super classes' static methods are acceessable
                ////     in child classes which is different than .NET, so we're not going to cascade specializers
                foreach (TypeReference tr in cls.SpecializedBy)
                {

                    if (tr.Class == null || tr.Class.ContainerName == "RIM" && !RimbaJavaRenderer.GenerateRim ||
                        tr.Class.IsAbstract)
                        continue;

                    Class child = tr.Class;

                    // Create factory for the child
                    Dictionary<String, String[]> ctors = ClassRenderer.CreateFactoryMethod(tr, "retVal", ownerPackage);
                    // Write factory
                    foreach (var kv in ctors)
                    {

                        string methodSignature = String.Format("{1}.{0}.{2}.create{3}", cls.ContainerName, ownerPackage, Util.Util.PascalCase(cls.Name), Util.Util.PascalCase(child.Name)),
                            publicName = methodSignature;

                        // Regex for extracting the parameter type rather than the type/name
                        Regex parmRegex = new Regex(@"(([\w<>,.]*)\s(\w*)),?\s?");
                        MatchCollection parmMatches = parmRegex.Matches(kv.Value[0]);
                        foreach (Match match in parmMatches)
                            methodSignature += match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf(" "));


                        // JF: Added to protected against rendering the same factory method
                        if (s_methodDeclarations.Contains(methodSignature))
                            continue;
                        s_methodDeclarations.Add(methodSignature);

                        // Render if there is even any content
                        if (kv.Value[0].Length > 0)
                        {
                            string clsDoc = DocumentationRenderer.Render(child.Documentation, 1);

                            string ctorClassName = String.Format("{0}.{2}.{1}", ownerPackage, tr.Class.Name, tr.Class.ContainerName.ToLower());
                            //// import already exists?
                            //if(!s_imports.Exists(o=>o.EndsWith(Util.Util.PascalCase(tr.Class.Name))))
                            //{
                            //    s_imports.Add(ctorClassName);
                            //    ctorClassName = ctorClassName.Substring(ctorClassName.LastIndexOf(".") + 1);
                            //}
                            //if (s_imports.Contains(ctorClassName))
                            //    ctorClassName = ctorClassName.Substring(ctorClassName.LastIndexOf(".") + 1);

                            if (clsDoc.Contains("*/"))
                                sw.Write(clsDoc.Substring(0, clsDoc.LastIndexOf("*/")));
                            sw.WriteLine("* This function creates a new instance of {5}.{1}\r\n\t {4}\r\n\t*/\t\n\tpublic final {0} create{2}({3}) {{ ", ctorClassName, tr.Class.Name, Util.Util.PascalCase(child.Name), kv.Value[0].Substring(0, kv.Value[0].Length - 1), kv.Value[2], tr.Class.ContainerName.ToLower(), Util.Util.PascalCase(cls.Name)
                                );
                            sw.WriteLine("\t\t{0} retVal = new {0}();", ctorClassName);
                            sw.WriteLine("{0}", kv.Value[1]);
                            sw.WriteLine("\t\treturn retVal;");
                            sw.WriteLine("\t}");

                            if (!factoryMethods.ContainsKey(tr.Name))
                                factoryMethods.Add(tr.Name, new List<MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer.ClassRenderer.FactoryMethodInfo>());

                            MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer.ClassRenderer.FactoryMethodInfo myInfo = new MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer.ClassRenderer.FactoryMethodInfo(publicName, kv.Value[2], methodSignature);


                            //Match the regular expression below and capture its match into backreference number 1 «(([\w<>,]*?)\s(\w*),?\s?)»
                            //Match the regular expression below and capture its match into backreference number 2 «([\w<>,]*?)»
                            //Match a single character present in the list below «[\w<>,]*?»
                            //Between zero and unlimited times, as few times as possible, expanding as needed (lazy) «*?»
                            //A word character (letters, digits, etc.) «\w»
                            //One of the characters “<>,” «<>,»
                            //Match a single character that is a “whitespace character” (spaces, tabs, line breaks, etc.) «\s»
                            //Match the regular expression below and capture its match into backreference number 3 «(\w*)»
                            //Match a single character that is a “word character” (letters, digits, etc.) «\w*»
                            //Between zero and unlimited times, as many times as possible, giving back as needed (greedy) «*»
                            //Match the character “,” literally «,?»
                            //Between zero and one times, as many times as possible, giving back as needed (greedy) «?»
                            //Match a single character that is a “whitespace character” (spaces, tabs, line breaks, etc.) «\s?»
                            //Between zero and one times, as many times as possible, giving back as needed (greedy) «?»
                            foreach (Match match in parmMatches)
                                myInfo.parameters.Add(match.Groups[1].Value);

                            // ADd the factory signature to the dictionary
                            factoryMethods[tr.Name].Add(myInfo);
                        }

                    }
                }

                //#endregion
            }
            // End class
            sw.WriteLine("}");


            #region Render the imports
            string[] apiImports = { "annotations.*", "datatypes.*", "datatypes.generic.*" },
                jImports = { "java.lang.*", "java.util.*" };
            foreach (var import in apiImports)
                tw.WriteLine("import {0}.{1};", apiNs, import);
            foreach (var import in jImports)
                tw.WriteLine("import {0};", import);
            foreach (var import in ClassRenderer.s_imports)
            {
                if (!import.EndsWith(String.Format(".{0}", Util.Util.PascalCase(f.Name))))
                    tw.WriteLine("import {0};", import);
            }

            tw.WriteLine(sw.ToString());
            #endregion

            if (cls.ContainerName == "RIM" && String.Format("{0}.rim.{1}", ownerPackage, Util.Util.PascalCase(cls.Name)) != RimbaJavaRenderer.RootClass && !RimbaJavaRenderer.GenerateRim)
                throw new NotSupportedException("RIM Elements will not be rendered");

        }

        /// <summary>
        /// Creates a flat list of specialization class references from a nested list
        /// </summary>
        private IEnumerable<TypeReference> CascadeSpecializers(List<TypeReference> list)
        {
            List<TypeReference> retVal = new List<TypeReference>(list);
            foreach (var tr in list)
                if (tr.Class != null && tr.Class.SpecializedBy != null)
                    retVal.AddRange(CascadeSpecializers(tr.Class.SpecializedBy));
            return retVal;
        }

        /// <summary>
        /// Determine if a factory is needed
        /// </summary>
        public string CreateFile(Feature f, string FilePath)
        {
            Class cls = f as Class;
            if(cls.IsAbstract && cls.SpecializedBy != null && cls.SpecializedBy.Count > 0)
                return Path.ChangeExtension(Path.Combine(Path.Combine(FilePath, cls.ContainerName.ToLower()), String.Format("{0}Factory", Util.Util.PascalCase(f.Name))), "java");
            throw new NotSupportedException();
        }

        #endregion
    }
}
