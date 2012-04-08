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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.Vocabulary
{
    /// <summary>
    /// Represents a specific code or mnemonic that is part of a code system.
    /// </summary>
    [XmlType(TypeName = "VocabularyCode", Namespace = "urn:hl7-org:v3/mif")]
    public class VocabularyCode
    {

        /// <summary>
        /// Mnemonic of the code that is supported by the code system.
        /// </summary>
        private string mnemonic;

        /// <summary>
        /// A list of additional properties of the concept. 
        /// A property is expressed as a coded name with an associated value.
        /// </summary>
        private List<VocabularyProperty> property;


        /// <summary>
        /// Gets or sets the list of additional properties of the concept.
        /// </summary>
        /// <value>The list of additional property of the concept.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("property")]
        public List<VocabularyProperty> Property
        {
            get { return property; }
            set { property = value; }
        }


        /// <summary>
        /// Gets or sets the mnemonic.
        /// </summary>
        /// <value>The mnemonic.</value>
        [XmlAttribute("mnemonic")]
        public string Mnemonic
        {
            get { return mnemonic; }
            set { mnemonic = value; }
        }

        /// <summary>
        /// Get a property by name
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Property")]
        public string GetProperty(String PropertyName)
        {
            foreach (VocabularyProperty prop in Property)
                if (prop.Name == PropertyName)
                    return prop.Value;

            return null;
        }
    }
}