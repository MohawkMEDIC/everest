using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;

namespace MARC.Everest.Sherpas.ResultDetail
{

    /// <summary>
    /// Types of constraints placed in templates
    /// </summary>
    public enum TemplateConstraintType
    {
        FixedValueMismatch,
        InvalidCodeSystem

    }

    /// <summary>
    /// Represents a violation of a template constraint
    /// </summary>
    public class TemplateConstraintViolationResultDetail : FormalConstraintViolationResultDetail
    {

        /// <summary>
        /// Template constraint violation result detail
        /// </summary>
        public TemplateConstraintViolationResultDetail(ResultDetailType type, String message) : base(type, message, null, null)
        {

        }

    }
}
