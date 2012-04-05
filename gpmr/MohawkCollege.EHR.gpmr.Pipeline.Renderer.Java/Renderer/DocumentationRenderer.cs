using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer
{
    /// <summary>
    /// Represents a renderer helper that creates JavaDoc from COR documentation
    /// </summary>
    public class DocumentationRenderer
    {
        /// <summary>
        /// Render COR documentation to JavaDoc
        /// </summary>
        /// <param name="doc">The documentation to render</param>
        /// <param name="tabLevel">The indentation level</param>
        /// <returns>The rendered JavaDoc comments</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor")]
        internal static string Render(Documentation doc, int tabLevel)
        {
            StringWriter sw = new StringWriter();

            if (doc == null) return "";

            #region Description

            string tabPrefix = new String('\t', tabLevel);
            sw.WriteLine(tabPrefix + "/** ");

            if (doc.Definition != null || doc.Description != null)
            {

                sw.WriteLine(tabPrefix + " * Summary:");

                List<String> defn = new List<string>();
                defn.AddRange(doc.Definition ?? new List<String>());
                defn.AddRange(doc.Description ?? new List<String>());
                foreach (String s in defn)
                {
                    sw.WriteLine(tabPrefix + " * ");
                    foreach (String line in s.Split('\n'))
                        sw.WriteLine(tabPrefix + " * {0}", line.Replace("\r", "").Replace("&", "&amp;"));
                    sw.WriteLine(tabPrefix + " * ");

                }

            }
            #endregion

            #region Walkthrough = Example

            if (doc.Walkthrough != null || doc.Usage != null)
            {
                sw.WriteLine(tabPrefix + " * Example:");
                sw.WriteLine(tabPrefix + " * Walkthrough:");
                foreach (string s in doc.Walkthrough ?? new List<String>())
                    foreach (String line in s.Split('\n'))
                        sw.WriteLine(tabPrefix + " * {0}", line.Replace("\r", "").Replace("&", "&amp;"));
                sw.WriteLine(tabPrefix + " * Usage Notes:");
                foreach (string s in doc.Usage ?? new List<String>())
                    foreach (String line in s.Split('\n'))
                        sw.WriteLine(tabPrefix + " * {0}", line.Replace("\r", "").Replace("&", "&amp;"));
            }

            #endregion

            #region Remarks
            if (doc.Rationale != null)
            {
                sw.WriteLine(tabPrefix + " * Remarks:");
                foreach (String s in doc.Rationale)
                {
                    foreach (String line in s.Split('\n'))
                        sw.WriteLine(tabPrefix + " * {0}", line.Replace("\r", "").Replace("&", "&amp;"));
                }
                sw.WriteLine(tabPrefix + " * {0}", doc.Copyright);
            }
            #endregion

            sw.WriteLine(tabPrefix + " */");

            return sw.ToString();
        }
    }
}
