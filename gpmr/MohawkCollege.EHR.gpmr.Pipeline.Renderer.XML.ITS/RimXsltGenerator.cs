using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.Util.SimpleXSD;
using System.Xml;
using MohawkCollege.EHR.gpmr.COR;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.XML.ITS
{
    /// <summary>
    /// Generate an XSLT that represents this as a RIM XML
    /// </summary>
    public class RimXsltGenerator
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

        // Template classes
        Dictionary<String, Feature> templateClasses = new Dictionary<string, Feature>();

        // Maps for the data types
        static Dictionary<String, XmlElement> dataTypeMaps = new Dictionary<String, XmlElement>();

        // Needed templates
        private List<String> requiredTemplates = new List<String>();

        /// <summary>
        /// Generate the RIM parsing operation
        /// </summary>
        /// <param name="interaction"></param>
        public void GenerateRIMParse(Interaction interaction)
        {

            templateClasses.Clear();

            // Verify the interaction can be used
            if (interaction.MessageType == null || interaction.MessageType.Class == null ||
               interaction.MessageType.Class.Realizations == null ||
               interaction.MessageType.Class.Realizations.Count == 0)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Interaction '{0}' does not have a RIM realization...", interaction.Name), "error");
                return;
            }

            requiredTemplates = new List<String>();

            // Create the writer
            XmlWriterSettings xwsettings = new XmlWriterSettings()
            {
                Indent = true
            };

            XmlWriter xw = null;

            if (dataTypeMaps.Count == 0) // Generate the data type maps
                GenerateDataTypeMaps();

            // Try to open the file
            try
            {
                xw = XmlWriter.Create(Path.Combine(OutputDir, String.Format("{0}_RimParse.xslt", interaction.Name)), xwsettings);

                // Write the template
                xw.WriteStartElement("xsl", "stylesheet", NS_XSLT);
                xw.WriteAttributeString("xmlns", "hl7", null, NS_SRC); // HACK: Declare a namespace without using it
                xw.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                xw.WriteAttributeString("xmlns", "marc", null, "urn:marc-hi:ca/hial");
                xw.WriteAttributeString("exclude-result-prefixes", "hl7 xsl");
                xw.WriteAttributeString("version", "1.0");

                #region Output Method
                xw.WriteStartElement("output", NS_XSLT);
                xw.WriteAttributeString("method", "xml");
                xw.WriteAttributeString("indent", "yes");
                xw.WriteEndElement(); // output method
                #endregion

                #region Root Template

                xw.WriteStartElement("template", NS_XSLT);
                xw.WriteAttributeString("match", String.Format("/hl7:Message[./hl7:interactionId/@extension = '{0}']", interaction.Name));
                xw.WriteStartElement(interaction.Name, NS_SRC);
                xw.WriteStartElement("attribute", NS_XSLT);
                xw.WriteAttributeString("name", "ITSVersion");
                xw.WriteStartElement("value-of", NS_XSLT);
                xw.WriteAttributeString("select", "'XML_1.0'");
                xw.WriteEndElement();// value-of
                xw.WriteEndElement(); // attribute

                // Write the attributes
                WriteInlineForeachExpand(xw, interaction.MessageType, interaction.MessageType.GenericSupplier, interaction.Name);

                xw.WriteEndElement(); // interaction.Name
                xw.WriteEndElement(); // end template

                #endregion

                #region Other Templates

                #region DT Copy Template
                xw.WriteStartElement("template", NS_XSLT);
                xw.WriteAttributeString("name", "dtMap");
                xw.WriteStartElement("for-each", NS_XSLT);
                xw.WriteAttributeString("select", "@*|child::node()");
                xw.WriteStartElement("copy-of", NS_XSLT);
                xw.WriteAttributeString("select", ".");
                xw.WriteEndElement(); // copy
                xw.WriteEndElement(); // for-each
                xw.WriteEndElement(); // template
                #endregion

                for (int i = 0; i < requiredTemplates.Count; i++)
                {
                    string s = requiredTemplates[i];
                    xw.WriteStartElement("template", NS_XSLT);
                    xw.WriteAttributeString("name", s);

                    xw.WriteStartElement("attribute", NS_XSLT);
                    xw.WriteAttributeString("name", "type");
                    xw.WriteAttributeString("namespace", "urn:marc-hi:ca/hial");
                    xw.WriteString(s);
                    xw.WriteEndElement(); // attribute

                    Class cls = templateClasses[s] as Class;
                    WriteInlineForeachExpand(xw, cls.CreateTypeReference(), interaction.MessageType.GenericSupplier, interaction.Name);
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
                System.Diagnostics.Trace.WriteLine(String.Format("Could not write to the file '{0}_RimGraph' ({1})...", interaction.Name, e.Message), "error");
            }
            finally
            {
                if (xw != null) xw.Close();
            }

        }

        /// <summary>
        /// Write an inline for-each element that expands
        /// </summary>
        private void WriteInlineForeachExpand(XmlWriter xw, TypeReference tr, List<TypeReference> genericSupplier, string interactionName)
        {
            // Clone the class
            Class clsTr = tr.Class.Clone() as Class;
            clsTr.MemberOf = tr.Class.MemberOf;


            // Correct the inheritence
            if (clsTr.BaseClass == null && clsTr.Name != "InfrastructureRoot")
                clsTr.BaseClass = (clsTr.MemberOf["RIM.InfrastructureRoot"] as Class).CreateTypeReference();

            List<ClassContent> elements = clsTr.GetFullContent();

            // Write any attributes

            // JF- To Support proper ordering
            //xw.WriteStartElement("for-each", NS_XSLT);
            //xw.WriteAttributeString("select", String.Format("@* | child::node()[namespace-uri() = '{0}']", NS_SRC));

            //xw.WriteStartElement("choose", NS_XSLT);

            int nc = 0;
            // Write the element data
            foreach (ClassContent cc in elements)
            {
                if (cc is Property)
                {
                    Property p = cc as Property;
                    nc += Convert.ToUInt16(WriteInlineWhenConditionExpand(xw, p, genericSupplier, interactionName));
                }
                else if (cc is Choice) // JF: Bug with choice selection
                {
                    Choice c = cc as Choice;
                    WriteInlineChooseWhenExpand(xw, c, genericSupplier, interactionName);
                }

            }
            // HACK: As an XS:Choose element must have at least one when element
            //if (nc == 0)
            //{
            //    xw.WriteStartElement("when", NS_XSLT);
            //    xw.WriteAttributeString("test", "1=2");
            //    xw.WriteEndElement(); // when
            //}
         


            // JF- To support proper ordering
            //xw.WriteEndElement(); // choose
            //xw.WriteEndElement(); // foreach
        }

        /// <summary>
        /// Write an inline condition for a when expand
        /// </summary>
        private bool WriteInlineChooseWhenExpand(XmlWriter xw, Choice c, List<TypeReference> genericSupplier, string interactionName)
        {
            Property rimName = null;
            if (c.Realization != null && c.Realization.Count > 0)
                rimName = c.Realization[0] as Property;
            else
            {
                xw.WriteComment(String.Format("TODO: Can't realize property '{0}' in the RIM as no realization data was found", c.Name));
                //rimName = p;
                return false;
            }

            List<Property> properties = FlattenPropertyHierarchy(c.Content);

            // Write the for-each 
            xw.WriteStartElement("for-each", NS_XSLT);
            string s = String.Format("./*[local-name() = '{0}' {1}]", rimName.Name, GenerateConditionString(rimName,  ResolveTypeReference(rimName.Type.Name, genericSupplier), ".", genericSupplier));

            xw.WriteAttributeString("select", s);
            xw.WriteStartElement("choose", NS_XSLT);

            foreach (var property in properties)
            {
                xw.WriteStartElement("when", NS_XSLT);

                TypeReference tr = property.Type;
                if (ResolveTypeReference(tr.Name, genericSupplier) != null)
                    tr = ResolveTypeReference(property.Type.Name, genericSupplier);

                // Find any ambiguous test conditions
                s = GenerateDeepConditionString(property, tr, new List<string>(), genericSupplier);

                xw.WriteAttributeString("test", String.Format("{0}", s));

                // Create transform
                if (property.PropertyType == Property.PropertyTypes.Structural)
                    WriteInlineAttributeMapExpand(xw, property);
                else
                    WriteInlineElementMapExpand(xw, property, tr, genericSupplier, interactionName);

                xw.WriteEndElement(); // when
            }

            xw.WriteEndElement(); // end choose
            xw.WriteEndElement(); // end for-each

            return true;
        }


        /// <summary>
        /// Write inline when condition
        /// </summary>
        private bool WriteInlineWhenConditionExpand(XmlWriter xw, Property p, List<TypeReference> genericSupplier, string interactionName)
        {

            // Resolve the type reference
            TypeReference tr = p.Type;
            if (ResolveTypeReference(tr.Name, genericSupplier) != null)
                tr = ResolveTypeReference(p.Type.Name, genericSupplier);

            // Fixed value
            if (p.FixedValue != null && p.PropertyType == Property.PropertyTypes.Structural) return false;

            // Get the RIM Realization
            Property rimName = null;

            // Is this representative of some sort of RIM structure?
            if ((p.Container is Class) && (p.Container as Class).ContainerName == "RIM") // This is a rim property
                rimName = p;
            else if (p.Container is Choice && (p.Container as Choice).Realization != null &&
                (p.Container as Choice).Realization.Count > 0)
                rimName = (p.Container as Choice).Realization[0] as Property;
            else if (p.Realization == null || p.Realization.Count == 0)
            {
                xw.WriteComment(String.Format("TODO: Can't realize property '{0}' in the RIM as no realization data was found", p.Name));
                //rimName = p;
                return false;
            }
            else
                rimName = p.Realization[0] as Property;

            if (tr.Class != null && tr.Class.IsAbstract)
                WriteInlineAbstractWhenConditionExpand(xw, tr, p, genericSupplier, interactionName);
            else
            {
                // Get all fixed values that could identify this item
                List<Property> elements = tr.Class == null ? new List<Property>() : FlattenPropertyHierarchy(tr.Class.GetFullContent()),
                    containerElements = p.Container is Class ? FlattenPropertyHierarchy((p.Container as Class).Content) : p.Container != null ? FlattenPropertyHierarchy((p.Container as Choice).Content) : new List<Property>();
                // Start the when condition

                xw.WriteStartElement("for-each", NS_XSLT);
                // JF- Support ordering
                //xw.WriteStartElement("when", NS_XSLT);

                // Calculate the test condition
                string s = String.Format("local-name() = '{0}' {1}", rimName.Name, GenerateConditionString(p, tr, ".", genericSupplier));

                // Find any ambiguous test conditions
                List<Property> ambiguousRimNames = containerElements.FindAll(o => o.Realization != null && o.Realization.Count > 0 && String.Format("local-name() = '{0}' {1}", o.Realization[0].Name, GenerateConditionString(o, null, ".", genericSupplier)) == s);

                if (ambiguousRimNames.Count > 1 || p.Container is Choice)
                    s = GenerateDeepConditionString(p, tr, new List<string>(), genericSupplier);


                // JF - Support attributes
                if (rimName.PropertyType == Property.PropertyTypes.Structural)
                    xw.WriteAttributeString("select", String.Format("@{0}", rimName.Name));
                else
                    xw.WriteAttributeString("select", String.Format("./*[{0}]", s));

                // JF- Support ordering
                //xw.WriteAttributeString("test", s);

                if (p.PropertyType == Property.PropertyTypes.Structural)
                    WriteInlineAttributeMapExpand(xw, p);
                else
                    WriteInlineElementMapExpand(xw, p, tr, genericSupplier, interactionName);

                xw.WriteEndElement(); // when
            }
            return true;
        }

        /// <summary>
        /// Generates a deep condition string for the specified property and type reference
        /// </summary>
        private string GenerateDeepConditionString(Property p, TypeReference tr, List<string> properties, List<TypeReference> genericSupplier)
        {

            // Get the RIM Realization
            Property rimName = null;

            // Is this representative of some sort of RIM structure?
            if ((p.Container is Class) && (p.Container as Class).ContainerName == "RIM") // This is a rim property
                rimName = p;
            else if (p.Container is Choice && (p.Container as Choice).Realization != null &&
                (p.Container as Choice).Realization.Count > 0)
                rimName = (p.Container as Choice).Realization[0] as Property;
            else if (p.Realization == null || p.Realization.Count == 0)
                return "";
            else
                rimName = p.Realization[0] as Property;

            // Get fixed elements
            List<Property> fixedElements = FlattenPropertyHierarchy(tr.Class.Content).FindAll(o => o.Realization != null && (o.MinOccurs != "0" || o.FixedValue != null));

            string retVal = "", root = "", currentLevelSelector = "";
            if (properties.Count == 0) // Initial node
            {
                retVal = String.Format("local-name() = '{0}' {1}", rimName.Name, GenerateConditionString(p, tr, ".", genericSupplier));
                properties.Add(".");
            }

            // Generate the root
            foreach(var property  in properties)
                root += String.Format("{0}/", property);
            
            foreach (var element in fixedElements.FindAll(o=>o.PropertyType != Property.PropertyTypes.Structural))
            {
                string tStr = GenerateConditionString(element, null, ".", genericSupplier);
                retVal += string.Format(" and {0}hl7:{1}[{2}]", root, element.Realization[0].Name, string.IsNullOrEmpty(tStr) ? "1" : tStr.Substring(4));
                
                // Recurse
                if (element.Type.Class != null)
                {
                    properties.Add(String.Format("hl7:{0}[{1}]", element.Realization[0].Name, string.IsNullOrEmpty(tStr) ? "1" : tStr.Substring(4)));
                    string tStrNested = GenerateDeepConditionString(element, element.Type, properties, genericSupplier);
                    if(!String.IsNullOrEmpty(tStrNested))
                        retVal += String.Format(" {0}", tStrNested);
                    properties.RemoveAt(properties.Count - 1); // Pop the current properties
                }
            }

            return retVal;
        }

        /// <summary>
        /// Write an inline abstract when condition for expansion
        /// </summary>
        private void WriteInlineAbstractWhenConditionExpand(XmlWriter xw, TypeReference tr, Property p, List<TypeReference> genericSupplier, string interactionName)
        {
            List<String> currentConditions = new List<string>();
            foreach (TypeReference child in tr.Class.SpecializedBy)
            {

                if (child.Class.IsAbstract)
                    WriteInlineAbstractWhenConditionExpand(xw, child, p, genericSupplier, interactionName);
                else
                {
                   
                    // Perform the inline element map
                    Property rimName = null;

                    // Is this representative of some sort of RIM structure?
                    if ((p.Container is Class) && (p.Container as Class).ContainerName == "RIM") // This is a rim property
                        rimName = p;
                    else if (p.Realization == null || p.Realization.Count == 0)
                    {
                        // xw.WriteElementString("comment", NS_XSLT, String.Format("TODO: Can't realize property '{0}' in the RIM as no realization data was found", p.Name));
                        rimName = p;
                        //return;
                    }
                    else
                        rimName = p.Realization[0] as Property;

                    
                    // Calculate the test condition
                    string s = String.Format("local-name() = '{0}' {1} and ./@*[local-name() = 'type'] = '{2}'", rimName.Name, GenerateConditionString(p, child, ".", genericSupplier), child.Name);
                    
                    //s = CascadeConditionString(s, child, null, genericSupplier);

                    if (currentConditions.Contains(s)) continue; // break out as we have already processed this.

                    
                    // JF- Support Ordering
                    //xw.WriteStartElement("when", NS_XSLT);
                    //xw.WriteAttributeString("test", s);
                    xw.WriteStartElement("for-each", NS_XSLT);
                    xw.WriteAttributeString("select", String.Format("./*[{0}]", s));



                    // Write the apply template
                    if (p.AlternateTraversalNames == null || p.AlternateTraversalNames.Count == 0)
                        xw.WriteStartElement(p.Name, NS_SRC); // p.name
                    else
                    {
                        Property.AlternateTraversalData traversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == child.Name && interactionName == o.InteractionOwner.Name);
                        if (traversal.TraversalName != null) // found an alt traversal
                            xw.WriteStartElement(traversal.TraversalName, NS_SRC);
                        else
                        {
                            traversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == child.Name && o.InteractionOwner == null);
                            if (traversal.TraversalName != null)
                                xw.WriteStartElement(traversal.TraversalName, NS_SRC);
                            else
                                xw.WriteStartElement(p.Name, NS_SRC); // fall back to the property name
                        }
                    }

                    // Determine if there are any other sibling classes that have the same classification as this
                    List<TypeReference> matching = tr.Class.SpecializedBy.FindAll(o => String.Format("local-name() = '{0}' {1}", rimName.Name, GenerateConditionString(p, o, ".", genericSupplier)) == s);
                    currentConditions.Add(s);

                    TypeReference trGenerated = child;
                    if (matching.Count > 1) // There are members that can be combined!
                        // Now the dangerous part
                        trGenerated = GenerateCombinedType(matching);

                    // Perform the inline element map
                    WriteInlineApplyTemplate(xw, p, trGenerated, genericSupplier);

                    xw.WriteEndElement(); // p.name
                    xw.WriteEndElement(); // when
                }
            }
        }

        private TypeReference GenerateCombinedType(List<TypeReference> matching)
        {
            Class generatedClass = new Class();
            generatedClass.Name = "";
            generatedClass.MemberOf = matching[0].Class.MemberOf;
            generatedClass.ContainerPackage = (matching[0].Class as Class).ContainerPackage;
            generatedClass.Content = new List<ClassContent>();

            // Combine everything
            foreach (var t in matching)
            {
                generatedClass.Name += t.Class.Name;
                List<Property> elements = FlattenPropertyHierarchy(t.Class.Content);
                int order = 0;
                foreach (var ele in elements)
                {
                    Property clonedElement = ele.Clone() as Property, 
                        currentGeneratedElement = generatedClass.Content.Find(o => o.Name == ele.Name) as Property; // clone the property
                    clonedElement.Container = generatedClass;
                    if (ele.Container is Choice)
                        clonedElement.Realization = (ele.Container as Choice).Realization;


                    if (currentGeneratedElement == null) // Doesn't already exist!
                        generatedClass.Content.Insert(order, clonedElement);
                    else if (currentGeneratedElement != null && currentGeneratedElement.Type.Name != ele.Type.Name && ele.Type.Class != null) // a property with the same name but with different type exists
                        // Combine those classes
                        currentGeneratedElement.Type = GenerateCombinedType(new List<TypeReference>() { currentGeneratedElement.Type, ele.Type });
                    order++;
                }
            }
            return generatedClass.CreateTypeReference();
        }


        /// <summary>
        /// Generate the condition string for this property
        /// </summary>
        /// <param name="root">If null defaults to the RIM name of the p parameter</param>
        private string GenerateConditionString(Property p, TypeReference tr, string root, List<TypeReference> genericSupplier)
        {
            Property rimName = p;

            // Type reference
            if (tr == null)
            {
                tr = p.Type;
                if (ResolveTypeReference(tr.Name, genericSupplier) != null)
                    tr = ResolveTypeReference(p.Type.Name, genericSupplier);
            }

            // Determine the RIM Name
            if (p.Container is Choice && (p.Container as Choice).Realization != null &&
                (p.Container as Choice).Realization.Count > 0)
                rimName = (p.Container as Choice).Realization[0] as Property;
            else if (p.Realization == null || p.Realization.Count == 0)
            {
                //rimName = p;
                return "";
            }
            else
                rimName = p.Realization[0] as Property;

            if (root == null && p.PropertyType != Property.PropertyTypes.Structural) // default to p
                root = String.Format("./hl7:{0}", rimName.Name, NS_SRC);
            else if(root == null)
                root = string.Format("./@{0}", rimName.Name);

            // Class of the type refernece is null, no point in generating a condition for it
            if (tr.Class == null && root != ".")
                return string.Format(" and count({0}) >= {1}", root, p.MinOccurs);
            else if (tr.Class == null)
                return "";

            // Get all fixed elements
            List<Property> fixedElements = FlattenPropertyHierarchy(tr.Class.GetFullContent()).FindAll(o => o.FixedValue != null && o.PropertyType == Property.PropertyTypes.Structural);

            string retVal = "";

            if (fixedElements.Count > 0)
                foreach (Property fixedProperty in fixedElements)
                    retVal += String.Format(" and {0}/@{1} = '{2}'", root, fixedProperty.Name == "semText" ? "*[local-name() = 'semText']" : fixedProperty.Name, fixedProperty.FixedValue);

            return retVal;
        }

        /// <summary>
        /// Write the inline element mapping
        /// </summary>
        private void WriteInlineElementMapExpand(XmlWriter xw, Property p, TypeReference tr, List<TypeReference> genericSupplier, string interactionName)
        {
            // Write the real name for the property
            // Write the apply template
            if (p.AlternateTraversalNames == null || p.AlternateTraversalNames.Count == 0)
                xw.WriteStartElement(p.Name, NS_SRC); // p.name
            else
            {
                Property.AlternateTraversalData traversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == tr.Name && interactionName == o.InteractionOwner.Name);
                if (traversal.TraversalName != null) // found an alt traversal
                    xw.WriteStartElement(traversal.TraversalName, NS_SRC);
                else
                {
                    traversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == tr.Name && o.InteractionOwner == null);
                    if (traversal.TraversalName != null)
                        xw.WriteStartElement(traversal.TraversalName, NS_SRC);
                    else
                        xw.WriteStartElement(p.Name, NS_SRC); // fall back to the property name
                }
            }

            // Write fixed properties
            List<Property> elements = tr.Class == null ? new List<Property>() : FlattenPropertyHierarchy(tr.Class.GetFullContent()),
               fixedElements = elements.FindAll(o => o.FixedValue != null && o.PropertyType == Property.PropertyTypes.Structural);

            // Generate the fixed type
            foreach (Property fixedElement in fixedElements)
                WriteInlineAttributeMapExpand(xw, fixedElement);
            WriteInlineTypeMapExpand(xw, tr, genericSupplier, interactionName);

            xw.WriteEndElement(); // p.Name
        }

        /// <summary>
        /// Write an inline attribute map collapse
        /// </summary>
        /// <param name="xw">The xml writer to write to</param>
        /// <param name="p">The property to write the map for</param>
        private void WriteInlineAttributeMapExpand(XmlWriter xw, Property p)
        {

            // Any annotations telling us of the collapse
            if (p.Annotations == null || p.Annotations.Count == 0) // No fancy stuff
            {
                xw.WriteStartElement("attribute", NS_XSLT);
                xw.WriteAttributeString("name", p.Name);
                if (p.Name == "semText")
                    xw.WriteAttributeString("namespace", "urn:marc-hi:ca/hial");

                xw.WriteStartElement("value-of", NS_XSLT);
                if (p.FixedValue != null)
                    xw.WriteAttributeString("select", String.Format("'{0}'", p.FixedValue));
                else
                    xw.WriteAttributeString("select", ".");
                xw.WriteEndElement(); // value-of
                xw.WriteEndElement(); // attribute
            }
            else
                System.Diagnostics.Debugger.Break();


        }


        /// <summary>
        /// Write an inline type mapping for the TypeReference specified
        /// </summary>
        private void WriteInlineTypeMapExpand(XmlWriter xw, TypeReference tr, List<TypeReference> genericSupplier, string interactionName)
        {
            List<String> collectionTypes = new List<string>() { "SET", "LIST", "QSET", "DSET", "HIST", "BAG" };

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
                if (dataTypeMaps[schemaFriendlyName ?? ""].LocalName == "for-each")
                {
                    xw.WriteStartElement("call-template", NS_XSLT);
                    xw.WriteAttributeString("name", "dtMap");
                    //xw.WriteStartElement("copy-of", NS_XSLT);
                    //xw.WriteAttributeString("select", ".");
                    //xw.WriteEndElement();
                    xw.WriteEndElement(); // call-template
                }
                else
                    dataTypeMaps[schemaFriendlyName ?? ""].WriteTo(xw);
            }
            else if (IsRecursiveReference(tr, tr, new List<string>())) // complex map
                WriteInlineApplyTemplate(xw, tr.Container as Property, tr, genericSupplier);
            else if (tr.Class != null)
                WriteInlineForeachExpand(xw, tr, genericSupplier, interactionName);
            //else
            //System.Diagnostics.Debugger.Break();
        }

        /// <summary>
        /// Generate a rim graph that maps an interaction into is RIM representation
        /// </summary>
        /// <param name="interaction"></param>
        public void GenerateRIMGraph(Interaction interaction)
        {
            templateClasses.Clear();

            if (interaction.MessageType == null || interaction.MessageType.Class == null ||
                interaction.MessageType.Class.Realizations == null ||
                interaction.MessageType.Class.Realizations.Count == 0)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Interaction '{0}' does not have a RIM realization...", interaction.Name), "error");
                return;
            }

            requiredTemplates = new List<String>();

            // Create the writer
            XmlWriterSettings xwsettings = new XmlWriterSettings()
            {
                Indent = true
            };

            XmlWriter xw = null;

            if (dataTypeMaps.Count == 0) // Generate the data type maps
                GenerateDataTypeMaps();

            // Try to open the file
            try
            {
                xw = XmlWriter.Create(Path.Combine(OutputDir, String.Format("{0}_RimGraph.xslt", interaction.Name)), xwsettings);

                // Write the template
                xw.WriteStartElement("xsl", "stylesheet", NS_XSLT);
                xw.WriteAttributeString("xmlns", "hl7", null, NS_SRC); // HACK: Declare a namespace without using it
                xw.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                xw.WriteAttributeString("xmlns", "marc", null, "urn:marc-hi:ca/hial");
                xw.WriteAttributeString("exclude-result-prefixes", "hl7 xsl");
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
                xw.WriteStartElement(interaction.MessageType.Class.Realizations[0].Class.Name, NS_SRC);

                // The Tag is used by BizTalk orchestrations to determing if the message is an acknowledging message
                xw.WriteStartElement("attribute", NS_XSLT);
                xw.WriteAttributeString("name", "tag");
                xw.WriteStartElement("value-of", NS_XSLT);
                xw.WriteAttributeString("select", "''");
                xw.WriteEndElement(); // value-of
                xw.WriteEndElement(); // attribute

                // Write the attributes
                WriteInlineForeachCollapse(xw, interaction.MessageType, interaction.MessageType.GenericSupplier, interaction.Name);

                xw.WriteEndElement(); // interaction.Name
                xw.WriteEndElement(); // end template

                #endregion

                #region Other Templates

                #region DT Copy Template
                xw.WriteStartElement("template", NS_XSLT);
                xw.WriteAttributeString("name", "dtMap");
                xw.WriteStartElement("for-each", NS_XSLT);
                xw.WriteAttributeString("select", "@*|child::node()");
                xw.WriteStartElement("copy-of", NS_XSLT);
                xw.WriteAttributeString("select", ".");
                xw.WriteEndElement(); // copy
                xw.WriteEndElement(); // for-each
                xw.WriteEndElement(); // template
                #endregion

                for (int i = 0; i < requiredTemplates.Count; i++)
                {
                    string s = requiredTemplates[i];
                    xw.WriteStartElement("template", NS_XSLT);
                    xw.WriteAttributeString("name", s);

                    xw.WriteStartElement("attribute", NS_XSLT);
                    xw.WriteAttributeString("name", "type");
                    xw.WriteAttributeString("namespace", "urn:marc-hi:ca/hial");
                    xw.WriteString(s);

                    xw.WriteEndElement(); // attribute

                    Class cls = templateClasses[s] as Class;
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
                System.Diagnostics.Trace.WriteLine(String.Format("Could not write to the file '{0}_RimGraph' ({1})...", interaction.Name, e.Message), "error");
            }
            finally
            {
                if (xw != null) xw.Close();
            }

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

            // Write the fixed attributes
            List<Property> fixedElements = elements.FindAll(o => o.FixedValue != null && o.PropertyType == Property.PropertyTypes.Structural);
            foreach (var element in fixedElements)
                WriteInlineAttributeMapCollapse(xw, element);

            // Is there even any data in the foreach?
            if (elements.Find(o => o.PropertyType != Property.PropertyTypes.Structural || o.PropertyType == Property.PropertyTypes.Structural && o.FixedValue == null) == null)
                return;

            // Write any attributes
            xw.WriteStartElement("for-each", NS_XSLT);
            xw.WriteAttributeString("select", String.Format("@* | child::node()[namespace-uri() = '{0}']", NS_SRC));

            xw.WriteStartElement("choose", NS_XSLT);

            // Write the element data
            foreach (Property p in elements)
                WriteInlineWhenConditionCollapse(xw, p, genericSupplier, interactionName);


            xw.WriteEndElement(); // choose
            xw.WriteEndElement(); // foreach
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
        /// Write inline when condition
        /// </summary>
        private void WriteInlineWhenConditionCollapse(XmlWriter xw, Property p, List<TypeReference> genericSupplier, string interactionName)
        {
            // Resolve the type reference
            TypeReference tr = p.Type;
            if (ResolveTypeReference(tr.Name, genericSupplier) != null)
                tr = ResolveTypeReference(p.Type.Name, genericSupplier);

            if (p.FixedValue != null && p.PropertyType == Property.PropertyTypes.Structural) return;


            // Need the rim representation
            Property rimName = p;

            if ((p.Container is Class) && (p.Container as Class).ContainerName == "RIM") // This is a rim property
                rimName = p;
            else if (p.Realization != null && p.Realization.Count > 0)
                rimName = p.Realization[0] as Property;
            else if (p.Container is Choice && (p.Container as Choice).Realization != null &&
                (p.Container as Choice).Realization.Count > 0)
                rimName = (p.Container as Choice).Realization[0] as Property;
            else
            {
                xw.WriteComment(String.Format("TODO: Can't realize property '{0}' in the RIM as no realization data was found", p.Name));
                //rimName = p;
                return;
            }

            if (p.AlternateTraversalNames == null || p.AlternateTraversalNames.Count == 0) // Alternate traversal names
            {
                xw.WriteStartElement("when", NS_XSLT);
                xw.WriteAttributeString("test", String.Format("local-name() = '{0}'", p.Name));

                // Write structural property
                if (rimName.PropertyType == Property.PropertyTypes.Structural && p.FixedValue == null)
                    WriteInlineAttributeMapCollapse(xw, p);
                else if (rimName.PropertyType != Property.PropertyTypes.Structural)
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
                    xw.WriteAttributeString("test", string.Format("local-name() = '{0}'", altTraversal.TraversalName));
                else
                    xw.WriteAttributeString("test", string.Format("local-name() = '{0}'", p.Name));

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
                    Property.AlternateTraversalData altTraversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == child.Name && o.InteractionOwner.Name == interactionName); // Alternate traversal
                    if (altTraversal.TraversalName == null)
                        altTraversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == child.Name);

                    // Traversal was found
                    if (altTraversal.TraversalName != null)
                        xw.WriteAttributeString("test", string.Format("local-name() = '{0}'", altTraversal.TraversalName));
                    else
                        throw new InvalidOperationException(String.Format("Can't find traversal for alternative data for '{0}' when type is '{1}'...", p.Name, child.Name));

                    // Perform the inline element map
                    Property rimName = null;

                    // Is this representative of some sort of RIM structure?
                    if ((p.Container is Class) && (p.Container as Class).ContainerName == "RIM") // This is a rim property
                        rimName = p;
                    else if (p.Realization == null || p.Realization.Count == 0)
                    {
                        // xw.WriteElementString("comment", NS_XSLT, String.Format("TODO: Can't realize property '{0}' in the RIM as no realization data was found", p.Name));
                        rimName = p;
                        //return;
                    }
                    else
                        rimName = p.Realization[0] as Property;

                    // Write the apply template
                    if (rimName.AlternateTraversalNames == null || rimName.AlternateTraversalNames.Count == 0)
                        xw.WriteStartElement(rimName.Name, NS_SRC); // p.name
                    else
                    {
                        Property.AlternateTraversalData traversal = rimName.AlternateTraversalNames.Find(o => o.CaseWhen.Name == child.Name);
                        if (traversal.TraversalName != null) // found an alt traversal
                            xw.WriteStartElement(traversal.TraversalName, NS_XSLT);
                        else
                            xw.WriteStartElement(rimName.Name, NS_XSLT); // fall back to the property name
                    }
                    // Perform the inline element map
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

            Property rimName = null;

            // Is this representative of some sort of RIM structure?
            if ((p.Container is Class) && (p.Container as Class).ContainerName == "RIM") // This is a rim property
                rimName = p;
            else if (p.Container is Choice && (p.Container as Choice).Realization != null &&
                (p.Container as Choice).Realization.Count > 0)
                rimName = (p.Container as Choice).Realization[0] as Property;
            else if (p.Realization == null || p.Realization.Count == 0)
            {
                xw.WriteElementString("comment", NS_XSLT, String.Format("TODO: Can't realize property '{0}' in the RIM as no realization data was found", p.Name));
                //rimName = p;
                return;
            }
            else
                rimName = p.Realization[0] as Property;

            if (ResolveTypeReference(tr.Name, genericSupplier) != null)
                tr = ResolveTypeReference(p.Type.Name, genericSupplier);

            if (rimName.AlternateTraversalNames == null || rimName.AlternateTraversalNames.Count == 0)
            {
                xw.WriteStartElement(rimName.Name, NS_SRC); // p.name
                if (tr.Class != null)
                {
                    //xw.WriteStartElement("attribute", NS_XSLT);

                    //xw.WriteAttributeString("name", "type");
                    //xw.WriteAttributeString("namespace", "urn:marc-hi:ca/hial");
                    //xw.WriteString(tr.Name);
                    //xw.WriteEndElement(); // attribute
                }
            }
            else
            {
                xw.WriteElementString("comment", NS_XSLT, "TODO: Alternate traversal names aren't represented! Perhaps this will be a choice?");
                //Property.AlternateTraversalData traversal = p.AlternateTraversalNames.Find(o => o.CaseWhen.Name == tr.Name);
                //if (traversal.TraversalName != null) // found an alt traversal
                //    xw.WriteStartElement(traversal.TraversalName, TargetNamespace);
                //else
                //    xw.WriteStartElement(p.Name, TargetNamespace); // fall back to the property name
            }

            // HACK: Need to know what the semantics text of the type is
            if (p.Type.Class != null &&
                p.Type.Class.Realizations != null &&
                p.Type.Class.Realizations.Count > 0 &&
                p.Type.Class.Realizations[0].Class != null &&
                p.Type.Class.Realizations[0].Class.Content.Find(o => o.Name == "semanticsText") != null
                && tr.Class != null && tr.Class.Content.Find(o => o.Name == "semText") == null)
                tr.Class.AddContent(new Property()
                {
                    Name = "semText",
                    PropertyType = Property.PropertyTypes.Structural,
                    Type = new TypeReference() { Name = "ST" },
                    FixedValue = p.Name,
                    MinOccurs = "1",
                    MaxOccurs = "1"
                     
                });
            // Determine if this should be a genericized parameter
            WriteInlineTypeMap(xw, tr, genericSupplier, interactionName, p.PropertyType, rimName.PropertyType);
            xw.WriteEndElement(); // p.name

        }

        /// <summary>
        /// Write an inline type mapping for the TypeReference specified
        /// </summary>
        private void WriteInlineTypeMap(XmlWriter xw, TypeReference tr, List<TypeReference> genericSupplier, string interactionName, Property.PropertyTypes srcType, Property.PropertyTypes destType)
        {
            List<String> collectionTypes = new List<string>() { "SET", "LIST", "QSET", "DSET", "HIST", "BAG" };

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
                if (dataTypeMaps[schemaFriendlyName ?? ""].LocalName == "for-each")
                {
                    
                    // Call the template for strait map only if the property type hasn't changed
                    if (srcType.Equals(destType))
                    {
                        xw.WriteStartElement("call-template", NS_XSLT);
                        xw.WriteAttributeString("name", "dtMap");
                        //xw.WriteStartElement("copy-of", NS_XSLT);
                        //xw.WriteAttributeString("select", ".");
                        //xw.WriteEndElement();
                        xw.WriteEndElement(); // call-template
                    }
                    else
                    {
                        xw.WriteStartElement("value-of", NS_XSLT);
                        xw.WriteAttributeString("select", ".");
                        xw.WriteEndElement(); // value-of
                    }
                }
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
        /// Write an inline attribute map collapse
        /// </summary>
        /// <param name="xw">The xml writer to write to</param>
        /// <param name="p">The property to write the map for</param>
        private void WriteInlineAttributeMapCollapse(XmlWriter xw, Property p)
        {
            ClassContent rimName = p;
            if(p.Realization != null && p.Realization.Count > 0)
            {
                //xw.WriteComment(String.Format("{0} realizes {1}", p.Name, p.Realization[0].Name));
                rimName = p.Realization[0];
            }

            // Any annotations telling us of the collapse
            if (p.Annotations == null || p.Annotations.Count == 0) // No fancy stuff
            {
                xw.WriteStartElement("attribute", NS_XSLT);
                xw.WriteAttributeString("name", rimName.Name);
                if (p.Name == "semText")
                    xw.WriteAttributeString("namespace", "urn:marc-hi:ca/hial");
                xw.WriteStartElement("value-of", NS_XSLT);
                if (p.FixedValue != null)
                    xw.WriteAttributeString("select", String.Format("'{0}'", p.FixedValue));
                else
                    xw.WriteAttributeString("select", ".");
                xw.WriteEndElement(); // value-of
                xw.WriteEndElement(); // attribute
            }
            else
                System.Diagnostics.Debugger.Break();

        }

        /// <summary>
        /// Create the data type maps 
        /// </summary>
        private void GenerateDataTypeMaps()
        {
            XmlDocument context = new XmlDocument();

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
                if (xst.Name != null)
                    dataTypeMaps.Add(xst.Name, foreachElement);
            }
        }


        /// <summary>
        /// Write inline apply template call
        /// </summary>
        private void WriteInlineApplyTemplate(XmlWriter xw, Property p, TypeReference tr, List<TypeReference> genericSupplier)
        {
            if (!requiredTemplates.Contains(tr.Name))
            {
                requiredTemplates.Add(tr.Name);
                if(!templateClasses.ContainsKey(tr.Name))
                    templateClasses.Add(tr.Name, tr.Class);
            }

            // Write the xsl:apply-template
            xw.WriteStartElement("call-template", NS_XSLT);
            xw.WriteAttributeString("name", tr.Name);
            xw.WriteEndElement(); // call-template
        }
    }
}
