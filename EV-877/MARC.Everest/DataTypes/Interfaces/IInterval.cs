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
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Identifies an interval
    /// </summary>
    public interface IInterval<T>
    {
        /// <summary>
        /// Original text
        /// </summary>
        ED OriginalText { get; set; }
        /// <summary>
        /// Low value in the set
        /// </summary>
        T Low { get; set; }
        /// <summary>
        /// True if the bottom bounds is inclusive of the set
        /// </summary>
        bool? LowClosed { get; set; }
        /// <summary>
        /// High value in the set
        /// </summary>
        T High { get; set; }
        /// <summary>
        /// True if the top bound is inclusive of the set
        /// </summary>
        bool? HighClosed { get; set; }
        /// <summary>
        /// Width of the interval
        /// </summary>
        PQ Width { get; set; }
    }
}