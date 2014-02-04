using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Format;

namespace MARC.Everest.Sherpas.Templating.Interface
{
    /// <summary>
    /// Represents a class that is capable of loading a RAW template file and interpreting it
    /// </summary>
    public interface ITemplateLoader
    {

        /// <summary>
        /// Loads the specified template file into the specified project
        /// </summary>
        TemplateProjectDefinition LoadTemplate(String fileName);

    }
}
