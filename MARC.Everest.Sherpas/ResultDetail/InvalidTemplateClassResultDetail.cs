using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;

namespace MARC.Everest.Sherpas.ResultDetail
{
    /// <summary>
    /// Indicates the class which implements a template cannot be used
    /// </summary>
    public class InvalidTemplateClassResultDetail : NotImplementedResultDetail
    {

        public const string INVALID_CTOR = "The class '{0}' does not have a default constructor";

        /// <summary>
        /// Creates a new invalid template class detail
        /// </summary>
        public InvalidTemplateClassResultDetail(ResultDetailType type, System.Type offendingType, string problemStatement, string location) : base(type, String.Format(problemStatement, offendingType), location)
        {
        }
 

    }
}
