/* 
 * Copyright 2011 Mohawk College of Applied Arts and Technology
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
 * Date: 06-17-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;

namespace MARC.Everest.Formatters.XML.Datatypes.R1
{
    /// <summary>
    /// Identifies that a property populated within the datatype wasn't rendered
    /// to the output stream by the R1 formatter
    /// </summary>
    /// <remarks>
    /// </remarks>
    [Serializable]
    public class UnsupportedDatatypeR1PropertyResultDetail : UnsupportedDatatypePropertyResultDetail
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        private UnsupportedDatatypeR1PropertyResultDetail() : base() { }
        /// <summary>
        /// Creates a new instance of the unsupported data type result detail
        /// </summary>
        /// <param name="type">The type of result detail</param>
        /// <param name="propertyName">The name of the property that is not supported</param>
        /// <param name="datatypeName">The name of the datatype that is not supported</param>
        /// <param name="location">The location within the instance that that is not supported</param>
        internal UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType type, string propertyName, string datatypeName, string location)
            : base(type, propertyName, datatypeName, location) { }
    }
}
