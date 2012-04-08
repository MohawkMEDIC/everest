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
using System.Xml;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10
{
    /// <summary>
    /// Descriptive information about the containing model element
    /// </summary>
    /// <remarks>
    /// Incomplete class. Only items needed to generate output schemas and classes are included.
    /// Many renderers will use very limited annotation data from the MIF Files
    /// </remarks>
    [XmlType(TypeName = "Annotation", Namespace = "urn:hl7-org:v3/mif")]
    public class Annotation
    {

        private List<ComplexAnnotation> description;
        private XmlElement[] notImplemented;
        private List<ComplexAnnotation> definition;
        private List<ComplexAnnotation> rationale;
        private List<ComplexAnnotation> walkthrough;
        private List<ComplexAnnotation> usage;
        private List<AppendixAnnotation> otherAnnotation;
        private List<AppendixAnnotation> appendix;

        /// <summary>
        /// Appendicies
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("appendix")]
        public List<AppendixAnnotation> Appendix
        {
            get { return appendix; }
            set { appendix = value; }
        }

        /// <summary>
        /// Other annotations
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("otherAnnotation")]
        public List<AppendixAnnotation> OtherAnnotation
        {
            get { return otherAnnotation; }
            set { otherAnnotation = value; }
        }

        /// <summary>
        /// Usage notes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("usageNotes")]
        public List<ComplexAnnotation> Usage
        {
            get { return usage; }
            set { usage = value; }
        }
	
        /// <summary>
        /// An overview of the primary and most important contents of the element.
        /// Used to provide broad understanding of the content without detailed review.
        /// It may contain formatting markup.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("walkthrough")]
        public List<ComplexAnnotation> Walkthrough
        {
            get { return walkthrough; }
            set { walkthrough = value; }
        }
	
        /// <summary>
        /// An explaination of why the element is necessary or potentially useful within this context
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("rationale")]
        public List<ComplexAnnotation> Rationale
        {
            get { return rationale; }
            set { rationale = value; }
        }
	

        /// <summary>
        /// An explaination of the meaning of the element
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("definition")]
        public List<ComplexAnnotation> Definition
        {
            get { return definition; }
            set { definition = value; }
        }

        /// <summary>
        /// Any elements not implemented are placed here
        /// </summary>
        /// <remarks>
        /// Hacked to support the annotation class
        /// </remarks>
        /// HACK: To support this
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), XmlAnyElement]
        public XmlElement[] NotImplemented
        {
            get { return notImplemented; }
            set 
            {
                notImplemented = value; 
            }
        }
	
        /// <summary>
        /// An explanation of the associated element
        /// </summary>
        /// <remarks>
        /// This element in the MIF schema is two types that are for all intents purposes identical (since
        /// we are removing graphical representations), so they have been collapsed into one
        /// </remarks>
        /// HACK: In the MIF schema these two elements are of two types (Graphical / Complex); See remarks
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "description", Type = typeof(ComplexAnnotation))]
        public List<ComplexAnnotation> Description
        {
            get { return description; }
            set { description = value; }
        }

    }
}