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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// State machine is responsible for the dictation of behaviors against a particular property 
    /// within a class. Depending on the output format, the state triggers may be changed into events
    /// </summary>
    public class StateMachine
    {

        private const int FONT_SIZE = 12;
        private const int CD_PADDING_X = 200;
        private const int CD_PADDING_Y = 50;

        private Dictionary<string,State> states;
        private List<StateTransition> transitions;
        private string stateDiagram;

        // State Diagram layout structure
        class StateData
        {
            public State State; // The state being represented
            public List<StateTransition> ExitTransitions; // Transitions where this state is the start
            public List<StateTransition> EntryTransitions; // Transitions where this state is the destination
        }

        /// <summary>
        /// Get a base64 encoded PNG image representing the state diagram that describes this
        /// state diagram
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])")]
        public string GraphicalRepresentation 
        {
            get
            {


                if (stateDiagram != null) return stateDiagram;

                StringBuilder sb = new StringBuilder();
                sb.Append("digraph State_GraphicRepresentation { rankdir=LR fontname=\"Arial\" fontsize=\"10\" ");

                Dictionary<string, StateData> stateData = new Dictionary<string, StateData>();

                #region Gather data about state transitions
                foreach (StateTransition st in this.transitions)
                {
                    // Exit transition part
                    if (!stateData.ContainsKey(st.StartState.Name))
                    {
                        StateData std = new StateData();
                        std.EntryTransitions = new List<StateTransition>();
                        std.ExitTransitions = new List<StateTransition>(new StateTransition[] { st });
                        std.State = st.StartState;
                        stateData.Add(std.State.Name, std);
                    }
                    else
                        stateData[st.StartState.Name].ExitTransitions.Add(st);

                    // Entry transitions
                    if (!stateData.ContainsKey(st.EndState.Name))
                    {
                        StateData std = new StateData();
                        std.ExitTransitions = new List<StateTransition>();
                        std.EntryTransitions = new List<StateTransition>(new StateTransition[] { st });
                        std.State = st.EndState;
                        stateData.Add(std.State.Name, std);
                    }
                    else
                        stateData[st.EndState.Name].EntryTransitions.Add(st);
                }
                #endregion

                // State Diagram data
                #region Terminal and initial state
                StateData initialState = new StateData(); // Initial state : the first state from the initial point
                List<StateData> terminalStates = new List<StateData>(); // States that ultimately lead to the terminal point

                foreach (KeyValuePair<string, StateData> kv in stateData)
                    if (kv.Value.EntryTransitions.Count == 0 && kv.Value.State.Name == "null") // No entry, this is the initial state
                        initialState = kv.Value;
                    else if (kv.Value.ExitTransitions.Count == 0) // No exit, this is a terminal state
                        terminalStates.Add(kv.Value);
                #endregion

                // Define the state nodes
                foreach (KeyValuePair<string, StateData> kv in stateData)
                {
                    // Define the node
                    if (kv.Value == initialState)
                        sb.AppendFormat("{{ node [fontname=\"Arial\" fontsize=10 shape=point style=filled fillcolor=black width=0.2 height=0.2] {0} }} ", kv.Value.State.Name);
                    else
                        sb.AppendFormat("node [fontname=\"Arial\" fontsize=10 shape=ellipse] {0} [label={0}] ", kv.Value.State.Name);
                }

                // State Transitions
                foreach (KeyValuePair<string, StateData> kv in stateData)
                    foreach (StateTransition st in kv.Value.ExitTransitions)
                        sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 label={0}] {1}-&gt;{2} ", st.Name, st.StartState.Name, st.EndState.Name);

                // Create a node for the terminal state
                if (terminalStates != null && terminalStates.Count > 0)
                {
                    sb.AppendFormat("node [shape=doublecircle fillcolor=black style=filled height=.1 width=.1 fontsize=2] terminal [label=\"\"] ");
                    foreach (StateData st in terminalStates)
                        sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 label=\"\"] {0}-&gt;terminal ", st.State.Name);
                }

                sb.Append("}");
                stateDiagram = sb.ToString();

                return stateDiagram;
            }
        }

        
        /// <summary>
        /// Get or set the transitions of state within this state machine
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<StateTransition> Transitions
        {
            get { return transitions; }
            set { transitions = value; }
        }

        /// <summary>
        /// Get or set the available states within this state machine
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Dictionary<string, State> States
        {
            get { return states; }
            set { states = value; }
        }

        
    }
}