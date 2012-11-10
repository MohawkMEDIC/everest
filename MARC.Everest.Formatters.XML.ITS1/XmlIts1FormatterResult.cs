using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;

namespace MARC.Everest.Formatters.XML.ITS1
{
    /// <summary>
    /// Represents a formatting result from an ITS 1 formatter
    /// </summary>
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class XmlIts1FormatterGraphResult : IFormatterGraphResult
    {
        // Details
        private List<IResultDetail> m_details = new List<IResultDetail>(10);

        /// <summary>
        /// Add a result detail item
        /// </summary>
        public void AddResultDetail(IResultDetail detail)
        {
            if (detail.Type == ResultDetailType.Warning)
                this.Code = ResultCode.AcceptedNonConformant;
            else if (detail.Type == ResultDetailType.Error)
                this.Code = ResultCode.Rejected;
            this.m_details.Add(detail);
        }

        /// <summary>
        /// Add result details from an array
        /// </summary>
        public void AddResultDetail(IEnumerable<IResultDetail> details)
        {

            foreach (var itm in details)
                AddResultDetail(itm);

        }

        #region IFormatterGraphResult Members

        /// <summary>
        /// Gets the details of the parse
        /// </summary>
        public IEnumerable<IResultDetail> Details
        {
            get { return this.m_details; }
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
        internal XmlIts1FormatterGraphResult() { }
        /// <summary>
        /// Datatype formatter graph result
        /// </summary>
        internal XmlIts1FormatterGraphResult(ResultCode code, IResultDetail[] details)
        {
            this.Code = code;
            if (details == null)
                this.m_details = new List<IResultDetail>(10);
            else
                this.m_details = new List<IResultDetail>(details);
        }

       
    }

    /// <summary>
    /// Formatter parse result for ITS1 structures
    /// </summary>
    public class XmlIts1FormatterParseResult : IFormatterParseResult
    {
        // Details
        private List<IResultDetail> m_details = new List<IResultDetail>(10);

        /// <summary>
        /// Add a result detail item
        /// </summary>
        public void AddResultDetail(IResultDetail detail)
        {
            if (detail.Type == ResultDetailType.Warning)
                this.Code = ResultCode.AcceptedNonConformant;
            else if (detail.Type == ResultDetailType.Error)
                this.Code = ResultCode.Rejected;
            this.m_details.Add(detail);
        }

        /// <summary>
        /// Add result details from an array
        /// </summary>
        public void AddResultDetail(IEnumerable<IResultDetail> details)
        {

            foreach (var itm in details)
                AddResultDetail(itm);

        }
        #region IFormatterParseResult Members

        /// <summary>
        /// Gets the details of the parse
        /// </summary>
        public IEnumerable<IResultDetail> Details
        {
            get { return this.m_details; }
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
        internal XmlIts1FormatterParseResult() { }
        /// <summary>
        /// Datatype formatter parse result
        /// </summary>
        internal XmlIts1FormatterParseResult(ResultCode code, IResultDetail[] details)
        {
            this.Code = code;

            if (details != null)
                this.m_details = new List<IResultDetail>(details);
            else
                this.m_details = new List<IResultDetail>(10);
        }
    }
}
