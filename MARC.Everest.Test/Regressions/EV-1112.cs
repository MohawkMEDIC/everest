using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using System.Xml;
using MARC.Everest.RMIM.UV.NE2010.RCMR_MT000001UV02;
using System.IO;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.Regressions
{
    /// <summary>
    /// Regression test for EV-1112
    /// </summary>
    [TestClass]
    public class EV_1112
    {
        public EV_1112()
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
        /// Fails parsing date in format 10/2014 instead of 201410
        /// </summary>
        [TestMethod]
        public void ParseInvalidIVLFromCDAMultiProcessor()
        {
            // Load a sample because I don't want to write a full construction method
            ClinicalDocument clinicalDocument = null;
            using (Stream inStream = typeof(EV_1110).Assembly.GetManifestResourceStream("MARC.Everest.Test.Resources.invalidCda.xml"))
            {
                XmlIts1Formatter fmtr = new XmlIts1Formatter();
                fmtr.Settings = SettingsType.DefaultLegacy;
                fmtr.ValidateConformance = false;
                fmtr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
                var result = fmtr.Parse(XmlReader.Create(inStream), typeof(ClinicalDocument));
                Assert.IsTrue(result.Details.Count(o => o.Type == Connectors.ResultDetailType.Error &&
                    o is DatatypeValidationResultDetail &&
                    (o as DatatypeValidationResultDetail).DatatypeName == "TS" &&
                    (o as DatatypeValidationResultDetail).Message == "Data type 'TS' failed basic validation, the violation was : Value 03/2010 is not a valid HL7 date"
                    ) > 0, "Should have an error message");
                clinicalDocument = result.Structure as ClinicalDocument;
            }
        }

        /// <summary>
        /// Fails parsing date in format 10/2014 instead of 201410
        /// </summary>
        [TestMethod]
        public void ParseInvalidIVLFromCDAUniProcessor()
        {
            // Load a sample because I don't want to write a full construction method
            ClinicalDocument clinicalDocument = null;
            using (Stream inStream = typeof(EV_1110).Assembly.GetManifestResourceStream("MARC.Everest.Test.Resources.invalidCda.xml"))
            {
                XmlIts1Formatter fmtr = new XmlIts1Formatter();
                fmtr.Settings = SettingsType.DefaultUniprocessor;
                fmtr.ValidateConformance = false;
                fmtr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
                var result = fmtr.Parse(XmlReader.Create(inStream), typeof(ClinicalDocument));
                Assert.IsTrue(result.Details.Count(o => o.Type == Connectors.ResultDetailType.Error &&
                    o is DatatypeValidationResultDetail &&
                    (o as DatatypeValidationResultDetail).DatatypeName == "TS" &&
                    (o as DatatypeValidationResultDetail).Message == "Data type 'TS' failed basic validation, the violation was : Value 03/2010 is not a valid HL7 date"
                    ) > 0, "Should have an error message");
                clinicalDocument = result.Structure as ClinicalDocument;
            }
        }

    }
}
