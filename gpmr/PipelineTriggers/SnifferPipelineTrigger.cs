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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.gpmr.Pipeline;
using MohawkCollege.Util.Console.MessageWriter;
using System.IO;

namespace gpmr.PipelineTriggers
{
    /// <summary>
    /// Sniffs the pipeline and outputs the data stack at each state change
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sniffer")]
    public class SnifferPipelineTrigger : IPipelineTrigger
    {

        /// <summary>
        /// Initialization order
        /// </summary>
        public int InitOrder { get { return -10; } }

        #region IPipelineTrigger Members

        // Message Writer
        bool enabled = false;
        string output = "stdout";

        public void Init(Pipeline Context)
        {
            Context.StateChanged += new StateChangeHandler(Context_StateChanged);

            // Check if we should be turned on
            enabled = Program.parameters == null ? false : Program.parameters.Extensions.ContainsKey("pipe-sniffer");

            if (enabled)
                output = Program.parameters.Extensions["pipe-sniffer"][0];
        }

        /// <summary>
        /// Fires when the state changed handler is fired
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)")]
        void Context_StateChanged(Pipeline Context, Pipeline.PipelineStates OldState, Pipeline.PipelineStates NewState)
        {
            if (!enabled) return;
            
            
            // string builder to construct the data segment string
            StringBuilder sb = new StringBuilder();
            if(Context.Data != null)
                foreach(KeyValuePair<string, object> kv in Context.Data)
                    sb.AppendFormat("{0}={1}\r\n", kv.Key, kv.Value);
            else
                sb.AppendFormat("--- EMPTY DATA SEGMENT ---");


            // Write out our data segment to the file
            TextWriter tw = output == "stdout" ? Console.Out : output == "stderr" ? Console.Error : null;
            try
            {
                if (tw == null)
                    tw = File.AppendText(output);

                tw.Write(string.Format("{4} - [PIPELINE STATE CHANGE ({0}->{1})]\r\nDATA SEGEMENT SIZE: {2}\r\nDATA SEGMENT DATA:\r\n{3}",
                OldState.ToString(), NewState.ToString(), Context.Data == null ? 0 : Context.Data.Count, sb.ToString(), DateTime.Now.ToString("dd-MMM-yy HH:mm:ss")));
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.ToString(), "fatal");
            }
            finally
            {
                if (tw != null && tw != System.Console.Out) tw.Close();
            }

        }

        #endregion
    }
}
