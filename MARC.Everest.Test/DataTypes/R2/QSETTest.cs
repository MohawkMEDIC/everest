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
using System.Reflection;
using System.IO;

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Tests QSETs
    /// </summary>
    [TestClass]
    public class QSETTest
    {
        public QSETTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static string[] GetResourceList()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceNames();
        }

        public static Stream GetResourceStream(string scriptname)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(scriptname);
        }

        public static string findResource(string targetResource)
        {
            // Find the resource to be parsed.
            string neededResource = "";
            foreach (string name in GetResourceList())
            {
                if (name.ToString().Contains(targetResource))
                {
                    neededResource = name;
                }
            }
            return neededResource;
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
        
        
        //////////// QSI SERIALIZATION TESTS ////////////
        //////////// QSI SERIALIZATION TESTS ////////////
        //////////// QSI SERIALIZATION TESTS ////////////

        /// <summary>
        /// Serialization test for a QSI with mixed R1 and R2 SXPR/SXCM/ETC... TERMS
        /// </summary>
        [TestMethod]
        public void R2QSIMixedTermsSerializationTest()
        {
            QSI<INT> set = QSI<INT>.CreateQSI(
                new IVL<INT>(1, 3),
                new IVL<INT>(2, 7),
                SXPR<INT>.CreateSXPR(
                    new IVL<INT>(4, 10) { Operator = SetOperator.Inclusive },
                    new IVL<INT>(5, 30) { Operator = SetOperator.Inclusive },
                    new IVL<INT>(1, 300) { Operator = SetOperator.Intersect }
                )
            );
            set = set.Normalize() as QSI<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><term xsi:type=""IVL_INT""><low value=""1""/><high value=""3""/></term><term xsi:type=""IVL_INT""><low value=""2""/><high value=""7""/></term><term xsi:type=""QSI_INT""><term xsi:type=""QSU_INT""><term xsi:type=""IVL_INT""><low value=""4""/><high value=""10""/></term><term xsi:type=""IVL_INT""><low value=""5""/><high value=""30""/></term></term><term xsi:type=""IVL_INT""><low value=""1""/><high value=""300""/></term></term></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for a QSI.
        /// /// Properties specified:
        ///     ValidTimeLow:       null
        ///     ValidTimeHigh:      null
        ///     NullFlavor:         null
        ///     UpdateMode:         null
        ///     OriginalText:       null
        /// </summary>
        [TestMethod]
        public void R2QSISerializationTest()
        {
            QSI<INT> set = QSI<INT>.CreateQSI(
                new IVL<INT>(1, 3),
                new IVL<INT>(2, 7)
            );

            // Set properties
            set.ValidTimeLow = null;
            set.ValidTimeHigh = null;
            set.NullFlavor = null;
            set.UpdateMode = null;
            set.OriginalText = null;

            set = set.Normalize() as QSI<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><term xsi:type=""IVL_INT""><low value=""1""/><high value=""3""/></term><term xsi:type=""IVL_INT""><low value=""2""/><high value=""7""/></term></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for a QSI.
        /// Properties specified:
        /// ValidTimeLow:       January 1, 2008
        /// ValidTimeHigh:      January 31, 2008
        /// NullFlavor:         null
        /// UpdateMode:         null
        /// OriginalText:       null
        /// </summary>
        [TestMethod]
        public void R2QSISerializationTest02()
        {
            QSI<INT> set = QSI<INT>.CreateQSI(
                new IVL<INT>(1, 3),
                new IVL<INT>(2, 7)
            );

            // Set properties
            set.ValidTimeLow = new TS(new DateTime(2008, 01, 01), DatePrecision.Day);
            set.ValidTimeHigh = new TS(new DateTime(2008, 01, 31), DatePrecision.Day);
            set.NullFlavor = null;
            set.UpdateMode = UpdateMode.Add;
            set.OriginalText = "Test";
            set.OriginalText.Language = "en-US";
            set = set.Normalize() as QSI<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" validTimeLow=""20080101"" validTimeHigh=""20080131"" updateMode=""A""><originalText language=""en-US"" value=""Test"" /><term xsi:type=""IVL_INT""><low value=""1""/><high value=""3""/></term><term xsi:type=""IVL_INT""><low value=""2""/><high value=""7""/></term></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        //////////// QSI PARSE TESTS ////////////
        //////////// QSI PARSE TESTS ////////////
        //////////// QSI PARSE TESTS ////////////


        /// <summary>
        /// </summary>
        [TestMethod]
        public void R2QSIMixedTermsParseTest()
        {
            /*
            QSI<INT> set = new QSI<INT>(
                new IVL<INT>(1, 3),
                new IVL<INT>(2, 7),
                new SXPR<INT>(
                    new IVL<INT>(4, 10) { Operator = SetOperator.Inclusive },
                    new IVL<INT>(5, 30) { Operator = SetOperator.Inclusive },
                    new IVL<INT>(1, 300) { Operator = SetOperator.Intersect }
                )
            );

            // normalize the set expression
            set = set.Normalize() as QSI<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSI<INT>>(actualXml);
            Assert.AreEqual(set, set2);
            */
        }



        /// <summary>
        /// Parse test for a QSI.
        /// /// Properties specified:
        ///     ValidTimeLow:       null
        ///     ValidTimeHigh:      null
        ///     NullFlavor:         null
        ///     UpdateMode:         null
        ///     OriginalText:       null
        /// </summary>
        [TestMethod]
        public void R2QSIParseTest()
        {
            QSI<INT> set = QSI<INT>.CreateQSI(
                new IVL<INT>(1, 3),
                new IVL<INT>(2, 7)
            );

            // Set properties
            set.ValidTimeLow = null;
            set.ValidTimeHigh = null;
            set.NullFlavor = null;
            set.UpdateMode = null;
            set.OriginalText = null;

            // normalize the set expression
            set = set.Normalize() as QSI<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSI<INT>>(actualXml);
            Assert.AreEqual(set, set2);
        }


        /// <summary>
        /// Serialization test for a QSI.
        /// /// Properties specified:
        ///     ValidTimeLow:       null
        ///     ValidTimeHigh:      null
        ///     NullFlavor:         null
        ///     UpdateMode:         null
        ///     OriginalText:       null
        /// </summary>
        [TestMethod]
        public void R2QSIParseTest02()
        {
            QSI<INT> set = QSI<INT>.CreateQSI(
                new IVL<INT>(1, 3),
                new IVL<INT>(2, 7)
            );

            // Set properties
            set.ValidTimeLow = null;
            set.ValidTimeHigh = null;
            set.NullFlavor = null;
            set.UpdateMode = null;
            set.OriginalText = null;

            // normalize the set expression
            set = set.Normalize() as QSI<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSI<INT>>(actualXml);
            Assert.AreEqual(set, set2);
        }


        //////////// QSS SERIALIZATION TESTS ////////////
        //////////// QSS SERIALIZATION TESTS ////////////
        //////////// QSS SERIALIZATION TESTS ////////////


        /// <summary>
        /// Serialization test for a QSS of Physical Quantities.
        /// No precision set.
        /// </summary>
        [TestMethod]
        public void R2QSS_REAL_SerializationTest()
        {
            QSS<REAL> set = QSS<REAL>.CreateQSS(
                new REAL(100),
                new REAL(200),
                new REAL(10)
            );

            set = set.Normalize() as QSS<REAL>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><term value=""100""/><term value=""200""/><term value=""10""/></test>";
                             
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for a QSS of Physical Quantities.
        /// Precision set to 2.
        /// </summary>
        [TestMethod]
        public void R2QSS_REAL_SerializationTest02()
        {
            QSS<REAL> set = QSS<REAL>.CreateQSS(
                new REAL(100) { Precision = 2 },
                new REAL(200) { Precision = 2 },
                new REAL(10) { Precision = 2 }
            );

            set = set.Normalize() as QSS<REAL>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><term value=""100.00""/><term value=""200.00""/><term value=""10.00""/></test>";

            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for a QSS of Timestamps.
        /// UpdateMode property is nullified.
        /// </summary>
        [TestMethod]
        public void R2QSS_TS_SerializationTest()
        {
            QSS<TS> set = QSS<TS>.CreateQSS(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2010, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2012, 01, 01), DatePrecision.Month)
                );
            set.NullFlavor = null;
            set.ValidTimeLow = new TS() { NullFlavor = NullFlavor.Unknown };
            set.ValidTimeHigh = new TS() { NullFlavor = NullFlavor.Unknown };
            set.UpdateMode = null;

            // Should result in a Timestamp Interval containing all dates in
            // January 2008, January 2010, and January 2012
            set = set.Normalize() as QSS<TS>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><term value=""200801""/><term value=""201001""/><term value=""201201""/></test>";

            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for a QSS of Timestamps.
        /// UpdateMode property is set (not nullified).
        /// </summary>
        [TestMethod]
        public void R2QSS_TS_SerializationTest02()
        {
            QSS<TS> set = QSS<TS>.CreateQSS(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2010, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2012, 01, 01), DatePrecision.Month)
            );
            set.NullFlavor = null;
            set.ValidTimeLow = new TS() { NullFlavor = NullFlavor.Unknown };
            set.ValidTimeHigh = new TS() { NullFlavor = NullFlavor.Unknown };

            // The QSS does not want to be serialized
            // when an update mode is assigned.
            set.UpdateMode = UpdateMode.Remove;

            // Should result in a Timestamp Interval containing all dates in
            // January 2008, January 2010, and January 2012
            set = set.Normalize() as QSS<TS>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" updateMode=""D""><term value=""200801""/><term value=""201001""/><term value=""201201""/></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        //////////// QSS PARSE TESTS ////////////
        //////////// QSS PARSE TESTS ////////////
        //////////// QSS PARSE TESTS ////////////

        /// <summary>
        /// Serialization test for a QSS of Physical Quantities.
        /// No precision set.
        /// </summary>
        [TestMethod]
        public void R2QSS_REAL_ParseTest()
        {
            QSS<REAL> set = QSS<REAL>.CreateQSS(
                new REAL(100),
                new REAL(200),
                new REAL(10)
            );

            set = set.Normalize() as QSS<REAL>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSS<REAL>>(actualXml);
            Assert.AreEqual(set,set2);
        }


        /// <summary>
        /// Serialization test for a QSS of Physical Quantities.
        /// Precision set to 2.
        /// </summary>
        [TestMethod]
        public void R2QSS_REAL_ParseTest02()
        {
            QSS<REAL> set = QSS<REAL>.CreateQSS(
                new REAL(100) { Precision = 2 },
                new REAL(200) { Precision = 2 },
                new REAL(10) { Precision = 2 }
            );

            set = set.Normalize() as QSS<REAL>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSS<REAL>>(actualXml);
            Assert.AreEqual(set, set2);
        }

        /// <summary>
        /// Serialization test for a QSS of Timestamps.
        /// UpdateMode property is nullified.
        /// </summary>
        [TestMethod]
        public void R2QSS_TS_ParseTest()
        {
            // create timestamp QSS
            QSS<TS> set = QSS<TS>.CreateQSS(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2010, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2012, 01, 01), DatePrecision.Month)
            );
            set.NullFlavor = null;
            set.ValidTimeLow = null;
            set.ValidTimeHigh = null;
            set.UpdateMode = null;

            set = set.Normalize() as QSS<TS>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSS<TS>>(actualXml);
            Assert.AreEqual(set, set2);
        }


        /// <summary>
        /// Serialization test for a QSS of Timestamps.
        /// UpdateMode property is populated.
        /// </summary>
        [TestMethod]
        public void R2QSS_TS_ParseTest02()
        {
            // create timestamp QSS
            QSS<TS> set = QSS<TS>.CreateQSS(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2010, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2012, 01, 01), DatePrecision.Month)
            );
            set.NullFlavor = null;
            set.ValidTimeLow = null;
            set.ValidTimeHigh = null;
            set.UpdateMode = UpdateMode.Add;
            

            set = set.Normalize() as QSS<TS>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSS<TS>>(actualXml);
            Assert.AreEqual(set, set2);
        }


        //////////// QSD SERIALIZATION TEST ////////////
        //////////// QSD SERIALIZATION TEST ////////////
        //////////// QSD SERIALIZATION TEST ////////////

        /// <summary>
        /// Serialization test for a QSD set of integers.
        /// Proeprties are all default.
        /// </summary>
        [TestMethod]
        public void R2QSD_INT_SerializationTest()
        {
            // create differentiating continuous set expression
            QSD<INT>  set = new QSD<INT>(
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                    new IVL<INT>(5, 8)
                    {
                        LowClosed = true,
                        HighClosed = true
                    }
            );

            // normalize the set expression
            set = set.Normalize() as QSD<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><minuend xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""10""/></minuend><subtrahend xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""5""/><high value=""8""/></subtrahend></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for a QSD set of integers.
        /// Nullflavor is populated.
        /// </summary>
        [TestMethod]
        public void R2QSD_INT_SerializationTest02()
        {
            // create differentiating continuous set expression
            QSD<INT> set = new QSD<INT>(
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                    new IVL<INT>(5, 8)
                    {
                        LowClosed = true,
                        HighClosed = true
                    }
            );

            // nullifiy the QSD instance
            set.NullFlavor = NullFlavor.NoInformation;

            // normalize the set expression
            set = set.Normalize() as QSD<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><minuend xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""10""/></minuend><subtrahend xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""5""/><high value=""8""/></subtrahend></test>";

            Assert.IsFalse(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);

            // Assert:  make sure that an exception is thrown
            try
            {
                R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.IsTrue(e.ToString().Contains("Exception"));
            }
        }



        /// <summary>
        /// Serialization test for a QSD set of integers.
        /// Properties specified:
        ///     ValidTimeLow:       January 1, 2008 at 00:00:00am
        ///     ValidTimeHigh:      January 1, 2008 at 11:59:59pm
        ///     NullFlavor:         null
        ///     UpdateMode:         null
        ///     OriginalText:       null
        /// </summary>
        [TestMethod]
        public void R2QSD_INT_SerializationTest03()
        {
            // create differentiating continuous set expression
            QSD<INT> set = new QSD<INT>(
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                    new IVL<INT>(5, 8)
                    {
                        LowClosed = true,
                        HighClosed = true
                    }
            );

            // Set Valid Contact Interval
            set.ValidTimeLow = new TS(new DateTime(2008, 01, 01, 00, 00, 00), DatePrecision.Second);
            set.ValidTimeHigh = new TS(new DateTime(2008, 01, 31, 23, 59, 59), DatePrecision.Second);

            set.NullFlavor = null;
            set.UpdateMode = null;
            set.OriginalText = null;


            // normalize the set expression
            set = set.Normalize() as QSD<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" validTimeLow=""20080101000000-0500"" validTimeHigh=""20080131235959-0500""><minuend xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""10""/></minuend><subtrahend xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""5""/><high value=""8""/></subtrahend></test>";

            //Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for a QSD set of integers.
        /// Properties specified:
        ///     ValidTimeLow:       January 1, 2008 at 00:00:00am
        ///     ValidTimeHigh:      January 1, 2008 at 11:59:59pm
        ///     NullFlavor:         null
        ///     UpdateMode:         UpdateMode.Add
        ///     OriginalText:       "Test"
        /// </summary>
        [TestMethod]
        public void R2QSD_INT_SerializationTest04()
        {
            // create differentiating continuous set expression
            QSD<INT> set = new QSD<INT>(
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                    new IVL<INT>(5, 8)
                    {
                        LowClosed = true,
                        HighClosed = true
                    }
            );

            // Set Valid Contact Interval
            set.ValidTimeLow = new TS(new DateTime(2008, 01, 01, 00, 00, 00), DatePrecision.Second);
            set.ValidTimeHigh = new TS(new DateTime(2008, 01, 31, 23, 59, 59), DatePrecision.Second);

            set.NullFlavor = null;
            set.UpdateMode = UpdateMode.Add;
            set.OriginalText = "Test";
            set.OriginalText.Language = "en-US";
            // normalize the set expression
            set = set.Normalize() as QSD<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" validTimeLow=""20080101000000-0500"" validTimeHigh=""20080131235959-0500"" updateMode=""A""><originalText language=""en-US"" value=""Test""/><minuend xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""10""/></minuend><subtrahend xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""5""/><high value=""8""/></subtrahend></test>";

            //Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization test for a QSD set of integers.
        /// Using SXPR with QSD.
        /// </summary>
        [TestMethod]
        public void R2QSD_INT_MixedTermsTest05()
        {
            // create differentiating continuous set expression
            QSD<INT> set = new QSD<INT>(

                    // minuend
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                    // subtrahend
                    SXPR<INT>.CreateSXPR(
                        new IVL<INT>(4,6) { 
                            Operator = SetOperator.Intersect,
                            LowClosed = true,
                            HighClosed = true
                        },
                        new IVL<INT>(9, 10)
                        {
                            Operator = SetOperator.Intersect,
                            LowClosed = true,
                            HighClosed = true
                        }
                    )
            );

            // normalize the set expression
            set = set.Normalize() as QSD<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><minuend xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""10""/></minuend><subtrahend xsi:type=""IVL_INT""><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""4""/><high value=""6""/></term><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""9""/><high value=""10""/></term></subtrahend></test>";

            //Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        //////////// QSD PARSE TESTS ////////////
        //////////// QSD PARSE TESTS ////////////
        //////////// QSD PARSE TESTS ////////////


        /// <summary>
        /// Parse test for a QSD set of integers.
        /// Proeprties are all default.
        /// </summary>
        [TestMethod]
        public void R2QSD_INT_ParseTest()
        {
            // create differentiating continuous set expression
            QSD<INT> set = new QSD<INT>(
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                    new IVL<INT>(5, 8)
                    {
                        LowClosed = true,
                        HighClosed = true
                    }
            );

            // normalize the set expression
            set = set.Normalize() as QSD<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSD<INT>>(actualXml);

            Assert.AreEqual(set, set2);
        }



        /// <summary>
        /// Parse test for a QSD set of integers.
        /// Properties specified:
        ///     ValidTimeLow:       January 1, 2008 at 00:00:00am
        ///     ValidTimeHigh:      January 1, 2008 at 11:59:59pm
        ///     NullFlavor:         null
        ///     UpdateMode:         null
        ///     OriginalText:       null
        /// </summary>
        [TestMethod]
        public void R2QSD_INT_ParseTest02()
        {
            // create differentiating continuous set expression
            QSD<INT> set = new QSD<INT>(
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                    new IVL<INT>(5, 8)
                    {
                        LowClosed = true,
                        HighClosed = true
                    }
            );

            // Set Valid Contact Interval
            set.ValidTimeLow = new TS(new DateTime(2008, 01, 01, 00, 00, 00), DatePrecision.Second);
            set.ValidTimeHigh = new TS(new DateTime(2008, 01, 31, 23, 59, 59), DatePrecision.Second);

            set.NullFlavor = null;
            set.UpdateMode = null;
            set.OriginalText = null;


            // normalize the set expression
            set = set.Normalize() as QSD<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSD<INT>>(actualXml);

            Assert.AreEqual(set, set2);
        }


        /// <summary>
        /// Parse test for a QSD set of integers.
        /// Properties specified:
        ///     ValidTimeLow:       January 1, 2008 at 00:00:00am
        ///     ValidTimeHigh:      January 1, 2008 at 11:59:59pm
        ///     NullFlavor:         null
        ///     UpdateMode:         UpdateMode.Add
        ///     OriginalText:       "Test"
        /// </summary>
        [TestMethod]
        public void R2QSD_INT_ParseTest03()
        {
            // create differentiating continuous set expression
            QSD<INT> set = new QSD<INT>(
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                    new IVL<INT>(5, 8)
                    {
                        LowClosed = true,
                        HighClosed = true
                    }
            );

            // Set Valid Contact Interval
            set.ValidTimeLow = new TS(new DateTime(2008, 01, 01, 00, 00, 00), DatePrecision.Second);
            set.ValidTimeHigh = new TS(new DateTime(2008, 01, 31, 23, 59, 59), DatePrecision.Second);

            set.NullFlavor = null;
            set.UpdateMode = UpdateMode.Add;
            set.OriginalText = "Test";


            // normalize the set expression
            set = set.Normalize() as QSD<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSD<INT>>(actualXml);
            Assert.AreEqual(set, set2);
        }



        /// <summary>
        /// Serialization test for a QSD set of integers.
        /// Using SXPR with QSD.
        /// </summary>
        [TestMethod]
        public void R2QSD_INT_MixedTermsParseTest04()
        {
            /*
            // create differentiating continuous set expression
            QSD<INT> set = new QSD<INT>(

                    // minuend
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                // subtrahend
                    new SXPR<INT>(
                        new IVL<INT>(4, 6)
                        {
                            Operator = SetOperator.Intersect,
                            LowClosed = true,
                            HighClosed = true
                        },
                        new IVL<INT>(9, 10)
                        {
                            Operator = SetOperator.Intersect,
                            LowClosed = true,
                            HighClosed = true
                        }
                    )
            );

            // normalize the set expression
            set = set.Normalize() as QSD<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSD<INT>>(actualXml);
            Assert.AreEqual(set, set2);
            */
        }




        //////////// QSU SERIALIZATION TEST ////////////
        //////////// QSU SERIALIZATION TEST ////////////
        //////////// QSU SERIALIZATION TEST ////////////

        /// <summary>
        /// Serialization test for a QSU set of integers.
        /// Using SXPR with QSU.
        /// </summary>
        [TestMethod]
        public void R2QSUSerializationTest()
        {
            QSU<INT> set = QSU<INT>.CreateQSU(
                new IVL<INT>(1,5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // normalize the set expression
            set = set.Normalize() as QSU<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""5""/></term><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""6""/><high value=""10""/></term></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Serialization test for a QSU set of integers.
        /// ValidTimeLow:    null
        /// ValidTimeHigh:   null
        /// UpdateMode:      null
        /// Nullflavor:      null
        /// </summary>
        [TestMethod]
        public void R2QSUSerializationTest02()
        {
            QSU<INT> set = QSU<INT>.CreateQSU(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // Set Properties
            set.NullFlavor = null;
            set.ValidTimeLow = null;
            set.ValidTimeHigh = null;
            set.UpdateMode = null;

            // normalize the set expression
            set = set.Normalize() as QSU<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""5""/></term><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""6""/><high value=""10""/></term></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for a QSU set of integers.
        /// ValidTimeLow:       January 1, 2008
        /// ValidTimeHigh:      December 1, 2008
        /// UpdateMode:         UpdateMode.Add
        /// Nullflavor:         null
        /// </summary>
        [TestMethod]
        public void R2QSUSerializationTest03()
        {
            QSU<INT> set = QSU<INT>.CreateQSU(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // Set Properties
            set.NullFlavor = null;
            set.ValidTimeLow = new TS((new DateTime(2008, 01, 01)), DatePrecision.Day);
            set.ValidTimeHigh = new TS((new DateTime(2008, 12, 01)), DatePrecision.Day);
            set.UpdateMode = UpdateMode.Add;

            // normalize the set expression
            set = set.Normalize() as QSU<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" updateMode=""A"" validTimeLow=""20080101"" validTimeHigh=""20081201""><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""5""/></term><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""6""/><high value=""10""/></term></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for a QSU set of integers.
        /// Using SXPR with QSU.
        /// </summary>
        [TestMethod]
        public void R2QSUMixedTermsSerializationTest()
        {
            QSU<INT> set = QSU<INT>.CreateQSU(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                SXPR<INT>.CreateSXPR(
                    new IVL<INT>(4,6) { 
                            Operator = SetOperator.Inclusive,
                            LowClosed = true,
                            HighClosed = true
                        },
                        new IVL<INT>(9, 10)
                        {
                            Operator = SetOperator.Inclusive,
                            LowClosed = true,
                            HighClosed = true
                        }
                    )
            );

            // normalize the set expression
            set = set.Normalize() as QSU<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""5""/></term><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""6""/><high value=""10""/></term><term xsi:type=""QSU_INT"" ><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""4""/><high value=""6""/></term><term xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""9""/><high value=""10""/></term></term></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        //////////// QSU PARSE TEST ////////////
        //////////// QSU PARSE TEST ////////////
        //////////// QSU PARSE TEST ////////////


        /// <summary>
        /// Parse test for a QSU set of integers.
        /// Using SXPR with QSU.
        /// </summary>
        [TestMethod]
        public void R2QSUParseTest()
        {
            QSU<INT> set = QSU<INT>.CreateQSU(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // normalize the set expression
            set = set.Normalize() as QSU<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSU<INT>>(actualXml);
            Assert.AreEqual(set, set2);
        }



        /// <summary>
        /// Parse test for a QSU set of integers.
        /// Using SXPR with QSU.
        /// </summary>
        [TestMethod]
        public void R2QSUParseTest02()
        {
            QSU<INT> set = QSU<INT>.CreateQSU(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // Set Properties
            set.NullFlavor = null;
            set.ValidTimeLow = null;
            set.ValidTimeHigh = null;
            set.UpdateMode = null;

            // normalize the set expression
            set = set.Normalize() as QSU<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSU<INT>>(actualXml);
            Assert.AreEqual(set, set2);
        }


        /// <summary>
        /// Parse test for a QSU set of integers.
        /// Using SXPR with QSU.
        /// </summary>
        [TestMethod]
        public void R2QSUParseTest03()
        {
            QSU<INT> set = QSU<INT>.CreateQSU(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // Set Properties
            set.NullFlavor = null;
            set.ValidTimeLow = new TS((new DateTime(2008, 01, 01)), DatePrecision.Day);
            set.ValidTimeHigh = new TS((new DateTime(2008, 12, 01)), DatePrecision.Day);
            set.UpdateMode = UpdateMode.Add;

            // normalize the set expression
            set = set.Normalize() as QSU<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSU<INT>>(actualXml);
            Assert.AreEqual(set, set2);
        }



        /// <summary>
        /// Serialization test for a QSU set of integers.
        /// Using SXPR with QSU.
        /// </summary>
        [TestMethod]
        public void R2QSUMixedTermsParseTest()
        {
            /*
            QSU<INT> set = new QSU<INT>(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new SXPR<INT>(
                    new IVL<INT>(4, 6)
                    {
                        Operator = SetOperator.Inclusive,
                        LowClosed = true,
                        HighClosed = true
                    },
                        new IVL<INT>(9, 10)
                        {
                            Operator = SetOperator.Inclusive,
                            LowClosed = true,
                            HighClosed = true
                        }
                    )
            );

            // normalize the set expression
            set = set.Normalize() as QSU<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSU<INT>>(actualXml);
            Assert.AreEqual(set, set2);
            */
        }



        //////////// QSP SERIALIZATION TEST ////////////
        //////////// QSP SERIALIZATION TEST ////////////
        //////////// QSP SERIALIZATION TEST ////////////

        /// <summary>
        /// Serialization test for a QSP set of integers.
        /// Using SXPR with QSP.
        /// </summary>
        [TestMethod]
        public void R2QSPMixedTermsSerializationTest()
        {
            QSP<INT> set = new QSP<INT>(

                // This set will result in an interval of 1..10,
                // and is also the Low part of the overall Periodic Hull
                SXPR<INT>.CreateSXPR(
                    new IVL<INT>(1, 5)
                    {
                        LowClosed = true,
                        HighClosed = true,
                        Operator = SetOperator.PeriodicHull
                    },
                    new IVL<INT>(6, 10)
                    {
                        LowClosed = true,
                        HighClosed = true,
                        Operator = SetOperator.PeriodicHull
                    }
                    ),

                // This interval is the High part of the overall Periodic Hull
                new IVL<INT>(8, 12)
                {
                    LowClosed = true,
                    HighClosed = true
                }
                );

            // resulting interval will be 1..12

            // normalize the set expression
            set = set.Normalize() as QSP<INT>;
            var expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""urn:hl7-org:v3""><low xsi:type=""QSP_INT""><low xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1"" /><high value=""5"" /></low><high xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""6"" /><high value=""10"" /></high></low><high xsi:type=""QSP_INT"" lowClosed=""true"" highClosed=""true""><low value=""8""/><high value=""12""/></high></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for a QSP set of integers.
        /// No properties specified.
        /// </summary>
        [TestMethod]
        public void R2QSPSerializationTest()
        {
            QSP<INT> set = new QSP<INT>(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // normalize the set expression
            set = set.Normalize() as QSP<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><low xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1"" /><high value=""5"" /></low><high xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""6""/><high value=""10""/></high></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for a QSP set of integers.
        /// NullFlavor:     null
        /// ValidTimeHigh:  null
        /// ValidTimeLow:   null
        /// UpdateMode:     null
        /// </summary>
        [TestMethod]
        public void R2QSPSerializationTest02()
        {
            QSP<INT> set = new QSP<INT>(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // Nullifiy properties
            set.NullFlavor = null;
            set.ValidTimeHigh = null;
            set.ValidTimeLow = null;
            set.UpdateMode = null;

            // normalize the set expression
            set = set.Normalize() as QSP<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><low xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1"" /><high value=""5"" /></low><high xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""6""/><high value=""10""/></high></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Serialization test for a QSP set of integers.
        /// NullFlavor:     null
        /// ValidTimeHigh:  January 1, 2008
        /// ValidTimeLow:   January 31, 2008
        /// UpdateMode:     Update.Add
        /// </summary>
        [TestMethod]
        public void R2QSPSerializationTest03()
        {
            QSP<INT> set = new QSP<INT>(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // Nullifiy properties
            set.NullFlavor = null;
            set.ValidTimeLow = new TS((new DateTime(2008, 01, 01)), DatePrecision.Day);
            set.ValidTimeHigh = new TS((new DateTime(2008, 01, 31)), DatePrecision.Day);
            set.UpdateMode = UpdateMode.Add;

            // normalize the set expression
            set = set.Normalize() as QSP<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" updateMode=""A"" validTimeLow=""20080101"" validTimeHigh=""20080131""><low xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""5""/></low><high xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""6""/><high value=""10""/></high></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        //////////// QSP PARSE TESTS ////////////
        //////////// QSP PARSE TESTS ////////////
        //////////// QSP PARSE TESTS ////////////


        /// <summary>
        /// Parse test for a QSP set of integers.
        /// Using SXPR with QSP.
        /// </summary>
        [TestMethod]
        public void R2QSPMixedTermsParseTest()
        {
            /*
            QSP<INT> set = new QSP<INT>(

                // This set will result in an interval of 1..10,
                // and is also the Low part of the overall Periodic Hull
                new SXPR<INT>(
                    new IVL<INT>(1, 5)
                    {
                        LowClosed = true,
                        HighClosed = true,
                        Operator = SetOperator.PeriodicHull
                    },
                    new IVL<INT>(6, 10)
                    {
                        LowClosed = true,
                        HighClosed = true,
                        Operator = SetOperator.PeriodicHull
                    }
                    ),

                // This interval is the High part of the overall Periodic Hull
                new IVL<INT>(8, 12)
                {
                    LowClosed = true,
                    HighClosed = true
                }
                );

            // normalize the set expression
            set = set.Normalize() as QSP<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSP<INT>>(actualXml);
            Assert.AreEqual(set, set2);
            */
        }



        /// <summary>
        /// Parsing test for a QSP set of integers.
        /// No properties specified.
        /// </summary>
        [TestMethod]
        public void R2QSPParseTest()
        {
            QSP<INT> set = new QSP<INT>(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // normalize the set expression
            set = set.Normalize() as QSP<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSP<INT>>(actualXml);
            Assert.AreEqual(set, set2);
        }


        /// <summary>
        /// Parsing test for a QSP set of integers.
        /// NullFlavor:     null
        /// ValidTimeHigh:  null
        /// ValidTimeLow:   null
        /// UpdateMode:     null
        /// </summary>
        [TestMethod]
        public void R2QSPParseTest02()
        {
            QSP<INT> set = new QSP<INT>(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // Nullifiy properties
            set.NullFlavor = null;
            set.ValidTimeHigh = null;
            set.ValidTimeLow = null;
            set.UpdateMode = null;

            // normalize the set expression
            set = set.Normalize() as QSP<INT>;

            // normalize the set expression
            set = set.Normalize() as QSP<INT>;
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            var set2 = R2SerializationHelper.ParseString<QSP<INT>>(actualXml);
            Assert.AreEqual(set, set2);
        }


        
        /// <summary>
        /// Serialization test for a QSP set of integers.
        /// NullFlavor:     null
        /// ValidTimeHigh:  January 1, 2008
        /// ValidTimeLow:   January 31, 2008
        /// UpdateMode:     Update.Add
        /// </summary>
        [TestMethod]
        public void R2QSPParseTest03()
        {
            QSP<INT> set = new QSP<INT>(
                new IVL<INT>(1, 5)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(6, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // Nullifiy properties
            set.NullFlavor = null;
            set.ValidTimeLow = new TS((new DateTime(2008, 01, 01)), DatePrecision.Day);
            set.ValidTimeHigh = new TS((new DateTime(2008, 01, 31)), DatePrecision.Day);
            set.UpdateMode = UpdateMode.Add;

            // normalize the set expression
            set = set.Normalize() as QSP<INT>;
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" updateMode=""A"" validTimeLow=""20080101"" validTimeHigh=""20080131""><low xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""1""/><high value=""5""/></low><high xsi:type=""IVL_INT"" lowClosed=""true"" highClosed=""true""><low value=""6""/><high value=""10""/></high></test>";

            Assert.IsTrue(set.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(set);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
    }
}
