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
using MARC.Everest.Xml;
using System.IO;
using System.Xml;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// A test that validates functionality of formatters 
    /// </summary>
    [TestClass]
    public class CDTest
    {
        public CDTest()
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
        /// Simple representations of a CV
        /// </summary>
        [TestMethod]
        public void R2CVOriginalTextNullFlavorParseTest()
        {
            CV<FamilyMember> cdi = new CV<FamilyMember>()
            {
                NullFlavor = NullFlavor.Other
            };
            cdi.CodeSystem = "2.16.840.1.113883.5.111";
            cdi.OriginalText = "Step-Brother in Law";

            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            var cd2 = R2SerializationHelper.ParseString<CV<FamilyMember>>(actualXml);
            Assert.AreEqual(cdi, cd2);
        }

        /// <summary>
        /// Simple representations of a CE
        /// </summary>
        [TestMethod]
        public void R2CEOriginalTextNullFlavorTranslationParseTest()
        {
            CE<String> cdi = new CE<string>();
            cdi.OriginalText = "Extraterrestrial Virus - Black Oil";
            cdi.NullFlavor = NullFlavor.Other;
            cdi.Translation = new SET<CD<string>>(
                 new CD<String>("X^0012", "2.16.3.234.34343.343")
            );


            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            var cd2 = R2SerializationHelper.ParseString<CE<String>>(actualXml);
            Assert.AreEqual(cdi, cd2);
        }
        /// <summary>
        /// Simple representations of a CD
        /// </summary>
        [TestMethod]
        public void R2CDSimpleCodeSerializationTest()
        {
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" code=""784.0"" codeSystem=""2.16.840.1.113883.6.42"" codeSystemName=""ICD-9""><displayName value=""Headache""/><originalText value=""gen. headache""/></test>";
            var cdi = new CD<String>("784.0", "2.16.840.1.113883.6.42", "ICD-9", null, new ST("Headache"), new ED("gen. headache"));
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// NullFlavor test
        /// </summary>
        [TestMethod]
        public void R2CDNullFlavorSerializationTest()
        {
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" nullFlavor=""OTH"" codeSystem=""2.16.840.1.113883.6.96""><originalText value=""Burnt ear with iron. Burnt other ear calling for ambulance""/> </test>";
            var cdi = new CD<String>(null, "2.16.840.1.113883.6.96", null, null, null, new ED("Burnt ear with iron. Burnt other ear calling for ambulance"));
            cdi.NullFlavor = NullFlavor.Other;
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);

        }

        /// <summary>
        /// Qualifier code
        /// </summary>
        [TestMethod]
        public void R2CDQualifierCodeSerializationTest()
        {
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" code=""128045006:363698007=56459004"" codeSystem=""2.16.840.1.113883.6.96"" codeSystemName=""Snomed-CT""><originalText value=""Cellulitis of the foot""/></test>";
            var cdi = new CD<String>("128045006", "2.16.840.1.113883.6.96", "Snomed-CT", null, new ST("Cellutitis (disorder)"), new ED("Cellulitis of the foot"));
            cdi.Qualifier = new LIST<CR<string>>();
            cdi.Qualifier.Add(new CR<string>(
                new CV<String>("363698007", "2.16.840.1.113883.6.96", "SNOMED-CT", null, "finding site", null),
                new CD<String>("56459004", "2.16.840.1.113883.6.96", "SNOMED-CT", null, "foot structure", null)
            ));
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Null flavor translation serialization test
        /// </summary>
        [TestMethod]
        public void R2CDNullFlavorTranslationSerializationTest()
        {
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" nullFlavor=""OTH"" codeSystem=""2.16.840.1.113883.6.96""><originalText value=""Burnt ear with iron. Burnt other ear calling for ambulance""/><translation code=""burn"" codeSystem=""2.16.840.1.113883.19.5.2""/></test>";
            var cdi = new CD<String>(null, "2.16.840.1.113883.6.96")
            {
                NullFlavor = NullFlavor.Other,
                OriginalText = new ED("Burnt ear with iron. Burnt other ear calling for ambulance"),
                Translation = SET<CD<String>>.CreateSET(
                    new CD<String>("burn", "2.16.840.1.113883.19.5.2")
                )
            };
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Translation on CE serialization test
        /// </summary>
        [TestMethod]
        public void R2CDCETranslationSerializationTest()
        {
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" flavorId=""CD.CE"" code=""232323232"" codeSystem=""2.16.840.1.113883.6.96""><originalText value=""Burnt ear with iron. Burnt other ear calling for ambulance""/><translation code=""burn"" codeSystem=""2.16.840.1.113883.19.5.2""/></test>";
            var cdi = new CE<String>("232323232", "2.16.840.1.113883.6.96")
            {
                OriginalText = new ED("Burnt ear with iron. Burnt other ear calling for ambulance"),
                Translation = SET<CD<String>>.CreateSET(
                    new CD<String>("burn", "2.16.840.1.113883.19.5.2")
                )
            };
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Translation on CE serialization test
        /// </summary>
        [TestMethod]
        public void R2CDCVSerializationTest()
        {
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" flavorId=""CD.CV"" code=""232323232"" codeSystem=""2.16.840.1.113883.6.96""><originalText value=""Burnt ear with iron. Burnt other ear calling for ambulance""/></test>";
            var cdi = new CV<String>("232323232", "2.16.840.1.113883.6.96")
            {
                OriginalText = new ED("Burnt ear with iron. Burnt other ear calling for ambulance")
            };
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Qualifier code
        /// </summary>
        [TestMethod]
        public void R2CDQualifierNestedCodeSerializationTest()
        {

            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" code=""64572001:{116676008=72704001,363698007=(12611008:272741003=7771000)}"" codeSystem=""2.16.840.1.113883.6.96"" codeSystemName=""Snomed-CT""><originalText value=""Some original text""/></test>";
            var cdi = new CD<String>("64572001", "2.16.840.1.113883.6.96", "Snomed-CT", null, new ST("disease"), new ED("Some original text"));
            cdi.Qualifier = new LIST<CR<string>>();
            cdi.Qualifier.Add(new CR<string>(
                new CV<String>("116676008", "2.16.840.1.113883.6.96", "SNOMED-CT", null, "associated morphology", null),
                new CD<String>("72704001", "2.16.840.1.113883.6.96", "SNOMED-CT", null, "fracture", null)
            ));
            cdi.Qualifier.Add(new CR<String>(
                new CV<String>("363698007", "2.16.840.1.113883.6.96", "SNOMED-CT", null, "finding site", null),
                new CD<String>("12611008", "2.16.840.1.113883.6.96", "SNOMED-CT", null, "bone structure of tibia", null)
                {
                    Qualifier = LIST<CR<String>>.CreateList(
                        new CR<String>(
                            new CV<String>("272741003","2.16.840.1.113883.6.96", "SNOMED-CT", null, "laterality", null),
                            new CD<String>("7771000","2.16.840.1.113883.6.96", "SNOMED-CT", null, "left", null)
                        )
                    )
                }
            ));
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }



        /// <summary>
        /// Simple representations of a CD
        /// </summary>
        [TestMethod]
        public void R2CDSimpleCodeParseTest()
        {
            var cdi = new CD<String>("784.0", "2.16.840.1.113883.6.42", "ICD-9", null, new ST("Headache"), new ED("gen. headache"));
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            var cd2 = R2SerializationHelper.ParseString<CD<String>>(actualXml);
            Assert.AreEqual(cdi, cd2);
        }

        /// <summary>
        /// NullFlavor test
        /// </summary>
        [TestMethod]
        public void R2CDNullFlavorParseTest()
        {
            var cdi = new CD<String>(null, "2.16.840.1.113883.6.96", null, null, null, new ED("Burnt ear with iron. Burnt other ear calling for ambulance"));
            cdi.NullFlavor = NullFlavor.Other;
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            var cd2 = R2SerializationHelper.ParseString<CD<String>>(actualXml);
            Assert.AreEqual(cdi, cd2);

        }

        /// <summary>
        /// Qualifier code
        /// </summary>
        [TestMethod]
        public void R2CDQualifierCodeParseTest()
        {
            var cdi = new CD<String>("128045006", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, new ED("Cellulitis of the foot"));
            cdi.Qualifier = new LIST<CR<string>>();
            cdi.Qualifier.Add(new CR<string>(
                new CV<String>("363698007", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, null),
                new CD<String>("56459004", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, null)
            ));
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            var cd2 = R2SerializationHelper.ParseString<CD<String>>(actualXml);
            Assert.AreEqual(cdi, cd2);

        }

        /// <summary>
        /// Translation on CE serialization test
        /// </summary>
        [TestMethod]
        public void R2CDCETranslationParseTest()
        {
            var cdi = new CE<String>("232323232", "2.16.840.1.113883.6.96")
            {
                OriginalText = new ED("Burnt ear with iron. Burnt other ear calling for ambulance"),
                Translation = SET<CD<String>>.CreateSET(
                    new CD<String>("burn", "2.16.840.1.113883.19.5.2")
                )
            };
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            var cd2 = R2SerializationHelper.ParseString<CE<String>>(actualXml);
            Assert.AreEqual(cdi, cd2);

        }

        /// <summary>
        /// Translation on CE serialization test
        /// </summary>
        [TestMethod]
        public void R2CDCVParseTest()
        {
            var cdi = new CV<String>("232323232", "2.16.840.1.113883.6.96")
            {
                OriginalText = new ED("Burnt ear with iron. Burnt other ear calling for ambulance")
            };
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            var cd2 = R2SerializationHelper.ParseString<CV<String>>(actualXml);
            Assert.AreEqual(cdi, cd2);

        }

        /// <summary>
        /// Qualifier code
        /// </summary>
        [TestMethod]
        public void R2CDQualifierNestedCodeParseTest()
        {

            var cdi = new CD<String>("64572001", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, new ED("Some original text"));
            cdi.Qualifier = new LIST<CR<string>>();
            cdi.Qualifier.Add(new CR<string>(
                new CV<String>("116676008", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, null),
                new CD<String>("72704001", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, null)
            ));
            cdi.Qualifier.Add(new CR<String>(
                new CV<String>("363698007", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, null),
                new CD<String>("12611008", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, null)
                {
                    Qualifier = LIST<CR<String>>.CreateList(
                        new CR<String>(
                            new CV<String>("272741003", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, null),
                            new CD<String>("7771000", "2.16.840.1.113883.6.96", "SNOMED-CT", null, null, null)
                        )
                    )
                }
            ));
            string actualXml = R2SerializationHelper.SerializeAsString(cdi);
            var cd2 = R2SerializationHelper.ParseString<CD<String>>(actualXml);
            Assert.AreEqual(cdi, cd2);

        }

    }
}
