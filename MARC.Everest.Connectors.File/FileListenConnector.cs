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
using System.Text.RegularExpressions;
using MARC.Everest.Interfaces;
using MARC.Everest.Exceptions;
using System.Threading;
using System.ComponentModel;

namespace MARC.Everest.Connectors.File
{ 
    /// <summary>
    /// Listens to the file system and notifies applications when a new file is received from the
    /// file system.
    /// </summary>
    /// <example>
    /// <code lang="cs" title="Listen for new files in directory C:\Temp">
    /// <![CDATA[
    ///static void Main(string[] args)
    ///{
    ///
    ///    // Load the RMIM assembly we want to process
    ///    Assembly.Load(new AssemblyName("MARC.Everest.RMIM.UV.NE2008"));
    ///
    ///    // Setup the connection string
    ///    FileConnectionStringBuilder csbuilder = new FileConnectionStringBuilder();
    ///    csbuilder.Directory = @"C:\temp";
    ///    csbuilder.KeepFiles = true;
    ///    csbuilder.Pattern = "*.xml";
    ///    csbuilder.ProcessExisting = false;
    ///
    ///    // Setup the formatter
    ///    XmlIts1Formatter formatter = new XmlIts1Formatter()
    ///    {
    ///        ValidateConformance = true
    ///    };
    ///    formatter.GraphAides.Add(new DatatypeFormatter()
    ///    {
    ///        ValidateConformance = true
    ///    });
    ///
    ///    // Setup the connector
    ///    FileListenConnector connector = new FileListenConnector(csbuilder.GenerateConnectionString());
    ///    connector.Formatter = formatter;
    ///
    ///    // Subscribe
    ///    try
    ///    {
    ///       
    ///        // Open the connector
    ///        connector.Open();
    ///
    ///        // Set the event handler for new messages
    ///        connector.MessageAvailable += new EventHandler<UnsolicitedDataEventArgs>(connector_MessageAvailable);
    ///
    ///        // Start listening for messages
    ///        connector.Start();
    ///
    ///        // Output a message
    ///        Console.WriteLine("Listening for files, press any key to stop...");
    ///        Console.ReadKey();
    ///    }
    ///    finally
    ///    {
    ///        // Stop listening
    ///        connector.Stop();
    ///
    ///        // Clean
    ///        connector.Close();
    ///        connector.Dispose();
    ///    }
    ///    
    ///}
    ///
    /// /// <summary>
    /// /// Message is available to be processed
    /// /// </summary>
    ///static void connector_MessageAvailable(object sender, UnsolicitedDataEventArgs e)
    ///{
    ///    // Get the connector that raised the event
    ///    var connector = (IListenWaitConnector)sender;
    ///
    ///    // Output the name of the file
    ///    Console.WriteLine("Received a message at '{0}'...", e.ReceiveEndpoint);
    ///
    ///    // Process the message
    ///    IReceiveResult result = connector.Receive();
    ///
    ///    // Output the type
    ///    if (result.Structure != null)
    ///        Console.WriteLine("Structure type is '{0}'", result.Structure.GetType().Name);
    ///    else // Couldn't process, so output the errors
    ///        foreach (var dtl in result.Details)
    ///        {
    ///            if (dtl.Type == ResultDetailType.Error)
    ///                Console.WriteLine("\t{0}", dtl.Message);
    ///        }
    ///}
    /// ]]>
    /// </code>
    /// </example>
    [Description("File Listen/Wait Connector")]
    public class FileListenConnector : IFormattedConnector, IListenWaitConnector
    {

        /// <summary>
        /// Creates a new instance of the FileListenConnector
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily")]
        public FileListenConnector() { }
        /// <summary>
        /// Creates a new instance of the FileListenConnector with the specified connection string.
        /// </summary>
        /// <param name="connectionString">Dictates how the connector should subscribe to file system events (<see cref="ConnectionString"/>).</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily")]
        public FileListenConnector(string connectionString) { ConnectionString = connectionString; }

