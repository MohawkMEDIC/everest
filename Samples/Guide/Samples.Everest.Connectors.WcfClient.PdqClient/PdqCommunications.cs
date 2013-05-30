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
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.DataTypes;

namespace Samples.Everest.Connectors.WcfClient.PdqClient
{
    public class PdqCommunications
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
                return SendReceive(connector, new MCCI_IN000002UV01(
                    Guid.NewGuid(),
                    DateTime.Now,
                    MCCI_IN000002UV01.GetInteractionId(),
                    ProcessingID.Debugging,
                    "T",
                    new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Receiver(),
                    new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Sender()
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
            PRPA_IN201305UV02 instance = new PRPA_IN201305UV02(
                  Guid.NewGuid(),
                  DateTime.Now,
                  PRPA_IN201305UV02.GetInteractionId(),
                  ProcessingID.Production,
                  "T",
                  AcknowledgementCondition.Always,
                  new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Receiver()
                  {
                      Telecom = new TEL("http://cr.marc-hi.ca:8080/pdqsupplier"),
                      Device = new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Device(
                        SET<II>.CreateSET(new II("1.3.6.1.4.1.33349.3.1.1.20.4", "CR"))
                      )
                  },
                  new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Sender()
                  {
                      Telecom = new TEL() { NullFlavor = NullFlavor.NoInformation },
                      Device = new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Device(
                        SET<II>.CreateSET(new II("1.2.3.4.5.4", "Sample"))
                      )
                  },
                  new MARC.Everest.RMIM.UV.NE2008.QUQI_MT021001UV01.ControlActProcess<MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.QueryByParameter>("EVN")
                  {
                      Id = SET<II>.CreateSET(Guid.NewGuid()),
                      Code = CD<String>.Parse(PRPA_IN201305UV02.GetTriggerEvent()),
                      AuthorOrPerformer = new List<MARC.Everest.RMIM.UV.NE2008.MFMI_MT700701UV01.AuthorOrPerformer>(),
                      queryByParameter = new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.QueryByParameter(
                          Guid.NewGuid(),
                          "new",
                          new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.ParameterList()

                    )
                    {
                        InitialQuantity = 10
                    }
                  }
                );

            instance.controlActProcess.EffectiveTime = new IVL<TS>(DateTime.Now);

            // Set the author
            instance.controlActProcess.AuthorOrPerformer.Add(new MARC.Everest.RMIM.UV.NE2008.MFMI_MT700701UV01.AuthorOrPerformer());
            instance.controlActProcess.AuthorOrPerformer[0].SetParticipationChoice(new MARC.Everest.RMIM.UV.NE2008.COCT_MT090300UV01.AssignedDevice(
                SET<II>.CreateSET(new II("1.2.3.4.5.4", "Sample")),
                "DEV"
              )
            );

            // Set the filter for given then family
            foreach (var enpt in new EntityNamePartType[] { EntityNamePartType.Given, EntityNamePartType.Family })
            {
                instance.Id = Guid.NewGuid();
                instance.controlActProcess.queryByParameter.QueryId = Guid.NewGuid();

                // Set the name
                instance.controlActProcess.queryByParameter.ParameterList.LivingSubjectName.Clear();
                instance.controlActProcess.queryByParameter.ParameterList.LivingSubjectName.Add(
                    new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.LivingSubjectName(
                        SET<EN>.CreateSET(new EN(EntityNameUse.Search, new ENXP[] {
                        new ENXP(filter, enpt)
                        })),
                        "livingSubject.name"
                    )
                );

                // Make the query
                PRPA_IN201306UV02 response = this.SendReceive(connector, instance) as PRPA_IN201306UV02;

                // Interpret the response code
                if (response.Acknowledgement[0].TypeCode != AcknowledgementType.ApplicationAcknowledgementAccept ||
                    response.controlActProcess == null)
                    continue;
                foreach (var subj in response.controlActProcess.Subject)
                {
                    // Ensure that the relationships exist
                    if (subj.RegistrationEvent == null ||
                        subj.RegistrationEvent.Subject1 == null ||
                        subj.RegistrationEvent.Subject1.registeredRole == null ||
                        subj.RegistrationEvent.Subject1.registeredRole.GetPatientEntityChoiceSubjectAsPRPA_MT201310UV02Person() == null ||
                        subj.RegistrationEvent.Subject1.registeredRole.GetPatientEntityChoiceSubjectAsPRPA_MT201310UV02Person().Name == null ||
                        subj.RegistrationEvent.Subject1.registeredRole.GetPatientEntityChoiceSubjectAsPRPA_MT201310UV02Person().Name.IsEmpty)
                        continue;

                    // Add the formatted name
                    var legalName = subj.RegistrationEvent.Subject1.registeredRole.GetPatientEntityChoiceSubjectAsPRPA_MT201310UV02Person().Name.Find(o => o.Use != null && o.Use.Contains(EntityNameUse.Legal));
                    if (legalName == null)
                        legalName = subj.RegistrationEvent.Subject1.registeredRole.GetPatientEntityChoiceSubjectAsPRPA_MT201310UV02Person().Name[0];
                    retVal.Add(String.Format("{0}@{1} - {2}",
                        subj.RegistrationEvent.Subject1.registeredRole.Id[0].Root,
                        subj.RegistrationEvent.Subject1.registeredRole.Id[0].Extension,
                        legalName.ToString("{FAM}, {GIV}")));
                }
                

            }

            // Now return results
            return retVal.ToArray();

        }
    }
}
