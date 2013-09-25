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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Code representing a type of relationship between concepts or vocabulary
    /// </summary>
    [XmlType(TypeName = "ConceptRelationshipKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum ConceptRelationshipKind 
    {
        /// <summary>
        /// The child code is a more narrow version of concept represented by the parent code. Must be transitive, irreflexive or antisymmetric
        /// </summary>
        /// <example>
        /// Every child concept is also a valid parent concept
        /// </example>
        Specializes,
        /// <summary>
        /// The child code is a concept represented by the parent code. Used to determine allowed nesting. Must be transitive, irreflexive, or antisymmetric
        /// </summary>
        /// <example>
        /// Component of address components
        /// </example>
        ComponentOf,
        /// <summary>
        /// The child code is a part of a grouping represented by the parent code. Used to determine navigational hierarchy not based on a specialized component relationship.
        /// Must be transitive, irreflexive and antisymmetric
        /// </summary>
        /// <example>
        /// ICD9 Heirarchy
        /// </example>
        GroupedBy,
        /// <summary>
        /// For coded ordinal code systems, indicates that the specified code is considered less than the related code. Used
        /// to determine relationships of coded ordinals. Must be transitive, irreflexive or antisymmetric.
        /// </summary>
        LessThan,
        /// <summary>
        /// Identifies a code that can act as a qualifier for the referenced code both as part of concept definition within the code system
        /// and as part of run-time concept definition to determine concepts allowed for use as qualifiers for a concept within a CD datatype. 
        /// Must be non-transitive, irreflxive
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Definitionally")]
        DefinitionallyQualifies,
        /// <summary>
        /// Same as definitionally qualifies but restricted to only being used at run-time. Must be non-transitive , irreflexive
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Definitionally")]
        NonDefinitionallyQualifies,
        /// <summary>
        /// Inverse of specializes. Only included as a derived relationship
        /// </summary>
        Generalizes,
        /// <summary>
        /// Inverse of component of, only included as a derived relationship
        /// </summary>
        Component,
        /// <summary>
        /// Inverse of grouped by. Only included as a derived relationship
        /// </summary>
        InGroup,
        /// <summary>
        /// Inverse of less than. Included as a derived relationship
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Inversion of definitionally qualifies. Only included as a derived relationship
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Definitionally")]
        DefinitionallyQualifiedBy,
        /// <summary>
        /// Inverse of NonDefinitionallyQualifies. Only included as a derived relationship
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Definitionally")]
        NonDefinitionallyQualifiedBy,
        /// <summary>
        /// The child code has a relationship with the parent that does not allow one of the pre-defined
        /// stereotypical patterns. Any application behavior based on the relationship must be custom-coded.
        /// Transivity, Reflexivity and symmetry must be asserted
        /// </summary>
        Other
    }
}