/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: fyfej
 * Date: 20-4-2014
 */
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
                    if (classTemplate.BaseClass.Type == null) // kick the can down the road
                    {
                        if (context.Project.Templates.Exists(t => t is NullArtifactTemplate && (t as NullArtifactTemplate).ReplacementFor == context.Artifact.Name))
                        {
                            context.Project.Templates[context.Project.Templates.IndexOf(context.Artifact)] = new NullArtifactTemplate() { ReplacementFor = context.Artifact.Name };
                            Trace.TraceWarning("Will ignore this template item as it appears nobody referenced it!");
                            return;
                        }
                        else
                        {
                            Trace.TraceInformation("Will process this later as it seems nothing has used it!");
                            context.Project.Templates[context.Project.Templates.IndexOf(context.Artifact)] = new NullArtifactTemplate() { ReplacementFor = context.Artifact.Name };
                            context.Project.Templates.Add(context.Artifact);
                            return;
                        }
                    }
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

           

            // Move validation and initialization down
            foreach (var mi in classTemplate.Initialize)
                PropertyTemplateBinder.BindStatement(mi, classTemplate);
            foreach (var mi in classTemplate.Validation)
                PropertyTemplateBinder.BindStatement(mi, classTemplate);
            foreach(var mi in classTemplate.FormalConstraint)
                PropertyTemplateBinder.BindStatement(mi, classTemplate);
           
            // trace finish
            Trace.TraceInformation("Finished binding '{0}'", classTemplate.Name);
        }
    }
}
