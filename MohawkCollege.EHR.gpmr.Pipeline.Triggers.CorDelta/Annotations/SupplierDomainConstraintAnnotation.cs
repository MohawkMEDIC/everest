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
 * Date: 12-11-2011
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Annotations
{
    /// <summary>
    /// Represents a constraint whereby the supplier domain is constrained
    /// </summary>
    public class SupplierDomainConstraintAnnotation : ConstraintAnnotation
    {

        /// <summary>
        /// If true, then the constraint is weakly referenced (ie: Is a name rather than a strong refernece to a vocab)
        /// </summary>
        public bool IsWeaklyReferenced { get { return this.NewValue is String; } }

        /// <summary>
        /// Get a strong reference, null if a weak reference
        /// </summary>
        public object StrongReference { get { return this.NewValue as Feature; } }

        /// <summary>
        /// Get the weak refernece, null if a strong reference
        /// </summary>
        public object WeakReference { get { return this.NewValue as String; } }
    }
}
