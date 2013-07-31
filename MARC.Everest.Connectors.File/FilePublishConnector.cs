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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Interfaces;
using MARC.Everest.Exceptions;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Security.AccessControl;
using System.ComponentModel;

namespace MARC.Everest.Connectors.File
{
    /// <summary>
    /// Allows developers to send messages to a FILE based transport.
    /// </summary>
    /// <example>
    /// <code lang="cs" title="Publishing a message to a directory">
    /// <![CDATA[
    /// // Setup the connection string
    ///FileConnectionStringBuilder csbuilder = new FileConnectionStringBuilder();
    ///csbuilder.NamingConvention = FileNamingConventionType.Guid;
    ///csbuilder.Directory = @"C:\temp";
    ///csbuilder.OverwriteExistingFiles = false;
    ///
    /// // Setup the formatter
    ///XmlIts1Formatter formatter = new XmlIts1Formatter()
    ///{
    ///    ValidateConformance = false
    ///};
    ///formatter.GraphAides.Add(new DatatypeFormatter()
    ///{
    ///    CompatibilityMode = DatatypeFormatterCompatibilityMode.Universal,
    ///    ValidateConformance = false
    ///});
    ///
    /// // Setup the connector
    ///FilePublishConnector connector = new FilePublishConnector(csbuilder.GenerateConnectionString());
    ///connector.Formatter = formatter;
    ///
    /// // Publish
    ///try
    ///{
    ///   
    ///    // Open the connector
    ///    connector.Open();
    ///
    ///    // Output 10 files
    ///    for (int i = 0; i < 10; i++)
    ///    {
    ///        // Create an instance
    ///        MCCI_IN000002UV01 instance = new MCCI_IN000002UV01(
    ///            Guid.NewGuid(),
    ///            DateTime.Now,
    ///            MCCI_IN000002UV01.GetInteractionId(),
    ///            ProcessingID.Training,
    ///            "T"
    ///        );
    ///
    ///        // Send the instance
    ///        connector.Send(instance);
    ///    }
    ///}
    ///finally
    ///{
    ///    // Clean
    ///    connector.Close();
    ///    connector.Dispose();
    ///}
    /// ]]>
    /// </code>
    /// </example>
    [Description("File Publish Connector")]
    public class FilePublishConnector : ISendingConnector, IFormattedConnector
    {
        /// <summary>
        /// Creates a new instance of the FilePublishConnector.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily")]
        public FilePublishConnector() { }
        /// <summary>
        /// Creates a new instance of the FilePublishConnector with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string that dictates how to publish files (<see cref="ConnectionString"/>).</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily")]
        public FilePublishConnector(string connectionString) { this.ConnectionString = connectionString; }
        /// <summary>
        /// Identifies a naming function.
        /// </summary>
        private delegate object NamingFunctiod(object Data);

        /// <summary>
        /// The worker class is used internally by the connector to format results. The purpose of having a separate
        /// worker class is reuse among the synchronous and asynchronous send methods.
        /// </summary>
        private class Worker
        {
            
            /// <summary>
            /// The worker id
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
            public Guid WorkerId = Guid.NewGuid();

            /// <summary>
            /// Gets or sets the formatter to use
            /// </summary>
            public IStructureFormatter Formatter { get; set; }

            /// <summary>
            /// Occurs when the operation is complete
            /// </summary>
            public event WaitCallback Completed;

            /// <summary>
            /// Gets or sets the target file name
            /// </summary>
            public string TargetFile { get; set; }

            /// <summary>
            /// Gets or sets the result of the formatting operation performed by the formatter
            /// </summary>
            public ISendResult Result { get; private set; }

