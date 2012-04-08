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
 * Date: 10-03-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Formatters.XML.Datatypes.R2
{
    /// <summary>
    /// Formatter graph result for datatypes structures
    /// </summary>
    [Serializable]
    public class DatatypeR2FormatterGraphResult : IFormatterGraphResult
    {
        // Details
        private List<IResultDetail> m_details = new List<IResultDetail>(10);

        /// <summary>
        /// Add a result detail item
        /// </summary>
        internal void AddResultDetail(IResultDetail detail)
        {
            this.m_details.Add(detail);
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
        internal DatatypeR2FormatterGraphResult(bool validateConformance) { this.ValidateConformance = validateConformance;  }
        /// <summary>
        /// Datatype formatter graph result
        /// </summary>
        internal DatatypeR2FormatterGraphResult(ResultCode code, IResultDetail[] details, bool validateConformance) : this(validateConformance)
        {
            this.Code = code;
            if (details != null)
                this.m_details = new List<IResultDetail>(details);
            else
                this.m_details = new List<IResultDetail>(10);
        }

        /// <summary>
        /// Gets or sets whether the formatter validated conformance
        /// </summary>
        public bool ValidateConformance { get; set; }

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
    public class DatatypeR2FormatterParseResult : IFormatterParseResult
    {
        // Details
        private List<IResultDetail> m_details = new List<IResultDetail>(10);

        /// <summary>
        /// Add a result detail item
        /// </summary>
        internal void AddResultDetail(IResultDetail detail)
        {
            this.m_details.Add(detail);
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
        internal DatatypeR2FormatterParseResult(bool validateConformance) { this.ValidateConformance = validateConformance; }
        /// <summary>
        /// Datatype formatter parse result
        /// </summary>
        internal DatatypeR2FormatterParseResult(ResultCode code, IResultDetail[] details, bool validateConformance) : this(validateConformance)
        {
            this.Code = code;

            if (details != null)
                this.m_details = new List<IResultDetail>(details);
            else
                this.m_details = new List<IResultDetail>(10);
        }

        /// <summary>
        /// Gets or sets whether the formatter validated conformance
        /// </summary>
        public bool ValidateConformance { get; set; }
    }
}
