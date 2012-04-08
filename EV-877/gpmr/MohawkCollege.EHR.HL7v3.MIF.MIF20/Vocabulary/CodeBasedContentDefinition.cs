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
    /// Identifies a specific code to include possibly with related codes
    /// </summary>
    [XmlType(TypeName = "CodeBasedContentDefinition", Namespace = "urn:hl7-org:v3/mif2")]
    public class CodeBasedContentDefinition
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CodeBasedContentDefinition()
        {
            IncludeHeadCode = true;
        }
        /// <summary>
        /// If true, the specified code should be included if false, only related
        /// codes should be included
        /// </summary>
        [XmlAttribute("includeHeadCode")]
        public bool IncludeHeadCode { get; set; }
        /// <summary>
        /// Identifies the specific code to be included in the value set. 
        /// </summary>
        /// <remarks>
        /// Codes for synonyms must be explicity listed to be considered part of the value set
        /// </remarks>
        [XmlAttribute("code")]
        public string Code { get; set; }
        /// <summary>
        /// Identifies relationship types from the current code to other codes that should also 
        /// be included.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [XmlElement("includeRelatedCodes")]
        public List<IncludeRelatedCodes> IncludeRelatedCodes { get; set; }
    }
}