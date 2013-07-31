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
    /// Implements IProcessingCode
    /// </summary>
    /// <typeparam name="T">The code system from which processing codes are drawn</typeparam>
    /// <remarks>
    /// <para>This interface is attached to RMIM classes by GPMR which implement
    /// the concept of a ProcessingCode. This interface is almost always used in 
    /// conjunction with <see cref="T:MARC.Everest.Interfaces.IInteraction"/>, but
    /// is separated as the code system (defined by <typeparamref name="T"/>) is 
    /// identified by the processing code property rather than the class itself.</para>
    /// </remarks>
    /// <seealso cref="T:MARC.Everest.Interfaces.IInteraction"/>
    public interface IImplementsProcessingCode<T> : IGraphable
    {
        /// <summary>
        /// Processing Code
        /// </summary>
        CS<T> ProcessingCode { get; }

    }
}