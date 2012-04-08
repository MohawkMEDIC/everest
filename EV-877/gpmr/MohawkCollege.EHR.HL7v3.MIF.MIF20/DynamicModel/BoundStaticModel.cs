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
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.DynamicModel
{
    /// <summary>
    /// Identifies an instance model as well as any models bound to parameterized stubs within the model
    /// </summary>
    [XmlType(TypeName = "BoundStaticModel", Namespace = "urn:hl7-org:v3/mif2")]
    public class BoundStaticModel : PackageRef
    {
        private StaticModelUseKind entryPointUseKind;
        private List<ParameterModel> parameterModel;

        /// <summary>
        /// Identifies all 'payload' models for the current wrapper model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("parameterModel")]
        public List<ParameterModel> ParameterModel
        {
            get { return parameterModel; }
            set { parameterModel = value; }
        }
	
        /// <summary>
        /// Identifies the type of content represented by the model when entered from 
        /// this entry point. The contentType determines whether the model is legitimate content for
        /// a classStuv form another model
        /// </summary>
        [XmlAttribute("entryPointUseKind")]
        public StaticModelUseKind EntryPointUseKind
        {
            get { return entryPointUseKind; }
            set { entryPointUseKind = value; }
        }

    }
}