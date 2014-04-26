using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.StructDoc
{
    /// <summary>
    /// Represents a struct doc comment node
    /// </summary>
    public class StructDocCommentNode : StructDocNode
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public StructDocCommentNode()
        {

        }

        /// <summary>
        /// Creates a new comment object
        /// </summary>
        public StructDocCommentNode(String text) : base(null, text)
        {

        }

        /// <summary>
        /// Read XML
        /// </summary>
        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.NodeType != System.Xml.XmlNodeType.Comment)
                throw new InvalidOperationException("Invalid state, must be in Comment");
            this.Value = reader.Value;
        }

        /// <summary>
        /// Write XML
        /// </summary>
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteComment(this.Value);
        }
    }
}
