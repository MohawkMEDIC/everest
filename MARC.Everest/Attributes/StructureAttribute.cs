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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Identifies a class or structure as belonging to an RMIM structure. Without this attribute, classes
    /// and structures are "not" graphable to an ITS
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = false)]
    public sealed class StructureAttribute : NamedAttribute
    {
        /// <summary>
        /// Identifies the type of structures that can be represented in the RMIM class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum StructureAttributeType
        {
            /// <summary>
            /// The structure represents a message structure
            /// </summary>
            MessageType,
            /// <summary>
            /// The structure represents a data type
            /// </summary>
            DataType,
            /// <summary>
            /// The structure represents an interaction, which can be used as an entry point 
            /// for formatting.
            /// </summary>
            Interaction,
            /// <summary>
            /// Structure represents a value set or a fixed set of
            /// values that can be selected
            /// </summary>
            ValueSet,
            /// <summary>
            /// Structure represents a concept domain or a fixed set of
            /// concepts that can be selected
            /// </summary>
            ConceptDomain,
            /// <summary>
            /// Structure represents a code system or a set of codes
            /// </summary>
            CodeSystem
        }

        /// <summary>
        /// Identifies the type of structure this represents.
        /// </summary>
        public StructureAttributeType StructureType { get; set; }

        /// <summary>
        /// When true, signals that the specified structure can be used as the root of serialization
        /// </summary>
        public bool IsEntryPoint { get; set; }

        /// <summary>
        /// Code system
        /// </summary>
        public string CodeSystem { get; set; }

        /// <summary>
        /// Default template type
        /// </summary>
        public Type DefaultTemplateType { get; set; }

        /// <summary>
        /// The name of the model that contained the file
        /// </summary>
        public String Model { get; set; }

        /// <summary>
        /// Gets the name of the publishing group/organization
        /// </summary>
        public String Publisher { get; set; }
    }
}