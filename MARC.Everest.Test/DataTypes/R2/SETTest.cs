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
    /// Summary description for SETTest
    /// </summary>
    [TestClass]
    public class SETTest
    {
        public SETTest()
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
        /// Tests R2 serialization of a SET with simple items
        /// </summary>
        [TestMethod]
        public void R2SETSimpleSerializationTest()
        {
            SET<INT> inti = SET<INT>.CreateSET(1, 2, 3, 4);
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><item value=""1""/><item value=""2""/><item value=""3""/><item value=""4""/></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Tests R2 serialization of a SET with items that don't match the generic parameter
        /// </summary>
        [TestMethod]
        public void R2SETTypeOverrideSerializationTest()
        {
            SET<IQuantity> inti = SET<IQuantity>.CreateSET(
                (INT)1, 
                (REAL)2, 
                new PQ(3, "ft"));
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><item xsi:type=""INT"" value=""1""/><item xsi:type=""REAL"" value=""2""/><item xsi:type=""PQ"" value=""3"" unit=""ft""/></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Tests R2 serliazation of a set with nested collections
        /// </summary>
        [TestMethod]
        public void R2SETNestedSerializationTest()
        {
            SET<IColl> inti = SET<IColl>.CreateSET(
                SET<INT>.CreateSET(1, 2, 3),
                LIST<INT>.CreateList(1, 1, 2),
                BAG<ST>.CreateBAG("1", "2")
            );
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><item xsi:type=""DSET_INT""><item value=""1""/><item value=""2""/><item value=""3""/></item><item xsi:type=""LIST_INT""><item value=""1""/><item value=""1""/><item value=""2""/></item><item xsi:type=""BAG_ST""><item value=""1"" language=""en-US""/><item value=""2"" language=""en-US""/></item></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Tests R2 parse of a SET with simple items
        /// </summary>
        [TestMethod]
        public void R2SETSimpleParseTest()
        {
            SET<INT> inti = SET<INT>.CreateSET(1, 2, 3, 4);
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            SET<INT> int2 = R2SerializationHelper.ParseString<SET<INT>>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Tests R2 parse of a SET with items that don't match the generic parameter
        /// </summary>
        [TestMethod]
        public void R2SETTypeOverrideParseTest()
        {
            SET<IQuantity> inti = SET<IQuantity>.CreateSET(
                (INT)1,
                (REAL)2,
                new PQ(3, "ft"));
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            SET<IQuantity> int2 = R2SerializationHelper.ParseString<SET<IQuantity>>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Tests R2 parse of a set with nested collections
        /// </summary>
        [TestMethod]
        public void R2SETNestedParseTest()
        {
            SET<IColl> inti = SET<IColl>.CreateSET(
                SET<INT>.CreateSET(1, 2, 3),
                LIST<INT>.CreateList(1, 1, 2),
                BAG<ST>.CreateBAG("1", "2")
            );
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            SET<IColl> int2 = R2SerializationHelper.ParseString<SET<IColl>>(actualXml);
            Assert.AreEqual(inti, int2);
        }
    }
}
