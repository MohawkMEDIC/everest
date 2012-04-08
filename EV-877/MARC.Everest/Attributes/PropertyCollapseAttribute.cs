using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Property collapsing attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PropertyCollapseAttribute : NamedAttribute
    {

        /// <summary>
        /// Gets or sets the order in which this collapsed member should be rendered
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets a list of fixed attribute values in the format
        /// ATTRIBUTE=VALUE;ATTRIBUTE=VALUE
        /// </summary>
        public string FixedAttributeValues { get; set; }

    }
}
