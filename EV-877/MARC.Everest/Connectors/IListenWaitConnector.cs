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

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Represents a <see cref="T:MARC.Everest.Connectors.IReceivingConnector"/> connector that is persistant in memory and can "listen" for data
    /// over a transport channel
    /// </summary>
    /// <seealso cref="T:MARC.Everest.Connectors.IListenWaitRespondConnector"/>
    /// <example>
    /// 
    /// <code lang="cs" title="Running an IListenWaitConnector in synchronous mode">
    /// <![CDATA[
    /// // Setup the formatter
    ///XmlIts1Formatter fmtr = new XmlIts1Formatter();
    ///fmtr.GraphAides.Add(new DatatypeFormatter() 
    ///{ 
    ///    ValidateConformance = false 
    ///});
    ///fmtr.ValidateConformance = false;
    ///
    /// // Prepare the connection string
    ///FileConnectionStringBuilder fsb = new FileConnectionStringBuilder()
    ///{
    ///    Directory = "C:\\temp",
    ///    Pattern = "*.xml"
    ///};
    ///
    /// // Create the connector
    ///IListenWaitConnector connector = new FileListenConnector(fsb.GenerateConnectionString());
    /// // Set the formatter
    ///((IFormattedConnector)connector).Formatter = fmtr;
    ///
    ///try
    ///{
    ///    // Open the channel and start accepting messages
    ///    connector.Open();
    ///    connector.Start();
    ///
    ///    // Wait until a message is available
    ///    IReceiveResult result = connector.Receive();
    ///
    ///    // Output the type
    ///    Console.WriteLine(result.Structure.ToString());
    ///}
    ///finally
    ///{
    ///    connector.Close();
    ///    connector.Dispose();
    ///}
    /// ]]>
    /// </code>
    /// 
    /// <code lang="cs" title="Running an IListenWaitConnector in Asynchronous mode">
    /// <![CDATA[
    ///static void Main(string[] args)
    ///{
    ///
    ///    // Good practice to force load
    ///    Assembly.Load(new AssemblyName("MARC.Everest.RMIM.UV.NE2008"));
    ///
    ///    // Setup the formatter
    ///    XmlIts1Formatter fmtr = new XmlIts1Formatter();
    ///    
    ///    // Formatter & Connector Connection String Truncated to save space
    ///
    ///    // Create the connector
    ///    IListenWaitConnector connector = new FileListenConnector(fsb.GenerateConnectionString());
    ///
    ///    // Set the formatter
    ///    ((IFormattedConnector)connector).Formatter = fmtr;
    ///
    ///    try
    ///    {
    ///        // Open the channel and start accepting messages
    ///        connector.Open();
    ///
    ///        // Set the event handler
    ///        connector.MessageAvailable += new 
    ///               EventHandler<UnsolicitedDataEventArgs>(
    ///                   connector_MessageAvailable
    ///               );
    ///
    ///        // Start listening
    ///        connector.Start();
    ///
    ///        Console.WriteLine("Drop a message in the directory!");
    ///        Console.WriteLine("Press any key to stop listening");
    ///        Console.ReadKey();
    ///    }
    ///    finally
    ///    {
    ///        connector.Close();
    ///        connector.Dispose();
    ///    }
    ///}
    ///
    /// /// <summary>
    /// /// Process the message
    /// /// </summary>
    ///static void connector_MessageAvailable(object sender, UnsolicitedDataEventArgs e)
    ///{
    ///    // Receive the result message
    ///    IReceiveResult result = (sender as IListenWaitConnector).Receive();
    ///    Console.WriteLine(result.Structure.ToString());
    ///}
    /// ]]>
    /// </code>
    /// </example>
    public interface IListenWaitConnector : IReceivingConnector
    {
        /// <summary>
        /// Start listening for unsolicited requests
        /// </summary>
        void Start();
        /// <summary>
        /// Stop listening
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Stop")]
        void Stop();
        /// <summary>
        /// Event is fired when unsolicited data is received
        /// </summary>
        event EventHandler<UnsolicitedDataEventArgs> MessageAvailable;
        
    }
}