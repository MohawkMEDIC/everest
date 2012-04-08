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

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Indicates how to represent an enumeration or field value when graphing to an ITS.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumerationAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the resulting value what will appear in the output of the ITS when graphed.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the supplier domain that this value belongs to
        /// </summary>
        public string SupplierDomain { get; set; }
    }
}
