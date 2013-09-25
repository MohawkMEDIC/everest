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
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.DynamicModel
{
    /// <summary>
    /// The allowed uses for a particular static model
    /// </summary>
    [XmlType(TypeName = "StaticModelUseKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum StaticModelUseKind
    {
        /// <summary>
        /// Defines a communication structure for other content
        /// </summary>
        CommunicationWrapper,
        /// <summary>
        /// Defines common information regarding acts upon a particular payload
        /// </summary>
        ControlActWrapper,
        /// <summary>
        /// A set of content that is the focus of a message or document
        /// </summary>
        SemanticPayload,
        /// <summary>
        /// Defines the organization and appearence of an underlying model
        /// </summary>
        PresentationStructure,
        /// <summary>
        /// Defines a set of data being solicited
        /// </summary>
        QueryDefinition,
        /// <summary>
        /// A re-usable static structure component
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CMET")]
        CMET,
        /// <summary>
        /// A pattern that can be applied to other models
        /// </summary>
        Template
    }
}