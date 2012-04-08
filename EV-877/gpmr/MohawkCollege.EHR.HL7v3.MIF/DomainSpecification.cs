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
    /// Used to reference a domain or code associated with another element
    /// </summary>
    [XmlType(TypeName = "DomainSpecification", Namespace = "urn:hl7-org:v3/mif")]
    public class DomainSpecification : Dependancy
    {
        private string domainName;
        private string codeSystemName;
        private string valueSetVersion;
        private string mnemonic;
        private string valueSetName;

        /// <summary>
        /// The formal name or OID for the value set from which content may be drawn
        /// </summary>
        [XmlAttribute("valueSetName")]
        public string ValueSetName
        {
            get { return valueSetName; }
            set { valueSetName = value; }
        }
	
        /// <summary>
        /// The code corresponding to the domain from which the content may be drawn
        /// </summary>
        [XmlAttribute("mnemonic")]
        public string Mnemonic
        {
            get { return mnemonic; }
            set { mnemonic = value; }
        }

	    /// <summary>
	    /// The data defining the valueSetVersion
	    /// </summary>
        [XmlAttribute("valueSetVersion")]
        public string ValueSetVersion
        {
            get { return valueSetVersion; }
            set { valueSetVersion = value; }
        }
	
        /// <summary>
        /// The formal name or OID for the value set from which the content may be drawn
        /// </summary>
        [XmlAttribute("codeSystemName")]
        public string CodeSystemName
        {
            get { return codeSystemName; }
            set { codeSystemName = value; }
        }
	
        /// <summary>
        /// The formal name for the vocabulary domain from which content may be drawn
        /// </summary>
        [XmlAttribute("domainName")]
        public string DomainName
        {
            get { return domainName; }
            set { domainName = value; }
        }
	
    }
}