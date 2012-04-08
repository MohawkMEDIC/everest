using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using MARC.Everest.Interfaces;
using System.Xml;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Identifies a structured formatter that generates assemblies "on the fly"
    /// </summary>
    /// <remarks>
    /// <para>
    /// CodeDom structure formatters are different from regular <see cref="T:MARC.Everest.Connectors.IStructureFormatter"/>
    /// in that they use CodeDom (or other dynamic "discovery" mechanism to generate code in memory which is compiled
    /// when the first instance is passed through the formatter.
    /// </para>
    /// <para>
    /// This class exposes the <see cref="M:BuildCache(Type[])"/> method which is intended to force the CodeDom formatter
    /// to build its code in memory and compile one or more assemblies. These assemblies can then be referenced (and saved
    /// for later use) by the developer at runtime.
    /// </para>
    /// </remarks>
    public interface ICodeDomStructureFormatter : IStructureFormatter
    {

        /// <summary>
        /// If true, generate the assembly in memory
        /// </summary>
        bool GenerateInMemory { set; }

        /// <summary>
        /// Get a list of assemblies this formatter has generated
        /// </summary>
        Assembly[] GeneratedAssemblies { get; }

        /// <summary>
        /// Parse an object from a stream using only interactions from the specified 
        /// assembly.
        /// </summary>
        /// <param name="a">The assembly from which to qualify the root element</param>
        /// <param name="s">The stream from which to parse</param>
        /// <remarks>
        /// <para>When using formatter, it is important to remember that the formatter
        /// by default will scan all assemblies in the current app domain. This may
        /// result in undesirable effects when formatting within an application that
        /// has multiple RMIM assemblies loaded (the formatter will just use the first
        /// </para>
        /// <para>This method override of Parse allows developers to specify which
        /// RMIM assembly they would like data parsed from.</para>
        /// </remarks>
        IFormatterParseResult Parse(Stream s, Assembly a);

        /// <summary>
        /// Build a cache for the specified types
        /// </summary>
        /// <example>
        /// <code lang="cs" title="Pre-building assemblies for formatting">
        /// <![CDATA[
        /// // Create an instance of the primary formatter
        /// ICodeDomStructureFormatter formatter = new XmlIts1Formatter();
        /// formatter.GenerateInMemory = false;
        ///
        /// // Assign a graph aide
        ///formatter.GraphAides.Add(new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture
        /// });
        ///
        /// // Let the user know we're initializing
        ///Console.WriteLine("Please Wait, Initializing...");
        ///
        /// // Build the type cache
        ///formatter.BuildCache(new Type[] { typeof(ClinicalDocument) });
        ///
        /// // Continue with the program here
        ///Console.WriteLine("Initialized, Program is continuing...");
        /// ]]>
        /// </code>
        /// </example>
        void BuildCache(Type[] t);
    }
}
