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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// Summary of StateMachineParser
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
    public class StateMachineParser
    {
        /// <summary>
        /// Parse a COR state machine from a MIF state machine
        /// </summary>
        internal static MohawkCollege.EHR.gpmr.COR.StateMachine Parse(MohawkCollege.EHR.HL7v3.MIF.MIF20.StateMachine StateMachine)
        {
            MohawkCollege.EHR.gpmr.COR.StateMachine retVal = new MohawkCollege.EHR.gpmr.COR.StateMachine();

            retVal.Transitions = new List<MohawkCollege.EHR.gpmr.COR.StateTransition>();
            Dictionary<String, MohawkCollege.EHR.gpmr.COR.State> tStates = new Dictionary<String, MohawkCollege.EHR.gpmr.COR.State>();
            retVal.States = new Dictionary<string, MohawkCollege.EHR.gpmr.COR.State>();

            // Sort each state
            StateMachine.SubState.Sort(new MohawkCollege.EHR.HL7v3.MIF.MIF20.State.Comparator());

            // Parse States
            foreach (MohawkCollege.EHR.HL7v3.MIF.MIF20.State st in StateMachine.SubState)
            {
                MohawkCollege.EHR.gpmr.COR.State nst = Parse(st);
                tStates.Add(nst.Name, nst);
            }

            // Sort parent/child states
            foreach (KeyValuePair<String, MohawkCollege.EHR.gpmr.COR.State> kv in tStates)
            {
                if (kv.Value.ParentStateName == null) // No parent? Its at the root
                    retVal.States.Add(kv.Key, kv.Value);
                else
                {
                    if (tStates[kv.Value.ParentStateName].ChildStates == null) tStates[kv.Value.ParentStateName].ChildStates = new Dictionary<string, State>();
                    tStates[kv.Value.ParentStateName].ChildStates.Add(kv.Key, kv.Value);
                }
            }

            // Parse Transitions
            foreach (MohawkCollege.EHR.HL7v3.MIF.MIF20.Transition tx in StateMachine.Transition)
                retVal.Transitions.Add(Parse(tx, tStates));

            return retVal;
        }

        /// <summary>
        /// Parse a COR state transition from a MIF 2.0 state transition
        /// </summary>
        private static StateTransition Parse(MohawkCollege.EHR.HL7v3.MIF.MIF20.Transition tx, Dictionary<string, State> tStates)
        {
            StateTransition retVal = new StateTransition();

            // Parse
            retVal.Name = tx.Name;
            retVal.StartState = tStates[tx.StartStateName];
            retVal.EndState = tStates[tx.EndStateName];

            return retVal;
        }

        /// <summary>
        /// Parse a COR state that belongs to a state machine from a MIF 2.0 state 
        /// </summary>
        private static MohawkCollege.EHR.gpmr.COR.State Parse(MohawkCollege.EHR.HL7v3.MIF.MIF20.State st)
        {
            State retVal = new State();

            if(st.Annotations != null)
                retVal.Documentation = DocumentationParser.Parse(st.Annotations.Documentation);
            retVal.Name = st.Name;
            retVal.ParentStateName = st.ParentStateName;
            //retVal.ChildStates = new Dictionary<string, State>();

            return retVal;
        }
    }
}