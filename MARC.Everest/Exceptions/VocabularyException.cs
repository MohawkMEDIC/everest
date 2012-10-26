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
        /// <summary>
        /// Creates a new instance of the VocabularyException
        /// </summary>
        /// <param name="message">The textual message describing the exception</param>
        public VocabularyException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of the VocabularyException
        /// </summary>
        /// <param name="message">The textual message describing the exception</param>
        /// <param name="innerException">The exception that caused this exception to be raised</param>
        public VocabularyException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// Creates a new instance of the VocabularyException
        /// </summary>
        /// <param name="message">The textual message of the exception</param>
        /// <param name="offender">The <see cref="T:MARC.Everest.Interfaces.IGraphable"/> interface which caused the exception to be thrown</param>
        public VocabularyException(string message, IGraphable offender) : base(message, offender) { }
        /// <summary>
        /// Creates a new instance of the VocabularyException
        /// </summary>
        /// <param name="message">The textual message of the exception</param>
        /// <param name="offender">The <see cref="T:MARC.Everest.Interfaces.IGrapable"/> instance which caused the exception to be thrown</param>
        /// <param name="innerException">The exception that caused this exception to be thrown</param>
        public VocabularyException(string message, Exception innerException, IGraphable offender) : base(message, innerException, offender) { }
        /// <summary>
        /// Creates a new instance of the VocabularyException
        /// </summary>
        /// <param name="message">The textual message describing the exception</param>
        /// <param name="mnemonic">The code mnemonic that caused the vocabulary exception to be thrown</param>
        /// <param name="codeSet">The code set from which the <paramref name="mnemonic"/> was drawn</param>
        /// <param name="offender">The <see cref="T:MARC.Everest.Interfaces.IGraphable"/> instance which caused the exception to be thrown</param>
        public VocabularyException(string message, string mnemonic, string codeSet, IGraphable offender)
            : base(message, offender)
        {
            this.Mnemonic = mnemonic;
            this.CodeSet = codeSet;
        }
    }
}