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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Interfaces
{

    /// <summary>
    /// Does this implement status code with no generics?
    /// </summary>
    /// <remarks>
    /// <para>This interface denotes that a particular RMIM class supports
    /// the concept of a status code. This is the non genericised version
    /// of IImplementsStatusCode where the code that identifies the status
    /// of the object is not strongly bound to a particular code system.</para>
    /// </remarks>
    public interface IImplementsStatusCode : IGraphable
    {
        /// <summary>
        /// Identifies the current state of the item
        /// </summary>
        CS<String> StatusCode { get; }
    }

    /// <summary>
    /// Signifies a class that implements a state machine
    /// </summary>
    /// <typeparam name="T">The code set from which status codes are drawn</typeparam>
    /// <seealso cref="T:MARC.Everest.Interfaces.IImplementsStatusCode"/>
    public interface IImplementsStatusCode<T> : IGraphable
    {
        /// <summary>
        /// This identifies the current state of the item
        /// </summary>
        CS<T> StatusCode { get; }
    }
}