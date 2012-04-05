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
using System.Xml;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.TemplateCore
{

    /// <summary>
    /// Represents an xml element template
    /// </summary>
    [XmlType(TypeName = "XmlElementTemplate", Namespace = "http://marc.mohawkcollege.ca/hi")]
    [Serializable]
    public class XmlElementTemplate : NamedTemplate
    {
        private string match;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("match")]
        public string Match
        {
            get { return match; }
            set { match = value; }
        }
	
        /// <summary>
        /// Fill this template
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public override string FillTemplate()
        {

            // Return value
            string output = Content.Clone() as string;
            XmlElement xe = Context as XmlElement;

            // Loop all 
            while (output.Contains("$"))
            {
                int litPos = output.IndexOf('$');
                int ePos = output.IndexOf('$', litPos + 1);
                string parmName = output.Substring(litPos + 1, ePos - litPos - 1);

                // Should we replace?
                if(parmName == "InnerXml")
                    output = output.Replace(string.Format("${0}$", parmName), xe.InnerXml);
                else
                    output = output.Replace(string.Format("${0}$", parmName), xe.Attributes[parmName] == null ? "" : xe.Attributes[parmName].Value);
            }

            return output;
        }
    }
}