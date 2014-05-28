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

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a type definition
    /// </summary>
    [XmlType("TypeDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class TypeDefinition : BasicTypeReference
    {

        /// <summary>
        /// Gets the template parameters (typically value sets) for the type
        /// </summary>
        [XmlElement("type", typeof(TypeDefinition))]
        [XmlElement("enum", typeof(BasicTypeReference))]
        public List<BasicTypeReference> TemplateParameter { get; set; }

        /// <summary>
        /// Override to set the template parameter
        /// </summary>
        [XmlIgnore]
        public override Type Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
                if (value.IsGenericType)
                {
                    this.TemplateParameter = new List<BasicTypeReference>();
                    foreach (var t in value.GetGenericArguments())
                        this.TemplateParameter.Add(new TypeDefinition() { Type = t });
                }
            }
        }
    }
}
