using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using System.IO;
using System.Xml;
using MARC.Everest.RMIM.UV.CDAr2;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using MARC.Everest.Connectors.File;
using MARC.Everest;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;


namespace Samples.Everest.ClinicalDocuments.CdaValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Ask the user what directory they want to listen to
                Console.Write("Enter directory to listen on (example: C:\\temp):");
                var directory = Console.ReadLine();

                // Create the directory if it doesn't exist
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // Setup the listener
                FileListenConnector flc = new FileListenConnector(String.Format("Directory={0};Pattern=*.xml", directory));

                // Create the formatter that will interpret CDAr2 (XML ITS 1.0 and Datatypes R1)
                MARC.Everest.Formatters.XML.ITS1.Formatter fmtr = new MARC.Everest.Formatters.XML.ITS1.Formatter();
                fmtr.ValidateConformance = true;
                fmtr.GraphAides.Add(new DatatypeFormatter());
                flc.Formatter = fmtr;

                // Subscribe to the message available event
                flc.MessageAvailable += new EventHandler<MARC.Everest.Connectors.UnsolicitedDataEventArgs>(flc_MessageAvailable);

                // Start the connector
                try
                {
                    flc.Open();
                    flc.Start();
                    Console.WriteLine("Waiting for messages in {0}, press any key to stop listening...", directory);
                    Console.ReadKey();
                }
                finally
                {
                    flc.Stop();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error in the execution of the example {0}", e);
            }
        }

        static void flc_MessageAvailable(object sender, MARC.Everest.Connectors.UnsolicitedDataEventArgs e)
        {
            // Get a local copy of the connector that raised the event
            var listenConnector = sender as FileListenConnector;
            // Receive the message from the listen connector's stack
            var receiveResult = listenConnector.Receive();

            // If the result was processed successfully, then show it
            if (receiveResult.Structure != null)
            {
                // Received structure should be cast as a clinical document for convenience
                var cda = receiveResult.Structure as ClinicalDocument;

                // Output the title of the CDA
                Console.WriteLine("Got a CDA titled : {0}", cda.Title);

                // If the CDA has a structured body then show the sub-sections
                if (cda.Component != null && cda.Component.BodyChoice is StructuredBody)
                    foreach (var comp in (cda.Component.BodyChoice as StructuredBody).Component)
                        Console.WriteLine("\tSubSection {0}", comp.Section.Title);
            }

            Console.WriteLine("The following issues were detected:");
            foreach (var dtl in receiveResult.Details)
                Console.WriteLine("{0} @ {1}", dtl.Type, dtl.Message);
        }
    }
}
