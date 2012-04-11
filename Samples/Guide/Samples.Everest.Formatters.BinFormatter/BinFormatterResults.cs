using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;

namespace Samples.Everest.Formatters.BinFormatter
{
    /// <summary>
    /// Binary formatter graph restul
    /// </summary>
    public class BinaryFormatterGraphResult : IFormatterGraphResult
    {
        #region IFormatterGraphResult Members

        /// <summary>
        /// Overall outcome of the graph operation
        /// </summary>
        public ResultCode Code
        {
            get;
            internal set;
        }

        /// <summary>
        /// Details of the graph operation
        /// </summary>
        public IResultDetail[] Details
        {
            get;
            internal set;
        }

        #endregion
    }

    /// <summary>
    /// Binary formatter parse result
    /// </summary>
    public class BinaryFormatterParseResult : IFormatterParseResult
    {
        #region IFormatterParseResult Members

        /// <summary>
        /// Gets the overall result of the parse
        /// </summary>
        public ResultCode Code
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the details of the parse
        /// </summary>
        public IResultDetail[] Details
        {
            get;
            internal set;
        }

        /// <summary>
        /// Get the structure that was parsed
        /// </summary>
        public MARC.Everest.Interfaces.IGraphable Structure
        {
            get;
            internal set;
        }

        #endregion
    }
}
