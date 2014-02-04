using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Format;

namespace MARC.Everest.Sherpas.Templating.Interface
{
    /// <summary>
    /// Template renderer is responsible for binding the template into a programming language
    /// </summary>
    public interface ITemplateRenderer
    {

        /// <summary>
        /// Gets the name of the template renderer
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Render the specified template to the specified output directory
        /// </summary>
        void Render(TemplateProjectDefinition project, String outputDirectory);

    }
}
