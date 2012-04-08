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
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Represents a coded value
    /// </summary>
    public interface ICodedValue : ICodedSimple, IOriginalText
    {

        /// <summary>
        /// The code system this code mnemonic comes from
        /// </summary>
        string CodeSystem { get; set; }
        /// <summary>
        /// The name of the code system
        /// </summary>
        string CodeSystemName { get; set; }
        /// <summary>
        /// The version of the code system
        /// </summary>
        string CodeSystemVersion { get; set; }
        /// <summary>
        /// Display name for the code
        /// </summary>
        ST DisplayName { get; set; }
        /// <summary>
        /// Identifies the value set that was applicable at the time of coding
        /// </summary>
        string ValueSet { get; set; }
        /// <summary>
        /// Identifies the version of the value set that was applicable at the time of coding
        /// </summary>
        string ValueSetVersion { get; set; }
        /// <summary>
        /// Addtional groups of logically related qualifiers
        /// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //LIST<CDGroup> Group { get; set;  }
        /// <summary>
        /// Coding rationale
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        SET<CodingRationale> CodingRationale { get; set;  }
    }
}