            /// <summary>
            /// Starts the worker's working process. This work function is responsible for formatting 
            /// and sending the object.
            /// </summary>
            /// <param name="state">The message that is to be sent.</param>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
            public void Work(object state)
            {
                // Prepare stream
                Stream s = null;
                FileSendResult result = new FileSendResult();
                IGraphable data = (IGraphable)state;
                IFormatterGraphResult fResult = null;

                try
                {
                    // Graph the object.
                    //We graph to a memory stream and transfer to a file stream on success because
                    //the state of the stream is unknown if the result code is not accepted. This
                    //could cause a process which is listening to new files to read an invalid or
                    //non-conformant message from the publish service. In this model, we graph,
                    //verify and finally commit.
                    MemoryStream ms = new MemoryStream();
                    fResult = Formatter.Graph(ms, data);
                    result.Code = fResult.Code;
                        result.Details = fResult.Details;
                    
                    // Did the operation succeed?
                    if (result.Code == ResultCode.Accepted || 
                        result.Code == ResultCode.AcceptedNonConformant)
                    {
                        //TODO: Should transfer in chunks instead of all at once.
                        s = System.IO.File.Create(TargetFile);
                        ms.WriteTo(s);
                        ms.Close();

                    }
                }
                catch (MessageValidationException e)
                {
                    result.Code = ResultCode.Rejected;
                    List<IResultDetail> dtl = new List<IResultDetail>(new IResultDetail[] { new FileResultDetail(ResultDetailType.Error, e.Message, TargetFile, e) });
                    dtl.AddRange(fResult.Details ?? new IResultDetail[0]);
                    result.Details = dtl.ToArray();
                }
                catch (FormatException e)
                {
                    result.Code = ResultCode.Rejected;
                    result.Details = new IResultDetail[] { new FileResultDetail(ResultDetailType.Error, e.Message, TargetFile, e) };
                }
                catch (DirectoryNotFoundException e)
                {
                    result.Code = ResultCode.NotAvailable;
                    result.Details = new IResultDetail[] { new FileResultDetail(ResultDetailType.Error, e.Message, TargetFile, e) };
                }
                catch (IOException e)
                {
                    result.Code = ResultCode.Error;
                    result.Details = new IResultDetail[] { new FileResultDetail(ResultDetailType.Error, e.Message, TargetFile, e) };
                }
                catch (Exception e)
                {
                    result.Code = ResultCode.Error;
                    result.Details = new IResultDetail[] { new FileResultDetail(ResultDetailType.Error, e.Message, TargetFile, e) };
                }
                finally
                {
                    if (s != null) s.Close();
                    if (result.Code != ResultCode.Accepted)
                        System.IO.File.Delete(TargetFile);
                }

                this.Result = result;

                // Fire completed event
                if (Completed != null) Completed(this);

            }
        }

        /// <summary>
        /// Generates a filename for the given data.
        /// </summary>
        /// <returns>A filename which is not already present in the target directory and represents the data in some way.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        private string GenerateTargetFile(IGraphable data)
        {

            // Is this directory even open?
            if (!IsOpen())
                throw new InvalidOperationException("Can't call send operation before .Open is called!");

            string fn = targetFile;

            // Find a functiod (example: Guid/ID/etc...) that can be used to generate the name of the file
            if (targetFunctiod != null)
                fn = targetFunctiod(data).ToString();

            // Non overwrite, get a new fn (filename)
            string origFn = fn;
            int i = 0;
            while(System.IO.File.Exists(Path.Combine(targetDir, fn)) && !overwriteFile)
            {
                fn = string.Format("{0}.{1}", origFn, i);
                i++;
            }

            // Combine & return
            return Path.Combine(targetDir, fn);
        }

        #region Fields
        private string targetDir = "." + Path.DirectorySeparatorChar.ToString();
        private NamingFunctiod targetFunctiod;
        private string targetFile = "output";
        private bool overwriteFile = true;
        private Dictionary<IAsyncResult, ISendResult> asyncResults = new Dictionary<IAsyncResult, ISendResult>();
        private bool isOpen = false;
        #endregion 

        #region ISendingConnector Members

        /// <summary>
        /// Send the structure <paramref name="data"/> to the file system.
        /// </summary>
        /// <param name="data">The <see cref="MARC.Everest.Interfaces.IGraphable"/> data to be sent.</param>
        /// <returns>The result of the send operation.</returns>
        /// <example>
        /// Get the result of a send operation
        /// <code>
        /// FileSendResult result = conn.Send(instance);
        /// if(result.Code != ResultCode.Accepted) // Result was not successful
        ///     foreach(IResultDetail dtl in result.Details)
        ///         Console.WriteLine(dtl.Message);
        ///         </code>
        /// </example>
        /// <seealso cref="BeginSend"/>
        public ISendResult Send(MARC.Everest.Interfaces.IGraphable data)
        {
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // Setup the Worker
            Worker w = new Worker();
            w.Formatter = (IStructureFormatter)this.Formatter.Clone();
            w.TargetFile = GenerateTargetFile(data);
            w.Work(data);
            
            return w.Result;
        }

        /// <summary>
        /// Starts an asynchronous send to the file system.
        /// </summary>
        /// <param name="data">The <see cref="MARC.Everest.Interfaces.IGraphable"/> data to be sent.</param>
        /// <param name="callback">The callback to execute when the send is complete.</param>
        /// <param name="state">An object representing state.</param>
        /// <returns>An instance of a callback pointer.</returns>
        /// <example>
        /// Sending an instance asynchronously using a wait handle
        /// <code>
        /// IAsyncResult sendResult = conn.BeginSend(instance, null, null);
        /// sendResult.AsyncWaitHandle.WaitOne(); // Wait for send to finish
        /// FileSendResult result = conn.EndSend(sendResult);
        /// if(result.Code != ResultCode.Accepted) // Result was not successful
        ///     foreach(IResultDetail dtl in result.Details)
        ///         Console.WriteLine(dtl.Message);
        /// </code>
        /// </example>
        /// <seealso cref="Send"/>
        public IAsyncResult BeginSend(MARC.Everest.Interfaces.IGraphable data, AsyncCallback callback, object state)
        {
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // Setup wroker
            Worker w = new Worker();
            // Create a new instance of the formatter
            w.Formatter = Formatter.Clone() as IStructureFormatter;
            w.TargetFile = GenerateTargetFile(data);
            
            // Set async result
            IAsyncResult Result = new SendResultAsyncResult(state, new AutoResetEvent(false));

            // Completed delegate
            w.Completed += delegate(object Sender)
            {
                Worker sWorker = Sender as Worker; // Strong type sender
                // Lookup the result in the dictionary
                if (!asyncResults.ContainsKey(Result))
                    lock (asyncResults) { asyncResults.Add(Result, sWorker.Result); }
                (Result as SendResultAsyncResult).SetComplete(); // Set completed
                (Result.AsyncWaitHandle as AutoResetEvent).Set(); // send signal
                if (callback != null) callback(Result); // callback
            };

            // Add to thread pool
            ThreadPool.QueueUserWorkItem(new WaitCallback(w.Work), data);

            return Result;
        }

