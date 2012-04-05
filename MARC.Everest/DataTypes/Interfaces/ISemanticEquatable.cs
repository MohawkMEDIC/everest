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
 * Date: 07-27-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Identifies a class that has the ability to compare itself
    /// not only on Equals but on semantic equality
    /// </summary>
    public interface ISemanticEquatable
    {
        /// <summary>
        /// Determines if the instance is equal to <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other object to compare to</param>
        /// <returns>True if the two objects are semantically equal</returns>
        BL SemanticEquals(IAny other);
    }
}
