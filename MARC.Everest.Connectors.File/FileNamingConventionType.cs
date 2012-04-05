using System;
using System.Collections.Generic;
using System.Text;

namespace MARC.Everest.Connectors.File
{
    /// <summary>
    /// Identifies the types of file naming conventions that can be used
    /// for generating files with the file connectors
    /// </summary>
    public enum FileNamingConventionType
    {
        /// <summary>
        /// The naming convention should be a globally unique identifier
        /// </summary>
        Guid,
        /// <summary>
        /// The naming convention should be based on the message's ID 
        /// element
        /// </summary>
        Id
    }
}
