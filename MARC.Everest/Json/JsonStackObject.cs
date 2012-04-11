/* 
 * Copyright 2012 Mohawk College of Applied Arts and Technology
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
 * Author: Justin Fyfe
 * Date: 02-23-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Json
{

    /// <summary>
    /// Types of JsonObjects
    /// </summary>
    public enum JsonObjectType
    {
        Attribute,
        Element,
        Array
    }

    /// <summary>
    /// Represents a Json object within the Json Stack
    /// </summary>
    public class JsonStackObject
    {

        /// <summary>
        /// Creates a new JSON stack object
        /// </summary>
        internal JsonStackObject(string name, JsonObjectType type)
        {
            this.Name = name;
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the name of the stack object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the JSON object type
        /// </summary>
        public JsonObjectType Type { get; set; }

    }
}