        /// <summary>
        /// The worker class performs the deserialization of objects
        /// </summary>
        private class Worker
        {
            /// <summary>
            /// The worker id - A GUID is used for the ID.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
            public Guid WorkerId = Guid.NewGuid();
            /// <summary>
            /// Gets or sets the formatter to use.
            /// </summary>
            public IStructureFormatter Formatter { get; set; }
            /// <summary>
            /// Fires when the operation is complete.
            /// </summary>
            public event WaitCallback Completed;
            /// <summary>
            /// Gets or sets the result from the parsing.
            /// </summary>
            public FileReceiveResult Result { get; set; }
            /// <summary>
            /// The number of retries performed.
            /// </summary>
            private int retryCount = 0;

            private const int MAX_RETRY_COUNT = 6;


            /// <summary>
            /// Parses the specified structure.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
            public void Work(object state)
            {
                // Prepare stream
                Stream s = null;
                FileReceiveResult result = new FileReceiveResult();
                result.FileName = state.ToString(); // reference filename
                IFormatterParseResult pResult = null;

                // parse the object
                try
                {
                    s = new FileStream(state.ToString(), FileMode.Open, FileAccess.Read, FileShare.Write);

                    // Parse the object
                    pResult = Formatter.Parse(s);
                    result.Structure = pResult.Structure;
                    result.Details = pResult.Details;
                    result.Code = ResultCode.Accepted;
                    if (Array.Find<IResultDetail>(result.Details, o => o.Type == ResultDetailType.Error) != null)
                        result.Code = ResultCode.AcceptedNonConformant;
                    else if (result.Structure == null)
                        result.Code = ResultCode.TypeNotAvailable;
                }
                catch (MessageValidationException e)
                {
                    result.Code = ResultCode.Rejected;
                    List<IResultDetail> dtl = new List<IResultDetail>(new IResultDetail[] { new FileResultDetail(ResultDetailType.Error, e.Message, state.ToString(), e) });
                    dtl.AddRange(pResult.Details ?? new IResultDetail[0]);
                    result.Details = dtl.ToArray();
                }
                catch (FormatException e)
                {
                    result.Code = ResultCode.Rejected;
                    result.Details = new IResultDetail[] { new FileResultDetail(ResultDetailType.Error, e.Message, state.ToString(), e) };
                }
                catch (DirectoryNotFoundException e)
                {
                    result.Code = ResultCode.NotAvailable;
                    result.Details = new IResultDetail[] { new FileResultDetail(ResultDetailType.Error, e.Message, state.ToString(), e) };
                }
                catch (IOException)
                {
                    retryCount++;
                    if(retryCount < MAX_RETRY_COUNT)
                        Work(state);
                    return;
                }
                catch (Exception e)
                {
                    result.Code = ResultCode.Error;
                    result.Details = new IResultDetail[] { new FileResultDetail(ResultDetailType.Error, e.Message, state.ToString(), e) };
                }
                finally
                {
                    if (s != null) s.Close();
                }

                // Set the result
                this.Result = result;

                // Fire completed event
                if (Completed != null) Completed(this);

            }
        }

        private bool keepFiles = false;
        private string directory = ".\\";
        private string pattern = @".*?\..*?";
        private bool isopen = false;
        private bool processExisting = false;

        private Queue<string> waitingData = new Queue<string>(); // Data that is waiting to be received off the file receiver

        // The watchman is responsible for handling this object 
        private FileSystemEventHandler watchman;

        //TODO: TD: Give a better piece of documentation. This doc says only one, but then why a dictionary.
        // Only one watcher can listen to the FS
        private static Dictionary<string, FileSystemWatcher> watcher = new Dictionary<string,FileSystemWatcher>();

        /// <summary>
        /// Close the watcher
        /// </summary>
        ~FileListenConnector()
        {
            Close();
        }

        #region IFormattedConnector Members

