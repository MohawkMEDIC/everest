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
using System.Threading;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Provides an AsyncResult class that can be supplied to/from EndReceive/BeginReceive methods
    /// </summary>
    /// <remarks>This class is used to implement the standard asynchronous BeginX/EndX pattern 
    /// used by many .NET frameworks. For more information see the MSDN documentation for 
    /// IAsyncResult</remarks>
    public class ReceiveResultAsyncResult : IAsyncResult
    {
        #region IAsyncResult Members
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public object AsyncState { get; set; }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public System.Threading.WaitHandle AsyncWaitHandle { get; set; }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public bool CompletedSynchronously { get; set; }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public bool IsCompleted { get; set; }
        #endregion

         /// <summary>
        /// Set the operation to complete
        /// </summary>
        public void SetComplete()
        {
            IsCompleted = true;
        }

        /// <summary>
        /// Create a new instance of the SendResultAsyncResult
        /// </summary>
        public ReceiveResultAsyncResult(object asyncState, WaitHandle asyncWaitHandle)
        {
            this.AsyncState = asyncState;
            this.AsyncWaitHandle = asyncWaitHandle;
        }
    }
}