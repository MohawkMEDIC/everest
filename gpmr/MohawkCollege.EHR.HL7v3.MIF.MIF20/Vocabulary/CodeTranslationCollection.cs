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
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// A collection of translations created by a particular group
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix"), XmlType(TypeName = "CodeTranslationCollection", Namespace = "urn:hl7-org:v3/mif2")]
    public class CodeTranslationCollection : PackageBase
    {
        /// <summary>
        /// The descriptive name of the package in circumstances where name is more of an identifier
        /// </summary>
        [XmlAttribute("title")]
        public string Title { get; set; }
        /// <summary>
        /// General metadata information about the package
        /// </summary>
        [XmlElement("header")]
        public Header Header { get; set; }
        /// <summary>
        /// Descriptive information about the CodeTranslationCollection
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }
        /// <summary>
        /// Identifies a translation created within the translation package
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("translation")]
        public List<CodeTranslation> Translation { get; set; }

    }
}