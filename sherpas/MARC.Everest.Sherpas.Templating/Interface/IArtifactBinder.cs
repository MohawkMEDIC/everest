using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Format;
using MARC.Everest.Sherpas.Templating.Binder;

namespace MARC.Everest.Sherpas.Templating.Interface
{
    /// <summary>
    /// Identifies a class that can bind an artifact to code
    /// </summary>
    public interface IArtifactBinder
    {

        /// <summary>
        /// Gets the type of artifact this binder operates on
        /// </summary>
        Type ArtifactTemplateType { get; }

        /// <summary>
        /// Binds the specified artifact
        /// </summary>
        void Bind(BindingContext context);


    }
}
