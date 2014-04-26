using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Xml;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// A formatter which can represent and parse SD content to/from the wire
    /// </summary>
    /// <remarks>SD is unique in that it is an ED however it is not an ED</remarks>
    public class SDFormatter : ANYFormatter
    {

        /// <summary>
        /// Graph
        /// </summary>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {

            // SD instance
            SD instance_sd = (SD)o;
            base.Graph(s, instance_sd, result);
            // Null flavor
            if (instance_sd == null || instance_sd.IsNull)
                return;

            // Attributes
            if (instance_sd.MediaType != null)
                s.WriteAttributeString("mediaType", instance_sd.MediaType);
            if (instance_sd.Language != null)
                s.WriteAttributeString("language", instance_sd.Language);
            if (instance_sd.StyleCode != null)
                s.WriteAttributeString("styleCode", instance_sd.StyleCode);
            if (instance_sd.Id != null)
                s.WriteAttributeString("ID", instance_sd.Id);

            // Value
            if (instance_sd.Content != null)
                foreach (var cnt in instance_sd.Content)
                    cnt.WriteXml(s);

        }

        /// <summary>
        /// Parse the SD
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Parse base (ANY) from the stream
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;


            // Parse ED
            SD retVal = base.Parse<SD>(s, result);

            // Now parse our data out... Attributes
            if (s.GetAttribute("styleCode") != null)
                retVal.StyleCode = s.GetAttribute("styleCode");
            if (s.GetAttribute("mediaType") != null)
                retVal.MediaType = s.GetAttribute("mediaType");
            if (s.GetAttribute("language") != null)
                retVal.Language = s.GetAttribute("language");
            if (s.GetAttribute("ID") != null)
                retVal.Id = s.GetAttribute("ID");

            // Elements and inner data
            #region Elements

            if (!s.IsEmptyElement)
            {
                // Read subtree of this node
                s = s.ReadSubtree();
                if(s.Read())
                {
                    var node = new MARC.Everest.DataTypes.StructDoc.StructDocElementNode();
                    node.ReadXml(s);
                    retVal.Content.AddRange(node.Children.OfType<MARC.Everest.DataTypes.StructDoc.StructDocElementNode>());
                }
            }

            #endregion

            // Validate
            base.Validate(retVal, pathName, result);

            return retVal;
        }

        /// <summary>
        /// Gets the type this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get
            {
                return "SD";
            }
        }

        /// <summary>
        /// Get the supported properties
        /// </summary>
        /// <returns></returns>
        public override List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            Type sdType = typeof(SD);
            return new List<System.Reflection.PropertyInfo>()
            {
                sdType.GetProperty("Language"),
                sdType.GetProperty("Content"),
                sdType.GetProperty("MediaType"),
                sdType.GetProperty("StyleCode"),
                sdType.GetProperty("Id")
            };
            
        }
    }
}
