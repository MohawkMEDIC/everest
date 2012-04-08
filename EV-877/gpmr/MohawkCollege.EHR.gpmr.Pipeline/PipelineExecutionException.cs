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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MohawkCollege.EHR.gpmr.Pipeline
{
    /// <summary>
    /// Represents an exception that defines a pipeline execution fault
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    [Serializable]
    public class PipelineExecutionException : Exception
    {
        private Pipeline faultingPipeline;
        private Pipeline.PipelineStates errorState;
        private Dictionary<string, object> dataSegment;

        /// <summary>
        /// Gets the data segment
        /// </summary>
        public Dictionary<string, object> DataSegment
        {
            get { return dataSegment; }
        }
	
        /// <summary>
        /// Gets the last state the pipeline was in before the error occured
        /// </summary>
        public Pipeline.PipelineStates ErrorState
        {
            get { return errorState; }
        }
	
        /// <summary>
        /// Gets the pipeline that caused the fault
        /// </summary>
        public Pipeline FaultingPipeline
        {
            get { return faultingPipeline; }
        }
	
        /// <summary>
        /// Create a new pipeline exception
        /// </summary>
        /// <param name="FaultingPipeline">The pipeline that is throwing the fault</param>
        /// <param name="ErrorState">The last state before the error was thrown</param>
        /// <param name="DataSegment">The current data segment</param>
        /// <param name="InnerException">The exception that caused the current exception</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Inner"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Faulting"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Error"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Data")]
        public PipelineExecutionException(Pipeline FaultingPipeline, Pipeline.PipelineStates ErrorState, Dictionary<string, object> DataSegment, Exception InnerException)
            : base("Pipeline Execution Failed", InnerException) 
        {
            this.faultingPipeline = FaultingPipeline;
            this.errorState = ErrorState;
            this.dataSegment = DataSegment;
        }

        /// <summary>
        /// Convert this exception to a string
        /// </summary>
        public override string ToString()
        {

            StringBuilder exceptionString = new StringBuilder(this.Message);
            exceptionString.AppendFormat("\r\nAt State {0}\r\n", ErrorState.ToString());
            exceptionString.Append("--- DATA SEGMENT ---\r\n");

            if(DataSegment != null)
                foreach (KeyValuePair<string, object> kv in DataSegment)
                    exceptionString.AppendFormat("{0} = {1}", kv.Key, kv.Value.ToString());

            return exceptionString.ToString();
        }

    }
}
