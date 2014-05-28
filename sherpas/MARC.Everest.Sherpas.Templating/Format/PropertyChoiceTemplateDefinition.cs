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
 * Date: 24-4-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a choice of items that can be selected. This is typically used to suppress the conformance statement for a particular property 
    /// as it will be forced in the validation routine
    /// </summary>
    [XmlType("PropertyChoiceTemplateDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class PropertyChoiceTemplateDefinition : PropertyTemplateContainer
    {

        /// <summary>
        /// Gets or sets the property this definition is bound to
        /// </summary>
        [XmlIgnore]
        public PropertyInfo Property { get; set; }
    }
}
