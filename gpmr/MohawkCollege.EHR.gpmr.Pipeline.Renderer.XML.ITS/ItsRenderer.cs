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
 * Date: 01-11-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using MohawkCollege.EHR.gpmr.COR;
using System.Linq;
using System.Xml.Schema;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.XML.ITS
{
    /// <summary>
    /// ITS Compiler. Will render the necessary files for XML ITS 1.0 
    /// </summary>
    public class ItsRenderer : RendererBase
    {
        #region Command Line Paramters
        internal static string targetNs = "urn:hl7-org:v3";
        internal static bool xslt = false;
        internal static bool genVocab = false;
        internal static string organization = "none";
        internal static string dataTypesXsd = null;
        internal static string outputDir = null;
        internal static string itsString = "XML_1.0";
        internal static string baseClass = "RIM.InfrastructureRoot";
        internal static bool xsltRim = false;
        #endregion

        #region XSD Handler
        internal static List<MohawkCollege.EHR.Util.SimpleXSD.XmlSchemaType> dataTypes = new List<MohawkCollege.EHR.Util.SimpleXSD.XmlSchemaType>();
        #endregion

        // Help text
        private string[][] helpText = new string[][] 
            { new string[] { "--xsd-target-ns", "Sets the target namespace of the XSD" } ,
                new string[] {"--xsd-xslt","Enables the creation of an XSLT file that translates"}, 
                new string[] {"--xsd-gen-vocab", "Generate all vocabulary into voc.xsd"},
                new string[] {"--xsd-org","The organization the generated code belongs to"},
                new string[] {"--xsd-datatypes-xsd","Set the xsd to use as the data-types"},
                new string[] {"--xsd-version","Set the ITSVersion string"},
                new string[] {"--xsd-rim-xslt", "Create the RIM XSLT" }

            };

        /// <summary>
        /// Gets the name of the renderer
        /// </summary>
        public override string Name
        {
            get { return "XML Schema Renderer"; }
        }

        /// <summary>
        /// Get the identifier of the render component
        /// </summary>
        public override string Identifier
        {
            get { return "XSD"; }
        }

        /// <summary>
        /// Execution order
        /// </summary>
        public override int ExecutionOrder
        {
            get { return 2; }
        }

        /// <summary>
        /// Called when the pipeline component is executed.
        /// </summary>
        public override void Execute()
        {

            // Don't execute this
            if (!(hostContext.Data["EnabledRenderers"] as StringCollection).Contains(this.Identifier)) return;

            System.Diagnostics.Trace.WriteLine("Starting XML ITS Renderer....", "information");

            // Parse the command line parameters
            ProcessParameters();

            System.Diagnostics.Trace.Write(String.Format("Loading '{0}'.", dataTypesXsd), "debug");
            // Process the datatypes XSD
            MohawkCollege.EHR.Util.SimpleXSD.XmlSchemaSet xss = new MohawkCollege.EHR.Util.SimpleXSD.XmlSchemaSet();
            xss.Add(dataTypesXsd);
            xss.Loading += new MohawkCollege.EHR.Util.SimpleXSD.XmlSchemaLoadingHandler(xss_Loading);
            xss.Load();
            System.Diagnostics.Trace.WriteLine("", "debug");
            dataTypes.AddRange(xss.Types);

            // Copy the data-types files to the target
            try
            {
                XmlSchema tSchema = XmlSchema.Read(File.OpenRead(dataTypesXsd), new ValidationEventHandler(delegate(object o, ValidationEventArgs e)
                    {
                        System.Diagnostics.Trace.WriteLine(String.Format("Error reading '{0}' ({1})...", dataTypesXsd, e.Message), "warn");
                    }));
                
                // Copy the core file
                if (dataTypesXsd != Path.Combine(outputDir, Path.GetFileName(dataTypesXsd)))
                    File.Copy(dataTypesXsd, Path.Combine(outputDir, Path.GetFileName(dataTypesXsd)), true);
                // Copy the includes
                foreach (XmlSchemaInclude xsi in tSchema.Includes)
                    File.Copy(Path.Combine(Path.GetDirectoryName(dataTypesXsd), xsi.SchemaLocation), Path.Combine(outputDir, Path.GetFileName(xsi.SchemaLocation)), true);
                dataTypesXsd = Path.GetFileName(dataTypesXsd);


            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Could not copy '{0}' to output directory...", dataTypesXsd), "error");
            }

            // Generate the voc.xsd
            GenerateVocabulary();
            // Generate InfrastructureRoot.xsd
            GenerateInfrastructureRoot();
            // Generate Message Type XSDs
            GenerateMessageTypes();
            // Generate the interaction XSDs
            GenerateInteractions();
        }

        /// <summary>
        /// Generate all MTs
        /// </summary>
        private void GenerateMessageTypes()
        {
            ClassRepository classRep = hostContext.Data["SourceCR"] as ClassRepository;

            // Sub-Systems only
            var subsystems = from clsRep in classRep
                               where clsRep.Value is SubSystem
                             select clsRep.Value as SubSystem;

            // Generate XSD for interactions
            foreach (var sType in subsystems)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Generating {0}.XSD...", sType.Name), "debug");
                XmlSchema schema = new XmlSchema()
                {
                    TargetNamespace = targetNs,
                    Namespaces = new System.Xml.Serialization.XmlSerializerNamespaces(
                        new XmlQualifiedName[] { 
                    new XmlQualifiedName("", targetNs)
                    })
                };

                // Includes
                List<String> includedModels = new List<string>() { "InfrastructureRoot" };

                // Generate the data types
                List<TypeReference> additionalComplexTypes = new List<TypeReference>();
                List<XmlSchemaComplexType> complexTypes = new List<XmlSchemaComplexType>();

                foreach (Class cls in sType.OwnedClasses)
                {
                    XmlSchemaComplexType type = GenerateComplexType(cls, null, new List<TypeReference>(), includedModels, additionalComplexTypes);
                    if(type != null)
                        complexTypes.Add(type);
                }

                // Process additional Includes
                foreach (string include in includedModels)
                {
                    if (include != sType.Name)
                        schema.Includes.Add(new XmlSchemaInclude() { SchemaLocation = Path.ChangeExtension(include, "xsd") });
                }

                // ADd the types to the schema
                foreach (XmlSchemaComplexType t in complexTypes)
                {
                    // Don't add duplicates
                    var currentItems = from XmlSchemaObject obj in schema.Items
                                       where obj is XmlSchemaComplexType &&
                                       (obj as XmlSchemaComplexType).Name == t.Name
                                       select obj;
                    if (currentItems.Count() == 0)
                        schema.Items.Add(t);
                }

                // Write to the text writer
                TextWriter tw = null;
                try
                {
                    tw = File.CreateText(Path.ChangeExtension(Path.Combine(outputDir, sType.Name), "xsd"));
                    schema.Write(tw);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message, "error");
                }
                finally
                {
                    if (tw != null) tw.Close();
                }
            }
        }

        /// <summary>
        /// Generate infrastructure root elements
        /// </summary>
        private void GenerateInfrastructureRoot()
        {
            ClassRepository classRep = hostContext.Data["SourceCR"] as ClassRepository;
            // Find the type for the infrastructure Root
            Feature infrastructureRoot = null;
            if(!classRep.TryGetValue("RIM.InfrastructureRoot", out infrastructureRoot))
                System.Diagnostics.Trace.WriteLine("Couldn't find definition for InfrastructureRoot!", "warn");
            System.Diagnostics.Trace.WriteLine("Generating InfrastructureRoot.xsd...", "debug");

            // Generate the schema
            XmlSchema schema = new XmlSchema()
                {
                    Namespaces = new System.Xml.Serialization.XmlSerializerNamespaces(new XmlQualifiedName[] {
                        new XmlQualifiedName("", targetNs)
                    }),
                    TargetNamespace = targetNs
                };

            // Add schema item
            schema.Items.Add(GenerateComplexType(infrastructureRoot as Class, null, new List<TypeReference>(), new List<string>(), new List<TypeReference>()));
            
            // Add include
            schema.Includes.Add(new XmlSchemaInclude() { SchemaLocation = dataTypesXsd });
            schema.Includes.Add(new XmlSchemaInclude() { SchemaLocation = "InfrastructureRoot.xsd" });

            // Write to the text writer
            TextWriter tw = null;
            try
            {
                tw = File.CreateText(Path.ChangeExtension(Path.Combine(outputDir, "InfrastructureRoot"), "xsd"));
                schema.Write(tw);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message, "error");
            }
            finally
            {
                if (tw != null) tw.Close();
            }
        }

        /// <summary>
        /// Generate the interactions
        /// </summary>
        private void GenerateInteractions()
        {
            ClassRepository classRep = hostContext.Data["SourceCR"] as ClassRepository;

            // XSLT Generator
            XsltGenerator xsltGenerator = new XsltGenerator()
            {
                DataTypesXsd = dataTypes,
                OutputDir = Path.Combine(outputDir, "xslt"), 
                TargetNamespace = targetNs
            };
            RimXsltGenerator rimXsltGenerator = new RimXsltGenerator()
            {
                DataTypesXsd = dataTypes, 
                OutputDir = Path.Combine(outputDir, "xslt")
            };

            // Create the file
            Directory.CreateDirectory(xsltGenerator.OutputDir);

            // Interactions
            var interactions = from clsRep in classRep
                               where clsRep.Value is Interaction
                               select clsRep.Value as Interaction;

            // Generate XSD for interactions
            foreach (var iType in interactions)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Generating {0}.XSD...", iType.Name), "debug");
                XmlSchema schema = new XmlSchema() { TargetNamespace = targetNs, Namespaces = new System.Xml.Serialization.XmlSerializerNamespaces(
                    new XmlQualifiedName[] { 
                    new XmlQualifiedName("", targetNs)
                    })
                };
                
                // Includes
                List<String> includedModels = new List<string>() { "InfrastructureRoot" };

                // Generate the data types
                List<TypeReference> additionalComplexTypes = new List<TypeReference>();
                List<XmlSchemaComplexType> complexTypes = GenerateComplexTypes(iType.MessageType, iType, includedModels, additionalComplexTypes);
                
                // Was there no complex types generated, if so we have to include the type model 
                if (complexTypes.Count == 0)
                    includedModels.Add(iType.MessageType.Class.ContainerName);

                // Generate the required additional types
                for (int i = 0; i < additionalComplexTypes.Count; i++)
                {
                    TypeReference tr = additionalComplexTypes[i].Clone() as TypeReference;
                    tr.Container = additionalComplexTypes[i].Container;
                    
                    // Resolve any cascaded generic parameters
                    for (int s = 0; s < tr.GenericSupplier.Count; s++)
                        tr.GenericSupplier[s] = ResolveGenericParameter(iType.MessageType, tr.GenericSupplier[s] as TypeParameter);

                    // Fix the generic supplier for this type reference
                    complexTypes.AddRange(GenerateComplexTypes(tr, iType, includedModels, additionalComplexTypes));
                }

                // Process additional Includes
                foreach(string include in includedModels)
                    schema.Includes.Add(new XmlSchemaInclude() { SchemaLocation = String.Format("./{0}",Path.ChangeExtension(include, "xsd")) });

                string baseTypeName = complexTypes.Count == 0 ? iType.MessageType.Name : String.Format("{0}.{1}", iType.Name, iType.MessageType.Name);

                // Setup the anonymous class that extends the model
                XmlSchemaComplexContentExtension interactionContentExtension = new XmlSchemaComplexContentExtension() 
                {
                     BaseTypeName = new XmlQualifiedName(baseTypeName, targetNs)
                };
                XmlSchemaComplexContent interactionContent = new XmlSchemaComplexContent() { Content = interactionContentExtension };
                XmlSchemaComplexType interactionType = new XmlSchemaComplexType() { ContentModel = interactionContent };
                interactionContentExtension.Attributes.Add(new XmlSchemaAttribute()
                {
                    Use= XmlSchemaUse.Required, 
                    FixedValue = itsString,
                    Name = "ITSVersion", 
                    SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema")
                });

                XmlSchemaElement element = new XmlSchemaElement()
                {
                    Name = iType.Name,
                    IsAbstract = false,
                    SchemaType = interactionType
                };

                // Add annotations
                if (!Documentation.IsEmpty(iType.Documentation))
                {
                    XmlSchemaDocumentation documentation = new XmlSchemaDocumentation();
                    XmlDocument documentationMarkup = new XmlDocument();
                    documentationMarkup.LoadXml("<div xmlns=\"http://www.w3.org/1999/xhtml\">" + iType.Documentation.ToString() + "</div>");
                    documentation.Markup = new List<XmlNode>(documentationMarkup.ChildNodes.Cast<XmlNode>()).ToArray();
                    element.Annotation = new XmlSchemaAnnotation();
                    element.Annotation.Items.Add(documentation);
                }

                schema.Items.Add(element);

                // ADd the types to the schema
                foreach (XmlSchemaComplexType t in complexTypes)
                {
                    // Don't add duplicates
                    var currentItems = from XmlSchemaObject obj in schema.Items
                                       where obj is XmlSchemaComplexType &&
                                       (obj as XmlSchemaComplexType).Name == t.Name
                                       select obj;
                    if (currentItems.Count() == 0 && t != null)
                        schema.Items.Add(t);
                }

                // Write to the text writer
                TextWriter tw = null;
                try
                {
                    tw = File.CreateText(Path.ChangeExtension(Path.Combine(outputDir, iType.Name), "xsd"));
                    schema.Write(tw);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message, "error");
                }
                finally
                {
                    if (tw != null) tw.Close();
                }

                // Generate the XSLTs
                if (xslt)
                {
                    xsltGenerator.GenerateCollapsingXslt(iType);
                    xsltGenerator.GenerateExpandingXslt(iType);
                }
                if (xsltRim)
                {
                    rimXsltGenerator.GenerateRIMGraph(iType);
                    rimXsltGenerator.GenerateRIMParse(iType);
                }
            }

        }

        /// <summary>
        /// Resolve a generic parameter to a concrete class if possible
        /// </summary>
        /// <param name="context">The parent class which contains the generic bindings</param>
        /// <param name="supplier">The current supplier to look for</param>
        /// <returns>The real type reference</returns>
        private TypeReference ResolveGenericParameter(TypeReference context, TypeParameter supplier)
        {
            if(context.Class == null ||
                context.GenericSupplier == null ||
                context.Class.TypeParameters.Count != context.GenericSupplier.Count) return supplier; // Can't resolve

            TypeReference retVal = supplier; // In case we can't find
            for (int i = 0; i < context.Class.TypeParameters.Count; i++)
                if (context.Class.TypeParameters[i].ParameterName == supplier.ParameterName && retVal == supplier)
                    retVal = context.GenericSupplier[i]; // Found, return the found value
                else if (retVal == supplier &&
                    context.GenericSupplier[i].GenericSupplier != null &&
                    context.GenericSupplier[i].GenericSupplier.Count > 0)
                    retVal = ResolveGenericParameter(context.GenericSupplier[i], supplier);
            
            return retVal; // Can't find
        }

        /// <summary>
        /// Generate complex types
        /// </summary>
        private List<XmlSchemaComplexType> GenerateComplexTypes(TypeReference typeReference, Interaction parent, List<String> includedModels, List<TypeReference> additionalCompileTargets)
        {

            // Is this a valid type reference
            if (typeReference.Class == null) return new List<XmlSchemaComplexType>();

            // Generate the complex types
            List<XmlSchemaComplexType> retVal = new List<XmlSchemaComplexType>();
            // Iterate through the complex types
            foreach (TypeReference tr in typeReference.GenericSupplier ?? new List<TypeReference>())
                retVal.AddRange(GenerateComplexTypes(tr, parent, includedModels, additionalCompileTargets));

            if (typeReference.Class.TypeParameters != null &&
                typeReference.Class.TypeParameters.Count > 0)
                retVal.Add(GenerateComplexType(typeReference.Class, parent.Name, typeReference.GenericSupplier, includedModels, additionalCompileTargets));
            return retVal;
        }


        /// <summary>
        /// Generate the complex type
        /// </summary>
        /// <param name="cls">The class to generate the complex type for</param>
        /// <param name="prefix">The prefix namespace for the class</param>
        /// <param name="genericParameters">Generic parameters</param>
        private XmlSchemaComplexType GenerateComplexType(Class cls, string prefix, List<TypeReference> genericParameters, List<String> includedModels, List<TypeReference> additionalCompileTargets)
        {

            if (cls != null && genericParameters != null && cls.TypeParameters != null && cls.TypeParameters.Count != genericParameters.Count)
                return null;
            if (cls == null)
            {
                Trace.WriteLine("Cannot generate complex type for null class", "error");
                return null;
            }

            // Create the complex type
            XmlSchemaComplexType complexType = new XmlSchemaComplexType()
            {
                Name = prefix != null ? String.Format("{0}.{1}.{2}", prefix, cls.ContainerName, cls.Name) : String.Format("{0}.{1}", cls.ContainerName, cls.Name),
                IsAbstract = cls.IsAbstract
            };

            // Populate documentation for the type
            if (!Documentation.IsEmpty(cls.Documentation))
            {
                XmlSchemaDocumentation documentation = new XmlSchemaDocumentation();
                XmlDocument documentationMarkup = new XmlDocument();
                documentationMarkup.LoadXml("<div xmlns=\"http://www.w3.org/1999/xhtml\">" + cls.Documentation.ToString() + "</div>");
                documentation.Markup = new List<XmlNode>(documentationMarkup.ChildNodes.Cast<XmlNode>()).ToArray();
                complexType.Annotation = new XmlSchemaAnnotation();
                complexType.Annotation.Items.Add(documentation);
            }

            // Complex Content
            XmlSchemaComplexContentExtension content = new XmlSchemaComplexContentExtension();

            // Null flavors and stuff
            if (cls.BaseClass != null)
            {
                // Add the import for model
                if (!includedModels.Contains(cls.BaseClass.Class.ContainerName))
                    includedModels.Add(cls.BaseClass.Class.ContainerName);

                // Set the extension
                content.BaseTypeName = new XmlQualifiedName(cls.BaseClass.Name, targetNs);
            }
            else if(cls.CreateTypeReference().Name != baseClass)
                content.BaseTypeName = new XmlQualifiedName(baseClass, targetNs);

            // Sequence
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            
            // Iterate through content
            foreach (ClassContent cc in cls.Content)
            {
                if (cc is Property)
                {
                    Property property = cc as Property;

                    TypeReference realReference = property.Type.Class == null && genericParameters != null ? genericParameters.Find(o => o is TypeParameter && (o as TypeParameter).ParameterName == property.Type.Name) : property.Type;

                    // Is this a property that really represents a choice?
                    // This sometimes happens when a property (namely ACT) points to an
                    // abstract class that has many different sub-traversals...
                    if (realReference != null && 
                        realReference.Class != null &&
                        realReference.Class.IsAbstract) // Choice
                    {
                        XmlSchemaChoice propertyChoice = new XmlSchemaChoice()
                            {
                                MinOccursString = property.MinOccurs,
                                MaxOccursString = property.MaxOccurs == "*" ? "unbounded" : property.MaxOccurs
                            };
                        List<XmlSchemaElement> elements = CreateChoice(property, realReference.Class, genericParameters, prefix, includedModels, additionalCompileTargets);
                        foreach (XmlSchemaElement ele in elements)
                            propertyChoice.Items.Add(ele);
                        sequence.Items.Add(propertyChoice);
                    }
                    else // The property is really a property
                        switch (property.PropertyType)
                        {
                            case Property.PropertyTypes.Structural:
                                content.Attributes.Add(CreateAttribute(property));
                                break;
                            default:
                                sequence.Items.Add(CreateElement(property, genericParameters, prefix, includedModels, additionalCompileTargets));
                                break;
                        }
                }
                else if (cc is Choice)
                {
                    // Render the choice
                    XmlSchemaChoice choice = new XmlSchemaChoice()
                    {
                        MinOccursString = cc.MinOccurs,
                        MaxOccursString = cc.MaxOccurs == "*" ? "unbounded" : cc.MaxOccurs
                    };

                    List<XmlSchemaElement> elements = CreateChoice(cc as Choice, genericParameters, prefix, includedModels, additionalCompileTargets);
                    foreach (XmlSchemaElement element in elements)
                        choice.Items.Add(element);
                    sequence.Items.Add(choice);
                }
            }

            // Now for the inherited classes, these could also be sent
            if (cls.SpecializedBy.Count > 0)
            {
                XmlSchemaChoice childChoice = new XmlSchemaChoice();
                //// Generate a choice for all child elements
                //List<XmlSchemaElement> elements = GenerateChildChoices(cls, genericParameters, prefix, includedModels, additionalCompileTargets);
                //foreach (XmlSchemaElement ele in elements)
                //    childChoice.Items.Add(ele);
            }

            content.Particle = sequence; // Sequence
            // Add the sequence to the content
            if (sequence.Items.Count > 0)
            {
                if (content.BaseTypeName != null && !String.IsNullOrEmpty(content.BaseTypeName.Name))
                    complexType.ContentModel = new XmlSchemaComplexContent() { Content = content };
                else
                    complexType.Particle = sequence;
            }

            return complexType;

        }

        /// <summary>
        /// Create choice content
        /// </summary>
        private List<XmlSchemaElement> CreateChoice(Choice chc, List<TypeReference> genericParameters, string prefix, List<string> includedModels, List<TypeReference> additionalCompileTargets)
        {
            List<XmlSchemaElement> retVal = new List<XmlSchemaElement>();

            // Iterate through the child classes
            foreach (ClassContent cc in chc.Content)
            {
                if (cc is Property)
                    retVal.Add(CreateElement(cc as Property, genericParameters, prefix, includedModels, additionalCompileTargets));
                else if (cc is Choice)
                    retVal.AddRange(CreateChoice(cc as Choice, genericParameters, prefix, includedModels, additionalCompileTargets));
            }

            return retVal;
        }

        /// <summary>
        /// Generate a choice structure for all child classes of <paramref name="property"/>
        /// </summary>
        private List<XmlSchemaElement> CreateChoice(Property property, Class cls, List<TypeReference> genericParameters, string prefix, List<string> includedModels, List<TypeReference> additionalCompileTargets)
        {
            List<XmlSchemaElement> retVal = new List<XmlSchemaElement>();

            // Iterate through the child classes
            foreach (TypeReference tr in cls.SpecializedBy)
            {
                if (tr.Class == null) continue; // Not interested in non-implementable classes!
                else if (tr.Class.IsAbstract && tr.Class.SpecializedBy.Count > 0) retVal.AddRange(CreateChoice(property, tr.Class, genericParameters, prefix, includedModels, additionalCompileTargets));

                // Clone the property and set the type reference to what we really want to set it up as
                Property choiceProperty = property.Clone() as Property;
                choiceProperty.MemberOf = property.MemberOf;
                choiceProperty.Type = tr;

                // Generate a choice element for this option
                XmlSchemaElement element = CreateElement(choiceProperty, genericParameters, prefix, includedModels, additionalCompileTargets);
                // Find the correct name for this element
                if (element.Name == property.Name && property.AlternateTraversalNames != null && property.AlternateTraversalNames.Count > 0)
                {
                    MohawkCollege.EHR.gpmr.COR.Property.AlternateTraversalData name = property.AlternateTraversalNames.Find(o => o.CaseWhen.Name == tr.Name &&
                                                                                       (o.InteractionOwner.Name == prefix || prefix == null));
                    if (name.TraversalName != null) element.Name = name.TraversalName;
                }
                else
                    element.Name = tr.Class.Name; 

                retVal.Add(element);
            }

            return retVal;
        }

        /// <summary>
        /// Create an element
        /// </summary>
        private XmlSchemaElement CreateElement(Property property, List<TypeReference> genericParameters, string prefix, List<String> includedModels, List<TypeReference> additionalCompileTargets)
        {
            // Return value
            XmlSchemaElement retVal = new XmlSchemaElement()
            {
                Name = property.Name,
                MaxOccursString = property.MaxOccurs == "*" ? "unbounded" : property.MaxOccurs,
                MinOccursString = property.MinOccurs
            };

            TypeReference propertyType = property.Type;
            List<String> collectionTypes = new List<string>() { "LIST","SET","DSET","BAG" };
            
            // Is the property type a collection
            if (property.MaxOccurs != "0" && collectionTypes.Contains(propertyType.CoreDatatypeName) && 
                propertyType.GenericSupplier != null &&
                propertyType.GenericSupplier.Count >0)
                propertyType = propertyType.GenericSupplier[0]; // It is, so we replace with the inner

            
            // Add appinfo for the collapsing needed
            if (property.Annotations != null && property.Annotations.Count > 0)
            {
                if (retVal.Annotation == null) retVal.Annotation = new XmlSchemaAnnotation();
                XmlSchemaAppInfo appInfo = new XmlSchemaAppInfo();
                XmlDocument appInfoDocument = new XmlDocument();
                appInfoDocument.AppendChild(appInfoDocument.CreateElement("annotations","http://marc.mohawkcollege.ca/hi"));
                foreach (Annotation annot in property.Annotations)
                {
                    XmlNode node = appInfoDocument.DocumentElement.AppendChild(appInfoDocument.CreateElement(annot.GetType().Name, "http://marc.mohawkcollege.ca/hi"));
                    node.InnerText = annot.ToString();
                    
                }
                appInfo.Markup = new XmlNode[] { appInfoDocument.DocumentElement };
                retVal.Annotation.Items.Add(appInfo);
            }

            // Generate the generic type parameter that has been bound upstream?
            if (property.Container is Class && (property.Container as Class).TypeParameters != null && property.Type.Name != null &&
                (property.Container as Class).TypeParameters.Find(o => o.ParameterName == property.Type.Name) != null)
            {
                // Get the current type
                Class containerClass = property.Container as Class;

                // Determine the type order of the generic parameter
                int order = containerClass.TypeParameters.FindIndex(o => o.ParameterName == property.Type.CoreDatatypeName);

                // Alternative traversal name usually happens on a generic
                if (property.AlternateTraversalNames != null && property.AlternateTraversalNames.Count > 0)
                {
                    MohawkCollege.EHR.gpmr.COR.Property.AlternateTraversalData name = property.AlternateTraversalNames.Find(o => o.CaseWhen.Name == genericParameters[order].Name &&
                                                                                        (o.InteractionOwner.Name == prefix || prefix==null));
                    if (name.TraversalName != null) retVal.Name = name.TraversalName;
                }

                // Now set the reference to the generated type
                if (prefix != null && genericParameters != null && genericParameters[order].GenericSupplier != null &&
                    genericParameters[order].GenericSupplier.Count > 0) // Prefix is here
                    retVal.SchemaTypeName = new XmlQualifiedName(String.Format("{0}.{1}", prefix, genericParameters[order].Name), targetNs);
                else if(genericParameters != null)
                {
                    // TODO: Validate this works
                    if (includedModels != null && genericParameters[order].Class != null && !includedModels.Contains(genericParameters[order].Class.ContainerName))
                        includedModels.Add(genericParameters[order].Class.ContainerName);
                    retVal.SchemaTypeName = new XmlQualifiedName(genericParameters[order].Name, targetNs);
                }
                else
                    Trace.WriteLine(String.Format("Generic parameters are not specified '{0}'...", property.Name), "error");
            }
            else if (property.Type.Class != null && property.Type.GenericSupplier != null && property.Type.GenericSupplier.Count > 0) // Other Generic
            {
                // Add this to the list of types that need to be generated
                additionalCompileTargets.Add(property.Type);
                // Now set the reference
                if (prefix != null) // Prefix is here
                    retVal.SchemaTypeName = new XmlQualifiedName(String.Format("{0}.{1}", prefix, property.Type.Name), targetNs);
                else
                {
                    // TODO: Validate this works
                    if (!includedModels.Contains(property.Type.Class.ContainerName))
                        includedModels.Add(property.Type.Class.ContainerName);
                    retVal.SchemaTypeName = new XmlQualifiedName(property.Type.Class.ContainerName, targetNs);
                }
            }
            else if (property.Type.Class != null) // Complex association
            {

                // Attempt to bind to the class

                // Genericized type reference, so we need to generate a new type reference
                if (property.Type.GenericSupplier != null && property.Type.GenericSupplier.Count > 0)
                    ;//TODO: Generate a new type here
                else
                {
                    if (!includedModels.Contains(property.Type.Class.ContainerName))
                        includedModels.Add(property.Type.Class.ContainerName);
                    retVal.SchemaTypeName = new XmlQualifiedName(property.Type.Name, targetNs);
                }
            }
            else if (propertyType.CoreDatatypeName != null) // Data Type
            {
                var dataType = dataTypes.Find(o => o.Name == MakeSchemaFriendlyCoreType(propertyType));
                if (dataType == null)
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Can't find data type '{0}' in the file '{1}'. Using xs:any...", propertyType.CoreDatatypeName, dataTypesXsd), "warn");
                    retVal.SchemaTypeName = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
                }
                else
                    retVal.SchemaTypeName = new XmlQualifiedName(dataType.Name, dataType.Namespace);
            }
            else
                System.Diagnostics.Trace.WriteLine(String.Format("Can't bind type '{0}' to element '{1}'. Can't determine type family.", property.Type.Name, property.Name), "warn");

            // Generate property documentation
            if (!Documentation.IsEmpty(property.Documentation))
            {
                if(retVal.Annotation == null) retVal.Annotation = new XmlSchemaAnnotation();

                XmlSchemaDocumentation documentation = new XmlSchemaDocumentation();
                XmlDocument documentationMarkup = new XmlDocument();
                documentationMarkup.LoadXml("<div xmlns=\"http://www.w3.org/1999/xhtml\">" + property.Documentation.ToString() + "</div>");
                documentation.Markup = new List<XmlNode>(documentationMarkup.ChildNodes.Cast<XmlNode>()).ToArray();
                retVal.Annotation.Items.Add(documentation);
            }

            return retVal;
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
        /// Parse <paramref name="property"/> and create an XmlSchemaObject for it
        /// </summary>
        private XmlSchemaAttribute CreateAttribute(Property property)
        {
            // property data type must be a data type
            if (property.Type.CoreDatatypeName == null)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Can't bind complex type '{0}' to attribute '{1}'.", property.Type.Name, property.Name), "warn");
                return null;
            }

            // Return value
            XmlSchemaAttribute retVal = new XmlSchemaAttribute()
            {
                Name = property.Name,
                Use = property.MaxOccurs == "0" ? XmlSchemaUse.Prohibited :
                    property.MinOccurs != "0" ? XmlSchemaUse.Required : XmlSchemaUse.Optional

            };

            // Fixed value
            if (property.FixedValue != null)
                retVal.FixedValue = property.FixedValue;

            // Is this bound to a vocab realm?
            if (property.SupplierDomain != null && property.SupplierStrength == Property.CodingStrengthKind.CodedNoExtensions)
                // Generate the type as a ref to the voc realm
                retVal.SchemaTypeName = new XmlQualifiedName(property.SupplierDomain.Name, targetNs);
            else
            {
                var dataType = dataTypes.Find(o => o.Name == property.Type.CoreDatatypeName.ToLower());
                if (dataType == null)
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Can't find data type '{0}' in the file '{1}'. Using xs:token...", property.Type.CoreDatatypeName, dataTypesXsd), "warn");
                    retVal.SchemaTypeName = new XmlQualifiedName("token", "http://www.w3.org/2001/XMLSchema");
                }
                else
                    retVal.SchemaTypeName = new XmlQualifiedName(dataType.Name, dataType.Namespace);
            }

            // Generate property documentation
            if (!Documentation.IsEmpty(property.Documentation))
            {
                XmlSchemaDocumentation documentation = new XmlSchemaDocumentation();
                XmlDocument documentationMarkup = new XmlDocument();
                documentationMarkup.LoadXml("<div xmlns=\"http://www.w3.org/1999/xhtml\">" + property.Documentation.ToString() + "</div>");
                documentation.Markup = new List<XmlNode>(documentationMarkup.ChildNodes.Cast<XmlNode>()).ToArray();
                retVal.Annotation = new XmlSchemaAnnotation();
                retVal.Annotation.Items.Add(documentation);
            }

            // Return the retVal
            return retVal;

        }

        /// <summary>
        /// Generate the VOC.xsd file
        /// </summary>
        private void GenerateVocabulary()
        {
            System.Diagnostics.Trace.WriteLine("Generating VOC.XSD...", "debug");
            ClassRepository classRep = hostContext.Data["SourceCR"] as ClassRepository;

            // Enumerations
            var enumerations = from clsRep in classRep
                               where clsRep.Value is Enumeration && (clsRep.Value as Enumeration).Literals.Count > 0
                               select clsRep.Value as Enumeration;

            XmlSchema schema = new XmlSchema();
            //schema.TargetNamespace = targetNs;
            
            // Enumerations
            foreach (var eType in enumerations)
            {
                #region Check for members
                bool used = false;
                // Does this enumeration have any references?
                foreach (KeyValuePair<String, Feature> kv in eType.MemberOf)
                    if (kv.Value is Class)
                        foreach (ClassContent cc in (kv.Value as Class).Content)
                            used |= cc is Property &&
                                (cc as Property).SupplierDomain == eType;
                #endregion Check for Members

                // Is this enumeration used?
                if (used)
                {
                    // Generate the type 
                    XmlSchemaSimpleType enumerationType = new XmlSchemaSimpleType();
                    enumerationType.Name = eType.Name;

                    // Populate documentation
                    if (!Documentation.IsEmpty(eType.Documentation))
                    {
                        XmlSchemaDocumentation documentation = new XmlSchemaDocumentation();
                        XmlDocument documentationMarkup = new XmlDocument();
                        documentationMarkup.LoadXml("<div xmlns=\"http://www.w3.org/1999/xhtml\">" + eType.Documentation.ToString() + "</div>");
                        documentation.Markup = new List<XmlNode>(documentationMarkup.ChildNodes.Cast<XmlNode>()).ToArray();
                        enumerationType.Annotation = new XmlSchemaAnnotation();
                        enumerationType.Annotation.Items.Add(documentation);
                    }

                    // Populate the simple type
                    XmlSchemaSimpleTypeRestriction restriction = new XmlSchemaSimpleTypeRestriction() { BaseTypeName = new XmlQualifiedName("token", "http://www.w3.org/2001/XMLSchema") };

                    // Create the restriction facets
                    foreach (var literal in eType.Literals)
                    {
                        XmlSchemaEnumerationFacet facet = new XmlSchemaEnumerationFacet() { Value = literal.Name };

                        // Populate documentation
                        if (!Documentation.IsEmpty(literal.Documentation))
                        {
                            try
                            {
                                XmlSchemaDocumentation facetDocumentation = new XmlSchemaDocumentation();
                                XmlDocument facetDocumentationMarkup = new XmlDocument();
                                facetDocumentationMarkup.LoadXml("<div xmlns=\"http://www.w3.org/1999/xhtml\">" + literal.Documentation.ToString() + "</div>");
                                facetDocumentation.Markup = new List<XmlNode>(facetDocumentationMarkup.ChildNodes.Cast<XmlNode>()).ToArray();
                                facet.Annotation = new XmlSchemaAnnotation();
                                facet.Annotation.Items.Add(facetDocumentation);
                            }
                            catch (Exception e)
                            {
                                if (hostContext.Mode == Pipeline.OperationModeType.Quirks)
                                    System.Diagnostics.Trace.WriteLine(String.Format("error {0} is being ignored", e.Message), "quirks");
                                else
                                    throw;
                            }
                        }

                        restriction.Facets.Add(facet);
                    }

                    enumerationType.Content = restriction;

                    // Now, add the type to the schema
                    schema.Items.Add(enumerationType);
                }
            }

            TextWriter tw = null;
            try
            {
                tw = File.CreateText(Path.Combine(outputDir, "voc.xsd"));
                schema.Write(tw);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message, "error");
            }
            finally
            {
                if (tw != null) tw.Close();
            }
        }

        /// <summary>
        /// Progress for the loading
        /// </summary>
        void xss_Loading(object Sender, float perc)
        {
            if ((int)(perc * 100) % 5 == 1)
                System.Diagnostics.Trace.Write(".", "debug");
        }

        /// <summary>
        /// Process the parameters passed on the command line
        /// </summary>
        private void ProcessParameters()
        {
            Dictionary<String, StringCollection> parameters = hostContext.Data["CommandParameters"] as Dictionary<String, StringCollection>;
            StringCollection tCollection = null;

            // ITS Target Namespace parameter
            if (parameters.TryGetValue("xsd-target-ns", out tCollection))
                targetNs = tCollection[0];
            // XSLT
            if(parameters.TryGetValue("xsd-xslt", out tCollection))
                xslt = Convert.ToBoolean(tCollection[0]);
            // Gen Vocab
            if(parameters.TryGetValue("xsd-gen-vocab", out tCollection))
                genVocab = Convert.ToBoolean(tCollection[0]);
            if (parameters.TryGetValue("xsd-version", out tCollection))
                itsString = tCollection[0];
            if(parameters.TryGetValue("xsd-datatypes-xsd", out tCollection))
                dataTypesXsd = tCollection[0];
            else
                throw new InvalidOperationException("The --xsd-datatypes parameter must be specified!");
            if (parameters.TryGetValue("xsd-rim-xslt", out tCollection))
                xsltRim = Convert.ToBoolean(tCollection[0]);
            outputDir = hostContext.Output;
        }

        /// <summary>
        /// Display help
        /// </summary>
        public override string Help
        {
            get
            {

                StringBuilder helpString = new StringBuilder();
                foreach (String[] helpData in helpText)
                    helpString.AppendFormat("{0}{2}{1}\r\n", helpData[0],
                        helpData[1].Length > 55 ? helpData[1].Substring(0, 54) + "\r\n\t" + helpData[1].Substring(54) : helpData[1],
                        new string(' ', 25 - helpData[0].Length));

                helpString.Append("\r\nExample:\r\ngpmr -v 7 -s mif/*.mif -r XSD -o .\\output --xsd-version=XML_1.0 \r\n");
                return helpString.ToString();
            }
        }
    }
}