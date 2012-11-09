/* 
 * Copyright 2011 Mohawk College of Applied Arts and Technology
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
 * Date: 06-17-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;

#if WINDOWS_PHONE
using MARC.Everest.Phone;
#endif

namespace MARC.Everest.Formatters.XML.Datatypes.R1
{
    /// <summary>
    /// Identifies the compatibility mode for the data type formatter
    /// </summary>
    public enum DatatypeFormatterCompatibilityMode
    {
        /// <summary>
        /// The datatype formatter should use the Universal definition of datatypes when formatting
        /// </summary>
        Universal,
        /// <summary>
        /// The datatype formatter should use the Canadian extensions to data types R1 when formatting
        /// </summary>
        Canadian,
        /// <summary>
        /// The data type formatter should be set to emulate the data types that are commonly used as part of 
        /// CDAr2 
        /// </summary>
        ClinicalDocumentArchitecture
    }

    /// <summary>
    /// Formatter graph result for datatypes structures
    /// </summary>
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class DatatypeFormatterGraphResult : IFormatterGraphResult
    {
        // Details
        private List<IResultDetail> m_details = new List<IResultDetail>(10);

        /// <summary>
        /// Gets or sets the operation mode
        /// </summary>
        public DatatypeFormatterCompatibilityMode CompatibilityMode { get; set; }

        /// <summary>
        /// Gets or sets whether the formatter validates
        /// </summary>
        public bool ValidateConformance { get; set; }

        /// <summary>
        /// Add a result detail item
        /// </summary>
        internal void AddResultDetail(IResultDetail detail)
        {
            this.m_details.Add(detail);
        }

        /// <summary>
        /// Remove a result detail
        /// </summary>
        internal void RemoveResultDetail(Predicate<IResultDetail> predicate)
        {
            this.m_details.RemoveAll(predicate);
        }

        #region IFormatterGraphResult Members

        /// <summary>
        /// Gets the details of the parse
        /// </summary>
        public IResultDetail[] Details
        {
            get { return this.m_details.ToArray(); }
        }

        /// <summary>
        /// Gets the result code of the formatting result
        /// </summary>
        public ResultCode Code
        {
            get;
            internal set;
        }

        #endregion

        /// <summary>
        /// Datatype formatter graph result
        /// </summary>
        internal DatatypeFormatterGraphResult(DatatypeFormatterCompatibilityMode mode) {
            this.CompatibilityMode = mode;
        }
        /// <summary>
        /// Datatype formatter graph result
        /// </summary>
        internal DatatypeFormatterGraphResult(DatatypeFormatterCompatibilityMode mode, ResultCode code, IResultDetail[] details, bool validating) : this(mode)
        {
            this.Code = code;
            this.ValidateConformance = validating;
            if (details != null)
                this.m_details = new List<IResultDetail>(details);
            else
                this.m_details = new List<IResultDetail>(10);
        }

        /// <summary>
        /// Add result details from an array
        /// </summary>
        internal void AddResultDetail(IEnumerable<IResultDetail> details)
        {
            m_details.AddRange(details);
        }
    }

    /// <summary>
    /// Formatter parse result for datatypes structures
    /// </summary>
    public class DatatypeFormatterParseResult : IFormatterParseResult
    {
        // Details
        private List<IResultDetail> m_details = new List<IResultDetail>(10);

        /// <summary>
        /// Gets or sets the operation mode
        /// </summary>
        public DatatypeFormatterCompatibilityMode CompatibilityMode { get; set; }

        /// <summary>
        /// Gets or sets whether this formatter will validate conformance
        /// </summary>
        public bool ValidateConformance { get; set; }

        /// <summary>
        /// Add a result detail item
        /// </summary>
        internal void AddResultDetail(IResultDetail detail)
        {
            this.m_details.Add(detail);
        }

        /// <summary>
        /// Remove a result detail
        /// </summary>
        internal void RemoveResultDetail(Predicate<IResultDetail> predicate)
        {
            this.m_details.RemoveAll(predicate);
        }


        /// <summary>
        /// Add result details from an array
        /// </summary>
        internal void AddResultDetail(IEnumerable<IResultDetail> details)
        {
            m_details.AddRange(details);
        }

        #region IFormatterParseResult Members

        /// <summary>
        /// Gets the details of the parse
        /// </summary>
        public IResultDetail[] Details
        {
            get { return this.m_details.ToArray(); }
        }

        /// <summary>
        /// Gets the result code of the formatting result
        /// </summary>
        public ResultCode Code
        {
            get;
            internal set;
        }

        public MARC.Everest.Interfaces.IGraphable Structure
        {
            get;
            internal set;
        }

        #endregion

        /// <summary>
        /// Datatype formatter parse result
        /// </summary>
        internal DatatypeFormatterParseResult(DatatypeFormatterCompatibilityMode mode) {
            this.CompatibilityMode = mode;   
        }
        /// <summary>
        /// Datatype formatter parse result
        /// </summary>
        internal DatatypeFormatterParseResult(DatatypeFormatterCompatibilityMode mode, ResultCode code, IResultDetail[] details, bool validating) :this(mode) 
        {
            this.Code = code;
            this.ValidateConformance = validating;
            if (details != null)
                this.m_details = new List<IResultDetail>(details);
            else
                this.m_details = new List<IResultDetail>(10);
        }
    }
}
