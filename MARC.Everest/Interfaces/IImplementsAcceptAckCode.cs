/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.

 * 
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Interfaces
{
    /// <summary>
    /// Identifies a class as implementing accept ack code
    /// </summary>
    /// <remarks>
    /// This interface is appended to any RMIM class that implements the AcceptAckCode. It is 
    /// usually used in conjunction with the <see cref="T:MARC.Everest.Interfaces.IInteraction"/> interface, 
    /// however its functionality is separated due to the fact that the codification system for 
    /// an AcceptAckCode is determined by the class itself, rather than the interface.
    /// </remarks>
    /// <typeparam name="T">The code system from which accept ack codes are taken</typeparam>
    /// <seealso cref="T:MARC.Everest.Interfaces.IInteraction"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ack")]
    public interface IImplementsAcceptAckCode<T> : IGraphable
    {
        /// <summary>
        /// A code that states when a acknowledgment message should be received.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ack")]
        CS<T> AcceptAckCode { get; }
    }
}