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

namespace MARC.Everest.Interfaces
{
    /// <summary>
    /// This interface is an empty interface used to identify that an object can be "graphed" into a particular
    /// ITS version.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The object itself does not add any functionality, rather it merely acts as a marker for graphable classes. Formatters
    /// assume that graphable classes are:
    ///     <list type="bullet">
    ///         <item>Fully annotated with <see cref="T:MARC.Everest.Attributes.StructureAttribute"/> attributes</item>    
    ///         <item>Are serializable and correct</item>
    ///         <item>Most are generated from GPMR</item>
    /// </list>
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Graphable")]
    public interface IGraphable
    {

    }
}