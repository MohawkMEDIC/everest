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
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary
{
    /// <summary>
    /// Indicates whether a vocabulary model is a complete model or a subset
    /// </summary>
    [XmlType(TypeName = "VocabularyModelDefinitionKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum VocabularyModelDefinitionKind
    {
        /// <summary>
        /// TODO: Find documentation
        /// </summary>
        [XmlEnum("complete")]
        Complete,
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("partial-validation")]
        PartialValidation,
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("partial-publishing")]
        PartialPublishing,
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("partial-implementation")]
        PartialImplementation
    }
}