        /// <summary>
        /// Gets or sets the formatter that should be used for deserializing messages from the
        /// file system.
        /// </summary>
        public IStructureFormatter Formatter { get; set; }

        #endregion

        #region IConnector Members

        /// <summary>
        /// Gets or sets the connection string that dictates how this file connector should receive events from the file system. Valid parameters are:
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <listheader><term>Key</term><description>Description</description></listheader>
        ///     <item><term>Directory</term><description>The directory to listen to.</description></item>
        ///     <item><term>KeepFiles</term><description>If set to true, files will not be deleted when received (requires more memory).</description></item>
        ///     <item><term>Pattern</term><description>The file pattern to use.</description></item>
        ///     <item><term>ProcessExisting</term><description>True indicates the connector should process the files that already exist in the directory.</description></item>
        /// </list>
        /// </remarks>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Open a connection to the file system. ie: This is translated to opening a file system watcher.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToBoolean(System.String)")]
        public void Open()
        {
            // Connection string
            if (ConnectionString == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_CONNECTION_STRING, ConnectorException.ReasonType.NullConnectionString);
            else if (Formatter == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_FORMATTER, ConnectorException.ReasonType.NullFormatter);

            //TODO: TD: This should probably iterate over the connectionData to support multiple items.
            // Split to key value pairs
            Dictionary<string, List<string>> connectionData = ConnectionStringParser.ParseConnectionString(ConnectionString);

            if (connectionData.ContainsKey("directory"))
                this.directory = connectionData["directory"][0];
            if (connectionData.ContainsKey("keepfiles"))
                this.keepFiles = Convert.ToBoolean(connectionData["keepfiles"][0]);
            if (connectionData.ContainsKey("pattern"))
                this.pattern = connectionData["pattern"][0].Replace(".", "\\.").Replace("?", ".").Replace("*", ".*?");
            if (connectionData.ContainsKey("processexisting"))
                this.processExisting = Convert.ToBoolean(connectionData["processexisting"][0]);

            if (!Directory.Exists(directory))
                throw new DirectoryNotFoundException(string.Format("Could not find the path '{0}'", directory));

            isopen = true;

        }

        /// <summary>
        /// Event handler that is fired when a new file matching the watcher's list was created on the file
        /// system.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
            // Ensure we can process this message
            if (pattern != null)
            {
                //TODO: TD: Should use a cached copy of the regex
                // Get the filename and see if it matches our pattern
                Regex r = new Regex(pattern);
                if (r.IsMatch(e.Name) && System.IO.File.Exists(e.FullPath))
                {
                    // Open for read
                    Stream s = null;
                    try
                    {
                        lock (waitingData) { waitingData.Enqueue(e.FullPath); } // Add the buffer to the queue of items to be received

                        // Data event args
                        UnsolicitedDataEventArgs usdea = new UnsolicitedDataEventArgs(
                            new Uri(string.Format("file://{0}/{1}", this.directory, e.Name)),
                            DateTime.Now, null);

                        // Call the delegate that handles processing, if there is one
                        if (MessageAvailable != null)
                            MessageAvailable(this, usdea);

                    }
                    finally
                    {
                        if (s != null) s.Close(); // Close the file
                    }
                }
            }
        }

        /// <summary>
        /// Stop the file system watcher.
        /// </summary>
        public void Close()
        {
            Stop();
            isopen = false;
        }

        /// <summary>
        /// Returns true if this connector is in an open state.
        /// </summary>
        /// <remarks>
        /// This is not a property because in some formatters it does actually start a more complex
        /// check
        /// </remarks>
        public bool IsOpen()
        {
            return isopen;
        }

        #endregion

        #region IListenWaitConnector Members

