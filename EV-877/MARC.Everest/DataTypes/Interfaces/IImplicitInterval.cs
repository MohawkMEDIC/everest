using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Identifies that the type has a precision that can be
    /// turned into an interval
    /// </summary>
    public interface IImplicitInterval<T> where T : IAny
    {
        /// <summary>
        /// Convert the object to an interval
        /// </summary>
        IVL<T> ToIVL();
    }
}
