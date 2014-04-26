using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Sherpas.StructDoc
{
    public abstract class StructDocNode
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

    }
}
