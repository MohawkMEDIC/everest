using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.StructDoc
{
    /// <summary>
    /// Represents an attribute node
    /// </summary>
    public class StructDocAttributeNode : StructDocNode
    {

        /// <summary>
        /// Creates a new instance of the StructureDocumentAttribute
        /// </summary>
        public StructDocAttributeNode()
        {

        }


        /// <summary>
        /// Creates a new instance of the StructuredDocumentAttribute
        /// </summary>
        public StructDocAttributeNode(String name, String value) : base(name, value)
        {
        }

        /// <summary>
        /// Read XML
        /// </summary>
        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.NodeType != System.Xml.XmlNodeType.Attribute)
                throw new InvalidOperationException("Invalid state, must be in Attribute");
            this.Name = reader.LocalName;
            this.Value = reader.Value;
        }

        /// <summary>
        /// Write XML
        /// </summary>
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString(this.Name, this.Value);
        }
    }
}
