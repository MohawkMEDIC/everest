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

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Base attribute that represents a property, class, etc... that has a corresponding RIM name.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors")]
    public abstract class NamedAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the RMIM name of this attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates a new instance of the named attribute.
        /// </summary>
        public NamedAttribute() { }
        /// <summary>
        /// Creates a new instance of the named attribute, supplying the <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The RMIM name of the attribute.</param>
        public NamedAttribute(String name) { this.Name = name; }
    }
}
