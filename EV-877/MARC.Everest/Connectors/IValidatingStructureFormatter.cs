using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Identifies a formatter that can validate structures
    /// </summary>
    /// <remarks>
    /// <para>This interface appends the ValidateConformance property to 
    /// the structure formatter. When true, this flag will enable
    /// the validation of messages when using the Graph and Parse
    /// methods.</para>
    /// <para>
    /// Most formatters will stop rendering messages when major errors
    /// are encountered. It is usually good practice to set this to 
    /// false when testing and debugging an application, and setting
    /// it to true when production data is being processed.
    /// </para>
    /// </remarks>
    public interface IValidatingStructureFormatter : IStructureFormatter
    {
        /// <summary>
        /// If true, validates the conformance of the structures
        /// </summary>
        bool ValidateConformance { get; set; }
    }
}
