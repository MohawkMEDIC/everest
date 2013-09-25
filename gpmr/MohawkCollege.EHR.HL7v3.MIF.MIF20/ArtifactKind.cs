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
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Identifies the kind of artifacts that can be packaged
    /// </summary>
    [XmlType(TypeName = "ArtifactKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum ArtifactKind
    {
        /// <summary>
        /// VocabularyModel
        /// </summary>
        [XmlEnum("VO")]
        VO,
        /// <summary>
        /// StoryBoard 
        /// </summary>
        [XmlEnum("SB")]
        SB,
        /// <summary>
        /// ReferenceInformationModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RIM")]
        [XmlEnum("RIM")]
        RIM,
        /// <summary>
        /// DomainInformationModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DIM")]
        [XmlEnum("DIM")]
        DIM,
        /// <summary>
        /// Interface
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IFC")]
        [XmlEnum("IFC")]
        IFC,
        /// <summary>
        /// MessageType
        /// </summary>
        [XmlEnum("MT")]
        MT,
        /// <summary>
        /// ConstrainedInformationModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CIM")]
        [XmlEnum("CIM")]
        CIM,
        /// <summary>
        /// LocalizedInformationModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIM")]
        [XmlEnum("LIM")]
        LIM,
        /// <summary>
        /// DocumentAnalysisModel 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DAM")]
        [XmlEnum("DAM")]
        DAM,
        /// <summary>
        /// Template 
        /// </summary>
        [XmlEnum("TP")]
        TP,
        /// <summary>
        /// Datatype model
        /// </summary>
        [XmlEnum("DT")]
        DT,
        /// <summary>
        /// ImplementableTechnologySpecification 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ITS")]
        [XmlEnum("ITS")]
        ITS, 
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
        /// Application Role
        /// </summary>
        [XmlEnum("AR")]
        AR,
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
        [XmlEnum("DMIM")]
        DMIM,
        /// <summary>
        /// RefinedMessageInformationModel
        /// </summary>
        [XmlEnum("RM")]
        RM,
        /// <summary>
        /// HierarchicalMessageDiscriptor
        /// </summary>
        [XmlEnum("HD")]
        HD
    }
}