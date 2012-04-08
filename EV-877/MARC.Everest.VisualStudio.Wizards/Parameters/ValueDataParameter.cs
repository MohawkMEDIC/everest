using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.VisualStudio.Wizards.Interfaces;

namespace MARC.Everest.VisualStudio.Wizards.Parameters
{
    /// <summary>
    /// Wizard parameter that has values
    /// </summary>
    public class ValueDataParameter : WizardParameter
    {
        /// <summary>
        /// Gets or sets the value of the wizard data parameter
        /// </summary>
        public object Value { get; set; }
    }
}
