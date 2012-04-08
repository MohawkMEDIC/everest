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
    //DOC: Documentation Required
    /// <summary>
    /// 
    /// </summary>
    [XmlType(TypeName = "CodingStrengthKind", Namespace = "urn:hl7-org:v3/mif")]
    public enum CodingStrengthKind
    {
        /// <summary>
        /// If not null, the element must be coded and must be drawn from the set of codes determined
        /// by the identified domain and the realm in which the instance is constructed. The set of code
        /// is fixed to those values in place at the time this model was successfully balloted
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CNE")]
        [XmlEnum("CNE")]
        CNE,
        /// <summary>
        /// If not null, the element must be coded if there is an appropriate value in the set of codes
        /// deteremined by the identified domain and the realm in which the instance is constructed. If
        /// no appropriate code is available, a local code may be used or the value may be populated with
        /// only original text
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CWE")]
        [XmlEnum("CWE")]
        CWE
    }
}