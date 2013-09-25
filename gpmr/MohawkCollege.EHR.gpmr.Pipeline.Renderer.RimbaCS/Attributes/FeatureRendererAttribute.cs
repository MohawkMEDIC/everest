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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Attributes
{
    /// <summary>
    /// Attribute is used to define a class as having the ability to render a particular type of feature
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Renderer"), AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FeatureRendererAttribute : Attribute
    {
        /// <summary>
        /// Get or set the name of the feature
        /// </summary>
        public Type Feature { get; set; }
        /// <summary>
        /// True if the feature requires the creation of a file
        /// </summary>
        public bool IsFile { get; set; }
    }
}