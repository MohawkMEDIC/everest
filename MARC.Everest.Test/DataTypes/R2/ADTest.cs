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
    /// Test the serialization of AD with DT R2
    /// </summary>
    [TestClass]
    public class ADTest
    {
        public ADTest()
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
        /// Basic AD serialization test
        /// </summary>
        [TestMethod]
        public void R2ADBasicSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3""><part value=""1050 Wishard Blvd""/><part type=""DEL""/></test>";
            String actualXml = R2SerializationHelper.SerializeAsString(AD.CreateAD(new ADXP("1050 Wishard Blvd"), new ADXP(null, AddressPartType.Delimiter)));
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Basic AD serialization with part types
        /// </summary>
        [TestMethod]
        public void R2ADBasicPartTypesSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3""><part type=""AL"" value=""1050 Wishard Blvd""/><part type=""AL"" value=""RG 5th Floor""/><part type=""STA"" value=""Ontario""/></test>";
            String actualXml = R2SerializationHelper.SerializeAsString(AD.CreateAD(new ADXP("1050 Wishard Blvd", AddressPartType.AddressLine), new ADXP("RG 5th Floor", AddressPartType.AddressLine), new ADXP("Ontario", AddressPartType.State)));
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Basic AD serialization with a null flavor 
        /// </summary>
        [TestMethod]
        public void R2ADNullFlavorSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" nullFlavor=""NI""/>";
            var adi = AD.CreateAD(new ADXP("1050 Wishard Blvd", AddressPartType.AddressLine), new ADXP("RG 5th Floor", AddressPartType.AddressLine), new ADXP("Ontario", AddressPartType.State));
            adi.NullFlavor = NullFlavor.NoInformation;
            String actualXml = R2SerializationHelper.SerializeAsString(adi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Basic line level scope of address
        /// </summary>
        [TestMethod]
        public void R2ADLineLevelSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" use=""WP DIR""><part type=""SAL"" value=""1050 W Wishard Blvd""/><part type=""ADL"" value=""RG 5th floor""/><part type=""CTY"" value=""Indianapolis""/><part type=""STA"" value=""IN""/><part type=""ZIP"" value=""46240""/></test>";
            var adi = AD.CreateAD(
                SET<PostalAddressUse>.CreateSET(PostalAddressUse.WorkPlace, PostalAddressUse.Direct),
                new ADXP("1050 W Wishard Blvd", AddressPartType.StreetAddressLine),
                new ADXP("RG 5th floor", AddressPartType.AdditionalLocator),
                new ADXP("Indianapolis", AddressPartType.City),
                new ADXP("IN", AddressPartType.State),
                new ADXP("46240", AddressPartType.PostalCode)
            );
            String actualXml = R2SerializationHelper.SerializeAsString(adi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Advanced with ISO 3166
        /// </summary>
        [TestMethod]
        public void R2ADISO3166SerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3""><part type=""STR"" value=""Windsteiner Weg""/><part type=""BNR"" value=""54a""/><part type=""CNT"" code=""DEU"" codeSystem=""1.0.3166.1.2"" value=""D""/></test>";
            var adi = AD.CreateAD(
                new ADXP("Windsteiner Weg", AddressPartType.StreetName),
                new ADXP("54a", AddressPartType.BuildingNumber),
                new ADXP("D", AddressPartType.Country) { CodeSystem = "1.0.3166.1.2", Code = "DEU" }
            );
            String actualXml = R2SerializationHelper.SerializeAsString(adi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Null flavor with use (unknown address)
        /// </summary>
        [TestMethod]
        public void R2ADUnknownAddressSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" use=""WP"" nullFlavor=""UNK""/>";
            var adi = new AD(PostalAddressUse.WorkPlace) { NullFlavor = NullFlavor.Unknown };
            String actualXml = R2SerializationHelper.SerializeAsString(adi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Basic AD serialization test
        /// </summary>
        [TestMethod]
        public void R2ADBasicParseTest()
        {
            var adi = AD.CreateAD(new ADXP("1050 Wishard Blvd"), new ADXP(null, AddressPartType.Delimiter));
            String actualXml = R2SerializationHelper.SerializeAsString(adi);
            var ad2 = R2SerializationHelper.ParseString<AD>(actualXml);
            Assert.AreEqual(adi, ad2);
        }
        /// <summary>
        /// Basic AD serialization with part types
        /// </summary>
        [TestMethod]
        public void R2ADBasicPartTypesParseTest()
        {
            var adi = AD.CreateAD(new ADXP("1050 Wishard Blvd", AddressPartType.AddressLine), new ADXP("RG 5th Floor", AddressPartType.AddressLine), new ADXP("Ontario", AddressPartType.State));
            String actualXml = R2SerializationHelper.SerializeAsString(adi);
            var ad2 = R2SerializationHelper.ParseString<AD>(actualXml);
            Assert.AreEqual(adi, ad2);
        }

        /// <summary>
        /// Basic line level scope of address
        /// </summary>
        [TestMethod]
        public void R2ADLineLevelParseTest()
        {
            var adi = AD.CreateAD(
                SET<PostalAddressUse>.CreateSET(PostalAddressUse.WorkPlace, PostalAddressUse.Direct),
                new ADXP("1050 W Wishard Blvd", AddressPartType.StreetAddressLine),
                new ADXP("RG 5th floor", AddressPartType.AdditionalLocator),
                new ADXP("Indianapolis", AddressPartType.City),
                new ADXP("IN", AddressPartType.State),
                new ADXP("46240", AddressPartType.PostalCode)
            );
            String actualXml = R2SerializationHelper.SerializeAsString(adi);
            var ad2 = R2SerializationHelper.ParseString<AD>(actualXml);
            Assert.AreEqual(adi, ad2);
        }

        /// <summary>
        /// Advanced with ISO 3166
        /// </summary>
        [TestMethod]
        public void R2ADISO3166ParseTest()
        {
            var adi = AD.CreateAD(
                SET<PostalAddressUse>.CreateSET(PostalAddressUse.WorkPlace, PostalAddressUse.Direct),
                new ADXP("Windsteiner Weg", AddressPartType.StreetName),
                new ADXP("54a", AddressPartType.BuildingNumber),
                new ADXP("D", AddressPartType.Country) { CodeSystem = "1.0.3166.1.2", Code = "DEU" }
            );
            String actualXml = R2SerializationHelper.SerializeAsString(adi);
            var ad2 = R2SerializationHelper.ParseString<AD>(actualXml);
            Assert.AreEqual(adi, ad2);
        }

        /// <summary>
        /// Null flavor with use (unknown address)
        /// </summary>
        [TestMethod]
        public void R2ADUnknownAddressParseTest()
        {
            var adi = new AD(PostalAddressUse.WorkPlace) { NullFlavor = NullFlavor.NoInformation };
            String actualXml = R2SerializationHelper.SerializeAsString(adi);
            var ad2 = R2SerializationHelper.ParseString<AD>(actualXml);
            Assert.AreEqual(adi, ad2);
        }
    }
}
