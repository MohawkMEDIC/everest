/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
    /// Identifies that a class or structure has a mood code
    /// </summary>
    /// <remarks>
    /// GPMR will attach this interface to any RMIM classes that implement 
    /// the concept of a mood code. A mood code can be used to determine the 
    /// mood of an act within an instance. Common mood codes are:
    /// <list type="bullet">
    ///     <item><strong>EVN</strong> - An event occurred triggering the act</item>
    ///     <item><strong>RQO</strong> - The act represents a request</item>
    /// </list>
    /// <para>
    /// This interface is the non-genericised version of IImplementsMoodCode, another 
    /// interface exists for classes which implement a mood code with a strongly bound
    /// code system.
    /// </para>
    /// </remarks>
    public interface IImplementsMoodCode : IGraphable
    {
        /// <summary>
        /// A code distinguishing whether an Act is conceived of as a factual statement or in some other manner as a command, possibility, goal, etc.
        /// </summary>
        CS<String> MoodCode { get; }
    }

    /// <summary>
    /// Identifies the the class or structure has a mood code
    /// </summary>
    /// <seealso cref="T:MARC.Everest.Interfaces.IImplementsMoodCode"/>
    /// <typeparam name="T">The code system from which mood codes are taken</typeparam>
    public interface IImplementsMoodCode<T> : IGraphable
    {
        /// <summary>
        /// A code distinguishing whether an Act is conceived of as a factual statement or in some other manner as a command, possibility, goal, etc.
        /// </summary>
        CS<T> MoodCode { get; }
    }
}