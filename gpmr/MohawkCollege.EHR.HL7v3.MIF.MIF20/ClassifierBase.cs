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
    /// Common content shared by classes and class interfaces
    /// </summary>
    [XmlType(TypeName = "ClassifierBase", Namespace = "urn:hl7-org:v3/mif2")]
    public class ClassifierBase : Classifier
    {

        private List<ClassDerivation> derivedFrom;
        private VocabularySpecification supplierStructuralDomain;

        /// <summary>
        /// For classes whose type hierarchy is extended through vocabulary, this identifies the concept that 
        /// corresponds to this physical class
        /// </summary>
        [XmlElement(ElementName = "definingVocabulary")]
        public VocabularySpecification SupplierStructuralDomain
        {
            get { return supplierStructuralDomain; }
            set { supplierStructuralDomain = value; }
        }
	
        /// <summary>
        /// Identifies the corresponding class in a model from which the current model has been 
        /// derived.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "derivedFrom")]
        public List<ClassDerivation> DerivedFrom
        {
            get { return derivedFrom; }
            set { derivedFrom = value; }
        }
	
    }
}