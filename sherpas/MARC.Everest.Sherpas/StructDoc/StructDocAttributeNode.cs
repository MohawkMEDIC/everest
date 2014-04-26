using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Sherpas.StructDoc
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
    }
}
