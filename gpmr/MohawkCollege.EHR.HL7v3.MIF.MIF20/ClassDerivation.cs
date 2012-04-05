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
 * User: Justin Fyfe
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// A derivation stereotype to a class
    /// </summary>
    [XmlType(TypeName = "ClassDerivation", Namespace = "urn:hl7-org:v3/mif2")]
    public class ClassDerivation
    {

        private string staticModelDerivationId;
        private string className;
        private string withinCMET;

        /// <summary>
        /// The name of the CMET within which the class was originally found
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CMET")]
        public string WithinCMET
        {
            get { return withinCMET; }
            set { withinCMET = value; }
        }
	
        /// <summary>
        /// The name of the corresponding class in the 'parent' model
        /// </summary>
        [XmlAttribute("className")]
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }
	

        /// <summary>
        /// Refers to the staticModelDerivationId on the parent static model which points to the model 
        /// in which the derived class is found
        /// </summary>
        [XmlAttribute("staticModelDerivationId")]
        public string StaticModelDerivationId
        {
            get { return staticModelDerivationId; }
            set { staticModelDerivationId = value; }
        }
	
    }
}