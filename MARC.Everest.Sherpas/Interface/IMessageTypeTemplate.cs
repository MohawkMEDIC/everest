using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Sherpas.Interface
{
    /// <summary>
    /// Identifies a class which represents a message type
    /// </summary>
    public interface IMessageTypeTemplate : IGraphable
    {

        /// <summary>
        /// Validate the instance
        /// </summary>
        IEnumerable<IResultDetail> ValidateEx();

        /// <summary>
        /// Initializes the instance with the default data
        /// </summary>
        void InitializeInstance();
    }
}
