using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Represents a formatter for the BAG class
    /// </summary>
    public class BAGFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream</param>
        /// <param name="o">The object</param>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            SETFormatter formatter = new SETFormatter();
            formatter.Host = this.Host;
            formatter.Graph(s, o, result);
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            SETFormatter formatter = new SETFormatter();
            formatter.Host = this.Host;
            formatter.GenericArguments = this.GenericArguments;
            object retval = formatter.Parse(s, result);
            return retval;
        }

        /// <summary>
        /// Get the name of the type this handles
        /// </summary>
        public string HandlesType
        {
            get { return "BAG"; }
        }

        /// <summary>
        /// Get or set the hosting formatter
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Generic arguments
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public List<PropertyInfo> GetSupportedProperties()
        {
            return new SETFormatter().GetSupportedProperties();
        }

        #endregion
    }
}
