using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Diagnostics;
using System.Reflection;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.PipelineLoader
{
    /// <summary>
    /// MIF Transformer component is responsible for transforming MIF files between versions for the 
    /// process. The PipelineComponent can load MIF 2.1.4
    /// </summary>
    internal class MifTransformer
    {

        /// <summary>
        /// True if the transformer did perform a transform
        /// </summary>
        public bool DidTransform { get; internal set; }

        /// <summary>
        /// Get a file from the transformer
        /// </summary>
        internal Stream GetFile(string fileName)
        {
            // Get the transform directory
            string txDirectory = ConfigurationManager.AppSettings["MIF.TransformDir"].Replace("~", Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            
            // Load the file and determine if we need a transform (native version)
            XPathDocument xpd = new XPathDocument(fileName);
            var verNode = xpd.CreateNavigator().SelectSingleNode("//*/@schemaVersion");
            if (verNode == null)
            {
                Trace.WriteLine("Can't find schemaVersion for {0}", Path.GetFileName(fileName));
                return null;
            }
            else if (verNode.Value.StartsWith("2.1.4"))
                return File.OpenRead(fileName);
            
            // Trasnform , does a transform dir exist?
            string xslFile = String.Format("{0}.{1}",Path.Combine(txDirectory, verNode.Value), "xslt");
            if (File.Exists(xslFile))
            {
                DidTransform = true;
                // Transform
                XslCompiledTransform xsl = new XslCompiledTransform();
                xsl.Load(xslFile);
                MemoryStream output = new MemoryStream();
                xsl.Transform(xpd, new XsltArgumentList(), output);
                output.Seek(0, SeekOrigin.Begin);
                return output;
            }
            else
                Trace.WriteLine(String.Format("Cannot locate suitable transform for MIF v{0}", verNode.Value), "error");
            return null;
        }

    }
}
