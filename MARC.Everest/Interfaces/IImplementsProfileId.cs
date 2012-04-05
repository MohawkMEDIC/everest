using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Interfaces
{
    /// <summary>
    /// Represents a class that implements a get of a profile identifier
    /// </summary>
    public interface IImplementsProfileId : IGraphable
    {

        /// <summary>
        /// Gets the profile identifier for the interaction
        /// </summary>
        LIST<II> ProfileId { get; }

    }
}
