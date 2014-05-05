using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Identifies a class as a container for a template
    /// </summary>
    /// <remarks>This is used by the formatter to ensure that deserialization of containers 
    /// works properly</remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = true)]
    public class TemplateContainerAttribute : Attribute
    {
        /// <summary>
        /// The type of template that this encapsulates
        /// </summary>
        public Type TemplateType { get; set; }

        /// <summary>
        /// The name of the property to which the template type is applied
        /// </summary>
        public String PropertyName { get; set; }
    }
}
