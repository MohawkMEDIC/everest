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
 * Date: 01-10-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// The TypeMap attribute overcomes the change of names of types
    /// from R1 to R2 datatypes that have no meaning other than name changes.
    /// For example, a QSI is nothing more than an SXPR with all components
    /// having the operator Intersect. Likewise a QSU is an SXPR with all 
    /// components having operator Union
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class TypeMapAttribute : NamedAttribute
    {

        /// <summary>
        /// Identifies the argument type that must be supplied to trigger the map
        /// </summary>
        public string ArgumentType { get; set; }

    }
}
