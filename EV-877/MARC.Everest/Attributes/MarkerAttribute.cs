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
    /// Indicates a property as marking a special HL7 property such as updateMode, classCode, etc...
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MarkerAttribute : NamedAttribute
    {
        /// <summary>
        /// Indicates the types of markers supported.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum MarkerAttributeType
        {
            /// <summary>
            /// The property identifies the flavour of the type.
            /// </summary>
            Flavor, 
            /// <summary>
            /// The property identifies the NullFlavor attribute.
            /// </summary>
            NullFlavor, 
            /// <summary>
            /// The property identifies the update mode attribute.
            /// </summary>
            UpdateMode,
            /// <summary>
            /// The property identifies the context conduction attribute.
            /// </summary>
            ContextConduction, 
            /// <summary>
            /// The property identifies the class code attribute.
            /// </summary>
            ClassCode, 
            /// <summary>
            /// The property identifies the type code attribute.
            /// </summary>
            TypeCode,
            /// <summary>
            /// The property identifies the determiner code attribute.
            /// </summary>
            DeterminerCode, 
            /// <summary>
            /// Property represents the data within the datatype.
            /// </summary>
            Data
        }

        /// <summary>
        /// Gets or sets the type of marker identified.
        /// </summary>
        public MarkerAttributeType MarkerType { get; set; }
    }
}