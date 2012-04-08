/* 
 * Copyright 2008/2009 Mohawk College of Applied Arts and Technology
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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10
{
    /// <summary>
    /// Identifies the kind of artifacts that can be packaged
    /// </summary>
    [XmlType(TypeName = "ArtifactKind", Namespace = "urn:hl7-org:v3/mif")]
    public enum ArtifactKind
    {
        /// <summary>
        /// VocabularyModel
        /// </summary>
        [XmlEnum("VO")]
        VO = 0,
        /// <summary>
        /// StoryBoard 
        /// </summary>
        [XmlEnum("SB")]
        SB = 1,
        /// <summary>
        /// ReferenceInformationModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RIM")]
        [XmlEnum("RIM")]
        RIM = 2,
        /// <summary>
        /// DomainInformationModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DIM")]
        [XmlEnum("DIM")]
        DIM = 3,
        /// <summary>
        /// CommonModelElementType
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CMET")]
        [XmlEnum("CMET")]
        CMET = 4,
        /// <summary>
        /// ConstrainedInformationModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CIM")]
        [XmlEnum("CIM")]
        CIM = 5,
        /// <summary>
        /// LocalizedInformationModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIM")]
        [XmlEnum("LIM")]
        LIM = 6,
        /// <summary>
        /// DocumentAnalysisModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DAM")]
        [XmlEnum("DAM")]
        DAM = 7,
        /// <summary>
        /// Template 
        /// </summary>
        [XmlEnum("TP")]
        TP = 8,
        /// <summary>
        /// ImplementableTechnologySpecification 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ITS")]
        [XmlEnum("ITS")]
        ITS = 9, 
        /// <summary>
        /// TemplateParameter
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STUB")]
        [XmlEnum("STUB")]
        STUB,
        /// <summary>
        /// Document
        /// </summary>
        [XmlEnum("DC")]
        DC,
        /// <summary>
        /// Glossary
        /// </summary>
        [XmlEnum("GL")]
        GL,
        /// <summary>
        /// TriggerEvent
        /// </summary>
        [XmlEnum("TE")]
        TE,
        /// <summary>
        /// Interaction
        /// </summary>
        [XmlEnum("IN")]
        IN,
        /// <summary>
        /// DomainMessageInformationModel
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DMIM")]
        [XmlEnum("DMIM-deprecated")]
        DMIM,
        /// <summary>
        /// RefinedMessageInformationModel
        /// </summary>
        [XmlEnum("RM-deprecated")]
        RM,
        /// <summary>
        /// HierarchicalMessageDiscriptor
        /// </summary>
        [XmlEnum("HD-deprecated")]
        HD,
        /// <summary>
        /// MessageType
        /// </summary>
        [XmlEnum("MT-deprecated")]
        MT
    }
}