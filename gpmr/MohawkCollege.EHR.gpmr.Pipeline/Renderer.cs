/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MohawkCollege.EHR.gpmr.Pipeline
{
    /// <summary>
    /// The IRenderer interface defines a framework for implementing MIF renderer components. A renderer is 
    /// charged with the rendering of a MIF file into a particular format.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Renderer")]
    public abstract class RendererBase : IPipelineComponent
    {

        /// <summary>
        /// The pipline that is the context of this renderer
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Pipeline hostContext;

        /// <summary>
        /// The human friendly name of the renderer component
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The identifier that is used to identify this renderer at the command line
        /// </summary>
        public abstract string Identifier { get; }

        #region IPipelineComponent Members

        /// <summary>
        /// Get the execution order of the renderer (usually this is 0)
        /// </summary>
        public abstract int ExecutionOrder { get; }

        /// <summary>
        /// Get the pipeline component type definition 
        /// </summary>
        public PipelineComponentType ComponentType
        {
            get { return PipelineComponentType.Renderer; }
        }

        /// <summary>
        /// Initialize the renderer
        /// </summary>
        /// <param name="Context"></param>
        public virtual void Init(Pipeline Context)
        {
            hostContext = Context;
        }

        /// <summary>
        /// Execute the renderer
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Get help for this component
        /// </summary>
        public abstract string Help { get; }
        #endregion
    }
}
