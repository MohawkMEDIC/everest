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
using MARC.Everest.Sherpas.Templating.Format;
using System.Reflection;
using MARC.Everest.Sherpas.Templating.Interface;

namespace MARC.Everest.Sherpas.Templating.Binder
{
    /// <summary>
    /// The binding context for the current bind operation
    /// </summary>
    public class BindingContext
    {
        // Binders
        private static Dictionary<Type, IArtifactBinder> s_binders = new Dictionary<Type, IArtifactBinder>();

        /// <summary>
        /// Static ctor
        /// </summary>
        static BindingContext()
        {
            foreach (var t in typeof(BindingContext).Assembly.GetTypes().Where(bc => typeof(IArtifactBinder).IsAssignableFrom(bc)))
            {
                if (t.IsInterface) continue;

                ConstructorInfo ci = t.GetConstructor(Type.EmptyTypes);
                if (ci == null) throw new InvalidOperationException("Binding type doesn't have parameterless constructor");
                IArtifactBinder binder = ci.Invoke(null) as IArtifactBinder;
                s_binders.Add(binder.ArtifactTemplateType, binder);
            }
        }

        /// <summary>
        /// Creates a new binding context
        /// </summary>
        public BindingContext(Assembly bindAssembly, ArtifactTemplateBase artifact, TemplateProjectDefinition project)
        {
            this.Artifact = artifact;
            this.BindAssembly = bindAssembly;
            this.Project = project;
        }

        /// <summary>
        /// Creates a new binding context
        /// </summary>
        public BindingContext(BindingContext parent, ArtifactTemplateBase artifact)
        {
            this.Parent = parent;
            this.Artifact = artifact;
            this.BindAssembly = parent.BindAssembly;
            this.Project = parent.Project;
        }

        /// <summary>
        /// Get the artifact binder
        /// </summary>
        public IArtifactBinder GetBinder()
        {
            if (this.Artifact == null)
                throw new ArgumentNullException(String.Empty, "Artifact must not be null!");

            IArtifactBinder retVal = null;
            if (s_binders.TryGetValue(this.Artifact.GetType(), out retVal))
                return retVal;
            return null;
        }

        /// <summary>
        /// Gets the parent context for this context
        /// </summary>
        public BindingContext Parent { get; private set; }

        /// <summary>
        /// Gets the artifact for the binding
        /// </summary>
        public ArtifactTemplateBase Artifact { get; private set; }

        /// <summary>
        /// Gets the assembly to which artifacts must be bound
        /// </summary>
        public Assembly BindAssembly { get; private set; }

        /// <summary>
        /// Identifies the project to which the item belongs
        /// </summary>
        public TemplateProjectDefinition Project { get; private set; }

    }
}
