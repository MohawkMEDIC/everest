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
 * 
 * User: Justin Fyfe
 * Date: 07-12-2011
 */
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.HeuristicEngine
{
    /// <summary>
    /// Used for sniffing datatypes in the API library and setting up maps
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class Datatypes
    {

        // Heuristic data
        private static HeuristicData s_heuristicData;
        // API namespace
        private static string s_apiNs = "ca.marc.everest";
        private static List<String> collectionTypes = new List<string>(new String[]{
            "LIST",
            "HIST",
            "SET",
            "BAG"
                                                      });
        /// <summary>
        /// Intialize
        /// </summary>
        /// <param name="apiNs">The Namespace of the API to scan</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static void Initialize(String apiNs, String heuristicFile)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(heuristicFile);
                XmlSerializer xsz = new XmlSerializer(typeof(HeuristicData));
                s_heuristicData = xsz.Deserialize(fs) as HeuristicData;
            }
            finally
            {
                if(fs != null)
                    fs.Close();

            }
            s_apiNs = apiNs;

        }

        /// <summary>
        /// Map datatype <paramref name="t"/> to match the internal datatype
        /// </summary>
        /// <param name="t">The type to match</param>
        /// <returns>The mapped type reference</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
        public static TypeReference MapDatatype(TypeReference t)
        {
            var m = t.MemberOf;
            var g = t.Container;
            t = t.Clone() as TypeReference;
            t.MemberOf = m;
            t.Container = g;
            TypeReference retVal = t.Clone() as TypeReference;

            // Create a type reference
            var type = s_heuristicData.Datatypes.Find(o => o.MifDatatype == t.Name + (t.Flavor != null ? "." + t.Flavor : ""));

            if(type == null && !String.IsNullOrEmpty(t.Flavor ))
                type = s_heuristicData.Datatypes.Find(o => o.MifDatatype == t.Name);

            if (t is TypeParameter)
                return t;
            else if (t.Name == null)
                retVal.Name = "java.lang.Object";
            else if (type != null)
            {
                retVal.Name = type.JavaType;
                //if (!String.IsNullOrEmpty(type.TemplateParameter))
                //    retVal.Name = String.Format("{0}.datatypes.generic.{1}", s_apiNs, retVal.Name);
                //else
                //    retVal.Name = String.Format("{0}.datatypes.{1}", s_apiNs, retVal.Name);

                retVal.MemberOf = t.MemberOf;
                retVal.GenericSupplier = t.GenericSupplier;

                if (t.Name != retVal.Name)
                    retVal.Flavor = null; // Clear flavors if pointing to a new type

                // Default binding information
                var tParm = type.DefaultBind;
                if ((t.GenericSupplier == null || t.GenericSupplier.Count == 0) && !String.IsNullOrEmpty(tParm))
                {
                    retVal.AddGenericSupplier("def", MapDatatype(new TypeReference() { Name = tParm }));
                }

            }
            // Default member?
            return retVal;

        }

        /// <summary>
        /// Get a builting vocabulary
        /// </summary>
        public static string GetBuiltinVocabulary(string codesetName)
        {
            var data = s_heuristicData.Vocabulary.Find(o => o.MifDatatype == codesetName);
            if (data != null)
                return data.JavaType;
            return null;
        }

        /// <summary>
        /// Get overridden setters
        /// </summary>
        public static SetterOverrideData[] GetOverrideSetters(TypeReference tr, Property p, string ownerPackage)
        {
            // Not a datatype so we won't need overrides
            if(tr.Class != null)
                return new SetterOverrideData[0];

            // Get all the setter overrides
            var dataType = s_heuristicData.Datatypes.Find(o => o.MifDatatype == tr.Name);

            // Sanity check
            if(dataType == null || dataType.SetterOverride.Count == 0) return new SetterOverrideData[0];

            // Set generic paramaters
            Dictionary<String, String> bind = new Dictionary<string, string>();
            string fillParameter = dataType.TemplateParameter;
            if (!String.IsNullOrEmpty(dataType.TemplateParameter))
            {
                int i = 0;
                foreach (string s in dataType.TemplateParameter.Split(','))
                {

                    TypeReference bindTypeRef = null;
                    if (tr.GenericSupplier != null) // Generic Supplier
                    {
                        bindTypeRef = tr.GenericSupplier.Find(o => o is TypeParameter && (o as TypeParameter).ParameterName == s);
                        if (bindTypeRef == null)
                            bindTypeRef = tr.GenericSupplier[i++];
                    }
                    else if (p != null && p.SupplierDomain != null && !String.IsNullOrEmpty(EnumerationRenderer.WillRender(p.SupplierDomain)))
                        bindTypeRef = new TypeReference() { Name = String.Format("{0}", EnumerationRenderer.WillRender(p.SupplierDomain)) };
                    else
                        bindTypeRef = new TypeReference() { Name = dataType.DefaultBind };

                    // Add Binding
                    bind.Add(s, ClassRenderer.CreateDatatypeRef(bindTypeRef ?? new TypeReference() { Name = null }, null, ownerPackage));
                    fillParameter = fillParameter.Replace(s, bind[s]);
                }
            }

            // Create setter override and return
            List<SetterOverrideData> overrides = new List<SetterOverrideData>(dataType.SetterOverride.Count);
            foreach (var sod in dataType.SetterOverride)
            {
                // Correct parameters
                SetterOverrideData templatedSod = new SetterOverrideData();
                templatedSod.Parameters = new List<PropertyInfoData>();
                foreach (var parameterData in sod.Parameters)
                {
                    string dt = String.Empty;
                    if (!bind.TryGetValue(parameterData.DataType, out dt))
                        dt = parameterData.DataType;
                    
                    templatedSod.Parameters.Add(new PropertyInfoData() { Name = parameterData.Name, DataType = dt.Replace(string.Format("<{0}>", dataType.TemplateParameter), String.Format("<{0}>", fillParameter)) });
                }

                // Correct Body
                templatedSod.Throws = new List<ThrowsData>(sod.Throws);
                templatedSod.SetterText = sod.SetterText.Replace(string.Format("<{0}>", dataType.TemplateParameter), String.Format("<{0}>", fillParameter));
                templatedSod.ValueInstance = sod.ValueInstance;
                overrides.Add(templatedSod);
            }

            return overrides.ToArray();
        }


        /// <summary>
        /// Returns true if the specified <paramref name="tr"/> is
        /// a collection
        /// </summary>
        public static bool IsCollectionType(TypeReference tr)
        {
            string trCoreName = tr.CoreDatatypeName;
            if (trCoreName == null)
                return false;
            return collectionTypes.Exists(o => trCoreName.StartsWith(o));
        }
    }
}