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
 * Author: Justin Fyfe
 * Date: 07-21-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Represents a structure that contains the results of a graph operation
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface has been added to Everest to facilitate clean up of the formatting
    /// architecture within previous versions of Everest. 
    /// </para>
    /// <para>
    /// Whenever a formatter's Graph method is called, it will return a populated <see cref="T:MARC.Everest.Connectors.IFormatterGraphResult"/>
    /// instance with the overall result of graphing and the details of the graph.
    /// </para>
    /// </remarks>
    public interface IFormatterGraphResult
    {

        /// <summary>
        /// Gets the details of the graphing operation
        /// </summary>
        /// <remarks>
        /// The details array contain a list of <see cref="T:MARC.Everest.Connectors.IResultDetail"/> instances
        /// that describe the outcome of the formatter operation. These classes have been architected similar to 
        /// exceptions in that the content of the class along with the type of class determines the meaning of the 
        /// result detail.
        /// </remarks>
        /// <seealso cref="T:MARC.Everest.Connectors.ResultDetail"/>
        /// <seealso cref="T:MARC.Everest.Connectors.InsufficientRepetitionsResultDetail"/>
        /// <seealso cref="T:MARC.Everest.Connectors.MandatoryElementMissingResultDetail"/>
        /// <seealso cref="T:MARC.Everest.Connectors.RequiredElementMissingResultDetail"/>
        /// <seealso cref="T:MARC.Everest.Connectors.FixedValueMisMatchedResultDetail"/>
        /// <seealso cref="T:MARC.Everest.Connectors.VocabularyIssueResultDetail"/>
        /// <seealso cref="T:MARC.Everest.Connectors.UnsupportedDatatypePropertyResultDetail"/>
        IEnumerable<IResultDetail> Details { get; }
        /// <summary>
        /// Gets the result code that describes the overall result of the graph operation
        /// </summary>
        ResultCode Code { get; }

    }
}
