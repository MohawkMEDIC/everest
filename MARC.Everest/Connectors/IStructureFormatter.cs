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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Represents a class that has the ability to render an IGraphable object into another form, effectively
    /// serializing it
    /// </summary>
    /// <remarks>
    /// <para>The manner in which structure formatters are implemented have changed for Everest 1.0, 
    /// namely the Graph and Parse methods will be updated to be harmonized with the manner that 
    /// jEverest formats structures. 
    /// </para>
    /// <para>This will rid us of the Details array and will alter the method signatures for the formatter
    /// methods like GraphObject and ParseObject
    /// </para>
    /// </remarks>
#if WINDOWS_PHONE
    public interface IStructureFormatter : IDisposable
#else
    public interface IStructureFormatter : ICloneable, IDisposable
#endif
    {
 

        /// <summary>
        /// Instances of other IStructureFormatters that provide assistance to the primary formatter
        /// </summary>
        /// <example>
        /// <code lang="cs" title="Assigning a graphing aide">
        /// <![CDATA[
        /// // Create an instance of the primary formatter
        ///IStructureFormatter formatter = new XmlIts1Formatter();
        ///
        /// // Assign a graph aide
        ///formatter.GraphAides.Add(new DatatypeR2Formatter());
        /// ]]>
        /// </code>
        /// </example>
        List<IStructureFormatter> GraphAides { get; set; }

        /// <summary>
        /// The host of this formatter
        /// </summary>
        IStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets the types that this formatter can graph to/from the specified format
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        List<string> HandleStructure { get; }

        /// <summary>
        /// Graphs the object <paramref name="o"/> onto stream <paramref name="s"/> and returns
        /// a <see cref="T:MARC.Everest.Connectors.IFormatterGraphResult"/> structure with additional 
        /// details
        /// </summary>
        /// <param name="s">The stream to graph the message to</param>
        /// <param name="o"></param>
        /// <returns>An instance of <see cref="T:MARC.Everest.Connectors.IFormatterGraphResult"/> containing the results of the graph operation</returns>
        /// <example>
        /// <code lang="cs" title="Graphing an instance to a stream">
        /// <![CDATA[
        ///Stream s = null;
        ///try
        ///{
        ///    // Initialize the stream
        ///    s = File.Create("mydata.dat");
        ///
        ///    // Make a stream writer for easier addition of data to the stream
        ///    StreamWriter sw = new StreamWriter(s);
        ///    sw.WriteLine("This demonstrates writing data before a formatter!");
        ///    sw.Flush();
        ///
        ///    // Create a simple instance
        ///    MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
        ///        Guid.NewGuid(),
        ///        DateTime.Now,
        ///        MCCI_IN000000UV01.GetInteractionId(),
        ///        ProcessingID.Production,
        ///        "P",
        ///        AcknowledgementCondition.Always);
        ///
        ///    // Setup the formatter
        ///    IStructureFormatter structureFormatter = new XmlIts1Formatter()
        ///    {
        ///        ValidateConformance = false
        ///    };
        ///    structureFormatter.GraphAides.Add(new DatatypeFormatter());
        ///
        ///    // Format
        ///    structureFormatter.Graph(s, instance);
        ///
        ///    // Write some data after
        ///    sw.WriteLine("This appears afterwards!");
        ///    sw.Close();
        ///}
        ///finally
        ///{
        ///    if (s != null)
        ///        s.Close();
        ///}
        /// ]]>
        /// </code>
        /// </example>
        IFormatterGraphResult Graph(Stream s, IGraphable o);

        /// <summary>
        /// Parses an object from <paramref name="s"/> and returns a <see cref="T:MARC.Everest.Connectors.IFormatterParseResult"/>
        /// structure with additional details
        /// </summary>
        /// <param name="s">The stream from which to graph</param>
        /// <returns>An instance of <see cref="T:MARC.Everest.Connectors.IFormatterParseResult"/> containing the results of the parse operation</returns>
        /// <example>
        /// <code lang="cs" title="Parsing data from a stream">
        /// <![CDATA[
        /// // Load the assembly into the current AppDomain
        ///Assembly.Load(new AssemblyName("MARC.Everest.RMIM.UV.NE2008, Version=1.0.4366.42027, Culture=neutral"));
        ///
        ///Stream s = null;
        ///try
        ///{
        ///    // Initialize the stream
        ///    s = File.OpenRead("mydata.xml");
        ///
        ///    // Setup the formatter
        ///    IStructureFormatter structureFormatter = new XmlIts1Formatter()
        ///    {
        ///        ValidateConformance = false
        ///    };
        ///    structureFormatter.GraphAides.Add(new DatatypeFormatter());
        ///
        ///    // Parse
        ///    var result = structureFormatter.Parse(s);
        ///
        ///    // Output the type of instance that was parsed
        ///    Console.WriteLine("This file contains a '{0}' instance", result.Structure.GetType().Name);
        ///}
        ///finally
        ///{
        ///    if (s != null)
        ///        s.Close();
        ///}
        /// ]]>
        /// </code>
        /// </example>
        IFormatterParseResult Parse(Stream s);


#if WINDOWS_PHONE
        /// <summary>
        /// Returns a new shallow copy of the object
        /// </summary>
        /// <remarks>Windows Phone doesn't expose ICloneable which is used by several Everest functions
        /// so this interface redefines it</remarks>
        object Clone();
#endif
    }
}