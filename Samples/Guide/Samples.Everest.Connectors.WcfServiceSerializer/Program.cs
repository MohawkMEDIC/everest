using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using System.IO;
using System.Xml;
using MARC.Everest.RMIM.CA.R020402;
using MARC.Everest.RMIM.CA.R020402.Interactions;
using MARC.Everest.RMIM.CA.R020402.Vocabulary;
using MARC.Everest.Connectors.WCF;
using MARC.Everest;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;


namespace Samples.Everest.Connectors.WcfServiceSerializer
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create the ServiceHost.
            using (ServiceHost host = new ServiceHost(typeof(ServiceBehavior)))
            {

                host.AddServiceEndpoint(typeof(IServiceContract), new BasicHttpBinding(), "http://localhost:8000");

                foreach (var b in host.Description.Behaviors)
                    if (b is ServiceDebugBehavior)
                        (b as ServiceDebugBehavior).IncludeExceptionDetailInFaults = true;

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                Console.WriteLine("The service is ready at {0}", "http://localhost:8000");
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }

        }
    }
}
