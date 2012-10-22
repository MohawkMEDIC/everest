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
using System.Reflection;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer;
using System.IO;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.HeuristicEngine
{
    /// <summary>
    /// This class is responsible for sniffing COR structures and determining which real API 
    /// interfaces that COR class does or will implement
    /// </summary>
    internal class Interfaces
    {

        // Heuristic data
        private static HeuristicData s_heuristicData;
        // API namespace
        private static string s_apiNs = "ca.marc.everest";

        /// <summary>
        /// Initialize the interfaces
        /// </summary>
        /// <param name="apiNs"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
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
                if (fs != null)
                    fs.Close();

            }
            s_apiNs = apiNs;

        }

        /// TODO: Explanation of parameters missing: c
        ///       Description of the value returned missing
        /// <summary>
        /// Determine which interfaces this class implements
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public static List<String> MapInterfaces(Class c, string ownerNs)
        {
            List<String> retVal = new List<string>();

            // Find which interfaces this class implements
            foreach(var iface in s_heuristicData.Interfaces)
            {
                bool matches = true;
                Enumeration supplierDomain = null;
                foreach (var prop in iface.Properties)
                {
                    Property p = c.Content.Find(o => (o is Property) && (o as Property).Type.Name != null &&
                        (o as Property).Type.Name.ToLower() + "." + o.Name.ToLower() == prop.DataType.ToLower() + "." + prop.Name.ToLower() &&
                        (o.MaxOccurs == "1" || o.MaxOccurs != "1" && prop.Name.StartsWith("LIST"))) as Property;
                    matches &= p != null;
                    if(matches)
                        supplierDomain = p.SupplierDomain;
                }

                // Determine if match was found
                if (matches)
                {
                    if (!String.IsNullOrEmpty(iface.GenericParameter))
                    {
                        string rv = iface.Name;
                        if (supplierDomain != null && 
                            !String.IsNullOrEmpty(EnumerationRenderer.WillRender(supplierDomain)))
                        {
                            if (retVal.Contains(rv))
                                retVal.Remove(rv);

                            rv += String.Format("<{1}>", ownerNs, EnumerationRenderer.WillRender(supplierDomain));
                            retVal.Add(rv);
                        }
                    }
                    else
                        retVal.Add(iface.Name);
                }
            }

            return retVal;
        }
    }
}