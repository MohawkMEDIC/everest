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
using System.ComponentModel;
using System.Collections;

namespace MARC.Everest.DataTypes.Interfaces
{

    /// <summary>
    /// Identfies a class that can determine if it is empty
    /// </summary>
    public interface IColl : IEnumerable
    {
        /// <summary>
        /// Determine if the collection is empty
        /// </summary>
        bool IsEmpty { get; }
    }

    /// <summary>
    /// Identifies a class as implementing the ICollection interface
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Coll"), EditorBrowsable(EditorBrowsableState.Never)]
    public interface IColl<T> : IList<T>, IColl
    {
        /// <summary>
        /// Items to be represented
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        List<T> Items { get; }

        /// <summary>
        /// Returns true if this instance of IColl contains all the elements of <paramref name="other"/>
        /// </summary>
        BL IncludesAll(IColl<T> other);

        /// <summary>
        /// Returns true if this instance of IColl contains none of the elements of <paramref name="other"/>
        /// </summary>
        BL ExcludesAll(IColl<T> other);
    }
}