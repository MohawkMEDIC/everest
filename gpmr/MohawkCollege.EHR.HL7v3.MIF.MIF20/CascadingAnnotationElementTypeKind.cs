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
    //DOC: Documentation Required
    /// <summary>
    /// 
    /// </summary>
    [XmlType("ElementTypeKind",  Namespace = "urn:hl7-org:v3/mif2")]
    public enum CascadingAnnotationElementTypeKind
    {
        /// <summary>
        /// Static models
        /// </summary>
        StaticModel,
        /// <summary>
        /// Classes in a static model
        /// </summary>
        Class,
        /// <summary>
        /// Stub references in a static model
        /// </summary>
        Stub,
        /// <summary>
        /// CMET references in a static model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CMET")]
        CMET, 
        /// <summary>
        /// Entry points in a static model
        /// </summary>
        EntryPoint,
        /// <summary>
        /// Attributes in a static model class
        /// </summary>
        Attribute,
        /// <summary>
        /// Associations in a static model
        /// </summary>
        Association,
        /// <summary>
        /// Ends of an association in a static model
        /// </summary>
        AssociationEnd,
        /// <summary>
        /// Generalizations in a static model
        /// </summary>
        Generalization,
        /// <summary>
        /// State machines in a static model
        /// </summary>
        StateMachine,
        /// <summary>
        /// States in a state machine in a static model
        /// </summary>
        State,
        /// <summary>
        /// State transitions in a state machine in a static model
        /// </summary>
        StateTransition,
        /// <summary>
        /// Datatypes in a datatype model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Datatype")]
        Datatype,
        /// <summary>
        /// Operations within a datatype model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Datatype")]
        DatatypeOperation
    }
}