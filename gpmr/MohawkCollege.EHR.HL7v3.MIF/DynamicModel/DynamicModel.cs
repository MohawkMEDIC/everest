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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.DynamicModel
{
    /// <summary>
    /// A dynamic HL7v3 Model
    /// </summary>
    /// <remarks>
    /// For some reason, the dmif files that I have a copy of don't match any dynamic
    /// model schemas provided. So this class is a reverse engineered version of those files
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces"), XmlRoot(ElementName = "dynamicModel", Namespace = "urn:hl7-org:v3/mif")]
    [XmlType(TypeName = "DynamicModel", Namespace = "urn:hl7-org:v3/mif")]
    public class DynamicModel : Package
    {

        private List<Hl7Interaction> hl7Interaction;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Hl"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("hl7Interaction")]
        public List<Hl7Interaction> Hl7Interaction
        {
            get { return hl7Interaction; }
            set { hl7Interaction = value; }
        }

        /// <summary>
        /// Initialize
        /// </summary>
        internal override void Initialize()
        {
           
        }
    }
}