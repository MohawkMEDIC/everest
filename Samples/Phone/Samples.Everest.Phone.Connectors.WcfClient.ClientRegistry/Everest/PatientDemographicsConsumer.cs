using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Connectors.WCF;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace Samples.Everest.Phone.Connectors.WcfClient.ClientRegistry.Everest
{
    public class PatientDemographicsConsumer
    {
        // Dispatcher
        private Dispatcher m_dispatcher;

        // Client connector
        private WcfClientConnector m_clientConnector;

        /// <summary>
        /// Query has completed
        /// </summary>
        public event EventHandler<PatientDemographicsQueryCompletedEventArgs> QueryCompleted;

        
        /// <summary>
        /// Patient demographics consumer
        /// </summary>
        /// <param name="callbackDispatcher"></param>
        public PatientDemographicsConsumer(Dispatcher callbackDispatcher)
        {
            this.m_dispatcher = callbackDispatcher;
            // Create
            this.m_clientConnector = new WcfClientConnector("endpointname=pds");
            this.m_clientConnector.Formatter = new XmlIts1Formatter()
            {
                ValidateConformance = false
            };
            this.m_clientConnector.Formatter.GraphAides.Add(new DatatypeFormatter()
                {
                    ValidateConformance = false
                });

        }

        /// <summary>
        /// Search for a patient based on name
        /// </summary>
        public void Search(String patientName)
        {
            // Attempt to split the name
            string firstName = String.Empty, lastName = String.Empty;
            Regex re = new Regex(@"^(.*?)[,](.*?)$");
            var match = re.Match(patientName + ",");
            if (!match.Success) return;
            
            // Next, get the parts
            if (match.Groups.Count > 1)
                lastName = match.Groups[1].Value.Replace(",", "");
            if(match.Groups.Count > 2)
                firstName = match.Groups[2].Value.Replace(",", "");
            
            // Now search
            PRPA_IN201305UV02 pdQuery = new PRPA_IN201305UV02(
                Guid.NewGuid(),
                DateTime.Now,
                PRPA_IN201305UV02.GetInteractionId(),
                ProcessingID.Production,
                "I",
                AcknowledgementCondition.Always,
                new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Receiver(
                    new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Device(
                        SET<II>.CreateSET(new II("1.3.6.1.4.1.33349.3.1.1.20.4", "CR-FAKE"))
                    )
                ),
                new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Sender(
                    new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Device(
                        SET<II>.CreateSET(new II("1.2.3.4.5.4", "Sample"))
                    )
                ),
                new MARC.Everest.RMIM.UV.NE2008.QUQI_MT021001UV01.ControlActProcess<MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.QueryByParameter>(
                    "EVN"
                )
                {
                    Id = SET<II>.CreateSET(Guid.NewGuid()),
                    EffectiveTime = new IVL<TS>(DateTime.Now),
                    Code = Util.Convert<CD<String>>(PRPA_IN201305UV02.GetTriggerEvent()),
                    queryByParameter = new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.QueryByParameter(
                        Guid.NewGuid(),
                        "New",
                        new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.ParameterList()
                    )
                }
            );
          
            // Build name
            EN searchName = new EN(EntityNameUse.Search, new ENXP[]{});
            foreach(var s in lastName.Split(' '))
                searchName.Part.Add(new ENXP(s, EntityNamePartType.Family));
            if(!String.IsNullOrEmpty(firstName))
                foreach(var s in firstName.Split(' '))
                    searchName.Part.Add(new ENXP(s, EntityNamePartType.Given));

            // Query parameters
            pdQuery.controlActProcess.queryByParameter.ParameterList.LivingSubjectName.Add(new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.LivingSubjectName(
                SET<EN>.CreateSET(searchName),
                "livingSubject.Name"
            ));

            // Send
            if(!this.m_clientConnector.IsOpen())
                this.m_clientConnector.Open();

            this.m_clientConnector.BeginSend(pdQuery, this.OnBeginSend, null);
        }

        /// <summary>
        /// Begin send callback
        /// </summary>
        private void OnBeginSend(IAsyncResult result)
        {
            // End send operation
            var sendResult  = this.m_clientConnector.EndSend(result);
            // Check to make sure the message made it
            if (sendResult.Code != ResultCode.Accepted &&
                sendResult.Code != ResultCode.AcceptedNonConformant)
                return;

            // Receive the response
            var receiveResult = this.m_clientConnector.Receive(sendResult);

            try
            {
                var resultMsg = receiveResult.Structure as PRPA_IN201306UV02;

                // Process results, this is a sample so we'll just stuff the message results
                // in the event arg and notify the handler
                if (this.QueryCompleted != null)
                {

                    PatientDemographicsQueryCompletedEventArgs e;
                    if (resultMsg.Acknowledgement[0].TypeCode != AcknowledgementType.ApplicationAcknowledgementAccept)
                        e = new PatientDemographicsQueryCompletedEventArgs();
                    else
                        e = new PatientDemographicsQueryCompletedEventArgs(resultMsg.controlActProcess.Subject);
                    this.m_dispatcher.BeginInvoke(this.QueryCompleted, this, e);
                }
            }
            finally
            {
                this.m_clientConnector.Close();
            }
        }
    }
}
