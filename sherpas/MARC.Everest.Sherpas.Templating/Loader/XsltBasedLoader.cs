using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Interface;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using MARC.Everest.Sherpas.Templating.Format;

namespace MARC.Everest.Sherpas.Templating.Loader
{
    /// <summary>
    /// An implementation of a loader that loads DECOR formatted templates
    /// </summary>
    public class XsltBasedLoader : ITemplateLoader
    {

        /// <summary>
        /// The locale to use
        /// </summary>
        public String Locale { get; set; }

        /// <summary>
        /// Load the specified template file and process it into a <see cref="T:MARC.Everest.Sherpas.Templating.Format.TemplateProjectDefinition"/> instance
        /// </summary>
        public Format.TemplateProjectDefinition LoadTemplate(string fileName)
        {
            // First check if the file is in fact a decor file
            XPathDocument navDocument = new XPathDocument(fileName);
            var navigator = navDocument.CreateNavigator();

            // Root node
            var rootNode = navigator.Select("/*");
            if (!rootNode.MoveNext())
                throw new InvalidOperationException("Document has no root!");
            
            String xsltName = rootNode.Current.LocalName + ".xslt";

            // Parameters
            XsltArgumentList args = new XsltArgumentList();
            args.AddParam("language", String.Empty, this.Locale);
            
            // Now apply the XSLT
            var xsltc = new AssemblyStylesheetLoader().LoadStylesheet(xsltName);
            
            using (MemoryStream ms = new MemoryStream())
            {
                xsltc.Transform(navigator, args, ms);
                ms.Seek(0, SeekOrigin.Begin);
                XmlSerializer xsz = new XmlSerializer(typeof(TemplateProjectDefinition));
                return xsz.Deserialize(ms) as TemplateProjectDefinition;
            }
        }
    }
}
