using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Interfaces;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Identifies an interface that the ANY data type implements
    /// </summary>
    public interface IAny : IImplementsNullFlavor, ISemanticEquatable, ICloneable
    {
        /// <summary>
        /// Validates the instance 
        /// </summary>
        bool Validate();

        /// <summary>
        /// Predicate that determines if the instance is logically null
        /// </summary>
        bool IsNull { get; }

        /// <summary>
        /// Gets the data type of the instance 
        /// </summary>
        Type DataType { get; }

        /// <summary>
        /// Gets or sets the flavor of the type
        /// </summary>
        string Flavor { get; set; }
    }
}
