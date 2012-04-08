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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Xml;
using MohawkCollege.EHR.gpmr.COR;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.TemplateCore
{
    /// <summary>
    /// Template representing a feature
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "FeatureTemplate", Namespace = "http://marc.mohawkcollege.ca/hi")]
    public class FeatureTemplate : NamedTemplate
    {
        private string feature;
        private bool newPage = false;
        private string pageTitleProperty;
        private List<NamedTemplate> subTemplates;
        private string pagePathTemplate;
        private string pageTitle;
        private string pageAbstractProperty;
        private string mustHave;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("mustHave")]
        public string MustHave
        {
            get { return mustHave; }
            set { mustHave = value; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("pageAbstractProperty")]
        public string PageAbstractProperty
        {
            get { return pageAbstractProperty; }
            set { pageAbstractProperty = value; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("pageTitle")]
        public string PageTitle
        {
            get { return pageTitle; }
            set { pageTitle = value; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("pageParent")]
        public string PagePathTemplate
        {
            get { return pagePathTemplate; }
            set { pagePathTemplate = value; }
        }
	
        /// <summary>
        /// Sub templates to apply in this scope
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [XmlElement("propertyTemplate", Type = typeof(PropertyTemplate))]
        [XmlElement("functionTemplate", Type = typeof(FunctoidTemplate))]
        [XmlElement("xmlElementTemplate", Type=typeof(XmlElementTemplate))]
        [XmlElement("objectTemplate", Type = typeof(ObjectTemplate))]
        public List<NamedTemplate> SubTemplates
        {
            get { return subTemplates; }
            set { subTemplates = value; }
        }
	
        /// <summary>
        /// Name of the property that should represent the title of this page
        /// </summary>
        [XmlAttribute("pageTitleProperty")]
        public string PageTitleProperty
        {
            get { return pageTitleProperty; }
            set { pageTitleProperty = value; }
        }
	
        /// <summary>
        /// True if a new article should be created when this feature template is invoked
        /// </summary>
        [XmlAttribute("createNewPage")]
        public bool NewPage
        {
            get { return newPage; }
            set { newPage = value; }
        }
	
        /// <summary>
        /// The feature being defined in this template
        /// </summary>
        [XmlAttribute("feature")]
        public string Feature
        {
            get { return feature; }
            set { feature = value; }
        }

        /// <summary>
        /// The feature mode, determines under what context the feature template should be
        /// applied
        /// </summary>
        [XmlAttribute("context")]
        public string Mode { get; set; }

        /// <summary>
        /// Fill the template
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)")]
        public override string FillTemplate()
        {

            //// Current template fields
            string[][] templateFields = new string[][] 
            {
                new string[] { "$feature$", "Invalid Operation" },
                new string[] { "01-09-2009", DateTime.Now.ToString("yyyy-MM-dd") },
                new string[] { "$date$", DateTime.Now.ToString("yyyy-MM-dd") },
                new string[] { "$time$", DateTime.Now.ToString("HH:mm:ss") },
                new string[] { "$user$", SystemInformation.UserName },
                new string[] { "$value$", Context.ToString().Replace("<","&lt;").Replace(">","&gt;").Replace("$","&#036;").Replace("@", "&#64;").Replace("^", "&#094;")}, 
                new string[] { "$$", "&#036;" }, 
                new string[] { "@@", "&#064;" },
                new string[] { "^^", "&#094;;" },
                new string[] { "$version$", Assembly.GetEntryAssembly().GetName().Version.ToString() },
                new string[] { "$typeName$", Context == null ? "" : Context.GetType().Name },
                new string[] { "$guid$", Guid.NewGuid().ToString() },
                new string[] { "$guid2$", Guid.NewGuid().ToString() }
            };

            if (this.Context is Feature && (this.Context as Feature).Annotations.Exists(o => o is SuppressBrowseAnnotation))
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Feature '{0}' won't be published as a SuppressBrowse annotation was found", (this.Context as Feature).Name), "warn");
                return "";
            }

            string output = Content;

            foreach (string[] kv in templateFields)
                output = output.Replace(kv[0], kv[1]);

            // Process output for literals
            #region Literals
            while (output.Contains("$"))
            {
                int litPos = output.IndexOf('$');
                int ePos = output.IndexOf('$', litPos + 1);

                System.Diagnostics.Debug.Assert(ePos >= litPos, String.Format("Unfinished template variable in HTML template '{0}'", output));

                string parmName = "";
                if(ePos > litPos)
                    parmName = output.Substring(litPos + 1, ePos - litPos - 1);

                PropertyTemplate pt = NonParameterizedTemplate.Spawn(GetPropertyTemplate(parmName) as NonParameterizedTemplate, Parent, GetContextPropertyValue(parmName)) as PropertyTemplate;
                var ctxPropertyValue = GetContextPropertyValue(parmName);

                if (pt == null && !string.IsNullOrEmpty(parmName)) // None found ... use value
                    output = output.Replace(string.Format("${0}$", parmName), string.Format("{0}", ctxPropertyValue).Replace("$", "&#36;").Replace("@", "&#64;").Replace("^", "&#094;"));
                else if (pt.Property == null)
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Invalid property name for parameter '{0}' propertyTemplate name '{1}'", parmName, pt.Name), "error");
                    output = output.Replace(String.Format("${0}$", parmName), "<span style=\"color:red\">Invalid property name</span>");
                }
                else if (!string.IsNullOrEmpty(parmName))// Found, so use
                {
                    pt.Context = GetContextPropertyValue(pt.Property) ;
                    output = output.Replace(string.Format("${0}$", parmName), pt.FillTemplate().Replace("$", "&#36;").Replace("@", "&#64;").Replace("^", "&#094;"));
                }
            }
            #endregion

            #region Arrays
            while (output.Contains("@"))
            {
                int litPos = output.IndexOf('@');
                int ePos = output.IndexOf('@', litPos + 1);
                string parmName = output.Substring(litPos + 1, ePos - litPos - 1);

                object[] value = null;
                PropertyTemplate pt = NonParameterizedTemplate.Spawn(GetPropertyTemplate(parmName) as NonParameterizedTemplate, Parent, null) as PropertyTemplate;

                if(parmName == "this")
                    value = ConvertToObjectArray(this.Context);
                else
                    value = ConvertToObjectArray(GetContextPropertyValue(pt != null ? pt.Property : parmName));
                
                // First, we'll look for a property template that has the specific name
                //value = ConvertToObjectArray(GetContextPropertyValue(pt != null ? pt.Property : parmName));

                if(value != null && pt == null)
                {
                    foreach(object o in value)
                        output = output.Replace(string.Format("@{0}@", parmName), string.Format("{0}@{1}@", o, parmName));
                    output = output.Replace(string.Format("@{0}@", parmName), "");
                }
                else if(value != null && pt != null) // Apply template
                {
                    foreach (object o in value)
                    {
                        PropertyTemplate cpt = NonParameterizedTemplate.Spawn(pt, Parent, o) as PropertyTemplate;
                        output = output.Replace(string.Format("@{0}@", parmName), string.Format("{0}@{1}@", cpt.FillTemplate().Replace("$", "&#36;").Replace("@", "&#64;").Replace("^", "&#094;"), parmName));
                    }
                    output = output.Replace(string.Format("@{0}@", parmName), "");
                }
                else
                    output = output.Replace(string.Format("@{0}@", parmName), "");
            }
            #endregion

            #region Functoids
            while (output.Contains("^"))
            {
                int litPos = output.IndexOf('^');
                int ePos = output.IndexOf('^', litPos + 1);

                System.Diagnostics.Debug.Assert(ePos != -1, String.Format("Template field not closed at '{0}'", this.Name));

                string funcName = output.Substring(litPos + 1, ePos - litPos - 1);

                // First, we'll look for a property template that has the specific name
                var funcTemplate = NonParameterizedTemplate.Spawn(this.SubTemplates.Find(o => o.Name == funcName && o is FunctoidTemplate), this.Parent, this.Context) as FunctoidTemplate;
                object content = null;
                if(funcTemplate == null)
                    content = String.Format("<span style=\"color:red\">Could not find function template '{0}'</span>", funcName);
                else
                    content = funcTemplate.FillTemplate();
                if (content is IEnumerable && !(content is String))
                {
                    foreach (object o in content as IEnumerable)
                    {
                        string ovalue = o == null ? "" : o.ToString().Replace("$", "&#36;").Replace("@", "&#64;").Replace("^", "&#094;");
                        output = output.Replace(string.Format("^{0}^", funcName), string.Format("{0}^{1}^", ovalue, funcName));
                    }
                    output = output.Replace(string.Format("^{0}^", funcName), "");
                }
                else
                    output = output.Replace(string.Format("^{0}^", funcName), string.Format("{0}", content.ToString().Replace("$", "&#36;").Replace("@", "&#64;").Replace("^", "&#094;")));
            }
            #endregion

            // Apply XmlElement Templates
            output = ApplyXmlElementTemplates(output);

            return output;
        }

        /// <summary>
        /// Apply all xml element templates
        /// </summary>
        /// <param name="output">The current output</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private string ApplyXmlElementTemplates(string output)
        {
            List<XmlElementTemplate> elementTemplates = new List<XmlElementTemplate>();

            foreach (NamedTemplate tpl in SubTemplates)
                if (tpl is XmlElementTemplate) elementTemplates.Add(tpl as XmlElementTemplate);
                else if (tpl is ObjectTemplate) elementTemplates.Add(tpl as XmlElementTemplate);

            // Clean v3 documentation markup
            if(elementTemplates.Count > 0)
                try
                {
                    XmlDocument pdoc = new XmlDocument();

                    pdoc.LoadXml(string.Format("<root>{0}</root>", output.Replace("&","&amp;")));

                    // Apply Templates
                    // Do a foreach
                    foreach (XmlElementTemplate tpl in elementTemplates)
                    {
                        XmlNodeList refNodeList = pdoc.SelectNodes(tpl.Match);
                        foreach (XmlNode nd in refNodeList)
                        {
                            XmlDocument tload = new XmlDocument();
                            tpl.Context = nd;
                            tload.LoadXml(string.Format("<span>{0}</span>", tpl.FillTemplate().Replace("&","&amp;")));
                            XmlElement repElement = pdoc.ImportNode(tload.DocumentElement, true) as XmlElement;
                            nd.ParentNode.InsertBefore(repElement, nd);
                            nd.ParentNode.RemoveChild(nd);
                        }
                    }

                    output = pdoc.DocumentElement.InnerXml.Replace("&amp;", "&").Replace("$", "&#36;").Replace("@", "&#64;");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.Message, "error");
                }

            return output;
        }

        
        /// <summary>
        /// Look for a property template that has the matching name (first priority) or
        /// failing that, property identifier
        /// </summary>
        /// <param name="NameOrProperty">The property or name of the template</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        internal PropertyTemplate GetPropertyTemplate(string NameOrProperty)
        {
            if (NameOrProperty == null) return null;

            foreach (NamedTemplate pt in SubTemplates)
                if (pt is PropertyTemplate && pt.Name == NameOrProperty) return pt as PropertyTemplate;
            foreach (NamedTemplate pt in SubTemplates)
                if (pt is PropertyTemplate && (pt as PropertyTemplate).Property == NameOrProperty && 
                    pt.Name == null) return pt as PropertyTemplate;

            return null;
        }

        /// <summary>
        /// Get the title of the article
        /// </summary>
        /// <returns></returns>
        internal string GetTitle()
        {
            PropertyTemplate ptm = NonParameterizedTemplate.Spawn(GetPropertyTemplate(pageTitleProperty), Parent, this.Context) as PropertyTemplate;
            if (ptm == null) return pageTitle ?? base.GetContextPropertyValue(PageTitleProperty).ToString();
            else return ptm.FillTemplate();
        }

        /// <summary>
        /// Get the abstract of the article
        /// </summary>
        /// <returns></returns>
        internal string GetAbstract()
        {
            PropertyTemplate ptm = NonParameterizedTemplate.Spawn(GetPropertyTemplate(pageAbstractProperty), Parent, this.Context) as PropertyTemplate;
            if (ptm == null) return base.GetContextPropertyValue(pageAbstractProperty).ToString();
            else return ptm.FillTemplate();
        }

        /// <summary>
        /// Get the page path
        /// </summary>
        internal string GetPath()
        {
            PropertyTemplate ptm = NonParameterizedTemplate.Spawn(GetPropertyTemplate(pagePathTemplate), Parent, this.Context) as PropertyTemplate;
            if (ptm == null) return pagePathTemplate;
            else return ptm.FillTemplate();
        }


    }
}