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
    /// Indicates that a method should be used to validate an instance of the named flavor. Note that the 
    /// method is expected to be a <see cref="T:System.Predicate">Predicate&lt;Type&gt;</see>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class FlavorAttribute : NamedAttribute
    {
        /// <summary>
        /// Creates a new instance of the FlavorAttribute class
        /// </summary>
        public FlavorAttribute() { }
        /// <summary>
        /// Creates a new instance of the FlavorAttribute class with the specified parameters
        /// </summary>
        public FlavorAttribute(string name)
        {
            this.Name = name;
        }
        
    }
}
