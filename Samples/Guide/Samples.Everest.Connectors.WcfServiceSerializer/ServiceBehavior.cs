using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.RMIM.CA.R020402.Vocabulary;
using MARC.Everest.RMIM.CA.R020402.Interactions;
using MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA;

using MARC.Everest.DataTypes;
using System.ServiceModel;
using MARC.Everest.Connectors.WCF.Serialization;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Interfaces;
using MARC.Everest.Attributes;

namespace Samples.Everest.Connectors.WcfServiceSerializer
{

    public class ServiceBehavior : IServiceContract
    {
        #region IServiceContract Members

        /// <summary>
        /// Default message
        /// </summary>
        /// <returns></returns>
        public MARC.Everest.RMIM.CA.R020402.Interactions.MCCI_IN000002CA Anything(IGraphable request)
        {
            
            return new MARC.Everest.RMIM.CA.R020402.Interactions.MCCI_IN000002CA(
                Guid.NewGuid(),
                DateTime.Now,
                ResponseMode.Immediate,
                MCCI_IN000002CA.GetInteractionId(),
                MCCI_IN000002CA.GetProfileId(),
                ProcessingID.Production,
                AcknowledgementCondition.Never,
                new Receiver(
                    new TEL() { NullFlavor = NullFlavor.NoInformation },
                    new Device2(
                        new II() { NullFlavor = NullFlavor.NoInformation }
                    )
                ),
                new Sender(
                    new TEL(OperationContext.Current.Channel.LocalAddress.Uri.ToString()),
                    new Device1(
                        new II("1.2.3.4", "1234"),
                        "Sample Service",
                        "A sample service",
                        null,
                        "Mohawk College of Applied Arts and Technology",
                        "Everest"
                    )
                ),
                new Acknowledgement(
                    AcknowledgementType.ApplicationAcknowledgementAccept,
                    new TargetMessage() { NullFlavor = NullFlavor.NoInformation },
                    new AcknowledgementDetail(
                        AcknowledgementDetailType.Information,
                        AcknowledgementDetailCode.InternalSystemError,
                        String.Format("You just used the Everest Serializer! Received a '{0}'", request.GetType().Name),
                        null
                    )
                )
            );
                    
                    
        }

        #endregion
    }
}
