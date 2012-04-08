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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Identifies a valid state transition for a property
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class StateTransitionAttribute : NamedAttribute
    {
        /// <summary>
        /// The name of the state that a state can be entered from
        /// </summary>
        public string FromState { get; set; }

        /// <summary>
        /// Create a new instance of the state transition attribute
        /// </summary>
        public StateTransitionAttribute() : base() { }
        /// <summary>
        /// Create a new instance of the state transition attribute with given from state and new state
        /// </summary>
        /// <param name="FromState">The state that the transition starts in</param>
        /// <param name="Name">The name of the state the transition may go to</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Name"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "From")]
        public StateTransitionAttribute(String FromState, String Name) : base(Name) { this.FromState = FromState; }
    }
}