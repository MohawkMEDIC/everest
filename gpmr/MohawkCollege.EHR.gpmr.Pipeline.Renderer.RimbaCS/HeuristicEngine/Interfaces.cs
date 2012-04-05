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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Renderer;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.HeuristicEngine
{
    /// <summary>
    /// This class is responsible for sniffing COR structures and determining which real API 
    /// interfaces that COR class does or will implement
    /// </summary>
    internal class Interfaces
    {
        private static Dictionary<Type, List<String>> interfaces = new Dictionary<Type, List<string>>();

        /// <summary>
        /// Initialize the interfaces
        /// </summary>
        /// <param name="apiNs"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
        public static void Initialize(String apiNs)
        {
            interfaces.Clear();
            // Try to scan the AppDomain for the API
            foreach(Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type t in a.GetTypes())
                {
                    if (t.IsInterface && t.Namespace.StartsWith(apiNs) &&
                        t.GetInterface(apiNs + ".Interfaces.IGraphable") != null)
                    {
                        List<String> properties = GetInterfaceProperties(t);
                        
                        // Add properties for the implementation interfaces
                        foreach (var ifc in t.GetInterfaces())
                            properties.AddRange(GetInterfaceProperties(ifc));

                        interfaces.Add(t, properties);
                    }
                }
        }

        /// <summary>
        /// Get the interface properties
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static List<string> GetInterfaceProperties(Type t)
        {

            List<String> properties = new List<string>();

            // Scan this type and get all properties
            foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
            {
                string pName = pi.PropertyType.Name;
                if (pName.Contains("`"))
                    pName = pName.Substring(0, pName.IndexOf("`"));
                properties.Add(pName + "." + pi.Name);
            }

            return properties;
        }


        /// TODO: Explanation of parameters missing: c
        ///       Description of the value returned missing
        /// <summary>
        /// Determine which interfaces this class implements
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public static List<String> MapInterfaces(Class c)
        {
            List<String> retVal = new List<string>();

            // Find which interfaces this class implements
            foreach(KeyValuePair<Type, List<String>> kv in interfaces)
            {
                bool matches = true;
                Enumeration supplierDomain = null;
                foreach (String s in kv.Value)
                {
                    Property p = c.Content.Find(o => (o is Property) &&
                        ((o as Property).Type.Name != null && (o as Property).Type.Name.ToLower() + "." + o.Name.ToLower() == s.ToLower()) &&
                        (o.MaxOccurs == "1" || o.MaxOccurs != "1" && s.StartsWith("LIST"))) as Property;
                    matches &= p != null;
                    if(matches && p.SupplierStrength == Property.CodingStrengthKind.CodedNoExtensions)
                        supplierDomain = p.SupplierDomain;
                }

                // Determine if match was found
                if (matches)
                {
                    if (kv.Key.FullName.Contains("`"))
                    {
                        string rv = kv.Key.FullName.Substring(0, kv.Key.FullName.IndexOf("`"));
                        if (supplierDomain != null &&
                            !String.IsNullOrEmpty((EnumerationRenderer.WillRender(supplierDomain))))
                        {
                            if (retVal.Contains(rv))
                                retVal.Remove(rv);

                            rv += "<" + EnumerationRenderer.WillRender(supplierDomain) + ">";
                            retVal.Add(rv);
                        }
                    }
                    else
                        retVal.Add(kv.Key.FullName);
                }
            }

            return retVal;
        }
    }
}