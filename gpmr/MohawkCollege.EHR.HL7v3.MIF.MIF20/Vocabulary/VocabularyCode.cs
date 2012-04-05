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
    /// Identifies a code bound to a vocabulary system
    /// </summary>
    [XmlType(TypeName = "VocabularyCode", Namespace = "urn:hl7-org:v3/mif2")]
    public class VocabularyCode
    {

        private string mnemonic;
        private List<VocabularyProperty> property;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("property")]
        public List<VocabularyProperty> Property
        {
            get { return property; }
            set { property = value; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
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