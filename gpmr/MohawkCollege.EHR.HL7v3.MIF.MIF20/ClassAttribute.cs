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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Corresponds to Attribute
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix"), XmlType(TypeName = "ClassAttribute", Namespace = "urn:hl7-org:v3/mif2")]
    public class ClassAttribute : Feature, IComparer<ClassAttribute>
    {

        /// <summary>
        /// Comparator for a class attribute
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class Comparator : IComparer<ClassAttribute>
        {
            #region IComparer<ClassAttribute> Members
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)")]
            public new int Compare(ClassAttribute x, ClassAttribute y)
            {
                return x.SortKey.CompareTo(y.SortKey);
            }

            #endregion
        }

        private string name;
        private bool isStructural;
        private List<BusinessName> businessName;
        private Annotation annotations;
        private List<AttributeDerivation> derivedFrom;
        private DatatypeRef type;
        private VocabularySpecification vocabulary;
        private bool isImmutable;
        private List<String> enumerationValues;

        /// <summary>
        /// Allowed enumerated values
        /// </summary>
        [XmlElement("enumerationValue")]
        public List<String> EnumerationValues
        {
            get { return enumerationValues; }
            set { enumerationValues = value; }
        }

        /// <summary>
        /// Indicates whether the attribute is permitted to change after the object has been 
        /// instantiated.
        /// </summary>
        [XmlAttribute("isImmutable")]
        public bool IsImmutable
        {
            get { return isImmutable; }
            set { isImmutable = value; }
        }
	
        /// <summary>
        /// References the HL7 vocabulary to define the set of allowed values that may be conveyed by this
        /// attribute
        /// </summary>
        [XmlElement("vocabulary")]
        public VocabularySpecification Vocabulary
        {
            get { return vocabulary; }
            set { vocabulary = value; }
        }
	

        /// <summary>
        /// Identifies the structure that may be used to convey information in an attribute
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods"), XmlElement("type")]
        public DatatypeRef Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Identifies the corresponding attribute in model from which the current model has
        /// been supplied
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("derivedFrom")]
        public List<AttributeDerivation> DerivedFrom
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "businessName")]
        public List<BusinessName> BusinessName
        {
            get { return businessName; }
            set { businessName = value; }
        }
	
        /// <summary>
        /// Indicates whether the attribute is considered essential to the interpretation of the meaning of the class.
        /// Affects how the attribute appears in some ITSs
        /// </summary>
        [XmlAttribute("isStructural")]
        [Obsolete("Will be removed later")]
        public bool IsStructural
        {
            get { return isStructural; }
            set { isStructural = value; }
        }
	
        /// <summary>
        /// The unique, formal name used to identify the attribute within the class and its ancestors
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        #region IComparer<ClassAttribute> Members
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)")]
        public int Compare(ClassAttribute x, ClassAttribute y)
        {
            return x.SortKey.CompareTo(y.SortKey);
        }

        #endregion
    }
}
