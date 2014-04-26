using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.DataTypes.StructDoc
{
    /// <summary>
    /// Represents a structured document element
    /// </summary>
    public class StructDocElementNode : StructDocNode, IEquatable<StructDocElementNode>
    {

        /// <summary>
        /// Structured document element
        /// </summary>
        public StructDocElementNode()
        {
            this.NamespaceUri = "urn:hl7-org:v3";
        }

        /// <summary>
        /// Structured document element
        /// </summary>
        /// <param name="name">The name of the element</param>
        public StructDocElementNode(String name) : base(name, null)
        {
            this.NamespaceUri = "urn:hl7-org:v3";
        }

        /// <summary>
        /// Structured document element
        /// </summary>
        /// <param name="name">The name of the element</param>
        /// <param name="value">The text value of the element inserted as a text element</param>
        public StructDocElementNode(String name, String value) : base(name, null)
        {
            this.Children.Add(new StructDocTextNode(value));
            this.NamespaceUri = "urn:hl7-org:v3";
        }

        /// <summary>
        /// Add a comment
        /// </summary>
        /// <param name="commentText">The text of the comment</param>
        /// <returns>A pointer to the new comment for chaining calls</returns>
        public StructDocCommentNode AddComment(String commentText)
        {
            var retVal = new StructDocCommentNode(commentText);
            this.Children.Add(retVal);
            return retVal;
        }

        /// <summary>
        /// Add an attribute
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <param name="value">The value of the attribute</param>
        /// <returns>A pointer to the new attribute for chaining calls</returns>
        public StructDocAttributeNode AddAttribute(String name, String value)
        {
            var retVal = new StructDocAttributeNode(name, value);
            this.Children.Add(retVal);
            return retVal;
        }

        /// <summary>
        /// Add an element with the specified name
        /// </summary>
        /// <param name="name">The name of the child element</param>
        /// <returns>A pointer to the new element for chaining calls</returns>
        public StructDocElementNode AddElement(String name)
        {
            var retVal = new StructDocElementNode(name) { NamespaceUri = this.NamespaceUri };
            this.Children.Add(retVal);
            return retVal;
        }

        /// <summary>
        /// Add an element with the specified name and value
        /// </summary>
        /// <param name="name">The name of the element</param>
        /// <param name="value">The initial value of the element</param>
        /// <returns>A pointer to the new element for chaining calls</returns>
        public StructDocElementNode AddElement(String name, String value)
        {
            var retVal = new StructDocElementNode(name, value) { NamespaceUri = this.NamespaceUri };
            this.Children.Add(retVal);
            return retVal;
        }

        /// <summary>
        /// Add an element with the specified name and children
        /// </summary>
        /// <param name="name">The name of the element</param>
        /// <param name="children">Child nodes to be made part of the element</param>
        /// <returns>A pointer to the new element for chaining calls</returns>
        public StructDocElementNode AddElement(String name, params StructDocNode[] children)
        {
            var sde = new StructDocElementNode(name) { NamespaceUri = this.NamespaceUri };
            foreach (var c in children)
                sde.Children.Add(c);
            this.Children.Add(sde);
            return sde;
        }

        /// <summary>
        /// Add text node
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>A pointer to the new text node for chaining calls</returns>
        public StructDocTextNode AddText(String text)
        {
            var retVal = new StructDocTextNode(text);
            this.Children.Add(retVal);
            return retVal;
        }

        /// <summary>
        /// Namespace of the node
        /// </summary>
        public String NamespaceUri { get; set; }

        #region IXmlSerializable Members

        /// <summary>
        /// Read XML
        /// </summary>
        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if(reader.NodeType != System.Xml.XmlNodeType.Element)
                throw new InvalidOperationException("Invalid state, must be in Comment");
            this.Name = reader.LocalName;
            this.NamespaceUri = reader.NamespaceURI;

            reader = reader.ReadSubtree();
            reader.Read(); // Move past initial

            // Read attributes
            if (reader.MoveToFirstAttribute())
            {
                do
                {
                    if (reader.LocalName != "xmlns" && reader.Prefix != "xmlns")
                    {
                        var node = new StructDocAttributeNode();
                        node.ReadXml(reader);
                        this.Children.Add(node);
                    }
                } while (reader.MoveToNextAttribute());
                reader.MoveToElement();
            }

            // REad elements
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case System.Xml.XmlNodeType.Comment:
                        {
                            var node = new StructDocCommentNode();
                            node.ReadXml(reader);
                            this.Children.Add(node);
                            break;
                        }
                    case System.Xml.XmlNodeType.Text:
                    case System.Xml.XmlNodeType.CDATA:
                        {
                            var node = new StructDocTextNode();
                            node.ReadXml(reader);
                            this.Children.Add(node);
                            break;
                        }
                    case System.Xml.XmlNodeType.Element:
                        {
                            var node = new StructDocElementNode();
                            node.ReadXml(reader);
                            this.Children.Add(node);
                            break;
                        }
                }
            }

        }

        /// <summary>
        /// Write XML
        /// </summary>
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            // Start element
            writer.WriteStartElement(this.Name, this.NamespaceUri);

            foreach (StructDocAttributeNode child in this.Children.Where(o => o is StructDocAttributeNode))
                child.WriteXml(writer);

            foreach (StructDocNode child in this.Children.Where(o => !(o is StructDocAttributeNode)))
                child.WriteXml(writer);

            // End element
            writer.WriteEndElement();
        }

        public bool Equals(StructDocElementNode other)
        {
            return base.Equals(other) && this.NamespaceUri == other.NamespaceUri ;
        }

        #endregion
    }
}
