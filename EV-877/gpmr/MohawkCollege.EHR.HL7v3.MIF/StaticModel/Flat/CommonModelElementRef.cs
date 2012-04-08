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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.StaticModel.Flat
{
    /// <summary>
    /// SubSystem referencing a particular common model element interface in a flat static model
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance"), XmlType(TypeName = "CommonModelElementRef", Namespace = "urn:hl7-org:v3/mif")]
    public class CommonModelElementRef : CommonModelElementRefBase
    {
        private List<CommonModelElementGeneralization> generalizationParent;

        /// <summary>
        /// Reference the CMET definition being implemented. Plus parameters if appropriate
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("generalizationParent")]
        public List<CommonModelElementGeneralization> GeneralizationParent
        {
            get { return generalizationParent; }
            set { generalizationParent = value; }
        }
	
    }
}