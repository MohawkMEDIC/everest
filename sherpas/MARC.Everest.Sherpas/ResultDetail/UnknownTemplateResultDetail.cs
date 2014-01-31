using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;

namespace MARC.Everest.Sherpas.ResultDetail
{
    /// <summary>
    /// Added to a result detail list when the formatter could not determine the template type
    /// </summary>
    public class UnknownTemplateResultDetail : NotSupportedChoiceResultDetail
    {
        public UnknownTemplateResultDetail(ResultDetailType type, Interfaces.IImplementsTemplateId templatedObject, string location) : base(type, "Could not locate a type implementing the specified template parameters", location, null)
        {
        }
    }
}
