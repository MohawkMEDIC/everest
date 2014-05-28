/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 4-2-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a restriction on a class
    /// </summary>
    [XmlType("ClassTemplate", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class ClassTemplateDefinition : PropertyTemplateContainer
    {

        /// <summary>
        /// Gets or sets the fully qualified name of the base class
        /// </summary>
        [XmlElement("baseClass")]
        public BasicTypeReference BaseClass
        {
            get;
            set;
        }

        
       
    }
}
