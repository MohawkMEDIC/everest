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
 * Date: 10-05-2011
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Annotations
{


    /// <summary>
    /// A constraint annotation is a special class
    /// that is used to annotate a feature to describe
    /// a constraint that is placed on the feature
    /// </summary>
    public abstract class ConstraintAnnotation : Annotation
    {

        /// <summary>
        /// Gets or sets the code of the realm that
        /// is placed on the feature
        /// </summary>
        public string RealmCode { get; set; }

        /// <summary>
        /// Realm name
        /// </summary>
        public string RealmName { get; set; }

        /// <summary>
        /// Gets or sets the type of change
        /// </summary>
        public ChangeType ChangeType { get; set; }

        /// <summary>
        /// Gets or sets the new value of the constraint
        /// </summary>
        public object NewValue { get; set; }
    }
}
