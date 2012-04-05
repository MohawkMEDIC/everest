using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// R2 Formatter test of the SC data type
    /// </summary>
    [TestClass]
    public class SCTest
    {
        public SCTest()
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
        public void R2SCBasicSerializationTest()
        {
            SC pregnancy = "Patient is 6 months pregnant";
            pregnancy.Code = new CD<string>("Z33.1", "2.16.840.1.113883.6.90")
            {
                DisplayName = "Pregnancy State, Incidental"
            };
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""Patient is 6 months pregnant"" language=""en-US""><code code=""Z33.1"" codeSystem=""2.16.840.1.113883.6.90""><displayName value=""Pregnancy State, Incidental"" language=""en-US""/></code></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(pregnancy);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        [TestMethod]
        public void R2SCCrazyComplexSerializationTest()
        {

            // The textual content
            SC crazySc = new SC("Burn", "en-CA");
            crazySc.Code = new CD<String>()
            {
                Code = "284196006",
                CodeSystem = "2.16.840.1.113883.6.96",
                CodeSystemName = "SNOMED CT",
                DisplayName = "Burn of skin",
                NullFlavor = null,
                Translation = new SET<CD<string>>(),
                Qualifier = new LIST<CR<string>>()
            };
            crazySc.Code.Translation.Add(
                new CD<string>()
                {
                    Code = "15376812",
                    CodeSystem = "2.16.840.1.113883.3.232.99.1",
                    CodeSystemName = "3M HDD",
                    DisplayName = "BurnOfSkinSCT",
                    NullFlavor = null,
                    Translation = new SET<CD<string>>(
                        new CD<string>()
                        {
                            Code = "284196006",
                            CodeSystem = "2.16.840.1.113883.6.96",
                            CodeSystemName = "SNOMED CT",
                            DisplayName = "Burn of skin"
                        }, CD<String>.Comparator),
                    Qualifier = new LIST<CR<string>>()
                });
            crazySc.Translation = new SET<ST>();
            crazySc.Translation.Add(new ST("Quemar", "es"));
            crazySc.Translation.Add(new ST("Brûlure", "fr"));
            CR<String> severity = new CR<String>(new CV<String>(), new CD<String>());
            crazySc.Code.Qualifier.Add(severity);
            severity.Name.Code = "246112005";
            severity.Name.CodeSystem = "2.16.840.1.113883.6.96";
            severity.Name.CodeSystemName = "SNOMED CT";
            severity.Name.DisplayName = "Severity";
            severity.Value.Code = "24484000";
            severity.Value.CodeSystem = "2.16.840.1.113883.6.96";
            severity.Value.CodeSystemName = "SNOMED CT";
            severity.Value.DisplayName = "Severe";
            CR<String> findingsite = new CR<String>();
            findingsite.Name = new CD<string>();
            findingsite.Value = new CD<string>();
            crazySc.Code.Qualifier.Add(findingsite);
            findingsite.Name.Code = "363698007";
            findingsite.Name.CodeSystem = "2.16.840.1.113883.6.96";
            findingsite.Name.CodeSystemName = "SNOMED CT";
            findingsite.Name.DisplayName = "Finding site";
            findingsite.Value.Code = "113185004";
            findingsite.Value.CodeSystem = "2.16.840.1.113883.6.96";
            findingsite.Value.CodeSystemName = "SNOMED CT";
            findingsite.Value.DisplayName = "Skin between Fourth and Fifth Toes";
            CR<String> laterality = new CR<String>();
            laterality.Name = new CD<string>();
            laterality.Value = new CD<string>();
            findingsite.Value.Qualifier = new LIST<CR<string>>();
            findingsite.Value.Qualifier.Add(laterality);
            laterality.Name.Code = "272741003";
            laterality.Name.CodeSystem = "2.16.840.1.113883.6.96";
            laterality.Name.CodeSystemName = "SNOMED CT";
            laterality.Name.DisplayName = "Laterality";
            laterality.Value.Code = "7771000";
            laterality.Value.CodeSystem = "2.16.840.1.113883.6.96";
            laterality.Value.CodeSystemName = "SNOMED CT";
            laterality.Value.DisplayName = "Left";

            string expectedXml = @"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" value=""Burn"" language=""en-CA"" xmlns=""urn:hl7-org:v3""><translation value=""Quemar"" language=""es"" /><translation value=""Brûlure"" language=""fr"" /><code code=""284196006:{246112005=24484000,363698007=(113185004:272741003=7771000)}"" codeSystem=""2.16.840.1.113883.6.96"" codeSystemName=""SNOMED CT""><translation code=""15376812"" codeSystem=""2.16.840.1.113883.3.232.99.1"" codeSystemName=""3M HDD""><displayName value=""BurnOfSkinSCT"" language=""en-US"" /><translation code=""284196006"" codeSystem=""2.16.840.1.113883.6.96"" codeSystemName=""SNOMED CT""><displayName value=""Burn of skin"" language=""en-US"" /></translation></translation></code></test>";
            string actualXml = R2SerializationHelper.SerializeAsString(crazySc);

            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);

        }
    }
}
