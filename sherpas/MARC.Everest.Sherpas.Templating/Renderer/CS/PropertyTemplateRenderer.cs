using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Template.Interface;
using MARC.Everest.Sherpas.Templating.Format;
using System.CodeDom;
using System.Diagnostics;
using MARC.Everest.Attributes;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using System.ComponentModel;

namespace MARC.Everest.Sherpas.Templating.Renderer.CS
{
    /// <summary>
    /// Represents a template renderer that can render a property
    /// </summary>
    public class PropertyTemplateRenderer : IArtifactRenderer
    {
        /// <summary>
        /// Retrieves the property attribute for the specified property template
        /// </summary>
        public PropertyAttribute ResolvePropertyAttributeForTraversal(PropertyTemplateDefinition propertyTemplate)
        {
            var pas = propertyTemplate.Property.GetCustomAttributes(typeof(PropertyAttribute), false);
            if (pas.Length == 0) return null;

            foreach (PropertyAttribute pa in pas)
                if (pa.Name == propertyTemplate.TraversalName)
                    return pa;
            return null;
        }

        /// <summary>
        /// Gets the type of template that this renderer can generate
        /// </summary>
        public Type ArtifactTemplateType
        {
            get { return typeof(PropertyTemplateDefinition); }
        }

        /// <summary>
        /// Render the object on the specified context
        /// </summary>
        public System.CodeDom.CodeTypeMemberCollection Render(RenderContext context)
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

            // Return value
            CodeTypeMemberCollection retVal = new CodeTypeMemberCollection();

            // First, we look to override the getter and setter that already exists!!
            if (context.Parent.Artifact is ClassTemplateDefinition) // Create the property!
            {
                CodeMemberProperty propertyCode = null;

                // Add the propertyattribute
                PropertyAttribute mpa = this.ResolvePropertyAttributeForTraversal(propertyTemplate);
                if (mpa == null)
                    mpa = new PropertyAttribute()
                    {
                        Name = propertyTemplate.TraversalName
                    };
                mpa.MinOccurs = String.IsNullOrEmpty(propertyTemplate.MinOccurs) ? 0 : Int32.Parse(propertyTemplate.MinOccurs);
                mpa.MaxOccurs = propertyTemplate.MaxOccurs == "*" ? -1 : String.IsNullOrEmpty(propertyTemplate.MaxOccurs) ? 1 : Int32.Parse(propertyTemplate.MaxOccurs);
                mpa.Conformance = propertyTemplate.Conformance;

                // Container type
                if (context.ContainerObject != null)
                    propertyCode = (context.ContainerObject as CodeTypeDeclaration).Members.OfType<CodeTypeMember>().FirstOrDefault(m => m.Name == propertyTemplate.Name) as CodeMemberProperty;
                if (propertyCode == null)
                {
                    propertyCode = new CodeMemberProperty();
                    propertyCode.Name = propertyTemplate.Name;
                    propertyCode.HasGet = true;
                    propertyCode.HasSet = true;
                    propertyCode.Attributes = MemberAttributes.Public;

                    var originalCheckType = propertyTemplate.Property.PropertyType;
                    if (originalCheckType.GetInterface(typeof(IList<>).FullName) != null)
                        originalCheckType = originalCheckType.GetGenericArguments()[0];

                    // Do the types match?
                    if (propertyTemplate.TemplateReference != null)
                    {

                        // Add a property for getting/setting the property name
                        propertyCode.Name = propertyTemplate.Name;

                        // It is a list
                        if (propertyTemplate.Property != null && propertyTemplate.Property.PropertyType.GetInterface(typeof(IList<>).FullName) != null)
                        {

                            // Max occurs is exactly one so we override and call base.Add
                            if (mpa.MaxOccurs == 1)
                            {
                                propertyCode.Attributes |= MemberAttributes.New;
                                propertyCode.Type = new CodeTypeReference(propertyTemplate.TemplateReference);
                                propertyCode.SetStatements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), "Add"), new CodeVariableReferenceExpression("value")));
                                propertyCode.GetStatements.Add(new CodeTryCatchFinallyStatement(
                                    new CodeStatement[] { 
                                        new CodeMethodReturnStatement(new CodeCastExpression(new CodeTypeReference(propertyTemplate.TemplateReference), new CodeIndexerExpression(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), new CodePrimitiveExpression(0))))
                                    },
                                    new CodeCatchClause[] { 
                                        new CodeCatchClause("e", new CodeTypeReference(typeof(System.InvalidCastException)), new CodeStatement[] { new CodeMethodReturnStatement(new CodePrimitiveExpression(null)) })
                                    },
                                    new CodeStatement[] { }
                                ));

