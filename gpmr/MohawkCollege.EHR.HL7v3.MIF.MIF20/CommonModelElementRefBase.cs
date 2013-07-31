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
    /// A reference to a CMET and possibly binding parameters
    /// </summary>
    [XmlType(TypeName = "CommonModelElementRefBase", Namespace = "urn:hl7-org:v3/mif2")] 
    public class CommonModelElementRefBase : ClassRoot
    {

        private string name;
        private string cmetName;
        private Annotation annotations;


        /// <summary>
        /// The name of the CMET reference
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cmet"), XmlAttribute("cmetName")]
        public string CmetName
        {
            get { return cmetName; }
            set { cmetName = value; }
        }
        
        /// <summary>
        /// Descriptive information about the containing element    
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations
        {
            get { return annotations; }
            set { annotations = value; }
        }
	
        /// <summary>
        /// The unique name for this CMET reference within the model (this is usually the same
        /// as the CMET name. However, where a CMET has exit points and different occurences have different bindings
        /// each separately bound occurence must have its own name)
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get {  return name; }
            set { name = value; }
        }
	

    }
}