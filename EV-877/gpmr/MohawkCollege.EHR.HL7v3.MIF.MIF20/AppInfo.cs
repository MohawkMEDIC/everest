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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Contains complex comments relating to a model element
    /// </summary>
    [XmlType(TypeName = "AppInfo", Namespace = "urn:hl7-org:v3/mif2")]
    public class AppInfo
    {
        /// <summary>
        /// A formal, testable limitation on the use,
        /// representation or value associated with the current element
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("formalConstraint")]
        public List<FormalContraint> FormalConstraint { get; set; }

        /// <summary>
        /// Information relating to the depreciation of the element including
        /// instructions on why the element is no longer required.
        /// </summary>
        [XmlElement("deprecationInfo")]
        public DeprecationInfo DeprecationInfo { get; set; }
    }
}