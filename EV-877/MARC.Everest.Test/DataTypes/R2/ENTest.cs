using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Summary description for ENTest
    /// </summary>
    [TestClass]
    public class ENTest
    {
        public ENTest()
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
        /// An EN instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2ENBasicSerializationTest()
        {
            EN inti = new EN(
                EntityNameUse.Legal,
                new ENXP[] {
                    new ENXP("Sir", EntityNamePartType.Title),
                    new ENXP("Dr", EntityNamePartType.Title),
                    new ENXP("John", EntityNamePartType.Given),
                    new ENXP("Jacob", EntityNamePartType.Given),
                    new ENXP("Jingleheimer", EntityNamePartType.Family),
                    new ENXP("-", EntityNamePartType.Delimiter),
                    new ENXP("Schmidt", EntityNamePartType.Family),
                    new ENXP("III", EntityNamePartType.Suffix)
                }
            );
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" use=""L""><part type=""TITLE"" value=""Sir""/><part type=""TITLE"" value=""Dr""/><part type=""GIV"" value=""John""/><part type=""GIV"" value=""Jacob""/><part type=""FAM"" value=""Jingleheimer""/><part type=""DEL"" value=""-""/><part type=""FAM"" value=""Schmidt""/><part qualifier=""SFX"" value=""III""/></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// An EN instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2ENQualifierSerializationTest()
        {
            EN inti = new EN(
                EntityNameUse.Legal,
                new ENXP[] {
                    new ENXP("Sir", EntityNamePartType.Prefix) { Qualifier = new SET<CS<EntityNamePartQualifier>>(EntityNamePartQualifier.Nobility) },
                    new ENXP("Dr", EntityNamePartType.Prefix) { Qualifier = new SET<CS<EntityNamePartQualifier>>(EntityNamePartQualifier.Academic) },
                    new ENXP("John", EntityNamePartType.Given),
                    new ENXP("Jacob", EntityNamePartType.Given),
                    new ENXP("Jingleheimer", EntityNamePartType.Family),
                    new ENXP("-", EntityNamePartType.Delimiter),
                    new ENXP("Schmidt", EntityNamePartType.Family),
                    new ENXP("III", EntityNamePartType.Suffix)
                }
            );
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" use=""L""><part type=""TITLE"" value=""Sir"" qualifier=""NB PFX""/><part type=""TITLE"" value=""Dr"" qualifier=""AC PFX""/><part type=""GIV"" value=""John""/><part type=""GIV"" value=""Jacob""/><part type=""FAM"" value=""Jingleheimer""/><part type=""DEL"" value=""-""/><part type=""FAM"" value=""Schmidt""/><part qualifier=""SFX"" value=""III""/></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// An PN instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2PNQualifierSerializationTest()
        {
            PN inti = new PN(
                EntityNameUse.Legal,
                new ENXP[] {
                    new ENXP("Sir", EntityNamePartType.Prefix) { Qualifier = new SET<CS<EntityNamePartQualifier>>(EntityNamePartQualifier.Nobility) },
                    new ENXP("Dr", EntityNamePartType.Prefix) { Qualifier = new SET<CS<EntityNamePartQualifier>>(EntityNamePartQualifier.Academic) },
                    new ENXP("John", EntityNamePartType.Given),
                    new ENXP("Jacob", EntityNamePartType.Given),
                    new ENXP("Jingleheimer", EntityNamePartType.Family),
                    new ENXP("-", EntityNamePartType.Delimiter),
                    new ENXP("Schmidt", EntityNamePartType.Family),
                    new ENXP("III", EntityNamePartType.Suffix)
                }
            );
            inti.Use.Add(EntityNameUse.OfficialRecord);
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" use=""L OR"" flavorId=""EN.PN""><part type=""TITLE"" value=""Sir"" qualifier=""NB PFX""/><part type=""TITLE"" value=""Dr"" qualifier=""AC PFX""/><part type=""GIV"" value=""John""/><part type=""GIV"" value=""Jacob""/><part type=""FAM"" value=""Jingleheimer""/><part type=""DEL"" value=""-""/><part type=""FAM"" value=""Schmidt""/><part qualifier=""SFX"" value=""III""/></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// An TN instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2TNQualifierSerializationTest()
        {
            TN inti = new TN("Sir Dr. John Jacob Jingleheimer-Schmidt III");
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" flavorId=""EN.TN""><part value=""Sir Dr. John Jacob Jingleheimer-Schmidt III""/></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// An ON instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2ONQualifierSerializationTest()
        {
            ON inti = new ON();
            inti.Part.Add(new ENXP("Health Level Seven, "));
            inti.Part.Add(new ENXP("Inc.", EntityNamePartType.Suffix) { Qualifier = SET<CS<EntityNamePartQualifier>>.CreateSET(EntityNamePartQualifier.LegalStatus) });
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" flavorId=""EN.ON""><part value=""Health Level Seven, ""/><part type=""TITLE"" value=""Inc."" qualifier=""LS SFX""/></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// An EN instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2ENBasicParseTest()
        {
            EN inti = new EN(
                EntityNameUse.Legal,
                new ENXP[] {
                    new ENXP("Sir", EntityNamePartType.Title),
                    new ENXP("Dr", EntityNamePartType.Title),
                    new ENXP("John", EntityNamePartType.Given),
                    new ENXP("Jacob", EntityNamePartType.Given),
                    new ENXP("Jingleheimer", EntityNamePartType.Family),
                    new ENXP("-", EntityNamePartType.Delimiter),
                    new ENXP("Schmidt", EntityNamePartType.Family),
                    new ENXP("III", EntityNamePartType.Title)
                }
            );
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<EN>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// An EN instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2ENQualifierParseTest()
        {
            EN inti = new EN(
                EntityNameUse.Legal,
                new ENXP[] {
                    new ENXP("Sir", EntityNamePartType.Title) { Qualifier = new SET<CS<EntityNamePartQualifier>>(EntityNamePartQualifier.Nobility) },
                    new ENXP("Dr", EntityNamePartType.Title) { Qualifier = new SET<CS<EntityNamePartQualifier>>(EntityNamePartQualifier.Academic) },
                    new ENXP("John", EntityNamePartType.Given),
                    new ENXP("Jacob", EntityNamePartType.Given),
                    new ENXP("Jingleheimer", EntityNamePartType.Family),
                    new ENXP("-", EntityNamePartType.Delimiter),
                    new ENXP("Schmidt", EntityNamePartType.Family),
                    new ENXP("III", EntityNamePartType.Title)
                }
            );
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<EN>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// An PN instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2PNQualifierParseTest()
        {
            PN inti = new PN(
                EntityNameUse.Legal,
                new ENXP[] {
                    new ENXP("Sir", EntityNamePartType.Title) { Qualifier = new SET<CS<EntityNamePartQualifier>>(new EntityNamePartQualifier[] { EntityNamePartQualifier.Nobility, EntityNamePartQualifier.Acquired }) },
                    new ENXP("Dr", EntityNamePartType.Title) { Qualifier = new SET<CS<EntityNamePartQualifier>>(EntityNamePartQualifier.Academic) },
                    new ENXP("John", EntityNamePartType.Given),
                    new ENXP("Jacob", EntityNamePartType.Given),
                    new ENXP("Jingleheimer", EntityNamePartType.Family),
                    new ENXP("-", EntityNamePartType.Delimiter),
                    new ENXP("Schmidt", EntityNamePartType.Family),
                    new ENXP("III")
                }
            ) { Flavor = "EN.PN" };
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<PN>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// An TN instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2TNQualifierParseTest()
        {
            TN inti = new TN("Sir Dr. John Jacob Jingleheimer-Schmidt III") { Flavor = "EN.TN" };
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<TN>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// An ON instance with basic properties
        /// </summary>
        [TestMethod]
        public void R2ONQualifierParseTest()
        {
            ON inti = new ON() { Flavor = "EN.ON" };
            inti.Part.Add(new ENXP("Health Level Seven, "));
            inti.Part.Add(new ENXP("Inc.", EntityNamePartType.Title) { Qualifier = SET<CS<EntityNamePartQualifier>>.CreateSET(EntityNamePartQualifier.LegalStatus) });
            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<ON>(actualXml);
            Assert.AreEqual(inti, int2);
        }

    }
}
