using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MARC.Everest.Xml;
using System.Xml;

using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.RMIM.UV.NE2008.RCMR_MT000001UV02;
using System.IO;

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for AnatomySelectedEvent
    /// </summary>
    [TestClass]
    public class AnatomySelectedEventTest
    {
        public AnatomySelectedEventTest()
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
        /*
        /// <summary>
        /// Example 80
        /// User has clicked on the anatomy control to locate the primary code.
        /// This manual example is theoretical.
        /// </summary>
        [TestMethod]
        public void anatomySelector01_OnAnatomySelected(object sender, AnatomySelectionEventArgs e)
        {
            
            Stream s = null;
            try
            {
                // Initialize the stream
                s = File.Create("C:/Test Files/mydata.dat");

                // Make a stream writer for easier addition of data to the stream
                StreamWriter sw = new StreamWriter(s);
                sw.WriteLine("This demonstrates writing data before a formatter!");
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

                instance.controlActEvent.Subject.Observation.Value = e.PrimaryCode;
         
                // Setup the formatter
                XmlIts1Formatter fmtr = new XmlIts1Formatter()
                {
                    ValidateConformance = false
                };
            
                // Add graphing aides
                fmtr.GraphAides.Add(new DatatypeFormatter()
                {
                    ValidateConformance = false,
                    CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian
                });

                // Format
                fmtr.Graph(s, instance);

                // Write some data after
                sw.WriteLine("This appears afterwards!");
                sw.Close();

            }
            finally
            {
                if (s != null)
                s.Close();
            } // end try-catch
            
        }*/
    }
}
