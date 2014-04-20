using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Attributes
{

    /// <summary>
    /// Conformance statement check constraint delegate
    /// </summary>
    public delegate bool ConformanceStatementCheckConstraintDelegate<T>(T instance);

    /// <summary>
    /// Attribute conformance statment
    /// </summary>
    /// <remarks>This attribute is used to describe a conformance statement violation</remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public class FormalConstraintAttribute : Attribute
    {

        /// <summary>
        /// Creates a new conformance statement
        /// </summary>
        public FormalConstraintAttribute()
        {

        }

        /// <summary>
        /// Creates a new conformance statement
        /// </summary>
        public FormalConstraintAttribute(String description, String methodName)
        {
            this.Description = description;
            this.CheckConstraintMethod = methodName;
        }

        /// <summary>
        /// Identifies the method which should be used to check the method 
        /// </summary>
        public String CheckConstraintMethod { get; set; }
        /// <summary>
        /// Gets or sets the description of the conformance rule
        /// </summary>
        public String Description { get; set; }
    }
}
