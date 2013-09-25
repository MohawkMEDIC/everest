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
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// A type of concept property supported by the containing code system for concept
    /// </summary>
    [XmlType(TypeName = "SupportedConceptProperty", Namespace = "urn:hl7-org:v3/mif2")]
    public class SupportedConceptProperty : SupportedProperty
    {
        /// <summary>
        /// Identifies whether the property may be or must be specified by non-codeBaseContent value sets
        /// whose entire content is drawn from this code system
        /// </summary>
        [XmlAttribute("applyToValueSetsIndicator")]
        public bool ApplyToValueSetsIndicator { get; set; }
        /// <summary>
        /// Indicates whether a property value should default based on properties specified further up the 
        /// subsumption hierarchy, or whether they should always be default to the default value specified for the 
        /// property definition
        /// </summary>
        [XmlAttribute("defaultHandlingCode")]
        public string DefaultHandlingCode { get; set; }
    }
}