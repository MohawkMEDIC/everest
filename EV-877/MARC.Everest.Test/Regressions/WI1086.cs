using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MARC.Everest.Xml;
using System.Xml;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.CA.R020402.Interactions;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.Regressions
{
    /// <summary>
    /// Test class that verifies that Bug 1086 - Error Processing xsi:nil is resolved
    /// </summary>
    [TestClass]
    public class WI1086
    {

        private string m_xmlInstance = "<MCCI_IN000002CA xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"urn:hl7-org:v3\" ITSVersion=\"XML_1.0\">" +
                        "<id root=\"17181F7A-A790-4372-AAFB-95A65C6D3926\"/>" +
                        "<creationTime value=\"201111110646-0500\"/>" +
                        "<responseModeCode code=\"I\"/>" +
                        "<versionCode code=\"V3-2008N\"/>" +
                        "<interactionId extension=\"MCCI_IN000002CA\"/>" +
                        "<profileId extension=\"R02.04.02\"/>" +
                        "<processingCode code=\"D\"/>" +
                        "<processingModeCode code=\"T\"/>" +
                        "<acceptAckCode code=\"AL\"/>" +
                        "<receiver xsi:nil=\"true\"/>" +
                        "<sender xsi:nil=\"true\" nullFlavor=\"NAV\"/>" +
                        "<acknowledgement nullFlavor=\"NAV\"/>" +
                        "</MCCI_IN000002CA>";
        public WI1086()
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
        /// Verifies that the bug exists in the XML ITS 1 formatter when processing an instance with XSI:NIL
        /// </summary>
        [WorkItem(1086), TestMethod]
        public void WI1086_VerifyBugLegacyTest()
        {

            // Load the XmlInstance into a string reader
            StringReader sr = new StringReader(this.m_xmlInstance);
            XmlStateReader xr = new XmlStateReader(XmlReader.Create(sr));

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.ValidateConformance = false;
            formatter.Settings = SettingsType.DefaultLegacy;
            formatter.GraphAides.Add(new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian });

            try
            {
                MCCI_IN000002CA instance = formatter.Parse(xr, typeof(MCCI_IN000002CA)).Structure as MCCI_IN000002CA;
                if (instance == null)
                    throw new InvalidOperationException("Invalid test data");

                // Test 1, /receiver should be null (xsi:nil = true)
                Assert.IsNull(instance.Receiver, "Receiver");

                // Test 2, /sender should also be null (xsi:nil = true)
                Assert.IsNull(instance.Sender, "Sender");

                // Test 3, /acknowledgement should not be null and should have a null flavor (a notification should be notified though)
                Assert.IsNotNull(instance.Acknowledgement);
                Assert.AreEqual((NullFlavor)instance.Acknowledgement.NullFlavor.Code, NullFlavor.Unavailable);
            }
            finally
            {
                formatter.Dispose();
            }

        }

        /// <summary>
        /// Verifies that the bug exists in the XML ITS 1 formatter when processing an instance with XSI:NIL
        /// </summary>
        [WorkItem(1086), TestMethod]
        public void WI1086_VerifyBugReflectTest()
        {

            // Load the XmlInstance into a string reader
            StringReader sr = new StringReader(this.m_xmlInstance);
            XmlStateReader xr = new XmlStateReader(XmlReader.Create(sr));

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.ValidateConformance = false;
            formatter.Settings = SettingsType.DefaultUniprocessor;
            formatter.GraphAides.Add(new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian });

            try
            {
                MCCI_IN000002CA instance = formatter.Parse(xr, typeof(MCCI_IN000002CA)).Structure as MCCI_IN000002CA;
                if (instance == null)
                    throw new InvalidOperationException("Invalid test data");

                // Test 1, /receiver should be null (xsi:nil = true)
                Assert.IsNull(instance.Receiver, "Receiver");

                // Test 2, /sender should also be null (xsi:nil = true)
                Assert.IsNull(instance.Sender, "Sender");

                // Test 3, /acknowledgement should not be null and should have a null flavor (a notification should be notified though)
                Assert.IsNotNull(instance.Acknowledgement);
                Assert.AreEqual((NullFlavor)instance.Acknowledgement.NullFlavor.Code, NullFlavor.Unavailable);
            }
            finally
            {
                formatter.Dispose();
            }

        }
    }
}
