using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Interfaces
{
    /// <summary>
    /// A class implements a reason code
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface is attached to RMIM classes by GPMR when the class implements the 
    /// concept of a reason code. This is the non-genericised version of the IImplementsReasonCode
    /// interface where the code set from which reason codes are drawn is not strongy bound.
    /// </para>
    /// </remarks>
    public interface IImplementsReasonCode
    {

        /// <summary>
        /// The reason why an event occured
        /// </summary>
        CV<String> ReasonCode { get; }

    }

    /// <summary>
    /// Implements a reason code
    /// </summary>
    /// <typeparam name="T">The code system from which the ReasonCode is drawn</typeparam>
    /// <seealso cref="T:MARC.Everest.Interfaces.IImplementsReasonCode"/>
    public interface IImplementsReasonCode<T>
    {

        /// <summary>
        /// The reason why an event occured
        /// </summary>
        CV<T> ReasonCode { get; }

    }
}
