using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Sherpas.StructDoc
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
    }
}
