using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Identifies an interface that the ANY data type implements
    /// </summary>
#if WINDOWS_PHONE
    public interface IAny : IImplementsNullFlavor, ISemanticEquatable, IGraphable
#else
    public interface IAny : IImplementsNullFlavor, ISemanticEquatable, IGraphable, ICloneable
#endif
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

        /// <summary>
        /// Validates the type and returns the detected issues with the data type instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IResultDetail> ValidateEx();

#if WINDOWS_PHONE
        /// <summary>
        /// Windows phone does not allow use of ICloneable so many methods
        /// will fail, so we redefine it in IAny for WP compiles
        /// </summary>
        object Clone();
#endif

    }
}
