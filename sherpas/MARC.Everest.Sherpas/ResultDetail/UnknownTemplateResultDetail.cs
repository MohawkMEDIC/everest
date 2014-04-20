using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Sherpas.ResultDetail
{
    /// <summary>
    /// Added to a result detail list when the formatter could not determine the template type
    /// </summary>
    public class UnknownTemplateResultDetail : NotSupportedChoiceResultDetail
    {
        /// <summary>
        /// Identifies the object which for which the specified template could not be found
        /// </summary>
        public IImplementsTemplateId TemplatedObject { get; set; }

        /// <summary>
        /// Creates a new UnknownTemplateResultDetail instance
        /// </summary>
        public UnknownTemplateResultDetail(ResultDetailType type, Interfaces.IImplementsTemplateId templatedObject, string location) : base(type, "Could not locate a type implementing the specified template parameters", location, null)
        {
            this.TemplatedObject = templatedObject;
        }
    }
}
