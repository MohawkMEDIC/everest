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
    /// Summary of IImplementsResponseModeCode
    /// </summary>
    /// <typeparam name="T">The code system enumeration from which response mode codes are drawn</typeparam>
    /// <remarks>
    /// <para>This interface is used by GPMR to annotate classes (usually which also implement
    /// the <see cref="T:MARC.Everest.Interfaces.IInteraction"/> interface) that contain 
    /// a response mode code.</para>
    /// </remarks>
    /// <seealso cref="T:MARC.Everest.Interfaces.IInteraction"/>
    public interface IImplementsResponseModeCode<T> : IGraphable
    {
        /// <summary>
        /// Way in which the receiver should respond
        /// </summary>
        CS<T> ResponseModeCode { get; }
    }
}