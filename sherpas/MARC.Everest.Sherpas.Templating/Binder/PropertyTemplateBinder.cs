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
        /// Update method to path
        /// </summary>
        public static void UpdateMethodToPath(MethodDefinitionBase method, Type containerType, PropertyTemplateDefinition template)
        {
            Type realType = containerType;
            if (containerType.GetInterface(typeof(IList<>).FullName) != null && containerType.IsGenericType)
                realType = containerType.GetGenericArguments()[0];

            foreach (var ins in method.Instruction)
            {
                if (ins is ConstructorInvokationStatementDefinition)
                {
                    var asd = ins as ConstructorInvokationStatementDefinition;

                    // Find the type
                    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        var t = Array.Find(asm.GetTypes(), o => o.FullName == asd.TypeName);
                        if (t != null)
                        {
                            containerType = t;
                            break;
                        }
                    }
                }
                else if (ins is AssignmentStatementDefinition)
                {
                    var asd = ins as AssignmentStatementDefinition;
                    if (asd.PropertyName != null && !(method is ConstructorInvokationStatementDefinition))
                    {
                        var pi = ResolvePropertyInfo(asd.PropertyName, realType);
                        if(pi != null)
                            asd.PropertyName = pi.Name;
                    }
                    
                }
                else if (ins is ConditionalStatementDefinition)
                {
                    var asd = ins as ConditionalStatementDefinition;

                    if (asd.PropertyName != null && (realType != containerType || template != null && template.Property != null && template.Property.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null) && asd.Operator != OperatorType.NotContains)
                    {
                        var pi = ResolvePropertyInfo(asd.PropertyName, realType);
                        if (pi != null)
                        {
                            asd.Instruction.Insert(0, new ConditionalStatementDefinition()
                            {
                                PropertyName = pi.Name,
                                Operator = asd.Operator == OperatorType.NotEquals ? OperatorType.Equals : OperatorType.NotEquals,
                                Value = asd.Value,
                                ValueRef = asd.ValueRef
                            });
                            asd.Operator = OperatorType.NotContains;
                            asd.ValueRef = null;
                            asd.Value = null;
                            asd.PropertyName = null;
                        }
                        continue;
                    }
                    else if (!String.IsNullOrEmpty(asd.PropertyName))
                    {
                        var pi = ResolvePropertyInfo(asd.PropertyName, realType);
                        if(pi != null)
                            asd.PropertyName = pi.Name;
                    }

                }

                if (ins is MethodDefinitionBase)
                    UpdateMethodToPath(ins as MethodDefinitionBase, realType, null);
                
            }
        }

        /// <summary>
        /// Bind
        /// </summary>
        public void Bind(BindingContext context)
        {
            var propertyTemplate = context.Artifact as PropertyTemplateDefinition;
            Trace.TraceInformation("Entering binder for property template '{0}'", propertyTemplate.TraversalName);
            // Class template
            String pathName = String.Empty;
            var classContext = context.Parent;
            while (classContext != null && !(classContext.Artifact is ClassTemplateDefinition))
            {
                pathName = String.Format("{0}.{1}", classContext.Artifact.Name, pathName);

                classContext = classContext.Parent;
            }
            if (classContext == null)
                throw new InvalidOperationException("PropertyTemplateDefinition must be contained within a ClassTemplateDefinition");
            var classTemplate = classContext.Artifact as ClassTemplateDefinition;

            // Find the property name .. the actual property that we're binding to
            Type scanType = typeof(System.Object);
            if(classContext == context.Parent) // we are scanning the root class
                scanType = classTemplate.BaseClass.Type;
            else
            {
                var parentPropertyTemplate = context.Parent.Artifact as PropertyTemplateDefinition;
                scanType = ResolvePropertyTraversalType(parentPropertyTemplate.TraversalName, parentPropertyTemplate.Property);
            }

            
            // List?
            if (scanType != null && scanType.GetInterface(typeof(IList<>).FullName) != null)
                scanType = scanType.GetGenericArguments()[0];

            // Seek property
            if(!String.IsNullOrEmpty(propertyTemplate.Name))
            {
                propertyTemplate.Property = scanType.GetProperty(propertyTemplate.Name);
            }
            else if (propertyTemplate.Property == null)
            {
                Trace.TraceInformation("Finding property with traversal name '{0}' in class '{1}'", propertyTemplate.TraversalName, scanType.Name);
                propertyTemplate.Property = ResolvePropertyInfo(propertyTemplate.TraversalName, scanType);
            }

            // Property check
            if (propertyTemplate.Property == null)
            {
                Trace.TraceError("Could not find property, removing from template");
                // TODO: Remove
                return;
            }
            else
            {
                propertyTemplate.Name = propertyTemplate.Property.Name;
                Trace.TraceInformation("Bound '{0}' to '{1}.{2}'", propertyTemplate.TraversalName, scanType.FullName, propertyTemplate.Property.Name);  
            }

            var propertyTraversalType = ResolvePropertyTraversalType(propertyTemplate.TraversalName, propertyTemplate.Property);
            if (propertyTemplate.Type != null)
                propertyTraversalType = propertyTemplate.Type.Type;


            // Perhaps we need to correct things like lists?
            if (typeof(ANY).IsAssignableFrom(propertyTemplate.Property.PropertyType) &&
                propertyTemplate.Property.PropertyType.GetInterface(typeof(IList<>).FullName) != null &&
                propertyTemplate.Property.PropertyType.GetGenericArguments()[0] == propertyTemplate.Type.Type)
                // Correct ?
                propertyTemplate.Type.Type = propertyTemplate.Property.PropertyType;

            // Move validation and initialization down
            foreach (var mi in propertyTemplate.Initialize)
                UpdateMethodToPath(mi, propertyTraversalType, propertyTemplate);
            foreach(var mi in propertyTemplate.Validation)
                UpdateMethodToPath(mi, propertyTraversalType, propertyTemplate);

            // Is there agreement 
            // Is this a datatype or another type?
            if (propertyTemplate.Contains != null)
            {
                // Attempt to find the template reference and set its base class! Since we know the context
                Trace.TraceInformation("Property is bound to contains relationship of '{0}' will create this rule...", propertyTemplate.Contains);
                var referencedTemplate = context.Project.Templates.Find(o => o.Name == propertyTemplate.Contains && o is ClassTemplateDefinition) as ClassTemplateDefinition;
                if (referencedTemplate == null)
                    throw new InvalidOperationException(String.Format("Attempt to make a reference to an unknown template '{0}'", propertyTemplate.Contains));

                // First, is the current propertyInfo a choice?
                if(propertyTemplate.Property.GetCustomAttributes(typeof(PropertyAttribute), false).Length == 1)
                {
                    // Is the choice in here?
                    var pi = ResolvePropertyInfo(referencedTemplate.TraversalName, propertyTraversalType);
                    // Create a new template for the contains relationship
                    if (pi == null)
                        throw new InvalidOperationException("Could not bind property choice element on contains relationship");
                    else
                    {
                        var childPropertyTemplate = new PropertyTemplateDefinition()
                        {
                            MinOccurs = propertyTemplate.MinOccurs,
                            MaxOccurs = propertyTemplate.MaxOccurs,
                            Conformance = PropertyAttribute.AttributeConformanceType.Mandatory,
                            Documentation = propertyTemplate.Documentation,
                            Name = pi.Name,
                            TraversalName = referencedTemplate.TraversalName,
                            TemplateReference = referencedTemplate.Name,
                            Property = pi
                        };
                        propertyTemplate.MinOccurs = childPropertyTemplate.MinOccurs;
                        propertyTemplate.MaxOccurs = (propertyTemplate.Property.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute).MaxOccurs.ToString();
                        propertyTemplate.Contains = null;
                        propertyTemplate.Templates.Add(childPropertyTemplate);

                        // Update validation statements
                        foreach (var method in propertyTemplate.Validation)
                        {
                            bool needMove = false;
                            foreach (var ins in method.Instruction)
                            {
                                if (ins is ConditionalStatementDefinition)
                                {
                                    var cond = ins as ConditionalStatementDefinition;
                                    if (cond.PropertyName == null &&
                                        cond.Operator == OperatorType.NotContains) // update the contains logic
                                    {
                                        cond.PropertyName = propertyTemplate.Name;
                                        if(cond.ValueRef != null || cond.Value != null)
                                            cond.Instruction.Insert(0, new ConditionalStatementDefinition()
                                            {
                                                PropertyName = childPropertyTemplate.Name,
                                                ValueRef = cond.ValueRef,
                                                Value = cond.Value,
                                                Operator = OperatorType.Is
                                            });
                                        cond.Value = cond.ValueRef = null;
                                        needMove = true;
                                    }
                                }
                            }

                            if (needMove)
                            {
                                (context.Parent.Artifact as PropertyTemplateContainer).Validation.Add(new MethodDefinitionBase()
                                {
                                    Instruction = new List<object>(method.Instruction)
                                });
                                method.Instruction.Clear();
                            }
                        }

                        propertyTemplate.Initialize.Clear();
                        
                        propertyTemplate = childPropertyTemplate;
                    }
                    
                }
            }
            

            if (propertyTemplate.TemplateReference != null)
            {
                // Attempt to find the template reference and set its base class! Since we know the context
                Trace.TraceInformation("Property is bound to template '{0}', will set it's base class...", propertyTemplate.TemplateReference);
                var referencedTemplate = context.Project.Templates.Find(o => o.Name == propertyTemplate.TemplateReference && o is ClassTemplateDefinition) as ClassTemplateDefinition;
                if (referencedTemplate == null)
                    throw new InvalidOperationException(String.Format("Attempt to make a reference to an unknown template '{0}'", propertyTemplate.TemplateReference));
                else
                {
                    referencedTemplate.BaseClass = new BasicTypeReference() { Type = ResolvePropertyTraversalType(propertyTemplate.TraversalName, propertyTemplate.Property) };
                    // Re-process this class
                    try
                    {
                        BindingContext childContext = new BindingContext(context.BindAssembly, referencedTemplate, context.Project);
                        childContext.GetBinder().Bind(childContext);
                    }
                    catch { }
                    Trace.TraceInformation("Bound template '{0}' to '{1}'", referencedTemplate.Name, referencedTemplate.BaseClass.Type.FullName);
                }
            }

            // Process sub-components? These go as a new class template
            
            var originalTemplate = context.Artifact as PropertyTemplateDefinition;
            if (originalTemplate == propertyTemplate && originalTemplate.Templates.Count > 0) // TODO: Only do this if there are any changes
            {
                ClassTemplateDefinition surrogateClassTemplate = new ClassTemplateDefinition()
                {
                    BaseClass = new BasicTypeReference() { Type = ResolvePropertyTraversalType(originalTemplate.TraversalName, originalTemplate.Property) },
                    Name = String.Format("{0}_{1}", classContext.Artifact.Name, originalTemplate.Name),
                    TraversalName = originalTemplate.TraversalName,
                    Templates = new List<PropertyTemplateContainer>(originalTemplate.Templates),
                    Validation = new List<MethodDefinitionBase>(originalTemplate.Validation),
                    Initialize = new List<MethodDefinitionBase>(originalTemplate.Initialize),
                    Example = originalTemplate.Example
                };
                originalTemplate.Templates.Clear();
                originalTemplate.Validation.Clear();
                originalTemplate.Initialize.Clear();
                originalTemplate.Example = null;
                originalTemplate.TemplateReference = surrogateClassTemplate.Name;
                originalTemplate.Type = null;
                context.Project.Templates.Add(surrogateClassTemplate);
            }
            else
                foreach (var tpl in originalTemplate.Templates)
                {
                    BindingContext childContext = new BindingContext(context, tpl);
                    var binder = childContext.GetBinder();
                    if (binder == null)
                        Trace.TraceInformation("Could not find template binder for type '{0}'", tpl.GetType().Name);
                    else
                        binder.Bind(childContext);
                }


            // trace finish
            Trace.TraceInformation("Finished binding '{0}'", propertyTemplate.Name);
        }
    }
}
