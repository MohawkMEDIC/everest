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
 * User: fyfej
 * Date: 4/27/2010 12:14:39 PM
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Interfaces;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Represents a structure that contains a set
    /// </summary>
    /// <remarks>
    /// This interface is primarily used by formatters which must be able to populate
    /// a list of a known interface (IGraphable) without knowing the specific 
    /// types in the constructed instance.
    /// </remarks>
    public interface IListContainer
    {

        /// <summary>
        /// The contained list
        /// </summary>
        LIST<IGraphable> ContainedList { get; set; }

    }
}
