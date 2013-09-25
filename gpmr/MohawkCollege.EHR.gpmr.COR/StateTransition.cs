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

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Represents a transition from one state into another state
    /// </summary>
    public class StateTransition
    {

        private string name;
        private State startState;
        private State endState;
        
        /// <summary>
        /// Get or sets the state the member is in when the transition is complete
        /// </summary>
        public State EndState
        {
            get { return endState; }
            set { endState = value; }
        }
	
        /// <summary>
        /// Get or set the initial state of the transition
        /// </summary>
        public State StartState
        {
            get { return startState; }
            set { startState = value; }
        }
	
        /// <summary>
        /// Get or set the name of the state transition
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Represent this state transition as a string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)")]
        public override string ToString()
        {
            return string.Format("{0} ({1} -&gt; {2})", Name, StartState.Name, EndState.Name);
        }
    }
}