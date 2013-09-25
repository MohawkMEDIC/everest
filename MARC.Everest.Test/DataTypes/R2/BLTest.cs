/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * User: Justin Fyfe
 * Date: 07-28-2011
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
    /// Test the serialization of BL with DTR2 formatter
    /// </summary>
    [TestClass]
    public class BLTest
    {
        public BLTest()
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
        /// Test to ensure that serialization of a BL 
        /// is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLFalseSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""false""/>";
            String actualXml = R2SerializationHelper.SerializeAsString(new BL(false));
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// true is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLTrueSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""true""/>";
            String actualXml = R2SerializationHelper.SerializeAsString(new BL(true));
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// null flavor and no value is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLNullFlavorNullSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" nullFlavor=""NI""/>";
            String actualXml = R2SerializationHelper.SerializeAsString(new BL() { NullFlavor = NullFlavor.NoInformation });
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// null flavor and true value is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLNullFlavorTrueSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" nullFlavor=""NI""/>";
            String actualXml = R2SerializationHelper.SerializeAsString(new BL(true) { NullFlavor = NullFlavor.NoInformation });
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// flavor of NONNULL and true value is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLFlavorTrueSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""true"" flavorId=""BL.NONNULL""/>";
            String actualXml = R2SerializationHelper.SerializeAsString(new BL(true) { Flavor = "BL.NONNULL" });
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// HXIT data
        /// </summary>
        [TestMethod]
        public void R2BLTrueHXITSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""true"" validTimeLow=""2000"" validTimeHigh=""2001"" controlInformationRoot=""2.3.4"" controlInformationExtension=""1""/>";
            String actualXml = R2SerializationHelper.SerializeAsString(new BL(true) { 
                ValidTimeLow = new TS(new DateTime(2000, 1, 1), DatePrecision.Year),
                ValidTimeHigh = new TS(new DateTime(2001, 1, 1), DatePrecision.Year),
                ControlActRoot = "2.3.4",
                ControlActExt = "1"
            });
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Test to ensure that parse of a BL 
        /// is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLFalseParseTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""false""/>";
            BL parsed = R2SerializationHelper.ParseString<BL>(expectedXml);
            Assert.AreEqual(false, parsed.Value);
        }
        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// true is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLTrueParseTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""true""/>";
            BL parsed = R2SerializationHelper.ParseString<BL>(expectedXml);
            Assert.AreEqual(true, parsed.Value);
        }
        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// null flavor and no value is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLNullFlavorNullParseTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" nullFlavor=""NI""/>";
            BL parsed = R2SerializationHelper.ParseString<BL>(expectedXml);
            Assert.AreEqual(NullFlavor.NoInformation, (NullFlavor)parsed.NullFlavor);

        }
        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// HXIT data
        /// </summary>
        [TestMethod]
        public void R2BLFalseHXITParseTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""false"" validTimeLow=""2000"" validTimeHigh=""2001"" controlInformationRoot=""2.3.4"" controlInformationExtension=""1""/>";
            BL parsed = R2SerializationHelper.ParseString<BL>(expectedXml);
            Assert.AreEqual(false, parsed.Value);
            Assert.AreEqual("2000", parsed.ValidTimeLow.Value);
            Assert.AreEqual("2001", parsed.ValidTimeHigh.Value);
            Assert.AreEqual("2.3.4", parsed.ControlActRoot);
            Assert.AreEqual("1", parsed.ControlActExt);
        }

        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// null flavor and true value is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLNullFlavorTruePraseTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" nullFlavor=""NI""/>";
            BL parsed = R2SerializationHelper.ParseString<BL>(expectedXml);
            Assert.AreEqual(NullFlavor.NoInformation, (NullFlavor)parsed.NullFlavor);
            Assert.AreEqual(null, parsed.Value);
        }
        /// <summary>
        /// Test to ensure that serialization of BL with value
        /// flavor of NONNULL and true value is appropriate
        /// </summary>
        [TestMethod]
        public void R2BLFlavorTrueParseTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""true"" flavorId=""BL.NONNULL""/>";
            BL parsed = R2SerializationHelper.ParseString<BL>(expectedXml);
            Assert.AreEqual("BL.NONNULL", parsed.Flavor);
            Assert.AreEqual(true, parsed.Value);
        }
    }
}
