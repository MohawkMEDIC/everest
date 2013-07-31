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
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    //DOC: Documentation Required
    /// <summary>
    /// 
    /// </summary>
    [XmlType(TypeName = "VocabularyValueSetBinding", Namespace = "urn:hl7-org:v3/mif2")]
    public class VocabularyValueSetBinding : VocabularyValueSetRef
    {

        private CodingStrengthKind codingStrength;
        private string revisionFrequency;

        /// <summary>
        /// Indicates how often applications are expected to update their codes to reflect
        /// changes to the value set
        /// </summary>
        [XmlAttribute("revisionFrequency")]
        public string RevisionFrequency
        {
            get { return revisionFrequency; }
            set { revisionFrequency = value; }
        }
	
        /// <summary>
        /// Identifies the level of flexibility the constructor of a model instance has in using 
        /// coded values
        /// </summary>
        [XmlAttribute("codingStrength")]
        public CodingStrengthKind CodingStrength
        {
            get { return codingStrength; }
            set { codingStrength = value; }
        }

        /// <summary>
        /// Identifies the code that is to be used from within the value set.
        /// </summary>
        [XmlAttribute("rootCode")]
        public string RootCode { get; set; }
    }
}