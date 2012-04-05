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
using System.ComponentModel;

namespace MARC.Everest.DataTypes.Interfaces
{

    /// <summary>
    /// Primitive data value
    /// </summary>
    public interface IPrimitiveDataValue : IAny
    {
        /// <summary>
        /// Get the value of the primitive data vlaue
        /// </summary>
        Object Value { get; set; }
    }

    /// <summary>
    /// Identifies that a class is a PDV
    /// </summary>
    public interface IPrimitiveDataValue<T> : IPrimitiveDataValue
    {
        /// <summary>
        /// Get or set the value of the primitive data value
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        T Value { get; set; }
    }
}