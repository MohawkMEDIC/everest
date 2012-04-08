using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// When used, this attributes marks all the RIM concepts that a particular entity 
    /// "realizes".
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class RealizationAttribute : NamedAttribute
    {
        /// <summary>
        /// The type that owns this realization 
        /// </summary>
        public Type OwnerClass { get; set; }

    }
}
