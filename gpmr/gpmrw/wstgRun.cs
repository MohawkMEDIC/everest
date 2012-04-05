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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.VisualStudio.Wizards.Interfaces;
using MohawkCollege.EHR.gpmr.Pipeline;
using System.IO;
using System.Reflection;
using gpmr;
using System.Threading;
using System.Diagnostics;

namespace gpmrw
{
    public partial class wstgRun : UserControl, IWizardStage
    {
        // Trace listener
        private EventTraceListener m_traceListener = new EventTraceListener();
        // True when the process is complete
        private IWizardStage m_previousStage = null;
        private object m_syncObject = new object();

        // GPMR render pipeline
        private Pipeline m_gpmrRenderPipeline;
        private string[] m_status = {
                                                "Initializing",
                                                "Loading MIF Files",
                                                "Compiling MIF to COR",
                                                "Rendering Output Format",
                                                "Operation Completed",
                                                "Operation Completed",
                                    };
        private string m_log = Path.GetTempFileName() + ".log";
        private TextWriterTraceListener m_writerListener;
        public wstgRun()
        {
            InitializeComponent();
            this.m_writerListener = new TextWriterTraceListener(this.m_log);
            Trace.Listeners.Add(m_writerListener);
        }


        ///// <summary>
        ///// Handles a new message from render pipelines
        ///// </summary>
        //void m_traceListener_MessageRaised(object sender, TraceListenerEventArgs e)
        //{
        //    if (e.Message.StartsWith("information:") ||
        //        e.Message.StartsWith("warn:") ||
        //        e.Message.StartsWith("error:") ||
        //        e.Message.StartsWith("fatal:") ||
        //        e.Message.StartsWith("debug:") ||
        //        e.Message.StartsWith("quirks:"))
        //    {

        //        if (e.Message.StartsWith("error:"))
        //            m_log.Append("ERROR ->");
        //        else if (e.Message.StartsWith("warn:"))
        //            m_log.Append("WARNING ->");
        //        else if (e.Message.StartsWith("fatal:"))
        //            m_log.Append("FATAL ->");
        //        else if (e.Message.StartsWith("quirks:"))
        //            m_log.Append("---> QUIRKS ---->");
        //        m_log.Append(e.Message.Substring(e.Message.IndexOf(":") + 1));
        //    }
        //}

        #region IWizardStage Members

        public IWizard Host { get; set; }

        public string StageName
        {
            get { return "Convert"; }
        }

        public bool IsTerminal
        {
            get 
            { 
                return !bwStatus.IsBusy;
            }
        }

        public bool CanExit
        {
            get { return false; }
        }

        public IWizardStage Next
        {
            get;
            private set;
        }

        public IWizardStage Previous
        {
            get { 
                if(bwStatus.IsBusy)
                    return null; 
                else
                    return m_previousStage;
            }
        }

        public void Shown(IWizardStage previous, WizardDirection direction)
        {

            if(direction == WizardDirection.Forward)
                this.m_previousStage = previous;
            tmrStatus.Enabled = true;
            this.loadAssemblies();
            m_gpmrRenderPipeline = new Pipeline();
            //m_gpmrRenderPipeline.StateChanged += new StateChangeHandler(m_gpmrRenderPipeline_StateChanged);

            bwStatus.RunWorkerAsync();
        }

        public void Hiding()
        {
            
        }

        #endregion

        #region GPMR instructions
        /// <summary>
        /// Load assemblies and extensions
        /// </summary>
        private void loadAssemblies()
        {
            // Load all assemblies in the app dir
            foreach (string file in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "*.dll"))
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Loading {0}...", file), "debug");
                Assembly a = Assembly.LoadFile(file);
                AppDomain.CurrentDomain.Load(a.FullName);
            }

        }

        #endregion

        private void tmrStatus_Tick(object sender, EventArgs e)
        {

            int stateValue = (int)this.m_gpmrRenderPipeline.State;
            if (stateValue < 0)
                pgMain.Value = 5;
            else
            {
                pgMain.Value = stateValue;

                if (!bwStatus.IsBusy)
                    return;

                lblStatus.Text = this.m_status[stateValue];
            }
            Application.DoEvents();
        }

        private void bwStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            // Load all pipeline components and triggers from available assemblies
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type t in a.GetTypes())
                {
                    if (t.GetInterface("MohawkCollege.EHR.gpmr.Pipeline.IPipelineComponent") != null
                        && !t.IsAbstract)
                    {
                        this.m_gpmrRenderPipeline.Components.Add((IPipelineComponent)a.CreateInstance(t.FullName));
                        System.Diagnostics.Trace.WriteLine(string.Format("Found pipeline component {0}", t.FullName), "debug");
                    }
                    else if (t.GetInterface("MohawkCollege.EHR.gpmr.Pipeline.IPipelineTrigger") != null
                        && !t.IsAbstract)
                    {
                        this.m_gpmrRenderPipeline.Triggers.Add((IPipelineTrigger)a.CreateInstance(t.FullName));
                        System.Diagnostics.Trace.WriteLine(string.Format("Found pipeline trigger {0}", t.FullName), "debug");
                    }
                }

            // Initialize the process pipeline
            this.m_gpmrRenderPipeline.Initialize();

            // Set data segment
            this.m_gpmrRenderPipeline.Data.Add("EnabledRenderers", new DumpableStringCollection(Program.s_parameters["renderer"]));
            this.m_gpmrRenderPipeline.Data.Add("CommandParameters", Program.s_parameters);
            this.m_gpmrRenderPipeline.Mode = Program.s_parameters.ContainsKey("strict") ? Pipeline.OperationModeType.Strict : Program.s_parameters.ContainsKey("quirks") ? Pipeline.OperationModeType.Quirks : Pipeline.OperationModeType.Normal;
            foreach (var itm in Program.s_parameters["s"])
                this.m_gpmrRenderPipeline.InputFiles.Push(itm);

            this.m_gpmrRenderPipeline.Output = Program.s_parameters["o"][0];


            if (this.m_gpmrRenderPipeline.Mode == Pipeline.OperationModeType.Quirks)
                Console.WriteLine("--- WARNING ---\r\n You are executing GPMR in Quirks mode, GPMR will continue to process models that cannot be verified to be correct\r\n--- WARNING ---");

            // Begin Pipeline Execution
            try
            {
                this.m_gpmrRenderPipeline.Execute();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("The execution of the pipeline has failed. No processing can continue", "fatal");
                System.Diagnostics.Trace.WriteLine(ex.Message, "fatal");
            }

        }

        private void bwStatus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //this.m_isDone = true;
            if (this.m_gpmrRenderPipeline.State != Pipeline.PipelineStates.Error)
            {
                this.lblStatus.Text = "Complete";
                this.m_previousStage = null;
            }
            else
                this.lblStatus.Text = "An error occurred, please check the log file";
            this.m_writerListener.Flush();
        }

        private void lnkLogLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo proc = new ProcessStartInfo(this.m_log);
            proc.UseShellExecute = true;
            Process p = new Process();
            p.StartInfo = proc;
            p.Start();
        }

    }
}
