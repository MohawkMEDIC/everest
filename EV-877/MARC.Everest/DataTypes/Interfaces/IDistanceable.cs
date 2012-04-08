using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// This interface is implemented by classes 
    /// where the distance between two instances 
    /// can be calculated
    /// </summary>
    public interface IDistanceable<T>
    {

        /// <summary>
        /// Calculate the distance between 
        /// this and <paramref name="other"/>
        /// </summary>
        PQ Distance(T other);

    }
}
