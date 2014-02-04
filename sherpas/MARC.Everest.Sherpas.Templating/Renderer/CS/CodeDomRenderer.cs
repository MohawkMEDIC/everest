using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Interface;
using System.CodeDom;

namespace MARC.Everest.Sherpas.Templating.Renderer.CS
{
    /// <summary>
    /// Represents a renderer that uses code dom
    /// </summary>
    public class CodeDomRenderer : ITemplateRenderer
    {
        /// <summary>
        /// Gets the name of the renderer
        /// </summary>
        public string Name
        {
            get { return "CS"; }
        }

        /// <summary>
        /// Render the project to the specified directory
        /// </summary>
        public void Render(Format.TemplateProjectDefinition project, string outputFile)
        {

            CodeCompileUnit renderUnit = new CodeCompileUnit();
            
            // Start rendering the project
            foreach (var itm in project)
            {
                RenderContext context = new RenderContext(
            }

        }
    }
}
