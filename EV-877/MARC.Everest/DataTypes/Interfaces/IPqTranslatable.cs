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
 * Date: 01-09-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Identifies classes that can be translated
    /// by a PQ value
    /// </summary>
    public interface IPqTranslatable<T>
    {

        /// <summary>
        /// Translates the value by the specified 
        /// <paramref name="translation"/> by adding
        /// the PQ to the current instance.
        /// </summary>
        /// <remarks>Results in a new instance of <typeparamref name="T"/> 
        /// that is translated by the specified amount</remarks>
        T Translate(PQ translation);

    }
}
