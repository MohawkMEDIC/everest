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
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Indicates the approval status of the artifacts
    /// </summary>
    [XmlType(TypeName = "ApprovalInfo", Namespace = "urn:hl7-org:v3/mif2")]
    public class ApprovalInfo
    {
        private string status;
        private DateTime approvalDate;

        /// <summary>
        /// Indicates the date of the intended ballot or the date on which the material was successfully balloted.
        /// </summary>
        [XmlAttribute("approvalDate")]
        public DateTime ApprovalDate
        {
            get { return approvalDate; }
            set { approvalDate = value; }
        }
	
        /// <summary>
        /// Identifies how far along in the ballot process this artifact has progressed
        /// </summary>
        [XmlAttribute("status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
	
    }
}