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
 * Date: 6-5-2014
 */
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
using System.Collections;

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
            if (propertyTemplate.Property == null)
                return null;
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

            // Was this bound? If not then don't render
            if (propertyTemplate.Property == null)
            {
                Trace.TraceInformation("Property {0} was never bound!", propertyTemplate.TraversalName);
                return new CodeTypeMemberCollection();
            }

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

            string propertyDocReference = String.Format("{0}.{1}", classTemplate.BaseClass.Type.FullName, propertyTemplate.Name);
            
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
                mpa.MaxOccurs = mpa.MaxOccurs == -1 || propertyTemplate.MaxOccurs == "*" ? -1 : String.IsNullOrEmpty(propertyTemplate.MaxOccurs) ? 1 : Int32.Parse(propertyTemplate.MaxOccurs);
                mpa.Conformance = propertyTemplate.Conformance;
                
                // Container type
                if (context.ContainerObject != null)
                    propertyCode = (context.ContainerObject as CodeTypeDeclaration).Members.OfType<CodeTypeMember>().FirstOrDefault(m => m.Name == propertyTemplate.Name) as CodeMemberProperty;
                if (propertyCode == null)
                {
                    propertyCode = new CodeMemberProperty();
                    propertyCode.Name = propertyTemplate.Name ?? propertyTemplate.TraversalName;
                    propertyCode.HasGet = true;
                    propertyCode.HasSet = true;
                    propertyCode.Attributes = MemberAttributes.Public;
                    propertyCode.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(InheritPropertyAttributesAttribute))));

                    var originalCheckType = propertyTemplate.Property.PropertyType;
                    if (originalCheckType.GetInterface(typeof(IList<>).FullName) != null)
                        originalCheckType = originalCheckType.GetGenericArguments()[0];

                    // Do the types match?
                    // i.e. generate some new stuff only if we have a template reference, or when the property represents a unique property
                    if ((propertyTemplate.TemplateReference != null || propertyTemplate.Contains != null) && classTemplate.Templates.Count(t=>t.Name == propertyTemplate.Name) == 1)
                    {

                        // Add a property for getting/setting the property name
                        propertyCode.Name = propertyTemplate.Name;
                        string boundTypeName = propertyTemplate.TemplateReference ?? propertyTemplate.Contains;

                        // It is a list
                        if (propertyTemplate.Property != null && propertyTemplate.Property.PropertyType.GetInterface(typeof(IList<>).FullName) != null)
                        {

                            // Max occurs is exactly one so we override and call base.Add
                            if (mpa.MaxOccurs == 1)
                            {

                                propertyCode.Attributes |= MemberAttributes.New;
                                propertyCode.Type = new CodeTypeReference(boundTypeName);
                                propertyCode.SetStatements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), "Add"), new CodeVariableReferenceExpression("value")));
                                propertyCode.GetStatements.Add(new CodeTryCatchFinallyStatement(
                                    new CodeStatement[] { 
                                        new CodeConditionStatement(
                                            new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), "Count"), CodeBinaryOperatorType.GreaterThan, new CodePrimitiveExpression(0)), 
                                            new CodeStatement[] { 
                                                new CodeMethodReturnStatement(new CodeCastExpression(new CodeTypeReference(boundTypeName), new CodeIndexerExpression(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), new CodePrimitiveExpression(0))))
                                            },
                                            new CodeStatement[] {
                                                new CodeMethodReturnStatement(new CodePrimitiveExpression(null))
                                            }
                                        )
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
                                
                            }

                        }
                        else
                        { // We have to use an new property!
                            propertyCode.Attributes |= MemberAttributes.New;
                            propertyCode.Type = new CodeTypeReference(boundTypeName);
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
                    else // Nope! It is just a datatype change!!! We should be able to handle this!
                    {
                        if (propertyTemplate.Type != null && !RenderUtils.TypeReferenceEquals(RenderUtils.CreateTypeReference(propertyTemplate.Type, propertyTemplate.MaxOccurs, context.Project), new CodeTypeReference(propertyTemplate.Property.PropertyType)))
                        {
                            // If the original type was a list? For some reason these don't take into account lists
                            propertyCode.Attributes |= MemberAttributes.New;
                            if (mpa.Type != null)
                                mpa.Type = propertyTemplate.Type.Type;

                            // First we need to create the type
                            propertyCode.Type = RenderUtils.CreateTypeReference(propertyTemplate.Type, propertyTemplate.MaxOccurs, context.Project);
                            propertyCode.GetStatements.Add(new CodeMethodReturnStatement(
                                new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Util)), "Convert", propertyCode.Type), new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name))
                            )); // Call Util.Convert<> 
                            propertyCode.SetStatements.Add(
                                new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Util)), "Convert", new CodeTypeReference(propertyTemplate.Property.PropertyType)), new CodeVariableReferenceExpression("value")))
                            );

                        }
                        else
                        {
                            propertyCode.Attributes |= MemberAttributes.Override;

                            // First we need to create the type
                            propertyCode.Type = propertyTemplate.Type == null ? new CodeTypeReference(propertyTemplate.Property.PropertyType) : RenderUtils.CreateTypeReference(propertyTemplate.Type, propertyTemplate.MaxOccurs, context.Project);
                            propertyCode.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name)));
                            propertyCode.SetStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), new CodeVariableReferenceExpression("value")));
                        }
                    }
                    // Add the property code
                    retVal.Add(propertyCode);

                } // need to add the property
                else // don't need to add, however it looks like a duplicate
                {
                   
                    // Are the types compatible ?
                    if ((propertyTemplate.TemplateReference ?? propertyTemplate.Contains) != null &&
                        (propertyTemplate.TemplateReference ?? propertyTemplate.Contains) != propertyCode.Type.BaseType) // Change ... Back to the base
                    {
                        propertyCode.Type = new CodeTypeReference(propertyTemplate.Property.PropertyType);
                        propertyCode.GetStatements.Clear();
                        propertyCode.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name)));
                        propertyCode.SetStatements.Clear();
                        propertyCode.SetStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), new CodeVariableReferenceExpression("value")));
                    }

                }

                string loosePropertyDocName = String.Format("{0}.{1}", classTemplate.Name, propertyCode.Name);

                // Looks like a relationship to a list with contains
                if ((propertyTemplate.TemplateReference ?? propertyTemplate.Contains) != null )
                {
                    CodeMemberMethod setMethod = null, 
                        getMethod = null;

                    // List so add a Add method
                    if (propertyCode.Type.BaseType == typeof(List<>).FullName)
                    {
                        setMethod = new CodeMemberMethod()
                        {
                            Name = String.Format("Add{0}", propertyCode.Name),
                            ReturnType = new CodeTypeReference(typeof(void)),
                            Attributes = MemberAttributes.Public
                            
                        };
                        setMethod.Comments.Add(new CodeCommentStatement(String.Format("<summary>Adds an instance of <see cref=\"T:{0}\"/> to <see cref=\"P:{1}\"/></summary>", propertyTemplate.TemplateReference ?? propertyTemplate.Contains, loosePropertyDocName), true));
                        setMethod.Comments.Add(new CodeCommentStatement(String.Format("<param name=\"value\">The instance of <see cref=\"T:{0}\"/> to be added to the collection</param>", propertyTemplate.TemplateReference ?? propertyTemplate.Contains), true));
                        setMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference((propertyTemplate.TemplateReference ?? propertyTemplate.Contains)), "value"));
                        setMethod.Statements.Add(new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)),
                            new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), new CodeObjectCreateExpression(propertyCode.Type))));
                        setMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyTemplate.Name), "Add"), new CodeVariableReferenceExpression("value")));

                    }
                        // Not repeating so add a Set method
                    else 
                    {
                        setMethod = new CodeMemberMethod()
                        {
                            Name = String.Format("Set{0}", propertyCode.Name),
                            ReturnType = new CodeTypeReference(typeof(void)),
                            Attributes = MemberAttributes.Public

                        };
                        setMethod.Comments.Add(new CodeCommentStatement(String.Format("<summary>Sets <see cref=\"P:{1}\"/> to an instance of <see cref=\"T:{0}\"/></summary>", propertyTemplate.TemplateReference ?? propertyTemplate.Contains, loosePropertyDocName), true));
                        setMethod.Comments.Add(new CodeCommentStatement(String.Format("<param name=\"value\">The instance of <see cref=\"T:{0}\"/> to set the property</param>", propertyTemplate.TemplateReference ?? propertyTemplate.Contains), true));
                        setMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference((propertyTemplate.TemplateReference ?? propertyTemplate.Contains)), "value"));
                        setMethod.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), propertyTemplate.Name), new CodeVariableReferenceExpression("value")));
                    }

                    // GetXAsY method
                    getMethod = new CodeMemberMethod()
                    {
                        Name = String.Format("Get{0}As{1}", propertyCode.Name, (propertyTemplate.TemplateReference ?? propertyTemplate.Contains)),
                        Attributes = MemberAttributes.Public,
                        ReturnType = new CodeTypeReference((propertyTemplate.TemplateReference ?? propertyTemplate.Contains))
                    };
                    getMethod.Comments.Add(new CodeCommentStatement(String.Format("<summary>Gets <see cref=\"P:{1}\"/> as an instance of <see cref=\"T:{0}\"/></summary>", propertyTemplate.TemplateReference ?? propertyTemplate.Contains, loosePropertyDocName), true));
                    getMethod.Comments.Add(new CodeCommentStatement(String.Format("<returns>The value of <see cref=\"P:{1}\"/> cast as an instance of <see cref=\"T:{0}\"/>, null if <see cref=\"P:{1}\"/> is not an instance of <see cref=\"T:{0}\"/></returns>", propertyTemplate.TemplateReference ?? propertyTemplate.Contains, loosePropertyDocName), true));

                    // Get the property
                    CodeExpression referenceExpression = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), propertyCode.Name);

                    // List add indexing
                    if (typeof(List<>).FullName == propertyCode.Type.BaseType)
                    {
                        getMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(int)), "i"));
                        referenceExpression = new CodeIndexerExpression(referenceExpression, new CodeVariableReferenceExpression("i"));
                    }

                    // Parameter type
                    getMethod.Statements.Add(
                        new CodeTryCatchFinallyStatement(
                            new CodeStatement[] {
                            new CodeMethodReturnStatement(new CodeCastExpression(getMethod.ReturnType, referenceExpression))
                        },
                            new CodeCatchClause[] {
                            new CodeCatchClause("e", new CodeTypeReference(typeof(System.Exception)), 
                            new CodeMethodReturnStatement(new CodePrimitiveExpression(null)))
                        }
                        ));

                    if ((context.ContainerObject as CodeTypeDeclaration).Members.OfType<CodeMemberMethod>().Count(m => m.Name == getMethod.Name && m.ReturnType.BaseType == getMethod.ReturnType.BaseType) == 0)
                        retVal.Add(getMethod);

                    // If the helper method doesn't exist then add it
                    if(setMethod != null && (context.ContainerObject as CodeTypeDeclaration).Members.OfType<CodeMemberMethod>().Count(m=>m.Name == setMethod.Name && m.Parameters.Count > 0 && m.Parameters[0].Type.BaseType == setMethod.Parameters[0].Type.BaseType) == 0)
                        retVal.Add(setMethod);

                }


                // Fix supplier domain?
                if (propertyTemplate.Type != null &&
                    propertyTemplate.Type.TemplateParameter != null && 
                    propertyTemplate.Type.TemplateParameter.Count > 0 &&
                    propertyTemplate.Type.TemplateParameter[0].Name != null)
                {
                    var vocab = classContext.Project.Templates.Find(o => o.Name == propertyTemplate.Type.TemplateParameter[0].Name);
                    if (vocab == null)
                    {
                        ; // TODO : Base vocab here
                    }
                    if (vocab == null) // Couldn't find so no binding!!!
                        Trace.TraceError("Cannot find the supplier domain for this type!!!");
                    else
                        mpa.SupplierDomain = vocab.Id[0];
                }

                propertyCode.Comments.AddRange(RenderUtils.RenderComments(propertyTemplate, string.Format("Template for property <see cref=\"P:{0}\"/>", propertyDocReference)));

                // Add custom attribute
                var typeArgument = new CodeAttributeArgument("Type", (propertyTemplate.TemplateReference ?? propertyTemplate.Contains) != null ?
                                    new CodeTypeOfExpression(new CodeTypeReference(propertyTemplate.TemplateReference ?? propertyTemplate.Contains)) :
                                mpa.Type == null ? (CodeExpression)new CodePrimitiveExpression(null) :
                            new CodeTypeOfExpression(new CodeTypeReference(mpa.Type)));
                var propAttDecl = new CodeAttributeDeclaration(new CodeTypeReference(typeof(PropertyAttribute)),
                        new CodeAttributeArgument("Name", new CodePrimitiveExpression(mpa.Name)),
                        new CodeAttributeArgument("MinOccurs", new CodePrimitiveExpression(mpa.MinOccurs)),
                        new CodeAttributeArgument("MaxOccurs", new CodePrimitiveExpression(mpa.MaxOccurs)),
                        new CodeAttributeArgument("Conformance", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(PropertyAttribute.AttributeConformanceType)), mpa.Conformance.ToString())),
                        new CodeAttributeArgument("PropertyType", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(PropertyAttribute.AttributeAttributeType)), mpa.PropertyType.ToString())),
                        new CodeAttributeArgument("ImposeFlavorId", new CodePrimitiveExpression(propertyTemplate.Type.Flavor ?? mpa.ImposeFlavorId)),
                        new CodeAttributeArgument("DefaultUpdateMode", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(UpdateMode)), mpa.DefaultUpdateMode.ToString())),
                        new CodeAttributeArgument("InteractionOwner", new CodePrimitiveExpression(mpa.InteractionOwner)),
                        new CodeAttributeArgument("SortKey", new CodePrimitiveExpression(mpa.SortKey)),
                        new CodeAttributeArgument("FixedValue", new CodePrimitiveExpression(null)),
                        new CodeAttributeArgument("SupplierDomain", new CodePrimitiveExpression(mpa.SupplierDomain))
                    );
                
                // Add type argument
                //string propTypeName = propertyCode.Type.BaseType;
                //if (typeof(List<>).FullName == propTypeName)
                //    propTypeName = propertyCode.Type.TypeArguments[0].BaseType;

                //// Was the traversal null in the base model, is there only this one property attribute?
                //if (typeArgument.Value is CodeTypeOfExpression && 
                //    !propTypeName.Equals((typeArgument.Value as CodeTypeOfExpression).Type.BaseType))
                propAttDecl.Arguments.Add(typeArgument);

                // Remove type arguments if we have more than one traversal of the same name otherwise add
                var existingTraversal = propertyCode.CustomAttributes.OfType<CodeAttributeDeclaration>().FirstOrDefault(pa => pa.Arguments.OfType<CodeAttributeArgument>().Count(arg => arg.Name == "Name" && (arg.Value as CodePrimitiveExpression).Value.ToString() == mpa.Name) > 0);
                
                if (existingTraversal != null)
                {
                    var existingTypeArg = existingTraversal.Arguments.OfType<CodeAttributeArgument>().FirstOrDefault(p => p.Name == typeArgument.Name);
                    if(existingTypeArg != null)
                        existingTraversal.Arguments.Remove(existingTypeArg);
                }
                else
                    propertyCode.CustomAttributes.Add(propAttDecl);

                //if (propertyCode.CustomAttributes.OfType<CodeAttributeDeclaration>().Count(pa => pa.Arguments.OfType<CodeAttributeArgument>().Count(arg => arg.Name == "Name" && (arg.Value as CodePrimitiveExpression).Value.ToString() == mpa.Name) > 0 && pa.Arguments.OfType<CodeAttributeArgument>().Count(arg => arg.Name == "Type" && arg.Value.Equals(typeArgument.Value)) > 1) == 0)
                //    propertyCode.CustomAttributes.Add(propAttDecl);
                // Contains?
                if (propertyTemplate.Contains != null)
                {
                    FormalConstraintDefinition constraint = new FormalConstraintDefinition() 
                    {
                        Message = String.Format("{0} must contain {1}..{2} of {3}", propertyCode.Name, propertyTemplate.MinOccurs, propertyTemplate.MaxOccurs, propertyTemplate.Contains)
                    };
                    
                    // Add instruction
                    if (propertyCode.Type.BaseType == typeof(List<>).FullName)
                        constraint.Instruction = new List<object>()
                        {
                            new ConstructorInvokationStatementDefinition()
                            {
                                TypeName = "System.Int32",
                                VariableName = "count"
                            }, 
                            new AssignmentStatementDefinition()
                            {
                                VariableName = "count",
                                ValueRef = String.Format("value.{0}.Count(o=>o is {1})", propertyCode.Name, propertyTemplate.Contains)
                            }, 
                            new ConditionalStatementDefinition() {
                                Operator = OperatorType.And,
                                Instruction = new List<object>() {
                                    new ConditionalStatementDefinition() {
                                        VariableName = "count",
                                        Operator = OperatorType.GreaterThanEqualTo,
                                        ValueRef = propertyTemplate.MinOccurs,
                                        SuppressAutoConvert = true
                                    },
                                    new ConditionalStatementDefinition() {
                                        VariableName = "count",
                                        Operator = OperatorType.LessThanEqualTo,
                                        ValueRef = propertyTemplate.MaxOccurs == "*" ? Int32.MaxValue.ToString() : propertyTemplate.MaxOccurs,
                                        SuppressAutoConvert = true
                                    }
                                }
                            }
                        };
                    else
                    {
                        constraint.Instruction = new List<object>()
                        {
                            new ConditionalStatementDefinition() {
                                Operator = OperatorType.Is,
                                PropertyName = propertyCode.Name,
                                ValueRef = propertyTemplate.Contains
                            }
                        };
                    }
                    

                    var parent = context.ContainerObject as CodeTypeDeclaration;
                    var method = RenderUtils.RenderFormalConstraintValidator(constraint, String.Format("Has{0}", propertyTemplate.Contains), new CodeTypeReference(parent.Name), context.Parent);
                    
                    // Add the attribute 
                    parent.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(FormalConstraintAttribute)),
                        new CodeAttributeArgument("Description", new CodePrimitiveExpression(constraint.Message)),
                        new CodeAttributeArgument("CheckConstraintMethod", new CodePrimitiveExpression(method.Name))
                    ));
                    retVal.Add(method);
                }

                // Formal constraint enforcements
                if (propertyTemplate.FormalConstraint != null && propertyTemplate.FormalConstraint.Count > 0)
                {
                    // Combine formal constraints into one check with AND semantics
                    FormalConstraintDefinition fca = new FormalConstraintDefinition();
                    fca.Message = "";
                    List<String> constraints = new List<string>();
                    foreach (var fc in propertyTemplate.FormalConstraint)
                    {
                        if (fc.Instruction.Count == 0)
                            continue;

                        foreach (var instr in fc.Instruction)
                        {
                            // Where condition?
                            ConditionalStatementDefinition cond = instr as ConditionalStatementDefinition;
                            if (cond == null)
                                fca.Instruction.Add(instr);
                            else
                            {
                                var orCondition = fca.Instruction.Find(i => i is ConditionalStatementDefinition && (i as ConditionalStatementDefinition).PropertyName == cond.PropertyName) as ConditionalStatementDefinition;
                                // Is this already in the list?
                                if (orCondition != null)
                                {   // Yes, so we need to construct an OR
                                    if (orCondition.Operator == OperatorType.Or) // already an or so just add
                                        orCondition.Instruction.Add(instr);
                                    else
                                    {
                                        fca.Instruction.Remove(orCondition);
                                        // Create an or condition
                                        fca.Instruction.Add(new ConditionalStatementDefinition()
                                        {
                                            Operator = OperatorType.Or,
                                            PropertyName = orCondition.PropertyName,
                                            Instruction = new List<object>()
                                            {
                                                orCondition,
                                                cond
                                            }
                                        });
                                    }
                                }
                                else // nope just add
                                    fca.Instruction.Add(instr);
                            }
                        }
                        fca.Message += "; " + fc.Message;
                        constraints.Add(fc.Message);

                    }

                    if (fca.Instruction.Count > 0)
                    {
                        fca.Message = fca.Message.Substring(2);

                        // Code member method for constraint
                        CodeMemberMethod method = RenderUtils.RenderFormalConstraintValidator(fca, propertyTemplate.Name, propertyCode.Type, context);

                        // Add the attribute 
                        propertyCode.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(FormalConstraintAttribute)),
                            new CodeAttributeArgument("Description", new CodePrimitiveExpression(fca.Message)),
                            new CodeAttributeArgument("CheckConstraintMethod", new CodePrimitiveExpression(method.Name))
                        ));

                        // Add constraints to the documentation
                        var termRemarksComment = propertyCode.Comments.OfType<CodeCommentStatement>().LastOrDefault(o => o.Comment.Text.Contains("</remarks>"));

                        CodeCommentStatementCollection formalConstraints = new CodeCommentStatementCollection()
                    {
                        new CodeCommentStatement("<h4>Constraints</h4>", true),
                        new CodeCommentStatement("<list type=\"table\">", true),
                        new CodeCommentStatement("<listheader><term>Check #</term><description>Statement</description></listheader>", true)
                    };
                        foreach (var con in constraints)
                        {
                            String confNumber = con.StartsWith("CONF") ? con.Substring(0, con.IndexOf(":")) : constraints.IndexOf(con).ToString();
                            String confDescription = con.StartsWith("CONF") ? con.Substring(con.IndexOf(":") + 1) : con;

                            formalConstraints.Add(new CodeCommentStatement(String.Format("<item><term>{0}</term><description>{1}</description></item>", confNumber, confDescription), true));
                        }
                        formalConstraints.Add(new CodeCommentStatement("</list>", true));

                        if (termRemarksComment == null)
                        {
                            propertyCode.Comments.Add(new CodeCommentStatement("<remarks>", true));
                            propertyCode.Comments.AddRange(formalConstraints);
                            propertyCode.Comments.Add(new CodeCommentStatement("</remarks>", true));
                        }
                        else
                        {
                            var insertIdx = propertyCode.Comments.IndexOf(termRemarksComment);
                            for (int i = formalConstraints.Count - 1; i >= 0; i--)
                                propertyCode.Comments.Insert(insertIdx, formalConstraints[i]);
                        }


                        // Add the method
                        retVal.Add(method);
                    }
                }

            }

            var initializeMethod = (context.ContainerObject as CodeTypeDeclaration).Members.OfType<CodeTypeMember>().FirstOrDefault(m => m.Name == "InitializeInstance") as CodeMemberMethod;
            var validateMethod = (context.ContainerObject as CodeTypeDeclaration).Members.OfType<CodeTypeMember>().FirstOrDefault(m => m.Name == "ValidateEx") as CodeMemberMethod;

            // Render the validate and initialize methods
            if (propertyTemplate.Initialize != null && propertyTemplate.GetPropertyAttribute().PropertyType != PropertyAttribute.AttributeAttributeType.Traversable)
                // Do the initialize thing
                foreach (var init in propertyTemplate.Initialize)
                    initializeMethod.Statements.AddRange(init.ToCodeDomStatement(context));
            //else if (propertyTemplate.TemplateReference != null)
            //    // Cascade initializers
            //    initializeMethod.Statements.Add(new CodeConditionStatement(
            //        new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), propertyTemplate.Name), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)),
            //        new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), propertyTemplate.Name), "InitializeInstance"))
            //    ));

            if (propertyTemplate.Validation != null)
            {

                foreach (var init in propertyTemplate.Validation)
                {
                    if (propertyTemplate.Type.Type.GetInterface(typeof(IList<>).FullName) != null)
                    {
                        validateMethod.Statements.AddRange(
                        new ConditionalStatementDefinition()
                        {
                            Operator = OperatorType.NotContains,
                            Instruction = RenderUtils.NegateConditions(init.Instruction)
                        }.ToCodeDomStatement(context));
                    }
                    else
                        validateMethod.Statements.AddRange(init.ToCodeDomStatement(context));
                }

            }

            // Cascade Validators
            //if (propertyTemplate.TemplateReference != null)
            //    // Cascade initializers
            //    validateMethod.Statements.Add(new CodeConditionStatement(
            //        new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), propertyTemplate.Name), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)),
            //        new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("retVal"), "AddRange", new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), propertyTemplate.Name), "ValidateEx")))
            //    ));

           
            return retVal;

        }
    }
}
