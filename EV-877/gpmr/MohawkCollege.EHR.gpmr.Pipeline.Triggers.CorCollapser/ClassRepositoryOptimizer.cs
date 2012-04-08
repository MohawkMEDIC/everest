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
 * 
 * User: Justin Fyfe
 * Date: 08-11-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorCollapser
{
    /// <summary>
    /// Class repository optimizer. This class will render an optimized class repository as specified
    /// by the user with the command line parameters
    /// </summary>
    public class ClassRepositoryOptimizer 
    {

        

        /// <summary>
        /// Perform optimizations on <paramref name="source"/>
        /// </summary>
        /// <param name="source">The class repository to optimize</param>
        /// <returns>The optimized class repository</returns>
        public MohawkCollege.EHR.gpmr.COR.ClassRepository Optimize(MohawkCollege.EHR.gpmr.COR.ClassRepository source, CombineLog currentLog)
        {
            MohawkCollege.EHR.gpmr.COR.ClassRepository optimizedClassRepository = new MohawkCollege.EHR.gpmr.COR.ClassRepository();

            // Copy to the optimized class library, two stop process because dictionaries are wierd
            // First pass, copy the destination stuff
            foreach (var featurePair in source)
            {
                if (currentLog.CombineOps.Exists(o => o.Destination == featurePair.Key))
                {
                    optimizedClassRepository.Add(featurePair.Key, featurePair.Value); // Attempt to fix a bug where the memberOf does not get updated on child content
                    optimizedClassRepository[featurePair.Key].MemberOf = optimizedClassRepository;
                }
            }
            // Second pass, copy the collapsed stuff
            foreach (var featurePair in source)
            {
                if (!currentLog.CombineOps.Exists(o => o.Destination == featurePair.Key))
                {
                    optimizedClassRepository.Add(featurePair.Key, featurePair.Value); // Attempt to fix a bug where the memberOf does not get updated on child content
                    optimizedClassRepository[featurePair.Key].MemberOf = optimizedClassRepository;
                }
            }

            currentLog.CombineOps.Clear();
            
            // Scan for optimizers
            Dictionary<String, ICorOptimizer> optimizers = new Dictionary<string,ICorOptimizer>();
            var optimizerTypes = from type in this.GetType().Assembly.GetTypes()
                                 where type.GetInterface("MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorCollapser.ICorOptimizer") != null
                                 select type;

            // Add the optimizers
            foreach (var type in optimizerTypes)
            {
                ICorOptimizer optimizer = (ICorOptimizer)type.GetConstructor(Type.EmptyTypes).Invoke(null);
                optimizer.Repository = optimizedClassRepository;
                optimizers.Add(optimizer.HandlesType.ToString(), optimizer);
            }

            // Now process the class repository
            foreach (var featurePair in source)
            {
                ICorOptimizer handler = null;
                Feature f = null;
                if (optimizers.TryGetValue(featurePair.Value.GetType().ToString(), out handler) && 
                    optimizedClassRepository.TryGetValue(featurePair.Key, out f))
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Processing '{0}'...", featurePair.Key), "debug");
                    Feature optimizedFeature = handler.Optimize(optimizedClassRepository[featurePair.Key], currentLog);
                }
                else if(handler == null)
                    System.Diagnostics.Trace.WriteLine(String.Format("Can't find handler for '{0}', no optimizations will be performed on '{1}'...", featurePair.Value.GetType().ToString(), featurePair.Key), "warn");

            }

            // Get rid of empty sub-systems
            var subSystems = from subSys in source
                             where subSys.Value is SubSystem
                             select subSys.Value as SubSystem;
            foreach (var subSystem in subSystems)
            {
                SubSystem ss = optimizedClassRepository[subSystem.Name] as SubSystem;
                if (ss.OwnedClasses.Count == 0)
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Removing empty sub-system '{0}'...", ss.Name), "error");
                    optimizedClassRepository.Remove(ss.Name);
                }
            }

            return optimizedClassRepository;
        }
    }
}