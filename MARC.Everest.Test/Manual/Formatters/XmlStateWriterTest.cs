using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.Xml;
using System.Xml;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using System.IO;

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for XmlStateWriter
    /// </summary>
    [TestClass]
    public class XmlStateWriterTest
    {
        public XmlStateWriterTest()
        {
            
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
        /// Example 74
        /// Using the XmlStateWriter.
        /// This test will use the XmlStateWriter's WriteState property
        /// to track the state of the xmlwriter while writing an xml document.
        /// </summary>
        [TestMethod]
        public void XmlStateWriterTest01()
        {

            // Initialize the XmlWriter & State writer
            StringWriter sw = new StringWriter();
            var xw = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true });

            // Write something at the start
            xw.WriteStartElement("myHl7ExampleInstance");
            xw.WriteAttributeString("interactionId", "MCCI_IN000000UV01");

            // State writer should always be used
            XmlStateWriter xsw = new XmlStateWriter(xw);

            // Create a simple instance
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                Guid.NewGuid(),
                DateTime.Now,
                MCCI_IN000000UV01.GetInteractionId(),
                ProcessingID.Production,
                "P",
                AcknowledgementCondition.Always);

            // Setup the formatter
            IXmlStructureFormatter structureFormatter = new XmlIts1Formatter()
            {
                ValidateConformance = false
            };
            structureFormatter.GraphAides.Add(new DatatypeFormatter());


            // Print current path of the xmlwriter before formatting instance
            var writeStatePreFormat = xsw.WriteState;
            Console.WriteLine("Write state pre-format: {0}", writeStatePreFormat);

            // Format
            var result = structureFormatter.Graph(xsw, instance);

            // Print current path of the xmlwriter after formatting instance
            var writeStatePostFormat = xsw.WriteState;
            Console.WriteLine("Write state post-format: {0}", writeStatePostFormat);

            // Test that the write states before and after formatting has changed.
            // This demonstrates how the state of the xml writer can be tracked while runnning.
            Assert.AreNotEqual(writeStatePreFormat, writeStatePostFormat);

            // Flush state writer
            xsw.Flush();

            // finish the stream
            xw.WriteEndElement();
        }



        /// <summary>
        /// Using the XmlStateWriter.
        /// This tests if the start element is valid when empty.
        /// Expecting test to result in FALSE.
        /// </summary>
        [TestMethod]
        public void XmlStateWriterTest02()
        {
            try
            {
                // Initialize the XmlWriter & State writer
                StringWriter sw = new StringWriter();
                var xw = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true });

                // Write something at the start
                xw.WriteStartElement("");
                xw.WriteAttributeString("interactionId", "MCCI_IN000000UV01");

                // State writer should always be used
                XmlStateWriter xsw = new XmlStateWriter(xw);

                // Create a simple instance
                MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                    Guid.NewGuid(),
                    DateTime.Now,
                    MCCI_IN000000UV01.GetInteractionId(),
                    ProcessingID.Production,
                    "P",
                    AcknowledgementCondition.Always);

                // Setup the formatter
                IXmlStructureFormatter structureFormatter = new XmlIts1Formatter()
                {
                    ValidateConformance = false
                };
                structureFormatter.GraphAides.Add(new DatatypeFormatter());


                // Print current path of the xmlwriter before formatting instance
                var writeStatePreFormat = xsw.WriteState;
                Console.WriteLine("Write state pre-format: {0}", writeStatePreFormat);

                // Format
                var result = structureFormatter.Graph(xsw, instance);

                // Print current path of the xmlwriter after formatting instance
                var writeStatePostFormat = xsw.WriteState;
                Console.WriteLine("Write state post-format: {0}", writeStatePostFormat);

                // Test that the write states before and after formatting has changed.
                // This demonstrates how the state of the xml writer can be tracked while runnning.
                Assert.AreNotEqual(writeStatePreFormat, writeStatePostFormat);

                // Flush state writer
                xsw.Flush();

                // finish the stream
                xw.WriteEndElement();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                // Expecting a failed test.
                Assert.IsTrue(e.ToString().Contains("Exception"));

            } // end try-catch
        } // end test method
    }
}
