using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Sherpas.StructDoc
{
    /// <summary>
    /// Represents a structured document element
    /// </summary>
    public class StructDocElementNode : StructDocNode
    {

        /// <summary>
        /// Structured document element
        /// </summary>
        public StructDocElementNode()
        {

        }

        /// <summary>
        /// Structured document element
        /// </summary>
        /// <param name="name">The name of the element</param>
        public StructDocElementNode(String name) : base(name, null)
        {

        }

        /// <summary>
        /// Structured document element
        /// </summary>
        /// <param name="name">The name of the element</param>
        /// <param name="value">The text value of the element inserted as a text element</param>
        public StructDocElementNode(String name, String value) : base(name, null)
        {
            this.Children.Add(new StructDocTextNode(value));
        }

        /// <summary>
        /// Add a comment
        /// </summary>
        /// <param name="commentText">The text of the comment</param>
        /// <returns>A pointer to "this" for chaining calls</returns>
        public StructDocElementNode AddComment(String commentText)
        {
            this.Children.Add(new StructDocCommentNode(commentText));
            return this;
        }

        /// <summary>
        /// Add an attribute
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <param name="value">The value of the attribute</param>
        /// <returns>A pointer to "this" for chaining calls</returns>
        public StructDocElementNode AddAttribute(String name, String value)
        {
            this.Children.Add(new StructDocAttributeNode(name, value));
            return this;
        }

        /// <summary>
        /// Add an element with the specified name
        /// </summary>
        /// <param name="name">The name of the child element</param>
        /// <returns>A pointer to "this" for chaining calls</returns>
        public StructDocElementNode AddElement(String name)
        {
            this.Children.Add(new StructDocElementNode(name));
            return this;
        }

        /// <summary>
        /// Add an element with the specified name and value
        /// </summary>
        /// <param name="name">The name of the element</param>
        /// <param name="value">The initial value of the element</param>
        /// <returns>A pointer to "this" for chaining calls</returns>
        public StructDocElementNode AddElement(String name, String value)
        {
            this.Children.Add(new StructDocElementNode(name, value));
            return this;
        }

        /// <summary>
        /// Add an element with the specified name and children
        /// </summary>
        /// <param name="name">The name of the element</param>
        /// <param name="children">Child nodes to be made part of the element</param>
        /// <returns>A pointer to "this" for chaining calls</returns>
        public StructDocElementNode AddElement(String name, params StructDocNode[] children)
        {
            var sde = new StructDocElementNode(name);
            foreach (var c in children)
                sde.Children.Add(sde);
            return this;
        }

        /// <summary>
        /// Add text node
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>A pointer to "this" for chaining calls</returns>
        public StructDocElementNode AddText(String text)
        {
            this.Children.Add(new StructDocTextNode(text));
            return this;
        }
    }
}
