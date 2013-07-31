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
using MohawkCollege.EHR.gpmr.COR;
using System.Reflection;
using MARC.Everest.Attributes;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.HeuristicEngine
{
    /// <summary>
    /// Used for sniffing datatypes in the API library and setting up maps
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class Datatypes
    {
        /// <summary>
        /// Identifies information related to default type parameters
        /// </summary>
        private class TypeParameterInformation
        {
            /// <summary>
            /// Get or sets the type the TPI points to
            /// </summary>
            public Type Type { get; set; }

            /// <summary>
            /// Gets the type's structure attribute
            /// </summary>
            public StructureAttribute StructureAttribute { get; set; }

            /// <summary>
            /// Gets or sets the default type parameter
            /// </summary>
            public Type DefaultTypeParameter
            {
                get
                {
                    return StructureAttribute.DefaultTemplateType;
                }
            }
        }

        // Flavor maps
        private static Dictionary<String, Type> flavMaps = new Dictionary<string, Type>();

        // Type maps
        private static Dictionary<String, Type> typeMaps = new Dictionary<string, Type>(10);

        // Type parameter info
        private static List<TypeParameterInformation> defaultTypeParms = new List<TypeParameterInformation>();

        // Collection Types
        private static List<String> collectionTypes = new List<string>(10);

        // Code Types
        private static List<String> codeTypes = new List<string>(10);

        // Builtin vocabulary
        private static Dictionary<String, String> builtinVocab = new Dictionary<string, string>();

        /// <summary>
        /// Get a builting vocabulary
        /// </summary>
        public static string GetBuiltinVocabulary(string codesetName)
        {
            string data = null;
            if (builtinVocab.TryGetValue(codesetName, out data)) ;
            return data;
        }

        /// <summary>
        /// Intialize
        /// </summary>
        /// <param name="apiNs">The Namespace of the API to scan</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static void Initialize(String apiNs)
        {

            defaultTypeParms.Clear();
            flavMaps.Clear();
            builtinVocab.Clear();
            codeTypes.Clear();
            collectionTypes.Clear();
            // Try to scan the AppDomain for the API
            foreach(Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type t in a.GetTypes())
                {
                    // Determine if the type implements IGraphable and is a structure
                    if (t.GetCustomAttributes(typeof(StructureAttribute), false).Length > 0 &&
                        t.FullName.StartsWith(apiNs))
                    {

                        // Get the structure and flavor map attributes
                        StructureAttribute sta = t.GetCustomAttributes(typeof(StructureAttribute), false)[0] as StructureAttribute;

                        // Setup the type parameter info
                        if (t.IsGenericTypeDefinition)
                            defaultTypeParms.Add(new TypeParameterInformation()
                            {
                                Type = t,
                                StructureAttribute = sta
                            });

                        if (sta.StructureType == StructureAttribute.StructureAttributeType.CodeSystem ||
                            sta.StructureType == StructureAttribute.StructureAttributeType.ConceptDomain ||
                            sta.StructureType == StructureAttribute.StructureAttributeType.ValueSet)
                            builtinVocab.Add(sta.Name, t.FullName);

                        if (sta.StructureType == StructureAttribute.StructureAttributeType.DataType &&
                            t.GetProperty("Code") != null &&
                            t.IsGenericTypeDefinition)
                            codeTypes.Add(sta.Name);
                        else if (sta.StructureType == StructureAttribute.StructureAttributeType.DataType &&
                            t.GetMethod("Add") != null &&
                            t.IsGenericTypeDefinition)
                            collectionTypes.Add(sta.Name);

                        Object[] flv = t.GetCustomAttributes(typeof(FlavorMapAttribute), false);

                        // Process the STA
                        if (flavMaps.ContainsKey(sta.Name ?? Guid.NewGuid().ToString()))
                            throw new InvalidOperationException(String.Format("Can't add duplicate datatype '{0}' to type maps", sta.Name));

                        flavMaps.Add(sta.Name ?? Guid.NewGuid().ToString(), t); // Map Structure Name to the type

                        // Process the flavor maps
                        foreach (FlavorMapAttribute fma in flv)
                        {
                            if (flavMaps.ContainsKey(String.Format("{0}.{1}", sta.Name, fma.FlavorId)))
                                throw new InvalidOperationException(String.Format("Can't add duplicate flavor map '{0}' to type maps", fma.FlavorId));
                            flavMaps.Add(String.Format("{0}.{1}", sta.Name, fma.FlavorId), fma.Implementer); // Map Structure Name to the type
                        }

                        
                    }

                    var ttypeMapes = t.GetCustomAttributes(typeof(TypeMapAttribute), false);
                    foreach (TypeMapAttribute tma in ttypeMapes)
                    {
                        if (typeMaps.ContainsKey(tma.Name))
                            throw new InvalidOperationException("Can't add duplicate type map");
                        typeMaps.Add(String.Format("{0}#{1}", tma.Name, tma.ArgumentType), t);
                    }
                }
            ;
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

        /// <summary>
        /// Returns true if the specified <paramref name="tr"/> is
        /// a code type
        /// </summary>
        public static bool IsCodeType(TypeReference tr)
        {
            string trCoreName = tr.CoreDatatypeName;
            if (trCoreName == null)
                return false;
            return codeTypes.Exists(o => trCoreName.StartsWith(o));
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
            TypeReference retVal = t.Clone() as TypeReference;
            retVal.MemberOf = m;
            retVal.Container = g;

            if (t is TypeParameter)
                return t;
            else if (t.Name == null)
                retVal.Name = "System.Object";
            else if (flavMaps.ContainsKey(t.Name + "." + t.Flavor))
            {
                // Create a type reference
                Type type = flavMaps[t.Name + "." + t.Flavor];
                retVal.Name = type.Name;
                if (type.IsInterface)
                    retVal.Name = type.FullName;

                if (retVal.Name.Contains("`"))
                    retVal.Name = retVal.Name.Substring(0, retVal.Name.IndexOf("`"));
                retVal.MemberOf = t.MemberOf;
                retVal.GenericSupplier = t.GenericSupplier;

                if (t.Name != retVal.Name)
                    retVal.Flavor = null; // Clear flavors if pointing to a new type
            }
            else if (typeMaps.ContainsKey(String.Format("{0}#{1}", t.Name, t.GenericSupplier != null && t.GenericSupplier.Count > 0 ? t.GenericSupplier[0].Name : null))) // Maps types 
            {
                Type type = typeMaps[String.Format("{0}#{1}", t.Name, t.GenericSupplier != null && t.GenericSupplier.Count > 0 ? t.GenericSupplier[0].Name : null)];
                retVal.Name = type.Name;
                if (type.IsInterface)
                    retVal.Name = type.FullName;
                if (retVal.Name.Contains("`"))
                    retVal.Name = retVal.Name.Substring(0, retVal.Name.IndexOf("`"));
                retVal.MemberOf = t.MemberOf;
                retVal.GenericSupplier = t.GenericSupplier;

            }
            else if(typeMaps.ContainsKey(String.Format("{0}#", t.Name))) // Maps types 
            {
                Type type = typeMaps[String.Format("{0}#", t.Name)];
                retVal.Name = type.Name;
                if (type.IsInterface)
                    retVal.Name = type.FullName;
                if (retVal.Name.Contains("`"))
                    retVal.Name = retVal.Name.Substring(0, retVal.Name.IndexOf("`"));
                retVal.MemberOf = t.MemberOf;
                retVal.GenericSupplier = t.GenericSupplier;

            }

            // Type parm info
            var tParm = defaultTypeParms.Find(o => o.StructureAttribute.Name.Equals(t.Name));
            if (tParm != null && (t.GenericSupplier == null || t.GenericSupplier.Count == 0) && tParm.DefaultTypeParameter != null)
            {
                // Create the type parm
                var staParm = tParm.DefaultTypeParameter.GetCustomAttributes(typeof(StructureAttribute), false);
                for(int i = 0; i < tParm.Type.GetGenericArguments().Length; i++)
                    if (staParm.Length > 0)
                        retVal.AddGenericSupplier(i.ToString(), MapDatatype(new TypeReference() { Name = (staParm[0] as StructureAttribute).Name }));
                    else
                        retVal.AddGenericSupplier(i.ToString(), new TypeReference() { Name = tParm.DefaultTypeParameter.FullName });
            }
            
            // Default member?
            return retVal;
        }
    }
}