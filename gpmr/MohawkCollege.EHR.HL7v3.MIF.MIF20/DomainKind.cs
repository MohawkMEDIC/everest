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
    /// Types of domains
    /// </summary>
    [XmlType(TypeName = "DomainKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum DomainKind
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [XmlEnum("DD")]
        DD,
        /// <summary>
        /// AccountingAndBilling
        /// </summary>
        [XmlEnum("AB")]
        AB,
        /// <summary>
        /// TriggerEventControlAct
        /// </summary>
        [XmlEnum("AI")]
        AI,
        /// <summary>
        /// BloodBank
        /// </summary>
        [XmlEnum("BB")]
        BB,
        /// <summary>
        /// ClinicalDocumentArchitecture
        /// </summary>
        [XmlEnum("CD")]
        CD,
        /// <summary>
        /// TransmissionInfrastructure
        /// </summary>
        [XmlEnum("CI")]
        CI,
        /// <summary>
        /// ClinicalGenomics
        /// </summary>
        [XmlEnum("CG")]
        CG,
        /// <summary>
        /// ClaimsAndReimbusment
        /// </summary>
        [XmlEnum("CR")]
        CR,
        /// <summary>
        /// ClinicalStatement
        /// </summary>
        [XmlEnum("CS")]
        CS,
        /// <summary>
        /// CommonTypes
        /// </summary>
        [XmlEnum("CT")]
        CT,
        /// <summary>
        /// CommonProduct Model
        /// </summary>
        [XmlEnum("CP")]
        CP,
        /// <summary>
        /// DiagnosticImaging
        /// </summary>
        [XmlEnum("DI")]
        DI,
        /// <summary>
        /// DecisionSupport
        /// </summary>
        [XmlEnum("DS")]
        DS,
        /// <summary>
        /// ImagingIntegration
        /// </summary>
        [XmlEnum("II")]
        II,
        /// <summary>
        /// Immunization
        /// </summary>
        [XmlEnum("IZ")]
        IZ,
        /// <summary>
        /// Laboratory
        /// </summary>
        [XmlEnum("LB")]
        LB,
        /// <summary>
        /// Medication
        /// </summary>
        [XmlEnum("ME")]
        ME,
        /// <summary>
        /// MasterfileInfrastructure
        /// </summary>
        [XmlEnum("MI")]
        MI,
        /// <summary>
        /// MaterialsManagement
        /// </summary>
        [XmlEnum("MM")]
        MM,
        /// <summary>
        /// MedicalRecords
        /// </summary>
        [XmlEnum("MR")]
        MR,
        /// <summary>
        /// SharedMessages
        /// </summary>
        [XmlEnum("MT")]
        MT,
        /// <summary>
        /// OrdersAndObservations
        /// </summary>
        [XmlEnum("OO")]
        OO,
        /// <summary>
        /// PatientAdministration
        /// </summary>
        [XmlEnum("PA")]
        PA,
        /// <summary>
        /// CareProvision
        /// </summary>
        [XmlEnum("PC")]
        PC,
        /// <summary>
        /// PersonnelManagement
        /// </summary>
        [XmlEnum("PM")]
        PM,
        /// <summary>
        /// QueryInfrastructure
        /// </summary>
        [XmlEnum("QI")]
        QI,
        /// <summary>
        /// InformativePublicHealth
        /// </summary>
        [XmlEnum("RI")]
        RI,
        /// <summary>
        /// RegulatedProducts
        /// </summary>
        [XmlEnum("RP")]
        RP,
        /// <summary>
        /// PublicHealthReporting
        /// </summary>
        [XmlEnum("RR")]
        RR,
        /// <summary>
        /// RegulatedStudies
        /// </summary>
        [XmlEnum("RT")]
        RT,
        /// <summary>
        /// Pharmacy
        /// </summary>
        [XmlEnum("RX")]
        RX,
        /// <summary>
        /// Scheduling
        /// </summary>
        [XmlEnum("SC")]
        SC,
        /// <summary>
        /// Specimen
        /// </summary>
        [XmlEnum("SP")]
        SP,
        /// <summary>
        /// TherapeuticDevices
        /// </summary>
        [XmlEnum("TD")]
        TD,
        /// <summary>
        /// Location
        /// </summary>
        [XmlEnum("LO")]
        LO
    }
}