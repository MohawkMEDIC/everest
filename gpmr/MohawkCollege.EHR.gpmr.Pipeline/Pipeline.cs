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
	/// Handler for the pipeline change of state
	/// </summary>
	/// <param name="Context">The pipeline that is changing state</param>
	/// <param name="OldState">The old state the pipeline is in</param>
	/// <param name="NewState">The new state the pipeline is entering</param>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Old"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "New"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Context")]
	public delegate void StateChangeHandler(Pipeline Context, Pipeline.PipelineStates OldState, Pipeline.PipelineStates NewState);

	/// <summary>
	/// The pipeline is the container that governs the transformation of the source files to 
	/// their output.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
	public class Pipeline
	{
		/// <summary>
		/// Enumeration of possible pipeline states
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
		public enum PipelineStates
		{
			/// <summary>
			/// The pipeline has not been initiliazed
			/// </summary>
			Uninitialized = 0,
			/// <summary>
			/// The pipeline components have been loaded and are ready to execute
			/// </summary>
			Initialized = 1,
			/// <summary>
			/// The MIF files have been loaded 
			/// </summary>
			SourceLoaded = 2,
			/// <summary>
			/// The MIF sources have been compiled
			/// </summary>
			Compiled = 3,
			/// <summary>
			/// The output has been rendered
			/// </summary>
			Rendered = 4,
			/// <summary>
			/// The entire pipeline has been processed
			/// </summary>
			Processed = 5,
			/// <summary>
			/// 
			/// </summary>
			Error = -1
		}

		/// <summary>
		/// Operation mode
		/// </summary>
		public enum OperationModeType
		{
			/// <summary>
			/// Operate in quirks mode, means that GPMR will forgive errors
			/// that might cause it to fail later on
			/// </summary>
			Quirks,
			/// <summary>
			/// Default, Operate in normal mode
			/// </summary>
			Normal,
			/// <summary>
			/// Operates in strict mode which does not do any additional attempts at interpretation
			/// </summary>
			Strict
		}

		private Stack<string> files = new Stack<string>();

		/// <summary>
		/// The stack of input files that need to be processed into the MIF repository
		/// </summary>
		public Stack<string> InputFiles
		{
			get { return files; }
		}
	


		// Internal Pipeline Parameters
		private OperationModeType mode = OperationModeType.Strict;
		private PipelineStates state = PipelineStates.Uninitialized;
		private Dictionary<string, object> pipelineData;
		private List<IPipelineComponent> components = new List<IPipelineComponent>();
		private List<IPipelineTrigger> triggers = new List<IPipelineTrigger>();

		/// <summary>
		/// Gets or sets the mode that GPMR operates within
		/// </summary>
		public OperationModeType Mode
		{
			get { return mode; }
			set { mode = value; }
		}
		/// <summary>
		/// Identifies the output directory as specified by the user
		/// </summary>
		public string Output { get; set; }

		/// <summary>
		/// List of pipeline triggers to execute
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public List<IPipelineTrigger> Triggers
		{
			get { return triggers; }
			private set { triggers = value; }
		}
	
		/// <summary>
		/// Get or set the pipeline components that are in this pipeline
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public List<IPipelineComponent> Components
		{
			get { return components; }
			private set { components = value; }
		}
	
		/// <summary>
		/// Provides a basic dictionary of data that components may have access to
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public Dictionary<string, object> Data
		{
			get { return pipelineData; }
			set { pipelineData = value; }
		}

		/// <summary>
		/// The current state of the pipeline
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public PipelineStates State
		{
			get 
			{ 
				return state; 
			}
			private set 
			{ 
				System.Diagnostics.Trace.WriteLine(string.Format("Pipeline State change from {0} -> {1}", state.ToString(), value.ToString()), "debug");

				// Fire state changed
				if (StateChanged != null)
					StateChanged(this, state, value);

				state = value; 
			}
		}
	
		/// <summary>
		/// Fires when the state of the pipeline has changed
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
		public event StateChangeHandler StateChanged;

		/// <summary>
		/// Initialize the pipeline
		/// </summary>
		public void Initialize()
		{
			Data = new Dictionary<string, object>();

			Components.Sort((a, b) => a.ExecutionOrder.CompareTo(b.ExecutionOrder));
			foreach (IPipelineComponent pc in Components)
				pc.Init(this);

			Triggers.Sort((a, b) => a.InitOrder.CompareTo(b.InitOrder));
			foreach (IPipelineTrigger pt in Triggers)
				pt.Init(this);

			State = PipelineStates.Initialized;
		}

		/// <summary>
		/// Execute the pipeline
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.GC.Collect"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		public void Execute()
		{

			DateTime start = DateTime.Now;

			// Basic Execution Pattern
			try
			{

				// Iterate through states and components and execute the appropriate components
				for (int stateNumeral = (int)State + 1; stateNumeral <= (int)PipelineStates.Processed; stateNumeral++)
				{
					foreach (IPipelineComponent pc in Components)
						switch (State)
						{
							case PipelineStates.Initialized: // Pipeline is initialized, so we want to load the MIF file
								if (pc.ComponentType == PipelineComponentType.Loader)
								{
									pc.Execute();
									System.Diagnostics.Trace.WriteLine(string.Format("{0} files not loaded", InputFiles.Count), "warn");
								}
								break;
							case PipelineStates.SourceLoaded:
								if (pc.ComponentType == PipelineComponentType.Compiler)
									pc.Execute();
								break;
							case PipelineStates.Compiled:
								if (pc.ComponentType == PipelineComponentType.Renderer)
									pc.Execute();
								break;
						}

					// Change state
					State = (PipelineStates)stateNumeral;

					// Clean
					long gcOptimization = System.GC.GetTotalMemory(false);
					System.GC.Collect();
					System.Diagnostics.Trace.WriteLine(string.Format("Cleaned {0:##,###,###} bytes wasted memory", gcOptimization - System.GC.GetTotalMemory(true)), "debug");

				}

			}
			catch (Exception e)
			{
				PipelineStates oldState = State;
				State = PipelineStates.Error;
				#if DEBUG
				System.Diagnostics.Trace.WriteLine(e.ToString(), "fatal");
				#else
				System.Diagnostics.Trace.WriteLine(e.Message, "fatal");
				#endif
				throw new PipelineExecutionException(this, oldState, this.Data, e);
			}
			finally
			{
				TimeSpan fts = DateTime.Now.Subtract(start);
				System.Diagnostics.Trace.Write(string.Format("Pipeline took {0} to finish execution", fts.ToString()), "information");
			}

		}
	}
}