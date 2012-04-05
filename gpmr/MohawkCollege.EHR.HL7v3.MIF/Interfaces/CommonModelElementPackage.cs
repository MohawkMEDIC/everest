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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.Interfaces
{
    /// <summary>
    /// A package of a common model element interface that can be
    /// imported into a static model for reference
    /// </summary>
    [XmlRoot(ElementName = "commonModelElementPackage", Namespace = "urn:hl7-org:v3/mif")]
    [XmlType(TypeName = "CommonModelElementPackage", Namespace = "urn:hl7-org:v3/mif")]
    public class CommonModelElementPackage : Package
    {

        private List<CommonModelElement> ownedModelElements;

        /// <summary>
        /// One of the CMET definitions included in the common model element
        /// interface package
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("ownedCommonModelElement")]
        public List<CommonModelElement> OwnedElements
        {
            get { return ownedModelElements; }
            set { ownedModelElements = value; }
        }
	
        /// <summary>
        /// Initialize the common model element package 
        /// </summary>
        internal override void Initialize()
        {
            foreach (CommonModelElement cmet in OwnedElements)
                cmet.Container = this;
        }
    }
}