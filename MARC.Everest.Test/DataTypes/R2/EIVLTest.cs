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
    /// Summary description for EIVLTest1
    /// </summary>
    [TestClass]
    public class EIVLTest
    {
        public EIVLTest()
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
        /// Testing EIVL serialization with the R2 formatter.
        /// Term:   30 minutes after dinner
        /// </summary>
        [TestMethod]
        public void EIVLSerializationTest01()
        {
            // construct eivl
            EIVL<TS> eivl = new EIVL<TS>(
                DomainTimingEventType.Dinner,
                new IVL<PQ>(
                    new PQ(30, "min"),  // low
                    new PQ(30, "min")  // high
                )
                );
            eivl.Offset.HighClosed = true;
            eivl.Offset.LowClosed = true;
            eivl.ValidTimeHigh = null;
            eivl.ValidTimeLow = null;
            eivl.ControlActRoot = null;
            eivl.ControlActExt = null;
            eivl.NullFlavor = null;
            eivl.UpdateMode = null;
            eivl.OriginalText = null;

            Assert.IsTrue(eivl.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" event=""CV"" ><offset lowClosed=""true"" highClosed=""true""><low value=""30"" unit=""min""/><high value=""30"" unit=""min""/></offset></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(eivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Testing EIVL serialization with the R2 formatter.
        /// Term:   One hour before breakfast for 10 minutes.s
        /// </summary>
        [TestMethod]
        public void EIVLSerializationTest02()
        {
            // construct eivl
            EIVL<TS> eivl = new EIVL<TS>(
                DomainTimingEventType.Breakfast,
                new IVL<PQ>(
                    new PQ(-1, "h"),  // low
                    new PQ(-50, "min")  // high
                )
                );
            eivl.Offset.HighClosed = true;
            eivl.Offset.LowClosed = true;
            eivl.ValidTimeHigh = null;
            eivl.ValidTimeLow = null;
            eivl.ControlActRoot = null;
            eivl.ControlActExt = null;
            eivl.NullFlavor = null;
            eivl.UpdateMode = null;
            eivl.OriginalText = null;

            Assert.IsTrue(eivl.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" event=""CM"" ><offset lowClosed=""true"" highClosed=""true""><low value=""-1"" unit=""h""/><high value=""-50"" unit=""min""/></offset></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(eivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// Testing EIVL serialization with the R2 formatter.
        /// Term:   One hour before breakfast for 10 minutes.
        /// All properties not-null except for NullFlavor.
        /// </summary>
        [TestMethod]
        public void EIVLSerializationTest03()
        {
            // construct eivl
            EIVL<TS> eivl = new EIVL<TS>(
                DomainTimingEventType.Breakfast,
                new IVL<PQ>(
                    new PQ(-1, "h"),  // low
                    new PQ(-50, "min")  // high
                )
                );
            eivl.Offset.HighClosed = true;
            eivl.Offset.LowClosed = true;
            eivl.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            eivl.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            eivl.NullFlavor = null;
            eivl.UpdateMode = UpdateMode.Add;
            eivl.OriginalText = "One hour before breakfast for 10 minutes";

            Assert.IsTrue(eivl.Validate());

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" event=""CM"" validTimeHigh=""20121231"" validTimeLow=""20120101"" updateMode=""A"" ><originalText value=""One hour before breakfast for 10 minutes"" language=""en-US"" /><offset lowClosed=""true"" highClosed=""true""><low value=""-1"" unit=""h""/><high value=""-50"" unit=""min""/></offset></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(eivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        // EIVL PARSE TESTS //


        /// <summary>
        /// Testing EIVL parsing with the R2 formatter.
        /// Term:   30 minutes after dinner.
        /// </summary>
        [TestMethod]
        public void EIVLParseTest01()
        {
            //// construct eivl
            //EIVL<TS> eivl = new EIVL<TS>(
            //    DomainTimingEventType.Dinner,
            //    new IVL<PQ>(
            //        new PQ(30, "min"),  // low
            //        new PQ(30, "min")  // high
            //    )
            //    );
            //eivl.Offset.HighClosed = true;
            //eivl.Offset.LowClosed = true;
            //eivl.ValidTimeHigh = null;
            //eivl.ValidTimeLow = null;
            //eivl.ControlActRoot = null;
            //eivl.ControlActExt = null;
            //eivl.NullFlavor = null;
            //eivl.UpdateMode = null;
            //eivl.OriginalText = null;

            //Assert.IsTrue(eivl.Validate());
            
            //// normalize the pivl expression
            //var actualXml = R2SerializationHelper.SerializeAsString(eivl);
            //var eivl2 = R2SerializationHelper.ParseString<EIVL<TS>>(actualXml);

            //Assert.IsTrue(eivl2.Validate());

            //Assert.AreEqual(eivl, eivl2);
            
        }


        /// <summary>
        /// Testing EIVL parsing with the R2 formatter.
        /// Term:   One hour before breakfast for 10 minutes.
        /// </summary>
        [TestMethod]
        public void EIVLParseTest02()
        {
            //// construct eivl
            //EIVL<TS> eivl = new EIVL<TS>(
            //    DomainTimingEventType.Breakfast,
            //    new IVL<PQ>(
            //        new PQ(-1, "hr"),  // low
            //        new PQ(-50, "min")  // high
            //    )
            //    );
            //eivl.Offset.HighClosed = true;
            //eivl.Offset.LowClosed = true;
            //eivl.NullFlavor = null;
            //eivl.ValidTimeHigh = null;
            //eivl.ValidTimeLow = null;
            //eivl.UpdateMode = null;
            //eivl.OriginalText = null;

            //Assert.IsTrue(eivl.Validate());

            ////// normalize the pivl expression
            //var actualXml = R2SerializationHelper.SerializeAsString(eivl);
            //var eivl2 = R2SerializationHelper.ParseString<EIVL<TS>>(actualXml);
            //Assert.AreEqual(eivl, eivl2);
        }


        /// <summary>
        /// Testing EIVL serialization with the R2 formatter.
        /// Term:   One hour before breakfast for 10 minutes.
        /// All properties not-null except for NullFlavor.
        /// </summary>
        [TestMethod]
        public void EIVLParseTest03()
        {
            //// construct eivl
            //EIVL<TS> eivl = new EIVL<TS>(
            //    DomainTimingEventType.Breakfast,
            //    new IVL<PQ>(
            //        new PQ(-1, "h"),  // low
            //        new PQ(-50, "min")  // high
            //    )
            //    );
            //eivl.Offset.HighClosed = true;
            //eivl.Offset.LowClosed = true;
            //eivl.ValidTimeLow = new TS(new DateTime(2012, 01, 01), DatePrecision.Day);
            //eivl.ValidTimeHigh = new TS(new DateTime(2012, 12, 31), DatePrecision.Day);
            //eivl.NullFlavor = null;
            //eivl.UpdateMode = UpdateMode.Add;
            //eivl.OriginalText = "One hour before breakfast for 10 minutes";

            //Assert.IsTrue(eivl.Validate());
            //var actualXml = R2SerializationHelper.SerializeAsString(eivl);
            //var eivl2 = R2SerializationHelper.ParseString<EIVL<TS>>(actualXml);
        }
    }
}
