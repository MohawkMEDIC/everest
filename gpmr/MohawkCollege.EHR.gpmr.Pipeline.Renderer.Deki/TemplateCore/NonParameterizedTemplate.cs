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
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.TemplateCore
{
    /// <summary>
    /// Identifies a template section with no parameter or property templates
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "NonParameterizedTemplate", Namespace = "http://marc.mohawkcollege.ca/hi")]
    public class NonParameterizedTemplate
    {

        private string templateContent;
        [NonSerialized]
        private object context;
        private List<string> annotations;
        [NonSerialized]
        private DocTemplate parent;

        /// <summary>
        /// Spawn a new template from another
        /// </summary>
        /// <param name="Clonee">The existing template to clone from</param>
        /// <param name="Parent">The parent DocTemplate</param>
        /// <param name="Context">The context object</param>
        /// <returns>A spawned clone</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Parent"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Context"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Clonee"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Clonee")]
        public static NonParameterizedTemplate Spawn(NonParameterizedTemplate Clonee, DocTemplate Parent, object Context)
        {

            if (Clonee == null) return null;

            // Use a serialize / deserialize
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, Clonee);
            ms.Seek(0, SeekOrigin.Begin);
            NonParameterizedTemplate retVal = bf.Deserialize(ms) as NonParameterizedTemplate;
            retVal.Parent = Parent;
            retVal.Context = Context;
            return retVal;
        }

        /// <summary>
        /// Parent template
        /// </summary>
        [XmlIgnore()]
        public DocTemplate Parent
        {
            get { return parent; }
            set { parent = value; }
        }
	
        /// <summary>
        /// Annotations
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("Annotations")]
        public List<string> Annotations
        {
            get { return annotations; }
            set { annotations = value; }
        }
	
        /// <summary>
        /// Get or set the context object that will be used to populate the template
        /// </summary>
        [XmlIgnore()]
        public object Context
        {
            internal get { return context; }
            set { context = value; }
        }

        /// <summary>
        /// The content of the template
        /// </summary>
        [XmlText()]
        public string Content
        {
            get { return templateContent; }
            set { templateContent = value; }
        }

        /// <summary>
        /// Get the content of this represented as an xml document
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode"), XmlIgnore()]
        public XmlDocument XmlContent
        {
            get
            {
                XmlDocument d = new XmlDocument();
                d.LoadXml(templateContent);
                return d;
            }
            set
            {
                templateContent = value.OuterXml;
            }
        }

        /// <summary>
        /// Get the value of a property
        /// </summary>
        /// <param name="PropertyName">The name of the property</param>
        /// <returns>The property</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Property"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        protected object GetContextPropertyValue(string PropertyName)
        {
            Type st = context.GetType();
            
            PropertyInfo pi = st.GetProperty(PropertyName);

            // Sanity check
            if (pi == null) 
                return string.Format("&quot;Invalid Property '{0}'&quot;", PropertyName);

            return pi.GetValue(context, null);
        }

        /// <summary>
        /// Fill this template
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)")]
        internal string FillTemplate(FeatureTemplate ftpl)
        {
            // Current template fields
            string[][] templateFields = new string[][] 
            {
                new string[] { "$feature$", ftpl.FillTemplate() },
                new string[] { "01-09-2009", DateTime.Now.ToString("yyyy-MM-dd") },
                new string[] { "$time$", DateTime.Now.ToString("HH:mm:ss") },
                new string[] { "$date$", DateTime.Now.ToString("yyyy-MM-dd") },
                new string[] { "$user$", SystemInformation.UserName },
                new string[] { "$value$", ftpl.Context.ToString()} ,
                new string[] { "$$", "&#036;" },
                new string[] { "^^", "&#094;" },
                new string[] { "@@", "&#064;" }
            };

            string output = Content;

            foreach (string[] kv in templateFields)
                output = output.Replace(kv[0], kv[1]);

            return output;
        }
    }
}