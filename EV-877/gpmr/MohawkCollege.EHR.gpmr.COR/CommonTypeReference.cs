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
 **/
using System;
using System.Collections.Generic;
using System.Text;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Summary of CommonTypeReference
    /// </summary>
    public class CommonTypeReference : Feature
    {
        /// <summary>
        /// The class that this type reference points to
        /// </summary>
        public TypeReference Class { get; set; }
        /// <summary>
        /// The classifier domain for the referenced type
        /// </summary>
        public Enumeration ClassifierDomain { get; set; }
        /// <summary>
        /// The fixed code for the classifier
        /// </summary>
        public String ClassifierCode { get; set; }

        /// <summary>
        /// Represent this structure as a string
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("typeref ");
            sb.AppendFormat("{0}\r\n\tbind to {1} classify as '{2}';",
                Name, Class.Name, ClassifierCode);
            return sb.ToString();

        }
    }
}