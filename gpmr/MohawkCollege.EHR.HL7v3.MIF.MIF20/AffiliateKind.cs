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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// The List of HL7 affiliates
    /// </summary>
    [XmlType(TypeName = "AffiliateKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum AffiliateKind
    {
        /// <summary>
        /// Argentina
        /// </summary>
        [XmlEnum("AR")]
        Argentina,
        /// <summary>
        /// Australia
        /// </summary>
        [XmlEnum("AU")]
        Australia,
        /// <summary>
        /// Austria
        /// </summary>
        [XmlEnum("AT")]
        Austria,
        /// <summary>
        /// Brazil
        /// </summary>
        [XmlEnum("BR")]
        Brazil,
        /// <summary>
        /// Canada
        /// </summary>
        [XmlEnum("CA")]
        Canada,
        /// <summary>
        /// Chile
        /// </summary>
        [XmlEnum("CL")]
        Chile,
        /// <summary>
        /// China
        /// </summary>
        [XmlEnum("CN")]
        China,
        /// <summary>
        /// Croatia
        /// </summary>
        [XmlEnum("HR")]
        Croatia,
        /// <summary>
        /// Czech Republic
        /// </summary>
        [XmlEnum("CZ")]
        CzechRepublic,
        /// <summary>
        /// Denmark
        /// </summary>
        [XmlEnum("DK")]
        Denmark,
        /// <summary>
        /// Finland
        /// </summary>
        [XmlEnum("FI")]
        Finland,
        /// <summary>
        /// France
        /// </summary>
        [XmlEnum("FR")]
        France,
        /// <summary>
        /// Germany
        /// </summary>
        [XmlEnum("DE")]
        Germany,
        /// <summary>
        /// Greece
        /// </summary>
        [XmlEnum("GR")]
        Greece,
        /// <summary>
        /// India
        /// </summary>
        [XmlEnum("IN")]
        India,
        /// <summary>
        /// Ireland
        /// </summary>
        [XmlEnum("IE")]
        Ireland,
        /// <summary>
        /// Italy
        /// </summary>
        [XmlEnum("IT")]
        Italy,
        /// <summary>
        /// Japan
        /// </summary>
        [XmlEnum("JP")]
        Japan,
        /// <summary>
        /// Korea
        /// </summary>
        [XmlEnum("KR")]
        Korea,
        /// <summary>
        /// Lithuania
        /// </summary>
        [XmlEnum("LT")]
        Lithuania,
        /// <summary>
        /// Mexico
        /// </summary>
        [XmlEnum("MX")]
        Mexico,
        /// <summary>
        /// Netherlands
        /// </summary>
        [XmlEnum("NL")]
        Netherlands,
        /// <summary>
        /// New Zealand
        /// </summary>
        [XmlEnum("NZ")]
        NewZealand,
        /// <summary>
        /// Romania
        /// </summary>
        [XmlEnum("RO")]
        Romania,
        /// <summary>
        /// South Africa
        /// </summary>
        [XmlEnum("SOA")]
        SouthAfrica,
        /// <summary>
        /// Spain
        /// </summary>
        [XmlEnum("ES")]
        Spain,
        /// <summary>
        /// Sweden
        /// </summary>
        [XmlEnum("SE")]
        Sweden,
        /// <summary>
        /// Swizerland
        /// </summary>
        [XmlEnum("CH")]
        Switzerland,
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("TW")]
        Taiwan,
        /// <summary>
        /// Turkey
        /// </summary>
        [XmlEnum("TR")]
        Turkey,
        /// <summary>
        /// United Kingdom
        /// </summary>
        [XmlEnum("UK")]
        UnitedKingdom,
        /// <summary>
        /// United States
        /// </summary>
        [XmlEnum("US")]
        UnitedStates,
        /// <summary>
        /// Uruguay
        /// </summary>
        [XmlEnum("UY")]
        Uruguay,
        /// <summary>
        /// All Realms
        /// </summary>
        [XmlEnum("UV")]
        AllRealms
    }
}