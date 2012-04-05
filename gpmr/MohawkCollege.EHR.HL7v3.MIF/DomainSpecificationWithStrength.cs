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
    /// Identifies the vocabulary concept that is the root for the content of an attribute. If the 
    /// concept is identified by multiple means (eg. conceptId, and domainName) it is the responsibility
    /// of the user to ensure that both point to the same concept
    /// </summary>
    [XmlType(TypeName = "DomainSpecificationWithStrength", Namespace = "urn:hl7-org:v3/mif")]
    public class DomainSpecificationWithStrength : DomainSpecification
    {
        private CodingStrengthKind? codingStrength;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("codingStrength")]
        public string CodingStrengthSerialization
        {
            get
            {
                return codingStrength == null ? null : codingStrength.ToString();
            }
            set
            {
                if (value == null) codingStrength = null;
                else
                    codingStrength = (CodingStrengthKind)Enum.Parse(typeof(CodingStrengthKind), value);
            }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public CodingStrengthKind? CodingStrength
        {
            get { return codingStrength; }
            set { codingStrength = value; }
        }
	
    }
}