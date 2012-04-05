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
using System.Xml.Serialization;

namespace MARC.Everest.Exceptions
{
    /// <summary>
    /// Identifies that a duplicate item has been added to a set that doesn't allow duplicate items
    /// </summary>
    /// <remarks>
    /// This exception is thrown whenever an attempt is made to add a duplicate item. This exception exists
    /// to enforce a conformance rule on <see cref="T:MARC.Everest.DataTypes.SET{T}"/> and is only fired if:
    /// <list type="bullet">
    ///     <item>The result of the comparator is 0 (ie: the data within the items are identical), or</item>
    ///     <item>The default comparator is 0 (ie: the object reference is the same)</item>
    /// </list>
    /// <para>The type of condition under which this exception is thrown depends solely on the type
    /// of comparator used in the SET</para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"), Serializable]
    public class DuplicateItemException : ArgumentException
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public DuplicateItemException() : base() { }
        /// <summary>
        /// Ctor taking a message.
        /// </summary>
        /// <param name="Message">An ExceptionMessage.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Message")]
        public DuplicateItemException(string Message) : base(Message) { }
        /// <summary>
        /// Ctor taking a exception message and inner exception.
        /// </summary>
        /// <param name="Message">An ExceptionMessage.</param>
        /// <param name="InnerException">An InnerException.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Message"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Inner")]
        public DuplicateItemException(string Message, Exception InnerException) : base(Message, InnerException) { }
    }
}