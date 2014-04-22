using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Interface;
using MARC.Everest.Sherpas.Templating.Format;
using System.Diagnostics;
using System.Reflection;

namespace MARC.Everest.Sherpas.Templating.Binder
{
    /// <summary>
    /// Represents a choice binder
    /// </summary>
    /// <remarks>Choice binders are passthrough at the binding stage and more important in the actual stage of rendering</remarks>
    public class PropertyChoiceTemplateBinder : IArtifactBinder
    {
        /// <summary>
        /// Gets the type of template this item binds to
        /// </summary>
        public Type ArtifactTemplateType
        {
            get { return typeof(PropertyChoiceTemplateDefinition); }
        }

        /// <summary>
        /// Bind the supplied context item
        /// </summary>
        public void Bind(BindingContext context)
        {

            PropertyChoiceTemplateDefinition choiceTemplate = context.Artifact as PropertyChoiceTemplateDefinition;
            Trace.TraceInformation("Entering binder for enumeration template '{0}'", choiceTemplate.Name);

            // Get the class
            BindingContext classContext = context;
            while (!(classContext.Artifact is ClassTemplateDefinition))
                classContext = classContext.Parent;
            ClassTemplateDefinition classTemplate = classContext.Artifact as ClassTemplateDefinition;

            // Scan the class to find the property which this choice is bound to
            Type scanType = classTemplate.BaseClass.Type;

            // Bind child
            foreach (var subProperty in choiceTemplate.Templates)
            {
                BindingContext childContext = new BindingContext(context, subProperty);
                var binder = childContext.GetBinder();
                if (binder == null)
                    Trace.TraceError("Could not find binder for type {0}", subProperty.GetType().FullName);
                else
                    binder.Bind(childContext);
            }

            // Now set the choice property
            if (choiceTemplate.TraversalName != null)
                choiceTemplate.Property = PropertyTemplateBinder.ResolvePropertyInfo(choiceTemplate.TraversalName, scanType);
            else
            {
                foreach (PropertyTemplateDefinition tpl in choiceTemplate.Templates)
                {
                    if (choiceTemplate.Property == null)
                        choiceTemplate.Property = tpl.Property;
                    else if (choiceTemplate.Property.Name != tpl.Property.Name &&
                        choiceTemplate.Property.PropertyType != tpl.Property.PropertyType) // conflict
                        System.Diagnostics.Debugger.Break();
                        
                }

            }
            
            // Property bound?
            if (choiceTemplate.Property == null)
            {
                Trace.TraceError("Cannot bind choice to property, bailing out!!");
                classTemplate.Templates.Remove(choiceTemplate);
                return;
            }

            choiceTemplate.Name = choiceTemplate.Property.Name;
            


            Trace.TraceInformation("Exiting binder for enumeration template '{0}'", choiceTemplate.Name);

        }
    }
}
