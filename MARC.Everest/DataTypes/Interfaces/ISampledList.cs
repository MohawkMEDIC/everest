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
 * Date: 02-14-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Sampled list
    /// </summary>
    public interface ISampledList : IAny
    {

        /// <summary>
        /// Gets or sets the origin
        /// </summary>
        object Origin { get; set; }

        /// <summary>
        /// Gets or sets the scale
        /// </summary>
        IQuantity Scale { get; set; }

        /// <summary>
        /// Gets or sets the digits
        /// </summary>
        List<INT> Items { get; }
    }
}
