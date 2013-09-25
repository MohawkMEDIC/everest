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
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    //DOC: Documentation Required
    /// <summary>
    /// 
    /// </summary>
    [XmlType(TypeName = "VocabularySpecification", Namespace = "urn:hl7-org:v3/mif2")]
    public class VocabularySpecification : Dependancy
    {
        private ConceptDomainRef conceptDomain;
        private VocabularyValueSetBinding valueSet;
        private VocabularyCodeRef code;

        /// <summary>
        /// The specific fixed-value code that must be used
        /// </summary>
        [XmlElement("code")]
        public VocabularyCodeRef Code
        {
            get { return code; }
            set { code = value; }
        }
	
        /// <summary>
        /// The vocabulary value set from which content may be drawn
        /// </summary>
        [XmlElement("valueSet")]
        public VocabularyValueSetBinding ValueSet
        {
            get { return valueSet; }
            set { valueSet = value; }
        }
	
        /// <summary>
        /// The vocabulary domain from which the content may be drawn
        /// </summary>
        [XmlElement("conceptDomain")]
        public ConceptDomainRef ConceptDomain
        {
            get { return conceptDomain; }
            set { conceptDomain = value; }
        }
	
    }
}