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
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Identifies an entity as implementing the concept qualifier
    /// </summary>
    public interface IConceptQualifier
    {
        /// <summary>
        /// Specifies the manner in which the concept role value contributes to the meaning of a code phrase
        /// </summary>
        ICodedValue Name { get; set; }
        /// <summary>
        /// The concept that modifies the primary code of a code phrase through the role relation
        /// </summary>
        IConceptDescriptor Value { get; set; }
        /// <summary>
        /// Indicatestif the sense of name is inverted
        /// </summary>
        bool Inverted { get; set; }
    }
}