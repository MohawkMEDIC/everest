using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Summary description for TELTest
    /// </summary>
    [TestClass]
    public class TELTest
    {
        public TELTest()
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

        [TestMethod]
        public void R2TELBasicSerializationTest()
        {
            var tel = new TEL("tel:+13335551212;postd=2345",
                new CS<TelecommunicationAddressUse>[] {
                    TelecommunicationAddressUse.WorkPlace, 
                    TelecommunicationAddressUse.Direct
                }
            );
            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""tel:+13335551212;postd=2345"" use=""WP DIR""/>";
            var actualXml = R2SerializationHelper.SerializeAsString(tel);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        [TestMethod]
        public void R2TELUseablePeriodParseTest()
        {
            var tel = new TEL("tel:+190555525485");

            // This PIVL<TS> selects the week of Aug 15 2011 - Aug 19 2011
            var weekdays = new PIVL<TS>(
                new IVL<TS>(DateTime.Parse("08-15-2011"), DateTime.Parse("08-19-2011")),
                new PQ(1, "wk")
            );

            // This PIVL<TS> selects the hours of 9-5
            var nineToFive = new PIVL<TS>(
                new IVL<TS>(DateTime.Parse("08-15-2011 09:00 AM"), DateTime.Parse("08-15-2011 05:00 PM")),
                new PQ(1, "d")
            );

            // We set the usable period to a set of
            // 1. All weekdays
            // 2. intersect with times from 9-5 daily
            tel.UseablePeriod = new GTS(QSI<TS>.CreateQSI(
                weekdays,
                nineToFive 
            ));

            var actualXml = R2SerializationHelper.SerializeAsString(tel);
            var inti = R2SerializationHelper.ParseString<TEL>(actualXml);
            Assert.AreEqual(tel, inti);
        }

        //////////// TEL SERIALIZATION TESTS ////////////
        //////////// TEL SERIALIZATION TESTS ////////////
        //////////// TEL SERIALIZATION TESTS ////////////

        // Web Address
        [TestMethod]
        public void R2TELSerializationTest1()
        {
            TEL telType = new TEL("http://www.temp.org.example/234232");

            telType.Capabilities = null;
            telType.ValidTimeLow = null;
            telType.ValidTimeHigh = null;
            telType.NullFlavor = null;
            telType.UpdateMode = null;
            telType.UseablePeriod = null;

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""http://www.temp.org.example/234232""/>";
            var actualXml = R2SerializationHelper.SerializeAsString(telType);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        //Combined home and work phone
        [TestMethod]
        public void R2TELSerializationTest2()
        {
            TEL telType = new TEL("tel:+15556755745",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.Home,
                    TelecommunicationAddressUse.WorkPlace
                });

            telType.Capabilities = new SET<CS<TelecommunicationCabability>>()
            {
                TelecommunicationCabability.Voice,
                TelecommunicationCabability.Fax
            };

            telType.ValidTimeLow = null;
            telType.ValidTimeHigh = null;
            telType.NullFlavor = null;
            telType.UpdateMode = null;
            telType.UseablePeriod = null;

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""tel:+15556755745"" use=""H WP"" capabilities=""voice fax""/>";
            var actualXml = R2SerializationHelper.SerializeAsString(telType);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        // Unknown home phone number
        [TestMethod]
        public void R2TELSerializationTest3()
        {
            TEL telType = new TEL("tel:",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.Home,
                });

            telType.Capabilities = null;
            telType.ValidTimeLow = null;
            telType.ValidTimeHigh = null;
            telType.NullFlavor = new CS<NullFlavor>(NullFlavor.Unknown);
            telType.UpdateMode = null;
            telType.UseablePeriod = null;

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" nullFlavor=""UNK""/>";
            var actualXml = R2SerializationHelper.SerializeAsString(telType);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        // Work phone with extension
        [TestMethod]
        public void R2TELSerializationTest4()
        {
            TEL telType = new TEL("tel:+1(555)6755745;postd=545",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace, 
                });

            telType.Capabilities = null;
            telType.ValidTimeLow = null;
            telType.ValidTimeHigh = null;
            telType.NullFlavor = null;
            telType.UpdateMode = null;
            telType.UseablePeriod = null;

            var expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""tel:+1(555)6755745;postd=545"" use=""WP""/>";
            var actualXml = R2SerializationHelper.SerializeAsString(telType);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        //////////// TEL PARSE TESTS ////////////
        //////////// TEL PARSE TESTS ////////////
        //////////// TEL PARSE TESTS ////////////

        // Web Address
        [TestMethod]
        public void R2TELParseTest1()
        {
            TEL telType = new TEL("http://www.temp.org.example/234232");

            telType.Capabilities = null;
            telType.ValidTimeLow = null;
            telType.ValidTimeHigh = null;
            telType.NullFlavor = null;
            telType.UpdateMode = null;
            telType.UseablePeriod = null;            

            var actualXml = R2SerializationHelper.SerializeAsString(telType);
            var set2 = R2SerializationHelper.ParseString<TEL>(actualXml);
            Assert.AreEqual(telType, set2);
        }

        //Combined home and work phone
        [TestMethod]
        public void R2TELParseTest2()
        {
            TEL telType = new TEL("tel:+15556755745",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.Home,
                    TelecommunicationAddressUse.WorkPlace
                });

            telType.Capabilities = new SET<CS<TelecommunicationCabability>>()
            {
                TelecommunicationCabability.Voice,
                TelecommunicationCabability.Fax
            };

            telType.ValidTimeLow = null;
            telType.ValidTimeHigh = null;
            telType.NullFlavor = null;
            telType.UpdateMode = null;
            telType.UseablePeriod = null;

            var actualXml = R2SerializationHelper.SerializeAsString(telType);
            var set2 = R2SerializationHelper.ParseString<TEL>(actualXml);
            Assert.AreEqual(telType, set2);
        }

        // Unknown home phone number
        [TestMethod]
        public void R2TELParseTest3()
        {
            //TEL telType = new TEL("tel:",
            //    new CS<TelecommunicationAddressUse>[]{
            //        TelecommunicationAddressUse.Home,
            //    });

            //telType.Capabilities = null;
            //telType.ValidTimeLow = null;
            //telType.ValidTimeHigh = null;
            //telType.NullFlavor = new CS<NullFlavor>(NullFlavor.Unknown);
            //telType.UpdateMode = null;
            //telType.UseablePeriod = null;

            //var actualXml = R2SerializationHelper.SerializeAsString(telType);
            //var set2 = R2SerializationHelper.ParseString<TEL>(actualXml);
            //Assert.AreEqual(telType, set2);
        }

        // Work phone with extension
        [TestMethod]
        public void R2TELParseTest4()
        {
            TEL telType = new TEL("tel:+1(555)6755745;postd=545",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace, 
                });

            telType.Capabilities = null;
            telType.ValidTimeLow = null;
            telType.ValidTimeHigh = null;
            telType.NullFlavor = null;
            telType.UpdateMode = null;
            telType.UseablePeriod = null;

            var actualXml = R2SerializationHelper.SerializeAsString(telType);
            var set2 = R2SerializationHelper.ParseString<TEL>(actualXml);
            Assert.AreEqual(telType, set2);
        }
    }
}
