using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templating.Interface;
using MARC.Everest.Sherpas.Templating.Format;
using System.Diagnostics;
using System.Reflection;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes;
using System.Collections;
using MARC.Everest.Sherpas.ResultDetail;

namespace MARC.Everest.Sherpas.Templating.Binder
{
    /// <summary>
    /// A binder that can operate on property templates
    /// </summary>
    public class PropertyTemplateBinder : IArtifactBinder
    {
        /// <summary>
        /// Gets the artifact type that this binder can operate on
        /// </summary>
        public Type ArtifactTemplateType
        {
            get { return typeof(PropertyTemplateDefinition); }
        }

        /// <summary>
        /// Find the property info
        /// </summary>
        public static PropertyInfo ResolvePropertyInfo(String traversalName, Type container)
        {
            foreach (var pi in container.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var attrs = pi.GetCustomAttributes(typeof(PropertyAttribute), false);
                foreach (PropertyAttribute pa in attrs)
                {
                    if (pa.Name == traversalName)
                        return pi;
                }
            }
            return null;
        }

        /// <summary>
        /// Find the property info
        /// </summary>
        public static Type ResolvePropertyTraversalType(String traversalName, PropertyInfo pi)
        {
            var attrs = pi.GetCustomAttributes(typeof(PropertyAttribute), false);
            foreach (PropertyAttribute pa in attrs)
            {
                if (pa.Name == traversalName && pa.Type != null)
                    return pa.Type;
            }
            // No? then base 
            if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                return pi.PropertyType.GetGenericArguments()[0];
            return pi.PropertyType;
        }



