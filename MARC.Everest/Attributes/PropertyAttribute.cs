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
using MARC.Everest.DataTypes;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Indicates how a rendering component should treat the name, conformance, validation of a bound property when rendering.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class PropertyAttribute : NamedAttribute
    {

        /// <summary>
        /// Creates a new instance of the property attribute
        /// </summary>
        public PropertyAttribute()
        {
            this.MaxOccurs = 1;
            this.MinLength = 0;
            this.MaxLength = Int32.MaxValue;
            this.NamespaceUri = "urn:hl7-org:v3";
        }

        /// <summary>
        /// The type of attribute represented by this class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum AttributeAttributeType
        {
            /// <summary>
            /// The attribute is a structural attribute.
            /// </summary>
            Structural,
            /// <summary>
            /// The attribute is a non structural attribute.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NonStructural")]
            NonStructural, 
            /// <summary>
            /// The attribute represents a traversable connection to another structure
            /// </summary>
            Traversable
        }

        /// <summary>
        /// Identifies levels of conformance that an attribute can employ.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum AttributeConformanceType
        {
            /// <summary>
            /// A value must be supplied and must not be null (no null flavors are permitted)
            /// </summary>
            Mandatory,
            /// <summary>
            /// The property must be supported, but data is supplied only when avaialable. (min occurs is 0)
            /// </summary>
            Required,
            /// <summary>
            /// A value must be supplied (min occurs is 1) but a null flavor can be used
            /// </summary>
            Populated,
            /// <summary>
            /// Implementers may choose to not support this concept
            /// </summary>
            Optional
        }

        /// <summary>
        /// Gets or sets the namespace of URI of the property
        /// </summary>
        public string NamespaceUri { get; set; }

        /// <summary>
        /// Gets or sets an identifier of a value set to which valid codes are members.
        /// </summary>
        public string SupplierDomain { get; set; }
        /// <summary>
        /// Gets or sets the attribute type of this property.
        /// </summary>
        public AttributeAttributeType PropertyType { get; set; }
        /// <summary>
        /// Gets or sets a <see cref="System.Type">System.Type</see> that is used when 
        /// choosing which <see cref="MARC.Everest.Attributes.PropertyAttribute">PropertyAttribute</see> 
        /// to use. This <see cref="System.Type">Type</see> will be compared to the type of object in the property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public Type Type { get; set; }
        /// <summary>
        /// Gets or sets a <see cref="System.Type"/> that is signifies the interaction this type must be a member of when determining if the current traversal path should be considered to be rendered into the formatter 
        /// </summary>
        /// <remarks>
        /// This is added to support the fact that HL7v3 allows certain traversals to occur within certain interactions
        /// </remarks>
        public Type InteractionOwner { get; set; }
        /// <summary>
        /// Gets or sets the minimum required conformance of the property when being serialized. If this conformance
        /// is not met, an exception will be thrown.
        /// </summary>
        public AttributeConformanceType Conformance { get; set; }
        /// <summary>
        /// Gets or sets the minimum number of occurences of an element in an array. This value is only used for Arrays.
        /// </summary>
        public int MinOccurs { get; set; }
        /// <summary>
        /// Gets or sets the maximum number of occurences of an element in an array. If the array contains more elements, a valiation error occurs. This value is only used for Arrays.
        /// </summary>
        public int MaxOccurs { get; set; }
        /// <summary>
        /// Gets or sets the minimum length of the default property within a PDV
        /// </summary>
        public int MinLength { get; set; }
        /// <summary>
        /// Gets or sets the maximum length of the default property within a PDV
        /// </summary>
        public int MaxLength { get; set; }
        /// <summary>
        /// Gets or sets the HL7 flavor to impose 
        /// Identifies what flavor to impose on the object. For example, an ImposeFlavorId of "BUS" on an II typed
        /// property dictates that the II.BUS rules should be applied against the property
        /// </summary>
        public string ImposeFlavorId { get; set; }
        /// <summary>
        /// Gets or sets the UpdateMode that is used if one is not explicitly specified.
        /// </summary>
        public UpdateMode DefaultUpdateMode { get; set; }
        /// <summary>
        /// Gets or sets value that specifies whether or not to ignore the traversal, ie: render the property in place of the containing type.
        /// </summary>
        public bool IgnoreTraversal { get; set; }
        /// <summary>
        /// Gets the fixed value for the object
        /// </summary>
        public string FixedValue { get; set; }
        /// <summary>
        /// Sort order for the property
        /// </summary>
        public int SortKey { get; set; }
    }
}