                                //propertyCode.GetStatements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), "Add"), new CodeVariableReferenceExpression("value")));

                            }
                            else 
                            {
                                propertyCode.Type = new CodeTypeReference(propertyTemplate.Property.PropertyType);
                                propertyCode.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name)));
                                propertyCode.SetStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), new CodeVariableReferenceExpression("value")));
                                var addUtil = new CodeMemberMethod()
                                {
                                    Name = String.Format("Add{0}", propertyTemplate.TemplateReference),
                                    ReturnType = new CodeTypeReference(typeof(void)),
                                    Attributes = MemberAttributes.Public

                                };
                                addUtil.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(propertyTemplate.TemplateReference), "value"));
                                addUtil.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), "Add"), new CodeVariableReferenceExpression("value")));
                                retVal.Add(addUtil);
                            }

                        }
                        else
                        {
                            propertyCode.Attributes |= MemberAttributes.New;
                            propertyCode.Type = new CodeTypeReference(propertyTemplate.TemplateReference);
                            propertyCode.GetStatements.Add(
                                new CodeTryCatchFinallyStatement(
                                    new CodeStatement[] { new CodeMethodReturnStatement(new CodeCastExpression(propertyCode.Type, new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name))) },
                                    new CodeCatchClause[] { 
                                    new CodeCatchClause("e", new CodeTypeReference(typeof(System.InvalidCastException)), new CodeStatement[] { new CodeMethodReturnStatement(new CodePrimitiveExpression(null)) })
                                },
                                    new CodeStatement[] { }
                                ));
                            propertyCode.SetStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), new CodeVariableReferenceExpression("value")));
                        }

                        mpa.Type = null;


                    }
                    else
                    {
                        if (propertyTemplate.Type != null && !RenderUtils.TypeReferenceEquals(RenderUtils.CreateTypeReference(propertyTemplate.Type), new CodeTypeReference(originalCheckType)))
                        {
                            // If the original type was a list? For some reason these don't take into account lists
                            propertyCode.Attributes |= MemberAttributes.New;
                            if (mpa.Type != null)
                                mpa.Type = propertyTemplate.Type.Type;
                        }
                        else
                            propertyCode.Attributes |= MemberAttributes.Override;

                        // First we need to create the type
                        propertyCode.Type = propertyTemplate.Type == null ? new CodeTypeReference(propertyTemplate.Property.PropertyType) : RenderUtils.CreateTypeReference(propertyTemplate.Type);
                        propertyCode.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name)));
                        propertyCode.SetStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), new CodeVariableReferenceExpression("value")));
                    }
                    // Add the property code
                    retVal.Add(propertyCode);

                }

                // Fix supplier domain?
                if (propertyTemplate.Type != null &&
                    propertyTemplate.Type.TemplateParameter != null && 
                    propertyTemplate.Type.TemplateParameter.Count > 0 &&
                    propertyTemplate.Type.TemplateParameter[0].Name != null)
                {
                    var vocab = classContext.Project.Templates.Find(o => o.Name == propertyTemplate.Type.TemplateParameter[0].Name);
                    if (vocab == null)
                        ; // TODO : Base vocab here
                    if (vocab == null) // Couldn't find so no binding!!!
                        Trace.TraceError("Cannot find the supplier domain for this type!!!");
                    else
                        mpa.SupplierDomain = vocab.Id[0];
                }

                propertyCode.Comments.AddRange(RenderUtils.RenderComments(propertyTemplate, string.Format("Template for property <see cref=\"P:{0}.{1}\"/>", classTemplate.BaseClass.Type.FullName, propertyTemplate.Name)));

                // Add custom attribute
                propertyCode.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(PropertyAttribute)),
                    new CodeAttributeArgument("Name", new CodePrimitiveExpression(mpa.Name)),
                    new CodeAttributeArgument("MinOccurs", new CodePrimitiveExpression(mpa.MinOccurs)),
                    new CodeAttributeArgument("MaxOccurs", new CodePrimitiveExpression(mpa.MaxOccurs)),
                    new CodeAttributeArgument("Conformance", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(PropertyAttribute.AttributeConformanceType)), mpa.Conformance.ToString())),
                    new CodeAttributeArgument("PropertyType", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(PropertyAttribute.AttributeAttributeType)), mpa.PropertyType.ToString())),
                    new CodeAttributeArgument("ImposeFlavorId", new CodePrimitiveExpression(mpa.ImposeFlavorId)),
                    new CodeAttributeArgument("InteractionOwner", new CodePrimitiveExpression(mpa.InteractionOwner)),
                    new CodeAttributeArgument("SortKey", new CodePrimitiveExpression(mpa.SortKey)),
                    new CodeAttributeArgument("Type", mpa.Type == null ? 
                            propertyTemplate.TemplateReference != null ?
                                new CodeTypeOfExpression(new CodeTypeReference(propertyTemplate.TemplateReference)) :
                            (CodeExpression)new CodePrimitiveExpression(null) : 
                        new CodeTypeOfExpression(new CodeTypeReference(mpa.Type))),
                    new CodeAttributeArgument("FixedValue", new CodePrimitiveExpression(null)),
                    new CodeAttributeArgument("SupplierDomain", new CodePrimitiveExpression(mpa.SupplierDomain))
                ));

                
            }

            // Render the validate and initialize methods
            if (propertyTemplate.Initialize != null)
            {
                var initializeMethod = (context.ContainerObject as CodeTypeDeclaration).Members.OfType<CodeTypeMember>().FirstOrDefault(m => m.Name == "InitializeInstance") as CodeMemberMethod;
                // Do the initialize thing
                foreach (var init in propertyTemplate.Initialize)
                    initializeMethod.Statements.AddRange(init.ToCodeDomStatement(context));
            }
            if (propertyTemplate.Validation != null)
            {
                var validateMethod = (context.ContainerObject as CodeTypeDeclaration).Members.OfType<CodeTypeMember>().FirstOrDefault(m => m.Name == "ValidateEx") as CodeMemberMethod;
                // Do the initialize thing
                foreach (var init in propertyTemplate.Validation)
                    validateMethod.Statements.AddRange(init.ToCodeDomStatement(context));
            }
            return retVal;

        }
    }
}
