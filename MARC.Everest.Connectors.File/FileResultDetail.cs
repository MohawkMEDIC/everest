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
using MARC.Everest.Connectors;

namespace MARC.Everest.Connectors.File
{
    /// <summary>
    /// Represents a datatype result detail.
    /// </summary>
    [Serializable]
    public class FileResultDetail : IResultDetail
    {
        #region IResultDetail Members

        /// <summary>
        /// Gets the type of result detail.
        /// </summary>
        public ResultDetailType Type { get; private set; }

        /// <summary>
        /// Gets the message of the detail.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets an exception if one occured. This value will be null if no exception was thrown.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Location of the error
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Create a new instance of the datatype result detail.
        /// </summary>
        public FileResultDetail(ResultDetailType type, string message, string location, Exception exception)
        {
            this.Type = type;
            this.Message = message;
            this.Exception = exception;
            this.Location = location;
        }

        #endregion
    }
}