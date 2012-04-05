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
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Indicates allowed values to identify how a concept can be used
    /// </summary>
    [XmlType(TypeName = "ConceptUseKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum ConceptUseKind
    {
        /// <summary>
        /// Indicates that the concept is not selectable nore intended to be displayed to the user
        /// when navigating the code system. It's sole purpose is to support analysis by grouping 
        /// like concepts for semantic reasons
        /// </summary>
        Analysis,
        /// <summary>
        /// Indicates that the concept is selctable; it is used to communicate or persist information
        /// </summary>
        Communication,
        /// <summary>
        /// Indicates that the concept is not selectable, but is displayable to users to help
        /// them navigate a hierarchy of concepts to find a concept that can be selected.
        /// </summary>
        Navigation
    }
}