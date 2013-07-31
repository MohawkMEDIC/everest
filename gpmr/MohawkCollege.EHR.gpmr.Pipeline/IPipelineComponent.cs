/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MohawkCollege.EHR.gpmr.Pipeline
{
    /// <summary>
    /// The pipeline component interface defines a standard framework that all pipeline components 
    /// must implement to be executed in a renderer pipeline
    /// </summary>
    public interface IPipelineComponent
    {

        /// <summary>
        /// Defines the order that this pipeline component should be executed in the pipeline amongst 
        /// its peer types.
        /// </summary>
        int ExecutionOrder { get; }

        /// <summary>
        /// Gets the type of component
        /// </summary>
        PipelineComponentType ComponentType { get; }

        /// <summary>
        /// Initializes this pipeline component
        /// </summary>
        /// <param name="Context">The pipeline that this component will be executing in</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Context")]
        void Init(Pipeline Context);

        /// <summary>
        /// Called when the pipeline component is executed
        /// </summary>
        void Execute();
    }
}