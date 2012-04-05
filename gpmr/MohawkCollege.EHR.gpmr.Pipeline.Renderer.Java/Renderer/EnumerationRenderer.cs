using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Attributes;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Interfaces;
using MohawkCollege.EHR.gpmr.COR;
using System.IO;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.HeuristicEngine;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer
{
    [FeatureRenderer(Feature = typeof(MohawkCollege.EHR.gpmr.COR.ValueSet), IsFile = true)]
    [FeatureRenderer(Feature = typeof(MohawkCollege.EHR.gpmr.COR.CodeSystem), IsFile = true)]
    [FeatureRenderer(Feature = typeof(MohawkCollege.EHR.gpmr.COR.ConceptDomain), IsFile = true)]
    internal class EnumerationRenderer : IFeatureRenderer
    {
        /// <summary>
        /// Determine if we'll render this or not
        /// </summary>
        public static bool WillRender(Enumeration enu)
        {
            if (!String.IsNullOrEmpty(Datatypes.GetBuiltinVocabulary(enu.Name)))
                return true;

            // Too big
            if (enu.Literals.Count > RimbaJavaRenderer.MaxLiterals)
                return false;

            bool used = false;
            foreach (KeyValuePair<String, Feature> kv in enu.MemberOf)
                if (kv.Value is Class)
                    foreach (ClassContent cc in (kv.Value as Class).Content)
                        used |= cc is Property &&
                            (cc as Property).SupplierDomain != null && 
                            (cc as Property).SupplierDomain == enu &&
                            (RimbaJavaRenderer.GenerateVocab ||
                            (!RimbaJavaRenderer.GenerateVocab && ((cc as Property).SupplierDomain is ValueSet)
                            && ((cc as Property).PropertyType == Property.PropertyTypes.Structural)));
            return used && enu.Literals.Count > 0 && enu.Literals.FindAll(l => !l.Annotations.Exists(o => o is SuppressBrowseAnnotation)).Count > 0; // don't process
        }

        #region IFeatureRenderer Members

        /// <summary>
        /// Render the enumeration
        /// </summary>
        public void Render(string ownerPackage, string apiNs, Feature f, System.IO.TextWriter tw)
        {
            // Validate arguments
            if (String.IsNullOrEmpty(ownerPackage))
                throw new ArgumentNullException("ownerPackage");
            if (String.IsNullOrEmpty(apiNs))
                throw new ArgumentNullException("apiNs");
            if (f == null || !(f is Enumeration))
                throw new ArgumentException("Parameter must be of type Enumeration", "f");

            Enumeration cls = f as Enumeration;

            if (!String.IsNullOrEmpty(Datatypes.GetBuiltinVocabulary(cls.Name)))
                throw new InvalidOperationException(String.Format("Not rendering '{0}' already included in core library", cls.Name));
            
            // Sanity check
            if (cls.Literals.Count == 0)
                throw new InvalidOperationException("Enumeration doesn't contain any data"); // Don't populate it

            // Is this code system even used?
            if (!WillRender(cls))
            {
                if (cls.Literals.Count > RimbaJavaRenderer.MaxLiterals)
                    throw new InvalidOperationException(String.Format("Enumeration '{2}' too large, enumeration has {0} literals, maximum allowed is {1}",
                        cls.Literals.Count, RimbaJavaRenderer.MaxLiterals, cls.Name));
                else
                    throw new InvalidOperationException("Enumeration is not used!");
            }


            tw.WriteLine("package {0}.vocabulary;", ownerPackage);

            #region Render the imports
            string[] apiImports = { "annotations.*", "datatypes.*", "datatypes.generic.*" },
                jImports = { "java.lang.*", "java.util.*" };
            foreach (var import in apiImports)
                tw.WriteLine("import {0}.{1};", apiNs, import);
            foreach (var import in jImports)
                tw.WriteLine("import {0};", import);
            #endregion

            #region Render Class Signature

            // Documentation
            if (DocumentationRenderer.Render(cls.Documentation, 0).Length == 0)
                tw.WriteLine("/** No Summary Documentation Found */");
            else
                tw.Write(DocumentationRenderer.Render(cls.Documentation, 0));

            // Create structure annotation
            tw.WriteLine(CreateStructureAnnotation(cls));

            // Create class signature
            tw.Write("public class {0} implements {1}.interfaces.IEnumeratedVocabulary", Util.Util.PascalCase(cls.Name), apiNs);

            tw.WriteLine("{");

            #endregion

            #region Render Properties

            StringWriter sw = new StringWriter();
            RenderLiterals(sw, cls, new List<string>(), new List<string>(), cls.Literals);
            String tStr = sw.ToString();
            tw.WriteLine(tStr);

            #endregion

            #region Render IEnumeratedVocabulary Methods

            tw.WriteLine("\tpublic {0}(String code, String codeSystem) {{ this.m_code = code; this.m_codeSystem = codeSystem; }}", Util.Util.PascalCase(cls.Name));
            tw.WriteLine("\tprivate final String m_code;");
            tw.WriteLine("\tprivate final String m_codeSystem;");
            tw.WriteLine("\tpublic String getCodeSystem() { return this.m_codeSystem; }");
            tw.WriteLine("\tpublic String getCode() { return this.m_code; }");
	
            #endregion
            // End enumeration
            tw.WriteLine("}");
        }

        /// <summary>
        /// Create structure attribute
        /// </summary>
        private string CreateStructureAnnotation(Enumeration cls)
        {
            return String.Format("@Structure(name = \"{0}\", codeSystem = \"{1}\", structureType = StructureType.{2})", cls.Name, cls.ContentOid, cls.GetType().Name.ToUpper());
        }

        /// <summary>
        /// Render literals
        /// </summary>
        private void RenderLiterals(StringWriter sw, Enumeration enu, List<string> rendered, List<String> mnemonics, List<Enumeration.EnumerationValue> literals)
        {
            // Literals
            foreach (Enumeration.EnumerationValue ev in literals)
            {
                string bn = Util.Util.PascalCase(ev.BusinessName);
                string rendName = Util.Util.PascalCase(bn ?? ev.Name) ?? "__Unknown";

                // Already rendered, so warn and skip
                if (rendered.Contains(rendName) || mnemonics.Contains(ev.Name))
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Enumeration value {0} already rendered, skipping", ev.BusinessName), "warn");
                    continue;
                }
                else if (!ev.Annotations.Exists(o => o is SuppressBrowseAnnotation))
                {
                    sw.Write(DocumentationRenderer.Render(ev.Documentation, 1));
                    if (DocumentationRenderer.Render(ev.Documentation, 1).Length == 0) // Documentation correction
                        sw.WriteLine("\t/** {0} */", (ev.BusinessName ?? ev.Name).Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\r", "").Replace("\n", ""));

                    // Annotations?
                    if (ev.Annotations != null && ev.Annotations.Find(o => o is SuppressBrowseAnnotation) != null)
                    {
                        // Can't suppress browse in Jaba
                        System.Diagnostics.Trace.WriteLine(String.Format("Enumation literal '{0}' won't be rendered as it has SuppressBrowse enabled", ev.Name));
                        //sw.WriteLine("\t\t[EditorBrowsable(EditorBrowsableState.Never)]\r\n\t\t[Browsable(false)]");
                    }

                    // Render ?
                    if (rendered.Find(o => o.Equals(rendName)) != null) // .NET enumeration field will be the same, so render something different
                        sw.Write("\tpublic static final {3} {0} = new {3}(\"{1}\",\"{2}\")", Util.Util.PascalCase(rendName + "_" + ev.Name) ?? "__Unknown",
                            ev.Name, ev.CodeSystem ?? enu.ContentOid, Util.Util.PascalCase(enu.Name));
                    else
                        sw.Write("\tpublic static final {3} {0} = new {3}(\"{1}\",\"{2}\")", rendName, ev.Name, ev.CodeSystem ?? enu.ContentOid, Util.Util.PascalCase(enu.Name));

                    sw.WriteLine(";"); // Another literal follows

                    sw.Write("\r\n"); // Newline

                    rendered.Add(rendName); // Add to rendered list to keep track
                    mnemonics.Add(ev.Name);

                    if (ev.RelatedCodes != null)
                        RenderLiterals(sw, enu, rendered, mnemonics, ev.RelatedCodes);
                }
            }
        }

        /// <summary>
        /// Create a file for the enumeration
        /// </summary>
        public string CreateFile(Feature f, string filePath)
        {
            string fileName = Path.ChangeExtension(Path.Combine(Path.Combine(filePath, "vocabulary"), Util.Util.MakeFriendly(f.Name)), ".java");

            if (File.Exists(fileName) && !(f as Enumeration).OwnerRealm.EndsWith(RimbaJavaRenderer.prefRealm))
                throw new InvalidOperationException("Enumeration has already been rendered from the preferred realm. Will not render this feature");

            return fileName;
        }

        #endregion
    }
}
