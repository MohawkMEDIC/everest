using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Interface;
using MARC.Everest.Sherpas.Templating.Format;
using MARC.Everest.Interfaces;
using System.Reflection;
using System.Diagnostics;

namespace MARC.Everest.Sherpas.Templating.Binder
{
    /// <summary>
    /// Class template binder
    /// </summary>
    public class ClassTemplateBinder : IArtifactBinder
    {

        /// <summary>
        /// Identifies the artifact template types
        /// </summary>
        public Type ArtifactTemplateType
        {
            get { return typeof(ClassTemplateDefinition); }
        }

        /// <summary>
        /// Bind the template
        /// </summary>
        public void Bind(BindingContext context)
        {
            // Attempt to find the base class for the binding ... eh?
            ClassTemplateDefinition classTemplate = context.Artifact as ClassTemplateDefinition;
            Trace.TraceInformation("Entering binder for class template '{0}'", classTemplate.Name);

            // Base class 
            if (classTemplate.BaseClass != null)
                try
                {
                    Trace.TraceInformation("Class is based on '{0}'", classTemplate.BaseClass.Type.FullName);
                }
                catch { 
                    ;
                }
            else
                throw new InvalidOperationException("Cannot have ClassTemplates which are not bound to a physical type");
            // Bind properties
            Trace.TraceInformation("Processing Contents");
            var tplColl = new List<PropertyTemplateContainer>(classTemplate.Templates);
            foreach(var tpl in tplColl)
            {
                BindingContext childContext = new BindingContext(context, tpl);
                var binder = childContext.GetBinder();
                if (binder == null)
                    Trace.TraceInformation("Could not find template binder for type '{0}'", tpl.GetType().Name);
                else
                    binder.Bind(childContext);
            }


            // trace finish
            Trace.TraceInformation("Finished binding '{0}'", classTemplate.Name);
        }
    }
}
