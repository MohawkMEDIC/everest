using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors.File;
using MARC.Everest.Formatters.XML.ITS1;
using System.Reflection;
using MARC.Everest.Connectors;
using System.IO;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace InstanceTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // This example uses the Everest File Connector to listen to a directory and validate messages that appear there
            // It can be used as a standalone application as a way of validating instance messages from other sources

            // copied the below code from the example in the dev guide page 50
            Assembly.Load(new AssemblyName("MARC.Everest.RMIM.CA.R020401"));

            XmlIts1Formatter its1Formatter = new XmlIts1Formatter();
            its1Formatter.GraphAides.Add(
                new DatatypeFormatter()
            );

            // Select a folder
            string directory = String.Empty;
            while (String.IsNullOrEmpty(directory) || !Directory.Exists(directory))
            {
                Console.Write("Enter directory to listen to:");
                directory = Console.ReadLine();
            }

            // We can use a connection string builder to make creating the connection string easier
            FileConnectionStringBuilder connStringBuilder = new FileConnectionStringBuilder();
            connStringBuilder.Directory = directory;
            connStringBuilder.KeepFiles = true;
            connStringBuilder.Pattern = "*.xml";

            // Create an instance of the connector
            FileListenConnector connector = new FileListenConnector(connStringBuilder.GenerateConnectionString());
            connector.Formatter = its1Formatter;
            connector.Open();

            // Assign message available event
            connector.MessageAvailable += new EventHandler<UnsolicitedDataEventArgs>(connector_MessageAvailable);

            connector.Start(); // Start watching the directory

            // Exit condition
            Console.Write("Press any key to quit...\n");
            Console.ReadKey();

            // Close connector
            connector.Close();


        }
        static void connector_MessageAvailable(object sender, UnsolicitedDataEventArgs args)
        {
            // Start the receive operation (starts on another thread)
            IAsyncResult iar = (sender as FileListenConnector).BeginReceive(null, null);
            iar.AsyncWaitHandle.WaitOne(); // Wait until complete
            FileReceiveResult result = (sender as FileListenConnector).EndReceive(iar) as FileReceiveResult;

            Console.WriteLine("Received file: {0}", result.FileName);

            if (result.Details.Length > 0)
            {
                Console.WriteLine("The following {0} errors were encountered: ", result.Details.Length.ToString());
                foreach (MARC.Everest.Connectors.ResultDetail r in result.Details)
                {
                    Console.WriteLine(r.Message.ToString());
                }

            }

            Console.WriteLine("Processing complete");

        }

    }


}
