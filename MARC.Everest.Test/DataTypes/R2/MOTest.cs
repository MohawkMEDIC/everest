using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.CA.R020402.Interactions;
using System.Xml;
using MARC.Everest.Xml;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.CA.R020402.Vocabulary;
using System.IO;

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Summary description for MOTest
    /// </summary>
    [TestClass]
    public class MOTest
    {
        public MOTest()
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
        /// Basic serialization test
        /// </summary>
        [TestMethod]
        public void R2MOBasicSerializationTest()
        {
            MO inti = new MO((decimal)1.304, "CAD"); 
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" currency=""CAD"" value=""1.304""/>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Basic serialization test with precision
        /// </summary>
        [TestMethod]
        public void R2MOPrecisionSerializationTest()
        {
            MO inti = new MO((decimal)1.30001, "CAD") { Precision = 3 };
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" currency=""CAD"" value=""1.300""/>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Serialization test with expresssion
        /// </summary>
        [TestMethod]
        public void R2MOExpressionSerializationTest()
        {
            MO inti = new MO(0, "CAD")
            {
                NullFlavor = NullFlavor.Derived,
                Expression = new ED(
                    System.Text.Encoding.UTF8.GetBytes("i = (10 + 2) / 2 + 2)"),
                    "application/mathml+xml"
                )
            };
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" currency=""CAD"" nullFlavor=""DER""><expression mediaType=""application/mathml+xml""><data>aSA9ICgxMCArIDIpIC8gMiArIDIp</data></expression></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Serialization test with nullflavor
        /// </summary>
        [TestMethod]
        public void R2MONullFlavorSerializationTest()
        {
            MO inti = new MO(48, "CAD")
            {
                NullFlavor = NullFlavor.Invalid
            };
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" nullFlavor=""INV""/>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Basic serialization test
        /// </summary>
        [TestMethod]
        public void R2MOBasicParseTest()
        {
            MO inti = new MO((decimal)1.304, "hr") { Precision = 3 };
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            MO int2 = R2SerializationHelper.ParseString<MO>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Basic serialization test with precision
        /// </summary>
        [TestMethod]
        public void R2MOPrecisionParseTest()
        {
            MO inti = new MO((decimal)1.300, "CAD") { Precision = 3 };
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            MO int2 = R2SerializationHelper.ParseString<MO>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Serialization test with expresssion
        /// </summary>
        [TestMethod]
        public void R2MOExpressionParseTest()
        {
            MO inti = new MO()
            {
                Currency = "CAD",
                NullFlavor = NullFlavor.Derived,
                Expression = new ED(
                    System.Text.Encoding.UTF8.GetBytes("i = (10 + 2) / 2 + 2)"),
                    "application/mathml+xml"
                )
            };
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            MO int2 = R2SerializationHelper.ParseString<MO>(actualXml);
            Assert.AreEqual(inti, int2);

        }

    }
}
