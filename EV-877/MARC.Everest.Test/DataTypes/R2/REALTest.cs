using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Test the REAL formatting in R2
    /// </summary>
    [TestClass]
    public class REALTest
    {
        public REALTest()
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
        /// Basic serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALBasicSerializationTest()
        {
            REAL inti = 8.24;
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""8.24"" xmlns=""urn:hl7-org:v3""/>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Uncertain Range serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALUncertainRangeSerializationTest()
        {
            REAL inti = 8.24;
            inti.UncertainRange = new IVL<MARC.Everest.DataTypes.Interfaces.IQuantity>(new REAL(3), new REAL(5));
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""8.24"" xmlns=""urn:hl7-org:v3""><uncertainRange><low value=""3"" xsi:type=""REAL""/><high value=""5"" xsi:type=""REAL""/></uncertainRange></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Uncertain Range serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALUncertaintySerializationTest()
        {
            REAL inti = 8.24;
            inti.Uncertainty = new REAL(0.93);
            inti.UncertaintyType = QuantityUncertaintyType.Normal;
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""8.24"" xmlns=""urn:hl7-org:v3"" uncertaintyType=""N""><uncertainty value=""0.93"" xsi:type=""REAL""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Uncertain Range serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALBasicPrecisionSerializationTest()
        {
            REAL inti = 8.243454;
            inti.Precision = 3;
            inti.Uncertainty = new REAL(0.93);
            inti.UncertaintyType = QuantityUncertaintyType.Normal;
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""8.243"" xmlns=""urn:hl7-org:v3"" uncertaintyType=""N""><uncertainty value=""0.93"" xsi:type=""REAL""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Expression serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALExpressionSerializationTest()
        {
            REAL inti = 8.24;
            inti.Uncertainty = new REAL(0.93);
            inti.UncertaintyType = QuantityUncertaintyType.Normal;
            inti.Expression = new ED(
                System.Text.Encoding.UTF8.GetBytes("i = (10 + 2) / 2 + 2)"),
                "application/mathml+xml"
            );
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""8.24"" xmlns=""urn:hl7-org:v3"" uncertaintyType=""N""><expression mediaType=""application/mathml+xml""><data>aSA9ICgxMCArIDIpIC8gMiArIDIp</data></expression><uncertainty value=""0.93"" xsi:type=""REAL""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Uncertain Range serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALNullFlavorSerializationTest()
        {
            REAL inti = 8.24;
            inti.Uncertainty = new REAL(0.93);
            inti.UncertaintyType = QuantityUncertaintyType.Normal;
            inti.NullFlavor = NullFlavor.PositiveInfinity;
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" nullFlavor=""PINF"" xmlns=""urn:hl7-org:v3"" />";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Basic serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALBasicParseTest()
        {
            REAL inti = 8.2358;
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<REAL>(actualXml);
            Assert.AreEqual(inti, int2);
            Assert.AreEqual(4, int2.Precision);
        }

        /// <summary>
        /// Uncertain Range serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALUncertainRangeParseTest()
        {
            REAL inti = 8.24;
            inti.UncertainRange = new IVL<MARC.Everest.DataTypes.Interfaces.IQuantity>(new REAL(3), new REAL(5));
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<REAL>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Uncertain Range serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALUncertaintyParseTest()
        {
            REAL inti = 8.24;
            inti.Uncertainty = new REAL(0.93);
            inti.UncertaintyType = QuantityUncertaintyType.Normal;
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<REAL>(actualXml);
            Assert.AreEqual(inti, int2);

        }

        /// <summary>
        /// Expression serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2REALExpressionParseTest()
        {
            REAL inti = 8.24;
            inti.Uncertainty = new REAL(0.93);
            inti.UncertaintyType = QuantityUncertaintyType.Normal;
            inti.Expression = new ED(
                System.Text.Encoding.UTF8.GetBytes("i = (10 + 2) / 2 + 2)"),
                "application/mathml+xml"
            );
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<REAL>(actualXml);
            Assert.AreEqual(inti, int2);
        }
    }
}
