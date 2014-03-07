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
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using MARC.Everest.DataTypes;
using System.Collections.Generic;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Summary description for PQRTest
    /// </summary>
    [TestClass]
    public class PQRTest
    {
        public PQRTest()
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
        /// Serialization test for PQR.
        /// Only Code and CodeSystem are not nullified.
        /// </summary>
        [TestMethod]
        public void PQRSerializationTest01()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = null;
            pqr.ValidTimeHigh = null;
            pqr.ControlActRoot = null;
            pqr.ControlActExt = null;
            pqr.NullFlavor = null;
            pqr.UpdateMode = null;
            pqr.CodeSystemName = null;
            pqr.CodeSystemVersion = null;
            pqr.ValueSet = null;
            pqr.ValueSetVersion = null;
            pqr.DisplayName = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""3"" code=""[ft_i]"" codeSystem=""2.16.840.1.113883.6.8""></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for PQR.
        /// NullFlavor is set to unknown therefore instance is invalid.
        /// </summary>
        [TestMethod]
        public void PQRSerializationTest02()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = null;
            pqr.ValidTimeHigh = null;
            pqr.ControlActRoot = null;
            pqr.ControlActExt = null;
            pqr.NullFlavor = NullFlavor.Unknown;
            pqr.UpdateMode = null;
            pqr.CodeSystemName = null;
            pqr.CodeSystemVersion = null;
            pqr.ValueSet = null;
            pqr.ValueSetVersion = null;
            pqr.DisplayName = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" nullFlavor=""UNK""></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for PQR.
        /// Testing serialization with non-existant code and codesystem
        /// (set but not real).
        /// </summary>
        [TestMethod]
        public void PQRSerializationTest03()
        {
            PQR pqr = new PQR(3, "fakeCode", "fakeCodeSystem");
            pqr.NullFlavor = null;
            pqr.ValidTimeHigh = null;
            pqr.ValidTimeLow = null;
            pqr.UpdateMode = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""3"" code=""fakeCode"" codeSystem=""fakeCodeSystem""></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRSerializationTest04()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.ControlActRoot = null;
            pqr.ControlActExt = null;
            pqr.NullFlavor = null;
            pqr.UpdateMode = null;
            pqr.CodeSystemName = null;
            pqr.CodeSystemVersion = null;
            pqr.ValueSet = null;
            pqr.ValueSetVersion = null;
            pqr.DisplayName = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""3"" code=""[ft_i]"" codeSystem=""2.16.840.1.113883.6.8"" validTimeHigh=""20121231"" validTimeLow=""20120101""></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// UpdateMode
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRSerializationTest05()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.NullFlavor = null;
            pqr.UpdateMode = UpdateMode.Add;
            pqr.CodeSystemName = null;
            pqr.CodeSystemVersion = null;
            pqr.ValueSet = null;
            pqr.ValueSetVersion = null;
            pqr.DisplayName = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""3"" code=""[ft_i]"" codeSystem=""2.16.840.1.113883.6.8"" validTimeHigh=""20121231"" validTimeLow=""20120101"" updateMode=""A""></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// CodeSystemName
        /// CodeSystemVersion
        /// ValueSet
        /// ValueSetVersion
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRSerializationTest06()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.NullFlavor = null;
            pqr.UpdateMode = UpdateMode.Add;
            pqr.CodeSystemName = "Fake Name";
            pqr.CodeSystemVersion = "1.0";
            pqr.ValueSet = null;
            pqr.ValueSetVersion = null;
            pqr.DisplayName = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""3"" code=""[ft_i]"" codeSystem=""2.16.840.1.113883.6.8"" validTimeHigh=""20121231"" validTimeLow=""20120101"" updateMode=""A"" codeSystemName=""Fake Name"" codeSystemVersion=""1.0""></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// CodeSystemName
        /// CodeSystemVersion
        /// ValueSet
        /// ValueSetVersion
        /// DisplayName
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRSerializationTest07()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.NullFlavor = null;
            pqr.UpdateMode = UpdateMode.Add;
            pqr.CodeSystemName = "Fake Name";
            pqr.CodeSystemVersion = "1.0";
            pqr.ValueSet = "Fake VS";
            pqr.ValueSetVersion = "1.0";
            pqr.DisplayName = "Fake DN";
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""3"" code=""[ft_i]"" codeSystem=""2.16.840.1.113883.6.8"" validTimeHigh=""20121231"" validTimeLow=""20120101"" updateMode=""A"" codeSystemName=""Fake Name"" codeSystemVersion=""1.0"" valueSet=""Fake VS"" valueSetVersion=""1.0""><displayName value=""Fake DN"" language=""en-US"" /></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// CodeSystemName
        /// CodeSystemVersion
        /// ValueSet
        /// ValueSetVersion
        /// DisplayName
        /// OriginalText
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRSerializationTest08()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.NullFlavor = null;
            pqr.UpdateMode = UpdateMode.Add;
            pqr.CodeSystemName = "Fake Name";
            pqr.CodeSystemVersion = "1.0";
            pqr.ValueSet = "Fake VS";
            pqr.ValueSetVersion = "1.0";
            pqr.DisplayName = "Fake DN";
            pqr.OriginalText = "3 Feet";

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""3"" code=""[ft_i]"" codeSystem=""2.16.840.1.113883.6.8"" validTimeHigh=""20121231"" validTimeLow=""20120101"" updateMode=""A"" codeSystemName=""Fake Name"" codeSystemVersion=""1.0"" valueSet=""Fake VS"" valueSetVersion=""1.0""><displayName value=""Fake DN"" language=""en-US"" /><originalText value=""3 Feet"" language=""en-US"" /></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        // PQR PARSE TESTS //


        /// <summary>
        /// Parse test for PQR.
        /// </summary>
        [TestMethod]
        public void PQRParseTest01()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = null;
            pqr.ValidTimeHigh = null;
            pqr.ControlActRoot = null;
            pqr.ControlActExt = null;
            pqr.NullFlavor = null;
            pqr.UpdateMode = null;
            pqr.CodeSystemName = null;
            pqr.CodeSystemVersion = null;
            pqr.ValueSet = null;
            pqr.ValueSetVersion = null;
            pqr.DisplayName = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            var pqr2 = R2SerializationHelper.ParseString<PQR>(actualXml);
            Assert.AreEqual(pqr, pqr2);
        }


        /// <summary>
        /// Parse test for PQR.
        /// Testing if PQR can be serialized and parsed properly when
        /// Code and Nullflavor are both not null.
        /// </summary>
        [TestMethod]
        public void PQRParseTest02()
        {
            //PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            //pqr.ValidTimeLow = null;
            //pqr.ValidTimeHigh = null;
            //pqr.ControlActRoot = null;
            //pqr.ControlActExt = null;
            //pqr.NullFlavor = NullFlavor.Unknown;
            //pqr.UpdateMode = null;
            //pqr.CodeSystemName = null;
            //pqr.CodeSystemVersion = null;
            //pqr.ValueSet = null;
            //pqr.ValueSetVersion = null;
            //pqr.DisplayName = null;
            //pqr.OriginalText = null;

            //Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            ////Assert.IsTrue(pqr.Validate());

            //// serialize and parse pivl
            //var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            //var pqr2 = R2SerializationHelper.ParseString<PQR>(actualXml);
            //Assert.AreEqual(pqr, pqr2);
        }


        /// <summary>
        /// Parse test for PQR.
        /// Testing if PQR can be serialized and parsed properly when
        /// the code and codesystem non-existant.
        /// </summary>
        [TestMethod]
        public void PQRParseTest03()
        {
            PQR pqr = new PQR(3, "fakeCode", "fakeCodeSystem");
            pqr.NullFlavor = null;
            pqr.ValidTimeHigh = null;
            pqr.ValidTimeLow = null;
            pqr.UpdateMode = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            var pqr2 = R2SerializationHelper.ParseString<PQR>(actualXml);
            Assert.AreEqual(pqr, pqr2);
        }



        /// <summary>
        /// Parse test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRParseTest04()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.ControlActRoot = null;
            pqr.ControlActExt = null;
            pqr.NullFlavor = null;
            pqr.UpdateMode = null;
            pqr.CodeSystemName = null;
            pqr.CodeSystemVersion = null;
            pqr.ValueSet = null;
            pqr.ValueSetVersion = null;
            pqr.DisplayName = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            var pqr2 = R2SerializationHelper.ParseString<PQR>(actualXml);
            Assert.AreEqual(pqr, pqr2);
        }



        /// <summary>
        /// Parse test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// UpdateMode
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRParseTest05()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.NullFlavor = null;
            pqr.UpdateMode = UpdateMode.Add;
            pqr.CodeSystemName = null;
            pqr.CodeSystemVersion = null;
            pqr.ValueSet = null;
            pqr.ValueSetVersion = null;
            pqr.DisplayName = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            var pqr2 = R2SerializationHelper.ParseString<PQR>(actualXml);
            Assert.AreEqual(pqr, pqr2);
        }


        /// <summary>
        /// Parse test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// CodeSystemName
        /// CodeSystemVersion
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRParseTest06()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.NullFlavor = null;
            pqr.UpdateMode = UpdateMode.Add;
            pqr.CodeSystemName = "Fake Name";
            pqr.CodeSystemVersion = "1.0";
            pqr.ValueSet = null;
            pqr.ValueSetVersion = null;
            pqr.DisplayName = null;
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            var pqr2 = R2SerializationHelper.ParseString<PQR>(actualXml);
            Assert.AreEqual(pqr, pqr2);
        }



        /// <summary>
        /// Parse test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// CodeSystemName
        /// CodeSystemVersion
        /// DisplayName
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRParseTest07()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.NullFlavor = null;
            pqr.UpdateMode = UpdateMode.Add;
            pqr.CodeSystemName = "Fake Name";
            pqr.CodeSystemVersion = "1.0";
            pqr.ValueSet = "Fake VS";
            pqr.ValueSetVersion = "1.0";
            pqr.DisplayName = "Fake DN";
            pqr.OriginalText = null;

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            var pqr2 = R2SerializationHelper.ParseString<PQR>(actualXml);
            Assert.AreEqual(pqr, pqr2);
        }



        /// <summary>
        /// Serialization test for PQR.
        /// Test should return true when
        /// the following are not nullified:
        /// Code
        /// CodeSystem
        /// ValidTimeLow
        /// ValidTimeHigh
        /// CodeSystemName
        /// CodeSystemVersion
        /// ValueSet
        /// ValueSetVersion
        /// DisplayName
        /// OriginalText
        /// And the rest of the properties are Nullified.
        /// </summary>
        [TestMethod]
        public void PQRParseTest08()
        {
            PQR pqr = new PQR(3, "[ft_i]", "2.16.840.1.113883.6.8");
            pqr.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            pqr.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            pqr.NullFlavor = null;
            pqr.UpdateMode = UpdateMode.Add;
            pqr.CodeSystemName = "Fake Name";
            pqr.CodeSystemVersion = "1.0";
            pqr.ValueSet = "Fake VS";
            pqr.ValueSetVersion = "1.0";
            pqr.DisplayName = "Fake DN";
            pqr.OriginalText = "3 Feet";

            Console.WriteLine("Value: {0} | Code: {1} | Codesystem: {2}", pqr.Value, pqr.Code, pqr.CodeSystem);
            Assert.IsTrue(pqr.Validate());

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pqr);
            var pqr2 = R2SerializationHelper.ParseString<PQR>(actualXml);
            Assert.AreEqual(pqr, pqr2);
        }
    }
}
