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
using MARC.Everest.Sherpas.Templating.Binder;

namespace MARC.Everest.Sherpas.Templating.Interface
{
    /// <summary>
    /// Identifies a class that can bind an artifact to code
    /// </summary>
    public interface IArtifactBinder
    {

        /// <summary>
        /// Gets the type of artifact this binder operates on
        /// </summary>
        Type ArtifactTemplateType { get; }

        /// <summary>
        /// Binds the specified artifact
        /// </summary>
        void Bind(BindingContext context);


    }
}
