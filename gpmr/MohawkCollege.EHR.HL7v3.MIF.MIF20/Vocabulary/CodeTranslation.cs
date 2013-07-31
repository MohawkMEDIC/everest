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
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Identifies a known translation between two code systems
    /// </summary>
    [XmlType(TypeName = "CodeTranslation", Namespace = "urn:hl7-org:v3/mif2")]
    public class CodeTranslation : ModelElement
    {
        /// <summary>
        /// An indication of the semantic relationship between the source and target code expressed
        /// in terms of the target
        /// </summary>
        [XmlAttribute("quality")]
        public MapRelationshipKind Quality { get; set; }
        /// <summary>
        /// Indicates in which direction, code translation is being asserted to be permissable
        /// </summary>
        [XmlAttribute("translatableDirection")]
        public TranslatableDirectionKind TranslatableDirection { get; set; }
        /// <summary>
        /// Descriptive information about the code translation
        /// </summary>
        [XmlElement("annotations")]
        public Annotation Annotations { get; set; }
        /// <summary>
        /// The code for which the translation exists
        /// </summary>
        [XmlElement("sourceConcept")]
        public ConceptRef SourceConcept { get; set; }
        /// <summary>
        /// The code being translated to
        /// </summary>
        [XmlElement("targetConcept")]
        public ConceptRef TargetConcept { get; set; }
    }
}