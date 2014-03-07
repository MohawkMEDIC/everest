/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: fyfej
 * Date: 3-6-2013
 */
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
    /// Summary description for RTOTest
    /// </summary>
    [TestClass]
    public class RTOTest
    {
        public RTOTest()
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
        public void R2RTOBasicSerializationTest()
        {
            RTO<MO, PQ> inti = new RTO<MO, PQ>(
                new MO((decimal)12.30, "CAD") { Precision = 2 },
                new PQ((decimal)1, "hr")
            );
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""urn:hl7-org:v3""><numerator xsi:type=""MO"" value=""12.30"" currency=""CAD""/><denominator xsi:type=""PQ"" value=""1"" unit=""hr""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Uncertain Range serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2RTOUncertainRangeSerializationTest()
        {
            RTO<MO, PQ> inti = new RTO<MO, PQ>();
            inti.UncertainRange = new IVL<MARC.Everest.DataTypes.Interfaces.IQuantity>(
                new RTO<MO, PQ>(
                    new MO((decimal)1.23, "CAD"),
                    new PQ(1, "hr")
                ),
                new RTO<MO, PQ>(
                    new MO((decimal)2.23, "CAD"),
                    new PQ(1, "hr")
                )
            );
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""urn:hl7-org:v3""><uncertainRange><low xsi:type=""RTO""><numerator xsi:type=""MO"" value=""1.23"" currency=""CAD""/><denominator xsi:type=""PQ"" value=""1"" unit=""hr""/></low><high xsi:type=""RTO""><numerator xsi:type=""MO"" value=""2.23"" currency=""CAD""/><denominator xsi:type=""PQ"" value=""1"" unit=""hr""/></high></uncertainRange></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Uncertain Range serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2RTOUncertaintySerializationTest()
        {
            RTO<MO, PQ> inti = new RTO<MO, PQ>(
                new MO((decimal)14.23, "CAD") { Uncertainty = new MO((decimal)0.94, "CAD"), UncertaintyType = QuantityUncertaintyType.Normal },
                new PQ(1, "hr") 
            );
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""urn:hl7-org:v3"" ><numerator xsi:type=""MO"" value=""14.23"" currency=""CAD"" uncertaintyType=""N""><uncertainty value=""0.94"" xsi:type=""MO"" currency=""CAD""/></numerator><denominator value=""1"" unit=""hr"" xsi:type=""PQ""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Expression serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2RTOExpressionSerializationTest()
        {
            RTO<PQ, MO> inti = new RTO<PQ,MO>();
            inti.NullFlavor = NullFlavor.Derived;
            inti.Expression = new ED(
                System.Text.Encoding.UTF8.GetBytes("i = (10 + 2) / 2 + 2)"),
                "application/mathml+xml"
            );
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""urn:hl7-org:v3"" nullFlavor=""DER""><expression mediaType=""application/mathml+xml""><data>aSA9ICgxMCArIDIpIC8gMiArIDIp</data></expression></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Uncertain Range serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2RTONullFlavorSerializationTest()
        {
            RTO<MO, PQ> inti = new RTO<MO, PQ>(
                new MO((decimal)12.30, "CAD"),
                new PQ((decimal)1, "hr")
            );
            inti.NullFlavor = NullFlavor.PositiveInfinity;
            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" nullFlavor=""PINF"" xmlns=""urn:hl7-org:v3"" />";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Basic serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2RTOBasicParseTest()
        {
            RTO<MO, PQ> inti = new RTO<MO, PQ>(
                new MO((decimal)12.30, "CAD") { Precision = 2 },
                new PQ((decimal)1, "hr")
            );
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<RTO<MO, PQ>>(actualXml);
            Assert.AreEqual(inti, int2);
            
            Assert.AreEqual(2, int2.Numerator.Precision);
        }

        /// <summary>
        /// PArse of an invalid RTO without an XSI type on one of the numerator
        /// or denominator
        /// </summary>
        public void R2RTOInvalidParseTest()
        {
            string xml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""urn:hl7-org:v3""><numerator xsi:type=""MO"" value=""12.30"" currency=""CAD""/><denominator value=""1"" unit=""hr""/></test>";
            var ret  = R2SerializationHelper.ParseString<RTO<IQuantity, IQuantity>>(xml);
            Assert.IsNull(ret);
        }

        /// <summary>
        /// Expression serialization of an R2 integer
        /// </summary>
        [TestMethod]
        public void R2RTOExpressionParseTest()
        {
            RTO<IQuantity, IQuantity> inti = new RTO<IQuantity, IQuantity>();
            inti.NullFlavor = NullFlavor.Derived;
            inti.Expression = new ED(
                System.Text.Encoding.UTF8.GetBytes("i = (10 + 2) / 2 + 2)"),
                "application/mathml+xml"
            );
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<RTO<IQuantity, IQuantity>>(actualXml);
            Assert.AreEqual(inti, int2);
        }


        /// <summary>
        /// Uncertain Range parse of an R2 RTO
        /// </summary>
        [TestMethod]
        public void R2RTOUncertainRangeParseTest()
        {
            RTO<IQuantity, IQuantity> inti = new RTO<IQuantity, IQuantity>();
            inti.UncertainRange = new IVL<MARC.Everest.DataTypes.Interfaces.IQuantity>(
                new RTO<PQ, PQ>(
                    new PQ(1, "d")
                    {
                        Translation = new SET<PQR>(
                            new PQR(24, "hr", "UCUM")
                            )
                    },
                    new PQ(2, "d")
                ),
                new RTO<PQ, PQ>(
                    new PQ(3, "d"),
                    new PQ(4, "d")
                )
            );
            //string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""urn:hl7-org:v3""><uncertainRange><low xsi:type=""RTO""><numerator xsi:type=""MO"" value=""1.23"" currency=""CAD""/><denominator xsi:type=""PQ"" value=""1"" unit=""hr""/></low><high xsi:type=""RTO""><numerator xsi:type=""MO"" value=""2.23"" currency=""CAD""/><denominator xsi:type=""PQ"" value=""1"" unit=""hr""/></high></uncertainRange></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<RTO<IQuantity, IQuantity>>(actualXml);
            Assert.AreEqual(inti, int2);
        }
    }
}
