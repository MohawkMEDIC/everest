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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Represents table contents
    /// </summary>
    [XmlRoot(ElementName = "vocabularyTableContents", Namespace = "urn:hl7-org:v3/mif2")]
    [XmlType(TypeName = "VocabularyTableContents", Namespace = "urn:hl7-org:v3/mif2")]
    public class VocabularyTableContents : ModelElement
    {
        private string name;
        private List<object> contents;

        /// <summary>
        /// the contents of the table
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "property", Type = typeof(VocabularyProperty))]
        [XmlElement(ElementName = "vocabularyDomain", Type = typeof(VocabularyDomain))]
        [XmlElement(ElementName = "vocabularyCode", Type = typeof(VocabularyCode))]
        public List<object> Contents
        {
            get { return contents; }
            set { contents = value; }
        }
	

        /// <summary>
        /// The name of the vocab contents
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
	
    }
}