        /// <summary>
        /// Bind the property to the model
        /// </summary>
        public void Bind(BindingContext context)
        {

            // Get the class context
            PropertyTemplateDefinition propertyTemplate = context.Artifact as PropertyTemplateDefinition;
            Trace.TraceInformation("Starting bind on {0}", propertyTemplate.TraversalName);
            BindingContext classContext = context;
            while (!(classContext.Artifact is ClassTemplateDefinition))
                classContext = classContext.Parent;
            ClassTemplateDefinition classTemplate = classContext.Artifact as ClassTemplateDefinition;
            string originalContains = propertyTemplate.Contains;

            // Bind the proeprty type we need to find the option that we're binding to
            Type contextType = classTemplate.BaseClass.Type;
            var parentProperty = context.Parent.Artifact as PropertyTemplateDefinition;
            if (parentProperty != null)
                contextType = ResolvePropertyTraversalType(parentProperty.TraversalName, parentProperty.Property);
           
            // List?
            if (contextType.GetInterface(typeof(IList<>).FullName) != null)
                contextType = contextType.GetGenericArguments()[0];

            // Look for a property with the specified traversal name
            propertyTemplate.Property = ResolvePropertyInfo(propertyTemplate.TraversalName, contextType);
            if (propertyTemplate.Property == null)
            {
                // Potentially it has been added? not supported for now remove
                Trace.TraceInformation("Could not find a property with traversal {0} in type {1}", propertyTemplate.TraversalName, contextType.FullName);
                (propertyTemplate as PropertyTemplateContainer).Templates.Remove(propertyTemplate);
                return;
            }
            propertyTemplate.Name = propertyTemplate.Property.Name;

            // No explcit property
            Type propertyType = propertyTemplate.Property.PropertyType;
            if (propertyTemplate.Type == null || propertyType.GetInterface(typeof(IList<>).FullName) != null &&
                propertyType.GetGenericArguments()[0] == propertyTemplate.Type.Type)
                propertyTemplate.Type = new TypeDefinition() { Type = propertyType };

            // Bound to a more stringent code system in the base class so use that one instead
            if (typeof(CS<String>).IsAssignableFrom(propertyTemplate.Type.Type) &&
                (propertyTemplate.Type.TemplateParameter == null || 
                propertyTemplate.Type.TemplateParameter.Count == 0) &&
                propertyType.GetGenericArguments().Length > 0 &&
                propertyType.GetGenericArguments()[0] != typeof(String))
                propertyTemplate.Type = new TypeDefinition() { Type = propertyType };
            
            // Do we need to bind the type on a contains relationship?
            if (propertyTemplate.Contains != null)
            {
                ClassTemplateDefinition containedClassTemplate = context.Project.Templates.Find(o => o.Name == propertyTemplate.Contains) as ClassTemplateDefinition;

                if (containedClassTemplate != null)
                {
                    var baseType = ResolvePropertyTraversalType(containedClassTemplate.TraversalName ?? propertyTemplate.TraversalName, propertyTemplate.Property);

                    var pi = ResolvePropertyInfo(containedClassTemplate.TraversalName, contextType);
                    if (pi == null) // Couldn't find at this level so go down one level
                    {
                        // Todo : Is there a property within this type that can be used?
                        pi = ResolvePropertyInfo(containedClassTemplate.TraversalName, baseType);
                        if (pi != null)
                        {

                            baseType = ResolvePropertyTraversalType(containedClassTemplate.TraversalName, pi);

                            containedClassTemplate = ResolveContainedClassConflicts(containedClassTemplate, baseType, context);
                            propertyTemplate.Contains = containedClassTemplate.Name;
                            containedClassTemplate.BaseClass.Type = baseType;

                            // Propogate down
                            propertyTemplate.Templates.Add(new PropertyTemplateDefinition()
                            {
                                Name = pi.Name,
                                TraversalName = containedClassTemplate.TraversalName,
                                Property = pi,
                                Type = new TypeDefinition() { Type = pi.PropertyType },
                                TemplateReference = propertyTemplate.TemplateReference,
                                Contains = propertyTemplate.Contains
                            });
                            propertyTemplate.TemplateReference = propertyTemplate.Contains = null;
                        }
                        else
                        {
                            containedClassTemplate = ResolveContainedClassConflicts(containedClassTemplate, baseType, context);
                            propertyTemplate.Contains = containedClassTemplate.Name;
                            containedClassTemplate.BaseClass.Type = baseType;
                        }

                    }
                    else
                        containedClassTemplate.BaseClass.Type = baseType;


                }
            }
            else if (propertyTemplate.TemplateReference != null) // a simple reference
            {
                ClassTemplateDefinition containedClassTemplate = context.Project.Templates.Find(o => o.Name == (propertyTemplate.TemplateReference)) as ClassTemplateDefinition;
                if (containedClassTemplate != null)
                {
                    var baseType = ResolvePropertyTraversalType(containedClassTemplate.TraversalName ?? propertyTemplate.TraversalName, propertyTemplate.Property);
                    containedClassTemplate.BaseClass.Type = baseType;
                }
                else
                {
                    Trace.TraceInformation("Could not find a contains class of {0} in project", propertyTemplate.TemplateReference ?? propertyTemplate.Contains, contextType.FullName);
                    (propertyTemplate as PropertyTemplateContainer).Templates.Remove(propertyTemplate);
                    return;
                }

            }

            // Bind the statements
            foreach (var itm in propertyTemplate.Initialize)
                BindStatement(itm, propertyTemplate);
            foreach (var itm in propertyTemplate.Validation)
                BindStatement(itm, propertyTemplate);
            foreach (var itm in propertyTemplate.FormalConstraint)
                BindStatement(itm, propertyTemplate);

            // Cascade sub-properties into their own class
            if (propertyTemplate.Templates.Count > 0)
            {

                String name = String.Format("{0}_{1}", classTemplate.Name, propertyTemplate.Name);
                // Originally contained something? Add it to the name
                if (!String.IsNullOrEmpty(originalContains))
                    name = originalContains + "Container";

                
                // TODO: More intelligent generation of these
                String uniqueName = name;
                for (int i = 1; context.Project.Templates.Exists(o => o.Name == uniqueName); i++)
                    uniqueName = String.Format("{0}{1}", name, i);

                // Generate the class definition
                ClassTemplateDefinition childTemplate = new ClassTemplateDefinition()
                {
                    Name = uniqueName,
                    FormalConstraint = new List<FormalConstraintDefinition>(propertyTemplate.FormalConstraint),
                    Initialize = new List<MethodDefinitionBase>(propertyTemplate.Initialize),
                    Templates = new List<PropertyTemplateContainer>(propertyTemplate.Templates)
                };


                Type baseClass = ResolvePropertyTraversalType(propertyTemplate.TraversalName, propertyTemplate.Property);

                if (baseClass.GetInterface(typeof(IList<>).FullName) != null)
                    baseClass = baseClass.GetGenericArguments()[0];

                if (!typeof(ANY).IsAssignableFrom(baseClass)) // ok go ahead and do this
                {
                    // Update the reference here
                    childTemplate.BaseClass = new BasicTypeReference() { Type = baseClass };
                    //propertyTemplate.Type = new TypeDefinition() { Name = childTemplate.Name };

                    if (String.IsNullOrEmpty(originalContains))
                        propertyTemplate.TemplateReference = childTemplate.Name;
                    else
                        propertyTemplate.Contains = childTemplate.Name;
                    
                    // Add to the repo
                    context.Project.Templates.Add(childTemplate);
                    foreach (NullArtifactTemplate nat in context.Project.Templates.FindAll(o => o is NullArtifactTemplate))
                        nat.ReplacementFor = null;
                    propertyTemplate.Templates.Clear(); // remove templates as we've cascaded them
                    propertyTemplate.FormalConstraint.Clear();
                    propertyTemplate.Initialize.Clear();
                }
            }
            //// Now we're done binding, cascade down 
            //foreach (var subProperty in propertyTemplate.Templates)
            //{
            //    BindingContext childContext = new BindingContext(context, subProperty);
            //    var binder = childContext.GetBinder();
            //    if (binder == null)
            //        Trace.TraceError("Could not find binder for type {0}", subProperty.GetType().FullName);
            //    else
            //        binder.Bind(childContext);
            //}

            // Are there more than one properties with the same name?
            for (int i = 0; i < propertyTemplate.Templates.Count; i++)
                if (propertyTemplate.Templates.Count(t => t.Name == propertyTemplate.Templates[i].Name) > 1) // We have more than one!
                {
                    // Collapse them together ... maybe?
                    for (int z = 0; z < propertyTemplate.Templates.Count; z++)
                        if (z != i && propertyTemplate.Templates[i].Name == propertyTemplate.Templates[z].Name)
                        {
                            (propertyTemplate.Templates[i] as PropertyTemplateDefinition).Merge(propertyTemplate.Templates[z] as PropertyTemplateDefinition);
                            propertyTemplate.Templates.RemoveAt(z); // Remove the duplicate
                            z--;
                        }
                }

            // Cacade the contains decisions
            BindingUtils.CreateContainsChoice(propertyTemplate);

            
            Trace.TraceInformation("Ending bind on {0}", context.Artifact.Name);
        }

