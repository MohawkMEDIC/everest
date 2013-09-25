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
using MARC.Everest.Interfaces;
using System.Xml.Serialization;

namespace MARC.Everest.Exceptions
{
    /// <summary>
    /// Marks a message level validation exception
    /// </summary>
    /// <remarks>
    /// This exception usually occurs due to a formal constraint violation in which the formatter can no longer 
    /// reliably interpret data.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly")]
#if WINDOWS_PHONE
    public class MessageValidationException : Exception
#else
    [Serializable]
    public class MessageValidationException : ApplicationException
#endif
    {

        /// <summary>
        /// Get the offending message part
        /// </summary>
        public IGraphable Offender { get; private set; }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public MessageValidationException() : base() { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MessageValidationException(string message) : base(message) { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MessageValidationException(string message, Exception innerException)
            : base
                (message, innerException) { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="offender"></param>
        public MessageValidationException(string message, Exception innerException, IGraphable offender)
            : base(message, innerException)
        {
            this.Offender = offender;
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="offender"></param>
        public MessageValidationException(string message, IGraphable offender)
            : base(message)
        {
            this.Offender = offender;
        }


    }
}