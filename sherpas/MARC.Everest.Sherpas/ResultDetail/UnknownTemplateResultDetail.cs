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
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Sherpas.ResultDetail
{
    /// <summary>
    /// Added to a result detail list when the formatter could not determine the template type
    /// </summary>
    public class UnknownTemplateResultDetail : NotSupportedChoiceResultDetail
    {
        /// <summary>
        /// Identifies the object which for which the specified template could not be found
        /// </summary>
        public IImplementsTemplateId TemplatedObject { get; set; }

        /// <summary>
        /// Creates a new UnknownTemplateResultDetail instance
        /// </summary>
        public UnknownTemplateResultDetail(ResultDetailType type, Interfaces.IImplementsTemplateId templatedObject, string location) : base(type, "Could not locate a type implementing the specified template parameters", location, null)
        {
            this.TemplatedObject = templatedObject;
        }
    }
}
