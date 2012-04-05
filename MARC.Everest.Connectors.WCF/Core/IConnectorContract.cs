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
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MARC.Everest.Connectors.WCF.Core
{
    /// <summary>
    /// Represents a generic service contract that will handle any message from any endpoint
    /// and will process it using the connector interface. 
    /// </summary>
    /// <remarks>
    /// This service contract is to be used when the developer wishes to use the WCF connector
    /// in an IIS hosted environment.
    /// </remarks>
    [ServiceContract]
    public interface IConnectorContract
    {
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "m"), OperationContract(Action = "*", ReplyAction = "*")]
        Message ProcessInboundMessage(Message m);
    }
}