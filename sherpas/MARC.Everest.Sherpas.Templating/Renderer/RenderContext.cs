using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Format;
using MARC.Everest.Sherpas.Template.Interface;
using System.Reflection;

namespace MARC.Everest.Sherpas.Templating.Renderer
{
    /// <summary>
    /// Represents a rendering context
    /// </summary>
    public class RenderContext
    {
        // Renderers
        private static Dictionary<Type, IArtifactRenderer> s_renderers = new Dictionary<Type, IArtifactRenderer>();

        /// <summary>
        /// Static ctor
        /// </summary>
        static RenderContext()
        {
            foreach (var t in typeof(RenderContext).Assembly.GetTypes().Where(bc => typeof(IArtifactRenderer).IsAssignableFrom(bc)))
            {
                if (t.IsInterface) continue;

                ConstructorInfo ci = t.GetConstructor(Type.EmptyTypes);
                if (ci == null) throw new InvalidOperationException("Rendering type doesn't have parameterless constructor");
                IArtifactRenderer binder = ci.Invoke(null) as IArtifactRenderer;
                s_renderers.Add(binder.ArtifactTemplateType, binder);
            }
        }

        /// <summary>
        /// Creates a new binding context
        /// </summary>
        public RenderContext(ArtifactTemplateBase artifact, TemplateProjectDefinition project, Object renderedObject)
        {
            this.Artifact = artifact;
            this.Project = project;
            this.ContainerObject = renderedObject;
        }

        /// <summary>
        /// Creates a new binding context
        /// </summary>
        public RenderContext(RenderContext parent, ArtifactTemplateBase artifact, Object renderedObject)
        {
            this.Parent = parent;
            this.Artifact = artifact;
            this.Project = parent.Project;
            this.ContainerObject = renderedObject;
        }

        /// <summary>
        /// Get the artifact binder
        /// </summary>
        public IArtifactRenderer GetRenderer()
        {
            if (this.Artifact == null)
                throw new ArgumentNullException(String.Empty, "Artifact must not be null!");

            IArtifactRenderer retVal = null;
            if (s_renderers.TryGetValue(this.Artifact.GetType(), out retVal))
                return retVal;
            return null;
        }

        /// <summary>
        /// Gets or sets the current generated item into which the current context item will be placed
        /// </summary>
        public Object ContainerObject { get; set; }

        /// <summary>
        /// Gets the parent context for this context
        /// </summary>
        public RenderContext Parent { get; private set; }

        /// <summary>
        /// Gets the artifact for the binding
        /// </summary>
        public ArtifactTemplateBase Artifact { get; private set; }

        /// <summary>
        /// Identifies the project to which the item belongs
        /// </summary>
        public TemplateProjectDefinition Project { get; private set; }

        /// <summary>
        /// Current object
        /// </summary>
        public Object CurrentObject { get; set; }

    }
}
