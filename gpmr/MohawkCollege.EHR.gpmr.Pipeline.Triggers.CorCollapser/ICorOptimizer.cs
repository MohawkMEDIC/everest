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
 * Date: 08-11-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorCollapser
{
    /// <summary>
    /// The ICorOptimizer defines an interface that is to be used by classes whose primary role
    /// is to collapse and optimize a COR repository
    /// </summary>
    public interface ICorOptimizer
    {
        /// <summary>
        /// Gets the type that this optimizer handles
        /// </summary>
        Type HandlesType { get; }
        /// <summary>
        /// Gets or sets the class repository that is being optimized
        /// </summary>
        MohawkCollege.EHR.gpmr.COR.ClassRepository Repository { get; set; }
        /// <summary>
        /// Optimize feature <paramref name="f"/>
        /// </summary>
        /// <param name="f">The feature to optimize</param>
        /// <returns>The optimized feature</returns>
        Feature Optimize(Feature f, CombineLog workingLog);
    }
}