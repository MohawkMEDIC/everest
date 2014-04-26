using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Sherpas.StructDoc
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
    }
}
