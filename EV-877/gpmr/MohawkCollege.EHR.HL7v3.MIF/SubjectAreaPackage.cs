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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10
{
    /// <summary>
    /// Defines the content for a subject area
    /// </summary>
    /// <remarks>
    /// Graphic elements have been removed to ease implementation
    /// </remarks>
    [XmlType(TypeName = "SubjectAreaPackage", Namespace = "urn:hl7-org:v3/mif")]
    public class SubjectAreaPackage : PackageBase
    {
        private Annotation annotation;
        private List<SubjectAreaPackage> ownedSubjectAreaPackage;
        private List<LocalClassRef> ownedClass;

        /// <summary>
        /// Classes that are part of the subject area
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "ownedClass")]
        public List<LocalClassRef> OwnedClass
        {
            get { return ownedClass; }
            set { ownedClass = value; }
        }
	
        /// <summary>
        /// Identifies a sub-package owned by this current static package. All classes within the sub-packages
        /// are always imported into their parent static package. This means the name of all classes within a static
        /// package must be unique. Graphically this represents a grouping of classes that may be represented on a 
        /// separate page
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "ownedSubjectAreaPackage")]
        public List<SubjectAreaPackage> OwnedSubjectAreaPackage
        {
            get { return ownedSubjectAreaPackage; }
            set { ownedSubjectAreaPackage = value; }
        }
	

        /// <summary>
        /// Descriptive information about this subject area
        /// </summary>
        [XmlElement(ElementName = "annotations")]
        public Annotation Annotation
        {
            get { return annotation; }
            set { annotation = value; }
        }
	
    }
}