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
    public class BAGFormatter : SETFormatter, IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream</param>
        /// <param name="o">The object</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            base.Graph(s, o, result);
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            return base.Parse(s, result);
        }

        /// <summary>
        /// Get the name of the type this handles
        /// </summary>
        public override string HandlesType
        {
            get { return "BAG"; }
        }


        #endregion
    }
}
