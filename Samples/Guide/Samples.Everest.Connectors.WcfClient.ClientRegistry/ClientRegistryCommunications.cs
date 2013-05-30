/*
 * This sample illustrates using the MARC-HI Everest Framework
 * to communicate with the unsecured endpoints on the MARC-HI 
 * client registry system. 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.Connectors.WCF;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;
using System.Diagnostics;
using MARC.Everest.RMIM.CA.R020402.Interactions;
using MARC.Everest.RMIM.CA.R020402.Vocabulary;
using MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA;
using MARC.Everest.RMIM.CA.R020402.MFMI_MT700751CA;
using MARC.Everest.DataTypes;

namespace Samples.Everest.Connectors.WcfClient.ClientRegistry
{
    public class ClientRegistryCommunications
    {

        /// <summary>
        /// Send a <paramref name="request "/>on the specified <paramref name="connector"/>
        /// and await a response
        /// </summary>
        public IGraphable SendReceive(ISendReceiveConnector connector, IGraphable request)
        {

            // Ensure the connector is open
            if (!connector.IsOpen())
                connector.Open();

            // Send the request
            ISendResult sendResult = connector.Send(request);

            // Was the send successful?
            if (sendResult.Code != ResultCode.Accepted &&
                sendResult.Code != ResultCode.AcceptedNonConformant)
                return null;

            // Await the response
            IReceiveResult receiveResult = connector.Receive(sendResult);

            // Debug information
            #if DEBUG
            foreach (var itm in receiveResult.Details)
                Trace.WriteLine(String.Format("{0}: {1} @ {2}", itm.Type, itm.Message, itm.Location));
            #endif
            // Structure
            return receiveResult.Structure;

        }

        /// <summary>
        /// Returns true if the client registry is available. This
        /// is done by sending an unsupported message to the CR,
        /// if the CR responds with anything then it is up, otherwise
        /// it is unavailable
        /// </summary>
        public bool IsCrAvailable(WcfClientConnector connector)
        {
            try
            {
                return SendReceive(connector, new MCCI_IN000002CA(
                    Guid.NewGuid(),
                    DateTime.Now,
                    ResponseMode.Immediate,
                    MCCI_IN000002CA.GetInteractionId(),
                    MCCI_IN000002CA.GetProfileId(),
                    ProcessingID.Debugging,
                    AcknowledgementCondition.Always,
                    new MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA.Receiver(),
                    new MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA.Sender(),
                    null
                )) != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Generate the auto complete data
        /// </summary>
        public String[] Filter(WcfClientConnector connector, string filter)
        {

            List<String> retVal = new List<String>();

            // Create the instance
            PRPA_IN101103CA instance = new PRPA_IN101103CA(
                  Guid.NewGuid(),
                  DateTime.Now,
                  ResponseMode.Immediate,
                  PRPA_IN101103CA.GetInteractionId(),
                  PRPA_IN101103CA.GetProfileId(),
                  ProcessingID.Debugging,
                  AcknowledgementCondition.Always,
                  new Receiver()
                  {
                      Telecom = new TEL("http://cr.marc-hi.ca:8080/cr"),
                      Device = new Device2(
                        new II("1.3.6.1.4.1.33349.3.1.1.20.4", "CR-FAKE"),
                        "MARC-HI Client Registry",
                        null
                      )
                  },
                  new Sender()
                  {
                      Telecom = new TEL() { NullFlavor = NullFlavor.NoInformation },
                      Device = new Device1(
                        new II("1.2.3.4.5.4", "Sample"),
                        "Everest Sample",
                        null,
                        null,
                        null,
                        "An Example"
                      )
                  },
                  new ControlActEvent<MARC.Everest.RMIM.CA.R020402.PRPA_MT101103CA.ParameterList>(
                    Guid.NewGuid(),
                    PRPA_IN101103CA.GetTriggerEvent(),
                    new Author(DateTime.Now),
                    new MARC.Everest.RMIM.CA.R020402.QUQI_MT120008CA.QueryByParameter<MARC.Everest.RMIM.CA.R020402.PRPA_MT101103CA.ParameterList>(
                        Guid.NewGuid(),
                        ResponseModality.RealTime,
                        10,
                        QueryRequestLimit.Record,
                        new MARC.Everest.RMIM.CA.R020402.PRPA_MT101103CA.ParameterList()
                    )
                ));

            instance.controlActEvent.EffectiveTime = new IVL<TS>(DateTime.Now);

            // Set the author
            instance.controlActEvent.Author.SetAuthorPerson(
              SET<II>.CreateSET(new II("1.3.6.1.4.1.21367.2010.3.2.202", "0008")),
              new MARC.Everest.RMIM.CA.R020402.COCT_MT090102CA.Person(
                new PN(EntityNameUse.License,
                  new ENXP[] {
                        new ENXP("Birth", EntityNamePartType.Family),
                        new ENXP("John", EntityNamePartType.Given)
                    }
                ),
                null
              )
            );

            // Set the filter for given then family
            foreach (var enpt in new EntityNamePartType[] { EntityNamePartType.Given, EntityNamePartType.Family })
            {
                instance.Id = Guid.NewGuid();
                instance.controlActEvent.QueryByParameter.QueryId = Guid.NewGuid();

                // Set the name
                instance.controlActEvent.QueryByParameter.parameterList.PersonName.Clear();
                instance.controlActEvent.QueryByParameter.parameterList.PersonName.Add(
                    new MARC.Everest.RMIM.CA.R020402.PRPA_MT101103CA.PersonName(
                        new PN(EntityNameUse.Legal, new ENXP[] {
                        new ENXP(filter, enpt)
                    })
                    )
                );

                // Make the query
                PRPA_IN101104CA response = this.SendReceive(connector, instance) as PRPA_IN101104CA;

                // Interpret the response code
                if (response.Acknowledgement.TypeCode != AcknowledgementType.ApplicationAcknowledgementAccept ||
                    response.controlActEvent == null)
                    continue;
                foreach (var subj in response.controlActEvent.Subject)
                {
                    // Ensure that the relationships exist
                    if (subj.RegistrationEvent == null ||
                        subj.RegistrationEvent.Subject == null ||
                        subj.RegistrationEvent.Subject.registeredRole == null ||
                        subj.RegistrationEvent.Subject.registeredRole.IdentifiedPerson == null ||
                        subj.RegistrationEvent.Subject.registeredRole.IdentifiedPerson.Name == null ||
                        subj.RegistrationEvent.Subject.registeredRole.IdentifiedPerson.Name.IsEmpty)
                        continue;

                    // Add the formatted name
                    var legalName = subj.RegistrationEvent.Subject.registeredRole.IdentifiedPerson.Name.Find(o => o.Use != null && o.Use.Contains(EntityNameUse.Legal));
                    if (legalName == null)
                        legalName = subj.RegistrationEvent.Subject.registeredRole.IdentifiedPerson.Name[0];
                    retVal.Add(String.Format("{0}@{1} - {2}",
                        subj.RegistrationEvent.Subject.registeredRole.Id[0].Root,
                        subj.RegistrationEvent.Subject.registeredRole.Id[0].Extension,
                        legalName.ToString("{FAM}, {GIV}")));
                }
                

            }

            // Now return results
            return retVal.ToArray();

        }
    }
}
