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
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// No documentation found in the MIFv2 XSD
    /// </summary>
    [XmlType(TypeName = "AnnotationCascadeInfo", Namespace = "urn:hl7-org:v3/mif2")]
    public class AnnotationCascadeInfo : PackageRef
    {

        private CascadingAnnotationElementTypeKind elementType;
        private string classifierName;
        private bool classifierNameExact;
        private string rimFeatureName;
        private string targetClassName;
        private bool targetClassNameExact;
        private string contextClassName;
        private DatatypeRef datatype;

        /// <summary>
        /// Contains the annotation to only apply to attributes whose type matches the 
        /// specified datatype
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Datatype"), XmlElement("datatype")]
        public DatatypeRef Datatype
        {
            get { return datatype; }
            set { datatype = value; }
        }
	
        /// <summary>
        /// Indicates the name of the parent class of the class
        /// to which the annotation applies
        /// </summary>
        [XmlAttribute("contextClassName")]
        public string ContextClassName
        {
            get { return contextClassName; }
            set { contextClassName = value; }
        }

	    /// <summary>
	    /// Inidicates whether the target class name must match the name exactly or
        /// can match it approximately based on classCode and/or mood for the
        /// annotation to apply
	    /// </summary>
        [XmlAttribute("targetClassNameExact")]
        public bool TargetClassNameExact
        {
            get { return targetClassNameExact; }
            set { targetClassNameExact = value; }
        }
	
        /// <summary>
        /// Identifies the name of the target class of the association.
        /// </summary>
        [XmlAttribute("targetClassName")]
        public string TargetClassName
        {
            get { return targetClassName; }
            set { targetClassName = value; }
        }

        /// <summary>
        /// Identifies the RIM name of the attribute or association end
        /// </summary>
        [XmlAttribute("rimFeatureName")]
        public string RimFeatureName
        {
            get { return rimFeatureName; }
            set { rimFeatureName = value; }
        }

        /// <summary>
        /// Indicates whether the class name must match the name exactly, or
        /// can match it approximately based on class code and/or mood for the
        /// annotation to apply.
        /// </summary>
        [XmlAttribute("classifierNameExact")]
        public bool ClassifierNameExact
        {
            get { return classifierNameExact; }
            set { classifierNameExact = value; }
        }
	
        /// <summary>
        /// Identifies the type of class or datatype or general class the annotation applies to
        /// </summary>
        [XmlAttribute("classifierName")]
        public string ClassifierName
        {
            get { return classifierName; }
            set { classifierName = value; }
        }
	
        /// <summary>
        /// Indentifies what sort of element this annotation applies to
        /// </summary>
        [XmlAttribute("elementType")]
        public CascadingAnnotationElementTypeKind ElementType
        {
            get { return elementType; }
            set { elementType = value; }
        }
	

    }
}