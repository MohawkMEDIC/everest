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

namespace MARC.Everest.Connectors.File
{
    /// <summary>
    /// Represents a receive result from a file connector.
    /// </summary>
    [Serializable]
    public class FileReceiveResult : IReceiveResult
    {
        #region IReceiveResult Members

        /// <summary>
        /// Gets the code of the result.
        /// </summary>
        public ResultCode Code { get; internal set; }
        /// <summary>
        /// Gets the details of how the code of the result was attained.
        /// </summary>
        public IEnumerable<IResultDetail> Details { get; internal set; }
        /// <summary>
        /// Gets the data for the result.
        /// </summary>
        public MARC.Everest.Interfaces.IGraphable Structure { get; internal set; }
        /// <summary>
        /// Gets the name of the file .
        /// </summary>
        public string FileName { get; internal set; }
        #endregion
    }
}