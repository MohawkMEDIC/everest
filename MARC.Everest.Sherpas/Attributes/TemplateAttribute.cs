using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Template attribute identifies a structure as being a template
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TemplateAttribute : Attribute
    {
        /// <summary>
        /// Creates a new template attribute with templateId 
        /// </summary>
        public TemplateAttribute(String templateId)
        {
            this.TemplateId = templateId;
        }

        /// <summary>
        /// The ID of the template
        /// </summary>
        public string TemplateId { get; set; }
    }
}
