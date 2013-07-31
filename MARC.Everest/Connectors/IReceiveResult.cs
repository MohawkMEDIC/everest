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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Represents a result from a receive connection
    /// </summary>
    /// <remarks>
    /// <para>The IReceiveResult interface allows connectors to pass back meta data about the
    /// instance received from the wire. For example, an MsMq connector could theoretically
    /// pass queue and authentication information along with the parsed message instance</para>
    /// </remarks>
    public interface IReceiveResult
    {
        /// <summary>
        /// The result code
        /// </summary>
        ResultCode Code { get; }
        /// <summary>
        /// The details of the result
        /// </summary>
        IEnumerable<IResultDetail> Details { get; }
        /// <summary>
        /// The result received from the connector
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Structure")]
        IGraphable Structure { get; }
    }
}