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
using System.ComponentModel;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Identifies a class as an instance Identifier
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IInstanceIdentifier
    {
        /// <summary>
        /// The root of the instance identifier
        /// </summary>
        string Root { get; set; }
        /// <summary>
        /// The extension of the instance identifier
        /// </summary>
        string Extension { get; set; }
        /// <summary>
        /// The use of the instance identifier
        /// </summary>
        IdentifierUse? Use { get; set; }
        /// <summary>
        /// True if the instance identifier is intended for display
        /// </summary>
        bool? Displayable { get; set; }
        /// <summary>
        /// The name of the authority that assigned the identifier
        /// </summary>
        string AssigningAuthorityName { get; set; }
    }
}