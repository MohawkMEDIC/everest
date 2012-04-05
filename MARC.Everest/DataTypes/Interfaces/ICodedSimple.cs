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
using MARC.Everest.DataTypes.Primitives;

namespace MARC.Everest.DataTypes.Interfaces
{

    /// <summary>
    /// Represents basic data for a coded simple
    /// </summary>
    public interface ICodedSimple
    {
        /// <summary>
        /// Get the code value
        /// </summary>
        object CodeValue { get; set; }
    }

    /// <summary>
    /// Identifies a class as implementing coded simple
    /// </summary>
    /// <typeparam name="T">The code system the codes may come from</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ICodedSimple<T> : ICodedSimple
    {
        /// <summary>
        /// The code mnemonic
        /// </summary>
        CodeValue<T> Code { get; set; }

    }
}