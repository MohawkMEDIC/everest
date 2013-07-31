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
 * Date: 17-11-2009
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.Util.SimpleXSD;
using MohawkCollege.EHR.gpmr.COR;
using System.Xml;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.XML.ITS
{
    /// <summary>
    /// The XSLT generator class is responsible for generating XSLTs that will inflate and deflate
    /// collapse models. The majority of these XSLT will use inline processes, however it may be 
    /// proper to create templates to recurse down the tree.
    /// </summary>
    public class XsltGenerator
    {
        /// <summary>
        /// Gets or sets the data types XSD
        /// </summary>
        public List<XmlSchemaType> DataTypesXsd { get; set; }
        
        /// <summary>
        /// Output directory
        /// </summary>
        public string OutputDir { get; set; }

        const string NS_XSLT = "http://www.w3.org/1999/XSL/Transform";
        const string NS_SRC = "urn:hl7-org:v3";

        // Maps for the data types
        static Dictionary<String, XmlElement> dataTypeMaps = new Dictionary<String, XmlElement>();

        // Needed templates
        private List<String> requiredTemplates = new List<String>();

        /// <summary>
        /// Target namespace of the converted items in the collapsing XSLT
        /// </summary>
        public string TargetNamespace { get; set; }

        /// <summary>
        /// Generate the collapsing XSLT
        /// </summary>
        public void GenerateCollapsingXslt(Interaction interaction)
        {

            requiredTemplates = new List<String>();

            // Create the writer
            XmlWriterSettings xwsettings = new XmlWriterSettings() { 
                Indent = true
            };

            XmlWriter xw = null;

            if (dataTypeMaps.Count == 0) // Generate the data type maps
                GenerateDataTypeMaps();

            // Try to open the file
            try
            {
                xw = XmlWriter.Create(Path.Combine(OutputDir, String.Format("{0}_Collapse.xslt", interaction.Name)), xwsettings);

                // Write the template
                xw.WriteStartElement("xsl", "stylesheet", NS_XSLT);
                xw.WriteAttributeString("xmlns", "hl7", null, NS_SRC); // HACK: Declare a namespace without using it
                xw.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                xw.WriteAttributeString("exclude-result-prefixes", "hl7 xsl xsi");
                xw.WriteAttributeString("version", "1.0");

                #region Output Method
                xw.WriteStartElement("output", NS_XSLT);
                xw.WriteAttributeString("method", "xml");
                xw.WriteAttributeString("indent", "yes");
                xw.WriteEndElement(); // output method
                #endregion

                #region Root Template

                xw.WriteStartElement("template", NS_XSLT);
                xw.WriteAttributeString("match", String.Format("/hl7:{0}", interaction.Name));
                xw.WriteStartElement(interaction.Name, TargetNamespace);

                // Write the attributes
                WriteXslAttribute(xw, "ITSVersion", string.Format("'{0}'", ItsRenderer.itsString), TargetNamespace);

                WriteInlineForeachCollapse(xw, interaction.MessageType, interaction.MessageType.GenericSupplier, interaction.Name);

                xw.WriteEndElement(); // interaction.Name
                xw.WriteEndElement(); // end template

                #endregion

                #region Other Templates

                for (int i = 0; i < requiredTemplates.Count; i++ )
                {
                    string s = requiredTemplates[i];
                    xw.WriteStartElement("template", NS_XSLT);
                    xw.WriteAttributeString("name", s);

                    Class cls = interaction.MemberOf[s] as Class;
                    WriteInlineForeachCollapse(xw, cls.CreateTypeReference(), interaction.MessageType.GenericSupplier, interaction.Name);
                    xw.WriteEndElement();
                }

                #endregion

                xw.WriteEndElement(); // stylesheet

                //            // Write the 
                //<?xml version="1.0" encoding="utf-8"?>
                //<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                //    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                //>
                //    <xsl:output method="xml" indent="yes"/>

                //    <xsl:template match="@* | node()">
                //        <xsl:copy>
                //            <xsl:apply-templates select="@* | node()"/>
                //        </xsl:copy>
                //    </xsl:template>
                //</xsl:stylesheet>

                
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Could not write to the file '{0}_Collapse' ({1})...", interaction.Name, e.Message), "error");
            }
            finally
            {
                if (xw != null) xw.Close();
            }

        }

        /// <summary>
        /// Create the data type maps 
        /// </summary>
        private void GenerateDataTypeMaps()
        {
            XmlDocument context = new XmlDocument();

            if (TargetNamespace == NS_SRC) // Source and target are the same ... 
            {
                foreach (XmlSchemaType xst in DataTypesXsd)
                {

                    // Only complex types
                    if (!(xst is XmlSchemaComplexType)) continue;
                    // Create elements
                    XmlElement foreachElement = context.CreateElement("for-each", NS_XSLT),
                        copyOfElement = context.CreateElement("copy-of", NS_XSLT);
                    XmlAttribute foreachSelect = context.CreateAttribute("select"),
                        copyOfSelect = context.CreateAttribute("select");
                    foreachSelect.Value = "@* | child::node()";
                    copyOfSelect.Value = ".";

                    // Append the attributes
                    foreachElement.Attributes.Append(foreachSelect);
                    copyOfElement.Attributes.Append(copyOfSelect);
                    foreachElement.AppendChild(copyOfElement);

                    //XmlElement copy = context.CreateElement("copy");
                    //copy.InnerText = ".";
                    if(xst.Name != null)
                        dataTypeMaps.Add(xst.Name, foreachElement);
                }
            }
        }

        /// <summary>
        /// Write an XSL Attribute element
        /// </summary>
        private void WriteXslAttribute(XmlWriter xw, string attributeName, string attributeXpath, string attributeNamespace)
        {
            xw.WriteStartElement("attribute", NS_XSLT);
            xw.WriteAttributeString("name", attributeName);
            xw.WriteStartElement("value-of", NS_XSLT);
            xw.WriteAttributeString("select", attributeXpath);
            xw.WriteEndElement(); // xs:value-of
            xw.WriteEndElement(); // xs:attribute
        }

        /// <summary>
        /// Generate an inline foreach query pattern
        /// </summary>
        private void WriteInlineForeachCollapse(XmlWriter xw, TypeReference tr, List<TypeReference> genericSupplier, string interactionName)
        {

            // Clone the class
            Class clsTr = tr.Class.Clone() as Class;
            clsTr.MemberOf = tr.Class.MemberOf;
            

            // Correct the inheritence
            if (clsTr.BaseClass == null && clsTr.Name != "InfrastructureRoot")
                clsTr.BaseClass = (clsTr.MemberOf["RIM.InfrastructureRoot"] as Class).CreateTypeReference();

            List<Property> elements = FlattenPropertyHierarchy(clsTr.GetFullContent());

            // Write any attributes
            xw.WriteStartElement("for-each", NS_XSLT);
            xw.WriteAttributeString("select", String.Format("@* | child::node()[namespace-uri() = '{0}']", NS_SRC));
            xw.WriteStartElement("choose", NS_XSLT);

            // Write the element data
            foreach(Property p in elements)
                WriteInlineWhenConditionCollapse(xw, p, genericSupplier, interactionName);


            xw.WriteEndElement(); // choose
            xw.WriteEndElement(); // foreach
        }

        /// <summary>
        /// Write inline when condition
        /// </summary>
        private void WriteInlineWhenConditionCollapse(XmlWriter xw, Property p, List<TypeReference> genericSupplier, string interactionName)
        {
            // Resolve the type reference
            TypeReference tr = p.Type;
            if (ResolveTypeReference(tr.Name, genericSupplier) != null)
                tr = ResolveTypeReference(p.Type.Name, genericSupplier);


            // Annotations
            if (p.Annotations != null && p.Annotations.Count > 0)
            {
                // Get the root of the collapsing
                CodeCollapseAnnotation cca = p.Annotations.Find(o => o is CodeCollapseAnnotation && (o as CodeCollapseAnnotation).Order == 0) as CodeCollapseAnnotation;
                xw.WriteStartElement("when", NS_XSLT);
                xw.WriteAttributeString("test", string.Format("name() = '{0}'", cca.Name));
                xw.WriteStartElement("for-each", NS_XSLT);

                string xpath = ".";
                // Annotations
                foreach (CodeCollapseAnnotation annot in p.Annotations.FindAll(o => o is CodeCollapseAnnotation && (o as CodeCollapseAnnotation).Order > 0))
                {
                    if (annot.Property != null && (annot.Property as Property).AlternateTraversalNames != null)
                    {
                        Property.AlternateTraversalData altTraversal = (annot.Property as Property).AlternateTraversalNames.Find(o => o.CaseWhen.Name == ResolveTypeReference(p.Type.Name, genericSupplier).Name && o.InteractionOwner.Name == interactionName);
                        if (altTraversal.TraversalName != null)
                            xpath += string.Format("/hl7:{0}", altTraversal.TraversalName);
                    }
                    else
                        xpath += string.Format("/hl7:{0}", annot.Name);
                }

                xw.WriteAttributeString("select", xpath);

                // Inline element map collapse
                WriteInlineElementMapCollapse(xw, p, p.Type, genericSupplier, interactionName);

                xw.WriteEndElement(); // for-each
                xw.WriteEndElement(); // test
            }
            else if (p.AlternateTraversalNames == null || p.AlternateTraversalNames.Count == 0) // Alternate traversal names
            {
                xw.WriteStartElement("when", NS_XSLT);
                xw.WriteAttributeString("test", String.Format("name() = '{0}'", p.Name));

                // Write structural property
                if (p.PropertyType == Property.PropertyTypes.Structural)
                    WriteInlineAttributeMapCollapse(xw, p);
                else
                    WriteInlineElementMapCollapse(xw, p, p.Type, genericSupplier, interactionName);

                xw.WriteEndElement(); // when
            }
            else if (tr.Class != null && tr.Class.IsAbstract)
                WriteInlineAbstractWhenCondition(xw, tr, p, genericSupplier, interactionName);
            else
            {
                xw.WriteStartElement("when", NS_XSLT);

                // Find the altTraversal
                Property.AlternateTraversalData altTraversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == tr.Name); // Alternate traversal

                // Alternate traversal
                if (altTraversal.TraversalName != null)
                    xw.WriteAttributeString("test", string.Format("name() = '{0}'", altTraversal.TraversalName));
                else
                    xw.WriteAttributeString("test", string.Format("name() = '{0}'", p.Name));

                // Write the taversing
                WriteInlineElementMapCollapse(xw, p, p.Type, genericSupplier, interactionName);

                xw.WriteEndElement();
            }
        }

        /// <summary>
        /// Write an inline abstract when condition
        /// </summary>
        private void WriteInlineAbstractWhenCondition(XmlWriter xw, TypeReference tr, Property p, List<TypeReference> genericSupplier, string interactionName)
        {
            foreach (TypeReference child in tr.Class.SpecializedBy)
            {

                if (child.Class.IsAbstract)
                    WriteInlineAbstractWhenCondition(xw, child, p, genericSupplier, interactionName);
                else
                {
                    xw.WriteStartElement("when", NS_XSLT);

                    // Find the altTraversal
                    Property.AlternateTraversalData altTraversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == child.Name); // Alternate traversal

                    // Traversal was found
                    if (altTraversal.TraversalName != null)
                        xw.WriteAttributeString("test", string.Format("name() = '{0}'", altTraversal.TraversalName));
                    else
                        throw new InvalidOperationException(String.Format("Can't find traversal for alternative data for '{0}' when type is '{1}'...", p.Name, child.Name));

                    // Perform the inline element map
                    if (p.AlternateTraversalNames == null || p.AlternateTraversalNames.Count == 0)
                        xw.WriteStartElement(p.Name, TargetNamespace); // p.name
                    else
                    {
                        Property.AlternateTraversalData traversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == child.Name);
                        if (traversal.TraversalName != null) // found an alt traversal
                            xw.WriteStartElement(traversal.TraversalName, TargetNamespace);
                        else
                            xw.WriteStartElement(p.Name, TargetNamespace); // fall back to the property name
                    }
                    WriteInlineApplyTemplate(xw, p, child, genericSupplier);
                    xw.WriteEndElement(); // p.name
                    xw.WriteEndElement(); // when
                }
            }
        }

        /// <summary>
        /// Write inline element map collapse
        /// </summary>
        private void WriteInlineElementMapCollapse(XmlWriter xw, Property p, TypeReference tr, List<TypeReference> genericSupplier, string interactionName)
        {
            if (ResolveTypeReference(tr.Name, genericSupplier) != null)
                tr = ResolveTypeReference(p.Type.Name, genericSupplier);

            if (p.AlternateTraversalNames == null || p.AlternateTraversalNames.Count == 0)
                xw.WriteStartElement(p.Name, TargetNamespace); // p.name
            else
            {
                Property.AlternateTraversalData traversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == tr.Name);
                if (traversal.TraversalName != null) // found an alt traversal
                    xw.WriteStartElement(traversal.TraversalName, TargetNamespace);
                else
                    xw.WriteStartElement(p.Name, TargetNamespace); // fall back to the property name
            }

            // Determine if this should be a genericized parameter
            WriteInlineTypeMap(xw, tr, genericSupplier, interactionName);
            xw.WriteEndElement(); // p.name
        
        }

        /// <summary>
        /// Write inline apply template call
        /// </summary>
        private void WriteInlineApplyTemplate(XmlWriter xw, Property p, TypeReference tr, List<TypeReference> genericSupplier)
        {
            if(!requiredTemplates.Contains(tr.Name))
                requiredTemplates.Add(tr.Name);

            // Write the xsl:apply-template
            xw.WriteStartElement("call-template", NS_XSLT);
            xw.WriteAttributeString("name", tr.Name);
            xw.WriteEndElement(); // call-template
        }

        /// <summary>
        /// Resolve the type reference
        /// </summary>
        private TypeReference ResolveTypeReference(string parameterName, List<TypeReference> genericParameters)
        {
            TypeReference retVal = null;
            foreach (TypeReference tr in genericParameters ?? new List<TypeReference>())
                if (tr is TypeParameter && (tr as TypeParameter).ParameterName == parameterName)
                    return tr;
                else if (tr.GenericSupplier != null)
                    retVal = ResolveTypeReference(parameterName, tr.GenericSupplier);
            return retVal;
        }

        /// <summary>
        /// Write an inline type mapping for the TypeReference specified
        /// </summary>
        private void WriteInlineTypeMap(XmlWriter xw, TypeReference tr, List<TypeReference> genericSupplier, string interactionName)
        {
            List<String> collectionTypes = new List<string>(){ "SET","LIST","QSET","DSET","HIST","BAG" };

            if (collectionTypes.Contains(tr.CoreDatatypeName ?? "") && tr.GenericSupplier.Count == 1) // Collection
                tr = tr.GenericSupplier[0];
            else if (collectionTypes.Contains(tr.CoreDatatypeName ?? ""))
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Can't handle a collection ({0}) with more than one generic parameter", tr.ToString()), "warn");
                return;
            }

            string schemaFriendlyName = MakeSchemaFriendlyCoreType(tr);

            // Map the type
            if (dataTypeMaps.ContainsKey(schemaFriendlyName ?? "")) // simple map
            {
                if (dataTypeMaps[schemaFriendlyName ?? ""].Name == "copy")
                    xw.WriteElementString(dataTypeMaps[schemaFriendlyName ?? ""].Name, NS_XSLT, dataTypeMaps[schemaFriendlyName ?? ""].InnerText);
                else
                    dataTypeMaps[schemaFriendlyName ?? ""].WriteTo(xw);
            }
            else if (IsRecursiveReference(tr, tr, new List<string>())) // complex map
                WriteInlineApplyTemplate(xw, tr.Container as Property, tr, genericSupplier);
            else if (tr.Class != null)
                WriteInlineForeachCollapse(xw, tr, genericSupplier, interactionName);
            //else
                //System.Diagnostics.Debugger.Break();
        }

        /// <summary>
        /// Make a schema friendly CORE datatype name (ie: Convert IVL&lt;TS&gt; to IVL_TS)
        /// </summary>
        /// <param name="typeReference"></param>
        /// <returns></returns>
        private string MakeSchemaFriendlyCoreType(TypeReference typeReference)
        {
            string retVal = typeReference.CoreDatatypeName;
            foreach (TypeReference tr in typeReference.GenericSupplier ?? new List<TypeReference>())
                retVal += "_" + tr.CoreDatatypeName;
            return retVal;
        }

        /// <summary>
        /// Is recursive reference
        /// </summary>
        private bool IsRecursiveReference(TypeReference needle, TypeReference haystack, List<String> alreadyChecked)
        {
            
            // Already checked?
            if (alreadyChecked.Contains(haystack.Name)) 
                return false;
            else
                alreadyChecked.Add(haystack.Name);

            if (haystack.Class == null) return false;
            
            bool isRecursive = false;
            foreach (Property cc in FlattenPropertyHierarchy(haystack.Class.GetFullContent()))
            {
                if (!alreadyChecked.Contains(cc.Type.Name))
                    isRecursive |= IsRecursiveReference(needle, cc.Type, alreadyChecked);
                else if (cc.Type.Name == needle.Name)
                    isRecursive |= true;
                if (isRecursive) break; // no need to check anymore
            }
            return isRecursive || (needle.Name == haystack.Name && needle.Container != haystack.Container);
        }

        /// <summary>
        /// Flatten the property hierarchy
        /// </summary>
        private List<Property> FlattenPropertyHierarchy(List<ClassContent> content)
        {
            List<Property> retVal = new List<Property>();
            foreach (ClassContent cc in content)
                if (cc is Property)
                    retVal.Add(cc as Property);
                else if (cc is Choice) // Single level of choices supported so we flatten
                    retVal.AddRange(FlattenPropertyHierarchy((cc as Choice).Content));
            return retVal;
        }

        /// <summary>
        /// Write an inline attribute map collapse
        /// </summary>
        /// <param name="xw">The xml writer to write to</param>
        /// <param name="p">The property to write the map for</param>
        private void WriteInlineAttributeMapCollapse(XmlWriter xw, Property p)
        {
            // Any annotations telling us of the collapse
            if (p.Annotations == null || p.Annotations.Count == 0) // No fancy stuff
            {
                xw.WriteStartElement("attribute", NS_XSLT);
                xw.WriteAttributeString("name", p.Name);
                xw.WriteStartElement("value-of", NS_XSLT);
                xw.WriteAttributeString("select", ".");
                xw.WriteEndElement(); // value-of
                xw.WriteEndElement(); // attribute
            }
            else
                System.Diagnostics.Debugger.Break();

        }

        /// <summary>
        /// Generate the expanding XSLT
        /// </summary>
        public void GenerateExpandingXslt(Interaction interaction)
        {

        }
    }
}
