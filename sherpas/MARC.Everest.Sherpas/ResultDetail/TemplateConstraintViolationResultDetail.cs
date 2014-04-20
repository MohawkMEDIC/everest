using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;

namespace MARC.Everest.Sherpas.ResultDetail
{

    /// <summary>
    /// Represents a violation of a template constraint
    /// </summary>
    public abstract class TemplateConstraintViolationResultDetail : FormalConstraintViolationResultDetail
    {

        /// <summary>
        /// Creates a new constraint violation result detail
        /// </summary>
        public TemplateConstraintViolationResultDetail() : base(null)
        {

        }

        /// <summary>
        /// Creates a new template constraint violation result detail
        /// </summary>
        public TemplateConstraintViolationResultDetail(ResultDetailType type, String message)
            : this(type, message, null, null)
        {
        }

        /// <summary>
        /// Creates a new template constraint violation result detail
        /// </summary>
        public TemplateConstraintViolationResultDetail(ResultDetailType type, String message, String location) : this(type, message, location, null)
        {
        }

        /// <summary>
        /// Template constraint violation result detail
        /// </summary>
        public TemplateConstraintViolationResultDetail(ResultDetailType type, String message, String location, System.Exception cause) : base(type, message, location, cause)
        {
        }

    }
}
