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

namespace MARC.Everest.Connectors.File
{
    /// <summary>
    ///Represents a result from a file publish operation.
    /// </summary>
    public class FileSendResult : ISendResult
    {
        #region ISendResult Members

        //TODO: What could the status code be - Is this documented automatically because it is a Result Code (just checking)
        /// <summary>
        /// Gets the status code that is returned as part of the formatting operation. 
        /// If the code is rejected, no file will be written to disk.
        /// </summary>
        public ResultCode Code { get; internal set; }
        /// <summary>
        /// Gets the details of the operation. If the Code is not Accepted, this property will contain at least one 
        /// error level detail item describing the nature of the error.
        /// </summary>
        public IResultDetail[] Details { get; internal set; }
        #endregion
    }
}