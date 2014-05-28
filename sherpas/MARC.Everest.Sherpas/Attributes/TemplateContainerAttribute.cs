/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 6-5-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Identifies a class as a container for a template
    /// </summary>
    /// <remarks>This is used by the formatter to ensure that deserialization of containers 
    /// works properly</remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = true)]
    public class TemplateContainerAttribute : Attribute
    {
        /// <summary>
        /// The type of template that this encapsulates
        /// </summary>
        public Type TemplateType { get; set; }

        /// <summary>
        /// The name of the property to which the template type is applied
        /// </summary>
        public String PropertyName { get; set; }
    }
}
