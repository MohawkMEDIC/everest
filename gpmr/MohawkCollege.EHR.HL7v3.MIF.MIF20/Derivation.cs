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
    /// Used for all derivedFrom elements
    /// </summary>
    [XmlType(TypeName = "Derivation", Namespace = "urn:hl7-org:v3/mif2")]
    public class Derivation : Dependancy
    {
        private bool areAnnotationsReviewed;
        private string annotationsReviewedBy;
        private DerivationRelationshipKind relationship;
        private ComplexMarkupWithLanguage reason;

        /// <summary>
        /// Identifies the reason why the derived element has changed
        /// </summary>
        [XmlElement("reason")]
        public ComplexMarkupWithLanguage Reason
        {
            get { return reason; }
            set { reason = value; }
        }
	
        /// <summary>
        /// Identifies the relationship between the current element and the element it was derived from
        /// </summary>
        [XmlAttribute("relationship")]
        public DerivationRelationshipKind Relationship
        {
            get { return relationship; }
            set { relationship = value; }
        }
	

        /// <summary>
        /// Indicates the human responsible for the manual review of annotations
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Annoations"), XmlAttribute("annotationsReviewedBy")]
        public string AnnoationsReviewedBy
        {
            get { return annotationsReviewedBy; }
            set { annotationsReviewedBy = value; }
        }
	

        /// <summary>
        /// Indentifies whether the analysis of the derivation has included review of changed annotations. This is false
        /// if (a) the annotations are identical; (b) an automated process was able to take into account the changes
        /// in making the assessment of derivation relationship; or (c) a human has manually reviewed the changes
        /// </summary>
        [XmlAttribute("areAnnotationsReviewed")]
        public bool AreAnnotationsReviewed
        {
            get { return areAnnotationsReviewed; }
            set { areAnnotationsReviewed = value; }
        }
	
    }
}