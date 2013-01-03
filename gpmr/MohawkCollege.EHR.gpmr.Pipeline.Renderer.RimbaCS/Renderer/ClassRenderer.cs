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
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Interfaces;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Attributes;
using System.IO;
using MohawkCollege.EHR.gpmr.COR;
using MARC.Everest.Attributes;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.HeuristicEngine;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Renderer
{
    /// <summary>
    /// Summary of ClassRenderer
    /// </summary>
    /// <remarks>
    /// Todo: These features are not supported, but are planned for future releases of GPMR
    /// - State Transitions
    /// - State Attributes
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Renderer"), FeatureRenderer(Feature = typeof(MohawkCollege.EHR.gpmr.COR.Class), IsFile = true)]
    public class ClassRenderer : IFeatureRenderer
    {

        /// <summary>
        /// Structure contains the factory method information
        /// </summary>
        private struct FactoryMethodInfo
        {
            public string documentation;
            public string signature;
            public List<String> parameters;
            public string name;
            public FactoryMethodInfo(string name, string documentation, string signature)
            {
                this.name = name;
                this.documentation = documentation;
                this.signature = signature;
                this.parameters = new List<string>();
            }
        }

        // Factory methods for each type. The dictionary is used to keep track of which factory methods
        // create which types. Example
        // KEY                              Value
        // MCCI_MTxxxxxxCA.Patient3         COCT_MTxxxxxxxCA.CreatePatient(x,y,z);
        static Dictionary<string, List<FactoryMethodInfo>> factoryMethods = new Dictionary<string, List<FactoryMethodInfo>>();

        // Namespaces for which documnetation has been generated
        static List<string> nsDocGenerated = new List<string>();

        // Method signatures that have been decleared 
        private List<String> m_methodDeclarations = new List<string>();

        /// <summary>
        /// Create the API structure attribute for class <paramref name="cls"/>
        /// </summary>
        /// <param name="cls">The COR Class to create the structure attribute for</param>
        /// <returns>The structure attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private string CreateStructureAttribute(Class cls)
        {
            StringBuilder sb = new StringBuilder("[Structure(");
            sb.AppendFormat("Name = \"{0}\", StructureType = StructureAttribute.StructureAttributeType.MessageType, IsEntryPoint = {1})]", cls.Name, 
                cls.ContainerPackage.EntryPoint.Exists(o=>o.Name == cls.Name) ? "true" : "false");
            return sb.ToString();
        }

        /// <summary>
        /// Create a collapsed constructor. Basically this will collapse off useless nesting
        /// of this type's properies if necessary
        /// </summary>
        /// <param name="cls">The class to create a constructor for</param>
        /// <param name="ownerNs">The namespace of the class</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "ownerNs"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor")]
        private string CreateCollapsedConstructor(Class cls, String ownerNs)
        {
            // Is there only one useful thing in this class?
            Property pseudoProperty = new Property();
            pseudoProperty.Name = "this";
            pseudoProperty.Type = cls.CreateTypeReference();

            // Determine the collapsed name
            Stack<KeyValuePair<String, TypeReference>> collapseData = DetermineCollapsedName(pseudoProperty);
            if (collapseData.Count > 0)
            {
                StringWriter sw = new StringWriter();

                StringBuilder methodBody  = new StringBuilder();
                String methodParms = "", methodHelp = "";

                // Create datatypes that will be used
                string path = "this";
                while (collapseData.Count > 0)
                {
                    KeyValuePair<String, TypeReference> current = collapseData.Pop();

                    string dt = CreateDatatypeRef(current.Value, current.Value.Container as Property);
                        string pCasedName = Util.Util.PascalCase(current.Key) == dt ? current.Key :
                        Util.Util.PascalCase(current.Key) == Util.Util.PascalCase(cls.Name) ? Util.Util.PascalCase(current.Key) + (current.Value.Container as Property).Type.Class.Name : Util.Util.PascalCase(current.Key);

                    path += "." + pCasedName;

                    if (current.Key != "this")
                        methodBody.AppendFormat("\t\t\t{0} = new {1}();\r\n", path, CreateDatatypeRef(current.Value, new Property()));
                    if (collapseData.Count == 0) // Last item, generate factory method data
                    {
                        Dictionary<String, String[]> childFactoryMethod = CreateFactoryMethod(current.Value, path);
                        // Get mandatory stuff
                        methodBody.Append(childFactoryMethod["mandatory"][1]);
                        methodParms = childFactoryMethod["mandatory"][0];
                        methodHelp = childFactoryMethod["mandatory"][2];
                    }
                }

                if (String.IsNullOrEmpty(methodParms)) return "";

                // Now generate the constructor
                sw.WriteLine("\t\t/// <summary> CTOR Populates {0} </summary>", path);
                sw.WriteLine(methodHelp);
                sw.WriteLine("\t\tpublic {0}({1}) {{ ", Util.Util.PascalCase(cls.Name), methodParms.Remove(methodParms.Length - 1));
                sw.WriteLine(methodBody.ToString());
                sw.WriteLine("\t\t}");
                return sw.ToString();
            }

            return "";
        }

        /// <summary>
        /// Determins the name of a collapsable type
        /// </summary>
        /// <param name="p">The property to collapse</param>
        /// <returns>A stack of name/type references that dictates the name of the collapsed class and a type reference to the class</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private Stack<KeyValuePair<string, TypeReference>> DetermineCollapsedName(Property p)
        {

            // No Property
            if (p == null || p.Type.Class == null)
                return new Stack<KeyValuePair<string, TypeReference>>();

            // Does this property only have one useful sub-element
            int nChildren = 0;
            ClassContent onlyChild = null;
            foreach (ClassContent child in p.Type.Class.Content)
                if (child is Property && ((child as Property).FixedValue == null || (child as Property).PropertyType == Property.PropertyTypes.TraversableAssociation)
                    && child.MaxOccurs == "1") // Property with no fixed value or a choice 
                {
                    onlyChild = child;
                    nChildren++;
                }
            
            // Is there only one child?
            if (nChildren == 1 && (onlyChild as Property).Type.Class != null)
            {
                Stack<KeyValuePair<String, TypeReference>> childCall = DetermineCollapsedName(onlyChild as Property);
                childCall.Push(new KeyValuePair<String, TypeReference>(onlyChild.Name, (onlyChild as Property).Type));
                return childCall;
            }
            else
                return new Stack<KeyValuePair<String, TypeReference>>();
        }

        /// <summary>
        /// Creates a validation function that will return true if this class meets the minimum criteria for validation
        /// </summary>
        /// <param name="cls">The class to create the validate function for</param>
        /// <returns>The string that contains the body of the validate function</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private string CreateValidateFunction(Class cls)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("\t\t/// <summary> Validates that the current instance meets conformance rules specified in the model </summary>");
            sw.WriteLine("\t\t/// <returns>True if this instance is valid, false otherwise</returns>");
            sw.WriteLine("\t\t/// <remarks>Provides only basic validation functionality</remarks>");
            sw.WriteLine("\t\tpublic {0} bool Validate() {{\r\n\t\t\tbool isValid = {1};\r\n\t\t\tif(!isValid) return false;",
                "RIM." + Util.Util.PascalCase(cls.Name) == RimbaCsRenderer.RootClass ? "virtual" : "override", "RIM." + Util.Util.PascalCase(cls.Name) == RimbaCsRenderer.RootClass ? "true" : "base.Validate()");
            
            // Write the validation function
            foreach (ClassContent cc in cls.Content)
            {
                string pCasedName = String.Empty;
                if (cc is Property)
                {
                    pCasedName = CreatePascalCasedName(cc as Property);

                    TypeReference tr = Datatypes.MapDatatype((cc as Property).Type);
                    string dt = CreateDatatypeRef(tr, cc as Property);

                }
                else
                {
                    if ((cc as Choice).Content.Count == 0)
                    {
                        Trace.WriteLine("Skipping validation for choice element with no choices", "warn");
                        continue;
                    }
                    
                    pCasedName = CreatePascalCasedName(cc as Choice);
                }

                // If this property is mandatory or populated then it must have a valid value
                if (cc.Conformance == ClassContent.ConformanceKind.Mandatory || cc.Conformance == ClassContent.ConformanceKind.Populated)
                {
                    if (cc.MaxOccurs != "1")
                        sw.WriteLine("\t\t\tisValid &= this.{0} != null && this.{0}.Count <= {1} && this.{0}.Count >= {2};", pCasedName, cc.MaxOccurs == "*" ? "int.MaxValue" : cc.MaxOccurs, cc.MinOccurs);
                    else
                    {
                        sw.WriteLine("\t\t\t#region Validate {0}", pCasedName);
                        sw.WriteLine("\t\t\tif(this.{0} == null) return false; // automatically not valid", pCasedName);
                        sw.WriteLine("\t\t\telse {");
                        sw.WriteLine("\t\t\t\tSystem.Reflection.MethodInfo mi = this.{0}.GetType().GetMethod(\"Validate\");", pCasedName);
                        sw.WriteLine("\t\t\t\tisValid &= (mi != null) ? ((bool)mi.Invoke(this.{0}, null)) : true;", pCasedName);
                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\t#endregion");
                    }
                }
            }

            sw.WriteLine("\t\t\treturn isValid;\r\n\t\t}");

            return sw.ToString();
        }

        /// <summary>
        /// Create the equality method
        /// </summary>
        private string CreateEqualityMethod(Class cls)
        {

            string genericString = "";
            foreach (TypeParameter tp in cls.TypeParameters ?? new List<TypeParameter>())
                genericString += tp + ",";
            if(!String.IsNullOrEmpty(genericString))
                genericString = genericString.Substring(0, genericString.Length - 1);

            string dataReference = String.Format("{0}{1}",
                    Util.Util.PascalCase(cls.Name),
                    String.IsNullOrEmpty(genericString) ? "" : String.Format("<{0}>", genericString)
                );

            StringWriter swEquals = new StringWriter(),
                swHash = new StringWriter();

            // Method signatures
            swEquals.WriteLine("\t\t/// <summary>Overload of the equality determiner</summary>\r\n\t\tpublic bool Equals({0} other)\r\n\t\t{{\r\n\t\t\tbool equal = true;",
                dataReference
            );
            swHash.WriteLine("\t\t/// <summary>Overload of hash code</summary>\r\n\t\tpublic override int GetHashCode()\r\n\t\t{");
            swHash.WriteLine("\t\tint result = 1;");
            
            swEquals.WriteLine("\t\t\tif(other != null) {");
            foreach (var prop in cls.Content)
            {
                if (prop is Property)
                {
                    if (prop.MaxOccurs == "1" ||
                        (prop as Property).Type.CoreDatatypeName != null && Datatypes.IsCollectionType((prop as Property).Type))
                        swEquals.WriteLine("\t\t\t\tequal &= this.{0} != null ? this.{0}.Equals(other.{0}) : other.{0} == null;", CreatePascalCasedName(prop as Property));
                    else
                    {
                        swEquals.WriteLine("\t\t\t\tequal &= this.{0}.Count == other.{0}.Count;", CreatePascalCasedName(prop as Property));
                        swEquals.WriteLine("\t\t\t\tfor(int i = 0; i < (equal ? this.{0}.Count : 0); i++) equal &= this.{0}[i] != null ? this.{0}[i].Equals(other.{0}[i]) : other.{0}[i] == null;", CreatePascalCasedName(prop as Property));
                    }
                    swHash.WriteLine("result = 17 * result + ((this.{0} == null) ? 0 : this.{0}.GetHashCode());", CreatePascalCasedName(prop as Property));
                }
                else
                {
                    if ((prop as Choice).Content.Count == 0)
                    {
                        Trace.WriteLine("Skipping equality check for choice with 0 choice elements", "warn");
                        continue;
                    }
                    swEquals.WriteLine("\t\t\t\tequal &= this.{0} != null ? this.{0}.Equals(other.{0}) : other.{0} == null;", CreatePascalCasedName(prop as Choice));
                    swHash.WriteLine("result = 17 * result + ((this.{0} == null) ? 0 : this.{0}.GetHashCode());", CreatePascalCasedName(prop as Choice));
                }
            }
            swEquals.WriteLine("\t\t\t\treturn equal;");
            swEquals.WriteLine("\t\t\t}");
            swEquals.WriteLine("\t\t\treturn false;");
            swEquals.WriteLine("\t\t}");
            swEquals.WriteLine("\t\t/// <summary>Overload of the equality determiner</summary>\r\n\t\tpublic override bool Equals(Object obj)\r\n\t\t{");
            swEquals.WriteLine("\t\t\tif(obj is {0}) return Equals(obj as {0});", dataReference);
            swEquals.WriteLine("\t\t\treturn base.Equals(obj);");
            swEquals.WriteLine("\t\t}");

             
            swHash.WriteLine("\t\t\treturn result;\r\n\t\t}");

            return String.Format("{0}\r\n{1}", swEquals.ToString(), swHash.ToString());
        }

        /// <summary>
        /// Create the API Property attribute for a property
        /// </summary>
        /// <param name="property">The property to create an attribute for</param>
        /// <param name="indent">The level of indentation to apply</param>
        /// <returns>The property attribute to append to the code file</returns>
        /// <param name="ownerNs">The namespace of the owner class</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)")]
        private string CreatePropertyAttribute(Property property, string ownerNs, int indent, int propertySort)
        {
            StringWriter sw = new StringWriter();

            // Enumerator alt-traversals
            List<Property.AlternateTraversalData> altTraversal = new List<Property.AlternateTraversalData>();
            // Current
            altTraversal.Add(new Property.AlternateTraversalData() { CaseWhen = property.Type, TraversalName = property.Name });
            
            // Alternatives
            if (property.AlternateTraversalNames != null)
                foreach (Property.AlternateTraversalData kv in property.AlternateTraversalNames)
                    altTraversal.Add(kv);
            //if(property.Type.Class != null && property.class.

            // Traversal names already rendered
            List<string> alreadyRendered = new List<string>();

            // Write properties
            foreach (Property.AlternateTraversalData kv in altTraversal)
            {
                if (kv.CaseWhen.Class != null && kv.CaseWhen.Class.IsAbstract)
                    continue;

                // Datatype
                TypeReference tr = Datatypes.MapDatatype(kv.CaseWhen);

                string key = string.Format("{0}.{1}.{2}.{3}", ownerNs, tr.Name, kv.TraversalName, kv.InteractionOwner != null ? kv.InteractionOwner.Name : "");

                if (!alreadyRendered.Contains(key))
                {
                    // Get a property reference
                    sw.Write("{3}[Property(Name = \"{0}\", PropertyType = PropertyAttribute.AttributeAttributeType.{1}, Conformance = PropertyAttribute.AttributeConformanceType.{2}, SortKey = {4}", kv.TraversalName, property.PropertyType == Property.PropertyTypes.Structural ? "Structural" : property.PropertyType == Property.PropertyTypes.NonStructural ? "NonStructural" : "Traversable", property.Conformance, new String('\t', indent), propertySort);

                    // Now for the fun part, 
                    // Now a type hint
                    if (tr.Class != null && (property.Container is Choice || property.AlternateTraversalNames != null))
                        sw.Write(", Type = typeof({0})", CreateDatatypeRef(tr, property));

                    // Now for an interaction hint
                    if (tr.Class != null && (property.Container is Choice || property.AlternateTraversalNames != null) && kv.InteractionOwner != null)
                        sw.Write(", InteractionOwner = typeof({0}.Interactions.{1})", ownerNs, kv.InteractionOwner.Name);
                    // Impose a flavor?
                    if (tr.Flavor != null)
                        sw.Write(", ImposeFlavorId = \"{0}\"", tr.Flavor);

                    // Is this a set?
                    if (property.MaxOccurs != "1")
                        sw.Write(", MinOccurs = {0}, MaxOccurs = {1}", property.MinOccurs, property.MaxOccurs == "*" ? "-1" : property.MaxOccurs);
                    if (property.MinLength != null)
                        sw.Write(", MinLength = {0}", property.MinLength);
                    if (property.MaxLength != null)
                        sw.Write(", MaxLength = {0}", property.MaxLength);

                    // Is there an update mode
                    if (property.UpdateMode != null)
                        sw.Write(", DefaultUpdateMode = UpdateMode.{0}", property.UpdateMode);

                    if (property.FixedValue != null)
                        sw.Write(", FixedValue = \"{0}\"", property.FixedValue);

                    // Is there a supplier domain?
                    if (property.SupplierDomain != null &&
                        property.SupplierStrength == MohawkCollege.EHR.gpmr.COR.Property.CodingStrengthKind.CodedNoExtensions)
                    {
                        var cd = property.SupplierDomain;
                        if (cd is ConceptDomain && (cd as ConceptDomain).ContextBinding != null &&
                            (cd as ConceptDomain).ContextBinding.Count == 1)
                            cd = (cd as ConceptDomain).ContextBinding[0];
                        sw.Write(", SupplierDomain = \"{0}\"", cd.ContentOid);
                    }

                    sw.WriteLine(")]");

                    // Realizations
                    // JF- 09/04/10 : Added to support the realization property
                    foreach (var realization in property.Realization ?? new List<ClassContent>())
                    {
                        if (realization == null) continue;
                        sw.Write("{1}[Realization(Name = \"{0}\"", realization.Name, new String('\t', indent));
                        if (RimbaCsRenderer.GenerateRim)
                            sw.Write(", OwnerClass = typeof({0}.RIM.{1})", ownerNs, realization.Container.Name);
                        sw.WriteLine(")]");
                    }
                    // Already rendered
                    alreadyRendered.Add(key);
                }
            }


            return sw.ToString();
        }

        /// <summary>
        /// Create a marker attribute
        /// </summary>
        /// <param name="indent">The indentation level of the code</param>
        /// <param name="markerType">The type of marker to create</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        private string CreateMarkerAttribute(MarkerAttribute.MarkerAttributeType markerType, int indent)
        {
            return String.Format("{0}[Marker(MarkerType = MarkerAttribute.MarkerAttributeType.{1})]\r\n", new String('\t', indent), markerType.ToString());
        }

        /// <summary>
        /// Create a datatype reference
        /// </summary>
        /// <param name="p">The proeprty to create a data type reference for</param>
        /// <param name="tr">The type reference to create the data type reference</param>
        /// <returns>The data type reference as created in code</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        internal static string CreateDatatypeRef(TypeReference tr, Property p)
        {
            tr = HeuristicEngine.Datatypes.MapDatatype(tr);
            string retVal = tr is TypeParameter && tr.Name == null ? Util.Util.MakeFriendly((tr as TypeParameter).ParameterName) : 
                tr.Class == null ? tr.Name : String.Format("{0}.{1}", tr.Class.ContainerName, Util.Util.PascalCase(tr.Class.Name));
            
            // Domain for a code?
            if (p != null &&
                p.SupplierDomain != null &&
                Datatypes.IsCodeType(tr)
                && !String.IsNullOrEmpty(EnumerationRenderer.WillRender(p.SupplierDomain as Enumeration)) &&
                p.SupplierStrength == Property.CodingStrengthKind.CodedNoExtensions )
            {
                EnumerationRenderer.MarkAsUsed(p.SupplierDomain);

                // Since we are going to reference it, we better ensure that any value we use will have it in the
                // domain
                if (p.FixedValue != null && p.FixedValue.Length > 0 &&
                    (p.SupplierDomain as Enumeration).GetEnumeratedLiterals().Find(o => o.Name == p.FixedValue) == null)
                {
                    Enumeration.EnumerationValue ev = new Enumeration.EnumerationValue();
                    ev.Name = p.FixedValue;
                    ev.Documentation = new Documentation();
                    ev.Documentation.Description = new List<string>() { "Added automatically because a property's fixed value referenced this mnemonic" };
                    (p.SupplierDomain as Enumeration).GetEnumeratedLiterals().Add(ev);
                }

                // Bind if appropriate
                if(!String.IsNullOrEmpty(HeuristicEngine.Datatypes.GetBuiltinVocabulary(p.SupplierDomain.Name)))
                    retVal += "<" + HeuristicEngine.Datatypes.GetBuiltinVocabulary(p.SupplierDomain.Name) + ">";
                else
                    retVal += "<Vocabulary." + Util.Util.MakeFriendly(EnumerationRenderer.WillRender(p.SupplierDomain)) + ">";
            }


            // Generics?
            if (tr.Class != null && tr.Class.TypeParameters != null)
            {
                retVal += "<";
                foreach (var parm in tr.Class.TypeParameters)
                {
                    List<TypeReference> tp = tr.GenericSupplier == null ? null : tr.GenericSupplier.FindAll(o => (o as TypeParameter).ParameterName.Equals(parm.ParameterName));
                    if (tp == null || tp.Count != 1) // nothing is bound to a property reference
                    {
                        var objRef = new TypeReference() { Name = "System.Object" };
                        retVal += CreateDatatypeRef(objRef, p) + ",";
                    }
                    else
                        retVal += CreateDatatypeRef(tp[0], p) + ",";
                }

                retVal = retVal.Substring(0, retVal.Length - 1);
                retVal += ">";
            }
            else if(tr.GenericSupplier != null && tr.GenericSupplier.Count > 0 && !retVal.Contains("<"))
            {
                retVal += "<";
                foreach (TypeReference gr in tr.GenericSupplier)
                    retVal += CreateDatatypeRef(gr, p) + ",";
                retVal = retVal.Substring(0, retVal.Length - 1);
                retVal += ">";
            }

            return retVal;
        }

        /// <summary>
        /// Create a .NET property 
        /// </summary>
        /// <remarks>hello</remarks>
        /// <param name="cc">The class content to render</param>
        /// <param name="ownerNs">The name of the owner namespace</param>
        /// <returns>The property string as it appears in C#</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private string CreateProperty(ClassContent cc, String ownerNs, int propertySort)
        {

            // JF: Supporting CeRX
            // There are some things where there is a choice that has no choices .. ?
            if (cc is Choice && (cc as Choice).Content.Count == 0)
            {
                Trace.WriteLine(String.Format("Choice '{0}' has no content, property will not be rendered!", cc.Name), "warn");
                return String.Empty;
            }

            StringWriter sw = new StringWriter();
            
            // Output documentation
            if (cc.Documentation != null)
                sw.Write(DocumentationRenderer.Render(cc.Documentation, 2).Replace("<summary>",String.Format("<summary>({0}) ", cc.Conformance)));
            else if (cc is Property && (cc as Property).Type.ClassDocumentation != null)
                sw.Write(DocumentationRenderer.Render((cc as Property).Type.ClassDocumentation, 2).Replace("<summary>", String.Format("<summary>({0}) ", cc.Conformance)));

            // Correct documentation
            if (sw.ToString().Length == 0)
                sw.WriteLine("\t\t/// <summary>({0}) {1}</summary>", cc.Conformance, cc.BusinessName == null ? "Documentation was not found" : cc.BusinessName.Replace("\r", "").Replace("\n","").Replace("&", "&amp;"));

            // Markers
            foreach (String s in Enum.GetNames(typeof(MarkerAttribute.MarkerAttributeType)))
                if (Util.Util.PascalCase(cc.Name) == s)
                    sw.Write(CreateMarkerAttribute((MarkerAttribute.MarkerAttributeType)Enum.Parse(typeof(MarkerAttribute.MarkerAttributeType), s), 2));

            // Determine property attributes
            if (cc is Property)
            {
                #region Properties
                Property property = cc as Property;

                // JF - 19/04/10 : This code is used to support the collapsing of C# classes
                foreach(var annotation in property.Annotations)
                    if (annotation is CodeCollapseAnnotation)
                    {
                        CodeCollapseAnnotation cca = annotation as CodeCollapseAnnotation;
                        sw.Write("\t\t[PropertyCollapse(Name = \"{0}\", Order = {1}", cca.Name, cca.Order);

                        string fixedValueString = ""; // Create the fixed value string
                        if(cca.OriginalType != null && cca.OriginalType.Class != null)
                            foreach(var originalContent in cca.OriginalType.Class.Content) // Iterate through the original type's members
                                if (originalContent is Property && (originalContent as Property).PropertyType == MohawkCollege.EHR.gpmr.COR.Property.PropertyTypes.Structural &&
                                    !String.IsNullOrEmpty((originalContent as Property).FixedValue)) // If the member is a structural property and the value is fixed then add
                                    fixedValueString += String.Format("{0}={1}|", originalContent.Name, (originalContent as Property).FixedValue);

                        if (!String.IsNullOrEmpty(fixedValueString)) // Set the fixed value string
                            sw.Write(", FixedAttributeValues = \"{0}\"", fixedValueString.Substring(0, fixedValueString.Length - 1));

                        // Now end the property
                        sw.WriteLine(")]");
                    }


                // Property attribute
                sw.Write(CreatePropertyAttribute(cc as Property, ownerNs, 2, propertySort));
                // Set browsing off for fixed values
                if (property.FixedValue != null && property.PropertyType != Property.PropertyTypes.TraversableAssociation)
                {
                    sw.WriteLine("\t\t[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]");
                    sw.WriteLine("\t\t[System.ComponentModel.ReadOnly(true)]");
                    sw.WriteLine("\t\t[System.ComponentModel.Browsable(false)]");
                }

                // Designer category, etc
                sw.WriteLine("\t\t[System.ComponentModel.Category(\"{0}\")]", property.Conformance);
                sw.WriteLine("\t\t[System.ComponentModel.Description(\"{0}\")]", Util.Util.StringEscape(property.BusinessName != null ? property.BusinessName.Replace("\n","").Replace("\r","") : property.Name));

                // Which type converter?
                TypeReference tr = Datatypes.MapDatatype((cc as Property).Type);

                sw.WriteLine("#if !WINDOWS_PHONE");
                if (property.SupplierDomain != null)
                    sw.WriteLine("\t\t[System.ComponentModel.TypeConverter(typeof(MARC.Everest.Design.DataTypeConverter))]");
                else
                {
                    sw.WriteLine("\t\t[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]");
                    if (!(property.MaxOccurs != "1" &&
                    (!Datatypes.IsCollectionType(tr))))
                        sw.WriteLine("\t\t[System.ComponentModel.Editor(typeof(MARC.Everest.Design.NewInstanceTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]");
                }
                // Default value
                if (property.DefaultValue != null)
                    sw.WriteLine("\t\t[System.ComponentModel.DefaultValue(\"{0}\")]", property.DefaultValue);
                sw.WriteLine("#endif");

                // Determine the datatype
                string dt = "";
                dt = CreateDatatypeRef(tr, property);

                sw.Write("\t\tpublic ");

                // Is it a list?
                if (property.MaxOccurs != "1" &&
                    (!Datatypes.IsCollectionType(tr)))
                    sw.Write("List<{0}>", dt);
                else
                    sw.Write(dt);

                // Property name
                // IF the property name is equal to the generic parameter, we don't pascal case it
                // IF the property name is equal to the containing class name, we use the name of the datatype it references as the property name

                // Is this a backing property
                string pName = CreatePascalCasedName(cc as Property);   
                sw.Write(" {0} {{", pName);

                // Property fixed
                if ((property.FixedValue != null || (property.DefaultValue != null && (property.Conformance == ClassContent.ConformanceKind.Populated || property.Conformance == ClassContent.ConformanceKind.Mandatory))) && property.PropertyType != Property.PropertyTypes.TraversableAssociation
                    ) // Type Reference for default value
                {
                    if (property.SupplierDomain != null &&
                        dt.Contains(Util.Util.MakeFriendly(property.SupplierDomain.Name) + ">") &&
                        property.SupplierStrength == MohawkCollege.EHR.gpmr.COR.Property.CodingStrengthKind.CodedNoExtensions) // supplier domain is known
                    {
                        // Get the real supplier (value set, or code system if concept domain)
                        var splrCd = property.SupplierDomain as ConceptDomain;
                        var bindingDomain = property.SupplierDomain;
                        Enumeration.EnumerationValue ev = null;
                        if (splrCd != null && splrCd.ContextBinding != null &&
                            splrCd.ContextBinding.Count == 1)
                            bindingDomain = splrCd.ContextBinding[0];

                        ev = bindingDomain.GetEnumeratedLiterals().Find(o => o.Name == (property.FixedValue ?? property.DefaultValue));

                        if (ev == null) // Enumeration value is not known in the enumeration, fixed value fails
                        {
                            System.Diagnostics.Trace.WriteLine(String.Format("Can't find literal '{0}' in supplier domain for property '{1}'", property.FixedValue ?? property.DefaultValue, property.Name), "error");
                            sw.WriteLine(" get; set; }");
                        }
                        else if(!Datatypes.IsCollectionType(tr)) // Fixed value is known
                            sw.WriteLine(" get {{ return __{3}; }} set {{ __{3} = value; }} }}\r\n\t\tprivate {2} __{3} = {0}.{1};",
                            Util.Util.MakeFriendly(EnumerationRenderer.WillRender(bindingDomain)), Util.Util.PascalCase(ev.BusinessName ?? ev.Name), dt, Util.Util.MakeFriendly(cc.Name));
                        else // Fixed value is known but it is some sort of collection
                            sw.WriteLine(" get {{ return __{2}; }} set {{ __{2} = value; }} }}\r\n\t\tprivate {1} __{2} = MARC.Everest.Connectors.Util.Convert<{1}>(\"{0}\", false);",
                            property.FixedValue ?? property.DefaultValue, CreateDatatypeRef(property.Type, property), Util.Util.MakeFriendly(cc.Name));
                    }
                    else if (property.Type.Class == null)
                        sw.WriteLine(" get {{ return __{2}; }} set {{ __{2} = value; }} }}\r\n\t\tprivate {1} __{2} = MARC.Everest.Connectors.Util.Convert<{1}>(\"{0}\", false);",
                            property.FixedValue ?? property.DefaultValue, CreateDatatypeRef(property.Type, property), Util.Util.MakeFriendly(cc.Name));
                    else
                        sw.Write(" get; set; }");  // Can't be cast
                }
                else if (property.MaxOccurs != "1" &&
                    (!Datatypes.IsCollectionType(tr)))
                    sw.WriteLine(" get {{ return __{0}; }} set {{ __{0} = value; }} }}\r\n\t\tprivate List<{1}> __{0} = new List<{1}>();", Util.Util.MakeFriendly(cc.Name), dt);
                else
                    sw.WriteLine(" get; set; }");
                #endregion
            }  // cc is property
            else // cc is choice
            {
                // Find the base type 
                TypeReference tr = new TypeReference();
                Choice choice = cc as Choice;

                if (cc.Documentation != null)
                    sw.Write(DocumentationRenderer.Render(cc.Documentation, 2));
                else
                {
                    sw.WriteLine("\t\t/// <summary>\r\n\t\t/// Choice Of: <list><listheader><term>Traversal</term><description>Class</description></listheader>");
                    foreach (Property p in choice.Content)
                        sw.WriteLine("\t\t/// <item><term>{1}</term><description>When class is <see cref=\"T:{2}.{0}\"/></description></item>", p.Type, p.Name, ownerNs);
                    sw.WriteLine("\t\t/// </list></summary>");
                }

                // Create property attributes
                foreach (Property p in choice.Content)
                    sw.Write(CreatePropertyAttribute(p, ownerNs, 2, propertySort));


                // Write editor attributes
                sw.WriteLine("#if !WINDOWS_PHONE");
                sw.WriteLine("\t\t[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]");
                if (!(cc.MaxOccurs != "1" && !Datatypes.IsCollectionType(tr)))
                    sw.WriteLine("\t\t[System.ComponentModel.Editor(typeof(MARC.Everest.Design.NewInstanceTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]");
                sw.WriteLine("#endif");
                // Get the type reference
                tr = (choice.Content[0] as Property).Type.Class.BaseClass;
                List<String> methods = new List<string>();

                foreach (Property p in choice.Content)
                {
                    // Set the property
                    if (p.Type.Class.BaseClass != null && tr != null && p.Type.Class.BaseClass.Name != tr.Name)
                        tr = null;

                    // Generate getter

                    // Generate a SETProperty method that sets for this permutation
                    StringBuilder method_sb = new StringBuilder("\t\t");
                    method_sb.AppendFormat("/// <summary> Get <see cref=\"P:$$pcName$$\"/> cast as an instance of <see cref=\"T:{0}\"/>, null if <see cref=\"P:$$pcName$$\"/> is not an instance of <see cref=\"T:{0}\"/></summary>\r\n", CreateDatatypeRef(p.Type, p));
                    method_sb.AppendFormat("public {0} $$pcName$$As{1}() {{ get {{ return this.$$pcName$$ as {0}; }} }}\r\n", CreateDatatypeRef(p.Type, p), Util.Util.MakeFriendly(p.Type.Name));
                    method_sb.AppendFormat("/// <summary> Set <see cref=\"P:$$pcName$$\"/> to an instance of <see cref=\"T:{0}\"/> </summary>\r\n", CreateDatatypeRef(p.Type, p));
                    method_sb.AppendFormat("\t\t/// <param name=\"value\">The value to set $$pcName$$ to</param>\r\n");
                    method_sb.AppendFormat("\t\tpublic void Set$$pcName$$({0} value) {{ this.$$pcName$$ = value; }}\r\n", CreateDatatypeRef(p.Type, p));
                    methods.Add(method_sb.ToString());

                    // Generate a shortcut SETProperty method
                    if (factoryMethods.ContainsKey(p.Type.Name))
                    {
                        foreach (FactoryMethodInfo mi in factoryMethods[p.Type.Name])
                        {
                            method_sb = new StringBuilder();
                            StringBuilder methodSig = new StringBuilder();
                            method_sb.AppendFormat("\r\n\t\tpublic void Set{0}(", Util.Util.PascalCase(cc.Name));
                            methodSig.AppendFormat("public void Set{0}(", Util.Util.PascalCase(cc.Name));
                            // Get the parameters
                            foreach (string parm in mi.parameters)
                                if (parm.Length > 0)
                                {
                                    string mParm = parm;
                                    method_sb.AppendFormat("{0},", mParm);
                                    methodSig.AppendFormat("{0},", mParm.Substring(0, mParm.LastIndexOf(" ")));
                                }

                            method_sb.Remove(method_sb.Length - 1, 1);

                            // Ensure that we don't declare the same method signature twice
                            if (m_methodDeclarations.Contains(methodSig.ToString()))
                                continue;

                            // Add a declaration
                            m_methodDeclarations.Add(methodSig.ToString());

                            // Insert the documentation
                            method_sb.Insert(0, mi.documentation);
                            method_sb.Insert(0, String.Format("/// <summary> Set <see cref=\"P:{0}\"/> to an instance of <see cref=\"T:{1}\"/> using the parameters specified</summary>\r\n", Util.Util.PascalCase(cc.Name), p.Type.Name));


                            method_sb.AppendFormat(") {{ this.$$pcName$$ = {0}(", mi.name);
                            foreach (var parm in mi.parameters)
                                if (parm.Length > 0) method_sb.AppendFormat("{0},", parm.Split(' ')[1]);
                            method_sb.Remove(method_sb.Length - 1, 1);
                            method_sb.Append("); }");
                            methods.Add(method_sb.ToString());
                        }
                    }
                }

                // If no type reference is agreed to, then the type reference is Object!
                string dt = tr == null ? "System.Object" : CreateDatatypeRef(tr, choice.Content[0] as Property);

                sw.Write("\t\tpublic ");

                // Is it a list?
                if (cc.MaxOccurs != "1" &&
                    (!Datatypes.IsCollectionType(tr)))
                    sw.Write("List<{0}>", dt);
                else
                    sw.Write(dt);

                // Property name
                // IF the property name is equal to the generic parameter, we don't pascal case it
                // IF the property name is equal to the containing class name, we use the name of the datatype it references as the property name
                string pName = CreatePascalCasedName(choice);

                sw.Write(" {0} {{ get; set; }}", pName);


                // Write the setters
                foreach (string s in methods)
                    sw.WriteLine(s.Replace("$$pcName$$", pName));
            }
                
            return sw.ToString(); 
        }

        /// <summary>
        /// Create pascal cased name
        /// </summary>
        private static string CreatePascalCasedName(Property property)
        {
            // Which type converter?
            TypeReference tr = Datatypes.MapDatatype((property as Property).Type);

            // Determine the datatype
            string dt = "";
            dt = CreateDatatypeRef(tr, property);

            string retVal = Util.Util.PascalCase(property.Name);
            if (retVal.Equals(dt))
                retVal = property.Name;
            else if ((retVal == property.Container.Name || retVal == Util.Util.PascalCase(property.Container.Name)))
            {
                // Find the last annotation 
                var candidateName = property.Annotations.FindLast(o => o is CodeCollapseAnnotation && (o as CodeCollapseAnnotation).Name != property.Container.Name);
                if (candidateName != null)
                    retVal = Util.Util.PascalCase((candidateName as CodeCollapseAnnotation).Name);
                else
                    retVal = "_" + retVal;
            }
            return retVal;
        }

        /// <summary>
        /// Renders the parameters and population statements for that variable
        /// </summary>
        /// <param name="clsRef">The class to create a factory method for</param>
        /// <param name="populateVarName">The variable to populate in code</param>
        /// <returns>A dictionary whereby the key references the constructor type and value references the parameters</returns>
        public static Dictionary<string, string[]> CreateFactoryMethod(TypeReference clsRef, string populateVarName)
        {
            return CreateFactoryMethod(clsRef, populateVarName, false);
        }

        /// <summary>
        /// Renders the parameters and population statements for that variable
        /// </summary>
        /// <param name="clsRef">The class to create a factory method for</param>
        /// <param name="populateVarName">The variable to populate in code</param>
        /// <returns>A dictionary whereby the key references the constructor type and value references the parameters</returns>
        /// <param name="bindObjectAsFallback">When set to true, the function will use System.Object to bind any generic arguments no bound. Should be false unless generating an interaction signature</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "methods"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static Dictionary<string, string[]> CreateFactoryMethod(TypeReference clsRef, string populateVarName, bool useBindings)
        {
            var cls = clsRef.Class;

            // Now for specialty constructors
            Dictionary<String, String[]> ctors = new Dictionary<string, String[]>();
            ctors.Add("structural", new string[] { "", "", "" });
            ctors.Add("mandatory", new string[] { "", "", "" });
            ctors.Add("required", new string[] { "", "", "" });
            ctors.Add("all", new string[] { "", "", "" });

            if (cls == null || cls.Content == null)
                return ctors;

            // Iterate through class content
            foreach (ClassContent cc in cls.Content)
            {
                //if (cc.Conformance == ClassContent.ConformanceKind.Mandatory || cc.MinOccurs != "0") // Mandatory ctor
                {
                    if (cc is Property && ((cc as Property).FixedValue == null || (cc as Property).PropertyType == Property.PropertyTypes.TraversableAssociation))
                    {
                        #region Property
                        Property p = cc as Property;
                        TypeReference tr = Datatypes.MapDatatype(p.Type);

                        

                        // Add generic suppliers
                        // The class is a generic parameter type if the name matches the name of a parameter
                        // of the container class, and the class is not "findable" by the type reference object
                        if (tr.Class == null && cls.TypeParameters != null && cls.TypeParameters.Find(o => o.ParameterName.Equals(tr.Name)) != null)
                        {
                            // Find the type parameter within this class
                            var typeParm = cls.TypeParameters.Find(o => o.ParameterName.Equals(tr.Name));
                            List<TypeReference> genSupplier = clsRef.GenericSupplier != null ? 
                                clsRef.GenericSupplier.FindAll(o => (o as TypeParameter).ParameterName.Equals(typeParm.ParameterName)) :
                                null;

                            // Case 1: There is a generic parameter for which we have no class bound
                            if (genSupplier != null && genSupplier.Count == 1)
                                tr = genSupplier[0];
                            else if(genSupplier != null || useBindings)
                                tr = new TypeReference() { Name = "System.Object" };
                                //foreach(var gs in genSupplier.GenericSupplier ?? new List<TypeReference>())
                                //    if(tr.GenericSupplier.Find(o=>(o as TypeParameter).ParameterName.Equals(tr.Name)) == null)
                                //        tr.AddGenericSupplier(tr.Name, gs.Clone() as TypeParameter);
                        }
                        else if (tr.Class != null && useBindings && tr.Class.TypeParameters != null) // the property has type 
                        {
                            tr.GenericSupplier.Clear();

                            foreach (var gp in tr.Class.TypeParameters)
                                if (clsRef.GenericSupplier != null && clsRef.GenericSupplier.FindAll(o => (o as TypeParameter).ParameterName == gp.ParameterName).Count == 1)
                                {
                                    tr.AddGenericSupplier(gp.ParameterName, clsRef.GenericSupplier.Find(o => (o as TypeParameter).ParameterName == gp.ParameterName));
                                }
                                else
                                {
                                    tr.AddGenericSupplier(gp.ParameterName, new TypeParameter() { Name = "System.Object", ParameterName = gp.ParameterName }); // bind to system.object
                                }

                        }

                        // Normalize the parameter name
                        string parmName = CreatePascalCasedName(p);
                        
                        if(parmName == CreateDatatypeRef(p.Type, p))
                            parmName = cc.Name;
                        else if(parmName == p.Container.Name && p.Type.Class != null)
                            parmName = Util.Util.PascalCase(cc.Name) + p.Type.Class.Name; // TODO: What if this is not true?
                        else if (parmName == cc.Container.Name && cc.Annotations.Count > 0)
                        {
                            // Find the last annotation 
                            var candidateName = cc.Annotations.FindLast(o => o is CodeCollapseAnnotation && (o as CodeCollapseAnnotation).Name != cc.Container.Name);
                            if (candidateName != null)
                                parmName = Util.Util.PascalCase((candidateName as CodeCollapseAnnotation).Name);
                            else
                                parmName = "_" + parmName;
                        }

                        // Variable initializor
                        string varInitValue = Util.Util.MakeFriendly(cc.Name);

                        // Documentation
                        string doc = String.Format("\t\t/// <param name=\"{0}\">({2}) {1}</param>\r\n", cc.Name, cc.Documentation != null && cc.Documentation.Definition != null && cc.Documentation.Definition.Count > 0 ? cc.Documentation.Definition[0] : "No documentation available", cc.Conformance);
                        if(RimbaCsRenderer.SuppressDoc)
                            doc = String.Format("\t\t/// <param name=\"{0}\">({2}) {1}</param>\r\n", cc.Name, cc.BusinessName != null ? cc.BusinessName.Replace("\n", "").Replace("\r", "").Replace("&", "&amp;") : "No documentation available", cc.Conformance);

                        // Is this a list?
                        if (p.MaxOccurs != "1" && (!Datatypes.IsCollectionType(tr)))
                        {
                            varInitValue = String.Format("new List<{0}>(new {0}[] {{ {1} }})",
                                CreateDatatypeRef(tr, p), varInitValue);
                        }

                        if (tr.Class != null && tr.Class.IsAbstract)
                            continue;

                        // Now the signature (Mandatory)
                        if (p.Conformance == ClassContent.ConformanceKind.Mandatory)
                        {
                            ctors["mandatory"][0] += string.Format("{0} {1},", CreateDatatypeRef(tr, p), Util.Util.MakeFriendly(cc.Name));
                            ctors["mandatory"][1] += string.Format("\t\t\t{1}.{0} = {2};\r\n", parmName, populateVarName, varInitValue);
                            ctors["mandatory"][2] += doc;
                        }
                        if (p.Conformance == ClassContent.ConformanceKind.Mandatory && (p.PropertyType == Property.PropertyTypes.Structural || p.PropertyType == Property.PropertyTypes.NonStructural)) // Structural
                        {
                            ctors["structural"][0] += string.Format("{0} {1},", CreateDatatypeRef(tr, p), Util.Util.MakeFriendly( cc.Name));
                            ctors["structural"][1] += string.Format("\t\t\t{1}.{0} = {2};\r\n", parmName, populateVarName, varInitValue);
                            ctors["structural"][2] += doc;
                        }
                        if (p.Conformance == ClassContent.ConformanceKind.Required || p.MinOccurs != "0" || p.Conformance == ClassContent.ConformanceKind.Mandatory)
                        {
                            // Required (default)
                            ctors["required"][0] += string.Format("{0} {1},", CreateDatatypeRef(tr, p), Util.Util.MakeFriendly(cc.Name));
                            ctors["required"][1] += string.Format("\t\t\t{1}.{0} = {2};\r\n", parmName, populateVarName, varInitValue);
                            ctors["required"][2] += doc;
                        }
                        ctors["all"][0] += string.Format("{0} {1},", CreateDatatypeRef(tr, p), Util.Util.MakeFriendly(cc.Name));
                        ctors["all"][1] += string.Format("\t\t\t{1}.{0} = {2};\r\n", parmName, populateVarName, varInitValue);
                        ctors["all"][2] += doc;

                        #endregion
                    }
                    else if(cc is Choice) // Choice
                    {

                        // Get the base datatype
                        Choice choice = cc as Choice;

                        // Skip
                        if (choice.Content.Count == 0)
                        {
                            Trace.WriteLine("Skipping choice property with no choices", "warn");
                            continue;
                        }

                        TypeReference tr = (choice.Content[0] as Property).Type.Class.BaseClass;
                        List<String> methods = new List<string>();
                        foreach (Property p in choice.Content)
                            if (p.Type.Class.BaseClass != null && tr != null && p.Type.Class.BaseClass.Name != tr.Name)
                                tr = null;

                        // Now the signature (Mandatory)
                        if (choice.Conformance == ClassContent.ConformanceKind.Mandatory)
                        {
                            ctors["mandatory"][0] += string.Format("{0} {1},", tr == null ? "System.Object" : CreateDatatypeRef(tr, new Property()), Util.Util.MakeFriendly(cc.Name));
                            ctors["mandatory"][1] += string.Format("\t\t\t{1}.{0} = {2};\r\n", CreatePascalCasedName(choice), populateVarName, Util.Util.MakeFriendly(cc.Name));
                            ctors["mandatory"][2] += String.Format("\t\t/// <param name=\"{0}\">({1}) Documentation was not found</param>\r\n", Util.Util.MakeFriendly(cc.Name), cc.Conformance);
                        }
                        if (cc.Conformance == ClassContent.ConformanceKind.Required || cc.MinOccurs != "0" || cc.Conformance == ClassContent.ConformanceKind.Mandatory)
                        {

                            // Required (default)
                            ctors["required"][0] += string.Format("{0} {1},", tr == null ? "System.Object" : CreateDatatypeRef(tr, new Property()), Util.Util.MakeFriendly(cc.Name));
                            ctors["required"][1] += string.Format("\t\t\t{1}.{0} = {2};\r\n", CreatePascalCasedName(choice), populateVarName, Util.Util.MakeFriendly(cc.Name));
                            ctors["required"][2] += String.Format("\t\t/// <param name=\"{0}\">({1}) Documentation was not found</param>\r\n", Util.Util.MakeFriendly(cc.Name), cc.Conformance);
                        }
                        ctors["all"][0] += string.Format("{0} {1},", tr == null ? "System.Object" : CreateDatatypeRef(tr, new Property()), Util.Util.MakeFriendly(cc.Name));
                        ctors["all"][1] += string.Format("\t\t\t{1}.{0} = {2};\r\n", CreatePascalCasedName(choice), populateVarName, Util.Util.MakeFriendly(cc.Name));
                        ctors["all"][2] += String.Format("\t\t/// <param name=\"{0}\">({1}) Documentation was not found</param>\r\n", Util.Util.MakeFriendly(cc.Name), cc.Conformance);

                    }
                }
            }

            // Return
            return ctors;
        }

        /// <summary>
        /// Create a pascal cased name
        /// </summary>
        private static string CreatePascalCasedName(Choice cc)
        {
            // Get the type reference
            var tr = (cc.Content[0] as Property).Type.Class.BaseClass;
            List<String> methods = new List<string>(), methodDeclarations = new List<string>();

            foreach (Property p in cc.Content)
                if (p.Type.Class.BaseClass != null && tr != null && p.Type.Class.BaseClass.Name != tr.Name)
                    tr = null;

            // If no type reference is agreed to, then the type reference is Object!
            string dt = tr == null ? "System.Object" : CreateDatatypeRef(tr, cc.Content[0] as Property);

            return Util.Util.PascalCase(cc.Name) == dt ? Util.Util.MakeFriendly(cc.Name) :
                   cc.Container != null && (Util.Util.PascalCase(cc.Name) == cc.Container.Name || Util.Util.PascalCase(cc.Name) == Util.Util.PascalCase(cc.Container.Name)) ? Util.Util.PascalCase(cc.Name) + "Choice" : Util.Util.PascalCase(cc.Name);

        }

        #region IFeatureRenderer Members

        /// <summary>
        /// Render the class from COR to C#
        /// </summary>
        /// <param name="apiNs">The namespace to render the api in</param>
        /// <param name="f">The feature (class) being rendered</param>
        /// <param name="OwnerNs">The namespace the class is to be rendered in</param>
        /// <param name="tw">The textwriter to write to</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor")]
        public void Render(String OwnerNs, String apiNs, MohawkCollege.EHR.gpmr.COR.Feature f, System.IO.TextWriter tw)
        {
            m_methodDeclarations = new List<string>();

            StringWriter sw = new StringWriter();
            
            // Make a strong typed reference to class
            Class cls = f as Class;

            #region Usings

            // Validate Usings
            string[] usings = new string[] { "Attributes", "Interfaces", "DataTypes" };
            foreach (string s in usings)
                sw.WriteLine("using {1}.{0};", s, apiNs);

            if (f.MemberOf.Find(o => o is Enumeration) != null)
                sw.WriteLine("using {0}.Vocabulary;", OwnerNs);


            #endregion

            #region Render

            sw.WriteLine("namespace {1}.{0} {{", cls.ContainerName, OwnerNs); // Start of Namespace

            // document the namespace if not already done
            if (!nsDocGenerated.Contains(cls.ContainerName))
            {
                if (cls.ContainerPackage.Documentation != null)
                {
                    sw.WriteLine("\t/// <summary>{0}</summary>", cls.ContainerPackage.BusinessName);
                    sw.WriteLine("\tinternal class NamespaceDoc { }");
                }
                nsDocGenerated.Add(cls.ContainerName);
            }

            // Documentation
            if (DocumentationRenderer.Render(cls.Documentation, 1).Length == 0)
                sw.WriteLine("\t/// <summary>{0}</summary>", cls.BusinessName == null ? "No documentation found" : cls.BusinessName.Replace("\r","").Replace("\n","").Replace("&","&amp;") );
            else
                sw.Write(DocumentationRenderer.Render(cls.Documentation, 1));

            #region Start Class
            // Structure attribute for the class
            sw.WriteLine("\t" + CreateStructureAttribute(cls));

            string genericString = "";
            foreach (TypeParameter tp in cls.TypeParameters ?? new List<TypeParameter>())
                genericString += tp + ",";
            
            // Open Class 
            sw.WriteLine("\t[System.ComponentModel.Description(\"{0}\")]", cls.BusinessName != null ? cls.BusinessName.Replace("\n", "").Replace("\r", "") : cls.Name);
            sw.WriteLine("\t#if !WINDOWS_PHONE");
            sw.WriteLine("\t[Serializable]");
            sw.WriteLine("\t#endif");
            sw.WriteLine("\t[System.CodeDom.Compiler.GeneratedCode(\"gpmr\",\"{0}\")]", Assembly.GetEntryAssembly().GetName().Version.ToString());

            // JF-19/04/10 : Used to support mapping back to RIM
            foreach (var realization in cls.Realizations ?? new List<TypeReference>())
            {
                sw.Write("\t[Realization(Name = \"{0}\"", realization.Class.Name);
                if (RimbaCsRenderer.GenerateRim)
                    sw.Write(", OwnerClass = typeof({0}.{1})", OwnerNs, realization.Name);
                sw.WriteLine(")]");
            }

            sw.Write("\tpublic {0}class {1}{2} : {3}.{4}, IGraphable, IEquatable<{1}{2}>\r\n#if !WINDOWS_PHONE\r\n, ICloneable\r\n#endif\r\n", 
                cls.IsAbstract ? "abstract " : "", Util.Util.PascalCase(cls.Name), genericString.Length == 0 ? "" : "<" + genericString.Substring(0, genericString.Length - 1) + ">",
                "RIM." + Util.Util.PascalCase(cls.Name) == RimbaCsRenderer.RootClass ? "System" : OwnerNs, "RIM." + Util.Util.PascalCase(cls.Name) == RimbaCsRenderer.RootClass ? "Object" : cls.BaseClass != null ? String.Format("{0}.{1}", cls.BaseClass.Class.ContainerName, Util.Util.PascalCase(cls.BaseClass.Class.Name)) : RimbaCsRenderer.RootClass); // Start of class structure

            // Determine which interfaces this class implements
            List<String> interfaces = MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.HeuristicEngine.Interfaces.MapInterfaces(cls);
            foreach (string s in interfaces)
                sw.Write(", {0}", s);
            
            sw.WriteLine("\r\n\t{");
            #endregion

            #region Object Reference Id (used for hashing)

            sw.WriteLine("\t\t// Object reference identifier");
            sw.WriteLine("\t\tprivate readonly object m_objectRefId = new object();");

            #endregion

            #region Properties

            int propertySort = 0;
            foreach (ClassContent cc in cls.Content)
                sw.WriteLine(CreateProperty(cc, OwnerNs, propertySort++));

            #endregion

            if(!cls.IsAbstract)
            {
                #region Constructors

                sw.WriteLine("\t\t/// <summary> Default CTOR </summary>");
                sw.WriteLine("\t\tpublic {0}() : base() {{ }}\r\n", Util.Util.PascalCase(cls.Name));

                Dictionary<String, String[]> ctors = CreateFactoryMethod(cls.CreateTypeReference(), "this");

                // Write CTOR
                List<String> wroteParms = new List<string>(); // Keep track of the parameters used
                foreach (KeyValuePair<String, String[]> kv in ctors)
                {
                    if (kv.Value[0].Length > 0 && !wroteParms.Contains(kv.Value[0]))
                    {
                        wroteParms.Add(kv.Value[0]);
                        sw.WriteLine("\t\t/// <summary>\r\n\t\t/// CTOR for all {0} elements\r\n\t\t/// </summary>", kv.Key);
                        sw.WriteLine(kv.Value[2]);
                        sw.WriteLine("\t\tpublic {0}({1}) : base() {{ \r\n\t\t{2}\t\t}}", Util.Util.PascalCase(cls.Name), kv.Value[0].Substring(0, kv.Value[0].Length - 1), kv.Value[1]);
                    }
                }
                
                #endregion

                //#region Generate Collapsed Constructor

                //sw.WriteLine(CreateCollapsedConstructor(cls, OwnerNs));

                //#endregion
            }

            // Is this an emtpy class that facilitates a choice?
            if (cls.SpecializedBy != null && cls.SpecializedBy.Count > 0 &&
                cls.IsAbstract)
            {
                #region Generate creator methods for each of the children

                foreach (TypeReference tr in CascadeSpecializers(cls.SpecializedBy))
                {

                    if (tr.Class == null || tr.Class.ContainerName == "RIM" && !RimbaCsRenderer.GenerateRim || 
                        tr.Class.IsAbstract)
                        continue;

                    Class child = tr.Class;

                    // Create factory for the child
                    Dictionary<String, String[]> ctors = CreateFactoryMethod(tr, "retVal");
                    // Write factory
                    foreach (var kv in ctors)
                    {

                        string methodSignature = String.Format("{1}.{0}.{2}.Create{3}", cls.ContainerName, OwnerNs, Util.Util.PascalCase(cls.Name), Util.Util.PascalCase(child.Name)),
                            publicName = methodSignature;
                        
                        // Regex for extracting the parameter type rather than the type/name
                        Regex parmRegex = new Regex(@"(([\w<>,.]*)\s(\w*)),?\s?");
                        MatchCollection parmMatches = parmRegex.Matches(kv.Value[0]);
                        foreach (Match match in parmMatches)
                            methodSignature += match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf(" "));


                        // JF: Added to protected against rendering the same factory method
                        if (m_methodDeclarations.Contains(methodSignature))
                            continue;
                        m_methodDeclarations.Add(methodSignature);

                        // Render if there is even any content
                        if (kv.Value[0].Length > 0)
                        {
                            sw.Write(DocumentationRenderer.Render(child.Documentation, 2));
                            sw.WriteLine("\t\t///<summary>Create a new instance of <see cref=\"T:{0}.{1}\"/></summary>\r\n{4}\t\tpublic static {0}.{1} Create{2}({3}) {{ ", OwnerNs, tr.Name, Util.Util.PascalCase(child.Name), kv.Value[0].Substring(0, kv.Value[0].Length - 1), kv.Value[2]);
                            sw.WriteLine("\t\t\t{0}.{1} retVal = new {0}.{1}();", OwnerNs, tr.Name);
                            sw.WriteLine("\t\t\t{0}", kv.Value[1]);
                            sw.WriteLine("\t\t\treturn retVal;");
                            sw.WriteLine("\t\t}");

                            if (!factoryMethods.ContainsKey(tr.Name))
                                factoryMethods.Add(tr.Name, new List<FactoryMethodInfo>());

                            FactoryMethodInfo myInfo = new FactoryMethodInfo(publicName, kv.Value[2], methodSignature);


                                //Match the regular expression below and capture its match into backreference number 1 «(([\w<>,]*?)\s(\w*),?\s?)»
                                //Match the regular expression below and capture its match into backreference number 2 «([\w<>,]*?)»
                                //Match a single character present in the list below «[\w<>,]*?»
                                //Between zero and unlimited times, as few times as possible, expanding as needed (lazy) «*?»
                                //A word character (letters, digits, etc.) «\w»
                                //One of the characters “<>,” «<>,»
                                //Match a single character that is a “whitespace character” (spaces, tabs, line breaks, etc.) «\s»
                                //Match the regular expression below and capture its match into backreference number 3 «(\w*)»
                                //Match a single character that is a “word character” (letters, digits, etc.) «\w*»
                                //Between zero and unlimited times, as many times as possible, giving back as needed (greedy) «*»
                                //Match the character “,” literally «,?»
                                //Between zero and one times, as many times as possible, giving back as needed (greedy) «?»
                                //Match a single character that is a “whitespace character” (spaces, tabs, line breaks, etc.) «\s?»
                                //Between zero and one times, as many times as possible, giving back as needed (greedy) «?»
                            foreach (Match match in parmMatches)
                                myInfo.parameters.Add(match.Groups[1].Value);

                            // ADd the factory signature to the dictionary
                            factoryMethods[tr.Name].Add(myInfo);
                        }

                    }
                }

                #endregion
            }

            #region Validate Function
            //if (!cls.IsAbstract)
                sw.WriteLine(CreateValidateFunction(cls));

            sw.WriteLine(CreateEqualityMethod(cls));
            #endregion

            #region Clone Function

            sw.WriteLine("\t\tpublic new object Clone() { return this.MemberwiseClone(); }");

            #endregion
            sw.WriteLine("\t}"); // End of Class
            sw.WriteLine("}"); // End of Namespace

            #endregion

            if (cls.ContainerName == "RIM" && ("RIM." + Util.Util.PascalCase(cls.Name) == RimbaCsRenderer.RootClass || RimbaCsRenderer.GenerateRim) ||
                cls.ContainerName != "RIM")
                tw.Write(sw); // Write string writer
            else
                throw new NotSupportedException("RIM Elements will not be rendered");

        }

        private IEnumerable<TypeReference> CascadeSpecializers(List<TypeReference> list)
        {
            List<TypeReference> retVal = new List<TypeReference>(list);
            foreach (var tr in list)
                if (tr.Class != null && tr.Class.SpecializedBy != null)
                    retVal.AddRange(CascadeSpecializers(tr.Class.SpecializedBy));
            return retVal;
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public string CreateFile(MohawkCollege.EHR.gpmr.COR.Feature f, string FilePath)
        {
            return Path.ChangeExtension(Path.Combine(Path.Combine(FilePath, (f as Class).ContainerName),Util.Util.PascalCase(f.Name)), "cs");
        }

        #endregion
    }
}
