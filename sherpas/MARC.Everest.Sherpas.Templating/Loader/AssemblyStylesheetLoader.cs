/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 4-2-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml;
using System.IO;

namespace MARC.Everest.Sherpas.Templating.Loader
{
    /// <summary>
    /// Will load stylesheets from this assembly
    /// </summary>
    public class AssemblyStylesheetLoader : XmlResolver
    {
        /// <summary>
        /// Load the assembly resource
        /// </summary>
        public IXPathNavigable LoadAssemblyResourceXml(String resourceName)
        {
            using (var str = typeof(AssemblyStylesheetLoader).Assembly.GetManifestResourceStream(resourceName))
            {
                if (str == null) return null;
                return new XPathDocument(str);
            }
        }

        /// <summary>
        /// Load the specified stylesheet
        /// </summary>
        public XslCompiledTransform LoadStylesheet(String stylesheetName)
        {
            string resourceName = this.GetStylesheetResourceName(stylesheetName);
            if (String.IsNullOrEmpty(resourceName))
                throw new FileNotFoundException("Cannot find the appropriate stylesheet in this assembly's manifest");
            
            var xslt = this.LoadAssemblyResourceXml(resourceName);
            XslCompiledTransform xsltc = new XslCompiledTransform(false);
            XsltSettings settings = new XsltSettings(false, true);
            xsltc.Load(xslt, settings, new AssemblyStylesheetLoader());

            return xsltc;
        }

        /// <summary>
        /// Get a stylesheet's resource name
        /// </summary>
        private String GetStylesheetResourceName(String stylesheetName)
        {
            foreach (var itm in typeof(AssemblyStylesheetLoader).Assembly.GetManifestResourceNames())
                if (itm.ToLower().EndsWith(stylesheetName.ToLower()))
                    return itm;
            return null;
        }

        /// <summary>
        /// Get the credentials for the specified entry
        /// </summary>
        public override System.Net.ICredentials Credentials
        {
            set { ; }
        }

        /// <summary>
        /// Get the entry
        /// </summary>
        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            var xsltName = Path.GetFileName(absoluteUri.LocalPath);
            var resourceName = this.GetStylesheetResourceName(xsltName);
            if (resourceName == null)
                throw new FileNotFoundException("Could not locate the specified transform in this assembly manifest");
            return typeof(AssemblyStylesheetLoader).Assembly.GetManifestResourceStream(resourceName);
        }
    }
}
