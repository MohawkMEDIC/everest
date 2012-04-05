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
    //DOC: Documentation Required
    /// <summary>
    /// 
    /// </summary>
    [XmlType(TypeName = "VocabularyCodeRef", Namespace = "urn:hl7-org:v3/mif2")]
    public class VocabularyCodeRef
    {
        private string codeSystem;
        private string codeSystemName;
        private string code;
        private string codePrintName;
        private string codeSystemVersion;

        /// <summary>
        /// The date on which the version of the code systems containing the code being referenced 
        /// was published
        /// </summary>
        [XmlAttribute("codeSystemVersion")]
        public string CodeSystemVersion
        {
            get { return codeSystemVersion; }
            set { codeSystemVersion = value; }
        }
	
        /// <summary>
        /// The descriptive name of the code
        /// </summary>
        [XmlAttribute("codePrintName")]
        public string CodePrintName
        {
            get { return codePrintName; }
            set { codePrintName = value; }
        }
	
        /// <summary>
        /// The unique code or mnemonic from the CodeSystem
        /// </summary>
        [XmlAttribute("code")]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
	
        /// <summary>
        /// The descriptive name for the code system in which the code is defined
        /// </summary>
        [XmlAttribute("codeSystemName")]
        public string CodeSystemName
        {
            get { return codeSystemName; }
            set { codeSystemName = value; }
        }
	
        /// <summary>
        /// The identifier for the code system in which the code is defined
        /// </summary>
        [XmlAttribute("codeSystem")]
        public string CodeSystem
        {
            get { return codeSystem; }
            set { codeSystem = value; }
        }
	
    }
}