/*
 * This sample illustrates the use of the WCF Client Connector to create an XML message that is sent
 * to a WCF service. In order to run this sample, you will need to run the Samples.Everest.Connectors.WcfService
 * sample and communicate on localhost:8000.
 * 
 * To see the messages using a proxy debugger:
 *  - Change http://localhost:8000 in the app.config to http://computername:8000 (where computer name is your
 *    computer name). 
 *  - Uncomment the system.net section in the app.config
 *  - Change the Samples.Everest.Connectors.WcfService endpoint to http://computername:8000
 *  
 * When you run this sample with the proxy debugger running (example: Fiddler), you will be able to see 
 * the SOAP messages being communicated. You may also uncomment other sections of the App.config and 
 * watch the different messages that are sent in the proxy debugger.
 * 
 * Author: Justin Fyfe
 * Date: September 12, 2009
 * 
 * Changes:
 * 
 *  Justin Fyfe - October 22, 2010 :
 *       - Updated references to combined references
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors.WCF;
using MARC.Everest.Interfaces;
using MARC.Everest.RMIM.CA.R020401.Interactions;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.CA.R020401.Vocabulary;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace Samples.Everest.Connectors.WcfClient
{
    class Program
    {

        /// <summary>
        /// Create an empty instance to send to the remote service
        /// </summary>
        /// <returns>An emtpy instance</returns>
        /// <remarks>IGraphable is similar to ISerializable. It identifies that a class can be represented 
        /// using a formatter.</remarks>
        static IGraphable CreateInstance()
        {
            // We can use the shortcut constructors to make creating instances easier. If you are confused about what
            // any of these parameters are, please consult the Hello World tutorial as it explains each parameter in 
            // more detail
            REPC_IN000076CA instance = new REPC_IN000076CA(
                new II(Guid.NewGuid()), // The message ID uniquely identifies the message. 
                //It is an II.TOKEN so only a ROOT is required
                DateTime.Now, // The creation time of the message identifies when the message was 
                // created. Since this is an exact TS, we can use DateTime.Now with no precision
                ResponseMode.Immediate, // The response mode tells the receiver how to process the 
                // message. In our case, we want the message processed Immediately
                REPC_IN000076CA.GetInteractionId(), // We need to explicitly set the ID of the interaction 
                // that is to be processed, this is done for consistency 
                // (esp when batching is performed)
                REPC_IN000076CA.GetProfileId(), // We also need to tell the receiver 
                // which profile we are conforming to.
                ProcessingID.Training, // Identifies how the message should be processed on the receiver. 
                // Training means the message shouldn't actually update data on the receiver
                AcknowledgementCondition.Always, // Identifies when the message should be acknowledged. 
                // We always want an acknowledgement in this case
                new MARC.Everest.RMIM.CA.R020401.MCCI_MT002300CA.Receiver(), // The sender/receiver are used 
                // by the receiver to validate 
                // that the sender is permitted 
                // to communicate with the host
                new MARC.Everest.RMIM.CA.R020401.MCCI_MT002300CA.Sender() // and that the message is being 
                // processed by the intended 
                // recipient (just like email)
            );

            // Set the receiver and sender structures in detail
            instance.Receiver.Device = new MARC.Everest.RMIM.CA.R020401.MCCI_MT002300CA.Device2(
                    new II("1.2.3.4", "RecipientDeviceExtension")
                );
            instance.Sender.Device = new MARC.Everest.RMIM.CA.R020401.MCCI_MT002300CA.Device1(
                    new II("1.2.3.4", "SenderExtension")
                    );
            instance.Sender.Device.Name = "WcfClient Sample Application"; // Additional data can be set about the sender

            // Create the payload, again we'll use the shortcut method
            instance.controlActEvent = REPC_IN000076CA.CreateControlActEvent(
                new II("1.2.3.4.5", Guid.NewGuid().ToString()), // The control act identifier uniquely 
                // identifies the event (or transaction) 
                // that occured. This is different from 
                // the message identifier that identifies
                // the message
                REPC_IN000076CA.GetTriggerEvent(), // The trigger event or controlActEvent.Code property is
                // used to identify the type of event that occured. In
                // many cases, the trigger event for an interaction is 
                // fixed, and can be accessed using the .TRIGGER_EVENT
                // constant
                new MARC.Everest.RMIM.CA.R020401.MCAI_MT700210CA.RecordTarget(), // The record target identifies
                // the target of the event. In this case, it is the patient
                // that we are recording the discharge for
                new MARC.Everest.RMIM.CA.R020401.MCAI_MT700211CA.Author(), // The author identifies the person
                // that is responsible for the act
                new MARC.Everest.RMIM.CA.R020401.MCAI_MT700210CA.Subject2<MARC.Everest.RMIM.CA.R020401.REPC_MT220001CA.Document>()
                // The subject is the actual data that is being created,
                // updated or retrieved from the remote system
            );

            // Create the record target
            instance.controlActEvent.RecordTarget.SetPatient1(new SET<II>(new II("1.1.1.1.1", "1234"), II.Comparator),
                new MARC.Everest.RMIM.CA.R020401.COCT_MT050207CA.Person(
                    new PN(
                        EntityNameUse.Legal, 
                        new ENXP[] { 
                            new ENXP("John", EntityNamePartType.Given),
                            new ENXP("Smith", EntityNamePartType.Family)
                        }
                    ), 
                    AdministrativeGender.Male,
                    DateTime.Parse("1984-07-07")
                   )
               );

            // Create the author
            instance.controlActEvent.Author.Time = DateTime.Now;
            instance.controlActEvent.Author.SetAuthorPerson(
                new MARC.Everest.RMIM.CA.R020401.COCT_MT090102CA.AssignedEntity(new SET<II>(new II("1.1.1.1.1", "1234"), II.Comparator)
                ));

            // This sample doesn't illustrate the creation of a complete discharge, however in order
            // to pass preliminary validation we'll need to set some null flavors on things we don't
            // want populated. RMIM classes can't infer nullflavors from the data types class, so they
            // use an auto-generated nullflavor from the MARC.Everest.RMIM.CA.R020401.Vocabulary.NullFlavor
            // enumeration
            instance.controlActEvent.Subject.NullFlavor = NullFlavor.NoInformation;

            return instance;
        }

        static void Main(string[] args)
        {

            // Create the WcfClient connector
            WcfConnectionStringBuilder builder = new WcfConnectionStringBuilder();
            builder.EndpointName = "ApplicationClient";
            WcfClientConnector client = new WcfClientConnector(builder.GenerateConnectionString()); // Endpoint name should match the ep name in app.config

            // Setup the formatter
            client.Formatter = new MARC.Everest.Formatters.XML.ITS1.Formatter();
            client.Formatter.GraphAides.Add(new DatatypeFormatter());

            // We want the service to validate our messages, not the formatter, so lets just turn off the 
            // formatter validation for now
            (client.Formatter as MARC.Everest.Formatters.XML.ITS1.Formatter).ValidateConformance = false;

            // Open the connection
            client.Open();

            // Start the async send
            IAsyncResult iaSendResult = client.BeginSend(CreateInstance(), null, null);
            Console.WriteLine("Formatting and sending, please wait...");
            iaSendResult.AsyncWaitHandle.WaitOne(); // Wait until the response is received

            // Now we have to check that the message was actually sent to the remote system
            WcfSendResult sndResult = client.EndSend(iaSendResult) as WcfSendResult;

            // The result of sending the message wasn't good.
            if (sndResult.Code != ResultCode.Accepted && sndResult.Code != ResultCode.AcceptedNonConformant)
                Console.WriteLine("The message wasn't sent!");
            else // The connector has sent the message 
            {
                // Receive the result (this involves waiting...)
                IAsyncResult iaRcvResult = client.BeginReceive(sndResult, null, null);
                Console.WriteLine("Parsing result...");
                iaRcvResult.AsyncWaitHandle.WaitOne(); // Wait for deserialization
                WcfReceiveResult rcvResult = client.EndReceive(iaRcvResult) as WcfReceiveResult;

                // Now lets print out the structure
                client.Formatter.Graph(Console.OpenStandardOutput(), rcvResult.Structure);
            }

            // Close the connection
            client.Close();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
