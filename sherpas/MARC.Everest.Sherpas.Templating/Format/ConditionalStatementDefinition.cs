using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.CodeDom;
using MARC.Everest.Sherpas.Templating.Renderer;
using MARC.Everest.Connectors;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Indicates a condition if statement
    /// </summary>
    [XmlType("ConditionalStatementDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class ConditionalStatementDefinition : PropertyReferenceInstructionDefinition, IMethodInstruction
    {
       
        /// <summary>
        /// Gets or sets the name of the operator that this is testing
        /// </summary>
        [XmlAttribute("operator")]
        public OperatorType Operator { get; set; }
        /// <summary>
        /// Gets or sets the value being compared to
        /// </summary>
        [XmlAttribute("value")]
        public String Value { get; set; }
        /// <summary>
        /// Gets or sets a programmatic reference to the comparison
        /// </summary>
        [XmlAttribute("valueRef")]
        public String ValueRef { get; set; }

        /// <summary>
        /// Build a condition statement
        /// </summary>
        public CodeBinaryOperatorExpression BuildConditionExpression(RenderContext context)
        {

            CodeBinaryOperatorExpression retVal = new CodeBinaryOperatorExpression();

            switch (this.Operator)
            {
                case OperatorType.And:
                    break;
                case OperatorType.Or:
                    break;
                case OperatorType.Xor:
                    break;
                case OperatorType.NotContains:
                    {

                        CodeExpressionCollection conditionCheckStatements = new CodeExpressionCollection();
                        CodeExpression scope = base.GetScopeStatement(context, null, conditionCheckStatements);

                        // Get the root context to append the method
                        var rootContext = context;
                        while (!(rootContext.CurrentObject is CodeTypeDeclaration))
                            rootContext = rootContext.Parent;

                        retVal.Right = new CodePrimitiveExpression(false);
                        retVal.Operator = CodeBinaryOperatorType.IdentityEquality;
                        
                        // Compare method to be called 
                        String methodName = String.Format("Is{0}Valid", (context.Artifact as PropertyTemplateContainer).Name);
                        CodeMemberMethod compareMethod = (rootContext.CurrentObject as CodeTypeDeclaration).Members.OfType<CodeTypeMember>().FirstOrDefault(m => m.Name == methodName) as CodeMemberMethod;
                        if (compareMethod == null)
                        {
                            compareMethod = new CodeMemberMethod()
                             {
                                 Attributes = MemberAttributes.Private | MemberAttributes.Static,
                                 Name = String.Format("Is{0}Valid", (context.Artifact as PropertyTemplateContainer).Name),
                                 ReturnType = new CodeTypeReference(typeof(Boolean))
                             };
                            compareMethod.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(Boolean)), "retVal", new CodePrimitiveExpression(true)));
                            compareMethod.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("retVal")));
                            // Append the method
                            (rootContext.CurrentObject as CodeTypeDeclaration).Members.Add(compareMethod);

                            // Parameter types
                            CodeTypeReference parameterType = null;
                            if (context.Artifact is PropertyTemplateDefinition)
                                parameterType = new CodeTypeReference((context.Artifact as PropertyTemplateDefinition).Property.PropertyType.GetGenericArguments()[0]);
                            else
                            {
                                var type = (context.Artifact as ClassTemplateDefinition).BaseClass.Type;
                                if (this.PropertyName != null)
                                    parameterType = new CodeTypeReference(type.GetProperty(this.PropertyName).PropertyType.GetGenericArguments()[0]);
                                else
                                    parameterType = new CodeTypeReference(type.GetGenericArguments()[0]);
                            }
                            compareMethod.Parameters.Add(new CodeParameterDeclarationExpression(parameterType, "value"));

                        }

                        
                        // Define the return value

                        CodeBinaryOperatorExpression successCriteria = null;
                        foreach (ConditionalStatementDefinition itm in this.Instruction.Where(o => o is ConditionalStatementDefinition))
                        {
                            itm.VariableName = "value";
                            if (successCriteria == null)
                                successCriteria = itm.BuildConditionExpression(context);
                            else
                                successCriteria = new CodeBinaryOperatorExpression(successCriteria, CodeBinaryOperatorType.BooleanAnd, itm.BuildConditionExpression(context));
                        }
                        compareMethod.Statements.Insert(1, new CodeAssignStatement(new CodeVariableReferenceExpression("retVal"), new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("retVal"), CodeBinaryOperatorType.BooleanAnd, successCriteria)));


                        // return compare method call as lhs

                        retVal.Left = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(scope, "Exists"), new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(rootContext.Artifact.Name), compareMethod.Name));

                        // Nest... 
                        foreach (CodeExpression cond in conditionCheckStatements)
                            retVal = new CodeBinaryOperatorExpression(cond, CodeBinaryOperatorType.BooleanOr, retVal);

                        break;
                    }
                case OperatorType.Is:
                    {
                        // Scope
                        CodeExpressionCollection conditionCheckStatements = new CodeExpressionCollection();
                        CodeExpression scope = base.GetScopeStatement(context, null, conditionCheckStatements);

                        retVal.Left = new CodeMethodInvokeExpression(new CodeTypeOfExpression(new CodeTypeReference(this.ValueRef)), "IsAssignableFrom", new CodeMethodInvokeExpression(scope, "GetType"));
                        retVal.Operator = CodeBinaryOperatorType.IdentityEquality;
                        retVal.Right = new CodePrimitiveExpression(true);

                        // Nest... 
                        foreach (CodeExpression cond in conditionCheckStatements)
                            retVal = new CodeBinaryOperatorExpression(cond, CodeBinaryOperatorType.BooleanOr, retVal);

                        break;
                    }
                case OperatorType.Equals:
                case OperatorType.GreaterThan:
                case OperatorType.LessThan:
                case OperatorType.NotEquals:
                    {

                        // Scope
                        CodeExpressionCollection conditionCheckStatements = new CodeExpressionCollection();
                        CodeExpression scope = base.GetScopeStatement(context, null, conditionCheckStatements);

                        retVal.Left = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Util)), "ToWireFormat"), scope);

                        // Set the rhs
                        retVal.Right = this.ValueRef != null ? (CodeExpression)new CodeSnippetExpression(this.ValueRef) : new CodePrimitiveExpression(this.Value);
                        switch (this.Operator)
                        {
                            case OperatorType.Equals:
                                retVal.Operator = CodeBinaryOperatorType.ValueEquality;
                                break;
                            case OperatorType.GreaterThan:
                                retVal.Operator = CodeBinaryOperatorType.GreaterThan;
                                break;
                            case OperatorType.Is:
                                retVal.Operator = CodeBinaryOperatorType.IdentityEquality;
                                break;
                            case OperatorType.LessThan:
                                retVal.Operator = CodeBinaryOperatorType.LessThan;
                                break;
                            case OperatorType.NotEquals:
                                retVal.Operator = CodeBinaryOperatorType.IdentityInequality;

                                break;
                        }

                        // Nest... 
                        foreach (CodeExpression cond in conditionCheckStatements)
                            retVal = new CodeBinaryOperatorExpression(cond, CodeBinaryOperatorType.BooleanOr, retVal);

                        break;
                    }
            }
            return retVal;
        }

        /// <summary>
        /// Convert this where clause to an if statement
        /// </summary>
        public override CodeStatementCollection ToCodeDomStatement(RenderContext context)
        {
            String variableName = String.Format("d{0:n}", Guid.NewGuid());
            CodeConditionStatement codeCondition = new CodeConditionStatement(this.BuildConditionExpression(context));
            foreach (IMethodInstruction itm in this.Instruction.Where(i => !(i is ConditionalStatementDefinition)))
                codeCondition.TrueStatements.AddRange(itm.ToCodeDomStatement(context));
            // TODO: Write an IF Statement with conditions
            return new CodeStatementCollection() {
                codeCondition
            };
        }
    }
}
