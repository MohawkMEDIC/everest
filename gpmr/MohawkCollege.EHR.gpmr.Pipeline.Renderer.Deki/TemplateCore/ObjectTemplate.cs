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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.TemplateCore
{
    /// <summary>
    /// Parses an object Tag and gives a template sane access to the param tags 
    /// </summary>
    /// <example>
    /// <code lang="xml">
    /// &lt;object class="Test"&gt;
    ///     &lt;param name="name1" value="value1"/&gt;
    /// &lt;/object&gt;
    /// </code>
    /// 
    /// Results in
    /// <code lang="xml">
    /// &lt;a href="$name1$"&gt;$name1$&lt;/a&gt;
    /// </code>
    /// </example>
    [XmlType(TypeName = "ObjectTemplate", Namespace = "http://marc.mohawkcollege.ca/hi")]
    [Serializable]
    public class ObjectTemplate : XmlElementTemplate
    {

        const string OBJ_REGEX = "<(.*?\\s)(.*?>)";
        const string PARM_REGEX = "name=\"(.*?)\"\\svalue=\"(.*?)\"";

        /// <summary>
        /// Fill the template with data
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public override string FillTemplate()
        {

            // Apply regex to the context element
            XmlNode context = this.Context as XmlNode;
            string content = Content;

            Dictionary<string, string> parameters = new Dictionary<string,string>(); // Parameters for the template

            // Apply the content filter
            Regex reg = new Regex(OBJ_REGEX);
            MatchCollection mc = reg.Matches(context.OuterXml);

            foreach (System.Text.RegularExpressions.Match match in mc)
            {
                if (match.Groups[1].Value == "param " && match.Groups.Count >= 2)
                {
                    // Extract parameters
                    reg = new Regex(PARM_REGEX);
                    System.Text.RegularExpressions.Match pMatch = reg.Match(match.Groups[2].Value);
                    parameters.Add(pMatch.Groups[1].Value, pMatch.Groups[2].Value);
                }
            }

            // Substitute
            foreach (KeyValuePair<string, string> kv in parameters)
                content = content.Replace(string.Format("${0}$", kv.Key), kv.Value);

            return content;
        }
    }
}