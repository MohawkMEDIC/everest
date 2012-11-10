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
    /// Event arguments for message based events
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Get or set the code that was generated from formatting the message
        /// </summary>
        public ResultCode Code { get; private set; }
        /// <summary>
        /// The details related to the formatting
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public IEnumerable<IResultDetail> Details { get; private set; }
        /// <summary>
        /// An alternate structure to send
        /// </summary>
        public IGraphable Alternate { get; set; }
        /// <summary>
        /// Create a new instance of the MessageEventArgs class
        /// </summary>
        public MessageEventArgs(ResultCode code, IEnumerable<IResultDetail> details)
        {
            this.Code = code;
            this.Details = details;
        }
    }
}