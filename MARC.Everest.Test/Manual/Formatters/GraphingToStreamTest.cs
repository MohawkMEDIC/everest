using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Test.Manual.Interfaces;

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for GraphingToStream01
    /// </summary>
    [TestClass]
    public class GraphingToStreamTest
    {
        public GraphingToStreamTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        
        /// <summary>
        /// ** THIS TEST IS COMMENTED OUT BECAUSE IT WRITES TO A LOCAL FILE (located on C: drive). **
        /// ** Using an XmlStateWriter instead would defeat the purpose of the test. **
        /// 
        /// Graphing to a stream that already contains data.
        /// This test will
        ///     1. Write a string to the stream.
        ///     2. Graph an instance to the stream that already contains data.
        ///     3. Write another string to the stream.
        ///     4. Write all data to a file.
        ///     5. Open and read the file.
        ///     6. Validate that the file contains the strings written
        ///        before and after the instance xml.
        /// 
        /// </summary>
        [TestMethod]
        public void GraphToStreamTest01()
        {
            /*
            Stream s = null;
            try
            {
                // Initialize the stream
                s = File.Create("C:/Test Files/mydata10.dat");

                // Make a stream writer for easier addition of data to the stream
                StreamWriter sw = new StreamWriter(s);

                // Initialize sample strings thata re to be injected into the streamwriter.
                String preString = "This demonstrates writing data before a formatter!";
                String postString = "This appears afterwards!";

                sw.WriteLine(preString);
                sw.Flush();

                // Create a simple instance
                MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                    Guid.NewGuid(),
                    DateTime.Now,
                    MCCI_IN000000UV01.GetInteractionId(),
                    ProcessingID.Production,
                    "P",
                    AcknowledgementCondition.Always
                    );

                // Setup the formatter
                IStructureFormatter structureFormatter = new XmlIts1Formatter()
                {
                    ValidateConformance = false
                };

                // Add graphing aides
                structureFormatter.GraphAides.Add(new DatatypeFormatter());

                // Format and write to console
                structureFormatter.Graph(s, instance);

                // Write some data after
                sw.WriteLine(postString);

                // Close streamwriter
                sw.Close();

                // Validate that the written file contains the two injected strings.
                StreamReader sr = new StreamReader("C:/Test Files/mydata10.dat");

                String xmlFromFile = "";
                // write everything in the stream to the console
                while (!sr.EndOfStream)
                {
                    // store lines into a string
                    xmlFromFile += sr.ReadLine();    
                }

                // Write xml from the file onto the console
                Console.WriteLine(xmlFromFile);

                try
                {
                    // Test that the strings injected into the stream
                    // are contained in the generated file
                    Assert.IsTrue(xmlFromFile.Contains(preString) && xmlFromFile.Contains(postString));

                    Console.WriteLine("The files contains the injected strings.");
                } catch {
                    Console.WriteLine("Error: The file does not contain the injected strings.");
                }
            }
            finally
            {
                if (s != null)
                    s.Close();
            } // end try-finally
           */
        } // end test method
    }
}
