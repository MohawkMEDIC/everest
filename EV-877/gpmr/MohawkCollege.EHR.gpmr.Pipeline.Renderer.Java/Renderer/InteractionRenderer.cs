using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Interfaces;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Attributes;
using MohawkCollege.EHR.gpmr.COR;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer
{
    [FeatureRenderer(Feature = typeof(Interaction), IsFile = true)]
    public class InteractionRenderer : IFeatureRenderer
    {

        /// <summary>
        /// Default for trigger event, interaction id and profile id OIDs
        /// </summary>
        internal static string triggerEventOid = "2.16.840.1.113883.1.18",
            interactionIdOid = "2.16.840.1.113883.1.18",
            profileIdOid = "2.16.840.1.113883.2.20.2",
            profileId = null;

        List<String> generatedFactoryMethods = new List<String>();

        /// TODO: Explanation of parameters missing: tr and ownerNs
        ///       Description of the value returned missing
        /// <summary>
        /// Create a static factory method that creates a type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private string GenerateFactoryMethod(TypeReference tr, string ownerNs)
        {

            StringWriter sw = new StringWriter();

            Dictionary<string, string[]> cfm = ClassRenderer.CreateFactoryMethod(tr, "retVal", true, ownerNs);


            if (tr.Class != null && tr.Class.IsAbstract)
            {
                foreach (var chld in tr.Class.SpecializedBy)
                    sw.WriteLine(GenerateFactoryMethod(chld, ownerNs));
            }
            else
            {

                string factoryName = String.Format("create{0}", tr.Class.Name), tfact = factoryName;
                if (generatedFactoryMethods.Contains(factoryName))
                    factoryName = String.Format("create{0}", Util.Util.MakeFriendly(tr.Name));
                generatedFactoryMethods.Add(factoryName);

                // Iterate and create shortcuts
                List<String> wroteParms = new List<string>(); // Keep track of the parameters used
                foreach (KeyValuePair<String, String[]> kv in cfm)
                {

                    if (kv.Value[0].Length == 0 || wroteParms.Contains(kv.Value[0])) continue;

                    wroteParms.Add(kv.Value[0]);

                    // Correct generic children
                    // TODO: Turn these into regexes
                    //if (tr.Class.TypeParameters != null)
                    //    for (int i = 0; i < tr.Class.TypeParameters.Count; i++)
                    //    {
                    //        List<TypeReference> filler = tr.GenericSupplier == null ? new List<TypeReference>() : tr.GenericSupplier.FindAll(o => (o as TypeParameter).ParameterName == tr.Class.TypeParameters[i].ParameterName);

                    //        for(int f = 0; f< 2; f++)
                    //            if (filler.Count == 1)
                    //                foreach(string replacement in replaceMe)
                    //                    kv.Value[f] = kv.Value[f].Replace(String.Format(replacement, tr.Class.TypeParameters[i].ParameterName), String.Format(replacement, filler[0].Name));
                    //            else
                    //                foreach (string replacement in replaceMe)
                    //                    kv.Value[f] = kv.Value[f].Replace(String.Format(replacement, tr.Class.TypeParameters[i].ParameterName), String.Format(replacement, "System.Object"));
                    //    }

                    // Generate method(s)
                    Class cls = tr.Class;
                    sw.WriteLine("\t/** Create a new instance of {0} to populate {1}", tr.Name, tr is TypeParameter ? (tr as TypeParameter).ParameterName : "");
                    sw.Write(kv.Value[2]);

                    sw.WriteLine("\t*/\r\n\tpublic static {0} {1} ({2}) {{ \r\n\t\t{0} retVal = new {0}();\r\n{3}", ClassRenderer.CreateDatatypeRef(tr, new Property(), ownerNs), factoryName,
                        kv.Value[0].Substring(0, kv.Value[0].Length - 1), kv.Value[1]);
                    sw.WriteLine("\t\treturn retVal;\r\n\t}");

                    if (!wroteParms.Contains(""))
                    {
                        wroteParms.Add("");
                        sw.WriteLine("\t/** Create a new instance of {0} to populate {1} */", tr.Name, tr.Class.Name);
                        sw.WriteLine("\tpublic static {0} {1} () {{ \r\n\t\treturn new {0}();\r\n\t}}", ClassRenderer.CreateDatatypeRef(tr, new Property(), ownerNs), factoryName);
                    }
                }


                foreach (TypeReference tpr in tr.GenericSupplier ?? new List<TypeReference>())
                    sw.WriteLine(GenerateFactoryMethod(tpr, ownerNs));
            }

            return sw.ToString();

        }

        #region IFeatureRenderer Members

        /// TODO: Explanation of parameters missing: OwnerNS, apiNs, f and tw
        ///       Summary explanation needed
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public void Render(string ownerPackage, string apiNs, Feature f, System.IO.TextWriter tw)
        {

            ClassRenderer.s_imports.Clear();

            // Validate arguments
            if (String.IsNullOrEmpty(ownerPackage))
                throw new ArgumentNullException("ownerPackage");
            if (String.IsNullOrEmpty(apiNs))
                throw new ArgumentNullException("apiNs");
            if (f == null || !(f is Interaction))
                throw new ArgumentException("Parameter must be of type Enumeration", "f");

            this.generatedFactoryMethods = new List<string>();
            // Get a strongly typed reference to the 
            Interaction interaction = f.Clone() as Interaction;
            interaction.MemberOf = f.MemberOf;

            StringWriter sw = new StringWriter();

            #endregion

            // HACK: If there is no description on the interaction then we'll 
            // HACK: add the business name as a description
            // TODO: Remove this and come up with a better solution
            if (interaction.Documentation == null ||
                interaction.Documentation.Description == null)
                interaction.Documentation = new Documentation()
                {
                    Description = new List<string>() { interaction.BusinessName }
                };

            sw.Write(DocumentationRenderer.Render(interaction.Documentation, 0));
            sw.WriteLine("@Structure(name = \"{0}\", structureType = StructureType.INTERACTION)", interaction.Name);
            sw.WriteLine("@Interaction(triggerEvent = \"{0}\")", interaction.TriggerEvent);
            sw.WriteLine("@InteractionResponses(");
            foreach (Interaction response in interaction.Responses)
                sw.WriteLine("\t@InteractionResponse(name = \"{0}\", triggerEvent = \"{1}\")", response.Name, response.TriggerEvent);
            sw.WriteLine(")");
            sw.WriteLine("public class {0} extends {1} {{", interaction.Name, CreateInteractionDatatype(interaction.MessageType, ownerPackage));

            #region Constants

            // Get the trigger event
            sw.WriteLine("\t/** Gets the default trigger event to be used for this interaction */");
            sw.WriteLine("\tpublic static CV<String> getTriggerEvent() {{ return new CV<String>(\"{0}\", \"{1}\"); }}", interaction.TriggerEvent, triggerEventOid, apiNs);

            // Get the interaction id
            sw.WriteLine("\t/** Gets the interaction ID of this interaction */");
            sw.WriteLine("\tpublic static II getInteractionId() {{ return new II(new OID(\"{1}\"), \"{0}\"); }}", interaction.Name, interactionIdOid, apiNs);

            // Get the profile id
            if (!String.IsNullOrEmpty(profileId))
            {
                sw.WriteLine("\t/** Gets the profile id of this interaction */");
                sw.WriteLine("\tpublic static LIST<II> getProfileId() {{ LIST<II> retVal = new LIST<II>(); retVal.add(new II (\"{0}\", \"{1}\")); }}", profileIdOid, profileId, apiNs);
            }


            #endregion

            #region Constructors

            sw.WriteLine("\t/** Creates a new, empty instance of {0} */", interaction.Name);
            sw.WriteLine("\tpublic {0}() {{ super(); }}\r\n", interaction.Name);

            //string ctor_parameters = "";
            //string ctor_body = "";
            //foreach (ClassContent cc in interaction.MessageType.Class.Content)
            //{
            //    if (cc.Conformance == ClassContent.ConformanceKind.Mandatory) // Mandatory ctor
            //    {
            //        if (cc is Property && ((cc as Property).FixedValue == null || (cc as Property).PropertyType == Property.PropertyTypes.TraversableAssociation))
            //        {
            //            Property p = cc as Property;
            //            TypeReference tr = Datatypes.MapDatatype(p.Type);

            //            // Documentation
            //            if (p.Documentation != null && p.Documentation.Definition != null && p.Documentation.Definition.Count > 0)
            //                sw.WriteLine("\t\t/// <param name=\"{0}\">{1}</param>", p.Name, p.Documentation.Definition[0]);

            //            // Now the signature
            //            ctor_parameters += string.Format("{0} {1},", ClassRenderer.CreateDatatypeRef(tr, p), p.Name);
            //            ctor_body += string.Format("\t\t\tthis.{0} = {1};\r\n", Util.Util.PascalCase(p.Name), p.Name);

            //        }
            //        else
            //        {
            //        }
            //    }
            //}

            //// Write CTOR
            //if (ctor_parameters.Length > 0)
            //{
            //    sw.WriteLine("\t\t/// <summary>\r\n\t\t/// CTOR for all mandatory elements\r\n\t\t/// </summary>");
            //    sw.WriteLine("\t\tpublic {0}({1}) : base() {{ \r\n\t\t{2}\t\t}}", interaction.Name, ctor_parameters.Substring(0, ctor_parameters.Length - 1), ctor_body);
            //}

            Dictionary<String, String[]> ctors = ClassRenderer.CreateFactoryMethod(interaction.MessageType, "this", true, ownerPackage);

            // Write CTOR
            List<String> wroteParms = new List<string>(); // Keep track of the parameters used
            foreach (KeyValuePair<String, String[]> kv in ctors)
            {
                if (kv.Value[0].Length > 0 && !wroteParms.Contains(kv.Value[0]))
                {
                    wroteParms.Add(kv.Value[0]);
                    sw.WriteLine("\t/**\r\n\t * CTOR for all {0} elements\r\n\t", kv.Key);
                    sw.WriteLine(kv.Value[2]);
                    sw.WriteLine("\t*/\r\n\tpublic {0}({1}) {{ \r\n\t\tsuper();\r\n{2}\t}}", interaction.Name, kv.Value[0].Substring(0, kv.Value[0].Length - 1), kv.Value[1]);
                }
            }
            #endregion

            #region Creator for payload, control act, etc...

            // Owner namespace
            if (interaction.MessageType.GenericSupplier != null)
                foreach (TypeReference tr in interaction.MessageType.GenericSupplier)
                    sw.WriteLine(GenerateFactoryMethod(tr, ownerPackage));

            #endregion

            sw.WriteLine("}");

            #region Usings

            // Interactions package
            tw.WriteLine("package {0}.interaction;", ownerPackage);

            #region Render the imports
            string[] apiImports = { "annotations.*", "datatypes.*", "datatypes.generic.*" },
                jImports = { "java.lang.*", "java.util.*", string.Format("{0}.vocabulary.*", ownerPackage) };
            foreach (var import in apiImports)
                tw.WriteLine("import {0}.{1};", apiNs, import);
            foreach (var import in jImports)
                tw.WriteLine("import {0};", import);
            foreach (var import in ClassRenderer.s_imports)
                tw.WriteLine("import {0};", import);
            #endregion

            tw.WriteLine(sw.ToString());

        }

        /// TODO: Explanation of parameters missing: typeReference
        ///       Description of the value returned missing
        /// <summary>
        /// Create the interaction inheritence for the specified type reference
        /// </summary>
        private string CreateInteractionDatatype(TypeReference typeReference, string ownerPackage)
        {
            if (typeReference.CoreDatatypeName != null)
                return typeReference.CoreDatatypeName;

            string retVal = String.Format("{0}.{1}.{2}", ownerPackage, typeReference.Class.ContainerName.ToLower(), Util.Util.PascalCase(typeReference.Class.Name));

            if (typeReference.Class.TypeParameters != null && typeReference.Class.TypeParameters.Count > 0)
            {
                retVal += "<";
                foreach (var tr in typeReference.Class.TypeParameters)
                {
                    List<TypeReference> tp = typeReference.GenericSupplier == null ? null : typeReference.GenericSupplier.FindAll(o => (o as TypeParameter).ParameterName.Equals(tr.ParameterName));
                    if (tp == null || tp.Count != 1) // nothing is bound
                    {
                        var objectReference = new TypeReference() { Name = "java.lang.Object" };
                        retVal += CreateInteractionDatatype(objectReference, ownerPackage) + ",";
                    }
                    else
                        retVal += CreateInteractionDatatype(tp[0], ownerPackage) + ",";
                }

                retVal = retVal.Substring(0, retVal.Length - 1);
                retVal += ">";
            }
            return retVal;
        }

        /// TODO: Explanation of parameters missing: tr
        ///       Description of the value returned missing
        /// <summary>
        /// Gather all of the using statements
        /// </summary>
        private List<string> GatherUsings(TypeReference tr)
        {
            List<String> retVal = new List<string>();
            retVal.Add(tr.Class.ContainerName);
            foreach (TypeReference tp in tr.GenericSupplier ?? new List<TypeReference>())
                retVal.AddRange(GatherUsings(tp));
            return retVal;
        }

        /// TODO: Explanation of parameters missing: f and FilePath
        ///       Description of the value returned missing
        /// <summary>
        /// Create interaction
        /// </summary>
        public string CreateFile(Feature f, string FilePath)
        {
            return Path.ChangeExtension(Path.Combine(Path.Combine(FilePath, "interaction"), f.Name), "java");
        }

        #endregion
    }
}
