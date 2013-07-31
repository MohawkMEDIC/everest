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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Indicates in which translation can occur
    /// </summary>
    [XmlType(TypeName = "TranslatableDirectionKind", Namespace = "urn:hl7-org:v3/mif2")]
    public enum TranslatableDirectionKind
    {
        /// <summary>
        /// Translation can occur from the source concept to the target concept
        /// </summary>
        [XmlEnum("sourceToTarget")]
        SourceToTarget,
        /// <summary>
        /// Translation can occur from the target concept to the source concept
        /// </summary>
        [XmlEnum("targetToSource")]
        TargetToSource,
        /// <summary>
        /// Translation can occur from both the source and target concept.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Bi")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BiDirectional")]
        [XmlEnum("bi-directional")]
        BiDirectional
    }
}