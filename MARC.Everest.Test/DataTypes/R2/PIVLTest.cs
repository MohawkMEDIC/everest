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
using System.Collections.Generic;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Summary description for PIVLTest
    /// </summary>
    [TestClass]
    public class PIVLTest
    {
        public PIVLTest()
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

        //////////// PIVL SERIALIZATION TESTS ////////////
        //////////// PIVL SERIALIZATION TESTS ////////////
        //////////// PIVL SERIALIZATION TESTS ////////////

        /// <summary>
        /// Serialization of a PIVL.
        /// Represents a PIVL of 'Twice per day' using period.
        /// 
        /// Properties specified:
        /// Period      :   Every 12 hours.
        /// 
        /// </summary>
        [TestMethod]
        public void PIVLSerializationTest01()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ(12, "h");

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" ><period value=""12"" unit=""h""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        // xsi:type=""PIVL_TS"" isFlexible=""true""

        /// <summary>
        /// Serialization of a PIVL.
        /// Represents a PIVL of 'Twice per day' using frequency.
        /// 
        /// Properties specified:
        /// Frequency   :   2 times for every 1 day.
        /// </summary>
        [TestMethod]
        public void PIVLSerializationTest02()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Frequency = new RTO<INT, PQ>()
            {
                Numerator = 2,
                Denominator = new PQ(1, "d")
            };
            
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><frequency><numerator xsi:type=""INT"" value=""2""/><denominator xsi:type=""PQ"" value=""1"" unit=""d""/></frequency></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization of a PIVL.
        /// Represents a PIVL of '7 times per day' using frequency.
        /// 
        /// Properties specified:
        /// Frequency   :   7 times for every 1 day.
        /// </summary>
        [TestMethod]
        public void PIVLSerializationTest03()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Frequency = new RTO<INT, PQ>()
            {
                Numerator = 7,
                Denominator = new PQ(1, "d")
            };

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><frequency><numerator xsi:type=""INT"" value=""7""/><denominator xsi:type=""PQ"" value=""1"" unit=""d""/></frequency></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization of a PIVL.
        /// Represents a PIVL of '7 times per day' using period.
        /// 
        /// Properties specified:
        /// Period  :   3.4285... times per hour.
        /// </summary>
        [TestMethod]
        public void PIVLSerializationTest04()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ((decimal)3.4285714285714285714285714285714285714, "h");

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><period value=""3.42857142857143"" unit=""h""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Parse of a PIVL.
        /// Represents a PIVL of 'Twice per day for 10 minutes' using period.
        /// 
        /// Properties specified:
        /// Phase   :   10 Minutes
        /// Period  :   Every 12 hours
        /// This test will pass once the UCUM code h is fixed (was 'hr').
        /// </summary>
        [TestMethod]
        public void PIVLSerializationTest05()
        {
            //// create pivl components
            //IVL<TS> ivl = new IVL<TS>(null, null) { Width = new PQ(10, "min") };  // phase
            //PQ pq = new PQ(12, "hr");                                              // period

            //Console.WriteLine("pq:{0}:", pq);

            //PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            //pq.Convert("min");

            //// test pivl components
            //Assert.IsTrue(ivl.Validate());
            //Assert.IsTrue(pq.Validate());

            //// create pivl
            //PIVL<TS> pivl = new PIVL<TS>(ivl, pq);
            //Assert.IsTrue(pivl.Validate());
            
            //var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><phase><width value=""10"" unit=""min""/></phase><period value=""12"" unit=""h"" /></test>";
            //var actualXml = R2SerializationHelper.SerializeAsString(pivl);

            //R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization of a PIVL.
        /// Represents a PIVL of 'Every September' using period and phase.
        /// 
        /// Properties specified:
        /// Phase   :   Month of September
        /// Period  :   1 Year
        /// </summary>
        [TestMethod]
        public void PIVLSerializationTest06()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ(1, "a");
            pivl.Phase = new IVL<TS>(
                new TS(new DateTime(1987,09, 01), DatePrecision.Month),
                new TS(new DateTime(1987, 10, 01), DatePrecision.Month)
            );

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><phase><low value=""198709""/><high value=""198710""/></phase><period value=""1"" unit=""a""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization of a PIVL.
        /// Represents a PIVL of 'Every other Saturday' using period and phase.
        /// 
        /// Properties specified:
        /// Phase   :   Saturday
        /// Period  :   Every 2 weeks
        /// </summary>
        [TestMethod]
        public void PIVLSerializationTest07()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ(2, "wk");
            pivl.Phase = new IVL<TS>(
                new TS(new DateTime(2000, 12, 02), DatePrecision.Day),
                new TS(new DateTime(2000, 12, 03), DatePrecision.Day)
            );

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><phase><low value=""20001202""/><high value=""20001203""/></phase><period value=""2"" unit=""wk""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Serialization of a PIVL.
        /// Represents a PIVL of 'Every 4-6 hours' using period and phase.
        /// 
        /// Properties specified:
        /// Uncertainty :   .57735 times per hour
        /// Period      :   Every 5 hours
        /// </summary>
        [TestMethod]
        public void PIVLSerializationTest08()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ(5, "h");
            pivl.Period.UncertaintyType = new QuantityUncertaintyType();
            pivl.Period.Uncertainty = new PQ((decimal)0.57735, "h");
            
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><period value=""5"" unit=""h"" uncertaintyType=""U""><uncertainty xsi:type=""PQ"" value=""0.57735"" unit=""h"" /></period></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        //////////// PIVL PARSE TESTS ////////////
        //////////// PIVL PARSE TESTS ////////////
        //////////// PIVL PARSE TESTS ////////////

        /// <summary>
        /// Parse test for a PIVL.
        /// Represents a PIVL of 'Twice per day'.
        /// 
        /// Properties specified:
        /// Period      :   Every 12 hours.
        /// </summary>
        [TestMethod]
        public void PIVLParseTest01()
        {
            // Create PIVL
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ(12, "h");

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            var pivl2 = R2SerializationHelper.ParseString<PIVL<TS>>(actualXml);
            Assert.AreEqual(pivl, pivl2);
        }


        // <summary>
        /// Parse of a PIVL.
        /// Represents a PIVL of 'Twice per day' using frequency.
        /// 
        /// Properties specified:
        /// Frequency   :   2 times for every 1 day.
        /// </summary>
        [TestMethod]
        public void PIVLParseTest02()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Frequency = new RTO<INT, PQ>()
            {
                Numerator = 2,
                Denominator = new PQ(1, "d")
            };

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            var pivl2 = R2SerializationHelper.ParseString<PIVL<TS>>(actualXml);
            Assert.AreEqual(pivl, pivl2);
        }


        /// <summary>
        /// Parse of a PIVL.
        /// Represents a PIVL of '7 times per day' using frequency.
        /// 
        /// Properties specified:
        /// Frequency   :   7 times for every 1 day.
        /// </summary>
        [TestMethod]
        public void PIVLParseTest03()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Frequency = new RTO<INT, PQ>()
            {
                Numerator = 7,
                Denominator = new PQ(1, "d")
            };

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            var pivl2 = R2SerializationHelper.ParseString<PIVL<TS>>(actualXml);
            Assert.AreEqual(pivl, pivl2);
        }



        /// <summary>
        /// Parse of a PIVL.
        /// Represents a PIVL of '7 times per day' using period.
        /// 
        /// Properties specified:
        /// Period  :   3.4285... times per hour
        /// </summary>
        [TestMethod]
        public void PIVLParseTest04()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ(3, "h");

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            var pivl2 = R2SerializationHelper.ParseString<PIVL<TS>>(actualXml);
            Assert.AreEqual(pivl, pivl2);
        }



        /// <summary>
        /// Parse of a PIVL.
        /// Represents a PIVL of 'Twice per day for 10 minutes' using period.
        /// 
        /// Properties specified:
        /// Phase   :   10 Minutes
        /// Period  :   Every 12 hours
        /// Unit passes when Period units is set to "hr" instead of "h"
        /// </summary>
        [TestMethod]
        public void PIVLParseTest05()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ(12, "h");
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            pivl.Period.Convert("min");
            pivl.Phase = new IVL<TS>();
            pivl.Phase.High = null;
            pivl.Phase.Low = null;
            pivl.Phase.Width = new PQ(10, "min");

            Assert.IsTrue(pivl.Validate());

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            var pivl2 = R2SerializationHelper.ParseString<PIVL<TS>>(actualXml);
            Assert.AreEqual(pivl, pivl2);
        }



        /// <summary>
        /// Parse of a PIVL.
        /// Represents a PIVL of 'Every September' using period and phase.
        /// 
        /// Properties specified:
        /// Phase   :   Month of September
        /// Period  :   1 Year
        /// </summary>
        [TestMethod]
        public void PIVLParseTest06()
        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ(1, "a");
            pivl.Phase = new IVL<TS>(
                new TS(new DateTime(1987, 09, 01), DatePrecision.Month),
                new TS(new DateTime(1987, 10, 01), DatePrecision.Month)
            );

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            var pivl2 = R2SerializationHelper.ParseString<PIVL<TS>>(actualXml);
            Assert.AreEqual(pivl, pivl2);
        }



        /// <summary>
        /// Parse of a PIVL.
        /// Represents a PIVL of 'Every other Saturday' using period and phase.
        /// 
        /// Properties specified:
        /// Phase   :   Saturday
        /// Period  :   Every 2 weeks
        /// </summary>
        [TestMethod]
        public void PIVLParseTest07()

        {
            // Create PIVL instance
            PIVL<TS> pivl = new PIVL<TS>();
            pivl.Period = new PQ(2, "wk");
            pivl.Phase = new IVL<TS>(
                new TS(new DateTime(2000, 12, 02), DatePrecision.Day),
                new TS(new DateTime(2000, 12, 03), DatePrecision.Day)
            );

            // serialize and parse pivl
            var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            var pivl2 = R2SerializationHelper.ParseString<PIVL<TS>>(actualXml);
            Assert.AreEqual(pivl, pivl2);
        }


        /// <summary>
        /// Parse of a PIVL.
        /// Represents a PIVL of 'Every 4-6 hours' using period and phase.
        /// 
        /// Properties specified:
        /// Uncertainty :   .57735 times per hour
        /// Period      :   Every 5 hours
        /// </summary>
        [TestMethod]
        public void PIVLParseTest08()
        {
            //// Create PIVL instance
            //PIVL<TS> pivl = new PIVL<TS>();
            //pivl.Period = new PQ(5, "hr");
            //pivl.Period.UncertaintyType = new QuantityUncertaintyType();

            //// floating point value from healthcare
            //// datatypes specification (0.57735) fails when casted to decimal
            //pivl.Period.Uncertainty = new PQ((decimal)0.57735, "hr");

            //// serialize and parse pivl
            //var actualXml = R2SerializationHelper.SerializeAsString(pivl);
            //var pivl2 = R2SerializationHelper.ParseString<PIVL<TS>>(actualXml);
            //Assert.AreEqual(pivl, pivl2);
        }
    }
}
