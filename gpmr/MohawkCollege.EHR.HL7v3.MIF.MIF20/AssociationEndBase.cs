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
    /// Corresponds to an association end
    /// </summary>
    [XmlType(TypeName = "AssociationEndBase", Namespace = "urn:hl7-org:v3/mif2")]
    public class AssociationEndBase : Relationship
    {

        private string name;
        private string minimumMultiplicity;
        private string maximumMultiplicity;
        private bool isMandatory;
        private ConformanceKind conformance = ConformanceKind.Optional;
        private UpdateModeKind updateModeDefault;
        private string updateModesAllowed;
        private bool referenceHistory;
        private List<BusinessName> businessName;
        private Annotation annotations;
        private List<AssociationEndDerivation> derivedFrom;
        private List<AssociationEndSpecialization> participantClassSpecialization;

        /// <summary>
        /// For association ends pointing to CMETs whose root is a choice, identifies the classes within the choice and the 
        /// association names tied to those classes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("choiceItem")]
        public List<AssociationEndSpecialization> ChoiceItem
        {
            get { return participantClassSpecialization; }
            set { participantClassSpecialization = value; }
        }
	
        /// <summary>
        /// Identifies the corresponding association in a mode from which the current model has been derived
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("derivedFrom")]
        public List<AssociationEndDerivation> DerivedFrom
        {
            get { return derivedFrom; }
            set { derivedFrom = value; }
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
        /// The business names associated with the element
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("businessName")]
        public List<BusinessName> BusinessName
        {
            get { return businessName; }
            set { businessName = value; }
        }
	
        /// <summary>
        /// Indicates that the element can include a reference to its history
        /// </summary>
        [XmlAttribute("referenceHistory")]
        public bool ReferenceHistory
        {
            get { return referenceHistory; }
            set { referenceHistory = value; }
        }
	
        /// <summary>
        /// Identifies the list of update modes that may be used for this element
        /// </summary>
        [XmlAttribute("updateModesAllowed")]
        public string UpdateModesAllowed
        {
            get { return updateModesAllowed; }
            set { updateModesAllowed = value; }
        }
	
        /// <summary>
        /// Identifies the update mode that should be assumed if no updateMode is specified
        /// </summary>
        [XmlAttribute("updateModeDefault")]
        public UpdateModeKind UpdateModeDefault
        {
            get { return updateModeDefault; }
            set { updateModeDefault = value; }
        }
	
        /// <summary>
        /// Identifies wheter the element must be supported by implementors or not. If not present
        /// indicates that the support is optional
        /// </summary>
        [XmlAttribute("conformance")]
        public ConformanceKind Conformance
        {
            get { return conformance; }
            set { conformance = value; }
        }
	
        /// <summary>
        /// If true, null values may not be sent for this element
        /// </summary>
        [XmlAttribute("isMandatory")]
        public bool IsMandatory
        {
            get { return isMandatory; }
            set { isMandatory = value; }
        }
	
        /// <summary>
        /// Identifies the maximum number of repetitions of this element that may occur within the containing
        /// element
        /// </summary>
        [XmlAttribute("maximumMultiplicity")]
        public string MaximumMultiplicity
        {
            get { return maximumMultiplicity; }
            set { maximumMultiplicity = value; }
        }
	
        /// <summary>
        /// Identifies the minimum number of repetitions of this element that may occur within the containing
        /// element
        /// </summary>
        [XmlAttribute("minimumMultiplicity")]
        public string MinimumMultiplicity
        {
            get { return minimumMultiplicity; }
            set { minimumMultiplicity = value; }
        }
	
        /// <summary>
        /// The unique formal name for this association
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
	
    }
}