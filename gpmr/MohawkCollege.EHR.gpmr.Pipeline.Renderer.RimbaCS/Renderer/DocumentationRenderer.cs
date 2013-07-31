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
using System.IO;
using System.Text.RegularExpressions;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Renderer
{
    /// <summary>
    /// Helper class for rendering documentation
    /// </summary>
    internal static class DocumentationRenderer
    {
        /// <summary>
        /// Render COR documentation to C# documentation
        /// </summary>
        /// <param name="doc">The documentation to render</param>
        /// <param name="tabLevel">The indentation level</param>
        /// <returns>The rendered c# comments</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor")]
        internal static string Render(Documentation doc, int tabLevel)
        {
            StringWriter sw = new StringWriter();

            if (doc == null ) return "";
            
            #region Description

            if (RimbaCsRenderer.SuppressDoc)
                return string.Empty;

            string tabPrefix = new String('\t', tabLevel);

            if (doc.Definition != null || doc.Description != null)
            {

                sw.WriteLine(tabPrefix + "/// <summary>");

                if (!RimbaCsRenderer.SuppressDoc)
                {
                    List<String> defn = new List<string>();
                    defn.AddRange(doc.Definition ?? new List<String>());
                    defn.AddRange(doc.Description ?? new List<String>());
                    foreach (String s in defn)
                    {
                        sw.WriteLine(tabPrefix + "/// ");
                        foreach (String line in s.Split('\n'))
                            sw.WriteLine(tabPrefix + "/// {0}", line.Replace("\r", "").Replace("&", "&amp;"));
                        sw.WriteLine(tabPrefix + "/// ");

                    }
                }
                sw.WriteLine(tabPrefix + "/// </summary>");
            }
            #endregion
            
            #region Walkthrough = Example

            if (doc.Walkthrough != null || doc.Usage != null)
            {
                if (!RimbaCsRenderer.SuppressDoc)
                {
                    sw.WriteLine(tabPrefix + "/// <example>");
                    sw.WriteLine(tabPrefix + "/// <para><b>Walkthrough</b></para>");
                    foreach (string s in doc.Walkthrough ?? new List<String>())
                        foreach (String line in s.Split('\n'))
                            sw.WriteLine(tabPrefix + "/// {0}", line.Replace("\r", "").Replace("&", "&amp;"));
                    sw.WriteLine(tabPrefix + "/// <para><b>Usage Notes</b></para>");
                    foreach (string s in doc.Usage ?? new List<String>())
                        foreach (String line in s.Split('\n'))
                            sw.WriteLine(tabPrefix + "/// {0}", line.Replace("\r", "").Replace("&", "&amp;"));
                    sw.WriteLine(tabPrefix + "/// </example>");
                }
            }

            #endregion

            #region Remarks
            if (doc.Rationale != null)
            {
                if (!RimbaCsRenderer.SuppressDoc)
                {
                    sw.WriteLine(tabPrefix + "/// <remarks>");
                    foreach (String s in doc.Rationale)
                    {
                        foreach (String line in s.Split('\n'))
                            sw.WriteLine(tabPrefix + "/// {0}", line.Replace("\r", "").Replace("&", "&amp;"));
                    }
                    sw.WriteLine(tabPrefix + "/// {0}", doc.Copyright);
                    sw.WriteLine(tabPrefix + "/// </remarks>");
                }
            }
            #endregion

            return sw.ToString();
        }
    }
}