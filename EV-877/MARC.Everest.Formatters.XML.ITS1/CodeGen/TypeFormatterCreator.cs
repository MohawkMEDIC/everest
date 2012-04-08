/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using MARC.Everest.Connectors;
using MARC.Everest.Attributes;
using MARC.Everest.Interfaces;
using System.Xml;
using System.Collections;
using System.Threading;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Xml;

namespace MARC.Everest.Formatters.XML.ITS1.CodeGen
{

    /// <summary>
    /// Delegate for the create type formatter
    /// </summary>
    public delegate void CreateTypeFormatterCompletedDelegate(CodeTypeDeclaration declaration);


    /// <summary>
    /// Creates TypeFormatters
    /// </summary>
    public class TypeFormatterCreator
    {

        /// <summary>
        /// Gets or sets the reset event
        /// </summary>
        public ManualResetEvent ResetEvent { get; set; }

        /// <summary>
        /// Code type declaration has been completed
        /// </summary>
        public event CreateTypeFormatterCompletedDelegate CodeTypeDeclarationCompleted;

        /// <summary>
        /// Create the detail property
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "forType")]
        private CodeMemberProperty CreateDetailProperty(Type forType)
        {
            CodeMemberProperty property = new CodeMemberProperty();
            property.Name = "Details";
            property.HasGet = true;
            property.HasSet = true;
            property.GetStatements.Add(new CodeSnippetExpression("return details;"));
            property.SetStatements.Add(new CodeSnippetExpression("details = value;"));
            property.Attributes = MemberAttributes.Public;
            property.Type = new CodeTypeReference(typeof(IResultDetail[]));

            // return 
            return property;
        }

        /// <summary>
        /// Get build properties
        /// </summary>
        private List<PropertyInfo> GetBuildProperties(Type instanceType)
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
            return buildProperties;
        }

        /// <summary>
        /// Creates the handles type property
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private CodeMemberProperty CreateHandlesTypeProperty(Type forType)
        {
            CodeMemberProperty property = new CodeMemberProperty();

            // Find the structure attribute
            property.Name = "HandlesType";
            property.HasGet = true;
            property.GetStatements.Add(new CodeSnippetExpression(string.Format("return typeof({0});",
                CreateTypeReference(new CodeTypeReference(forType)))));

            property.Attributes = MemberAttributes.Public;
            property.Type = new CodeTypeReference(typeof(Type));

            // return 
            return property;
        }

        /// <summary>
        /// Create a type reference in string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
        private string CreateTypeReference(CodeTypeReference tref)
        {
            StringBuilder sb = new StringBuilder();
            string bt = tref.BaseType;

            if (bt.Contains("`"))
                sb.Append(bt.Substring(0, bt.IndexOf("`")));
            else
                sb.Append(bt);

            if (tref.TypeArguments != null && tref.TypeArguments.Count > 0)
            {
                sb.Append("<");
                foreach (CodeTypeReference ctref in tref.TypeArguments)
                    sb.Append(CreateTypeReference(ctref) + ",");
                sb.Remove(sb.Length - 1, 1);
                sb.Append(">");
            }

            // Is it an array
            if (tref.ArrayElementType != null)
                sb.Append("[]");

            if (sb.ToString().Equals("System.Void"))
                System.Diagnostics.Debugger.Break();

            return sb.ToString();
        }

        /// <summary>
        /// Create validator method
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
        private CodeMemberMethod CreateValidateMethod(Type forType)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = "Validate";
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(MARC.Everest.Interfaces.IGraphable), "o"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "location"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IResultDetail[]), "outDetails") { Direction = FieldDirection.Out });
            method.Attributes = MemberAttributes.Public;
            method.ReturnType = new CodeTypeReference(typeof(bool));


            // Details
            CodeTypeReference tref = new CodeTypeReference(forType);
            string trefTypeRefernece = CreateTypeReference(tref);
            method.Statements.Add(new CodeSnippetExpression(string.Format("{0} instance = o as {0};", trefTypeRefernece)));
            method.Statements.Add(new CodeSnippetExpression("outDetails = new MARC.Everest.Connectors.IResultDetail[0]; System.Collections.Generic.List<MARC.Everest.Connectors.IResultDetail> details = new System.Collections.Generic.List<MARC.Everest.Connectors.IResultDetail>(10);"));
            method.Statements.Add(new CodeSnippetExpression("bool isValid = true;"));



            // Now determine the attributes
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("if(instance == null && o != null) throw new System.NullReferenceException(System.String.Format(\"Could not cast type '{{0}}' to '{0}'!\", o.GetType().FullName));", trefTypeRefernece);
            if (forType.GetProperty("NullFlavor") != null)
                sb.Append("if(instance == null || instance.NullFlavor != null) return true;"); // null flavor always returns true

            foreach (PropertyInfo pi in forType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {

                object[] propAttributes = pi.GetCustomAttributes(typeof(PropertyAttribute), true);

                if (propAttributes.Length > 0)
                {
                    PropertyAttribute pa = propAttributes[0] as PropertyAttribute;
                    if (pa.Conformance == PropertyAttribute.AttributeConformanceType.Mandatory &&
                        pi.PropertyType.GetProperty("NullFlavor") != null)
                        sb.AppendFormat("if(instance.{0} == null || instance.{0}.NullFlavor != null) {{ isValid &= false; details.Add(new MARC.Everest.Connectors.MandatoryElementMissingResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, \"Property {0} in {1} is marked mandatory and is either not assigned, or is assigned a null flavor. This is not permitted.\", location));  }} \r\n", pi.Name, tref.BaseType);
                    else if (pa.Conformance == PropertyAttribute.AttributeConformanceType.Populated)
                        sb.AppendFormat("if(instance.{0} == null) {{ isValid &= Host.CreateRequiredElements; details.Add(new MARC.Everest.Connectors.RequiredElementMissingResultDetail(isValid ? MARC.Everest.Connectors.ResultDetailType.Warning : MARC.Everest.Connectors.ResultDetailType.Error, \"Property {0} in {1} is marked 'populated' and isn't assigned (you must at minimum, assign a nullFlavor for this attribute)!\", location));  }} \r\n", pi.Name, tref.BaseType);
                    else if (pa.MinOccurs != 0)
                        sb.AppendFormat("if(instance.{0} != null && (instance.{0}.Count > {3} ||  instance.{0}.Count < {1})) {{ isValid &= false; details.Add(new MARC.Everest.Connectors.InsufficientRepetionsResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, \"Property {0} in {2} does not have enough elements in the list, need between {1} and {3} elements!\", location));  }} \r\n", pi.Name, pa.MinOccurs, tref.BaseType, pa.MaxOccurs < 0 ? Int32.MaxValue : pa.MaxOccurs);
                }
            }

            method.Statements.Add(new CodeSnippetExpression(sb.ToString()));
            method.Statements.Add(new CodeSnippetExpression("outDetails = details.ToArray();"));
            method.Statements.Add(new CodeSnippetExpression(string.Format("return isValid;")));
            return method;
        }

        /// <summary>
        /// Returns true if the property is traversable
        /// </summary>
        public bool IsTraversable(PropertyInfo pi)
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
        public bool IsNonStructural(PropertyInfo pi)
        {
            object[] propertyAttributes = pi.GetCustomAttributes(typeof(PropertyAttribute), false);
            if (propertyAttributes.Length > 0)
                return (propertyAttributes[0] as PropertyAttribute).PropertyType == PropertyAttribute.AttributeAttributeType.NonStructural;
            else
                return false;
        }

        /// <summary>
        /// Create a graphing method
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "lastWasAttribute"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private CodeMemberMethod CreateGraphMethod(Type forType)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = "Graph";
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Xml.XmlWriter), "s"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Object), "o"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IGraphable), "context"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(XmlIts1FormatterGraphResult), "resultContext"));

            method.Attributes = MemberAttributes.Public;
            method.ReturnType = new CodeTypeReference(typeof(ResultCode));

            // Type reference to the forType
            CodeTypeReference tref = new CodeTypeReference(forType);
            string trefTypeReference = CreateTypeReference(tref);

            // Build the method body
            CodeStatementCollection methodBodyEle = new CodeStatementCollection(), methodBodyAtt = new CodeStatementCollection();
            methodBodyAtt.Add(new CodeSnippetExpression("if(o == null) throw new System.ArgumentNullException(\"o\")"));
            methodBodyAtt.Add(new CodeSnippetExpression(String.Format("if(instance == null) throw new System.ArgumentException(System.String.Format(\"Could not cast type '{{0}}' to '{0}'!\", o.GetType().FullName))", trefTypeReference)));
            methodBodyAtt.Add(new CodeSnippetExpression("bool isInstanceNull = instance.NullFlavor != null"));

            // Interaction?
            object[] structureAttributes = forType.GetCustomAttributes(typeof(StructureAttribute), false);
            StructureAttribute structureAttribute = structureAttributes[0] as StructureAttribute;
            if (structureAttribute.StructureType == StructureAttribute.StructureAttributeType.Interaction)
            {
                methodBodyAtt.Add(new CodeSnippetExpression(String.Format("bool isEntryPoint = true; s.WriteStartElement(\"{0}\", \"urn:hl7-org:v3\")", structureAttribute.Name)));
                methodBodyAtt.Add(new CodeSnippetExpression("s.WriteAttributeString(\"ITSVersion\",\"XML_1.0\")")); // Add ITS version
                methodBodyAtt.Add(new CodeSnippetExpression("if(isEntryPoint) s.WriteAttributeString(\"xmlns\", \"xsi\", null, MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter.NS_XSI)"));
            }
            else if (structureAttribute.IsEntryPoint)
            {
                methodBodyAtt.Add(new CodeSnippetExpression(String.Format("bool isEntryPoint = s is MARC.Everest.Xml.XmlStateWriter && (s as MARC.Everest.Xml.XmlStateWriter).ElementStack.Count == 0 || s.WriteState == System.Xml.WriteState.Start; if(isEntryPoint) s.WriteStartElement(\"{0}\", \"urn:hl7-org:v3\")", structureAttribute.Name)));
                methodBodyAtt.Add(new CodeSnippetExpression("if(isEntryPoint) s.WriteAttributeString(\"xmlns\", \"xsi\", null, MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter.NS_XSI)"));
            }

            #region Build inherited properties in correct order
            var buildProperties = GetBuildProperties(forType);
            #endregion

            bool lastWasAttribute = true;
            // Get property information
            foreach (PropertyInfo pi in buildProperties)
            {

                Type piType = pi.PropertyType;
                List<Type> piInterfaces = new List<Type>(piType.GetInterfaces()); ;
                object[] propertyAttributes = pi.GetCustomAttributes(typeof(PropertyAttribute), true);

                if (propertyAttributes.Length > 0) // Property attribute ... process it
                {
                    var propertyAttribute = propertyAttributes[0] as PropertyAttribute;
                    CodeStatementCollection methodBody = propertyAttribute.PropertyType == PropertyAttribute.AttributeAttributeType.Structural ? methodBodyAtt : methodBodyEle;

                    bool retValChanged = false;

                    #region Process Property
                    // Validation Rule Change: We'll require the user to perform this
                    // Is this a required attribute that is null? We'll set a null flavor 
                    if ((propertyAttribute.Conformance == PropertyAttribute.AttributeConformanceType.Required || propertyAttribute.Conformance == PropertyAttribute.AttributeConformanceType.Populated) &&
                        propertyAttribute.PropertyType != PropertyAttribute.AttributeAttributeType.Structural &&
                        piType.GetProperty("NullFlavor") != null &&
                        !piType.IsAbstract &&
                        pi.CanWrite)
                    {
                        var nullFlavorProperty = piType.GetProperty("NullFlavor");
                        // Locate the default property 
                        methodBody.Add(new CodeSnippetStatement(String.Format("if(instance.{0} == null && Host.CreateRequiredElements) {{ instance.{0} = new {1}(); instance.{0}.NullFlavor = {2}.NoInformation; }}", pi.Name, CreateTypeReference(new CodeTypeReference(pi.PropertyType)), CreateTypeReference(new CodeTypeReference(nullFlavorProperty.PropertyType.GetGenericArguments()[0])))));
                    }


                    // Is the instance's null flavor set?
                    // Remarks: We do this way because we still need to write the null flavor out even if the  null flavor
                    if (pi.Name != "NullFlavor" && forType.GetProperty("NullFlavor") != null)
                        methodBody.Add(new CodeSnippetStatement(String.Format("if(instance.{0} != null && !isInstanceNull) {{\r\n", pi.Name)));
                    else if (pi.Name == "NullFlavor")
                    {
                        methodBody.Add(new CodeSnippetExpression("if(instance.NullFlavor != null) this.Host.WriteNullFlavorUtil(s, instance.NullFlavor)"));
                        continue;
                    }

                    // Represents a choice
                    if (propertyAttributes.Length > 1)
                    {
                        #region Property is a Choice
                        int ic = 0;
                        // Must be a non structural attribute
                        foreach (PropertyAttribute pa in propertyAttributes)
                        {

                            // Only process if
                            //  Type on the pa is not null , and
                            //  Either
                            //      1. The pa type matches the real property type (represents an alt traversal)
                            //      2. The pi type is abstract (represents a choice)
                            //      3. The pi is an object (represents a choice)

                            if (pa.Type != null && (pa.Type == pi.PropertyType || (pa.Type.IsSubclassOf(pi.PropertyType) && pi.PropertyType.IsAssignableFrom(pa.Type)) || pi.PropertyType == typeof(object)))
                            {
                                // write if statement
                                if(pa.InteractionOwner != null)
                                    methodBody.Add(new CodeSnippetStatement(String.Format("{2} if(instance.{0}.GetType() == typeof({1}) && context is {3}) {{\r\n", pi.Name, pa.Type.FullName, ic > 0 ? "else" : "", CreateTypeReference(new CodeTypeReference(pa.InteractionOwner)))));
                                else
                                    methodBody.Add(new CodeSnippetStatement(String.Format("{2} if(instance.{0}.GetType() == typeof({1})) {{\r\n", pi.Name, pa.Type.FullName, ic > 0 ? "else" : "")));
                                // Output
                                if (pa.Type.GetInterface("MARC.Everest.Interfaces.IGraphable") != null) // Non Graphable
                                    methodBody.Add(new CodeSnippetExpression(String.Format("retVal = Host.WriteElementUtil(s, \"{0}\", (MARC.Everest.Interfaces.IGraphable)instance.{1}, typeof({2}), context, resultContext)", pa.Name, pi.Name, CreateTypeReference(new CodeTypeReference(pa.Type)))));
                                else if (pa.Type.GetInterface("System.Collections.IEnumerable") != null) // List
                                    methodBody.Add(new CodeSnippetStatement(String.Format("foreach(MARC.Everest.Interfaces.IGraphable ig in instance.{0}) {{ retVal = Host.WriteElementUtil(s, \"{1}\", ig, typeof({2}), context, resultContext); }}", pi.Name, pa.Name, CreateTypeReference(new CodeTypeReference(pa.Type)))));
                                else // Not recognized
                                    methodBody.Add(new CodeSnippetExpression(String.Format("s.WriteElementString(\"{0}\", \"urn:hl7-org:v3\", instance.{1}.ToString())\r\n", pa.Name, pi.Name)));

                                methodBody.Add(new CodeSnippetStatement("}"));
                                ic++;
                            }

                        }

                        // Was a choice found?
                        if (ic == 0) // nope
                        {
                            PropertyAttribute pa = Array.Find<Object>(propertyAttributes, o => (o as PropertyAttribute).Type == null) as PropertyAttribute;
                            // Output
                            if (pa != null && pi.PropertyType.GetInterface("MARC.Everest.Interfaces.IGraphable") != null) // Non Graphable
                            {
                                retValChanged = true;
                                methodBody.Add(new CodeSnippetExpression(String.Format("retVal = Host.WriteElementUtil(s, \"{0}\", (MARC.Everest.Interfaces.IGraphable)instance.{1}, typeof({2}), context, resultContext)", pa.Name, pi.Name, CreateTypeReference(new CodeTypeReference(pi.PropertyType)))));
                            }
                            else if (pa != null && pi.PropertyType.GetInterface("System.Collections.IEnumerable") != null) // List
                            {
                                methodBody.Add(new CodeSnippetStatement(String.Format("foreach(MARC.Everest.Interfaces.IGraphable ig in instance.{0}) {{ retVal = Host.WriteElementUtil(s, \"{1}\", ig, typeof({2}), context, resultContext); }}", pi.Name, pa.Name, CreateTypeReference(new CodeTypeReference(pi.PropertyType)))));
                                retValChanged = true;
                            }
                            else if (pa != null) // Not recognized
                                methodBody.Add(new CodeSnippetExpression(String.Format("s.WriteElementString(\"{0}\", \"urn:hl7-org:v3\", instance.{1}.ToString())", pa.Name, pi.Name)));
                        }
                        else
                            methodBody.Add(new CodeSnippetStatement(String.Format("else {{ resultContext.AddResultDetail(new MARC.Everest.Connectors.NotSupportedChoiceResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, System.String.Format(\"Type {{0}} is not a valid choice according to available choice elements\", instance.{0}.GetType()), s.ToString(), null)); }}", pi.Name)));
                        #endregion
                    }
                    else
                    {
                        #region Property is a Structural, or NonStructural property
                        PropertyAttribute pa = propertyAttribute;
                        if (pa.DefaultUpdateMode != MARC.Everest.DataTypes.UpdateMode.Unknown && piType.GetProperty("UpdateMode") != null)
                            methodBody.Add(new CodeSnippetExpression(String.Format("if(instance.{0}.UpdateMode == null && (host.Settings & MARC.Everest.Formatters.XML.ITS1.SettingsType.AllowUpdateModeImposing) == MARC.Everest.Formatters.XML.ITS1.SettingsType.AllowUpdateModeImposing) instance.{0}.UpdateMode = MARC.Everest.DataTypes.UpdateMode.{1}", pi.Name, pa.DefaultUpdateMode)));
                        if (pa.ImposeFlavorId != null && piType.GetProperty("Flavor") != null)
                            methodBody.Add(new CodeSnippetExpression(String.Format("if((host.Settings & MARC.Everest.Formatters.XML.ITS1.SettingsType.AllowFlavorImposing) == MARC.Everest.Formatters.XML.ITS1.SettingsType.AllowFlavorImposing) instance.{0}.Flavor = \"{1}\"", pi.Name, pa.ImposeFlavorId)));
                        if (pa.SupplierDomain != null && piType.GetProperty("CodeSystem") != null)
                            methodBody.Add(new CodeSnippetExpression(String.Format("if((host.Settings & MARC.Everest.Formatters.XML.ITS1.SettingsType.AllowSupplierDomainImposing) == MARC.Everest.Formatters.XML.ITS1.SettingsType.AllowSupplierDomainImposing && System.String.IsNullOrEmpty(instance.{0}.CodeSystem) && instance.{0}.NullFlavor == null) instance.{0}.CodeSystem = \"{1}\"", pi.Name, pa.SupplierDomain)));

                        // Write attribute/element
                        if (pa.PropertyType == PropertyAttribute.AttributeAttributeType.Structural) // Attribute
                            methodBody.Add(new CodeSnippetExpression(String.Format("s.WriteAttributeString(\"{0}\", MARC.Everest.Connectors.Util.ToWireFormat(instance.{1}))", pa.Name, pi.Name)));
                        else if (piInterfaces.Contains(typeof(IGraphable))) // Non Graphable
                        {
                            if (piInterfaces.Contains(typeof(IColl)))
                                methodBody.Add(new CodeSnippetStatement(String.Format("if(!instance.{0}.IsEmpty)", pi.Name)));
                            retValChanged = true;
                            methodBody.Add(new CodeSnippetExpression(String.Format("retVal = Host.WriteElementUtil(s, \"{0}\", (MARC.Everest.Interfaces.IGraphable)instance.{1}, typeof({2}), context, resultContext)", pa.Name, pi.Name, CreateTypeReference(new CodeTypeReference(pi.PropertyType)))));
                        }
                        else if (piInterfaces.Contains(typeof(ICollection))) // List
                        {
                            // Is this a collection
                            Type lType = pi.PropertyType;
                            if (lType.GetGenericArguments().Length > 0)
                                lType = lType.GetGenericArguments()[0];
                            methodBody.Add(new CodeSnippetStatement(String.Format("foreach(MARC.Everest.Interfaces.IGraphable ig in instance.{0}) {{ retVal = Host.WriteElementUtil(s, \"{1}\", ig, typeof({2}), context, resultContext); }}", pi.Name, pa.Name, CreateTypeReference(new CodeTypeReference(lType)))));
                            retValChanged = true;
                        }
                        else // Not recognized
                        {
                            methodBody.Add(new CodeSnippetExpression(String.Format("s.WriteElementString(\"{0}\", \"urn:hl7-org:v3\", instance.{1}.ToString())", pa.Name, pi.Name)));
                        }
                        #endregion

                        lastWasAttribute = pa.PropertyType == PropertyAttribute.AttributeAttributeType.Structural;
                    }

                    if(retValChanged)
                        methodBody.Add(new CodeSnippetExpression("if(resultContext.Code != MARC.Everest.Connectors.ResultCode.Accepted) retVal = resultContext.Code"));

                    //if(pi.Name != "NullFlavor")
                    //    methodBody.Add(new CodeSnippetStatement("}")); // Finish the if that encapsulates this property code
                    methodBody.Add(new CodeSnippetStatement("}")); // Finish the if that encapsulates this property code

                    #endregion

                }
            }

            //if (forType.GetProperty("NullFlavor") != null && !forTypeNullFlavor)
            //    methodBody.Append("}");

            // End interaction
            if (structureAttribute.StructureType == StructureAttribute.StructureAttributeType.Interaction || structureAttribute.IsEntryPoint)
                methodBodyEle.Add(new CodeSnippetExpression("if(isEntryPoint) s.WriteEndElement()")); // End Interaction

            // Create stuff finally
            method.Statements.Add(new CodeSnippetExpression(string.Format("{0} instance = o as {0};", trefTypeReference)));
            method.Statements.Add(new CodeSnippetExpression("MARC.Everest.Connectors.ResultCode retVal = MARC.Everest.Connectors.ResultCode.Accepted"));
            method.Statements.AddRange(methodBodyAtt);
            method.Statements.AddRange(methodBodyEle);
            method.Statements.Add(new CodeSnippetExpression("return retVal"));

            return method;
        }

        /// <summary>
        /// Create a parse method
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "r"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private CodeMemberMethod CreateParseMethod(Type forType)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = "Parse";
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Xml.XmlReader), "s"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Type), "useType"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Type), "currentInteractionType"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(XmlIts1FormatterParseResult), "resultContext"));
            method.Attributes = MemberAttributes.Public;
            method.ReturnType = new CodeTypeReference(typeof(object));

            CodeStatementCollection methodBuilder = new CodeStatementCollection();

            
                // Check for xsi:nil
            methodBuilder.Add(new CodeSnippetExpression("System.String nil = s.GetAttribute(\"nil\", MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter.NS_XSI)"));
            methodBuilder.Add(new CodeSnippetExpression("if(!System.String.IsNullOrEmpty(nil) && System.Convert.ToBoolean(nil)) return null;"));
            methodBuilder.Add(new CodeSnippetExpression(String.Format("{0} instance = new {0}();", CreateTypeReference(new CodeTypeReference(forType)))));


            // Assert that it is a start element
            methodBuilder.Add(new CodeSnippetExpression("if(s.NodeType != System.Xml.XmlNodeType.Element) throw new System.InvalidOperationException(System.String.Format(\"Expected node type of Element, actual node type is '{0}'\", s.NodeType))"));

            // Check for ITS verison?
            object[] structureAttributes = forType.GetCustomAttributes(typeof(StructureAttribute), false);
            var structureAttribute = structureAttributes[0] as StructureAttribute;
            if (structureAttribute.StructureType == StructureAttribute.StructureAttributeType.Interaction)
            {
                methodBuilder.Add(new CodeSnippetStatement("if(s.GetAttribute(\"ITSVersion\") != \"XML_1.0\") "));
                methodBuilder.Add(new CodeSnippetExpression("throw new System.InvalidOperationException(System.String.Format(\"This formatter can only parse XML_1.0 structures. This structure claims to be '{0}'.\", s.GetAttribute(\"ITSVersion\")))"));
            }

            CodeStatementCollection methodAttributes = new CodeStatementCollection(), methodElements = new CodeStatementCollection();

            //methodBuilder.Append("System.Diagnostics.Debug.WriteLine(System.String.Format(\"{0}{1}\", new System.String('\\t',s.Depth), s.Name));");
            // Method element statements
            //methodBuilder.Add(new CodeSnippetExpression("System.Collections.Generic.List<MARC.Everest.Connectors.ResultDetail> resultDetail = new System.Collections.Generic.List<MARC.Everest.Connectors.ResultDetail>()"));
            methodElements.Add(new CodeSnippetExpression("if(s.IsEmptyElement) return instance;\r\nstring sName = s.Name;\r\nint sDepth = s.Depth"));
            methodElements.Add(new CodeSnippetStatement("s.Read();\r\nwhile(!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Name == sName && s.Depth == sDepth)) {"));
            methodElements.Add(new CodeSnippetStatement("string oldName = s.LocalName; \r\n try { if(s.NodeType == System.Xml.XmlNodeType.Element) { "));

            int cnt = 0;

            List<String> generatedProperties = new List<string>(forType.GetProperties().Length);

            // Read properties and create the parsing logic for them
            foreach (PropertyInfo pi in forType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (generatedProperties.Contains(pi.Name))
                    continue;
                generatedProperties.Add(pi.Name);
                object[] propertyAttribute = pi.GetCustomAttributes(typeof(PropertyAttribute), true); // get prop atts
                if (propertyAttribute.Length > 0) // we have a match
                {

                    foreach (PropertyAttribute pa in propertyAttribute) // Iterate through property attributes
                    {

                        // Are we interested in this
                        // 1. The property is a generic parameter that matches the argument supplied to the type
                        if (!(forType.IsGenericType && ((pi.PropertyType == forType.GetGenericArguments()[0] && pa.Type == pi.PropertyType) ||
                            (pa.Type == null && pa.Type != forType.GetGenericArguments()[0])) ||
                            (pi.PropertyType == typeof(object)) || propertyAttribute.Length == 1 || pa.Type == null || pa.Type != null && pa.Type.IsSubclassOf(pi.PropertyType) && pi.PropertyType.IsAssignableFrom(pa.Type)))
                            continue;

                        
                        // Determine attribute or property
                        if (pa.PropertyType == PropertyAttribute.AttributeAttributeType.Structural) // att
                        {
                            // TODO: This could be cleaned up a little
                            //methodAttributes.AppendFormat("System.Diagnostics.Debug.WriteLine(System.String.Format(\"{{0}}@{0}\", new System.String('\\t',s.Depth)));", pa.Name);
                            if (pi.GetSetMethod() != null)
                            {
                                if (!String.IsNullOrEmpty(pa.FixedValue))
                                    methodAttributes.Add(new CodeSnippetStatement(String.Format("if(s.GetAttribute(\"{1}\") != null){{ if(!\"{3}\".Equals(s.GetAttribute(\"{1}\"))) resultContext.AddResultDetail(new MARC.Everest.Connectors.FixedValueMisMatchedResultDetail(s.GetAttribute(\"{1}\"), \"{3}\", false, s.ToString()));  instance.{0} = ({2})MARC.Everest.Connectors.Util.FromWireFormat(s.GetAttribute(\"{1}\"), typeof({2})); }}", pi.Name, pa.Name, CreateTypeReference(new CodeTypeReference(pi.PropertyType)), pa.FixedValue)));
                                else
                                    methodAttributes.Add(new CodeSnippetExpression(String.Format("if(s.GetAttribute(\"{1}\") != null) instance.{0} = ({2})MARC.Everest.Connectors.Util.FromWireFormat(s.GetAttribute(\"{1}\"), typeof({2}))", pi.Name, pa.Name, CreateTypeReference(new CodeTypeReference(pi.PropertyType)))));
                            }
                            else
                                methodAttributes.Add(new CodeSnippetExpression(String.Format("if(s.GetAttribute(\"{1}\") != null && s.GetAttribute(\"{1}\") != MARC.Everest.Connectors.Util.ToWireFormat(instance.{0})) resultContext.AddResultDetail(new MARC.Everest.Connectors.FixedValueMisMatchedResultDetail(s.GetAttribute(\"{1}\"), instance.{0}.ToString(), true, s.ToString()))", pi.Name, pa.Name)));
                        }
                        else
                        {
                            methodElements.Add(new CodeSnippetStatement(String.Format("{2} if(s.LocalName == \"{0}\" {1}) {{\r\n", pa.Name, pa.InteractionOwner != null ? string.Format("&& currentInteractionType == typeof({0})", pa.InteractionOwner.FullName) : "", cnt > 0 ? "else" : "")));
                            cnt++;

                            // Fake attributes
                            if (!String.IsNullOrEmpty(pa.ImposeFlavorId))
                                methodElements.Add(new CodeSnippetExpression(String.Format("if(System.String.IsNullOrEmpty(s.GetAttribute(\"specializationType\")) && s is MARC.Everest.Xml.XmlStateReader && (this.Host.Settings & MARC.Everest.Formatters.XML.ITS1.SettingsType.AllowFlavorImposing) == MARC.Everest.Formatters.XML.ITS1.SettingsType.AllowFlavorImposing) (s as MARC.Everest.Xml.XmlStateReader).AddFakeAttribute(\"specializationType\", \"{0}\")", pa.ImposeFlavorId)));

                            // No way to deserialize this appropriately :( So we'll add a remark that says so
                            if (pa.Type == null && propertyAttribute.Length == 1 && pi.PropertyType == typeof(System.Object))
                                methodElements.Add(new CodeSnippetExpression(String.Format("resultContext.AddResultDetail(new MARC.Everest.Connectors.NotImplementedElementResultDetail(MARC.Everest.Connectors.ResultDetailType.Warning, \"{0}\", \"urn:hl7-org:v3\", s.ToString(), null))", pa.Name)));
                            else if (pi.GetSetMethod() != null &&
                                ((pa.Type != null && pa.Type.GetInterface("MARC.Everest.Interfaces.IGraphable") != null) ||
                                (pa.Type == null && pi.PropertyType.GetInterface("MARC.Everest.Interfaces.IGraphable") != null))) // element that is graphable
                            {

                                // Directly assignable? 
                                var nameGuid = Guid.NewGuid().ToString("N");
                                methodElements.Add(new CodeSnippetExpression(String.Format("object d{2} = Host.ParseObject(s, typeof({1}), currentInteractionType, resultContext); instance.{0} = d{2} is {1} ? d{2} as {1} : ({1})MARC.Everest.Connectors.Util.FromWireFormat(d{2}, typeof({1}))", pi.Name, CreateTypeReference(new CodeTypeReference(pa.Type ?? pi.PropertyType)), nameGuid)));
                                
                                // Fixed value
                                if (!String.IsNullOrEmpty(pa.FixedValue) && pa.PropertyType != PropertyAttribute.AttributeAttributeType.Traversable)
                                    methodElements.Add(new CodeSnippetExpression(String.Format("if(!\"{1}\".Equals(d{0}.ToString())) resultContext.AddResultDetail(new MARC.Everest.Connectors.FixedValueMisMatchedResultDetail(d{0}.ToString(), \"{1}\", false, s.ToString()))", nameGuid, pa.FixedValue)));

                            }
                            else if (pi.PropertyType.GetMethod("Add") != null) // Collection...
                                methodElements.Add(new CodeSnippetExpression(String.Format("object d{2} = Host.ParseObject(s, typeof({1}), currentInteractionType, resultContext); instance.{0}.Add(d{2} is {1} ? ({1})d{2} : ({1})MARC.Everest.Connectors.Util.FromWireFormat(d{2}, typeof({1})))", pi.Name, CreateTypeReference(new CodeTypeReference(pi.PropertyType.GetGenericArguments()[0])), Guid.NewGuid().ToString("N"))));
                            else if (pi.GetSetMethod() != null && pi.PropertyType.GetMethod("ParseXml", BindingFlags.Public | BindingFlags.Static) != null)
                                methodElements.Add(new CodeSnippetExpression(String.Format("instance.{0} = {1}.ParseXml(s)", pi.Name, CreateTypeReference(new CodeTypeReference(pi.PropertyType)))));
                            else if (pi.GetSetMethod() != null && pi.PropertyType == typeof(string)) // Read content... 
                                methodElements.Add(new CodeSnippetExpression(String.Format("instance.{0} = s.ReadInnerXml();", pi.Name)));
                            else
                            {
                                Guid toGuid = Guid.NewGuid();
                                methodElements.Add(new CodeSnippetExpression(String.Format("object to{0:N} = Host.ParseObject(s, typeof({1}), currentInteractionType, resultContext)", toGuid, CreateTypeReference(new CodeTypeReference(pa.Type ?? pi.PropertyType)))));
                                if(pa.PropertyType != PropertyAttribute.AttributeAttributeType.Traversable)
                                    methodElements.Add(new CodeSnippetExpression(String.Format("if(to{0:N}.ToString() != instance.{1}.ToString()) resultContext.AddResultDetail(new MARC.Everest.Connectors.FixedValueMisMatchedResultDetail(to{0:N}.ToString(), instance.{1}.ToString(), true, s.ToString()))", toGuid, pi.Name)));
                            }

                            methodElements.Add(new CodeSnippetStatement("}"));

                        }
                    }
                }
            }
            //TODO: XmlReader r;

            // Didn't understand that element?
            methodElements.Add(new CodeSnippetStatement("else { resultContext.AddResultDetail(new MARC.Everest.Connectors.NotImplementedElementResultDetail(MARC.Everest.Connectors.ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null)); }"));

            // Finish method elements
            methodElements.Add(new CodeSnippetStatement("}")); // if s.NodeType ==
            methodElements.Add(new CodeSnippetStatement("}")); // Try
            methodElements.Add(new CodeSnippetStatement("catch (System.Exception e) { resultContext.AddResultDetail(new MARC.Everest.Connectors.ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, s.ToString(), e)); }"));
            methodElements.Add(new CodeSnippetStatement("finally { if(oldName.Equals(s.LocalName)) s.Read(); } }")); // while
            //methodElements.Add(new CodeSnippetExpression("Details = resultDetail.ToArray()"));

            method.Statements.AddRange(methodBuilder);
            method.Statements.AddRange(methodAttributes);
            method.Statements.AddRange(methodElements);
            method.Statements.Add(new CodeSnippetExpression("return instance"));

            return method;
        }

        /// <summary>
        /// Create the host property
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "forType")]
        private CodeTypeMember CreateHostProperty(Type forType)
        {
            CodeMemberProperty property = new CodeMemberProperty();

            // Find the structure attribute
            property.Name = "Host";
            property.HasGet = true;
            property.HasSet = true;
            property.GetStatements.Add(new CodeSnippetExpression("return host;"));
            property.SetStatements.Add(new CodeSnippetExpression("host = value;"));

            property.Attributes = MemberAttributes.Public;
            property.Type = new CodeTypeReference(typeof(XmlIts1Formatter));

            // return 
            return property;
        }

        /// <summary>
        /// Create the type formatter declaration
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void CreateTypeFormatter(object state)
        {
            CodeTypeDeclaration ctd = null;
            try
            {
                Type forType = (Type)state;
                if (forType.IsGenericType && forType.ContainsGenericParameters || forType.IsAbstract)
                    return;

                // Generate code type definition
                ctd = new CodeTypeDeclaration(string.Format("d{0}", Guid.NewGuid().ToString("N")));
                ctd.IsClass = true;
                ctd.TypeAttributes = System.Reflection.TypeAttributes.Public;
                ctd.BaseTypes.Add(new CodeTypeReference(typeof(ITypeFormatter)));

                // Add members
                CodeMemberField fld = new CodeMemberField();
                fld.Name = "details";
                fld.Attributes = MemberAttributes.Private;
                fld.Type = new CodeTypeReference(typeof(IResultDetail[]));
                ctd.Members.Add(fld);
                fld = new CodeMemberField();
                fld.Name = "host";
                fld.Attributes = MemberAttributes.Private;
                fld.Type = new CodeTypeReference(typeof(XmlIts1Formatter));
                ctd.Members.Add(fld);
                ctd.Members.Add(CreateHostProperty(forType));
                //ctd.Members.Add(CreateDetailProperty(forType));
                ctd.Members.Add(CreateGraphMethod(forType));
                ctd.Members.Add(CreateParseMethod(forType));
                ctd.Members.Add(CreateHandlesTypeProperty(forType));
                ctd.Members.Add(CreateValidateMethod(forType));
            }
            finally
            {
                // Return type definition
                if (CodeTypeDeclarationCompleted != null)
                    CodeTypeDeclarationCompleted(ctd);
            }
        }



    }
}
