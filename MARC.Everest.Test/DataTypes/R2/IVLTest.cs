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
    /// Summary description for IVLTest
    /// </summary>
    [TestClass]
    public class IVLTest
    {
        public IVLTest()
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


        ////////////// IVL INT SERIALIZATION TESTS //////////////
        ////////////// IVL INT SERIALIZATION TESTS //////////////
        ////////////// IVL INT SERIALIZATION TESTS //////////////

        /// <summary>
        /// IVL Serialization Test using basetype of INT
        /// Properties:
        /// LowClosed       :   true
        /// HighClosed      :   true
        /// ValidTimeLow    :   null
        /// ValidTimeHigh   :   null
        /// NullFlavor      :   null
        /// UpdateMode      :   null
        /// OriginalText    :   null
        /// Flavor          :   null
        /// </summary>
        [TestMethod]
        public void IVL_INTSerializationTest()
        {
            IVL<INT> ivl = new IVL<INT>(10, 100) 
            {
                ValidTimeLow = null,
                ValidTimeHigh = null,
                NullFlavor = null,
                UpdateMode = null,
                LowClosed = true,
                HighClosed = true,
                OriginalText = null,
                Flavor = null
            };

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" lowClosed=""true"" highClosed=""true""><low value=""10""/><high value=""100""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// IVL Serialization Test using basetype of INT
        /// Properties:
        /// LowClosed       :   true
        /// HighClosed      :   true
        /// ValidTimeLow    :   January 01, 2008
        /// ValidTimeHigh   :   January 31, 2008
        /// NullFlavor      :   null
        /// UpdateMode      :   Update.Add
        /// OriginalText    :   "Test"
        /// Flavor          :   null
        /// </summary>
        [TestMethod]
        public void IVL_INTSerializationTest02()
        {
            IVL<INT> ivl = new IVL<INT>(10, 100)
            {
                ValidTimeLow = new TS(new DateTime(2008, 01, 01), DatePrecision.Day),
                ValidTimeHigh = new TS(new DateTime(2008, 01, 31), DatePrecision.Day),
                NullFlavor = null,
                UpdateMode = UpdateMode.Add,
                LowClosed = true,
                HighClosed = true,
                OriginalText = "Test",
                Flavor = null
            };
            ivl.OriginalText.Language = "en-US";

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" validTimeLow=""20080101"" validTimeHigh=""20080131"" lowClosed=""true"" highClosed=""true"" updateMode=""A"" ><originalText language=""en-US"" value=""Test"" /><low value=""10""/><high value=""100""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// IVL Serialization Test using basetype of INT
        /// Properties:
        /// Low         :   10
        /// Width       :   100
        /// LowClosed   :   true
        /// </summary>
        [TestMethod]
        public void IVL_INTSerializationTest03()
        {
            IVL<INT> ivl = new IVL<INT>(10, null)
            {
                Width = 100,
                LowClosed = true
            };

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" lowClosed=""true""><low value=""10""/><width value=""100""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// IVL Serialization Test using basetype of INT
        /// Properties:
        /// High        :   100
        /// Width       :   90
        /// HighClosed  :   true
        /// </summary>
        [TestMethod]
        public void IVL_INTSerializationTest04()
        {
            IVL<INT> ivl = new IVL<INT>(null, 100)
            {
                Width = 90,
                HighClosed = true
            };

            Assert.IsTrue(ivl.Validate());
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" highClosed=""true""><high value=""100""/><width value=""90""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        ////////////// IVL INT PARSE TESTS //////////////
        ////////////// IVL INT PARSE TESTS //////////////
        ////////////// IVL INT PARSE TESTS //////////////


        /// <summary>
        /// IVL Parse Test using basetype of INT
        /// Properties:
        /// Low             :   10
        /// High            :   100
        /// LowClosed       :   true
        /// HighClosed      :   true
        /// ValidTimeLow    :   null
        /// ValidTimeHigh   :   null
        /// NullFlavor      :   null
        /// UpdateMode      :   null
        /// OriginalText    :   null
        /// Flavor          :   null
        /// </summary>
        [TestMethod]
        public void IVL_INTParseTest()
        {
            IVL<INT> ivl = new IVL<INT>(10, 100)
            {
                ValidTimeLow = null,
                ValidTimeHigh = null,
                NullFlavor = null,
                UpdateMode = null,
                LowClosed = true,
                HighClosed = true,
                OriginalText = null,
                Flavor = null
            };

            // normalize the ivl expression
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<INT>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
        }



        /// <summary>
        /// IVL Parse Test using basetype of INT
        /// Properties:
        /// Low             :   10
        /// High            :   100
        /// LowClosed       :   true
        /// HighClosed      :   true
        /// ValidTimeLow    :   January 01, 2008
        /// ValidTimeHigh   :   January 31, 2008
        /// NullFlavor      :   null
        /// UpdateMode      :   Update.Add
        /// OriginalText    :   "Test"
        /// Flavor          :   null
        /// </summary>
        [TestMethod]
        public void IVL_INTParseTest02()
        {
            IVL<INT> ivl = new IVL<INT>(10, 100)
            {
                ValidTimeLow = new TS(new DateTime(2008, 01, 01), DatePrecision.Day),
                ValidTimeHigh = new TS(new DateTime(2008, 01, 31), DatePrecision.Day),
                NullFlavor = null,
                UpdateMode = UpdateMode.Add,
                LowClosed = true,
                HighClosed = true,
                OriginalText = "Test",
                Flavor = null
            };

            // normalize the ivl expression
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<INT>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
        }



        /// <summary>
        /// IVL Parse Test using basetype of INT
        /// Properties:
        /// Low         :   10
        /// Width       :   100
        /// LowClosed   :   true
        /// </summary>
        [TestMethod]
        public void IVL_INTParseTest03()
        {
            /*
            IVL<INT> ivl = new IVL<INT>(10, null)
            {
                LowClosed = true,
                Width = 100                
            };

            // normalize the ivl expression
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<INT>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
             * */
        }



        /// <summary>
        /// IVL Parse Test using basetype of INT
        /// Properties:
        /// High        :   100
        /// Width       :   90
        /// HighClosed  :   true
        /// </summary>
        [TestMethod]
        public void IVL_INTParseTest04()
        {
            /*
            IVL<INT> ivl = new IVL<INT>(null, 100)
            {
                Width = 90,
                HighClosed = true
            };

            // normalize the ivl expression
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<INT>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
             * */
        }

        ////////////// IVL TS SERIALIZATION TESTS //////////////
        ////////////// IVL TS SERIALIZATION TESTS //////////////
        ////////////// IVL TS SERIALIZATION TESTS //////////////


        /// <summary>
        /// IVL Serialization Test using basetype of TS
        /// Properties:
        /// Low         :   January 01, 2008
        /// High        :   January 31, 2008
        /// LowClosed   :   true
        /// Highclosed  :   true
        /// </summary>
        [TestMethod]
        public void IVL_TSSerializationTest01()
        {
            IVL<TS> ivl = new IVL<TS>(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Day),  // low
                new TS(new DateTime(2008, 01, 31), DatePrecision.Day)   // high
                );

            ivl.HighClosed = true;
            ivl.LowClosed = true;

            Assert.IsTrue(ivl.Validate());
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" lowClosed=""true"" highClosed=""true""><low value=""20080101""/><high value=""20080131""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// IVL Serialization Test using basetype of TS
        /// Properties:
        /// Width
        /// Low         :   January 01, 2008
        /// LowClosed   :   true
        /// </summary>
        [TestMethod]
        public void IVL_TSSerializationTest02()
        {
            IVL<TS> ivl = new IVL<TS>(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Day),  // low
                null                                                    // high
                );

            ivl.Width = new PQ(1, "w");
            ivl.LowClosed = true;

            Assert.IsTrue(ivl.Validate());
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" lowClosed=""true""><low value=""20080101""/><width value=""1"" unit=""w""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// IVL Serialization Test using basetype of TS
        /// Properties:
        /// Width
        /// High        :   January 31, 2008
        /// Highclosed  :   true
        /// </summary>
        [TestMethod]
        public void IVL_TSSerializationTest03()
        {
            IVL<TS> ivl = new IVL<TS>(
                null,                                                   // low
                new TS(new DateTime(2008, 01, 31), DatePrecision.Day)  // high
                );

            ivl.Width = new PQ(1, "w");
            ivl.HighClosed = true;

            Assert.IsTrue(ivl.Validate());
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" highClosed=""true""><high value=""20080131""/><width value=""1"" unit=""w""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        ////////////// IVL TS PARSE TESTS //////////////
        ////////////// IVL TS PARSE TESTS //////////////
        ////////////// IVL TS PARSE TESTS //////////////

        /// <summary>
        /// IVL Parse Test using basetype of TS
        /// Properties:
        /// Low         :   January 01, 2008
        /// High        :   January 31, 2008
        /// LowClosed   :   true
        /// Highclosed  :   true
        /// </summary>
        [TestMethod]
        public void IVL_TSParseTest01()
        {
            IVL<TS> ivl = new IVL<TS>(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Day),  // low
                new TS(new DateTime(2008, 01, 31), DatePrecision.Day)   // high
                );

            ivl.HighClosed = true;
            ivl.LowClosed = true;

            // normalize the ivl expression
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<TS>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
        }



        /// <summary>
        /// IVL Parse Test using basetype of TS
        /// Properties:
        /// Low         :   January 01, 2008
        /// Width       :   1 week
        /// LowClosed   :   true
        /// </summary>
        [TestMethod]
        public void IVL_TSParseTest02()
        {
            IVL<TS> ivl = new IVL<TS>(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Day),  // low
                null                                                    // high
                );

            ivl.Width = new PQ(1, "w");
            ivl.LowClosed = true;

            // normalize the ivl expression
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<TS>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
        }



        /// <summary>
        /// IVL Parse Test using basetype of TS
        /// Properties:
        /// Width       :   1 week
        /// High        :   January 31, 2008
        /// Highclosed  :   true
        /// </summary>
        [TestMethod]
        public void IVL_TSParseTest03()
        {
            IVL<TS> ivl = new IVL<TS>(
                null,                                                   // low
                new TS(new DateTime(2008, 01, 31), DatePrecision.Day)  // high
                );

            ivl.Width = new PQ(1, "w");
            ivl.HighClosed = true;

            // normalize the ivl expression
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<TS>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
        }


        /// <summary>
        /// IVL Parse Test using basetype of TS.
        /// ** Testing if TS properties will be parsed. **
        /// 
        /// Properties:
        /// Width       :   1 week
        /// High        :   January 31, 2008
        /// Highclosed  :   true
        /// 
        /// TS UpdateMode   :   Update.Add
        /// </summary>
        [TestMethod]
        public void IVL_TSParseTest04()
        {

            TS high = new TS(new DateTime(2008, 01, 31), DatePrecision.Day);
            high.UpdateMode = UpdateMode.Add;

            IVL<TS> ivl = new IVL<TS>(
                null,                                                   // low
                high  // high
                );

            ivl.Width = new PQ(1, "w");
            ivl.HighClosed = true;

            // normalize the ivl expression
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<TS>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
        }

        ////////////// IVL PQ SERIALIZATION TESTS //////////////
        ////////////// IVL PQ SERIALIZATION TESTS //////////////
        ////////////// IVL PQ SERIALIZATION TESTS //////////////


        /// <summary>
        /// IVL Serialization Test using basetype of PQ.
        /// /// Properties:
        /// Low             :   1 second
        /// High            :   10 seconds
        /// Width:          :   null
        /// ValidTimeHigh   :   null
        /// ValidTimeLow    :   null
        /// updateMode      :   null
        /// LowClosed       :   true
        /// HighClosed      :   true
        /// OriginalText    :   null
        /// Nullflavor      :   null
        /// </summary>
        [TestMethod]
        public void IVL_PQSerializationTest01()
        {
            IVL<PQ> ivl = new IVL<PQ>(
                new PQ(1, "s"),
                new PQ(10, "s")
            );

            ivl.LowClosed = true;
            ivl.HighClosed = true;
            ivl.ValidTimeLow = null; ;
            ivl.ValidTimeHigh = null;
            ivl.UpdateMode = null;
            ivl.Width = null;
            ivl.NullFlavor = null;

            Assert.IsTrue(ivl.Validate());
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" lowClosed=""true"" highClosed=""true""><low value=""1"" unit=""s"" /><high value=""10"" unit=""s""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// IVL Serialization Test using basetype of PQ.
        /// ** Testing to see if basetype properties are graphed. **
        /// 
        /// Properties:
        /// Low             :   1 second
        /// High            :   10 seconds
        /// Width:          :   null
        /// ValidTimeLow:       January 1, 2008
        /// ValidTimeHigh:      January 31, 2008
        /// updateMode      :   Update.Add
        /// LowClosed       :   true
        /// HighClosed      :   true
        /// OriginalText    :   "Test"
        /// Nullflavor      :   null
        ///
        /// High CodingRationale :   Required
        /// Low CodingRationale  :   Required
        /// 
        /// </summary>
        [TestMethod]
        public void IVL_PQSerializationTest02()
        {
            IVL<PQ> ivl = new IVL<PQ>(
                new PQ(1, "s"),
                new PQ(10, "s")
            );

            ivl.ValidTimeLow = new TS(new DateTime(2008, 01, 01), DatePrecision.Day);
            ivl.ValidTimeHigh = new TS(new DateTime(2008, 01, 31), DatePrecision.Day);
            ivl.UpdateMode = UpdateMode.Add;
            ivl.LowClosed = true;
            ivl.HighClosed = true;
            ivl.Width = null;
            ivl.OriginalText = "Test";
            ivl.OriginalText.Language = "en-US";

            ivl.NullFlavor = null;

            Assert.IsTrue(ivl.Validate());
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" lowClosed=""true"" highClosed=""true"" validTimeLow=""20080101"" validTimeHigh=""20080131"" updateMode=""A""><originalText language=""en-US"" value=""Test"" /><low value=""1"" unit=""s"" /><high value=""10"" unit=""s"" /></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }


        /// <summary>
        /// IVL Parse Test using basetype of PQ.
        /// Properties:
        /// ValidTimeLow:       January 1, 2008
        /// ValidTimeHigh:      January 31, 2008
        /// updateMode      :   Update.Add
        /// LowClosed       :   null
        /// HighClosed      :   true
        /// Nullflavor      :   null
        /// Width:          :   null
        /// OriginalText    :   "Test"
        /// 
        /// Testing to see if basetype properties are parsed.
        /// High CodingRationale :   Required
        /// Low CodingRationale  :   Required
        /// </summary>
        [TestMethod]
        public void IVL_PQSerializationTest03()
        {
            PQ low = new PQ(1, "s");
            low.CodingRationale = new SET<CodingRationale>(
                CodingRationale.Required
                );

            PQ high = new PQ(10, "s");
            high.CodingRationale = new SET<CodingRationale>(
                CodingRationale.Required
                );

            IVL<PQ> ivl = new IVL<PQ>(
                low, high
            );


            ivl.ValidTimeLow = new TS(new DateTime(2008, 01, 01), DatePrecision.Day);
            ivl.ValidTimeHigh = new TS(new DateTime(2008, 01, 31), DatePrecision.Day);
            ivl.UpdateMode = UpdateMode.Add;
            ivl.Width = null;
            ivl.LowClosed = true;
            ivl.HighClosed = true;
            ivl.NullFlavor = null;
            ivl.OriginalText = "Test";
            ivl.OriginalText.Language = "en-US";

            Assert.IsTrue(ivl.Validate());
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" lowClosed=""true"" highClosed=""true"" validTimeLow=""20080101"" validTimeHigh=""20080131"" updateMode=""A"" ><originalText language=""en-US"" value=""Test"" /><low value=""1"" unit=""s"" codingRationale=""Required""/><high value=""10"" unit=""s"" codingRationale=""Required""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        } 


        ////////////// IVL PQ PARSE TESTS //////////////
        ////////////// IVL PQ PARSE TESTS //////////////
        ////////////// IVL PQ PARSE TESTS //////////////

        /// <summary>
        /// IVL Parse Test using basetype of PQ.
        /// Properties:
        /// ValidTimeHigh   :   null
        /// ValidTimeLow    :   null
        /// updateMode      :   null
        /// LowClosed       :   true
        /// HighClosed      :   true
        /// Nullflavor      :   null
        /// Width:          :   null
        /// </summary>
        [TestMethod]
        public void IVL_PQParseTest01()
        {
            IVL<PQ> ivl = new IVL<PQ>(
                new PQ(1, "s"),
                new PQ(10, "s")
            );

            ivl.Width = new PQ(1, "w");
            ivl.HighClosed = true;
            ivl.ValidTimeLow = null; ;
            ivl.ValidTimeHigh = null;
            ivl.UpdateMode = null;
            ivl.Width = null;
            ivl.NullFlavor = null;

            // normalize the ivl expression
            Assert.IsTrue(ivl.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<PQ>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
        }




        /// <summary>
        /// IVL Parse Test using basetype of PQ.
        /// Properties:
        /// ValidTimeLow:       January 1, 2008
        /// ValidTimeHigh:      January 31, 2008
        /// updateMode      :   Update.Add
        /// LowClosed       :   true
        /// HighClosed      :   true
        /// Nullflavor      :   null
        /// Width:          :   null
        /// OriginalText    :   "Test"
        /// 
        /// Testing to see if basetype properties are parsed.
        /// High CodingRationale :   Required
        /// Low CodingRationale  :   Required
        /// </summary>
        [TestMethod]
        public void IVL_PQParseTest02()
        {
           IVL<PQ> ivl = new IVL<PQ>(
                new PQ(1, "s"),
                new PQ(10, "s")
            );

            ivl.ValidTimeLow = new TS(new DateTime(2008, 01, 01), DatePrecision.Day);
            ivl.ValidTimeHigh = new TS(new DateTime(2008, 01, 31), DatePrecision.Day);
            ivl.UpdateMode = UpdateMode.Add;
            ivl.Width = null;
            ivl.NullFlavor = null;
            ivl.OriginalText = "Test";

            // normalize the ivl expression
            Assert.IsTrue(ivl.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<PQ>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
        }



        /// <summary>
        /// IVL Parse Test using basetype of PQ.
        /// Properties:
        /// ValidTimeLow:       January 1, 2008
        /// ValidTimeHigh:      January 31, 2008
        /// updateMode      :   Update.Add
        /// LowClosed       :   true
        /// HighClosed      :   true
        /// Nullflavor      :   null
        /// Width:          :   null
        /// OriginalText    :   "Test"
        /// 
        /// Testing to see if basetype properties are parsed.
        /// High CodingRationale :   Required
        /// Low CodingRationale  :   Required
        /// </summary>
        [TestMethod]
        public void IVL_PQParseTest03()
        {
            /*
            PQ low = new PQ(1, "s");
            low.CodingRationale = new SET<CodingRationale>(
                CodingRationale.Required
                );

            PQ high = new PQ(10, "s");
            high.CodingRationale = new SET<CodingRationale>(
                CodingRationale.Required
                );

            IVL<PQ> ivl = new IVL<PQ>(
                low, high
            );

            ivl.ValidTimeLow = new TS(new DateTime(2008, 01, 01), DatePrecision.Day);
            ivl.ValidTimeHigh = new TS(new DateTime(2008, 01, 31), DatePrecision.Day);
            ivl.UpdateMode = UpdateMode.Add;
            ivl.Width = null;
            ivl.NullFlavor = null;
            ivl.OriginalText = "Test";
             * 
            // normalize the ivl expression
            Assert.IsTrue(ivl.Validate());
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            var ivl2 = R2SerializationHelper.ParseString<IVL<PQ>>(actualXml);
            Assert.AreEqual(ivl, ivl2);
            */
        }

        ////////////// OPERATION RECORDS TESTS //////////////

        /// <summary>
        /// IVL Serialization Test using basetype of PQ.
        /// Testing  Operation Record (PW with only width specified).
        /// 
        /// Properties:
        /// ValidTimeHigh   :   null
        /// ValidTimeLow    :   null
        /// updateMode      :   null
        /// LowClosed       :   null
        /// HighClosed      :   null
        /// Nullflavor      :   null
        /// Width:          :   2 hours
        /// OriginalText    :   null
        /// </summary>
        [TestMethod]
        public void IVL_PQSerializationTest04()
        {
            /*
            IVL<PQ> ivl = new IVL<PQ>(null, null);  // no high or low specified

            ivl.Width = new PQ(2, "h");
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.ValidTimeLow = null;
            ivl.ValidTimeHigh = null;
            ivl.UpdateMode = null;
            ivl.NullFlavor = null;

            Assert.IsTrue(ivl.Validate());
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><width value=""2"" unit=""h""/></test>";
            var actualXml = R2SerializationHelper.SerializeAsString(ivl);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
            */
        }
    }
}