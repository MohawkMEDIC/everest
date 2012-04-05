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
using MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Annotations
{
    /// <summary>
    /// Represents an annotation that 
    /// </summary>
    public class AnnotationConstraintAnnotation : ConstraintAnnotation
    {
        /// <summary>
        /// Modification target type
        /// </summary>
        public enum ModificationTargetType
        {
            Usage,
            Description,
            Walkthrough,
            Definition,
            Other,
            Rationale,
            Constraint
        }

        /// <summary>
        /// Identifies the type of annotation that is to be edited.
        /// </summary>
        public ModificationTargetType AnnotationType { get; set; }

    }
}
