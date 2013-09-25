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
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using System.Threading;
using MARC.Everest.Interfaces;
using MARC.Everest.Exceptions;
using System.ComponentModel;

namespace MARC.Everest.Connectors.MSMQ
{
    /// <summary>
    /// The MSMQ Publish provides a mechanism for publishing data to an Microsoft Message Queue.
    /// </summary>
    /// <example>
    /// 
    /// <code lang="cs" title="Publish variable 'instance' to Queue .\Private$\test">
    ///        PRPA_IN101001CA instance = new PRPA_IN101001CA();
    ///        MsmqPublishConnector conn = new MsmqPublishConnector();
    ///        conn.Formatter = new MARC.Everest.Formatters.XML.ITS1.Formatter(); //"queue=.\Private$\test"
    ///        conn.Formatter.GraphAides.Add(typeof(MARC.Everest.Formatters.XML.Datatypes.R1.Formatter));
    ///        conn.Open();
    ///        conn.Send(instance);
    ///        conn.Close();
    /// </code>
    /// </example> 
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Msmq")]
    [Description("MSMQ Publish Connector")]
    public class MsmqPublishConnector : ISendingConnector, IFormattedConnector, IDisposable
    {

        /// <summary>
        /// Creates a new instance of the Msmq
        /// </summary>
        public MsmqPublishConnector() { }
        /// <summary>
        /// Create a new instance of the Msmq connector with the specified connection string
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        public MsmqPublishConnector(string connectionString) { this.ConnectionString = connectionString; }

        /// <summary>
        /// Message queue that represents the connection to the queue
        /// </summary>
        private MessageQueue queue;

        /// <summary>
        /// Worker
        /// </summary>
        private class Worker
        {
            /// <summary>
            /// Result of the operation
            /// </summary>
            public MsmqSendResult Result { get; set; }
            /// <summary>
            /// Formatter
            /// </summary>
            public IStructureFormatter Formatter { get; set; }
            /// <summary>
            /// Callback
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public WaitCallback Completed { get; set; }
            /// <summary>
            /// A shared handle to the parent's queue
            /// </summary>
            public MessageQueue Queue { get; set; }
            /// <summary>
            /// Perform the work (formatting)
            /// </summary>
            /// <param name="state">The user state in this function is an instance of IGraphable</param>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
            public void Work(object state)
            {
                IGraphable data = (IGraphable)state;
                MessageQueueTransaction tx = null;
                IFormatterGraphResult gResult = null;

                try
                {
                    // Create a message and prepare for sending
                    Message msg = new Message();
                    Result = new MsmqSendResult();

                    // Format and prepare result
                    gResult = Formatter.Graph(msg.BodyStream, data);
                    Result.Code = gResult.Code;
                    Result.Details = gResult.Details;

                    // Accepted messages get sent
                    if (Result.Code == ResultCode.Accepted || Result.Code == ResultCode.AcceptedNonConformant)
                    {
                        // Create a transaction if the queue is transactional
                        if (Queue.Transactional)
                        {
                            tx = new MessageQueueTransaction();
                            tx.Begin();
                        }

                        // Publish
                        if (tx != null) Queue.Send(msg, tx);
                        else Queue.Send(msg);

                        // Commit the transaction
                        if (tx != null) tx.Commit();
                    }
                    
                }
                catch (MessageValidationException e)
                {
                    Result.Code = ResultCode.Rejected;
                    List<IResultDetail> dtl = new List<IResultDetail>(new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) });
                    dtl.AddRange(gResult.Details ?? new IResultDetail[0]);
                    Result.Details = dtl.ToArray();
                    if (tx != null && tx.Status == MessageQueueTransactionStatus.Pending) tx.Abort();
                }
                catch (FormatException e)
                {
                    Result.Code = ResultCode.Rejected;
                    Result.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                    if (tx != null && tx.Status == MessageQueueTransactionStatus.Pending) tx.Abort();
                }
                catch (Exception e)
                {
                    Result.Code = ResultCode.Error;
                    Result.Details = new IResultDetail[] { new ResultDetail(ResultDetailType.Error, e.Message, e) };
                    if (tx != null && tx.Status == MessageQueueTransactionStatus.Pending) tx.Abort();
                }
                finally
                {
                }

