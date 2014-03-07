using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.CodeDom;
using MARC.Everest.Sherpas.Templating.Renderer;
using MARC.Everest.Connectors;
using MARC.Everest.Sherpas.Templating.Binder;
using System.Reflection;
using MARC.Everest.Attributes;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents an assignment statement
    /// </summary>
    [XmlType("AssignmentStatementDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class AssignmentStatementDefinition : PropertyReferenceInstructionDefinition, IMethodInstruction
    {

        
        /// <summary>
        /// Gets or sets the string value of the statement
        /// </summary>
        [XmlAttribute("value")]
        public String Value { get; set; }
        /// <summary>
        /// Gets or sets the value reference (programmatic statement) that will set the value
        /// </summary>
        [XmlAttribute("valueRef")]
        public String ValueRef { get; set; }

        /// <summary>
        /// Find the property info
        /// </summary>
        public static PropertyInfo ResolvePropertyInfo(String propertyName, Type container)
        {
            return container.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name == propertyName);
        }

        /// <summary>
        /// Convert this to a codedom statement
        /// </summary>
        public override CodeStatementCollection ToCodeDomStatement(RenderContext context)
        {
            return this.ToCodeDomStatement(context, false);
        }

        /// <summary>
        /// Special toCodeDomStatement that indicates a var decl needs to eb included
        /// </summary>
        internal CodeStatementCollection ToCodeDomStatement(RenderContext context, bool declRequired)
        {
            var retVal = new CodeStatementCollection();
            // Scope
            var scope = base.GetScopeStatement(context, retVal);

            // Initialize the values
            CodeExpression value = null;
            if (this.Value != null)
            {
                var tpl = context.Artifact as PropertyTemplateDefinition;
                Type scanType = null;

                if (tpl != null)
                    scanType = tpl.Property.PropertyType;
                else
                    scanType = (context.Artifact as ClassTemplateDefinition).BaseClass.Type;
                if (scanType.GetInterface(typeof(IList<>).FullName) != null)
                    scanType = scanType.GetGenericArguments()[0];

                PropertyInfo pi = ResolvePropertyInfo(this.PropertyName, scanType); 

                if (pi != null && pi.PropertyType != typeof(String))
                    value = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Util)), "Convert", new CodeTypeReference(pi.PropertyType)), new CodePrimitiveExpression(this.Value));
                else
                    value = new CodePrimitiveExpression(this.Value);

            }
            else if (this.ValueRef != null)
                value = new CodeSnippetExpression(this.ValueRef);
            else if (this.Instruction != null && this.Instruction.Count == 1)
            {
                if (this.Instruction[0] is ConstructorInvokationStatementDefinition)
                {
                    var cisd = this.Instruction[0] as ConstructorInvokationStatementDefinition;

                    String tVariableName = String.Format("d{0:n}", Guid.NewGuid());
                    cisd.VariableName = tVariableName;
                    retVal.AddRange(cisd.ToCodeDomStatement(context));
                    value = new CodeVariableReferenceExpression(tVariableName);
                }
                else if (this.Instruction[0] is MethodInvokationStatementDefinition)
                {
                    var cisd = this.Instruction[0] as MethodInvokationStatementDefinition;
                    String tVariableName = String.Format("d{0:n}", Guid.NewGuid());
                    cisd.VariableName = tVariableName;
                    retVal.AddRange(cisd.ToCodeDomStatement(context));
                    value = new CodeVariableReferenceExpression(tVariableName);
                }

            }

            if(declRequired && this.VariableName != null)
                retVal.Add(new CodeVariableDeclarationStatement(new CodeTypeReference("var"), this.VariableName, value)); 
            else
                retVal.Add(new CodeAssignStatement(scope, value));
            return retVal;
            
        }
    }
}
