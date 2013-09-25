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
 * Date: 21-12-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Interfaces;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Classes the implement the INormalizable interface
    /// represent a hybrid where the structure needs to be 
    /// normalized prior to serialization
    /// </summary>
    public interface INormalizable
    {
        /// <summary>
        /// Normalize the structure
        /// </summary>
        /// <returns></returns>
        IGraphable Normalize();
    }
}
