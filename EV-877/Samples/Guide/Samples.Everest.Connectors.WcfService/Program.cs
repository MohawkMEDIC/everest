/*
 * This sample illustrates the use of the WCF Server Connector in a standalone application setting. It 
 * listens for SOAP messages on http://localhost:8000 and responds to them with a generic acknowledgement. The
 * acknowledgement contains validation errors from the received message.
 * 
 * Note: You can use the Samples.Everest.Connectors.WcfClient project to contact this service.
 * 
 * See App.Config for more experiments you can do with this sample.
 * 
 * Author: Justin Fyfe
 * Date: September 10, 2009
 * 
 * Changes:
 *      Justin Fyfe - October 22, 2010
 *          Updated references to combined classes
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors.WCF;
using MARC.Everest.RMIM.CA.R020401.Interactions;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.CA.R020401.Vocabulary;
using MARC.Everest.RMIM.CA.R020401.MCCI_MT002200CA;
using MARC.Everest.Interfaces;
using System.Reflection;
using MARC.Everest.Connectors;
using MARC.Everest.RMIM.CA.R020401.MCCI_MT002300CA;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace Samples.Everest.Connectors.WcfService
{
    public class ServerTest
    {
        // Main function
        public static void Main(string[] args)
        {
            Assembly.Load(new AssemblyName("MARC.Everest.RMIM.CA.R020401"));

            // Instantiate the connector
            WcfServerConnector connector = new WcfServerConnector();
            // Assign the formatter
            connector.Formatter = new MARC.Everest.Formatters.XML.ITS1.Formatter();
            connector.Formatter.GraphAides.Add(
                new DatatypeFormatter()
            );

            // Subscribe to the message available event
            connector.MessageAvailable += new EventHandler<UnsolicitedDataEventArgs>(
                           connector_MessageAvailable
                        );

            // Open and start the connector
            try
            {
                WcfConnectionStringBuilder builder = new WcfConnectionStringBuilder();
                builder.ServiceName = "ApplicationService";
                connector.ConnectionString = builder.GenerateConnectionString();
                connector.Open();
                connector.Start();

                Console.WriteLine("Server started, press any key to close...");
                Console.ReadKey();

                // Teardown
                connector.Stop();
                connector.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Delegate handles the message available event
        static void connector_MessageAvailable(object sender,
                 UnsolicitedDataEventArgs e)
        {
            // Cast connector
            WcfServerConnector connector = sender as WcfServerConnector;
            connector.InvalidResponse += new
                EventHandler<MessageEventArgs>(connector_InvalidResponse);
            // Receive the structure
            WcfReceiveResult rcvResult = connector.Receive() as WcfReceiveResult;

            if (rcvResult.Structure != null)
                Console.WriteLine(rcvResult.Structure.GetType().Name);

            // Prepare acknowledgement structure
            Acknowledgement acknowledgement = new Acknowledgement();

            // Assign the correlation
            acknowledgement.TargetMessage = new TargetMessage(
                     (rcvResult.Structure as IIdentifiable).Id
            );

            // Determine the deserialization outcome
            if (rcvResult.Code != ResultCode.Accepted &&
                rcvResult.Code != ResultCode.AcceptedNonConformant)
                // There were problems parsing the request message
                acknowledgement.TypeCode =
                    AcknowledgementType.AcceptAcknowledgementCommitError;
            else
                // Message is all good
                acknowledgement.TypeCode =
                   AcknowledgementType.AcceptAcknowledgementCommitAccept;

            // Append all details
            foreach (IResultDetail dtl in rcvResult.Details)
            {
                AcknowledgementDetail detail = new AcknowledgementDetail(
                        AcknowledgementDetailType.Information,
                        AcknowledgementDetailCode.SyntaxError,
                        dtl.Message, 
                        new SET<ST>((ST)dtl.Location)
                        );
                acknowledgement.AcknowledgementDetail.Add(detail);
            }

            // Create a response
            MCCI_IN000002CA response = new MCCI_IN000002CA(
                new II(Guid.NewGuid()),
                DateTime.Now,
                ResponseMode.Immediate,
                MCCI_IN000002CA.GetInteractionId(),
                MCCI_IN000002CA.GetProfileId(),
                ProcessingID.Production,
                AcknowledgementCondition.Never,
                new
                 MARC.Everest.RMIM.CA.R020401.MCCI_MT102001CA.Receiver(
                 new
                 MARC.Everest.RMIM.CA.R020401.MCCI_MT102001CA.Device2(
                        new II()
                 )
                ),
                new
                 MARC.Everest.RMIM.CA.R020401.MCCI_MT102001CA.Sender(
                 new
                  MARC.Everest.RMIM.CA.R020401.MCCI_MT102001CA.Device1(
                        new II("1.1.1.1.1")
                 )
                ),
                acknowledgement
            );

            // Send the result
            connector.Send(response, rcvResult);
        }

        static void connector_InvalidResponse(object sender, MessageEventArgs e)
        {
            if (e.Code != ResultCode.NotAvailable)
                e.Alternate = new MCCI_IN000002CA(); // Construct alternative 

            // if e.Alternate is not specified a soap fault is thrown
        } // end connector_InvalidResponse

    }
}

