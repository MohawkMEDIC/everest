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
 * Date: 09-26-2011
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta
{
    /// <summary>
    /// Pipeline trigger for 
    /// </summary>
    public class DeltaPipelineTrigger : IPipelineTrigger
    {

        /// <summary>
        /// Get initialization order
        /// </summary>
        public int InitOrder { get { return 0; } }

        // Pipeline context
        private Pipeline m_context = null;

        // Enabled
        private bool m_enabled = false;
        // Delta files
        private StringCollection m_deltaFiles = null;

        #region IPipelineTrigger Members

        /// <summary>
        /// Initialize the component
        /// </summary>
        public void Init(Pipeline Context)
        {
            // Set host context
            m_context = Context;
            System.Diagnostics.Trace.WriteLine("COR Delta Trigger Component\r\nCopyright (C) 2011 Mohawk College of Applied Arts and Technology", "information");

            // Set the trigger
            m_context.StateChanged += new StateChangeHandler(m_context_StateChanged);

        }

        /// <summary>
        /// State has changed on the context
        /// </summary>
        void m_context_StateChanged(Pipeline Context, Pipeline.PipelineStates OldState, Pipeline.PipelineStates NewState)
        {
            switch (NewState)
            {
                case Pipeline.PipelineStates.SourceLoaded:

                    Dictionary<String, StringCollection> parameters = Context.Data["CommandParameters"] as Dictionary<String, StringCollection>;
                    StringCollection tCollection = null;
                    if (parameters.TryGetValue("apply-deltas", out tCollection))
                        this.m_enabled = Convert.ToBoolean(tCollection[0]);
                    // Collection
                    if (parameters.TryGetValue("deltafile", out tCollection))
                        this.m_deltaFiles = tCollection;
                    else if (this.m_enabled)
                        throw new InvalidOperationException("When enabled, you must supply delta set files using --deltafile parameter");

                    // Validate
                    if (this.m_enabled)
                    {
                        List<String> remDelta = new List<string>();
                        foreach (var file in this.m_deltaFiles)
                            if (!File.Exists(file))
                            {
                                System.Diagnostics.Trace.WriteLine(String.Format("Cannot find '{0}', delta will be ignored", file), "error");
                                remDelta.Add(file);
                            }
                        foreach (var f in remDelta)
                            this.m_deltaFiles.Remove(f);
                    }
                    break;
                case Pipeline.PipelineStates.Compiled:
                    if (!m_enabled) return;
                    // Optimize the COR repository
                    System.Diagnostics.Trace.WriteLine("Applying Delta-Sets...", "information");
                    foreach (var delta in this.m_deltaFiles)
                        ApplyDelta(delta, Context.Data["SourceCR"] as ClassRepository);
                    break;
            }
        }

        /// <summary>
        /// Apply the delta set
        /// </summary>
        private void ApplyDelta(string delta, ClassRepository classRepository)
        {
            // Load the Delta Set
            try
            {
                DeltaSet ds = DeltaSet.Load(delta);
                if (ds.MetaData == null)
                    throw new InvalidOperationException("Delta set is missing data...");

                DeltaEngine de = new DeltaEngine(ds);
                de.Apply(classRepository);
                
            }
            catch (Exception e)
            {
                Trace.WriteLine(String.Format("Delta: Won't apply '{0}', reason: {1}", delta, e.Message), "error");
                Exception ie = e.InnerException;
                while (ie != null)
                {
                    Trace.WriteLine(String.Format("Caused by: {0}", ie.Message), "error");
                    ie = ie.InnerException;
                }
            }
        }

        #endregion
    }
}
