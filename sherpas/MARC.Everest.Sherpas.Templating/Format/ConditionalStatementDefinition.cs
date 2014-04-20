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
        /// Supresses the ToWireFormat() conversion
        /// </summary>
        [XmlAttribute("dontConvert")]
        public bool SuppressAutoConvert { get; set; }

        /// <summary>
        /// Build a condition statement
        /// </summary>
        public CodeBinaryOperatorExpression BuildConditionExpression(RenderContext context)
        {

            CodeBinaryOperatorExpression retVal = new CodeBinaryOperatorExpression();
            CodePrimitiveExpression compareValue = new CodePrimitiveExpression(true);

            OperatorType op = this.Operator;
            if ((op & OperatorType.Not) != 0)
            {
                compareValue = new CodePrimitiveExpression(false);
            }
            op &= (OperatorType)~(int)OperatorType.Not;


            switch (op)
            {
                case OperatorType.Unknown: // Means it is NOT (we cleared that and now it is nothing)
                    {
                        CodeBinaryOperatorExpression successCriteria = null;
                        var instruction = this.Instruction.Where(o => o is ConditionalStatementDefinition);
                        if(instruction.Count() != 1)
                            throw new InvalidOperationException("NOT can only be applied to one where clause");

                        retVal.Left = (instruction.First() as ConditionalStatementDefinition).BuildConditionExpression(context);
                        retVal.Operator = CodeBinaryOperatorType.IdentityEquality;
                        retVal.Right = compareValue;
                        break;
                    }
                case OperatorType.And:
                case OperatorType.Or:
                    {
                        CodeBinaryOperatorType opType = op == OperatorType.Or ? CodeBinaryOperatorType.BooleanOr : CodeBinaryOperatorType.BooleanAnd;
                        CodeBinaryOperatorExpression successCriteria = null;
                        foreach (ConditionalStatementDefinition itm in this.Instruction.Where(o => o is ConditionalStatementDefinition))
                        {
                            itm.VariableName = itm.VariableName ?? this.VariableName;
                            itm.PropertyName = itm.PropertyName ?? this.PropertyName;
                            if (successCriteria == null)
                                successCriteria = itm.BuildConditionExpression(context);
                            else
                                successCriteria = new CodeBinaryOperatorExpression(successCriteria, opType, itm.BuildConditionExpression(context));
                        }

                        if ((bool)compareValue.Value == false)
                        {
                            retVal.Left = successCriteria;
                            retVal.Operator = CodeBinaryOperatorType.IdentityEquality;
                            retVal.Right = compareValue;
                        }
                        else
                            retVal = successCriteria;
                        break;
                    }
                    break;
                case OperatorType.Xor:
                    // Xor is (A & !B) | (B & !A)
                    {
                        CodeBinaryOperatorExpression successCriteria = null;
                        foreach(ConditionalStatementDefinition itm in this.Instruction.Where(o=> o is ConditionalStatementDefinition))
                        {
                            itm.VariableName = this.VariableName ?? itm.VariableName;
                            itm.PropertyName = this.PropertyName ?? itm.PropertyName;
                            if (successCriteria == null)
                                successCriteria = itm.BuildConditionExpression(context);
                            else 
                            {
                                CodeBinaryOperatorExpression aExpr = successCriteria,
                                    bExpr = itm.BuildConditionExpression(context);
                                successCriteria = new CodeBinaryOperatorExpression(
                                    new CodeBinaryOperatorExpression(
                                        new CodeBinaryOperatorExpression(aExpr, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(true)), // A
                                        CodeBinaryOperatorType.BitwiseAnd, // &&
                                        new CodeBinaryOperatorExpression(bExpr, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(false)) // !B
                                    ), CodeBinaryOperatorType.BooleanOr, // ||
                                    new CodeBinaryOperatorExpression(
                                        new CodeBinaryOperatorExpression(aExpr, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(false)), // !A
                                        CodeBinaryOperatorType.BooleanAnd, // &&
                                        new CodeBinaryOperatorExpression(bExpr, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(true)) // B
                                    )
                                );
                            }
                        }
                        retVal.Left = successCriteria;
                        retVal.Operator = CodeBinaryOperatorType.IdentityEquality;
                        retVal.Right = compareValue;
                        break;
                    }
                case OperatorType.Contains:
                    {

                        CodeExpressionCollection conditionCheckStatements = new CodeExpressionCollection();
                        CodeExpression scope = base.GetScopeStatement(context, null, conditionCheckStatements);

                        // Get the root context to append the method
                        var rootContext = context;
                        while (!(rootContext.CurrentObject is CodeTypeDeclaration))
                            rootContext = rootContext.Parent;

                        retVal.Right = compareValue;
                        retVal.Operator = CodeBinaryOperatorType.IdentityEquality;
                        
                        // Compare method to be called 
                        String methodName = String.Format("Is{0}Valid", (context.Artifact as PropertyTemplateContainer).Name);
                        var isCondition = this.Instruction.Find(c=>c is ConditionalStatementDefinition && (c as ConditionalStatementDefinition).Operator == OperatorType.Is);
                        if (isCondition != null)
                            methodName = String.Format("{0}Has{1}", context.Artifact.Name, (isCondition as ConditionalStatementDefinition).ValueRef);

                        CodeMemberMethod compareMethod = (rootContext.CurrentObject as CodeTypeDeclaration).Members.OfType<CodeTypeMember>().FirstOrDefault(m => m.Name == methodName) as CodeMemberMethod;
                        int i = 0;
                        while (compareMethod != null)
                        {
                            methodName = String.Format("Is{0}Valid{1}", (context.Artifact as PropertyTemplateContainer).Name, ++i);
                            compareMethod = (rootContext.CurrentObject as CodeTypeDeclaration).Members.OfType<CodeTypeMember>().FirstOrDefault(m => m.Name == methodName) as CodeMemberMethod;
                        }
                        
                        compareMethod = new CodeMemberMethod()
                            {
                                Attributes = MemberAttributes.Private | MemberAttributes.Static,
                                Name = methodName,
                                ReturnType = new CodeTypeReference(typeof(Boolean))
                            };

                        // Is it a code block?
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
                        
                        // Define the return value
                        CodeBinaryOperatorExpression successCriteria = null;
                        
                        var toBeAdded = new List<object>();
                        foreach (ConditionalStatementDefinition itm in this.Instruction.Where(o => o is ConditionalStatementDefinition))
                        {
                            itm.VariableName = "value";
                            if (successCriteria == null)
                                successCriteria = itm.BuildConditionExpression(context);
                            else
                                successCriteria = new CodeBinaryOperatorExpression(successCriteria, CodeBinaryOperatorType.BooleanAnd, itm.BuildConditionExpression(context));

                            // Are there any instructions? if so then we need to add them to the master condition as "exists" doesn't do this
                            if (itm.Instruction.Count > 0)
                                toBeAdded.AddRange(itm.Instruction.Where(o=>!(o is ConditionalStatementDefinition)));
                        }
                        this.Instruction.AddRange(toBeAdded);
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
                        retVal.Operator = this.Operator == OperatorType.Is ? CodeBinaryOperatorType.IdentityEquality : CodeBinaryOperatorType.IdentityInequality;
                        retVal.Right = compareValue;

                        // Nest... 
                        foreach (CodeExpression cond in conditionCheckStatements)
                            retVal = new CodeBinaryOperatorExpression(cond, CodeBinaryOperatorType.BooleanOr, retVal);
                        retVal = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(scope, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), CodeBinaryOperatorType.BooleanAnd, retVal);
                        break;
                    }
                case OperatorType.Equals:
                case OperatorType.GreaterThan:
                case OperatorType.LessThan:
                case OperatorType.GreaterThanEqualTo:
                case OperatorType.LessThanEqualTo:
                    {

                        // Scope
                        CodeExpressionCollection conditionCheckStatements = new CodeExpressionCollection();
                        CodeExpression scope = base.GetScopeStatement(context, null, conditionCheckStatements);

                        if (SuppressAutoConvert)
                            retVal.Left = scope;
                        else
                            retVal.Left = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Util)), "ToWireFormat"), scope);

                        // Set the rhs
                        retVal.Right = this.ValueRef != null ? (CodeExpression)new CodeSnippetExpression(this.ValueRef) : new CodePrimitiveExpression(this.Value);
                        switch (op)
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
                            case OperatorType.LessThanEqualTo:
                                retVal.Operator = CodeBinaryOperatorType.LessThanOrEqual;
                                break;
                            case OperatorType.GreaterThanEqualTo:
                                retVal.Operator = CodeBinaryOperatorType.GreaterThanOrEqual;
                                break;
                            case OperatorType.NotEquals:
                                retVal.Operator = CodeBinaryOperatorType.IdentityInequality;

                                break;
                        }

                        if ((bool)compareValue.Value == false)
                            retVal = new CodeBinaryOperatorExpression(retVal, CodeBinaryOperatorType.IdentityEquality, compareValue);
                            
                        // Nest... 
                        foreach (CodeExpression cond in conditionCheckStatements)
                            retVal = new CodeBinaryOperatorExpression(cond, CodeBinaryOperatorType.BooleanOr, retVal);

                        break;
                    }
            }

            if (retVal.Left == null || retVal.Right == null)
                System.Diagnostics.Debugger.Break();
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