        private ClassTemplateDefinition ResolveContainedClassConflicts(ClassTemplateDefinition containedClassTemplate, Type baseType, BindingContext context)
        {
            // Is this template compatible with the current property? No? Then clone and rename
            if (containedClassTemplate.BaseClass.Type != null && !baseType.IsAssignableFrom(containedClassTemplate.BaseClass.Type) && !containedClassTemplate.BaseClass.Type.IsAssignableFrom(baseType))
            {
                string uniqueName = containedClassTemplate.Name;
                int i = 0;
                ClassTemplateDefinition otherUnique = context.Project.Templates.First(t => t.Name == uniqueName) as ClassTemplateDefinition;
                while (otherUnique != null)
                {
                    // While we're here, is this assignable?
                    if (otherUnique.BaseClass.Type != null && (
                        baseType.IsAssignableFrom(otherUnique.BaseClass.Type) || otherUnique.BaseClass.Type.IsAssignableFrom(baseType))
                    )
                        return otherUnique;
                    uniqueName = String.Format("{0}{1}", containedClassTemplate.Name, ++i);
                    otherUnique = context.Project.Templates.Find(t => t != null && t.Name == uniqueName) as ClassTemplateDefinition;
                }

                containedClassTemplate = new ClassTemplateDefinition()
                {
                    Name = uniqueName,
                    Documentation = containedClassTemplate.Documentation,
                    Example = containedClassTemplate.Example,
                    FormalConstraint = new List<FormalConstraintDefinition>(containedClassTemplate.FormalConstraint),
                    Initialize = new List<MethodDefinitionBase>(containedClassTemplate.Initialize),
                    Templates = new List<PropertyTemplateContainer>(containedClassTemplate.Templates),
                    TraversalName = containedClassTemplate.TraversalName,
                    Validation = new List<MethodDefinitionBase>(containedClassTemplate.Validation),
                    BaseClass = new BasicTypeReference() { Name = "MARC.Everest.IGraphable" }
                };
                containedClassTemplate.Name = uniqueName;
                context.Project.Templates.Add(containedClassTemplate);
                
            }
            return containedClassTemplate;
        }

        /// <summary>
        /// Bind the statement
        /// </summary>
        public static void BindStatement(MethodDefinitionBase method, ArtifactTemplateBase template)
        {

            Type contextType = typeof(System.Object);
            if (template is PropertyTemplateDefinition)
                contextType = (template as PropertyTemplateDefinition).Type.Type;
            else if (template is ClassTemplateDefinition)
                contextType = (template as ClassTemplateDefinition).BaseClass.Type;

            if (contextType.GetInterface(typeof(IList<>).FullName) != null)
                contextType = contextType.GetGenericArguments()[0];

            var trashBin = new List<Object>();

            // Method instruction
            foreach (var stmt in method.Instruction)
            {
                var prid = stmt as PropertyReferenceInstructionDefinition;
                if (prid != null && prid.PropertyName != null)
                {
                    var pi = ResolvePropertyInfo(prid.PropertyName, contextType);
                    if (pi != null)
                        prid.PropertyName = pi.Name;
                    else if(contextType.GetProperty(prid.PropertyName) == null)
                        trashBin.Add(stmt);
                }

                var cont = stmt as MethodDefinitionBase;

                if (cont != null)
                    BindStatement(cont, template);
            }


            method.Instruction.RemoveAll(o => trashBin.Contains(o));
        }
    }
}
