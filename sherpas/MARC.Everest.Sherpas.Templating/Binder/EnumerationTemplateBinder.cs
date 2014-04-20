using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Interface;
using MARC.Everest.Sherpas.Templating.Format;
using System.Diagnostics;
using System.Reflection;
using MARC.Everest.Attributes;

namespace MARC.Everest.Sherpas.Templating.Binder
{
    /// <summary>
    /// A template binder for the template enumeration
    /// </summary>
    public class EnumerationTemplateBinder : IArtifactBinder
    {
        /// <summary>
        /// Artifact template type binder
        /// </summary>
        public Type ArtifactTemplateType
        {
            get { return typeof(EnumerationTemplateDefinition); }
        }

        /// <summary>
        /// Resolve the specified concept domain
        /// </summary>
        public static Type ResolveConceptDomain(String domainId, Assembly asm)
        {
            foreach (var t in asm.GetTypes().Where(ot => ot.IsEnum))
            {
                var customAttributes = t.GetCustomAttributes(typeof(StructureAttribute), false);
                if (customAttributes.Length == 0) continue;
                var structAtt = customAttributes[0] as StructureAttribute;
                if (structAtt.StructureType != StructureAttribute.StructureAttributeType.CodeSystem &&
                    structAtt.StructureType != StructureAttribute.StructureAttributeType.ValueSet)
                    continue; // improper structure

                // Match?
                if (structAtt.CodeSystem == domainId)
                    return t;

            }

            return null;
        }

        /// <summary>
        /// Bind the specified context
        /// </summary>
        public void Bind(BindingContext context)
        {
            // First, do we even need to do anything?
            var enumTemplate = context.Artifact as EnumerationTemplateDefinition;
            Trace.TraceInformation("Entering binder for enumeration template '{0}'", enumTemplate.Name);

            // Binding enumerations is easy ... first, we need to see if it is a ref?
            if (enumTemplate.ConceptDomainReference != null)
            {
                Trace.TraceInformation("Resolving concept domain '{0}'.", enumTemplate.ConceptDomainReference.Name);

                // So find it
                var boundEnum = EnumerationTemplateBinder.ResolveConceptDomain(enumTemplate.ConceptDomainReference.Name, context.BindAssembly);
                if (boundEnum == null) // couldn't find!
                {
                    Trace.TraceInformation("Could not find concept domain, removing reference");
                    enumTemplate.ConceptDomainReference = null;
                }
                else // Fill out the 
                {
                    Trace.TraceInformation("Binding to '{0}'", boundEnum.FullName);
                    enumTemplate.ConceptDomainReference.Type = boundEnum;
                }

            }
            else
            {
                // Enumeration literals... these can get messy... We want to remove any duplicate mnemonics
                List<Object> trashBin = new List<object>();
                foreach (var itm in enumTemplate.Literal)
                    if(!trashBin.Contains(itm))
                        foreach (var dup in enumTemplate.Literal.FindAll(l => l.Code == itm.Code && l.Literal == itm.Literal && enumTemplate.Literal.IndexOf(l) > enumTemplate.Literal.IndexOf(itm)))
                            trashBin.Add(dup);
                enumTemplate.Literal.RemoveAll(l => trashBin.Contains(l));

            }
            // Emit finish
            Trace.TraceInformation("Finished binding '{0}'", enumTemplate.Name);

        }
    }
}
