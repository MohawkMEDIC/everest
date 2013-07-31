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
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Attributes;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Interfaces;
using System.IO;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.HeuristicEngine;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Renderer
{
    /// <summary>
    /// Summary of InteractionRenderer
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Renderer"), FeatureRenderer(Feature = typeof(Interaction), IsFile = true)]
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

            Dictionary<string, string[]> cfm = ClassRenderer.CreateFactoryMethod(tr, "retVal", true);


            if (tr.Class != null && tr.Class.IsAbstract)
            {
                foreach (var chld in tr.Class.SpecializedBy)
                        sw.WriteLine(GenerateFactoryMethod(chld, ownerNs));
            }
            else
            {

                string factoryName = String.Format("Create{0}", tr.Class.Name), tfact = factoryName;
                if (generatedFactoryMethods.Contains(factoryName))
                    factoryName = String.Format("Create{0}", Util.Util.MakeFriendly(tr.Name));
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
                    sw.WriteLine("\t\t/// <summary> Create a new instance of {0} to populate {1} </summary>", tr.Name, tr is TypeParameter ? (tr as TypeParameter).ParameterName : "");
                    sw.Write(kv.Value[2]);
                    sw.WriteLine("\t\tpublic static {0} {1} ({2}) {{ \r\n\t\t\t{0} retVal = new {0}();\r\n{3}", ClassRenderer.CreateDatatypeRef(tr, new Property()), factoryName,
                        kv.Value[0].Substring(0, kv.Value[0].Length - 1), kv.Value[1]);
                    sw.WriteLine("\t\t\treturn retVal;\r\n\t\t}");

                    if (!wroteParms.Contains(""))
                    {
                        wroteParms.Add("");
                        sw.WriteLine("\t\t/// <summary> Create a new instance of {0} to populate {1} </summary>", tr.Name, tr.Class.Name);
                        sw.WriteLine("\t\tpublic static {0} {1} () {{ \r\n\t\t\treturn new {0}();\r\n\t\t}}", ClassRenderer.CreateDatatypeRef(tr, new Property()), factoryName);
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
        public void Render(string OwnerNS, string apiNs, Feature f, System.IO.TextWriter tw)
        {
            this.generatedFactoryMethods = new List<string>();
            // Get a strongly typed reference to the 
            Interaction interaction = f.Clone() as Interaction;
            interaction.MemberOf = f.MemberOf;

            StringWriter sw = new StringWriter();

            #region Usings

            // Validate Usings
            string[] usings = new string[] { "Attributes", "Interfaces", "DataTypes", "DataTypes.Primitives" };
            // API usings
            foreach (string s in usings)
                sw.WriteLine("using {1}.{0};", s, apiNs);

            // Owner Usings
            foreach (String s in GatherUsings(interaction.MessageType))
                sw.WriteLine("using {0}.{1};", OwnerNS, s);

            if(f.MemberOf.Find(o=>o is Enumeration) != null)
                sw.WriteLine("using {0}.Vocabulary;", OwnerNS);
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

            // Determine if the interaction is an IInteraction
            bool isIInteraction = true;
            string[] members = {
                                   "creationtime",
                                   "versioncode",
                                   "interactionid",
                                   "processingmodecode"
                               };
            foreach (var m in members)
                isIInteraction &= interaction.MessageType.Class.Content.Exists(o => o.Name.ToLower() == m);


            sw.WriteLine("namespace {0}.Interactions {{\r\n", OwnerNS);
            var vLength = sw.ToString().Length;
            sw.Write(DocumentationRenderer.Render(interaction.Documentation, 1));
            if (sw.ToString().Length == vLength)
                sw.WriteLine("\t/// <summary>{0}</summary>", interaction.BusinessName != null ? interaction.BusinessName.Replace("\n", "").Replace("\r", "") : interaction.Name);
            sw.WriteLine("\t[Structure(Name = \"{0}\", StructureType = StructureAttribute.StructureAttributeType.Interaction)]", interaction.Name);
            sw.WriteLine("\t[Interaction(TriggerEvent = \"{0}\")]", interaction.TriggerEvent);
            sw.WriteLine("\t#if !WINDOWS_PHONE");
            sw.WriteLine("\t[Serializable]");
            sw.WriteLine("\t#endif");
            sw.WriteLine("\t[System.CodeDom.Compiler.GeneratedCode(\"gpmr\",\"{0}\")]", Assembly.GetEntryAssembly().GetName().Version.ToString());
            foreach (Interaction response in interaction.Responses)
                sw.WriteLine("\t[InteractionResponse(Name = \"{0}\", TriggerEvent = \"{1}\")]", response.Name, response.TriggerEvent);
            sw.WriteLine("\t[System.ComponentModel.Description(\"{0}\")]", interaction.BusinessName != null ? interaction.BusinessName.Replace("\n", "").Replace("\r", "") : interaction.Name);
            sw.WriteLine("\tpublic class {0} : {1}{2} {{", interaction.Name, CreateInteractionDatatype(interaction.MessageType), isIInteraction ? ", MARC.Everest.Interfaces.IInteraction" : "");

            #region Constants

            // Get the trigger event
            sw.WriteLine("\t\t/// <summary> Gets the default trigger event to be used for this interaction </summary>");
            sw.WriteLine("\t\tpublic static CV<String> GetTriggerEvent() {{ return new CV<String>(\"{0}\", \"{1}\"); }}", interaction.TriggerEvent, triggerEventOid);

            // Get the interaction id
            sw.WriteLine("\t\t/// <summary> Gets the interaction ID of this interaction </summary>");
            sw.WriteLine("\t\tpublic static II GetInteractionId() {{ return new II(new OID(\"{1}\"), \"{0}\"); }}", interaction.Name, interactionIdOid);

            // Get the profile id
            if (!String.IsNullOrEmpty(profileId))
            {
                sw.WriteLine("\t\t/// <summary> Gets the profile id of this interaction </summary>");
                sw.WriteLine("\t\tpublic static LIST<II> GetProfileId() {{ return new LIST<II>() {{ new II (new OID(\"{0}\"), \"{1}\") }}; }}", profileIdOid, profileId);
            }


            #endregion

            #region Constructors

            sw.WriteLine("\t\t/// <summary> Creates a new, empty instance of {0} </summary>", interaction.Name);
            sw.WriteLine("\t\tpublic {0}() : base() {{ }}\r\n", interaction.Name);

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

            Dictionary<String, String[]> ctors = ClassRenderer.CreateFactoryMethod(interaction.MessageType, "this", true);

            // Write CTOR
            List<String> wroteParms = new List<string>(); // Keep track of the parameters used
            foreach (KeyValuePair<String, String[]> kv in ctors)
            {
                if (kv.Value[0].Length > 0 && !wroteParms.Contains(kv.Value[0]))
                {
                    wroteParms.Add(kv.Value[0]);
                    sw.WriteLine("\t\t/// <summary>\r\n\t\t/// CTOR for all {0} elements\r\n\t\t/// </summary>", kv.Key);
                    sw.WriteLine(kv.Value[2]);
                    sw.WriteLine("\t\tpublic {0}({1}) : base() {{ \r\n\t\t{2}\t\t}}", interaction.Name, kv.Value[0].Substring(0, kv.Value[0].Length - 1), kv.Value[1]);
                }
            }
            #endregion

            #region Creator for payload, control act, etc...

            // Owner namespace
            if(interaction.MessageType.GenericSupplier != null)
                foreach (TypeReference tr in interaction.MessageType.GenericSupplier)
                    sw.WriteLine(GenerateFactoryMethod(tr, OwnerNS));




            if (isIInteraction)
            {
                // Version code
                sw.WriteLine("\t\t/// <summary>Implementation of version code</summary>");
                sw.WriteLine("\t\tMARC.Everest.DataTypes.Interfaces.ICodedSimple MARC.Everest.Interfaces.IInteraction.VersionCode { get { return this.VersionCode; } }");

                // Version code
                sw.WriteLine("\t\t/// <summary>Implementation of processing mode code</summary>");
                sw.WriteLine("\t\tMARC.Everest.DataTypes.Interfaces.ICodedSimple MARC.Everest.Interfaces.IInteraction.ProcessingModeCode { get { return this.ProcessingModeCode; } }");

                sw.WriteLine("\t\t/// <summary>Implementation of generic IInteraction.ControlActEvent Property</summary>");
                // Create the control act event
                if (interaction.MessageType.GenericSupplier != null && interaction.MessageType.GenericSupplier.Count > 0)
                {
                    Property cactProperty = interaction.MessageType.Class.Content.Find(o => o is Property && (o as Property).Type.Name == interaction.MessageType.Class.TypeParameters[0].ParameterName) as Property;
                    if (cactProperty == null)
                        ;

                    string cactName = cactProperty.Type.Name == interaction.MessageType.Class.TypeParameters[0].ParameterName ? Util.Util.MakeFriendly(cactProperty.Name) : Util.Util.PascalCase(cactProperty.Name);
                    sw.WriteLine("\t\tSystem.Object MARC.Everest.Interfaces.IInteraction.ControlAct {");
                    sw.WriteLine("\t\t\tget {{ return this.{0}; }}", cactName);
                    sw.WriteLine("\t\t\tset {{ this.{1} = value as {0}; }}", CreateInteractionDatatype(interaction.MessageType.GenericSupplier[0]), cactName);
                    sw.WriteLine("\t\t}");
                }
                else
                {
                    sw.WriteLine("\t\tSystem.Object MARC.Everest.Interfaces.IInteraction.ControlAct {");
                    sw.WriteLine("\t\t\tget { return null; }");
                    sw.WriteLine("\t\t\tset { ; }");
                    sw.WriteLine("\t\t}");
                }
            }
            #endregion

            sw.WriteLine("\t}");
            sw.WriteLine("}");
            tw.WriteLine(sw);

        }

        /// TODO: Explanation of parameters missing: typeReference
        ///       Description of the value returned missing
        /// <summary>
        /// Create the interaction inheritence for the specified type reference
        /// </summary>
        private string CreateInteractionDatatype(TypeReference typeReference)
        {
            if (typeReference.CoreDatatypeName != null)
                return typeReference.CoreDatatypeName;

            string retVal = Util.Util.PascalCase(typeReference.Class.Name);

            if(typeReference.Class.TypeParameters != null && typeReference.Class.TypeParameters.Count > 0)
            {
                retVal += "<";
                foreach (var tr in typeReference.Class.TypeParameters)
                {
                    List<TypeReference> tp = typeReference.GenericSupplier == null ? null : typeReference.GenericSupplier.FindAll(o=>(o as TypeParameter).ParameterName.Equals(tr.ParameterName));
                    if (tp == null || tp.Count != 1) // nothing is bound
                    {
                        var objectReference = new TypeReference() { Name = "System.Object" };
                        retVal += CreateInteractionDatatype(objectReference) + ",";
                    }
                    else
                        retVal += CreateInteractionDatatype(tp[0]) + ",";
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
            return Path.ChangeExtension(Path.Combine(Path.Combine(FilePath, "Interaction"), f.Name), "cs");
        }

        #endregion
    }
}