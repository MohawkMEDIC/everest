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

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Summary description for PQTest
    /// </summary>
    [TestClass]
    public class PQTest
    {
        public PQTest()
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
        public void R2PQBasicSerializationTest()
        {
            PQ dist = new PQ((decimal)2.1, "m");
            dist.Translation = new SET<PQR>(
                new PQR((decimal)6.8897, "ft_i", "2.16.840.1.113883.6.8")
            );
            R2SerializationHelper.SerializeAsString(dist);

            PQ inti = new PQ((decimal)1.304, "hr"); 
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" unit=""hr"" value=""1.304""/>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Basic serialization test with precision
        /// </summary>
        [TestMethod]
        public void R2PQPrecisionSerializationTest()
        {
            PQ inti = new PQ((decimal)1.30001, "hr") { Precision = 3 };
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" unit=""hr"" value=""1.300""/>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Serialization test with expresssion
        /// </summary>
        [TestMethod]
        public void R2PQExpressionSerializationTest()
        {
            PQ inti = new PQ(0, "hr")
            {
                NullFlavor = NullFlavor.Derived,
                Expression = new ED(
                    System.Text.Encoding.UTF8.GetBytes("i = (10 + 2) / 2 + 2)"),
                    "application/mathml+xml"
                )
            };
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" unit=""hr"" nullFlavor=""DER""><expression mediaType=""application/mathml+xml""><data>aSA9ICgxMCArIDIpIC8gMiArIDIp</data></expression></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Serialization test with translation
        /// </summary>
        [TestMethod]
        public void R2PQTranslationSerializationTest()
        {
            PQ inti = new PQ(48, "hr")
            {
                Translation = new SET<PQR>(
                    new PQR(2, "d", "UCUM")
                )
            };
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" unit=""hr"" value=""48""><translation value=""2"" code=""d"" codeSystem=""UCUM""/></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Serialization test with nullflavor
        /// </summary>
        [TestMethod]
        public void R2PQNullFlavorSerializationTest()
        {
            PQ inti = new PQ(48, "hr")
            {
                Translation = new SET<PQR>(
                    new PQR(2, "d", "UCUM")
                ),
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
        public void R2PQBasicParseTest()
        {
            PQ inti = new PQ((decimal)1.304, "hr") { Precision = 3 };
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            PQ int2 = R2SerializationHelper.ParseString<PQ>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Basic serialization test with precision
        /// </summary>
        [TestMethod]
        public void R2PQPrecisionParseTest()
        {
            PQ inti = new PQ((decimal)1.300, "hr") { Precision = 3 };
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            PQ int2 = R2SerializationHelper.ParseString<PQ>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Serialization test with expresssion
        /// </summary>
        [TestMethod]
        public void R2PQExpressionParseTest()
        {
            PQ inti = new PQ()
            {
                Unit = "hr",
                NullFlavor = NullFlavor.Derived,
                Expression = new ED(
                    System.Text.Encoding.UTF8.GetBytes("i = (10 + 2) / 2 + 2)"),
                    "application/mathml+xml"
                )
            };
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            PQ int2 = R2SerializationHelper.ParseString<PQ>(actualXml);
            Assert.AreEqual(inti, int2);

        }

        /// <summary>
        /// Serialization test with translation
        /// </summary>
        [TestMethod]
        public void R2PQTranslationParseTest()
        {
            PQ inti = new PQ(48, "hr")
            {
                Translation = new SET<PQR>(
                    new PQR(2, "d", "UCUM")
                )
            };
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            PQ int2 = R2SerializationHelper.ParseString<PQ>(actualXml);
            Assert.AreEqual(inti, int2);

        }

    }
}
