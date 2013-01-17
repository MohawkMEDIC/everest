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
 * Date: 11-16-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using System.Text.RegularExpressions;
using MARC.Everest.Interfaces;
using System.Reflection;
using MARC.Everest.Exceptions;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Represents a formatter that can serialize a CD to the wire
    /// </summary>
    internal class CDFormatter : IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

        /// <summary>
        /// Creates an code expression for a coded value
        /// </summary>
        private string CreateCodeExpression(ICodedValue code)
        {
            StringBuilder expr = new StringBuilder();
            // Represent as IHTSDO format
            expr.AppendFormat("{0}", Util.ToWireFormat(code.CodeValue));
            //if (code.DisplayName != null)
            //    expr.AppendFormat("|{0}|", code.DisplayName);
            return expr.ToString();
        }

        /// <summary>
        /// Create code expression for the specified concept descriptor
        /// </summary>
        public string CreateCodeExpression(IConceptDescriptor code)
        {
            return CreateCodeExpression(code, true);
        }

        /// <summary>
        /// Creates a code expression following the rules 
        /// </summary>
        private string CreateCodeExpression(IConceptDescriptor code, bool isOwnSegment)
        {

            // Validate that all qualifiers come from the same code system
            string codeSystemOid = code.CodeSystem;
            foreach (IConceptQualifier cr in code.Qualifier)
                if (cr.Name.CodeSystem != codeSystemOid ||
                    cr.Value.CodeSystem != codeSystemOid)
                    throw new InvalidOperationException("The specified code cannot be represented as a code expression as all qualifiers must come from the same code system as the parent");

            // Expression builder
            StringBuilder expr = new StringBuilder(CreateCodeExpression(code as ICodedValue));

            // Create qualifiers
            if (code.Qualifier != null)
            {
                if (code.Qualifier.Count == 1)
                {
                    expr.AppendFormat(":{0}={1}", CreateCodeExpression((code.Qualifier[0] as IConceptQualifier).Name),
                        CreateCodeExpression((code.Qualifier[0] as IConceptQualifier).Value, false));
                }
                else if (code.Qualifier.Count > 1)
                {
                    if (isOwnSegment)
                        expr.Append(":{");
                    else
                        expr.Append("{");

                    foreach (IConceptQualifier itm in code.Qualifier)
                    {
                        string formatString = "{0}={1},";
                        if (itm.Value.Qualifier != null && itm.Value.Qualifier.Count > 0)
                            formatString = "{0}=({1}),";
                        expr.AppendFormat(formatString, CreateCodeExpression(itm.Name),
                            CreateCodeExpression(itm.Value, false));
                    }
                    expr.Remove(expr.Length - 1, 1);
                    expr.Append("}");
                }
            }
            return expr.ToString();
        }

        /// <summary>
        /// Parse IHTSDO expression code
        /// </summary>
        private CD<String> ParseCodeExpression(string expression, ICodedValue context)
        {
            if (expression.StartsWith("(") && expression.EndsWith(")"))
                expression = expression.Substring(1, expression.Length - 2);
            else if (expression.StartsWith("("))
                throw new InvalidOperationException();

            Regex tokenReg = new Regex(@"^([0-9]+)[|]?(.*?)[|]?([:{])(.*)$");
            var tokenMatch = tokenReg.Match(expression);

            if (!tokenMatch.Success)
                return new CD<string>(expression, context.CodeSystem, context.CodeSystemName, context.CodeSystemVersion)
                {
                    ValueSet = context.ValueSet,
                    ValueSetVersion = context.ValueSetVersion
                };

            // Create retVal
            CD<String> retVal = new CD<string>(tokenMatch.Groups[1].Value, context.CodeSystem, context.CodeSystemName, context.CodeSystemVersion)
            {
                DisplayName = String.IsNullOrEmpty(tokenMatch.Groups[2].Value) ? null : tokenMatch.Groups[2].Value,
                ValueSet = context.ValueSet,
                ValueSetVersion = context.ValueSetVersion
            };


            // Determine how the qualifier is identified
            var qualifierExpression = tokenMatch.Groups[4].Value;
            if (tokenMatch.Groups[3].Value == "{")
                qualifierExpression = "{" + qualifierExpression;
            retVal.Qualifier = ParseCodeExpressionQualifier(qualifierExpression, retVal);
            
            return retVal;
        }

        /// <summary>
        /// Parse IHTSDO Expression CR List
        /// </summary>
        private LIST<CR<String>> ParseCodeExpressionQualifier(string expression, ICodedValue context)
        {
            if (expression.StartsWith("{") && expression.EndsWith("}"))
                expression = expression.Substring(1, expression.Length - 2);
            else if (expression.StartsWith("{"))
                throw new InvalidOperationException();

            List<String> crExpressions = new List<string>(10);
            Regex splitReg = new Regex("^([0-9]*.*?)([=,{}():])(.*?)$", RegexOptions.Multiline);

            int bDepth = 0;
            string cExpr = String.Empty;
            while (expression.Length > 0)
            {
                var splitMatch = splitReg.Match(expression);

                // Successful match?
                if (!splitMatch.Success)
                    break;

                // Determine the delimiter
                switch (splitMatch.Groups[2].Value)
                {
                    case "{": // Increases scope
                    case "(":
                        bDepth++;
                        cExpr += splitMatch.Groups[1].Value + splitMatch.Groups[2].Value;
                        break;
                    case "}": // Decreases scope
                    case ")":
                        bDepth--;
                        cExpr += splitMatch.Groups[1].Value + splitMatch.Groups[2].Value;
                        break;
                    case ",": // Seperator
                        if (bDepth == 0)
                        {
                            cExpr += splitMatch.Groups[1].Value;
                            crExpressions.Add(cExpr);
                            cExpr = String.Empty;
                        }
                        else
                            cExpr += splitMatch.Groups[1].Value + splitMatch.Groups[2].Value;
                        break;
                    default:
                        cExpr += splitMatch.Groups[1].Value + splitMatch.Groups[2].Value;
                        break;
                }
                expression = splitMatch.Groups[3].Value;
            }

            // Last match?
            splitReg = new Regex("([0-9]+.*)");
            var lastMatch = splitReg.Match(expression);

            // Successful match?
            if (lastMatch.Success)
                cExpr += lastMatch.Groups[1].Value;

            // Depth
            if (bDepth > 1)
                throw new InvalidOperationException("Missing closing bracket in expression");

            crExpressions.Add(cExpr);

            // Prepare return value
            LIST<CR<String>> retVal = new LIST<CR<string>>(crExpressions.Count);

            // Now Process each expression
            splitReg = new Regex("^([0-9]+)[|]?(.*?)[|]?=([(]?.*)$", RegexOptions.Multiline);
            Regex codeMnemReg = new Regex("^([0-9]+)[|](.*?)[|]$", RegexOptions.Multiline);
            foreach (var expr in crExpressions)
            {
                // Each expression must match
                var exprMatch = splitReg.Match(expr);
                if (!exprMatch.Success)
                    throw new InvalidOperationException("Qualifier expression does not follow format of name=value");

                // Process the name
                CV<String> name = new CV<string>(exprMatch.Groups[1].Value, context.CodeSystem, context.CodeSystemName, context.CodeSystemVersion)
                {
                    DisplayName = String.IsNullOrEmpty(exprMatch.Groups[2].Value) ? null : exprMatch.Groups[2].Value,
                    ValueSet = context.ValueSet,
                    ValueSetVersion = context.ValueSetVersion
                };


                // Process value
                CD<String> value = null;
                decimal id;

                // Try to match the exact pattern of a code with description
                var codeMnemMatch = codeMnemReg.Match(exprMatch.Groups[3].Value);
                if (codeMnemMatch.Success)
                    value = new CD<string>(codeMnemMatch.Groups[1].Value, context.CodeSystem, context.CodeSystemName, context.CodeSystemVersion)
                    {
                        DisplayName = String.IsNullOrEmpty(codeMnemMatch.Groups[2].Value) ? null : codeMnemMatch.Groups[2].Value,
                        ValueSet = context.ValueSet,
                        ValueSetVersion = context.ValueSetVersion
                    };
                else if (decimal.TryParse(exprMatch.Groups[3].Value, out id)) // Simple code
                    value = new CD<string>(exprMatch.Groups[3].Value, context.CodeSystem, context.CodeSystemName, context.CodeSystemVersion)
                    {
                        ValueSet = context.ValueSet,
                        ValueSetVersion = context.ValueSetVersion
                    };
                else // IHTSDO expression tree
                {
                    string valueExpression = exprMatch.Groups[3].Value;
                    value = ParseCodeExpression(valueExpression, context);
                }

                retVal.Add(new CR<string>(name, value));
            }

            return retVal;
        }

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeR2FormatterGraphResult result)
        {
            // Base formatter
            ANYFormatter baseFormatter = new ANYFormatter();
            baseFormatter.Graph(s, o, result);

            // Next, start to format the properties
            ICodedSimple cs = o as ICodedSimple;
            ICodedValue cv = o as ICodedValue;
            ICodedEquivalents ce = o as ICodedEquivalents;
            IConceptDescriptor cd = o as IConceptDescriptor;

            ST displayName = cv.DisplayName;
            IAny any = o as IAny;

            // Unless "other" is specified don't serialize
            if (any.NullFlavor != null && !((NullFlavor)any.NullFlavor).IsChildConcept(NullFlavor.Other))
                return; 

            // First we need to serialize code, now Code in R2 is a little wierd
            // It is an IHTSDO standard as described:
            // http://www.ihtsdo.org/fileadmin/user_upload/Docs_01/Technical_Docs/abstract_models_and_representational_forms.pdf
            // Serialize attributes first
            if (cs.CodeValue != null)
                try
                {
                    if (cd != null && cd.Qualifier != null && cd.Qualifier.Count > 0)
                    {
                        s.WriteAttributeString("code", CreateCodeExpression(cd));
                        displayName = null;
                    }
                    else
                        s.WriteAttributeString("code", Util.ToWireFormat(cs.CodeValue));
                }
                catch (Exception e)
                {
                    result.AddResultDetail(new VocabularyIssueResultDetail(ResultDetailType.Error, e.Message, s.ToString(), e));
                }
            if (cv.CodeSystem != null)
                s.WriteAttributeString("codeSystem", Util.ToWireFormat(cv.CodeSystem));
            if (cv.CodeSystemName != null)
                s.WriteAttributeString("codeSystemName", Util.ToWireFormat(cv.CodeSystemName));
            if (cv.CodeSystemVersion != null)
                s.WriteAttributeString("codeSystemVersion", Util.ToWireFormat(cv.CodeSystemVersion));
            if (cv.ValueSet != null)
                s.WriteAttributeString("valueSet", Util.ToWireFormat(cv.ValueSet));
            if (cv.ValueSetVersion != null)
                s.WriteAttributeString("valueSetVersion", Util.ToWireFormat(cv.ValueSetVersion));
            if (cv.CodingRationale != null)
                s.WriteAttributeString("codingRationale", Util.ToWireFormat(cv.CodingRationale));

            // Elements
            
            // Display name
            if (displayName != null)
            {
                s.WriteStartElement("displayName", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, displayName as IGraphable);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }

            // Original text
            if (cv.OriginalText != null)
            {
                s.WriteStartElement("originalText", "urn:hl7-org:v3");
                var hostResult = this.Host.Graph(s, cv.OriginalText);
                result.Code = hostResult.Code;
                result.AddResultDetail(hostResult.Details);
                s.WriteEndElement();
            }

            // Translation
            if(ce != null && ce.Translation != null)
                foreach (var translation in ce.Translation)
                {
                    s.WriteStartElement("translation", "urn:hl7-org:v3");
                    var hostResult = this.Host.Graph(s, translation);
                    result.Code = hostResult.Code;
                    result.AddResultDetail(hostResult.Details);
                    s.WriteEndElement();
                }

            // Done
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <returns></returns>
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {

            if (String.IsNullOrEmpty(s.GetAttribute("flavorId")))
                return Parse<CD<String>>(s, result);
            else
            {
                IAny retVal = null;
                switch (s.GetAttribute("flavorId"))
                {
                    case "CD.CE":
                        retVal = Parse<CE<String>>(s, result);
                        retVal.Flavor = null;
                        break;
                    case "CD.CV":
                        retVal = Parse<CV<String>>(s, result);
                        retVal.Flavor = null;
                        break;
                    default:
                        retVal = Parse<CD<String>>(s, result);
                        break;
                }
                return retVal;
            }
        }

        /// <summary>
        /// Get the type that this formatter helper handles
        /// </summary>
        public string HandlesType
        {
            get { return "CD"; }
        }

        /// <summary>
        /// Gets or sets the host 
        /// </summary>
        public MARC.Everest.Connectors.IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets or sets the generic arguments
        /// </summary>
        public Type[] GenericArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Get the supported properties
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            var retVal = new List<PropertyInfo>(new CEFormatter().GetSupportedProperties());
            retVal.AddRange(typeof(CD<>).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));
            return retVal;
        }

        #endregion

        /// <summary>
        /// Parse a CD as a particular type
        /// </summary>
        internal T Parse<T>(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result) where T : ANY, ICodedValue, new()
        {
            // Instantiate the genericized T
            ANYFormatter baseFormatter = new ANYFormatter();
            T retVal = baseFormatter.Parse<T>(s);

            if (s.GetAttribute("codeSystem") != null)
                retVal.CodeSystem = s.GetAttribute("codeSystem");
            if (s.GetAttribute("codeSystemName") != null)
                retVal.CodeSystemName = s.GetAttribute("codeSystemName");
            if (s.GetAttribute("codeSystemVersion") != null)
                retVal.CodeSystemVersion = s.GetAttribute("codeSystemVersion");
            if (s.GetAttribute("valueSet") != null)
                retVal.ValueSet = s.GetAttribute("valueSet");
            if (s.GetAttribute("valueSetVersion") != null)
                retVal.ValueSetVersion = s.GetAttribute("valueSetVersion");
            // Now parse our data out... Attributes
            if (s.GetAttribute("code") != null)
            {
                string codeString = s.GetAttribute("code");
                if (retVal is IConceptDescriptor)
                {
                    var cdExpr = ParseCodeExpression(codeString, retVal);
                    retVal.CodeValue = cdExpr.Code;
                    retVal.DisplayName = cdExpr.DisplayName;
                    if(cdExpr.Qualifier != null && !cdExpr.Qualifier.IsEmpty)
                        (retVal as IConceptDescriptor).Qualifier = new LIST<IGraphable>(cdExpr.Qualifier);
                }
                else
                    retVal.CodeValue = codeString;
            }
                        
            // Elements
            #region Elements
            if (!s.IsEmptyElement)
            {

                int sDepth = s.Depth;
                string sName = s.Name;
                LIST<IGraphable> translations = new LIST<IGraphable>(10);

                s.Read();
                // string Name
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {
                        if (s.LocalName == "originalText") // Format using ED
                        {
                            var hostResult = this.Host.Parse(s, typeof(ED));
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            retVal.OriginalText = hostResult.Structure as ED;
                        }
                        else if (s.LocalName == "displayName") // display name
                        {
                            var hostResult = this.Host.Parse(s, typeof(ST));
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            retVal.DisplayName = hostResult.Structure as ST;
                        }
                        else if (s.LocalName == "translation") // Translation
                        {
                            var hostResult = Host.Parse(s, typeof(CD<String>));
                            result.Code = hostResult.Code;
                            result.AddResultDetail(hostResult.Details);
                            translations.Add(hostResult.Structure);
                        }
                        else if (s.NodeType == System.Xml.XmlNodeType.Element)
                            result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
                    }
                    catch (VocabularyException e)
                    {
                        result.AddResultDetail(new VocabularyIssueResultDetail(ResultDetailType.Error, e.Message, e));
                    }
                    catch (MessageValidationException e)
                    {
                        result.AddResultDetail(new MARC.Everest.Connectors.ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, s.ToString(), e));
                    }
                    finally
                    {
                        if (s.Name == oldName) s.Read();
                    }
                }

                // Translations
                if (!translations.IsEmpty)
                {
                    if (retVal is ICodedEquivalents)
                        (retVal as ICodedEquivalents).Translation = translations;
                    else
                        result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Warning, String.Format("Type does not support translations, '{0}' translations will not be included", translations.Count), s.ToString(), null));
                }

            }
            #endregion


            // Validate
            baseFormatter.Validate(retVal, s.ToString(), result);


            // Add validation to details
            return retVal;
        }
    }
}
