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
    /// Represents an instantiable collection of classes that make up a message that can be 
    /// sent/received to/from an endpoint
    /// </summary>
    public class Interaction : Feature
    {
        /// <summary>
        /// The trigger event that relates to this interaction
        /// </summary>
        public string TriggerEvent { get; set; }

        /// <summary>
        /// The fully qualified name of the classes that need to be instantiated to create this 
        /// interaction. Usually this is in the form : Transport&lt;ControlAct&lt;Message&gt;&gt;
        /// </summary>
        public TypeReference MessageType { get; set; }

        /// <summary>
        /// Identifies interactions that can respond to this interaction. 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Interaction> Responses { get; set; }

        /// <summary>
        /// Represent this interaction in string format
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])")]
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("concrete interaction ");
            sb.AppendFormat("{0}{1} of {2} \r\n\twith trigger {3}",
                Name,
                BusinessName == null ? "" : " alias '" + BusinessName + "'", 
                MessageType.ToString(), 
                TriggerEvent);

            if (Responses != null)
            {
                sb.Append("\r\n\twith response (\r\n");
                foreach (Interaction i in Responses)
                    sb.AppendFormat("\t\t{0};\r\n", i.Name);
                sb.Append(")");
            }

            return sb.ToString();
        }
    }
}