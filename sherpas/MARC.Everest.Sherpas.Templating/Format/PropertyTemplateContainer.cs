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
 * Date: 20-4-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Property template container which contains other property templates
    /// </summary>
    [XmlType("PropertyTemplateContainer", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public abstract class PropertyTemplateContainer : ArtifactTemplateBase
    {
        /// <summary>
        /// Container ctor
        /// </summary>
        public PropertyTemplateContainer()
        {
            this.Templates = new List<PropertyTemplateContainer>();
            this.Validation = new List<MethodDefinitionBase>();
            this.FormalConstraint = new List<FormalConstraintDefinition>();

            this.Initialize = new List<MethodDefinitionBase>();
        }

        /// <summary>
        /// Template instructions applied to the class
        /// </summary>
        [XmlElement("propertyTemplate", typeof(PropertyTemplateDefinition))]
        [XmlElement("propertyChoiceTemplate", typeof(PropertyChoiceTemplateDefinition))]
        public List<PropertyTemplateContainer> Templates { get; set; }

        /// <summary>
        /// Gets or sets the validation rules
        /// </summary>
        [XmlElement("validationInstruction")]
        public List<MethodDefinitionBase> Validation { get; set; }

        /// <summary>
        /// Gets or sets the validation rules
        /// </summary>
        [XmlElement("formalConstraint")]
        public List<FormalConstraintDefinition> FormalConstraint { get; set; }

        /// <summary>
        /// Traversal name of the attribute 
        /// </summary>
        [XmlAttribute("traversalName")]
        public string TraversalName { get; set; }

        /// <summary>
        /// Gets or sets the validation rules
        /// </summary>
        [XmlElement("initialize")]
        public List<MethodDefinitionBase> Initialize { get; set; }
    }
}
