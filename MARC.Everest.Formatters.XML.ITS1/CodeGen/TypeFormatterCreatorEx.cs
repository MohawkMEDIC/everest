/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * User: Justin Fyfe
 * Date: 02-19-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.CodeDom;
using System.Xml;
using System.Reflection;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using MARC.Everest.Attributes;
using System.Collections;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Formatters.XML.ITS1.CodeGen
{
    /// <summary>
    /// Delegate for the create type formatter
    /// </summary>
    public delegate void CreateTypeFormatterCompletedDelegate(CodeTypeDeclaration declaration);

    /// <summary>
    /// Represents a utility class that can create a code dom formatter
    /// </summary>
    public class TypeFormatterCreatorEx
    {

        /// <summary>
        /// Build property context info
        /// </summary>
        private struct BuildProperty
        {
            public PropertyInfo PropertyInfo { get; set; }
            public IEnumerable<PropertyAttribute> PropertyAttributes { get; set; }
        }

        // Constants for CodeDom
        private static readonly CodePrimitiveExpression s_true = new CodePrimitiveExpression(true);
        private static readonly CodePrimitiveExpression s_false = new CodePrimitiveExpression(false);
        private static readonly CodePrimitiveExpression s_null = new CodePrimitiveExpression(null);
        private static readonly CodeThisReferenceExpression s_this = new CodeThisReferenceExpression();
        private static readonly CodeFieldReferenceExpression s_resultDetailError = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ResultDetailType)), "Error");
        private static readonly CodeFieldReferenceExpression s_resultDetailWarning = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ResultDetailType)), "Warning");
        private static readonly CodeFieldReferenceExpression s_resultDetailInformation = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ResultDetailType)), "Information");

        private static readonly CodeVariableReferenceExpression s_reader = new CodeVariableReferenceExpression("r");
        private static readonly CodePropertyReferenceExpression s_readerLocalName = new CodePropertyReferenceExpression(s_reader, "LocalName");
        private static readonly CodePropertyReferenceExpression s_readerNamespace = new CodePropertyReferenceExpression(s_reader, "NamespaceURI");
        private static readonly CodePropertyReferenceExpression s_readerValue = new CodePropertyReferenceExpression(s_reader, "Value");
        private static readonly CodePropertyReferenceExpression s_readerPrefix = new CodePropertyReferenceExpression(s_reader, "Prefix");
        private static readonly CodeMethodInvokeExpression s_readerToString = new CodeMethodInvokeExpression(s_reader, "ToString");

        private static readonly CodeVariableReferenceExpression s_writer = new CodeVariableReferenceExpression("s");
        private static readonly CodeMethodInvokeExpression s_writerToString = new CodeMethodInvokeExpression(s_writer, "ToString");
        
        private static readonly CodeVariableReferenceExpression s_useType = new CodeVariableReferenceExpression("useType");
        private static readonly CodeVariableReferenceExpression s_interactionType = new CodeVariableReferenceExpression("interactionType");

        private static readonly CodeVariableReferenceExpression s_resultContext = new CodeVariableReferenceExpression("resultContext");
        private static readonly CodeMethodReferenceExpression s_resultContextAddResultDetailMethod = new CodeMethodReferenceExpression(s_resultContext, "AddResultDetail");

        private static readonly CodePropertyReferenceExpression s_host = new CodePropertyReferenceExpression(s_this, "Host");
        private static readonly CodePropertyReferenceExpression s_hostSettings = new CodePropertyReferenceExpression(s_host, "Settings");

        private static readonly CodeVariableReferenceExpression s_instance = new CodeVariableReferenceExpression("instance");
        private static readonly CodePropertyReferenceExpression s_instanceNullFlavor = new CodePropertyReferenceExpression(s_instance, "NullFlavor");

        private static readonly CodeVariableReferenceExpression s_retVal = new CodeVariableReferenceExpression("retVal");

        private static readonly CodeTypeReference s_iGraphable = new CodeTypeReference(typeof(IGraphable));
        
        /// <summary>
        /// Gets or sets the reset event which signals the end of creation
        /// </summary>
        public ManualResetEvent ResetEvent { get; set; }

        /// <summary>
        /// Code type declaration has been completed
        /// </summary>
        public event CreateTypeFormatterCompletedDelegate CodeTypeDeclarationCompleted;

        #region Helper Methods

        /// <summary>
        /// Returns true if the property is traversable
        /// </summary>
        private bool IsTraversable(PropertyInfo pi)
        {
            object[] propertyAttributes = pi.GetCustomAttributes(typeof(PropertyAttribute), false);
            if (propertyAttributes.Length > 0)
                return (propertyAttributes[0] as PropertyAttribute).PropertyType == PropertyAttribute.AttributeAttributeType.Traversable;
            else
                return false;
        }

        /// <summary>
        /// Returns true if the property is traversable
        /// </summary>
        private bool IsNonStructural(PropertyInfo pi)
        {
            object[] propertyAttributes = pi.GetCustomAttributes(typeof(PropertyAttribute), false);
            if (propertyAttributes.Length > 0)
                return (propertyAttributes[0] as PropertyAttribute).PropertyType == PropertyAttribute.AttributeAttributeType.NonStructural;
            else
                return false;
        }

        /// <summary>
        /// Creates a code method invokation expression
        /// </summary>
        private CodeExpression CreateUtilConvertMethodInvoke(Type destinationType, CodeExpression parm)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Util)), "Convert", new CodeTypeReference(destinationType)), parm);
        }

        /// <summary>
        /// Get build properties
        /// </summary>
        private List<BuildProperty> GetBuildProperties(Type instanceType)
        {
            List<PropertyInfo> buildProperties = new List<PropertyInfo>(10);
            Type cType = instanceType;
            int nonTrav = 0, nonStruct = 0;
            while (cType != typeof(System.Object))
            {
                PropertyInfo[] typeTypes = cType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                typeTypes = Array.FindAll(typeTypes, o => o.GetCustomAttributes(typeof(PropertyAttribute), false).Length > 0);
                PropertyInfo[] nonTraversable = Array.FindAll(typeTypes, o => !IsTraversable(o) && !IsNonStructural(o) && !buildProperties.Exists(f => f.Name == o.Name)),
                    traversable = Array.FindAll(typeTypes, o => IsTraversable(o) && !buildProperties.Exists(f => f.Name == o.Name)),
                    nonStructural = Array.FindAll(typeTypes, o => IsNonStructural(o) && !buildProperties.Exists(f => f.Name == o.Name));

                //Array.Sort<PropertyInfo>(traversable, (a, b) => (a.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute).SortKey.CompareTo((b.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute).SortKey));
                nonTrav += nonTraversable.Length + nonStructural.Length;
                nonStruct += nonTraversable.Length;
                // NonTraversable properties need to be serialized first
                buildProperties.InsertRange(0, nonTraversable);
                buildProperties.InsertRange(nonStruct, nonStructural);
                buildProperties.InsertRange(nonTrav, traversable);
                cType = cType.BaseType;
            }

            List<BuildProperty> retVal = new List<BuildProperty>();
            foreach (var pi in buildProperties)
                retVal.Add(new BuildProperty()
                {
                    PropertyInfo = pi,
                    PropertyAttributes = new List<PropertyAttribute>(pi.GetCustomAttributes(typeof(PropertyAttribute), false).OfType<PropertyAttribute>())
                });
            return retVal;
        }

        /// <summary>
        /// Get build properties
        /// </summary>
        //private List<BuildProperty> GetBuildProperties(Type instanceType)
        //{
        //    List<BuildProperty> buildProperties = new List<BuildProperty>(10);
        //    Type cType = instanceType;
        //    int nonTrav = 0, nonStruct = 0;
        //    while (cType != typeof(System.Object))
        //    {
        //        PropertyInfo[] typeTypes = cType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        //        var propertiesWithPa = Array.FindAll(typeTypes, o => o.GetCustomAttributes(typeof(PropertyAttribute), false).Length > 0);
        //        Array.Sort(propertiesWithPa, (b,a)=>(a.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute).SortKey.CompareTo((b.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute).SortKey));
        //        foreach(var o in propertiesWithPa)
        //        {
        //            if(buildProperties.Exists(p=>p.PropertyInfo.Name == o.Name))
        //                continue;

        //            BuildProperty bp = new BuildProperty() { PropertyInfo = o, PropertyAttributes = o.GetCustomAttributes(typeof(PropertyAttribute), false).OfType<PropertyAttribute>() };
        //            //buildProperties.Add(bp);
        //            if (!IsTraversable(o))
        //            {
        //                if (!IsNonStructural(o))
        //                {
        //                    buildProperties.Insert(0, bp);
        //                    nonStruct++;
        //                }
        //                else
        //                {
        //                    buildProperties.Insert(nonStruct, bp);
        //                    nonTrav++;
        //                }
        //            }
        //            else
        //                buildProperties.Insert(nonTrav + nonStruct, bp);

        //        }
        //        cType = cType.BaseType;
        //    }

        //    return buildProperties;
        //}

        /// <summary>
        /// Create a instance cast expression
        /// </summary>
        private CodeStatement CreateCastTryCatch(Type toType, CodeExpression targetObject, CodeExpression sourceObject, CodeExpression returnObject, CodeExpression locationReference, CodeMethodReferenceExpression resultDetails)
        {
            return new CodeTryCatchFinallyStatement(
                new CodeStatement[] {
                    new CodeAssignStatement(targetObject, new CodeCastExpression(new CodeTypeReference(toType), sourceObject)),
                },
                new CodeCatchClause[] {

                new CodeCatchClause("e", new CodeTypeReference(typeof(Exception)), 
                    new CodeExpressionStatement(new CodeMethodInvokeExpression(resultDetails,  this.CreateResultDetailExpression(typeof(ResultDetail), s_resultDetailError, new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("e"), "Message"), locationReference, new CodeVariableReferenceExpression("e")))),
                    new CodeMethodReturnStatement(returnObject))
                    
                });
            
        }

        /// <summary>
        /// Creates a String.Format() expression
        /// </summary>
        private CodeExpression CreateStringFormatExpression(string formatString, params CodeExpression[] parms)
        {
            var retVal = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(String)), "Format"), new CodePrimitiveExpression(formatString));
            foreach (var p in parms)
                retVal.Parameters.Add(p);
            return retVal;
        }

        /// <summary>
        /// Create a simaple method call expression
        /// </summary>
        private CodeExpression CreateSimpleMethodCallExpression(CodeExpression target, String methodName, params Object[] simpleParameters)
        {
            var retVal = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(target, methodName));
            foreach (var p in simpleParameters)
                retVal.Parameters.Add(new CodePrimitiveExpression(p));
            return retVal;
        }

        /// <summary>
        /// Create a add result detail expression
        /// </summary>
        private CodeExpression CreateResultDetailExpression(Type resultDetailType, params CodeExpression[] parameters)
        {
            return new CodeObjectCreateExpression(resultDetailType, parameters);
        }

        /// <summary>
        /// Create ConvertExpression
        /// </summary>
        private CodeExpression CreateConvertExpression(String methodName, CodeExpression parameter)
        {
            return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(Convert)), methodName, parameter);
        }
        #endregion

        /// <summary>
        /// Create the type formatter declaration
        /// </summary>
        public void CreateTypeFormatter(object state)
        {
            CodeTypeDeclaration ctdecl = null;

            try
            {
                Type forType = (Type)state;

                //if (forType.Name == "RegistrationRequest")
                //    System.Diagnostics.Debugger.Break();

                // Don't generate for generic type definitions
                if (forType.IsGenericType && forType.ContainsGenericParameters || forType.IsGenericTypeDefinition || forType.IsAbstract)
                    return;

                // Generate the type definition
                ctdecl = new CodeTypeDeclaration(String.Format("d{0:N}", Guid.NewGuid()));
                ctdecl.IsClass = true;
                ctdecl.TypeAttributes = System.Reflection.TypeAttributes.Public;
                ctdecl.BaseTypes.Add(new CodeTypeReference(typeof(ITypeFormatter)));

                // Add the host reference field
                ctdecl.Members.Add(new CodeMemberField()
                {
                    Name = "m_host",
                    Attributes = MemberAttributes.Private,
                    Type = new CodeTypeReference(typeof(XmlIts1Formatter))
                });

                ctdecl.Members.Add(this.CreateHostProperty()); // Host property
                ctdecl.Members.Add(this.CreateHandlesTypeProperty(forType));

                var buildProperties = this.GetBuildProperties(forType);

                // Generate the code based on properties
                //this.GeneratePropertyPopulationCode(forType, parseAttributeStatements, parseElementStatements, graphStatements, validateStatements);
                // Create the graph/validate/parse/parseElement method signatures
                ctdecl.Members.Add(this.CreateParseMethod(forType, buildProperties));
                ctdecl.Members.Add(this.CreateGraphMethod(forType, buildProperties));
                ctdecl.Members.Add(this.CreateParseElementsMethod(forType, buildProperties));
                ctdecl.Members.Add(this.CreateValidateMethod(forType, buildProperties));

            }
            finally
            {
                if (this.CodeTypeDeclarationCompleted != null)
                    this.CodeTypeDeclarationCompleted(ctdecl);
            }
        }



        /// <summary>
        /// Create the validation method
        /// </summary>
        private CodeTypeMember CreateValidateMethod(Type forType, List<BuildProperty> buildProperties)
        {
            var retVal = new CodeMemberMethod()
            {
                Name = "Validate",
                ReturnType = new CodeTypeReference(typeof(IEnumerable<IResultDetail>)),
                Attributes = MemberAttributes.Public
            };
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(s_iGraphable, "o"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(String)), "location"));

            var _location = new CodeVariableReferenceExpression("location");

            // Cast the object to a strong typed instance
            retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(forType), "instance", s_null));
            // Now construct a return value
            retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(List<IResultDetail>)), "retVal", new CodeObjectCreateExpression(typeof(List<IResultDetail>))));

            retVal.Statements.Add(this.CreateCastTryCatch(forType, s_instance, new CodeVariableReferenceExpression("o"), s_retVal, new CodeVariableReferenceExpression("location"), new CodeMethodReferenceExpression(s_retVal, "Add")));


            // Check conditions for null and/or null flavor
            CodeBinaryOperatorExpression nullCheck = new CodeBinaryOperatorExpression(s_instance, CodeBinaryOperatorType.IdentityEquality, s_null),
                nullFlavorCheck = new CodeBinaryOperatorExpression(s_instanceNullFlavor, CodeBinaryOperatorType.IdentityInequality, s_null);

            // Sanity checks
            retVal.Statements.Add(new CodeConditionStatement(nullCheck, new CodeMethodReturnStatement(s_retVal)));
            if (forType.GetProperty("NullFlavor") != null) // Ooops... Only emit nullFlavor check if the type has the NullFlavor property
                retVal.Statements.Add(new CodeConditionStatement(nullFlavorCheck, new CodeMethodReturnStatement(s_retVal)));

            // Formal constraints on the type
            foreach (FormalConstraintAttribute fca in forType.GetCustomAttributes(typeof(FormalConstraintAttribute), true))
                if(forType.GetMethod(fca.CheckConstraintMethod, new Type[] { forType }) != null)
                    retVal.Statements.Add(new CodeConditionStatement(
                        new CodeBinaryOperatorExpression(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(forType), fca.CheckConstraintMethod, s_instance), CodeBinaryOperatorType.IdentityInequality, s_true),
                        new CodeExpressionStatement(new CodeMethodInvokeExpression(s_retVal, "Add", this.CreateResultDetailExpression(typeof(FormalConstraintViolationResultDetail), s_resultDetailError, new CodePrimitiveExpression(fca.Description), _location, new CodePrimitiveExpression(null))))));

            // Add the validation statements
            //retVal.Statements.AddRange(validateStatements);
            // Determine if the instance has a null flavor
            // Now loop through properties and add validation statements
            foreach (var bp in buildProperties)
            {
                PropertyAttribute pa = bp.PropertyAttributes.First();
                var cpre = new CodePropertyReferenceExpression(s_instance, bp.PropertyInfo.Name);

                if (pa.Conformance == PropertyAttribute.AttributeConformanceType.Mandatory && bp.PropertyInfo.PropertyType.GetProperty("NullFlavor") != null)
                    retVal.Statements.Add(
                        new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                new CodeBinaryOperatorExpression(cpre, CodeBinaryOperatorType.IdentityEquality, s_null),
                                CodeBinaryOperatorType.BooleanOr,
                                new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(cpre, "NullFlavor"), CodeBinaryOperatorType.IdentityInequality, s_null)
                            ),
                            new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(s_retVal, "Add"), this.CreateResultDetailExpression(typeof(MandatoryElementMissingResultDetail), s_resultDetailError, new CodePrimitiveExpression(String.Format("Property {0} in {1} is marked mandatory and is either not assigned, or is assigned a null flavor. This is not permitted", bp.PropertyInfo.Name, forType.FullName)), _location)))
                        )
                    );
                else if(pa.Conformance == PropertyAttribute.AttributeConformanceType.Populated)
                    retVal.Statements.Add(
                        new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(cpre, CodeBinaryOperatorType.IdentityEquality, s_null),
                            new CodeVariableDeclarationStatement(typeof(ResultDetailType), "level", s_resultDetailError),
                            new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(s_host, "CreateRequiredElements"), CodeBinaryOperatorType.IdentityEquality, s_true),
                                new CodeAssignStatement(new CodeVariableReferenceExpression("level"), s_resultDetailWarning)
                            ),
                            new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(s_retVal, "Add"), this.CreateResultDetailExpression(typeof(RequiredElementMissingResultDetail), new CodeVariableReferenceExpression("level"), new CodePrimitiveExpression(String.Format("Property {0} in {1} is marked 'populated' and is not assigned (you must at minimum, assign a NullFlavor for this attribute)", bp.PropertyInfo.Name, forType.FullName)), _location)))
                        )
                    );
                if(pa.MinOccurs != 0 || (pa.MaxOccurs != -1 && pa.MaxOccurs != 1) && bp.PropertyInfo.PropertyType.GetInterface(typeof(IList<>).FullName) != null)
                {
                    CodePropertyReferenceExpression count = new CodePropertyReferenceExpression(cpre, "Count");
                    int maxOccurs = pa.MaxOccurs < 0 ? Int32.MaxValue : pa.MaxOccurs;
                    retVal.Statements.Add(
                        new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                new CodeBinaryOperatorExpression(cpre, CodeBinaryOperatorType.IdentityInequality, s_null), 
                                CodeBinaryOperatorType.BooleanAnd,
                                new CodeBinaryOperatorExpression(
                                    new CodeBinaryOperatorExpression(count, CodeBinaryOperatorType.GreaterThan, new CodePrimitiveExpression(maxOccurs)), 
                                    CodeBinaryOperatorType.BooleanOr,
                                    new CodeBinaryOperatorExpression(count, CodeBinaryOperatorType.LessThan, new CodePrimitiveExpression(pa.MinOccurs))
                                )
                            ),
                            new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(s_retVal, "Add"), this.CreateResultDetailExpression(typeof(InsufficientRepetitionsResultDetail), s_resultDetailError, new CodePrimitiveExpression(cpre.PropertyName), new CodePrimitiveExpression(pa.MinOccurs), new CodePrimitiveExpression(maxOccurs), count, _location)))
                        )
                    );
                }


                // Formal constraints on the type
                foreach (FormalConstraintAttribute fca in bp.PropertyInfo.GetCustomAttributes(typeof(FormalConstraintAttribute), false))
                    if(forType.GetMethod(fca.CheckConstraintMethod, new Type[] { bp.PropertyInfo.PropertyType }) != null)
                        retVal.Statements.Add(new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(forType), fca.CheckConstraintMethod, cpre), CodeBinaryOperatorType.IdentityInequality, s_true),
                            new CodeExpressionStatement(new CodeMethodInvokeExpression(s_retVal, "Add", this.CreateResultDetailExpression(typeof(FormalConstraintViolationResultDetail), s_resultDetailError, new CodePrimitiveExpression(fca.Description), _location, new CodePrimitiveExpression(null))))));

            }
            // Return the result array
            retVal.Statements.Add(new CodeMethodReturnStatement(s_retVal));

            return retVal;
        }

        // Determine if something is null
        private bool IsStatementNull(CodeObject obj)
        {
            if(obj == null)
                return true;

            bool hasNull = false;

            if (obj is CodeConditionStatement)
            {
                hasNull |= IsStatementNull((obj as CodeConditionStatement).Condition);
                if((obj as CodeConditionStatement).TrueStatements != null)
                    foreach (CodeObject stmt in (obj as CodeConditionStatement).TrueStatements)
                        hasNull |= IsStatementNull(stmt);
                if((obj as CodeConditionStatement).FalseStatements != null)
                    foreach (CodeObject stmt in (obj as CodeConditionStatement).FalseStatements)
                        hasNull |= IsStatementNull(stmt);
            }
            else if (obj is CodeIterationStatement)
            {
                hasNull |= IsStatementNull((obj as CodeIterationStatement).TestExpression);
                foreach (CodeObject stmt in (obj as CodeIterationStatement).Statements)
                    hasNull |= IsStatementNull(stmt);
            }
            else if (obj is CodeBinaryOperatorExpression)
            {
                hasNull |= IsStatementNull((obj as CodeBinaryOperatorExpression).Right);
                hasNull |= IsStatementNull((obj as CodeBinaryOperatorExpression).Left);
            }
            else if (obj is CodeTryCatchFinallyStatement)
            {
                foreach(CodeObject stmt in (obj as CodeTryCatchFinallyStatement).TryStatements)
                    hasNull |= IsStatementNull(stmt);
            }
            else if (obj is CodeMemberMethod)
                foreach (CodeObject stmt in (obj as CodeMemberMethod).Statements)
                    hasNull |= IsStatementNull(stmt); 
            return hasNull;
        }

        /// <summary>
        /// Create the parse elements method
        /// </summary>
        private CodeTypeMember CreateParseElementsMethod(Type forType, List<BuildProperty> buildProperties)
        {
            var retVal = new CodeMemberMethod() {
                Name = "ParseElementContent",
                ReturnType = new CodeTypeReference(typeof(Object)),
                Attributes = MemberAttributes.Public
            };
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(XmlReader)), "r"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Object)), "o"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(String)), "terminationElement"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Int32)), "terminationDepth"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Type)), "interactionType"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(XmlIts1FormatterParseResult)), "resultContext"));

            // Cast the object to a strong typed instance
            retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(forType), "instance", s_null));
            retVal.Statements.Add(this.CreateCastTryCatch(forType, s_instance, new CodeVariableReferenceExpression("o"), new CodeVariableReferenceExpression("o"), s_readerToString, s_resultContextAddResultDetailMethod));


            // define the last element read
            retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(String)), "lastElementRead", s_readerLocalName));
            var _lastElementRead = new CodeVariableReferenceExpression("lastElementRead");

            // Conditions commonly used in this method
            CodeBinaryOperatorExpression terminationIsLastElementCondition = new CodeBinaryOperatorExpression(_lastElementRead, CodeBinaryOperatorType.IdentityEquality, new CodeVariableReferenceExpression("terminationElement")),
                elementNameIsTerminationCondition = new CodeBinaryOperatorExpression(s_readerLocalName, CodeBinaryOperatorType.IdentityEquality, new CodeVariableReferenceExpression("terminationElement")),
                elementDepthIsTerminationCondition = new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(s_reader, "Depth"), CodeBinaryOperatorType.IdentityEquality, new CodeVariableReferenceExpression("terminationDepth")),
                nodeTypeElementCondition = new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(s_reader, "NodeType"), CodeBinaryOperatorType.IdentityEquality, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(XmlNodeType)), "Element")),
                nodeTypeEndElementCondition = new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(s_reader, "NodeType"), CodeBinaryOperatorType.IdentityEquality, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(XmlNodeType)), "EndElement")),
                externalSourceCondition = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(terminationIsLastElementCondition, CodeBinaryOperatorType.BooleanAnd, nodeTypeElementCondition), CodeBinaryOperatorType.IdentityInequality, s_true),
                notReaderReadCondition = new CodeBinaryOperatorExpression(this.CreateSimpleMethodCallExpression(s_reader, "Read"), CodeBinaryOperatorType.IdentityInequality, s_true),
                localNameEqualsLastCondition = new CodeBinaryOperatorExpression(s_readerLocalName, CodeBinaryOperatorType.IdentityEquality, _lastElementRead);

            // From an external source?
            retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(Boolean)), "fromExternalSource", externalSourceCondition));

            // Parsing helpers
            var elementReadLoop = new CodeIterationStatement(new CodeSnippetStatement(), new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(s_reader, "EOF"), CodeBinaryOperatorType.IdentityInequality, s_true), new CodeSnippetStatement());
            // Don't read first element if from external source!
            elementReadLoop.Statements.Add(new CodeConditionStatement(new CodeVariableReferenceExpression("fromExternalSource"), 
                new CodeStatement[] { new CodeAssignStatement(new CodeVariableReferenceExpression("fromExternalSource"), s_false) },
                new CodeStatement[] { 
                    new CodeConditionStatement(new CodeBinaryOperatorExpression(localNameEqualsLastCondition, CodeBinaryOperatorType.BooleanAnd, notReaderReadCondition), 
                        new CodeSnippetStatement("break;")
                    )
                }
            ));

            // Assign last element read to this element
            elementReadLoop.Statements.Add(new CodeAssignStatement(_lastElementRead, s_readerLocalName));

            // Condition contains all the steps to read elements
            var elementParseCondition = new CodeConditionStatement(nodeTypeElementCondition);
            //elementParseCondition.TrueStatements.AddRange(parseElementStatements);
            CodeConditionStatement currentStatement = new CodeConditionStatement();
            elementParseCondition.TrueStatements.Add(currentStatement);

            // Ensure we don't duplicate properties
            List<String> generatedProperties = new List<string>(buildProperties.Count());
            foreach (var bp in buildProperties)
            {
                // Sanity check : Don't generate the same property info twice
                if (generatedProperties.Contains(bp.PropertyInfo.Name))
                    continue;
                generatedProperties.Add(bp.PropertyInfo.Name);

                var propertyAttributes = bp.PropertyAttributes.ToArray();
                var _propertyReference = new CodePropertyReferenceExpression(s_instance, bp.PropertyInfo.Name);

                foreach (var pa in propertyAttributes)
                {

                    // Ignore structural attributes
                    if (pa.PropertyType == PropertyAttribute.AttributeAttributeType.Structural)
                        continue;

                    // Are we interested in this
                    // 1. The property is a generic parameter that matches the argument supplied to the type
                    // This means that we don't want any collisions
                    if (!(forType.IsGenericType && ((bp.PropertyInfo.PropertyType == forType.GetGenericArguments()[0] && pa.Type == bp.PropertyInfo.PropertyType) ||
                        (pa.Type == null && pa.Type != forType.GetGenericArguments()[0])) ||
                        (bp.PropertyInfo.PropertyType == typeof(object)) || bp.PropertyAttributes.Count() == 1 || pa.Type == null || pa.Type != null && pa.Type.IsSubclassOf(bp.PropertyInfo.PropertyType) && bp.PropertyInfo.PropertyType.IsAssignableFrom(pa.Type)))
                        continue;

                    // Build the current statment condition
                    var condition = new CodeBinaryOperatorExpression(s_readerLocalName, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(pa.Name));
                    condition = new CodeBinaryOperatorExpression(condition, CodeBinaryOperatorType.BooleanAnd,
                        new CodeBinaryOperatorExpression(s_readerNamespace, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(pa.NamespaceUri)));

                    // Set current statement 
                    if (currentStatement.TrueStatements.Count > 0)
                    {
                        var falseStatement = new CodeConditionStatement();
                        currentStatement.FalseStatements.Add(falseStatement);
                        currentStatement = falseStatement;
                    }

                    // Set the condition and parse if needed
                    currentStatement.Condition = condition;

                    // SET method
                    // Cannot deserialize this
                    if (pa.Type == null && bp.PropertyInfo.PropertyType == typeof(System.Object))
                        // Add not implemented element if we can't write
                        currentStatement.TrueStatements.Add(new CodeMethodInvokeExpression(s_resultContextAddResultDetailMethod, this.CreateResultDetailExpression(typeof(NotImplementedElementResultDetail), s_resultDetailWarning, s_readerLocalName, s_readerLocalName, s_readerToString, s_null)));
                    // Simple deserialization if PA type has IGraphable or PI type has IGraphable and PA type not specified
                    else if (bp.PropertyInfo.GetSetMethod() != null &&
                        (pa.Type != null && pa.Type.GetInterface(typeof(IGraphable).FullName, false) != null) ||
                        (pa.Type == null && bp.PropertyInfo.PropertyType.GetInterface(typeof(IGraphable).FullName, false) != null))
                        currentStatement.TrueStatements.Add(new CodeAssignStatement(
                            _propertyReference,
                            this.CreateUtilConvertMethodInvoke(pa.Type ?? bp.PropertyInfo.PropertyType,
                                new CodeMethodInvokeExpression(s_host, "ParseObject", s_reader, new CodeTypeOfExpression(pa.Type ?? bp.PropertyInfo.PropertyType), s_interactionType, s_resultContext))));
                    // Call an Add method on a collection type
                    else if (bp.PropertyInfo.PropertyType.GetMethod("Add") != null) // Collection type
                        currentStatement.TrueStatements.Add(new CodeMethodInvokeExpression(_propertyReference, "Add", this.CreateUtilConvertMethodInvoke(bp.PropertyInfo.PropertyType.GetGenericArguments()[0], 
                                new CodeMethodInvokeExpression(s_host, "ParseObject", s_reader, new CodeTypeOfExpression(bp.PropertyInfo.PropertyType.GetGenericArguments()[0]), s_interactionType, s_resultContext))));
                    // Call the ParseXML custom function on object
                    else if (bp.PropertyInfo.GetSetMethod() != null && bp.PropertyInfo.PropertyType.GetMethod("ParseXml", BindingFlags.Public | BindingFlags.Static) != null)
                        currentStatement.TrueStatements.Add(new CodeAssignStatement(_propertyReference, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(bp.PropertyInfo.PropertyType), "ParseXml", s_reader)));
                    // Property type is a simple string
                    else if (bp.PropertyInfo.GetSetMethod() != null && bp.PropertyInfo.PropertyType == typeof(string)) // Read content... 
                        currentStatement.TrueStatements.Add(new CodeAssignStatement(_propertyReference, new CodeMethodInvokeExpression(s_reader, "ReaderInnerXml")));
                    // No Set method is used, fixed value?
                    else
                        currentStatement.TrueStatements.Add(new CodeMethodInvokeExpression(s_resultContextAddResultDetailMethod, this.CreateResultDetailExpression(typeof(NotImplementedResultDetail), s_resultDetailError, new CodePrimitiveExpression(String.Format("Property {0} is readonly, cannot set value", bp.PropertyInfo.Name)), s_readerToString)));

                    // Need to switch or re-evaluate our instance?
                    var ma = bp.PropertyInfo.GetCustomAttributes(typeof(MarkerAttribute), true);
                    if (ma.Length > 0)
                        switch ((ma[0] as MarkerAttribute).MarkerType)
                        {
                            case MarkerAttribute.MarkerAttributeType.TemplateId:
                            case MarkerAttribute.MarkerAttributeType.TypeId:
                                currentStatement.TrueStatements.Add(new CodeVariableDeclarationStatement(typeof(Object), "newInstance", new CodeMethodInvokeExpression(s_host, "CorrectInstance", s_instance)));
                                currentStatement.TrueStatements.Add(new CodeConditionStatement(new CodeBinaryOperatorExpression(this.CreateSimpleMethodCallExpression(new CodeVariableReferenceExpression("newInstance"), "GetType"), CodeBinaryOperatorType.IdentityInequality, this.CreateSimpleMethodCallExpression(s_instance, "GetType")),
                                    new CodeMethodReturnStatement(new CodeMethodInvokeExpression(s_host, "ParseElementContent", s_reader, new CodeVariableReferenceExpression("newInstance"), new CodeVariableReferenceExpression("terminationElement"), new CodePropertyReferenceExpression(s_reader, "Depth"), s_interactionType, s_resultContext))));
                                break;
                            default:
                                break;
                        }

                }
            }

            // Break out or continue condition
            elementReadLoop.Statements.Add(new CodeConditionStatement(
                new CodeBinaryOperatorExpression(
                        new CodeBinaryOperatorExpression(nodeTypeEndElementCondition, CodeBinaryOperatorType.BooleanAnd, elementNameIsTerminationCondition),
                            CodeBinaryOperatorType.BooleanAnd, elementDepthIsTerminationCondition),
                    new CodeStatement[] { // true statements
                        new CodeSnippetStatement("break;")
                    },
                    new CodeStatement[] { // false statements
                        elementParseCondition // Parse the elements from XmlReader stream
                    }
                ));

            retVal.Statements.Add(elementReadLoop);

            if (IsStatementNull(retVal))
                System.Diagnostics.Debugger.Break();

            retVal.Statements.Add(new CodeMethodReturnStatement(s_instance));
            return retVal;
        }


        /// <summary>
        /// Create the graph method for the formatter
        /// </summary>
        private CodeTypeMember CreateGraphMethod(Type forType, List<BuildProperty> buildProperties)
        {
            CodeMemberMethod retVal = new CodeMemberMethod()
            {
                Name = "Graph",
                Attributes = MemberAttributes.Public,
                ReturnType = new CodeTypeReference(typeof(void))
            };
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(XmlWriter)), "s"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Object)), "o"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(s_iGraphable, "context"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(XmlIts1FormatterGraphResult)), "resultContext"));

            // Cast o to a strong instance type
            retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(forType), "instance", s_null));
            retVal.Statements.Add(this.CreateCastTryCatch(forType, s_instance, new CodeVariableReferenceExpression("o"), null, s_writerToString, s_resultContextAddResultDetailMethod));

            var structureAttributes = forType.GetCustomAttributes(typeof(StructureAttribute), false);
            if (structureAttributes.Length != 0)
            {
                var structureAttribute = structureAttributes[0] as StructureAttribute;
                // Type is an interaction so output the root and the XML ITS Version
                if (structureAttribute.StructureType == StructureAttribute.StructureAttributeType.Interaction)
                {
                    retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(bool)), "isEntryPoint", s_true));
                    retVal.Statements.Add(this.CreateSimpleMethodCallExpression(s_writer, "WriteStartElement", structureAttribute.Name, structureAttribute.NamespaceUri));
                    retVal.Statements.Add(this.CreateSimpleMethodCallExpression(s_writer, "WriteAttributeString", "ITSVersion", "XML_1.0"));
                }
                else if (structureAttribute.IsEntryPoint)
                {
                    CodeBinaryOperatorExpression entryPointDetectionCondition = new CodeBinaryOperatorExpression(
                        new CodeBinaryOperatorExpression(new CodeSnippetExpression("s is MARC.Everest.Xml.XmlStateWriter"), CodeBinaryOperatorType.BooleanAnd, new CodeSnippetExpression("(s as MARC.Everest.Xml.XmlStateWriter).ElementStack.Count == 0")),
                        CodeBinaryOperatorType.BooleanOr,
                        new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(s_writer, "WriteState"), CodeBinaryOperatorType.IdentityEquality, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(WriteState)), "Start")));

                    retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(bool)), "isEntryPoint", entryPointDetectionCondition));
                    retVal.Statements.Add(new CodeConditionStatement(
                        new CodeVariableReferenceExpression("isEntryPoint"),
                        new CodeExpressionStatement(this.CreateSimpleMethodCallExpression(s_writer, "WriteStartElement", structureAttribute.Name, structureAttribute.NamespaceUri))
                    ));
                }
                else
                    retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(bool)), "isEntryPoint", s_false));
            }
            else
                retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(bool)), "isEntryPoint", s_false));

            // If we're at an entry point emit XSI: namespace
            retVal.Statements.Add(new CodeConditionStatement(
                new CodeVariableReferenceExpression("isEntryPoint"),
                new CodeExpressionStatement(this.CreateSimpleMethodCallExpression(s_writer, "WriteAttributeString", "xmlns", "xsi", null, XmlIts1Formatter.NS_XSI))
            ));

            CodeExpression isInstanceNullCondition = new CodeBinaryOperatorExpression(s_instanceNullFlavor, CodeBinaryOperatorType.IdentityInequality, s_null),
               notIsInstanceNullCondition = new CodeBinaryOperatorExpression(s_instanceNullFlavor, CodeBinaryOperatorType.IdentityEquality, s_null),
               isSuppressNullCondition = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(s_hostSettings, CodeBinaryOperatorType.BitwiseAnd, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SettingsType)), "SuppressNullEnforcement")), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(0)),
               isSuppressXsiNilCondition = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(s_hostSettings, CodeBinaryOperatorType.BitwiseAnd, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SettingsType)), "SuppressXsiNil")), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(0)),
               isAlwaysCheckOverridesCondition = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(s_hostSettings, CodeBinaryOperatorType.BitwiseAnd, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SettingsType)), "AlwaysCheckForOverrides")), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(0)),
               isFlavorImposingCondition = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(s_hostSettings, CodeBinaryOperatorType.BitwiseAnd, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SettingsType)), "AllowFlavorImposing")), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(0)),
               isUpdateModeImposingCondition = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(s_hostSettings, CodeBinaryOperatorType.BitwiseAnd, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SettingsType)), "AllowUpdateModeImposing")), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(0)),
               isVocabularyImposingCondition = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(s_hostSettings, CodeBinaryOperatorType.BitwiseAnd, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SettingsType)), "AllowSupplierDomainImposing")), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(0));


            CodeMethodReferenceExpression _hostWriteElementUtil = new CodeMethodReferenceExpression(s_host, "WriteElementUtil"),
                _hostWriteNullFlavorUtil = new CodeMethodReferenceExpression(s_host, "WriteNullFlavorUtil");

            if (forType.GetProperty("NullFlavor") == null)
            {
                (isInstanceNullCondition as CodeBinaryOperatorExpression).Operator = CodeBinaryOperatorType.IdentityEquality;
                (isInstanceNullCondition as CodeBinaryOperatorExpression).Left = s_instance;
            }

            // Define control variables
            retVal.Statements.Add(new CodeVariableDeclarationStatement(typeof(bool), "isInstanceNull", isInstanceNullCondition));
            retVal.Statements.Add(new CodeVariableDeclarationStatement(typeof(bool), "suppressNull", isSuppressNullCondition));
            retVal.Statements.Add(new CodeVariableDeclarationStatement(typeof(bool), "suppressXsiNil", isSuppressXsiNilCondition));
            retVal.Statements.Add(new CodeVariableDeclarationStatement(typeof(bool), "alwaysCheckOverrides", isAlwaysCheckOverridesCondition));
            retVal.Statements.Add(new CodeVariableDeclarationStatement(typeof(bool), "flavorImposing", isFlavorImposingCondition));
            retVal.Statements.Add(new CodeVariableDeclarationStatement(typeof(bool), "updateModeImposing", isUpdateModeImposingCondition));
            retVal.Statements.Add(new CodeVariableDeclarationStatement(typeof(bool), "vocabularyImposing", isVocabularyImposingCondition));

            isInstanceNullCondition = new CodeVariableReferenceExpression("isInstanceNull");
            notIsInstanceNullCondition = new CodeBinaryOperatorExpression(isInstanceNullCondition, CodeBinaryOperatorType.IdentityInequality, s_true);
            isSuppressNullCondition = new CodeVariableReferenceExpression("suppressNull");
            isSuppressXsiNilCondition = new CodeVariableReferenceExpression("suppressXsiNil");
            isAlwaysCheckOverridesCondition = new CodeVariableReferenceExpression("alwaysCheckOverrides");
            isFlavorImposingCondition = new CodeVariableReferenceExpression("flavorImposing");
            isUpdateModeImposingCondition = new CodeVariableReferenceExpression("updateModeImposing");
            isVocabularyImposingCondition = new CodeVariableReferenceExpression("vocabularyImposing");

            // Shall we validate?
            retVal.Statements.Add(new CodeConditionStatement(new CodePropertyReferenceExpression(s_host, "ValidateConformance"),
                new CodeExpressionStatement(new CodeMethodInvokeExpression(s_host, "ValidateHelper", s_writer, s_instance, s_this, s_resultContext))));


            // Now emit the graph statements
            foreach (BuildProperty buildProperty in buildProperties)
            {

                var propertyInfo = buildProperty.PropertyInfo;
                Type piType = buildProperty.PropertyInfo.PropertyType;
                List<Type> piTypeInterfaces = new List<Type>(piType.GetInterfaces());
                var _propertyReference = new CodePropertyReferenceExpression(s_instance, propertyInfo.Name);

                    var propertyAttribute = buildProperty.PropertyAttributes.First();

                    // Create required elements is set to true therefore we should create required elemenets
                    if ((propertyAttribute.Conformance == PropertyAttribute.AttributeConformanceType.Required || propertyAttribute.Conformance == PropertyAttribute.AttributeConformanceType.Populated) &&
                        propertyAttribute.PropertyType != PropertyAttribute.AttributeAttributeType.Structural &&
                        piType.GetProperty("NullFlavor") != null &&
                        !piType.IsAbstract &&
                        propertyInfo.CanWrite)
                        retVal.Statements.Add(new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                new CodeBinaryOperatorExpression(_propertyReference, CodeBinaryOperatorType.IdentityEquality, s_null),
                                CodeBinaryOperatorType.BooleanAnd,
                                new CodePropertyReferenceExpression(s_host, "CreateRequiredElements")
                            ),
                            new CodeAssignStatement(_propertyReference, new CodeObjectCreateExpression(new CodeTypeReference(buildProperty.PropertyInfo.PropertyType))),
                            new CodeAssignStatement(new CodePropertyReferenceExpression(_propertyReference, "NullFlavor"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(NullFlavor)), "NoInformation"))
                        ));

                    // The condition of graphing
                    CodeBinaryOperatorExpression graphObjectContentsCondition = null;

                    // Is the instance's null flavor set?
                    if (propertyInfo.Name != "NullFlavor" && propertyAttribute.PropertyType == PropertyAttribute.AttributeAttributeType.Structural)
                        graphObjectContentsCondition = new CodeBinaryOperatorExpression(
                            new CodeBinaryOperatorExpression(_propertyReference, CodeBinaryOperatorType.IdentityInequality, s_null),
                            CodeBinaryOperatorType.BooleanAnd,
                            new CodeBinaryOperatorExpression(
                                new CodeBinaryOperatorExpression(isInstanceNullCondition, CodeBinaryOperatorType.BooleanAnd, isSuppressNullCondition),
                                CodeBinaryOperatorType.BooleanOr,
                                notIsInstanceNullCondition)
                            );
                    else if (propertyInfo.Name != "NullFlavor")
                        graphObjectContentsCondition = new CodeBinaryOperatorExpression(
                            new CodeBinaryOperatorExpression(_propertyReference, CodeBinaryOperatorType.IdentityInequality, s_null),
                            CodeBinaryOperatorType.BooleanAnd,
                            new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(
                                new CodeBinaryOperatorExpression(isInstanceNullCondition, CodeBinaryOperatorType.BooleanAnd, isSuppressNullCondition),
                                CodeBinaryOperatorType.BooleanAnd,
                                isSuppressXsiNilCondition),
                                CodeBinaryOperatorType.BooleanOr,
                                notIsInstanceNullCondition)
                                );
                    else
                    {
                        retVal.Statements.Add(new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(_propertyReference, CodeBinaryOperatorType.IdentityInequality, s_null),
                            new CodeExpressionStatement(new CodeMethodInvokeExpression(_hostWriteNullFlavorUtil, s_writer, _propertyReference))
                            ));
                    }


                    // If statement for graph
                    if (graphObjectContentsCondition != null)
                    {

                        var graphObjectContentsConditionStatement = new CodeConditionStatement(graphObjectContentsCondition);
                        retVal.Statements.Add(graphObjectContentsConditionStatement);

                        // A choice
                        List<CodeConditionStatement> graphConditions = new List<CodeConditionStatement>();
                        foreach (PropertyAttribute pa in buildProperty.PropertyAttributes)
                        {
                            // Create the condition in the appropriate manner
                            // Conditions for graphing the particular property choice
                            if (pa.Type != null && (pa.Type == propertyInfo.PropertyType || (pa.Type.IsSubclassOf(propertyInfo.PropertyType) && propertyInfo.PropertyType.IsAssignableFrom(pa.Type)) || propertyInfo.PropertyType == typeof(object)))
                            {
                                CodeBinaryOperatorExpression primaryCondition = new CodeBinaryOperatorExpression(new CodeMethodInvokeExpression(_propertyReference, "GetType"), CodeBinaryOperatorType.IdentityEquality, new CodeTypeOfExpression(pa.Type ?? propertyInfo.PropertyType)),
                                    secondaryCondition = new CodeBinaryOperatorExpression(isAlwaysCheckOverridesCondition, CodeBinaryOperatorType.BooleanAnd, new CodeMethodInvokeExpression(new CodeTypeOfExpression(pa.Type), "IsAssignableFrom", new CodeMethodInvokeExpression(_propertyReference, "GetType")));

                                // Is there a pa.InteractionOwner ? If so we need to check that as well
                                if (pa.InteractionOwner != null)
                                {
                                    var interactionOwnerCondition = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("context"), CodeBinaryOperatorType.IdentityInequality, s_null),
                                            CodeBinaryOperatorType.BooleanAnd,
                                            new CodeMethodInvokeExpression(new CodeTypeOfExpression(pa.InteractionOwner), "IsAssignableFrom", new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("context"), "GetType")));

                                    primaryCondition = new CodeBinaryOperatorExpression(primaryCondition, CodeBinaryOperatorType.BooleanAnd, interactionOwnerCondition);
                                    secondaryCondition = new CodeBinaryOperatorExpression(secondaryCondition, CodeBinaryOperatorType.BooleanAnd, interactionOwnerCondition);
                                }
                                CodeConditionStatement currentGraphCondition = new CodeConditionStatement(primaryCondition),
                                    secondaryGraphCondition = new CodeConditionStatement(secondaryCondition);

                                if (pa.Type.GetInterfaces().Contains(typeof(IGraphable))) // Non Graphable
                                {
                                    currentGraphCondition.TrueStatements.Add(
                                        new CodeMethodInvokeExpression(s_host, "WriteElementUtil", s_writer, new CodePrimitiveExpression(pa.NamespaceUri), new CodePrimitiveExpression(pa.Name), new CodeCastExpression(typeof(IGraphable), _propertyReference), new CodeTypeOfExpression(pa.Type), new CodeVariableReferenceExpression("context"), s_resultContext));
                                }
                                else if (pa.Type.GetInterface("System.Collections.IEnumerable") != null) // List
                                {
                                    currentGraphCondition.TrueStatements.Add(new CodeVariableDeclarationStatement(typeof(IEnumerator), "enumerator", new CodeMethodInvokeExpression(_propertyReference, "GetEnumerator")));
                                    currentGraphCondition.TrueStatements.Add(new CodeIterationStatement(new CodeSnippetStatement(), new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("enumerator"), "MoveNext"), new CodeSnippetStatement()));
                                    currentGraphCondition.TrueStatements.Add(new CodeMethodInvokeExpression(s_host, "WriteElementUtil", s_writer, new CodePrimitiveExpression(pa.NamespaceUri), new CodePrimitiveExpression(pa.Name), new CodeCastExpression(typeof(IGraphable), new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("enumerator"), "Current")), new CodeTypeOfExpression(pa.Type), new CodeVariableReferenceExpression("context"), s_resultContext));
                                }
                                else // Not recognized
                                    currentGraphCondition.TrueStatements.Add(new CodeMethodInvokeExpression(s_writer, "WriteElementString", new CodePrimitiveExpression(pa.Name), new CodePrimitiveExpression(pa.NamespaceUri), new CodeMethodInvokeExpression(_propertyReference, "ToString")));

                                graphConditions.Insert(0, currentGraphCondition);
                                secondaryGraphCondition.TrueStatements.AddRange(currentGraphCondition.TrueStatements);
                                graphConditions.Add(secondaryGraphCondition);
                            }

                        } // Foreach for graph

                        // Were there any conditions generated?
                        // TODO: Clean this up and merge with the previous?
                        if (graphConditions.Count == 0)
                        {
                            // Get the property attribute with no type
                            PropertyAttribute pa = buildProperty.PropertyAttributes.First(o => o.Type == null);

                            // UpdateMode imposition
                            if (pa != null && pa.DefaultUpdateMode != MARC.Everest.DataTypes.UpdateMode.Unknown && piType.GetProperty("UpdateMode") != null)
                                graphObjectContentsConditionStatement.TrueStatements.Add(new CodeConditionStatement(
                                    new CodeBinaryOperatorExpression(isUpdateModeImposingCondition, CodeBinaryOperatorType.BooleanAnd,
                                        new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(_propertyReference, "UpdateMode"), CodeBinaryOperatorType.IdentityEquality, s_null)),
                                        new CodeAssignStatement(new CodePropertyReferenceExpression(_propertyReference, "UpdateMode"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(UpdateMode)), pa.DefaultUpdateMode.ToString()))
                                ));
                            // Flavor imposition
                            if (pa != null && pa.ImposeFlavorId != null && piType.GetProperty("Flavor") != null)
                                graphObjectContentsConditionStatement.TrueStatements.Add(new CodeConditionStatement(
                                    new CodeBinaryOperatorExpression(isFlavorImposingCondition, CodeBinaryOperatorType.BooleanAnd,
                                        new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(_propertyReference, "Flavor"), CodeBinaryOperatorType.IdentityEquality, s_null)),
                                        new CodeAssignStatement(new CodePropertyReferenceExpression(_propertyReference, "Flavor"), new CodePrimitiveExpression(pa.ImposeFlavorId))
                                ));
                            // Code system imposition
                            if (pa != null && pa.SupplierDomain != null && piType.GetProperty("CodeSystem") != null)
                                graphObjectContentsConditionStatement.TrueStatements.Add(new CodeConditionStatement(
                                    new CodeBinaryOperatorExpression(isFlavorImposingCondition, CodeBinaryOperatorType.BooleanAnd,
                                        new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(_propertyReference, "CodeSystem"), CodeBinaryOperatorType.IdentityEquality, s_null)),
                                        new CodeAssignStatement(new CodePropertyReferenceExpression(_propertyReference, "CodeSystem"), new CodePrimitiveExpression(pa.SupplierDomain))
                                ));

                            // Structure = Attribute
                            if (pa != null && pa.PropertyType == PropertyAttribute.AttributeAttributeType.Structural)
                                graphObjectContentsConditionStatement.TrueStatements.Add(new CodeMethodInvokeExpression(s_writer, "WriteAttributeString", new CodePrimitiveExpression(pa.Name), new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(Util)), "ToWireFormat", _propertyReference)));
                            else if (pa != null && piTypeInterfaces.Contains(typeof(IGraphable)))
                                graphObjectContentsConditionStatement.TrueStatements.Add(new CodeMethodInvokeExpression(s_host, "WriteElementUtil", s_writer, new CodePrimitiveExpression(pa.NamespaceUri), new CodePrimitiveExpression(pa.Name), new CodeCastExpression(typeof(IGraphable), _propertyReference), new CodeTypeOfExpression(piType), new CodeVariableReferenceExpression("context"), s_resultContext));
                            else if (pa != null && piTypeInterfaces.Contains(typeof(ICollection)))
                            {
                                Type lType = piType;
                                if (lType.GetGenericArguments().Length > 0)
                                    lType = piType.GetGenericArguments()[0];
                                graphObjectContentsConditionStatement.TrueStatements.Add(new CodeIterationStatement(new CodeVariableDeclarationStatement(typeof(Int32), "i", new CodePrimitiveExpression(0)), new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("i"), CodeBinaryOperatorType.LessThan, new CodePropertyReferenceExpression(_propertyReference, "Count")), new CodeSnippetStatement("i++"),
                                    new CodeExpressionStatement(new CodeMethodInvokeExpression(s_host, "WriteElementUtil", s_writer, new CodePrimitiveExpression(pa.NamespaceUri), new CodePrimitiveExpression(pa.Name), new CodeCastExpression(typeof(IGraphable), new CodeIndexerExpression(_propertyReference, new CodeVariableReferenceExpression("i"))), new CodeTypeOfExpression(lType), new CodeVariableReferenceExpression("context"), s_resultContext))));
                            }
                            else // Not recognized
                                graphObjectContentsConditionStatement.TrueStatements.Add(new CodeMethodInvokeExpression(s_writer, "WriteElementString", new CodePrimitiveExpression(pa.Name), new CodePrimitiveExpression(pa.NamespaceUri), new CodeMethodInvokeExpression(_propertyReference, "ToString")));


                        }
                        else
                        {
                            graphConditions.Last().FalseStatements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(s_resultContextAddResultDetailMethod, this.CreateResultDetailExpression(typeof(NotSupportedChoiceResultDetail), s_resultDetailError, new CodePrimitiveExpression(String.Format("Could not find valid representation for {0}", propertyInfo.Name)), s_writerToString, new CodePrimitiveExpression(null)))));

                            // Nest the choices for property serialization
                            CodeConditionStatement graphConditionPa = null;
                            foreach (var gcond in graphConditions)
                            {
                                if (graphConditionPa == null)
                                {
                                    graphConditionPa = gcond;
                                    graphObjectContentsConditionStatement.TrueStatements.Add(graphConditionPa);
                                }
                                else
                                {
                                    graphConditionPa.FalseStatements.Add(gcond);
                                    graphConditionPa = gcond;
                                }
                            }
                        }
                }
            }


            // Close
            retVal.Statements.Add(new CodeConditionStatement(
                new CodeVariableReferenceExpression("isEntryPoint"), 
                new CodeExpressionStatement(this.CreateSimpleMethodCallExpression(s_writer, "WriteEndElement"))));
            return retVal;
        }

        /// <summary>
        /// Create the parse method
        /// </summary>
        private CodeTypeMember CreateParseMethod(Type forType, List<BuildProperty> buildProperties)
        {

            // Create the parse method return value
            CodeMemberMethod retVal = new CodeMemberMethod()
            {
                Name = "Parse",
                Attributes = MemberAttributes.Public,
                ReturnType = new CodeTypeReference(typeof(object))
            };
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(XmlReader)), "r"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Type)), "useType"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Type)), "interactionType"));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(XmlIts1FormatterParseResult)), "resultContext"));

            // Some common constructs 
            var forTypeReference = new CodeTypeReference(forType);


            // Is there a parameterless constructor that we can use?
            if (forType.GetConstructor(Type.EmptyTypes) == null)
                throw new InvalidOperationException(String.Format("Cannot create a formatter for {0} as it has no parameterless constructors", forType));
            retVal.Statements.Add(new CodeVariableDeclarationStatement(forType, "instance", new CodeObjectCreateExpression(forTypeReference)));

            // Get the nil value
            retVal.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(String)), "nil", this.CreateSimpleMethodCallExpression(s_reader, "GetAttribute", "nil", XmlIts1Formatter.NS_XSI)));

            // Start by checking the xsi:nil property
            CodeBinaryOperatorExpression settingsCondition = new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(s_hostSettings, CodeBinaryOperatorType.BitwiseAnd, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SettingsType)), "SuppressNullEnforcement")), CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(0)),
                attributeCountCondition = new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(s_reader, "AttributeCount"), CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(1)),
                xsiNilNilCondition = new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("nil"), CodeBinaryOperatorType.IdentityInequality, s_null), 
                xsiNilCondition = new CodeBinaryOperatorExpression(this.CreateConvertExpression("ToBoolean", new CodeVariableReferenceExpression("nil")), CodeBinaryOperatorType.IdentityEquality, s_true),
                isEmptyElementCondition = new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(s_reader, "IsEmptyElement"), CodeBinaryOperatorType.IdentityEquality, s_true),
                returnNullCondition = new CodeBinaryOperatorExpression(
                    new CodeBinaryOperatorExpression(
                    new CodeBinaryOperatorExpression(settingsCondition, CodeBinaryOperatorType.BooleanAnd, attributeCountCondition),
                    CodeBinaryOperatorType.BooleanAnd,
                    new CodeBinaryOperatorExpression(xsiNilNilCondition, CodeBinaryOperatorType.BooleanAnd, xsiNilCondition)
                    ), CodeBinaryOperatorType.BooleanAnd,
                    isEmptyElementCondition);

            // Create the condition 
            retVal.Statements.Add(new CodeConditionStatement(returnNullCondition , new CodeMethodReturnStatement(s_null)));

            // Now read in the attributes
            var attributeLoop = new CodeIterationStatement(new CodeSnippetStatement(), s_true, new CodeSnippetStatement());
            var attributeCondition = new CodeConditionStatement(this.CreateSimpleMethodCallExpression(s_reader, "MoveToFirstAttribute"), attributeLoop);

            // Ensure that if this attribute isn't XML ITS1 we don't process
            attributeLoop.Statements.Add(new CodeConditionStatement(
                new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(s_readerLocalName, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression("ITSVersion")),
                    CodeBinaryOperatorType.BooleanAnd,
                    new CodeBinaryOperatorExpression(s_readerValue, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression("XML_1.0"))
                ),
                new CodeThrowExceptionStatement(new CodeObjectCreateExpression(typeof(InvalidOperationException), this.CreateStringFormatExpression("This formatter can only parse XML_1.0 structures. This structure claims to be '{0}'", s_readerValue)))
            ));
            // Ensure that we don't process XMLNS
            var ignoreAttributesConditionStatement = new CodeConditionStatement(
                new CodeBinaryOperatorExpression(
                    new CodeBinaryOperatorExpression(
                        new CodeBinaryOperatorExpression(new CodeBinaryOperatorExpression(s_readerPrefix, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression("xmlns")), CodeBinaryOperatorType.BooleanOr,
                        new CodeBinaryOperatorExpression(s_readerLocalName, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression("xmlns"))), CodeBinaryOperatorType.BooleanOr,
                    new CodeBinaryOperatorExpression(s_readerLocalName, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression("ITSVersion"))), CodeBinaryOperatorType.BooleanOr,
                    new CodeBinaryOperatorExpression(s_readerNamespace, CodeBinaryOperatorType.IdentityEquality, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(XmlIts1Formatter)), "NS_XSI")))
            );
            
            // Build the statements
            CodeConditionStatement currentStatement = new CodeConditionStatement();
            ignoreAttributesConditionStatement.FalseStatements.Add(currentStatement);

            
            foreach (var bp in buildProperties)
            {

               
                var pa = bp.PropertyAttributes.FirstOrDefault();

                if (pa == null || pa.PropertyType != PropertyAttribute.AttributeAttributeType.Structural)
                    continue;

                // Build the current statment condition
                var condition = new CodeBinaryOperatorExpression(s_readerLocalName, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(pa.Name));
                if (pa.NamespaceUri != XmlIts1Formatter.NS_HL7)
                    condition = new CodeBinaryOperatorExpression(condition, CodeBinaryOperatorType.BooleanAnd,
                        new CodeBinaryOperatorExpression(s_readerNamespace, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(pa.NamespaceUri)));

                // Set current statement 
                if (currentStatement.TrueStatements.Count > 0)
                {
                    var falseStatement = new CodeConditionStatement();
                    currentStatement.FalseStatements.Add(falseStatement);
                    currentStatement = falseStatement;
                }

                // Set the condition and parse if needed
                currentStatement.Condition = condition;

                // SET method
                if (bp.PropertyInfo.GetSetMethod() != null)
                    currentStatement.TrueStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(s_instance, bp.PropertyInfo.Name), this.CreateUtilConvertMethodInvoke(bp.PropertyInfo.PropertyType, s_readerValue)));
                else
                    currentStatement.TrueStatements.Add(new CodeMethodInvokeExpression(s_resultContextAddResultDetailMethod, this.CreateResultDetailExpression(typeof(NotImplementedResultDetail), s_resultDetailError, new CodePrimitiveExpression(String.Format("Property {0} is readonly, cannot set value", bp.PropertyInfo.Name)), s_readerToString)));

                
            }

            if (currentStatement.Condition == null)
            {
                ignoreAttributesConditionStatement.FalseStatements.Remove(currentStatement);
                ignoreAttributesConditionStatement.FalseStatements.Add(new CodeMethodInvokeExpression(s_resultContextAddResultDetailMethod, this.CreateResultDetailExpression(typeof(NotImplementedElementResultDetail), s_resultDetailInformation, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(String)), "Format", new CodePrimitiveExpression("@{0}"), s_readerLocalName), s_readerNamespace, s_readerToString, s_null)));
            }
            else
            {
                currentStatement.FalseStatements.Clear();
                currentStatement.FalseStatements.Add(new CodeMethodInvokeExpression(s_resultContextAddResultDetailMethod, this.CreateResultDetailExpression(typeof(NotImplementedElementResultDetail), s_resultDetailInformation, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(String)), "Format", new CodePrimitiveExpression("@{0}"), s_readerLocalName), s_readerNamespace, s_readerToString, s_null)));
            }
            
            //ignoreAttributesConditionStatement.FalseStatements.AddRange(setStatements);
            attributeLoop.Statements.Add(ignoreAttributesConditionStatement);


            // Break out statement
            attributeLoop.Statements.Add(new CodeConditionStatement(
                new CodeBinaryOperatorExpression(this.CreateSimpleMethodCallExpression(s_reader, "MoveToNextAttribute"), CodeBinaryOperatorType.IdentityInequality, s_true),
                new CodeSnippetStatement("break;")
            ));

            // Now move to element and continue
            attributeCondition.TrueStatements.Add(this.CreateSimpleMethodCallExpression(s_reader, "MoveToElement"));
            retVal.Statements.Add(attributeCondition);

            // Is xsi:nil set then return early
            CodeBinaryOperatorExpression returnInstanceEarlyCondition = new CodeBinaryOperatorExpression(
                new CodeBinaryOperatorExpression(xsiNilNilCondition, CodeBinaryOperatorType.BitwiseAnd, xsiNilCondition),
                CodeBinaryOperatorType.BitwiseAnd, settingsCondition);
            retVal.Statements.Add(new CodeConditionStatement(returnInstanceEarlyCondition, new CodeMethodReturnStatement(s_instance)));
                
            // Reader is an empty element condition
            retVal.Statements.Add(new CodeConditionStatement(isEmptyElementCondition, new CodeMethodReturnStatement(s_instance)));

            // Read content
            retVal.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(s_host, "ParseElementContent", s_reader, s_instance, s_readerLocalName, new CodePropertyReferenceExpression(s_reader, "Depth"), s_interactionType, s_resultContext)));

            if (IsStatementNull(retVal))
                System.Diagnostics.Debugger.Break();

            return retVal;
        }


     
        /// <summary>
        /// Create the handles type property
        /// </summary>
        private CodeTypeMember CreateHandlesTypeProperty(Type forType)
        {
            CodeMemberProperty retVal = new CodeMemberProperty()
            {
                Name = "HandlesType",
                HasGet = true,
                Attributes = MemberAttributes.Public,
                Type = new CodeTypeReference(typeof(Type))
            };

            // Get statements
            var _this = new CodeThisReferenceExpression();
            retVal.GetStatements.Add(new CodeMethodReturnStatement(new CodeTypeOfExpression(forType)));

            // Return
            return retVal;
        }

        /// <summary>
        /// Create the host property for the specified type
        /// </summary>
        private CodeTypeMember CreateHostProperty()
        {
            CodeMemberProperty retVal = new CodeMemberProperty()
            {
                Name = "Host",
                HasGet = true,
                HasSet = true,
                Attributes = MemberAttributes.Public,
                Type = new CodeTypeReference(typeof(XmlIts1Formatter))
            };

            // Get statements
            var _this = new CodeThisReferenceExpression();
            retVal.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(_this, "m_host")));
            retVal.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(_this, "m_host"), new CodeVariableReferenceExpression("value")));

            // Return
            return retVal;
        }

    }
}
