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
    /// Defines a binding between a value set and a concept domain for resolution based
    /// on the realm-based context instance
    /// </summary>
    [XmlType (TypeName = "ContextBinding", Namespace = "urn:hl7-org:v3/mif2")]
    public class ContextBinding : ModelElement
    {
        /// <summary>
        /// Identifies the concept domain being bound to
        /// </summary>
        [XmlAttribute("conceptDomain")]
        public string ConceptDomain { get; set; }
        /// <summary>
        /// Identifies the value set being bound to the concept domain
        /// </summary>
        [XmlAttribute("valueSet")]
        public string ValueSet { get; set; }
        /// <summary>
        /// Identifies the specific version of the value set being bound. If missing, this is a 
        /// dynamic binding.
        /// </summary>
        [XmlAttribute("versionDate")]
        public DateTime VersionDate { get; set; }
        /// <summary>
        /// Supplements the versionDate to distinguish value set version where multiples occur
        /// on a single date
        /// </summary>
        [XmlAttribute("versionTime")]
        public DateTime VersionTime { get; set; }
        /// <summary>
        /// Identifies the binding realm in whose context the binding is occured
        /// </summary>
        [XmlAttribute("bindingRealmName")]
        public string BindingRealmName { get; set; }
        /// <summary>
        /// Identifies whether codes are restricted to those expressed in the value set or not
        /// </summary>
        [XmlAttribute("codingStrength")]
        public CodingStrengthKind CodingStrength { get; set; }
        /// <summary>
        /// Identifies the relative preference for the coding system within the context. Lower numbers
        /// indicates higher preference. Implementers are strongly encouraged to use the most
        /// preferred code system
        /// </summary>
        [XmlAttribute("bindingPriority")]
        public int BindingPriority { get; set; }
        /// <summary>
        /// Indicates when this binding first takes effect
        /// </summary>
        [XmlAttribute("effectiveDate")]
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// Indicates the date on which this binding is no longer effective
        /// </summary>
        [XmlAttribute("expiryDate")]
        public DateTime ExpiryDate { get; set; }
    }
}