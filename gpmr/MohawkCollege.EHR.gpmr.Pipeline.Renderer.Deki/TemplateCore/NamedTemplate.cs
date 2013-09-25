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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Collections;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.TemplateCore
{
    /// <summary>
    /// Represents a template with additional sub-scope templates below it
    /// </summary>
    [XmlType(TypeName = "NamedTemplate", Namespace = "http://marc.mohawkcollege.ca/hi")]
    [Serializable]
    public abstract class NamedTemplate : NonParameterizedTemplate
    {

        private string name;

        /// <summary>
        /// Get or set the name of the template
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Fill the template with the data it needs
        /// </summary>
        public abstract string FillTemplate();

        /// <summary>
        /// Convert object o to an array of object
        /// </summary>
        /// <param name="o">The object array to convert to</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        internal object[] ConvertToObjectArray(object o)
        {
            // No value so return null
            if (o == null) return null;

            if (o.GetType().GetMethod("GetEnumerator") == null) return null;

            IEnumerator enumerator = (IEnumerator)o.GetType().GetMethod("GetEnumerator").Invoke(o, null); // Get the enumerator

            List<object> retVal = new List<object>();

            while (enumerator.MoveNext())
                retVal.Add(enumerator.Current);

            return retVal.ToArray();
        }

    }
}