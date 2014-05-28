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

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Template attribute identifies a structure as being a template
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = true)]
    public class TemplateAttribute : Attribute
    {

        public TemplateAttribute()
        {
        }

        /// <summary>
        /// Creates a new template attribute with templateId 
        /// </summary>
        public TemplateAttribute(String templateId)
        {
            this.TemplateId = templateId;
        }

        /// <summary>
        /// The ID of the template
        /// </summary>
        public string TemplateId { get; set; }
    }
}