                // Fire completed event
                if (Completed != null) Completed(this);
            }
        }

        #region ISendingConnector Members

        /// <summary>
        /// Publish a message to the currently opened queue
        /// </summary>
        /// <param name="data">The data to publish to the queue</param>
        /// <returns>A <see cref="ISendResult"/> structure representing the result of the send operation </returns>
        public ISendResult Send(MARC.Everest.Interfaces.IGraphable data)
        {
            if (!IsOpen())
                throw new ConnectorException(ConnectorException.MSG_INVALID_STATE, ConnectorException.ReasonType.NotOpen);

            // Send
            Worker w = new Worker();
            w.Formatter = Formatter.Clone() as IStructureFormatter;
            w.Queue = queue;

            // Perform the work
            w.Work(data);

            return w.Result;
        }

        /// <summary>
        /// Start an asynchronous send operation
        /// </summary>
        /// <param name="data">The data to publish to the queue</param>
        /// <param name="callback">An <see cref="AsyncCallback"/> delegate to call when the operation is completed</param>
        /// <param name="state">An object representing user state</param>
        /// <returns>An <see cref="IAsyncResult"/> structure that represent the asynchronous method</returns>
        public IAsyncResult BeginSend(MARC.Everest.Interfaces.IGraphable data, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// End the asynchronous send operation 
        /// </summary>
        /// <param name="asyncResult">The result of the BeginSend method</param>
        /// <returns>A <see cref="ISendResult"/> representing the result of the send operation</returns>
        public ISendResult EndSend(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IConnector Members

        /// <summary>
        /// Gets or sets the connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Open a connection to the queue
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <listheader><term>Key</term><description>Description</description></listheader>
        /// <item><term>Queue</term><description>The name of the queue to open</description>   </item>
        /// <item><term>Exclusive</term><description>True if shared mode should be disabled on the queue (first app to the queue has exclusive access)</description></item>
        /// <item><term>Cache</term><description>True if a connection cache should be created</description></item>
        /// </list>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToBoolean(System.String)")]
        public void Open()
        {
            // Connection string
            if (ConnectionString == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_CONNECTION_STRING, ConnectorException.ReasonType.NullConnectionString);
            else if (Formatter == null)
                throw new ConnectorException(ConnectorException.MSG_NULL_FORMATTER, ConnectorException.ReasonType.NullFormatter);

            Dictionary<string, List<string>> parms = ConnectionStringParser.ParseConnectionString(ConnectionString);

            string qName = null;
            bool qExclusive = false, qCache = false;

            // Parse parameters
            foreach(KeyValuePair<string, List<string>> parm in parms)
                switch (parm.Key)
                {
                    case "queue":
                        qName = parm.Value[0];
                        break;
                    case "exclusive":
                        qExclusive = Convert.ToBoolean(parm.Value[0]);
                        break;
                    case "cache":
                        qCache = Convert.ToBoolean(parm.Value[0]);
                        break;
                }

            // Create message queue
            queue = new MessageQueue(qName, qExclusive, qCache, QueueAccessMode.Send);
            
        }

        /// <summary>
        /// Close the connection to the queue
        /// </summary>
        public void Close()
        {
            queue.Close();
        }

        /// <summary>
        /// Returns true if the queue connection is open
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            return queue != null;
        }

        #endregion

        #region IFormattedConnector Members

        /// <summary>
        /// Gets or sets the formatter to use when sending messages to the queue
        /// </summary>
        public IStructureFormatter Formatter { get; set; }

        #endregion

        #region IDisposable Members
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly")]
        public void Dispose()
        {
            Dispose(false);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            this.Close();
            queue.Dispose();
        }

        #endregion
    }
}