        /// <summary>
        /// Start the listening of the file system.
        /// </summary>
        public void Start()
        {
            // Check if open
            if (!IsOpen())
                throw new ConnectorException("Connector is not in a state that allows this operation to proceed", ConnectorException.ReasonType.NotOpen);

            // Create the watcher if not exists
            if (!watcher.ContainsKey(directory))
            {
                FileSystemWatcher wtchr = new FileSystemWatcher(this.directory, "*.*"); // Listen for everything
                wtchr.EnableRaisingEvents = true;
                watcher.Add(directory, wtchr);
            }

            // Create the watchman
            watchman = new FileSystemEventHandler(watcher_Created);

            // Process existing files
            if (processExisting)
            {
                foreach (string s in Directory.GetFiles(directory))
                {
                    watchman(this, new FileSystemEventArgs(WatcherChangeTypes.Created, directory, Path.GetFileName(s)));
                }
            }

            watcher[directory].Created += watchman; // Assign the watchman
            
        }

        /// <summary>
        /// Stop listening on the file system.
        /// </summary>
        public void Stop()
        {
            if(this.IsOpen())
                watcher[directory].Created -= watchman;
        }

        /// <summary>
        /// Occurs when a new piece of data is received from the source.
        /// </summary>
        public event EventHandler<UnsolicitedDataEventArgs> MessageAvailable;

        #endregion

        #region IReceivingConnector Members

        /// <summary>
        /// Receive the message from the waiting received message queues.
        /// </summary>
        /// <example>
        /// <code lang="cs" title="Perform a blocking receive">
        /// <![CDATA[
        ///using System;
        ///using System.Collections.Generic;
        ///using System.Linq;
        ///using System.Text;
        ///using MARC.Everest.DataTypes;
        ///using MARC.Everest.Core.MR2009.Interactions;
        ///using MARC.Everest.Core.MR2009.Vocabulary;
        ///using System.IO;
        ///using System.Xml;
        ///using MARC.Everest.Connectors.File;
        ///using MARC.Everest.Connectors;
        ///using MARC.Everest.Formatters.XML.ITS1;
        ///using MARC.Everest.Interfaces;
        ///using System.Diagnostics;
        ///using System.Reflection;
        ///
        ///namespace ExampleConnector
        ///{
        ///    class Program
        ///    {
        ///        public static IGraphable results;
        ///        static void Main(string[] args)
        ///        {
        ///            try
        ///            {
        ///                
        ///                // Loads the Assembly so it can be used right away        
        ///                Assembly.Load(new AssemblyName("MARC.Everest.Core.MR2009"));
        ///                
        ///                // Creates the formatter
        ///                Formatter its1Formatter = new Formatter();
        ///                its1Formatter.GraphAides.Add(
        ///                    typeof(
        ///                        MARC.Everest.Formatters.XML.Datatypes.R1.Formatter
        ///                    )
        ///                );    
        ///               
        ///                // Create an instance of the connector
        ///                FileListenConnector connector = new FileListenConnector(@"Directory=C:\work3;KeepFiles=False");
        ///                
        ///                // Guarantees  that the generated instances will be valid against the conformance statements 
        ///                // in the RMIM object.
        ///                its1Formatter.ValidateConformance = false;
        ///               
        ///                
        ///                connector.Formatter = its1Formatter;
        ///                connector.Open();
        ///                connector.Start();
        ///               
        ///                // Wait until a file is available
        ///                IReceiveResult result = connector.Receive();
        ///                // Process the file  
        ///                results = result.Structure;
        ///                
        ///
        ///                its1Formatter.GraphObject(Console.OpenStandardOutput(), results);
        ///                // Close connector
        ///                connector.Close();
        ///
        ///            }
        ///            catch (Exception e)
        ///            {
        ///                Console.WriteLine(e.Message + "\r\n" + e.StackTrace);
        ///           }
        ///
        ///            Console.ReadKey();            
        ///        }
        ///    }
        ///}
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="System.Xml.XmlException"/>
        /// <exception cref="MARC.Everest.Exceptions.ConnectorException"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public IReceiveResult Receive()
        {

            // Formatter check
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // Create the work that will perform the operations
            Worker w = new Worker();
            w.Formatter = (IStructureFormatter)this.Formatter.Clone();

            //TODO: Use a WaitHandle instead of Thread.Sleep()
            // If there is no data in the waiting data queue, block
            while (waitingData.Count == 0)
                Thread.Sleep(250);

            string state;
            lock (waitingData) { state = waitingData.Dequeue(); }
            w.Work(state);

            // Delete this file
            if (!keepFiles)
                try
                {
                    System.IO.File.Delete(w.Result.FileName);
                }
                catch (Exception) { } // Don't care if file delete fails


            // Return the result
            return w.Result;
        }