        /// <summary>
        /// Finishes an asynchronous send operation and retreive the result of the send.
        /// </summary>
        /// <param name="asyncResult">The instance of the callback pointer.</param>
        /// <returns>The result of the send operation.</returns>
        /// <example cref="BeginSend(MARC.Everest.Interfaces.IGraphable,AsyncCallback,object)">
        /// </example>
        public ISendResult EndSend(IAsyncResult asyncResult)
        {
            //TODO: This should block until completed.
            if (!asyncResult.IsCompleted)
                return null;
            else
            {
                ISendResult result = asyncResults[asyncResult];
                lock (asyncResults) { asyncResults.Remove(asyncResult); }
                return result;
            }
        }

        #endregion

        #region IConnector Members

        /// <summary>
        /// Gets or sets the connection string. 
        /// </summary>
        /// <remarks>
        /// Valid parameters are:
        /// <list>
        /// <listheader><term>Key</term><description>Description</description></listheader>
        /// <item><term>directory</term><description>The directory to publish files to.</description></item>
        /// <item><term>file</term><description>The name of the file to publish to.</description></item>
        /// <item><term>namer</term><description>Naming convention.</description></item>
        /// <item><term>overwrite</term><description>True if files should be overwritten.</description></item>
        /// </list>
        /// </remarks>
        public string ConnectionString { get; set; }

        /// <summary>
        /// "Open" a connection to the file system. This doesn't actually open any file 
        /// handles, however it verifies the directory. 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToBoolean(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public void Open()
        {
            // Connection string
            if (ConnectionString == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_CONNECTION_STRING, ConnectorException.ReasonType.NullConnectionString);
            else if (Formatter == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_FORMATTER, ConnectorException.ReasonType.NullFormatter);

            // Split to key value pairs
            string[] kvps = ConnectionString.Split(';');
            foreach (var kv in ConnectionStringParser.ParseConnectionString(this.ConnectionString))
            {
                switch (kv.Key)
                {
                    case "directory":
                        this.targetDir = kv.Value[0];
                        break;
                    case "file":
                        this.targetFile = kv.Value[0];
                        break;
                    case "naming":
                        switch (kv.Value[0].ToLower())
                        {
                            case "guid": // guid naming
                                this.targetFunctiod = delegate(object data) { return System.Guid.NewGuid(); };
                                break; 
                            case "id": // id naming
                                this.targetFunctiod = delegate(object data) 
                                {
                                    if (data.GetType().GetInterface("MARC.Everest.Interfaces.IIdentifiable") != null)
                                        return string.Format("{0}.{1}", ((IIdentifiable)data).Id.Root, ((IIdentifiable)data).Id.Extension);
                                    else
                                        throw new InvalidOperationException("Can't determine name for object as it does not implemnt IIDentifiable");
                                };
                                break;
                        }
                        break;
                    case "overwrite":
                        this.overwriteFile = Convert.ToBoolean(kv.Value[0]);
                        break;
                }
            }

            // Ensure the directory exists
            if (!Directory.Exists(this.targetDir))
                throw new DirectoryNotFoundException(string.Format("Could not find directory '{0}'", this.targetDir));

            // Ensure we have write permission
            try
            {
                string fn = Path.Combine(this.targetDir, Guid.NewGuid().ToString());
                System.IO.File.Create(fn).Close();
                System.IO.File.Delete(fn);
            }
            catch (Exception) //TODO: This should throw a more specific exception.
            {
                throw new Exception(string.Format("No write permission to directory '{0}'", this.targetDir));
            }

            this.isOpen = true; // Set connection to open
        }

        /// <summary>
        /// Close the file connection.
        /// </summary>
        public void Close()
        {
            this.isOpen = false;
        }

        /// <summary>
        /// Returns true if the file connection has been verified (ie: opened).
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            return isOpen;
        }

        #endregion

        #region IFormattedConnector Members

        /// <summary>
        /// Gets or sets the <see cref="MARC.Everest.Connectors.IStructureFormatter"/> to use for sending messages to the file system.
        /// </summary>
        public IStructureFormatter Formatter { get; set; }

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
