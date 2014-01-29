using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Interfaces
{
    /// <summary>
    /// Represents a class which contains a list template identifiers
    /// </summary>
    /// <remarks>
    /// This interface is commonly used for CDA implementations which need to populate the template identifiers
    /// </remarks>
    public interface IImplementsTemplateId : IGraphable
    {
        /// <summary>
        /// Gets or sets a list of identifiers which specify templates this particular instantiation of the class implements
        /// </summary>
        LIST<II> TemplateId { get; set; }

    }
}
