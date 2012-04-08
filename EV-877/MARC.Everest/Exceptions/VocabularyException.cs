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
using MARC.Everest.Interfaces;
using System.Xml.Serialization;

namespace MARC.Everest.Exceptions
{
    /// <summary>
    /// Signifies a vocabulary exception
    /// </summary>
    /// <remarks>
    /// This exception occurs when Everest attempts to parse a vocabulary structure such as <see cref="T:MARC.Everest.DataTypes.CD"/> and 
    /// fails to interpret the meaning of the Code. The main cause of this exception is a code that does not fall within a bound code system
    /// where no alternative code system is provided.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), Serializable]
    public class VocabularyException : MessageValidationException
    {
        /// <summary>
        /// The Mnemonic that caused the error
        /// </summary>
        public string Mnemonic { get; set; }
        /// <summary>
        /// The code set 
        /// </summary>
        public string CodeSet { get; set; }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public VocabularyException() : base() { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public VocabularyException(string message) : base(message) { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public VocabularyException(string message, Exception innerException) : base(message, innerException) { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="offender"></param>
        public VocabularyException(string message, IGraphable offender) : base(message, offender) { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="offender"></param>
        /// <param name="innerException"></param>
        public VocabularyException(string message, IGraphable offender, Exception innerException) : base(message, innerException, offender) { }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mnemonic"></param>
        /// <param name="codeSet"></param>
        /// <param name="offender"></param>
        public VocabularyException(string message, string mnemonic, string codeSet, IGraphable offender)
            : base(message, offender)
        {
            this.Mnemonic = mnemonic;
            this.CodeSet = codeSet;
        }
    }
}