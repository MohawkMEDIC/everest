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
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Attributes
{
    /// <summary>
    /// Annotation for the render as
    /// </summary>
    public class RenderAsAnnotation : Annotation
    {
        /// <summary>
        /// Gets or sets the name to render as
        /// </summary>
        public string RenderName { get; set; }
    }
}
