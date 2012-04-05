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
    /// Represents the kind of subsection
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SubSection"), XmlType(TypeName = "SubsectionKind", Namespace = "urn:hl7-org:v3/mif")]
    public enum SubSectionKind
    {
        /// <summary>
        /// Common
        /// </summary>
        [XmlEnum("CO")]
        CO,
        /// <summary>
        /// Financial
        /// </summary>
        [XmlEnum("FI")]
        FI,
        /// <summary>
        /// MessageControl
        /// </summary>
        [XmlEnum("MC")]
        MC,
        /// <summary>
        /// MasterFile
        /// </summary>
        [XmlEnum("MF")]
        MF,
        /// <summary>
        /// Operations
        /// </summary>
        [XmlEnum("PO")]
        PO,
        /// <summary>
        /// Practice
        /// </summary>
        [XmlEnum("PR")]
        PR,
        /// <summary>
        /// Query
        /// </summary>
        [XmlEnum("QU")]
        QU,
        /// <summary>
        /// Records
        /// </summary>
        [XmlEnum("RC")]
        RC,
        /// <summary>
        /// Reasoning
        /// </summary>
        [XmlEnum("RE")]
        RE,
        /// <summary>
        /// Unknown
        /// </summary>
        [XmlEnum("UU")]
        UU
    }
}