        // Async results
        private Dictionary<IAsyncResult, FileReceiveResult> asyncResults = new Dictionary<IAsyncResult, FileReceiveResult>();

        /// <summary>
        /// Start an asychronous receive of the data from the receive queue.
        /// <para>
        /// This method may perform better than a Receive() method used in the MessageAvailable delegate as it spawns a new
        /// thread to perform the parsing of the object.
        /// </para>
        /// </summary>
        /// <example>
        /// Perform an asynchronous receive
        /// <code>
        /// FileListenConnector conn = new FileListenConnector();
        /// conn.Formatter = new MARC.Everest.Formatters.XML.ITS1.Formatter();
        /// conn.Open("Directory=C:\\temp;Pattern=*");
        /// conn.Start();
        /// // Message available is fired when a new message is received from the server
        /// conn.MessageAvailable += new EventHandler&lt;UnsolicitedDataEventArgs&gt;(delegate(object sender, UnsolicitedDataEventArgs evt)
        /// {
        ///    // Process the message
        ///    IAsyncResult receiveResult = conn.BeginReceive(null, null);
        ///    receiveResult.AsyncWaitHandle.WaitOne();
        ///    IReceiveResult result = conn.EndReceive(receiveResult);
        ///    Console.WriteLine("Received a message with {0} conformance...", result.Code);
        /// });
        /// Console.WriteLine("Press any key to stop...");
        /// Console.ReadKey();
        /// conn.Close();
        /// </code>        
        /// </example>
        public IAsyncResult BeginReceive(AsyncCallback callback, object state)
        {

            // Formatter check
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            if (waitingData.Count == 0) // Can't start as no messages exist!
                return null;

            // Setup worker
            Worker w = new Worker();

            // Create a new instance of the formatter
            w.Formatter = Formatter.Clone() as IStructureFormatter;
            
            // Set async result
            IAsyncResult Result = new ReceiveResultAsyncResult(state, new AutoResetEvent(false));

            // Completed delegate
            w.Completed += delegate(object Sender)
            {
                Worker sWorker = Sender as Worker; // Strong type sender
                // Lookup the result in the dictionary
                if (!asyncResults.ContainsKey(Result))
                    lock (asyncResults) { asyncResults.Add(Result, sWorker.Result); }
                (Result as ReceiveResultAsyncResult).SetComplete(); // Set completed
                (Result.AsyncWaitHandle as AutoResetEvent).Set(); // send signal
                if (callback != null) callback(Result); // callback
            };

            // JF: Fixed
            // Add to thread pool
            lock (waitingData)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(w.Work), waitingData.Dequeue());
            }

            return Result;
            
        }

        /// <summary>
        /// Retrieve the result of the async receive operation
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public IReceiveResult EndReceive(IAsyncResult asyncResult)
        {
            if (!asyncResult.IsCompleted)
                return null;
            else
            {
                FileReceiveResult result = asyncResults[asyncResult];
                // Delete this file
                if (!keepFiles)
                    try
                    {
                        System.IO.File.Delete(result.FileName);
                    }
                    catch (Exception)
                    {
                    }

                lock (asyncResults) { asyncResults.Remove(asyncResult); }
                return result;
            }
        }

        /// <summary>
        /// Returns true if the connector has data waiting
        /// </summary>
        public bool HasData
        {
            get { return waitingData.Count > 0; }
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}
