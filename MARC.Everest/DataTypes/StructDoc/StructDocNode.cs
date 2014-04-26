using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
#if WINDOWS_PHONE
using MARC.Everest.Phone;
#endif 

namespace MARC.Everest.DataTypes.StructDoc
{
    public abstract class StructDocNode : IXmlSerializable, IEquatable<StructDocNode>
    {
        /// <summary>
        /// Creates a structured document node
        /// </summary>
        public StructDocNode()
        {
            this.Children = new List<StructDocNode>();
        }

        /// <summary>
        /// Creates a new structured document node with the specified tagName and value
        /// </summary>
        /// <param name="name">The name of the node</param>
        /// <param name="value">The value of the node</param>
        public StructDocNode(String name, String value) : this()
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public String Value { get; set; }

        /// <summary>
        /// Gets the list of the children
        /// </summary>
        public List<StructDocNode> Children { get; private set; }


        #region IXmlSerializable Members

        /// <summary>
        /// Get schema
        /// </summary>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Read XML from the specified stream
        /// </summary>
        public abstract void ReadXml(System.Xml.XmlReader reader);

        /// <summary>
        /// Write XML to the specified stream
        /// </summary>
        public abstract void WriteXml(System.Xml.XmlWriter writer);

        #endregion


        /// <summary>
        /// Determine equality
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is StructDocNode)
                return this.Equals((StructDocNode)obj);
            else
                return base.Equals(obj);
        }
        #region IEquatable<StructDocNode> Members

        /// <summary>
        /// Determine equality
        /// </summary>
        public virtual bool Equals(StructDocNode other)
        {
            if (other == null)
                return false;

            bool equals = other.GetType() == this.GetType();
            equals &= other.Name == this.Name;
            equals &= other.Value == this.Value;
            if (this.Children != null && other.Children != null &&
                this.Children.Count == other.Children.Count)
            {
                foreach (var child in this.Children.OfType<StructDocAttributeNode>())
                    equals &= other.Children.Exists(c => c.Equals(child));
                List<StructDocNode> thisNonChildren = this.Children.FindAll(c => !(c is StructDocAttributeNode)),
                    otherNonChildren = other.Children.FindAll(c => !(c is StructDocAttributeNode));
                for (int i = 0; i < thisNonChildren.Count; i++)
                    equals &= (thisNonChildren[i] == null && otherNonChildren[i] == null) ^ (thisNonChildren[i].Equals(otherNonChildren[i]));
            }
            else
                equals = false;
            return equals;
        }

        #endregion
    }
}
