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
 * User: fyfej
 * Date: 01-09-2011
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Represents a value set style enumeration within GPMR
    /// </summary>
    public class ValueSet : Enumeration
    {

        /// <summary>
        /// Gets or sets the filter expression from which the value set was derived
        /// </summary>
        public List<FilterExpression> FilterExpression { get; set;  }

        /// <summary>
        /// Programmatically determine if this is a valueset
        /// </summary>
        public override bool IsValueSet
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Get the type of enumeration structure this class represents
        /// </summary>
        public override string EnumerationType
        {
            get { return "Value Set"; }
        }

        /// <summary>
        /// Gets or sets the context binding for the value set
        /// </summary>
        public Enumeration ContextBinding { get; set; }
    }
}
