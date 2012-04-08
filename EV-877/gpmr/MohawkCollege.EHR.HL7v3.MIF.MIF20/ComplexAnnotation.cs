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
    /// A complex representation of notes related to the 
    /// </summary>
    [XmlType(TypeName = "ComplexAnnotation", Namespace = "urn:hl7-org:v3/mif2")]
    public class ComplexAnnotation : ModelElement
    {
        private List<ComplexMarkupWithLanguage> text;

        /// <summary>
        /// Text content of the annotion
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("text")]
        public List<ComplexMarkupWithLanguage> Text
        {
            get { return text; }
            set { text = value; }
        }


        #region IComparer<ComplexAnnotation> Members

        /// <summary>
        /// The comparator for the complex annotation type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public new static Comparer Comparator = new Comparer();
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class Comparer : IComparer<ComplexAnnotation>
        {
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(ComplexAnnotation x, ComplexAnnotation y)
            {
                return new ModelElement.Comparator().Compare(x, y);
            }
        }
        #endregion
    }
}