using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.StructDoc
{
    /// <summary>
    /// Represents a text node
    /// </summary>
    public class StructDocTextNode : StructDocNode
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public StructDocTextNode()
        {

        }
        /// <summary>
        /// Creates a new Structure Document Txt node
        /// </summary>
        /// <param name="text"></param>
        public StructDocTextNode(String text) : base(null, text)
        {

        }

        /// <summary>
        /// When true read/write CDATA
        /// </summary>
        public bool AsCdata { get; set; }

        /// <summary>
        /// Read XML
        /// </summary>
        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.NodeType != System.Xml.XmlNodeType.Text &&
                reader.NodeType != System.Xml.XmlNodeType.Whitespace && 
                reader.NodeType != System.Xml.XmlNodeType.CDATA)
                throw new InvalidOperationException("Invalid state, must be in Text");
            this.Value = reader.Value;
            this.AsCdata = reader.NodeType == System.Xml.XmlNodeType.CDATA;
        }

        /// <summary>
        /// Write XML
        /// </summary>
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (this.AsCdata)
                writer.WriteCData(this.Value);
            else
                writer.WriteString(this.Value);
        }


    }
}
