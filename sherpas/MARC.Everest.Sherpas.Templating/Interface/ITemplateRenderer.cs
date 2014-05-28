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
using MARC.Everest.Sherpas.Templating.Format;

namespace MARC.Everest.Sherpas.Templating.Interface
{
    /// <summary>
    /// Template renderer is responsible for binding the template into a programming language
    /// </summary>
    public interface ITemplateRenderer
    {

        /// <summary>
        /// Gets the name of the template renderer
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Render the specified template to the specified output directory
        /// </summary>
        void Render(TemplateProjectDefinition project, String outputDirectory);

    }
}
