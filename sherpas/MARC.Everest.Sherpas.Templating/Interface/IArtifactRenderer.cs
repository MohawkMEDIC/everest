using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Interface;
using MARC.Everest.Sherpas.Templating.Renderer;

namespace MARC.Everest.Sherpas.Template.Interface
{
    /// <summary>
    /// Represents a renderer that can represet an artifact
    /// </summary>
    public interface IArtifactRenderer
    {
        /// <summary>
        /// Gets the type that this renderer will represent
        /// </summary>
        Type ArtifactTemplateType { get; }

        /// <summary>
        /// Render the specified object returning the declared member
        /// </summary>
        System.CodeDom.CodeTypeMember Render(RenderContext context);
    }
}
