using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Interfaces;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Attributes;
using MohawkCollege.EHR.gpmr.COR;
using System.IO;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.HeuristicEngine;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer
{
    /// <summary>
    /// This class represents a feature render that will emit Java code that can represent a class
    /// from the COR representation
    /// </summary>
    [FeatureRenderer(Feature = typeof(Class), IsFile = true)]
    public class ClassRenderer : IFeatureRenderer
    {

        /// <summary>
        /// Structure contains the factory method information
        /// </summary>
        internal struct FactoryMethodInfo
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

        // Method signatures that have been decleared 
        internal List<String> s_methodDeclarations = new List<string>();

        // Imports
        internal static List<String> s_imports = new List<String>(10);

        /// <summary>
        /// Render class content to class file
        /// </summary>
        private string RenderClassContent(ClassContent cc, string ownerPackage, int propertySort)
        {

            StringWriter sw = new StringWriter();

            // Render the backing field
            TypeReference backingFieldType = null;
            if (cc is Property)
            {
                // HACK: Java can't hide members, so what we need to do 
                //       is detect if the model changes data types between overridden
                //       classes.
                backingFieldType = GetBackingFieldTypeThroughChildren((cc as Property), cc.Container as Class);
            //    backingFieldType = (cc as Property).Type;

            }
            else
            {


                backingFieldType = ((cc as Choice).Content[0] as Property).Type;
                if (backingFieldType != null && backingFieldType.Class != null)
                    backingFieldType = backingFieldType.Class.BaseClass;
                else
                    backingFieldType = new TypeReference() { Name = null };
                foreach (var chc in (cc as Choice).Content)
                {
                    if (backingFieldType.Name == null) break; // Just going to use System.Object Anyways

                    Property chcProperty = chc as Property; // Cast a property

                    // This line ensures that a common root class can be used across all the choices
                    if (chcProperty.Type == null || chcProperty.Type.Class == null || chcProperty.Type.Class.BaseClass.Name != backingFieldType.Name)
                        backingFieldType = new TypeReference() { Name = null };
                }
            }
            
            
            // Backing field write
            sw.WriteLine("\t// Backing field for {0}", cc.Name);
            sw.Write("\tprivate ");
            
            // Fixed value?
            //if (cc is Property && !String.IsNullOrEmpty((cc as Property).FixedValue))
            //    sw.Write("final ");

            // Datatype reference
            string dtr = CreateDatatypeRef(backingFieldType, cc as Property, ownerPackage);
            bool initialize = false,
                isNativeCollection = false;
            if (cc.MaxOccurs != "1" &&
                    !Datatypes.IsCollectionType(backingFieldType))
            {
                dtr = string.Format("ArrayList<{0}>", dtr);
                initialize = true;
                isNativeCollection = true;
            }

            sw.Write("{0} m_{1}", dtr, Util.Util.MakeFriendly(cc.Name));

            if (initialize)
                sw.Write(" = new {0}()", dtr);

            string setterName = "set";

            // TODO: Fixed Values here
            var property = cc as Property;
            // Only render fixed values when:
            // 1. The property is a property
            // 2. The property has a fixed value
            // 3. The property is mandatory or populated
            // 4. The property is not a traversable association.
            if (property != null && !String.IsNullOrEmpty(property.FixedValue) && (property.Conformance == ClassContent.ConformanceKind.Populated || property.Conformance == ClassContent.ConformanceKind.Mandatory) &&
                property.PropertyType != Property.PropertyTypes.TraversableAssociation)
            {
                // Get the real supplier (value set, or code system if concept domain)
                var splrCd = property.SupplierDomain as ConceptDomain;
                var bindingDomain = property.SupplierDomain;
                Enumeration.EnumerationValue ev = null;
                if (splrCd != null && splrCd.ContextBinding != null &&
                    splrCd.ContextBinding.Count == 1)
                    bindingDomain = splrCd.ContextBinding[0];

                if(bindingDomain != null)
                    ev = bindingDomain.GetEnumeratedLiterals().Find(o => o.Name == (property.FixedValue ?? property.DefaultValue));

                bool wontRenderBd = bindingDomain != null ? String.IsNullOrEmpty(EnumerationRenderer.WillRender(bindingDomain)) : true;

                if (bindingDomain == null)
                    sw.Write(" = ({2})org.marc.everest.formatters.FormatterUtil.fromWireFormat(\"{1}\", {0}.class);",
                        backingFieldType.Name, property.FixedValue, dtr);
                else if (ev == null || wontRenderBd) // Enumeration value is not known in the enumeration, fixed value fails
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Can't find literal '{0}' in supplier domain for property '{1}'", property.FixedValue, property.Name), "error");
                    if (wontRenderBd) // wont be rendering binding domain, so best to just emit it
                    {
                        if(Datatypes.GetOverrideSetters(backingFieldType, property, ownerPackage).FirstOrDefault(o=>o.Parameters.Count == 2) != null)
                            sw.Write(" = new {0}(\"{1}\", {2})",
                                dtr, property.FixedValue, ev == null ? "null" : String.Format("\"{0}\"", ev.CodeSystem));
                        else
                            sw.Write(" = new {0}(\"{1}\")",
                                dtr, property.FixedValue, ev == null ? "null" : String.Format("\"{0}\"", ev.CodeSystem));
                    }
                    else
                        sw.Write(" = new {0}(new {2}(\"{3}\"))",
                            dtr, ownerPackage, Util.Util.MakeFriendly(EnumerationRenderer.WillRender(property.SupplierDomain)), property.FixedValue);
                }
                else // Fixed value is known
                    sw.Write(" = new {2}({0}.{1})",
                    Util.Util.MakeFriendly(EnumerationRenderer.WillRender(bindingDomain)), Util.Util.PascalCase(ev.BusinessName ?? ev.Name), dtr, ownerPackage);

                // Update setter name so the output method has the right naming convention
                setterName = "override";

            }

            sw.WriteLine(";");

            // Render the documentation
            sw.Write(DocumentationRenderer.Render(cc.Documentation, 1));

            // Render the property attribute
            sw.Write(RenderPropertyAttribute(cc, ownerPackage, propertySort));

            // JF: Are there any members in the parent that we're overriding?
            String modifier = "";
            if (cc.Container != null && cc.Container is Class)
            {
                var containerClassRef = (cc.Container as Class).BaseClass;
                while (containerClassRef != null && containerClassRef.Class != null)
                {
                    foreach (var content in containerClassRef.Class.Content)
                    {
                        if (content.ToString() == property.ToString())
                            modifier = "@Override";
                        //else if (content is Property && CreatePascalCasedName(content as Property) == pName ||
                        //    content is Choice && CreatePascalCasedName(content as Choice) == pName)
                        //    modifier = "new virtual";

                    }
                    containerClassRef = containerClassRef.Class.BaseClass;
                }

            }
            // Render the getter
            sw.Write(modifier);
            sw.WriteLine("\tpublic {0} get{1}() {{ return this.m_{2}; }}", dtr, Util.Util.PascalCase(cc.Name), Util.Util.MakeFriendly(cc.Name));

            // Render setters
            // Default setter
            sw.Write(DocumentationRenderer.Render(cc.Documentation, 1));

            sw.Write(modifier);
            sw.WriteLine("\tpublic void {3}{1}({0} value) {{ this.m_{2} = value; }}", dtr, Util.Util.PascalCase(cc.Name), Util.Util.MakeFriendly(cc.Name), setterName);

            // Now to render the helper methods that can set the backing property, these are convenience methods
            if (cc is Choice && !isNativeCollection)
            {
                // Now, return getAsMethods

                StringBuilder getTraversalFor = new StringBuilder();

                foreach (Property p in (cc as Choice).Content)
                {
                    if (p.Type.Class != null && p.Type.Class.IsAbstract) continue; // don't output abstract classes
                    getTraversalFor.AppendFormat("\t\tif(this.m_{0} instanceof {1}) return \"{2}\";\r\n", Util.Util.MakeFriendly(cc.Name), CreateDatatypeRef(p.Type, p, ownerPackage, false), p.Name);
                    sw.WriteLine("\t/** Gets the {0} property cast to {1} or null if {0} is not an instance of {1}. This happens when the traversal name is {2}. This convenience method saves the developer from casting */", Util.Util.PascalCase(cc.Name), Util.Util.MakeFriendly(p.Type.Name), p.Name);
                    sw.WriteLine("\tpublic {0} get{1}If{4}() {{ if(this.m_{3} instanceof {0}) return ({0})this.m_{3}; else return null; }}",
                        CreateDatatypeRef(p.Type, p, ownerPackage), Util.Util.PascalCase(cc.Name), Util.Util.MakeFriendly(p.Type.Name), Util.Util.MakeFriendly(cc.Name), Util.Util.PascalCase(p.Name));
                    sw.WriteLine("\t/** Sets the {0} property given the specified instance of {1}. */", Util.Util.PascalCase(cc.Name), Util.Util.MakeFriendly(p.Type.Name));
                    sw.WriteLine("\tpublic void {3}{1}({0} value) {{ this.m_{2} = value; }}", CreateDatatypeRef(p.Type, p, ownerPackage), Util.Util.PascalCase(cc.Name), Util.Util.MakeFriendly(cc.Name), setterName);
                }

                sw.WriteLine("\t/** Gets the actual traversal name used for the choice element {0}, null if traversal was not provided **/", Util.Util.PascalCase(cc.Name));
                sw.WriteLine("\tpublic String get{0}TraversalName() {{", Util.Util.PascalCase(cc.Name));
                sw.WriteLine(getTraversalFor);
                sw.WriteLine("\t\treturn null;");
                sw.WriteLine("\t}");
                ; // TODO: Factory methods and etc
            }
            else if (!isNativeCollection)
            {
                foreach (var sod in MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.HeuristicEngine.Datatypes.GetOverrideSetters(backingFieldType, cc as Property, ownerPackage))
                {
                    sw.Write(DocumentationRenderer.Render(cc.Documentation, 1));
                    sw.Write("\tpublic void {1}{0}(", Util.Util.PascalCase(cc.Name), setterName);
                    foreach (var parm in sod.Parameters)
                    {
                        sw.Write("{0} {1}", parm.DataType, parm.Name);
                        if (sod.Parameters.Last() != parm)
                            sw.Write(", ");
                    }
                    sw.Write(")");
                    if (sod.Throws != null && sod.Throws.Count > 0)
                    {
                        sw.Write(" throws ");
                        foreach (var thrw in sod.Throws)
                            sw.Write("{0} {1}", thrw.Type, thrw == sod.Throws.Last() ? "" : ",");
                    }
                    sw.WriteLine("{");
                    sw.WriteLine("\t\t{0}", sod.SetterText);
                    sw.WriteLine("\t\tthis.m_{0} = {1};", Util.Util.MakeFriendly(cc.Name), sod.ValueInstance.Name);
                    sw.WriteLine("\t}");
                }
            }

            return sw.ToString();

        }

        /// <summary>
        /// Gets a common backing type for a particular property type reference through children
        /// Handles when data types change through child implementations ( handled by .NET via NEW )
        /// </summary>
        private TypeReference GetBackingFieldTypeThroughChildren(Property property, Class searchClass)
        {
            TypeReference initialType = property.Type;
            foreach (var child in searchClass.SpecializedBy) // iterate through children
            {
                var childProp = child.Class.Content.Find(o => o.Name == property.Name);
                if (childProp is Property)
                {
                    TypeReference candidate = GetBackingFieldTypeThroughChildren(childProp as Property, child.Class);
                    if (!candidate.Name.Equals(initialType.Name))
                        initialType = new TypeReference() { Name = null };
                }
            }
            return initialType;
        }

        /// <summary>
        /// Render property attribute
        /// </summary>
        private String RenderPropertyAttribute(ClassContent cc, string ownerPackage, int propertySort)
        {

            StringWriter retBuilder = new StringWriter();

            // Represent as an option
            Choice options = cc as Choice;
            if(options == null)
                options = new Choice() {
                    Content = new List<ClassContent>() { cc as Property }
                };

            // Traversal names already rendered
            List<string> alreadyRendered = new List<string>();


            // Iterate through choices
            foreach (Property property in options.Content)
            {
                // Enumerator for alt-traversals
                List<Property.AlternateTraversalData> altTraversal = new List<Property.AlternateTraversalData>();

                altTraversal.Add(new Property.AlternateTraversalData() { CaseWhen = property.Type, TraversalName = property.Name });
                // Alternatives
                if (property.AlternateTraversalNames != null)
                    foreach (Property.AlternateTraversalData kv in property.AlternateTraversalNames)
                        altTraversal.Add(kv);

                // Write properties
                foreach (Property.AlternateTraversalData kv in altTraversal)
                {

                    // Datatype
                    TypeReference tr = Datatypes.MapDatatype(kv.CaseWhen);

                    string key = string.Format("{0}.{1}.{2}.{3}", ownerPackage, tr.Name, kv.TraversalName, kv.InteractionOwner != null ? kv.InteractionOwner.Name : "");

                    // Already rendered
                    if (!alreadyRendered.Contains(key))
                    {
                        retBuilder.Write("\t@Property(name = \"{0}\", conformance = ConformanceType.{1}, propertyType = PropertyType.{2}, sortKey = {3}",
                            kv.TraversalName, cc.Conformance.ToString().ToUpper(), (options.Content[0] as Property).PropertyType.ToString().ToUpper(), propertySort);

                        // Now a type hint
                        if (tr.Class != null && (property.Container is Choice || property.AlternateTraversalNames != null))
                            retBuilder.Write(", type = {0}.class", CreateDatatypeRef(tr, property, ownerPackage));

                        // Now for an interaction hint
                        if (tr.Class != null && (property.Container is Choice || property.AlternateTraversalNames != null) && kv.InteractionOwner != null)
                            retBuilder.Write(", interactionOwner = {0}.interaction.{1}.class", ownerPackage, kv.InteractionOwner.Name);
                        // Impose a flavor?
                        if (tr.Flavor != null)
                            retBuilder.Write(", imposeFlavorId = \"{0}\"", tr.Flavor);

                        // Is this a set?
                        if (property.MaxOccurs != "1")
                            retBuilder.Write(", minOccurs = {0}, maxOccurs = {1}", property.MinOccurs, property.MaxOccurs == "*" ? "-1" : property.MaxOccurs);
                        if (property.MinLength != null)
                            retBuilder.Write(", minLength = {0}", property.MinLength);
                        if (property.MaxLength != null)
                            retBuilder.Write(", maxLength = {0}", property.MaxLength);

                        // Is there an update mode
                        //if (property.UpdateMode != null)
                        //    retBuilder.Write(", defaultUpdateMode = UpdateMode.{0}", property.UpdateMode);

                        // Is there a supplier domain?
                        if (property.SupplierDomain != null &&
                            property.SupplierStrength == MohawkCollege.EHR.gpmr.COR.Property.CodingStrengthKind.CodedNoExtensions)
                            retBuilder.Write(", supplierDomain = \"{0}\"", (property.SupplierDomain.ContentOid));

                        // Fixed value
                        if (!String.IsNullOrEmpty(property.FixedValue))
                            retBuilder.Write(", fixedValue = \"{0}\"", property.FixedValue);

                        // generic supplier
                        if (property.Type != null && property.Type.GenericSupplier != null &&
                            property.Type.GenericSupplier.Count > 0)
                        {
                            retBuilder.Write(", genericSupplier = {");
                            foreach (var genSupp in property.Type.GenericSupplier)
                                if((property.Container as Class).TypeParameters == null ||
                                    !(property.Container as Class).TypeParameters.Exists(o=>o.Name == genSupp.Name))
                                    retBuilder.Write("{0}.class{1}", CreateDatatypeRef(genSupp, property, ownerPackage, false), genSupp == property.Type.GenericSupplier.Last() ? "" : ",");
                            retBuilder.Write("}");
                        }
                        else if (property.MaxOccurs != "1" && !Datatypes.IsCollectionType(property.Type)) // Array list so we still need to put this in
                        {
                            retBuilder.Write(", genericSupplier = {");
                            CreateDatatypeRef(property.Type, property, ownerPackage);
                            retBuilder.Write("}");
                        }

                        retBuilder.WriteLine("),");
                        alreadyRendered.Add(key);
                    }
                }
            }

            string retVal = retBuilder.ToString();
            retVal = retVal.Substring(0, retVal.LastIndexOf(","));
            retVal += "\r\n";
            if (alreadyRendered.Count > 1)
            {
                if (!s_imports.Contains("org.marc.everest.annotations.Properties"))
                    s_imports.Add("org.marc.everest.annotations.Properties");
                return String.Format("\t@Properties( value = {{\r\n{0}\t }})\r\n", retVal);
            }
            else
                return retVal;
        }

        /// <summary>
        /// Create the structure annotation
        /// </summary>
        private string CreateStructureAnnotation(Class cls)
        {
            return String.Format("@Structure(name = \"{0}\", structureType = StructureType.MESSAGETYPE, isEntryPoint = {1})", cls.Name, cls.ContainerPackage.EntryPoint.Exists(o => o.Name == cls.Name) ? "true" : "false");
        }

        /// <summary>
        /// Get supplier domain reference for the type
        /// </summary>
        private static string GetSupplierDomain(TypeReference tr, Property p, string ownerPackage)
        {
            tr = HeuristicEngine.Datatypes.MapDatatype(tr);
            if(p != null &&
                p.SupplierDomain != null &&
                new List<String>(new String[] { "CS", 
                    "CV", 
                    "CE", 
                    "CR", 
                    "CD" }).Contains(tr.Name)
                && !String.IsNullOrEmpty(EnumerationRenderer.WillRender(p.SupplierDomain)))
            {

                EnumerationRenderer.MarkAsUsed(p.SupplierDomain);

                // Since we are going to reference it, we better ensure that any value we use will have it in the
                // domain
                if (p.FixedValue != null && p.FixedValue.Length > 0 &&
                    p.SupplierDomain.Literals.Find(o => o.Name == p.FixedValue) == null)
                {
                    Enumeration.EnumerationValue ev = new Enumeration.EnumerationValue();
                    ev.Name = p.FixedValue;
                    p.SupplierDomain.Literals.Add(ev);
                }

                return Util.Util.MakeFriendly(EnumerationRenderer.WillRender(p.SupplierDomain));
            }
            return null;
        }

        /// <summary>
        /// Create a datatype reference
        /// </summary>
        /// <param name="p">The proeprty to create a data type reference for</param>
        /// <param name="tr">The type reference to create the data type reference</param>
        /// <returns>The data type reference as created in code</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        internal static string CreateDatatypeRef(TypeReference tr, Property p, string ownerPackage)
        {
            return CreateDatatypeRef(tr, p, ownerPackage, true);    
        }

        /// <summary>
        /// Create datatype reference
        /// </summary>
        /// <param name="tr">The datatype reference to emit</param>
        /// <param name="p">The property containing the datatype reference</param>
        /// <param name="ownerPackage">The package owner</param>
        /// <param name="emitGenerics">True if generic parameter should be emitted</param>
        private static string CreateDatatypeRef(TypeReference tr, Property p, string ownerPackage, bool emitGenerics)
        {
            tr = HeuristicEngine.Datatypes.MapDatatype(tr);
            string retVal = tr is TypeParameter && tr.Name == null ? Util.Util.MakeFriendly((tr as TypeParameter).ParameterName) :
                tr.Class == null ? tr.Name : String.Format("{0}", Util.Util.PascalCase(tr.Class.Name));

            if (tr.Class != null)
            {
               
                string import = String.Format("{0}.{1}.{2}", ownerPackage, tr.Class.ContainerName.ToLower(), Util.Util.PascalCase(tr.Class.Name));
                if (!s_imports.Exists(o => o.EndsWith(retVal))) // Ensure duplicate class isn't imported
                    s_imports.Add(import);
                else if (!s_imports.Contains(import)) // Ensure duplicate import isn't imported
                    retVal = String.Format("{0}.{1}.{2}", ownerPackage, tr.Class.ContainerName.ToLower(), retVal);

                 if (p != null && p.Container is Class && Util.Util.PascalCase(((Class)p.Container).Name) == retVal) // Property has the same name as the container class
                    retVal = import;
                
                // Correct the import
            }

            // Domain for a code?
            if (emitGenerics && !String.IsNullOrEmpty(GetSupplierDomain(tr, p, ownerPackage)))
            {
                string vocabDomain = GetSupplierDomain(tr, p, ownerPackage);

                if (String.IsNullOrEmpty(Datatypes.GetBuiltinVocabulary(vocabDomain)))
                {
                    // Output the import for vocab if it doesn't exist
                    string import = String.Format("{0}.vocabulary.{1}", ownerPackage, vocabDomain);

                    string containerName = p.Container is Choice ? (p.Container as Choice).Container.Name : p.Container.Name;

                    if (!vocabDomain.Equals(containerName) && !s_imports.Exists(o => o.EndsWith(vocabDomain))) // Ensure duplicate class isn't imported
                        s_imports.Add(import);
                    if (!s_imports.Contains(import)) // Didn't add the import so we must reference by full name
                        vocabDomain = import;
                }

                // Bind if appropriate
                retVal += String.Format("<{0}>", vocabDomain);
                
            }


            // Generics?
            if (emitGenerics && tr.Class != null && tr.Class.TypeParameters != null)
            {
                retVal += "<";
                foreach (var parm in tr.Class.TypeParameters)
                {
                    List<TypeReference> tp = tr.GenericSupplier == null ? null : tr.GenericSupplier.FindAll(o => (o as TypeParameter).ParameterName.Equals(parm.ParameterName));
                    if (tp == null || tp.Count != 1) // nothing is bound to a property reference
                    {
                        var objRef = new TypeReference() { Name = "java.lang.Object" };
                        retVal += CreateDatatypeRef(objRef, p, ownerPackage) + ",";
                    }
                    else
                        retVal += CreateDatatypeRef(tp[0], p, ownerPackage) + ",";
                }

                retVal = retVal.Substring(0, retVal.Length - 1);
                retVal += ">";
            }
            else if (emitGenerics && tr.GenericSupplier != null && tr.GenericSupplier.Count > 0 && !retVal.Contains("<"))
            {
                retVal += "<";
                foreach (TypeReference gr in tr.GenericSupplier)
                    retVal += CreateDatatypeRef(gr, p, ownerPackage) + ",";
                retVal = retVal.Substring(0, retVal.Length - 1);
                retVal += ">";
            }

            return retVal;
        }

        
        
        #region IFeatureRenderer Members

        /// <summary>
        /// Renders feature <paramref name="f"/> to <paramref name="tw"/> in the specified <paramref name="OwnerNS"/>
        /// </summary>
        /// <param name="apiNs">The namespace to render the api in</param>
        /// <param name="f">The feature (class) being rendered</param>
        /// <param name="ownerPackage">The package the class is to be rendered in</param>
        /// <param name="tw">The textwriter to write to</param>
        public void Render(string ownerPackage, string apiNs, MohawkCollege.EHR.gpmr.COR.Feature f, System.IO.TextWriter tw)
        {
            s_imports.Clear();
            // Validate arguments
            if (String.IsNullOrEmpty(ownerPackage))
                throw new ArgumentNullException("ownerPackage");
            if (String.IsNullOrEmpty(apiNs))
                throw new ArgumentNullException("apiNs");
            if (f == null || !(f is Class))
                throw new ArgumentException("Parameter must be of type Class", "f");

            // Create a local copy of the class
            Class cls = f as Class;

            StringWriter sw = new StringWriter();

            tw.WriteLine("package {0}.{1};", ownerPackage, cls.ContainerName.ToLower());


            #region Render Class Signature

            // Documentation
            if (DocumentationRenderer.Render(cls.Documentation, 0).Length == 0)
                sw.WriteLine("\t/** No Summary Documentation Found */");
            else
                sw.Write(DocumentationRenderer.Render(cls.Documentation, 0));

            // Create structure annotation
            sw.WriteLine(CreateStructureAnnotation(cls));

            // Create class signature
            sw.Write("public {1} class {0}", Util.Util.PascalCase(cls.Name), cls.IsAbstract ? "abstract" : "");

            // If class is generic class
            string genericString = String.Empty;
            foreach (TypeParameter tp in cls.TypeParameters ?? new List<TypeParameter>())
                genericString += tp + ",";
            if (!String.IsNullOrEmpty(genericString))
                sw.Write("<{0}>", genericString.Substring(0, genericString.Length - 1)); // get rid of trailing ,

            sw.Write(" extends ");

            // Base class? 
            if (String.Format("{0}.rim.", ownerPackage) + Util.Util.MakeFriendly(cls.Name) == RimbaJavaRenderer.RootClass) // This class is the root
                sw.Write("java.lang.Object");
            else if (cls.BaseClass != null)
                sw.Write("{0}.{1}.{2}", ownerPackage, cls.BaseClass.Class.ContainerName.ToLower(), Util.Util.PascalCase(cls.BaseClass.Class.Name));
            else
                sw.Write("{0}", RimbaJavaRenderer.RootClass);

            // Interfaces
            sw.Write(" implements IGraphable ");
            List<String> interfaces = MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.HeuristicEngine.Interfaces.MapInterfaces(cls, ownerPackage);
            if (interfaces.Count > 0)
            {
                sw.Write(",");
                foreach (string s in interfaces)
                    sw.Write("{0} {1}", s, s == interfaces.Last() ? "" : ",");
            }

            sw.WriteLine("{");

            #endregion

            #region Render Properties

            // Get the content up the tree

            List<ClassContent> content = new List<ClassContent>(cls.Content);
            var c = cls.BaseClass;
            while (c != null && c.Class != null)
            {
                foreach (var itm in c.Class.Content)
                {
                    if (content.Exists(o => o.Name == itm.Name)) continue;
                    var addItm = itm.Clone() as ClassContent;
                    addItm.Container = cls; // set the container
                    content.Add(addItm);
                }
                c = c.Class.BaseClass;
            }
            // Sort
            content.Sort(new ClassContent.Comparator());

            int propertySort = 0;
            foreach (ClassContent cc in content)
                sw.WriteLine(RenderClassContent(cc, ownerPackage, propertySort++));

            #endregion

            if (!cls.IsAbstract)
            {
                #region Constructors

                sw.WriteLine("\t/** Default CTOR */");
                sw.WriteLine("\tpublic {0}() {{ super(); }}\r\n", Util.Util.PascalCase(cls.Name));

                Dictionary<String, String[]> ctors = CreateFactoryMethod(cls.CreateTypeReference(), "this", ownerPackage);

                // Write CTOR
                List<String> wroteParms = new List<string>(); // Keep track of the parameters used
                foreach (KeyValuePair<String, String[]> kv in ctors)
                {
                    if (kv.Value[0].Length > 0 && !wroteParms.Contains(kv.Value[0]))
                    {
                        wroteParms.Add(kv.Value[0]);
                        sw.WriteLine("\t/** Constructor for all {0} elements\r\n\t * ", kv.Key);
                        sw.WriteLine(kv.Value[2]);
                        sw.WriteLine("\t*/");
                        sw.WriteLine("\tpublic {0}({1}) {{ \r\n\t\tsuper();\r\n{2}\t}}", Util.Util.PascalCase(cls.Name), kv.Value[0].Substring(0, kv.Value[0].Length - 1), kv.Value[1]);
                    }
                }

                #endregion

                //#region Generate Collapsed Constructor

                //sw.WriteLine(CreateCollapsedConstructor(cls, OwnerNs));

                //#endregion
            }

            // Move to a factory
            // Is this an emtpy class that facilitates a choice?
            if (cls.SpecializedBy != null && cls.SpecializedBy.Count > 0 &&
                cls.IsAbstract)
            {
                foreach (TypeReference tr in CascadeSpecializers(cls.SpecializedBy))
                {
                    // Is method
                    if (tr.Class == null || tr.Class.ContainerName == "RIM" && !RimbaJavaRenderer.GenerateRim ||
                        tr.Class.IsAbstract)
                        continue;


                    sw.WriteLine("\t\t/** Returns true if this abstract class instance is an instance of {0}.{1}.{2} */\r\n\t\tpublic boolean is{1}{2}() {{ ", ownerPackage, tr.Class.ContainerName, Util.Util.PascalCase(tr.Class.Name));
                    sw.WriteLine("\t\t\treturn this.getClass().getName().equals(\"{0}.{1}.{2}\");", ownerPackage, tr.Class.ContainerName.ToLower(), Util.Util.PascalCase(tr.Class.Name));
                    sw.WriteLine("\t\t}");
                }
            }
            //    // NB: This isn't possible in Java (at least I don't have time to work around Java's weirdness) so
            //    //      I'm going to disable it. This causes problems when
            //    //
            //    //      abstract A.A
            //    //      abstract B.B extends A.A
            //    //      C.C extends A.A
            //    //      B.C extends B.B
            //    //
            //    // Results in :
            //    //     A.A { C.C CreateC(); }
            //    //     B.B { B.C CreateC(); }
            //    //
            //    // Java freaks out because the return type of CreateC has changed.
            //    // The only thing I can think of is to blow out the method name...
            //    // wait a tick, I'll do that :@

            //    //#region Generate creator methods for each of the children

            //    //// NB: In Java apparently super classes' static methods are acceessable
            //    ////     in child classes which is different than .NET, so we're not going to cascade specializers
            //    foreach (TypeReference tr in cls.SpecializedBy)
            //    {

            //        if (tr.Class == null || tr.Class.ContainerName == "RIM" && !RimbaJavaRenderer.GenerateRim ||
            //            tr.Class.IsAbstract)
            //            continue;

            //        Class child = tr.Class;

            //        // Create factory for the child
            //        Dictionary<String, String[]> ctors = CreateFactoryMethod(tr, "retVal", ownerPackage);
            //        // Write factory
            //        foreach (var kv in ctors)
            //        {

            //            string methodSignature = String.Format("{1}.{0}.{2}.create{3}", cls.ContainerName, ownerPackage, Util.Util.PascalCase(cls.Name), Util.Util.PascalCase(child.Name)),
            //                publicName = methodSignature;

            //            // Regex for extracting the parameter type rather than the type/name
            //            Regex parmRegex = new Regex(@"(([\w<>,.]*)\s(\w*)),?\s?");
            //            MatchCollection parmMatches = parmRegex.Matches(kv.Value[0]);
            //            foreach (Match match in parmMatches)
            //                methodSignature += match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf(" "));


            //            // JF: Added to protected against rendering the same factory method
            //            if (s_methodDeclarations.Contains(methodSignature))
            //                continue;
            //            s_methodDeclarations.Add(methodSignature);

            //            // Render if there is even any content
            //            if (kv.Value[0].Length > 0)
            //            {
            //                string clsDoc = DocumentationRenderer.Render(child.Documentation, 1);

            //                string ctorClassName = String.Format("{0}.{2}.{1}", ownerPackage, tr.Class.Name, tr.Class.ContainerName.ToLower());
            //                //// import already exists?
            //                //if(!s_imports.Exists(o=>o.EndsWith(Util.Util.PascalCase(tr.Class.Name))))
            //                //{
            //                //    s_imports.Add(ctorClassName);
            //                //    ctorClassName = ctorClassName.Substring(ctorClassName.LastIndexOf(".") + 1);
            //                //}
            //                //if (s_imports.Contains(ctorClassName))
            //                //    ctorClassName = ctorClassName.Substring(ctorClassName.LastIndexOf(".") + 1);

            //                if (clsDoc.Contains("*/"))
            //                    sw.Write(clsDoc.Substring(0, clsDoc.LastIndexOf("*/")));
            //                sw.WriteLine("* This function creates a new instance of {5}.{1}\r\n\t {4}\r\n\t*/\t\n\tpublic static {0} create{6}{2}({3}) {{ ", ctorClassName, tr.Class.Name, Util.Util.PascalCase(child.Name), kv.Value[0].Substring(0, kv.Value[0].Length - 1), kv.Value[2], tr.Class.ContainerName.ToLower(), Util.Util.PascalCase(cls.Name));
            //                sw.WriteLine("\t\t{0} retVal = new {0}();", ctorClassName);
            //                sw.WriteLine("{0}", kv.Value[1]);
            //                sw.WriteLine("\t\treturn retVal;");
            //                sw.WriteLine("\t}");

            //                if (!factoryMethods.ContainsKey(tr.Name))
            //                    factoryMethods.Add(tr.Name, new List<FactoryMethodInfo>());

            //                FactoryMethodInfo myInfo = new FactoryMethodInfo(publicName, kv.Value[2], methodSignature);


            //                //Match the regular expression below and capture its match into backreference number 1 «(([\w<>,]*?)\s(\w*),?\s?)»
            //                //Match the regular expression below and capture its match into backreference number 2 «([\w<>,]*?)»
            //                //Match a single character present in the list below «[\w<>,]*?»
            //                //Between zero and unlimited times, as few times as possible, expanding as needed (lazy) «*?»
            //                //A word character (letters, digits, etc.) «\w»
            //                //One of the characters “<>,” «<>,»
            //                //Match a single character that is a “whitespace character” (spaces, tabs, line breaks, etc.) «\s»
            //                //Match the regular expression below and capture its match into backreference number 3 «(\w*)»
            //                //Match a single character that is a “word character” (letters, digits, etc.) «\w*»
            //                //Between zero and unlimited times, as many times as possible, giving back as needed (greedy) «*»
            //                //Match the character “,” literally «,?»
            //                //Between zero and one times, as many times as possible, giving back as needed (greedy) «?»
            //                //Match a single character that is a “whitespace character” (spaces, tabs, line breaks, etc.) «\s?»
            //                //Between zero and one times, as many times as possible, giving back as needed (greedy) «?»
            //                foreach (Match match in parmMatches)
            //                    myInfo.parameters.Add(match.Groups[1].Value);

            //                // ADd the factory signature to the dictionary
            //                factoryMethods[tr.Name].Add(myInfo);
            //            }

            //        }
            //    }

            //    //#endregion
            //}

            // Render equals
            sw.WriteLine(CreateEqualityMethod(cls));
            // End class
            sw.WriteLine("}");


            #region Render the imports
            string[] apiImports = { "annotations.*", "datatypes.*", "datatypes.generic.*", "interfaces.IGraphable" },
                jImports = { "java.lang.*", "java.util.*" };
            foreach (var import in apiImports)
                tw.WriteLine("import {0}.{1};", apiNs, import);
            foreach (var import in jImports)
                tw.WriteLine("import {0};", import);
            foreach (var import in s_imports)
            {
                if (!import.EndsWith(String.Format(".{0}", Util.Util.PascalCase(f.Name)))) 
                    tw.WriteLine("import {0};", import);
            }

            tw.WriteLine(sw.ToString());
            #endregion

            if (cls.ContainerName == "RIM" && String.Format("{0}.rim.{1}", ownerPackage, Util.Util.PascalCase(cls.Name)) != RimbaJavaRenderer.RootClass && !RimbaJavaRenderer.GenerateRim)
                throw new NotSupportedException("RIM Elements will not be rendered");

        }

        /// <summary>
        /// Creates a flat list of specialization class references from a nested list
        /// </summary>
        private IEnumerable<TypeReference> CascadeSpecializers(List<TypeReference> list)
        {
            List<TypeReference> retVal = new List<TypeReference>(list);
            foreach (var tr in list)
                if (tr.Class != null && tr.Class.SpecializedBy != null)
                    retVal.AddRange(CascadeSpecializers(tr.Class.SpecializedBy));
            return retVal;
        }


        /// <summary>
        /// Create the file in the specified path
        /// </summary>
        public string CreateFile(MohawkCollege.EHR.gpmr.COR.Feature f, string FilePath)
        {
            return Path.ChangeExtension(Path.Combine(Path.Combine(FilePath, (f as Class).ContainerName.ToLower()), Util.Util.PascalCase(f.Name)), "java");
        }

        #endregion

        /// <summary>
        /// Create the equality method
        /// </summary>
        private string CreateEqualityMethod(Class cls)
        {

            string genericString = "";
            foreach (TypeParameter tp in cls.TypeParameters ?? new List<TypeParameter>())
                genericString += tp + ",";
            if (!String.IsNullOrEmpty(genericString))
                genericString = genericString.Substring(0, genericString.Length - 1);

            string dataReference = String.Format("{0}{1}",
                    Util.Util.PascalCase(cls.Name),
                    String.IsNullOrEmpty(genericString) ? "" : String.Format("<{0}>", genericString)
                );

            StringWriter swEquals = new StringWriter(),
                swHash = new StringWriter();

            // Method signatures
            swEquals.WriteLine("\t\t/** Overload of the equality determiner */\r\n\t\tpublic boolean equals({0} other)\r\n\t\t{{\r\n\t\t\tboolean equal = true;",
                dataReference
            );
            swHash.WriteLine("\t\t/** Overload of hash code */\r\n\t\t@Override\r\n\t\tpublic int hashCode()\r\n\t\t{");
            swHash.WriteLine("\t\tint result = 31;");

            swEquals.WriteLine("\t\t\tif(other != null) {");
            foreach (var prop in cls.Content)
            {
                if (prop is Property)
                {
                    if (prop.MaxOccurs == "1" ||
                        (prop as Property).Type.CoreDatatypeName != null && Datatypes.IsCollectionType((prop as Property).Type))
                        swEquals.WriteLine("\t\t\t\tequal &= this.get{0}() != null ? this.get{0}().equals(other.get{0}()) : other.get{0}() == null;", Util.Util.PascalCase(prop.Name));
                    else
                    {
                        swEquals.WriteLine("\t\t\t\tequal &= this.get{0}().size() == other.get{0}().size();", Util.Util.PascalCase(prop.Name));
                        swEquals.WriteLine("\t\t\t\tfor(int i = 0; i < (equal ? this.get{0}().size() : 0); i++) equal &= this.get{0}().get(i) != null ? this.get{0}().get(i).equals(other.get{0}().get(i)) : other.get{0}().get(i) == null;", Util.Util.PascalCase(prop.Name));
                    }
                    swHash.WriteLine("result = 17 * result + ((this.get{0}() == null) ? 0 : this.get{0}().hashCode());", Util.Util.PascalCase(prop.Name));
                }
                else
                {
                    if ((prop as Choice).Content.Count == 0)
                    {
                        Trace.WriteLine("Skipping equality check for choice with 0 choice elements", "warn");
                        continue;
                    }
                    swEquals.WriteLine("\t\t\t\tequal &= this.get{0}() != null ? this.get{0}().equals(other.get{0}()) : other.get{0}() == null;", Util.Util.PascalCase(prop.Name));
                    swHash.WriteLine("result = 17 * result + ((this.get{0}() == null) ? 0 : this.get{0}().hashCode());", Util.Util.PascalCase(prop.Name));
                }
            }
            swEquals.WriteLine("\t\t\t\treturn equal;");
            swEquals.WriteLine("\t\t\t}");
            swEquals.WriteLine("\t\t\treturn false;");
            swEquals.WriteLine("\t\t}");
            swEquals.WriteLine("\t\t/** Overload of the equality determiner*/\r\n\t\t@Override\r\n\t\tpublic boolean equals(Object obj)\r\n\t\t{");

            // Replace Subject<Act> with Subject<?>
            if (cls.TypeParameters != null && cls.TypeParameters.Count > 0)
                dataReference = Util.Util.PascalCase(cls.Name);

            swEquals.WriteLine("\t\t\tif(obj instanceof {0}) return this.equals(({0})obj);", dataReference);
            swEquals.WriteLine("\t\t\treturn super.equals(obj);");
            swEquals.WriteLine("\t\t}");


            swHash.WriteLine("\t\t\treturn result;\r\n\t\t}");

            return String.Format("{0}\r\n{1}", swEquals.ToString(), swHash.ToString());
        }

        /// <summary>
        /// Create factory methods
        /// </summary>
        /// <summary>
        /// Renders the parameters and population statements for that variable
        /// </summary>
        /// <param name="clsRef">The class to create a factory method for</param>
        /// <param name="populateVarName">The variable to populate in code</param>
        /// <returns>A dictionary whereby the key references the constructor type and value references the parameters</returns>
        public static Dictionary<string, string[]> CreateFactoryMethod(TypeReference clsRef, string populateVarName, string ownerPackage)
        {
            return CreateFactoryMethod(clsRef, populateVarName, false, ownerPackage);
        }

        /// <summary>
        /// Renders the parameters and population statements for that variable
        /// </summary>
        /// <param name="clsRef">The class to create a factory method for</param>
        /// <param name="populateVarName">The variable to populate in code</param>
        /// <returns>A dictionary whereby the key references the constructor type and value references the parameters</returns>
        /// <param name="bindObjectAsFallback">When set to true, the function will use System.Object to bind any generic arguments no bound. Should be false unless generating an interaction signature</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "methods"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static Dictionary<string, string[]> CreateFactoryMethod(TypeReference clsRef, string populateVarName, bool useBindings, string ownerPackage)
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
                        TypeReference tr = p.Type;



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
                            else if (genSupplier != null || useBindings)
                                tr = new TypeReference() { Name = "java.lang.Object" };
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
                                    tr.AddGenericSupplier(gp.ParameterName, new TypeParameter() { Name = "java.lang.Object", ParameterName = gp.ParameterName }); // bind to system.object
                                }

                        }

                        // Normalize the parameter name
                        string parmName = Util.Util.PascalCase(p.Name);

                        // Variable initializor
                        string varInitValue = string.Empty, 
                            varInitType = Util.Util.MakeFriendly(cc.Name);

                        // Documentation
                        string doc = String.Format("\t * @param {0} ({2}) {1}\r\n", cc.Name, cc.Documentation != null && cc.Documentation.Definition != null && cc.Documentation.Definition.Count > 0 ? cc.Documentation.Definition[0] : "No documentation available", cc.Conformance);

                        // Is this a list?
                        if (p.MaxOccurs != "1" && !Datatypes.IsCollectionType(p.Type))
                        {
                            varInitValue = String.Format(".add({0})", varInitType);
                            varInitType = String.Format("new ArrayList<{0}>()",
                                CreateDatatypeRef(tr, p, ownerPackage));
                        }

                        if (tr.Class != null && tr.Class.IsAbstract)
                            continue;

                        // Since Jaba uses type erasure, and we don't want
                        // poor developers to see foo(CS<?> code) in their IDE,
                        // I mean c'mon they're already using Jaba which is horrible
                        // enough as it is. We'll change the supplier property
                        // so they can use that instead (ie: foo(NullFlavor code))
                        if (!String.IsNullOrEmpty(GetSupplierDomain(tr, p, ownerPackage)))
                        {
                            var setters = Datatypes.GetOverrideSetters(tr, p, ownerPackage);
                            // Find a setter override that has one parameter that matches
                            var setter = Array.Find(setters, o => o.Parameters != null && o.Parameters.Count == 1);
                            if (setter != null) // We found one, now capitilize on the opportunity
                            {
                                var varName = String.Format("d{0:N}", Guid.NewGuid());
                                string setterText = setter.SetterText;
                                setterText = setterText.Replace(setter.Parameters[0].Name, Util.Util.MakeFriendly(cc.Name)); // Replace all instance of the parameter to our parameter
                                setterText = setterText.Replace(setter.ValueInstance.Name, varName);
                                if (p.Conformance == ClassContent.ConformanceKind.Mandatory)                            
                                    ctors["mandatory"][1] += setterText;
                                if (p.Conformance == ClassContent.ConformanceKind.Mandatory && (p.PropertyType == Property.PropertyTypes.Structural || p.PropertyType == Property.PropertyTypes.NonStructural)) // Structural
                                    ctors["structural"][1] += setterText;
                                if (p.Conformance == ClassContent.ConformanceKind.Required || p.MinOccurs != "0" || p.Conformance == ClassContent.ConformanceKind.Mandatory)
                                    ctors["required"][1] += setterText;
                                ctors["all"][1] += setterText;

                                // Create the datatype reference so the import gets registered
                                CreateDatatypeRef(tr, p, ownerPackage);

                                if (p.MaxOccurs != "1" && !Datatypes.IsCollectionType(p.Type))
                                    varInitValue = String.Format(".add({0})", varName);
                                else
                                    varInitType = varName;

                                // Update the class signature
                                tr = new TypeReference()
                                {
                                    Name = setter.Parameters[0].DataType
                                };


                            }

                        }                            


                        // Now the signature (Mandatory)
                        if (p.Conformance == ClassContent.ConformanceKind.Mandatory)
                        {
                            ctors["mandatory"][0] += string.Format("{0} {1},", CreateDatatypeRef(tr, p, ownerPackage), Util.Util.MakeFriendly(cc.Name));
                            ctors["mandatory"][1] += string.Format("\t\t{1}.set{0}({2});\r\n", parmName, populateVarName, varInitType);
                            if (!String.IsNullOrEmpty(varInitValue))
                                ctors["mandatory"][1] += string.Format("\t\t{1}.get{0}(){2};\r\n", parmName, populateVarName, varInitValue);
                            ctors["mandatory"][2] += doc;
                        }
                        if (p.Conformance == ClassContent.ConformanceKind.Mandatory && (p.PropertyType == Property.PropertyTypes.Structural || p.PropertyType == Property.PropertyTypes.NonStructural)) // Structural
                        {
                            ctors["structural"][0] += string.Format("{0} {1},", CreateDatatypeRef(tr, p, ownerPackage), Util.Util.MakeFriendly(cc.Name));
                            ctors["structural"][1] += string.Format("\t\t{1}.set{0}({2});\r\n", parmName, populateVarName, varInitType);
                            if (!String.IsNullOrEmpty(varInitValue))
                                ctors["structural"][1] += string.Format("\t\t{1}.get{0}(){2};\r\n", parmName, populateVarName, varInitValue);
                            ctors["structural"][2] += doc;
                        }
                        if (p.Conformance == ClassContent.ConformanceKind.Required || p.MinOccurs != "0" || p.Conformance == ClassContent.ConformanceKind.Mandatory)
                        {
                            // Required (default)
                            ctors["required"][0] += string.Format("{0} {1},", CreateDatatypeRef(tr, p, ownerPackage), Util.Util.MakeFriendly(cc.Name));
                            ctors["required"][1] += string.Format("\t\t{1}.set{0}({2});\r\n", parmName, populateVarName, varInitType);
                            if (!String.IsNullOrEmpty(varInitValue))
                                ctors["required"][1] += string.Format("\t\t{1}.get{0}(){2};\r\n", parmName, populateVarName, varInitValue);
                            ctors["required"][2] += doc;
                        }
                        ctors["all"][0] += string.Format("{0} {1},", CreateDatatypeRef(tr, p, ownerPackage), Util.Util.MakeFriendly(cc.Name));
                        ctors["all"][1] += string.Format("\t\t{1}.set{0}({2});\r\n", parmName, populateVarName, varInitType);
                        if (!String.IsNullOrEmpty(varInitValue))
                            ctors["all"][1] += string.Format("\t\t{1}.get{0}(){2};\r\n", parmName, populateVarName, varInitValue);
                        ctors["all"][2] += doc;

                        #endregion
                    }
                    else if (cc is Choice) // Choice
                    {
                        // Get the base datatype
                        Choice choice = cc as Choice;

                        TypeReference tr = (choice.Content[0] as Property).Type.Class.BaseClass;
                        List<String> methods = new List<string>();
                        foreach (Property p in choice.Content)
                            if (p.Type.Class.BaseClass != null && tr != null && p.Type.Class.BaseClass.Name != tr.Name)
                                tr = null;

                        // Now the signature (Mandatory)
                        if (choice.Conformance == ClassContent.ConformanceKind.Mandatory)
                        {
                            ctors["mandatory"][0] += string.Format("{0} {1},", tr == null ? "java.lang.Object" : CreateDatatypeRef(tr, new Property() { Container = cc.Container }, ownerPackage), Util.Util.MakeFriendly(cc.Name));
                            ctors["mandatory"][1] += string.Format("\t\t{1}.set{0}({2});\r\n", Util.Util.PascalCase(choice.Name), populateVarName, Util.Util.MakeFriendly(cc.Name));
                            ctors["mandatory"][2] += String.Format("\t * @param {0} ({1}) No documentation available", Util.Util.MakeFriendly(cc.Name), cc.Conformance);
                        }
                        if (cc.Conformance == ClassContent.ConformanceKind.Required || cc.MinOccurs != "0" || cc.Conformance == ClassContent.ConformanceKind.Mandatory)
                        {

                            // Required (default)
                            ctors["required"][0] += string.Format("{0} {1},", tr == null ? "java.lang.Object" : CreateDatatypeRef(tr, new Property() { Container = cc.Container }, ownerPackage), Util.Util.MakeFriendly(cc.Name));
                            ctors["required"][1] += string.Format("\t\t\t{1}.set{0}({2});\r\n", Util.Util.PascalCase(choice.Name), populateVarName, Util.Util.MakeFriendly(cc.Name));
                            ctors["required"][2] += String.Format("\t\t * @param {0} ({1}) No documentation available\r\n", Util.Util.MakeFriendly(cc.Name), cc.Conformance);
                        }
                        ctors["all"][0] += string.Format("{0} {1},", tr == null ? "java.lang.Object" : CreateDatatypeRef(tr, new Property() { Container = cc.Container }, ownerPackage), Util.Util.MakeFriendly(cc.Name));
                        ctors["all"][1] += string.Format("\t\t\t{1}.set{0}({2});\r\n", Util.Util.PascalCase(choice.Name), populateVarName, Util.Util.MakeFriendly(cc.Name));
                        ctors["all"][2] += String.Format("\t\t/* @param {0} ({1}) No documentation available\r\n", Util.Util.MakeFriendly(cc.Name), cc.Conformance);

                    }
                }
            }

            // Return
            return ctors;
        }

    }
}
