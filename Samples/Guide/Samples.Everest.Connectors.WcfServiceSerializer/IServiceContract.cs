using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors.WCF.Serialization;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using System.ServiceModel;
using MARC.Everest.RMIM.CA.R020402.Interactions;
using System.ServiceModel.Web;
using MARC.Everest.Interfaces;

namespace Samples.Everest.Connectors.WcfServiceSerializer
{
    [EverestSerializerFormat(Formatter = typeof(XmlIts1Formatter), GraphAide = typeof(DatatypeFormatter), ValidateConformance = false)]
    [ServiceContract(Namespace="urn:hl7-org:v3")]
    public interface IServiceContract
    {
        /// <summary>
        /// Get default
        /// </summary>
        [OperationContract(Action="*")]
        MCCI_IN000002CA Anything(IGraphable request);

    }
}
