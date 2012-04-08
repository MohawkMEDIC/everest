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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using System.Xml;
using System.Collections.Specialized;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// Parses a mif 2.0 documentation structure into a COR documentation structure
    /// </summary>
    internal static class DocumentationParser
    {
        /// <summary>
        /// Parse a document object from an annotation
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        internal static MohawkCollege.EHR.gpmr.COR.Documentation Parse(MohawkCollege.EHR.HL7v3.MIF.MIF20.Documentation annot)
        {
            return ParseAddPrefix(String.Empty, annot);
        }

        /// <summary>
        /// Cleans the HTML
        /// </summary>
        private static XmlElement CleanHtml(XmlNode xel)
        {

            XmlDocument retVal = new XmlDocument();
            if (xel is XmlText)
            {
                retVal.AppendChild(retVal.CreateElement("p"));
                retVal.DocumentElement.AppendChild(retVal.CreateTextNode(xel.Value));
                return retVal.DocumentElement;
            }
            else
                retVal.AppendChild(retVal.CreateElement(xel.LocalName));
            foreach (XmlNode itm in xel.ChildNodes)
            {
                if (itm is XmlText)
                    retVal.DocumentElement.AppendChild(retVal.CreateTextNode(itm.Value));
                else if (itm is XmlAttribute)
                    retVal.DocumentElement.AppendChild(retVal.ImportNode(itm, true));
                else if (itm is XmlElement)
                    retVal.DocumentElement.AppendChild(retVal.ImportNode(CleanHtml(itm as XmlElement), true));
            }

            return retVal.DocumentElement;
        }


        /// <summary>
        /// Parse the documentation and add a prefix
        /// </summary>
        internal static MohawkCollege.EHR.gpmr.COR.Documentation ParseAddPrefix(string prefix, Documentation annot)
        {
            MohawkCollege.EHR.gpmr.COR.Documentation documentation = new MohawkCollege.EHR.gpmr.COR.Documentation();
            Dictionary<String, StringCollection> parameters = MifCompiler.hostContext.Data["CommandParameters"] as Dictionary<String, StringCollection>;

            if (annot == null) return documentation;

            // Append documentation
            #region Definition
            if (annot.Definition != null && annot.Definition.Count > 0)
            {
                documentation.Definition = new List<string>();
                if (!String.IsNullOrEmpty(prefix))
                    documentation.Definition.Add(String.Format("<em>{0}</em>", prefix));
                annot.Definition.Sort(ComplexAnnotation.Comparator);
                foreach (ComplexAnnotation ca in annot.Definition)
                    foreach (ComplexMarkupWithLanguage cmwl in ca.Text)
                        if (cmwl.Language == MifCompiler.Language || cmwl.Language == null)
                        {
                            foreach (XmlElement xel in cmwl.MarkupElements ?? new List<XmlElement>().ToArray())
                                documentation.Definition.Add(CleanHtml(xel).OuterXml); // Clean mif doc data from docs
                            if (cmwl.MarkupText != null) documentation.Definition.Add(cmwl.MarkupText);
                        }
            }
            #endregion
            #region Description
            if (annot.Description != null && annot.Description.Count > 0)
            {
                documentation.Description = new List<string>();
                if (!String.IsNullOrEmpty(prefix))
                    documentation.Description.Add(String.Format("<em>{0}</em>", prefix));

                annot.Description.Sort(ComplexAnnotation.Comparator);
                foreach (ComplexAnnotation ca in annot.Description)
                    foreach (ComplexMarkupWithLanguage cmwl in ca.Text)
                        if (cmwl.Language == MifCompiler.Language || cmwl.Language == null)
                        {
                            foreach (XmlElement xel in cmwl.MarkupElements ?? new List<XmlElement>().ToArray())
                                documentation.Description.Add(CleanHtml(xel).OuterXml);
                            if (cmwl.MarkupText != null) documentation.Description.Add(cmwl.MarkupText);
                        }
            }
            #endregion
            #region Rationale
            if (annot.Rationale != null && annot.Rationale.Count > 0)
            {
                documentation.Rationale = new List<string>();
                if (!String.IsNullOrEmpty(prefix))
                    documentation.Rationale.Add(String.Format("<em>{0}</em>", prefix));
                annot.Rationale.Sort(ComplexAnnotation.Comparator);
                foreach (ComplexAnnotation ca in annot.Rationale)
                    foreach (ComplexMarkupWithLanguage cmwl in ca.Text)
                        if (cmwl.Language == MifCompiler.Language || cmwl.Language == null)
                        {
                            foreach (XmlElement xel in cmwl.MarkupElements ?? new List<XmlElement>().ToArray())
                                documentation.Rationale.Add(CleanHtml(xel).OuterXml);
                            if (cmwl.MarkupText != null) documentation.Rationale.Add(cmwl.MarkupText);
                        }
            }
            #endregion
            #region Walkthrough
            if (annot.Walkthrough != null && annot.Walkthrough.Count > 0)
            {
                documentation.Walkthrough = new List<string>();
                if (!String.IsNullOrEmpty(prefix))
                    documentation.Walkthrough.Add(String.Format("<em>{0}</em>", prefix)); 
                annot.Walkthrough.Sort(ComplexAnnotation.Comparator);
                foreach (ComplexAnnotation ca in annot.Walkthrough)
                    foreach (ComplexMarkupWithLanguage cmwl in ca.Text)
                        if (cmwl.Language == MifCompiler.Language || cmwl.Language == null)
                        {
                            foreach (XmlElement xel in cmwl.MarkupElements ?? new List<XmlElement>().ToArray())
                                documentation.Walkthrough.Add(CleanHtml(xel).OuterXml);
                            if (cmwl.MarkupText != null) documentation.Walkthrough.Add(cmwl.MarkupText);
                        }
            }
            #endregion
            #region Usage
            if (annot.Usage != null && annot.Usage.Count > 0)
            {
                documentation.Usage = new List<string>();
                if (!String.IsNullOrEmpty(prefix))
                    documentation.Usage.Add(String.Format("<em>{0}</em>", prefix)); 
                annot.Usage.Sort(ComplexAnnotation.Comparator);
                foreach (ComplexAnnotation ca in annot.Usage)
                    foreach (ComplexMarkupWithLanguage cmwl in ca.Text)
                        if (cmwl.Language == MifCompiler.Language || cmwl.Language == null)
                        {
                            foreach (XmlElement xel in cmwl.MarkupElements ?? new List<XmlElement>().ToArray())
                                documentation.Usage.Add(CleanHtml(xel).OuterXml);
                            if (cmwl.MarkupText != null) documentation.Usage.Add(cmwl.MarkupText);
                        }
            }
            #endregion
            #region Appendicies
            if (annot.Appendix != null && annot.Appendix.Count > 0)
            {
                documentation.Appendix = new List<MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation>();
                foreach (AppendixAnnotation aa in annot.Appendix)
                {
                    MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation td = new MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation();
                    td.Name = aa.Name;
                    td.Title = aa.Title;
                    td.Text = new List<string>();
                    if (!String.IsNullOrEmpty(prefix))
                        td.Text.Add(String.Format("<em>{0}</em>", prefix)); 
                    foreach (ComplexMarkupWithLanguage cmwl in aa.Text)
                        if (cmwl.Language == MifCompiler.Language || cmwl.Language == null)
                        {
                            foreach (XmlElement xel in cmwl.MarkupElements ?? new List<XmlElement>().ToArray())
                                td.Text.Add(CleanHtml(xel).OuterXml);
                            if (cmwl.MarkupText != null) td.Text.Add(cmwl.MarkupText);
                        }
                    documentation.Appendix.Add(td);
                }
            }
            #endregion
            #region Other
            if (annot.OtherAnnotation != null && annot.OtherAnnotation.Count > 0)
            {
                documentation.Other = new List<MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation>();
                foreach (AppendixAnnotation aa in annot.OtherAnnotation)
                {
                    MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation td = new MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation();
                    td.Name = aa.Name;
                    td.Title = aa.Title ?? aa.Type;
                    td.Text = new List<string>();
                    if (!String.IsNullOrEmpty(prefix))
                        td.Text.Add(String.Format("<em>{0}</em>", prefix)); 
                    foreach (ComplexMarkupWithLanguage cmwl in aa.Text)
                        if (cmwl.Language == MifCompiler.Language || cmwl.Language == null)
                        {
                            foreach (XmlElement xel in cmwl.MarkupElements ?? new List<XmlElement>().ToArray())
                                td.Text.Add(CleanHtml(xel).OuterXml);
                            if (cmwl.MarkupText != null) td.Text.Add(cmwl.MarkupText);
                        }

                    if (aa.Data != null)
                        foreach (XmlNode nd in aa.Data)
                            td.Text.Add(CleanHtml(nd).OuterXml);

                    // Add if not ignored
                    if (parameters.ContainsKey("omit-other-annotation") &&
                        parameters["omit-other-annotation"] != null &&
                        parameters["omit-other-annotation"].Contains(td.Title))
                        continue;
                    documentation.Other.Add(td);
                }
            }

            #endregion
            return documentation;
        }
    }
}
