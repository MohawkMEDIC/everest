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
 * Date: 09-11-2009
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using MohawkCollege.EHR.gpmr.COR;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorCollapser
{
    /// <summary>
    /// Pipeline trigger for the COR Collapser
    /// </summary>
    public class CorCollapserPipelineTrigger : IPipelineTrigger
    {
        private Pipeline hostContext = null;

        /// <summary>
        /// Initialization order
        /// </summary>
        public int InitOrder { get { return 10; } }

        #region Command Parameters
        internal static bool enabled = false;
        internal static bool combine = false;
        internal static bool collapse = false;
        internal static StringCollection collapseIgnore = new StringCollection();
        internal static bool collapseIgnoreFixed = false;
        internal static StringCollection uselessWords = new StringCollection(), importantWords = new StringCollection();
        internal static bool collapseSpecialNaming = false;
        internal static string logFile = String.Empty;
        #endregion

        #region IPipelineTrigger Members

        static CorCollapserPipelineTrigger()
        {
            uselessWords.AddRange(new string[] {
                "value",
                "component",
                "section", 
                "id",
                "code"
            });
            importantWords.AddRange(new string[] { 
                "subject"
            });
        }

        /// <summary>
        /// Initialize the pipeline
        /// </summary>
        /// <param name="Context"></param>
        public void Init(Pipeline Context)
        {
            // Set host context
            hostContext = Context;
            System.Diagnostics.Trace.WriteLine("COR Optimization Trigger Component\r\nCopyright (C) 2009 Mohawk College of Applied Arts and Technology", "information");

            // Set the trigger
            hostContext.StateChanged += new StateChangeHandler(hostContext_StateChanged);

        }

        /// <summary>
        /// Fires when the host context has changed
        /// </summary>
        /// <param name="Context">The pipeline that is changing state</param>
        /// <param name="OldState">The old state</param>
        /// <param name="NewState">The new state</param>
        void hostContext_StateChanged(Pipeline Context, Pipeline.PipelineStates OldState, Pipeline.PipelineStates NewState)
        {
            switch (NewState)
            {
                case Pipeline.PipelineStates.SourceLoaded:

                    Dictionary<String, StringCollection> parameters = hostContext.Data["CommandParameters"] as Dictionary<String, StringCollection>;
                    StringCollection tCollection = null;
                    if (parameters.TryGetValue("optimize", out tCollection))
                        enabled = Convert.ToBoolean(tCollection[0]);
                    // Collection
                    if (parameters.TryGetValue("collapse", out tCollection))
                        collapse = Convert.ToBoolean(tCollection[0]);
                    // Combination
                    if (parameters.TryGetValue("combine", out tCollection))
                        combine = Convert.ToBoolean(tCollection[0]);
                    if (parameters.TryGetValue("collapse-ignore-fixed", out tCollection))
                        collapseIgnoreFixed = Convert.ToBoolean(tCollection[0]);
                    if (parameters.TryGetValue("collapse-useless-name", out tCollection))
                        foreach(String s in tCollection)
                            uselessWords.Add(s);
                    if (parameters.TryGetValue("collapse-important-name", out tCollection))
                        foreach (String s in tCollection)
                            importantWords.Add(s);
                    if (parameters.TryGetValue("collapse-adv-naming", out tCollection))
                        collapseSpecialNaming = Convert.ToBoolean(tCollection[0]);
                    if (parameters.TryGetValue("combine-replay", out tCollection))
                        logFile = tCollection[0];
                    // Collapse ignore
                    if (!parameters.TryGetValue("collapse-ignore", out collapseIgnore) && collapse)
                        throw new InvalidOperationException("If --collapse is specified, then the --collapse-ignore must be specified");
                    break;
                case Pipeline.PipelineStates.Compiled:
                    if (!enabled) return;
                    // Optimize the COR repository
                    System.Diagnostics.Trace.WriteLine("Optimizing COR Repository...", "information");
                    ClassRepository classRep = hostContext.Data["SourceCR"] as ClassRepository;
                    ClassRepositoryOptimizer optimizer = new ClassRepositoryOptimizer();

                    // Load the replay log if possible
                    var combineLog = new CombineLog();
                    if (!String.IsNullOrEmpty(logFile) && File.Exists(logFile))
                        combineLog = CombineLog.Load(logFile);

                    ClassRepository optimizedRepository = optimizer.Optimize(classRep, combineLog);

                    // Save the replay log
                    if (!String.IsNullOrEmpty(logFile))
                        combineLog.Save(logFile);

                    System.Diagnostics.Trace.WriteLine(string.Format("Optimization resulted in {0:#,##0} fewer features ({1:#0}% reduction)", classRep.Count - optimizedRepository.Count,
                        (1 - ((float)optimizedRepository.Count / classRep.Count)) * 100), "debug");
                    hostContext.Data["SourceCR"] = optimizedRepository;
                    break;
            }

        }

        #endregion
    }
}
