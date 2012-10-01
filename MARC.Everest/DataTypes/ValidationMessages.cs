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
 * User: Justin Fyfe
 * Date: 01-07-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Validation messages for common errors that occur in the framework
    /// </summary>
    internal class ValidationMessages
    {
        internal const string MSG_NULLFLAVOR_WITH_VALUE = "Data type cannot carry a value when NullFlavor is present";
        internal const string MSG_NULLFLAVOR_MISSING = "Data type must carry a NullFlavor when no value is present";
        internal const string MSG_CODE_REQUIRES_CODESYSTEM = "When Code is present, CodeSystem must also be present";
        internal const string MSG_PROPERTY_NOT_PERMITTED = "{0} is not permitted on property {1}";
        internal const string MSG_PROPERTY_NOT_POPULATED = "{0} must be populated and must contain a valid instance of {1}";
        internal const string MSG_INVALID_VALUE = "'{0}' is not a valid value for {1}";
        internal const string MSG_DEPENDENT_VALUE_MISSING = "{0} cannot be populated unless {1} is populated";
        internal const string MSG_NULL_COLLECTION_VALUE = "Collection item cannot be null or null-flavored";
    }
}
