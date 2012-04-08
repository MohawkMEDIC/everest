/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 * User: Justin Fyfe
 * Date: 07-28-2011
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using System.Globalization;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Test the serialization of ED with DT R2
    /// </summary>
    [TestClass]
    public class EDTest
    {
        public EDTest()
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
        /// Test serialization of an ED that contains text
        /// </summary>
        [TestMethod]
        public void R2EDContentTextSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""this is my test""/>";
            String actualXml = R2SerializationHelper.SerializeAsString(new ED("this is my test"));
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// TEst the serialization of an ED that contains Text with a flavor
        /// </summary>
        [TestMethod]
        public void R2EDContentTextFlavorSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" value=""this is my test"" flavorId=""ED.TEXT""/>";
            String actualXml = R2SerializationHelper.SerializeAsString(new ED("this is my test") { Flavor = "ED.TEXT" });
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test the serialization of an ED that contains Language with serialization
        /// </summary>
        [TestMethod]
        public void R2EDContentTextLanguageSerializationTest()
        {
            String expectedXml = String.Format(@"<test xmlns=""urn:hl7-org:v3"" language=""{0}"" value=""this is my test"" flavorId=""ED.TEXT""/>", CultureInfo.CurrentCulture.Name);
            String actualXml = R2SerializationHelper.SerializeAsString(new ED("this is my test", CultureInfo.CurrentCulture.Name) { Flavor = "ED.TEXT" });
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Test the serialization of an ED with compression
        /// </summary>
        [TestMethod]
        public void R2EDContentTextCompressionSerializationTest()
        {
            ED edInstance = new ED("this is my test");
            String expectedXml = String.Format(@"<test xmlns=""urn:hl7-org:v3"" mediaType=""text/plain"" compression=""GZ""><data>{1}</data></test>", CultureInfo.CurrentCulture.Name, edInstance.Compress(EncapsulatedDataCompression.GZ).Base64Data);
            edInstance = edInstance.Compress(EncapsulatedDataCompression.GZ);
            String actualXml = R2SerializationHelper.SerializeAsString(edInstance);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test serialization of binary content
        /// </summary>
        [TestMethod]
        public void R2EDContentBinSerializationTest()
        {
            String expectedXml = String.Format(@"<test xmlns=""urn:hl7-org:v3"" mediaType=""bin/plain""><data>{0}</data></test>", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("This is binary")));
            String actualXml = R2SerializationHelper.SerializeAsString(new ED(System.Text.Encoding.UTF8.GetBytes("This is binary"), "bin/plain"));
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test the serialization of ED content with thumbnail
        /// </summary>
        [TestMethod]
        public void R2EDContentBinThumbnailSerializationTest()
        {
            ED edInstance = new ED(Encoding.UTF8.GetBytes("this is a binary with thumbnail"), "text/plain");
            edInstance.Thumbnail = edInstance.Compress(EncapsulatedDataCompression.GZ);
            String expectedXml = String.Format(@"<test xmlns=""urn:hl7-org:v3"" mediaType=""text/plain""><data>{0}</data><thumbnail mediaType=""text/plain"" compression=""GZ""><data>{1}</data></thumbnail></test>", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("this is a binary with thumbnail")), edInstance.Thumbnail.Base64Data);
            String actualXml = R2SerializationHelper.SerializeAsString(edInstance);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test the serialization of an ED with translations
        /// </summary>
        [TestMethod]
        public void R2EDContentBinThumbnailTranslationSerializationTest()
        {
            ED edInstance = new ED(Encoding.UTF8.GetBytes("this is a binary with thumbnail and translation"), "text/plain");
            edInstance.Thumbnail = edInstance.Compress(EncapsulatedDataCompression.GZ);
            edInstance.Translation = new SET<ED>(
                new ED[] {
                    "this is translation 1",
                    "this is translation 2"
                }
            );
            String expectedXml = String.Format(@"<test xmlns=""urn:hl7-org:v3"" mediaType=""text/plain""><data>{0}</data><thumbnail mediaType=""text/plain"" compression=""GZ""><data>{1}</data></thumbnail><translation value=""this is translation 1"" language=""en-US""/><translation language=""en-US"" value=""this is translation 2""/></test>", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("this is a binary with thumbnail and translation")), edInstance.Thumbnail.Base64Data);
            String actualXml = R2SerializationHelper.SerializeAsString(edInstance);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test the serialization of an ED with translations and integrity check
        /// </summary>
        [TestMethod]
        public void R2EDContentBinThumbnailTranslationSerializationIntegrityCheckTest()
        {
            ED edInstance = new ED(Encoding.UTF8.GetBytes("this is a binary with thumbnail and translation"), "text/plain");
            edInstance.Thumbnail = edInstance.Compress(EncapsulatedDataCompression.GZ);
            edInstance.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA256;
            edInstance.IntegrityCheck = edInstance.ComputeIntegrityCheck();
            edInstance.Translation = new SET<ED>(
                new ED[] {
                    "this is translation 1",
                    "this is translation 2"
                }
            );
            String expectedXml = String.Format(@"<test xmlns=""urn:hl7-org:v3"" mediaType=""text/plain"" integrityCheckAlgorithm=""SHA-256""><data>{0}</data><integrityCheck>2sv8IRiWNYypBMLqJxAl79Yl2YJGBz3dtG3f3aNIIEg=</integrityCheck><thumbnail mediaType=""text/plain"" compression=""GZ""><data>{1}</data></thumbnail><translation value=""this is translation 1"" language=""en-US""/><translation language=""en-US"" value=""this is translation 2""/></test>", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("this is a binary with thumbnail and translation")), edInstance.Thumbnail.Base64Data);
            String actualXml = R2SerializationHelper.SerializeAsString(edInstance);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test the serialization of an ED with a flavor
        /// </summary>
        [TestMethod]
        public void R2EDContentBinFlavorSerializationTest()
        {
            String expectedXml = String.Format(@"<test xmlns=""urn:hl7-org:v3"" flavorId=""ED.DOC"" mediaType=""bin/plain""><data>{0}</data></test>", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("This is binary")));
            String actualXml = R2SerializationHelper.SerializeAsString(new ED(System.Text.Encoding.UTF8.GetBytes("This is binary"), "bin/plain") { Flavor = "ED.DOC" });
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }
        /// <summary>
        /// Test the serialization of an ED with a flavor and binary content and a null flavor
        /// </summary>
        [TestMethod]
        public void R2EDContentNullFlavorBinFlavorSerializationTest()
        {
            String expectedXml = @"<test xmlns=""urn:hl7-org:v3"" nullFlavor=""NI""/>";
            String actualXml = R2SerializationHelper.SerializeAsString(new ED(System.Text.Encoding.UTF8.GetBytes("This is binary"), "bin/plain") { Flavor = "ED.DOC", NullFlavor = NullFlavor.NoInformation });
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }




        /// <summary>
        /// Test deserializtion of an ED that contains text
        /// </summary>
        [TestMethod]
        public void R2EDContentTextParseTest()
        {
            var ved = new ED("this is my test");
            String actualXml = R2SerializationHelper.SerializeAsString(ved);
            var ved2 = R2SerializationHelper.ParseString<ED>(actualXml);
            Assert.AreEqual(ved, ved2);
        }

        /// <summary>
        /// TEst the serialization of an ED that contains Text with a flavor
        /// </summary>
        [TestMethod]
        public void R2EDContentTextFlavorParseTest()
        {
            var ved = new ED("this is my test") { Flavor = "ED.TEXT" };
            String actualXml = R2SerializationHelper.SerializeAsString(ved);
            var ved2 = R2SerializationHelper.ParseString<ED>(actualXml);
            Assert.AreEqual(ved, ved2);
        }
        /// <summary>
        /// Test the serialization of an ED that contains Language with serialization
        /// </summary>
        [TestMethod]
        public void R2EDContentTextLanguageParseTest()
        {
            var ved = new ED("this is my test") { Flavor = "ED.TEXT" };
            String actualXml = R2SerializationHelper.SerializeAsString(ved);
            var ved2 = R2SerializationHelper.ParseString<ED>(actualXml);
            Assert.AreEqual(ved, ved2);
        }

        /// <summary>
        /// Test the serialization of an ED with compression
        /// </summary>
        [TestMethod]
        public void R2EDContentTextCompressionParseTest()
        {
            ED ved = new ED("this is my test");
            ved = ved.Compress(EncapsulatedDataCompression.GZ);
            String actualXml = R2SerializationHelper.SerializeAsString(ved);
            var ved2 = R2SerializationHelper.ParseString<ED>(actualXml);
            Assert.AreEqual(ved, ved2);
        }
        /// <summary>
        /// Test serialization of binary content
        /// </summary>
        [TestMethod]
        public void R2EDContentBinParseTest()
        {
            var ved = new ED(System.Text.Encoding.UTF8.GetBytes("This is binary"), "bin/plain");
            String actualXml = R2SerializationHelper.SerializeAsString(ved);
            var ved2 = R2SerializationHelper.ParseString<ED>(actualXml);
            Assert.AreEqual(ved, ved2);
        }
        /// <summary>
        /// Test the serialization of ED content with thumbnail
        /// </summary>
        [TestMethod]
        public void R2EDContentBinThumbnailParseTest()
        {
            ED ved = new ED(Encoding.UTF8.GetBytes("this is a binary with thumbnail"), "text/plain");
            ved.Thumbnail = ved.Compress(EncapsulatedDataCompression.GZ);
            String actualXml = R2SerializationHelper.SerializeAsString(ved);
            var ved2 = R2SerializationHelper.ParseString<ED>(actualXml);
            Assert.AreEqual(ved, ved2);

        }
        /// <summary>
        /// Test the serialization of an ED with translations
        /// </summary>
        [TestMethod]
        public void R2EDContentBinThumbnailTranslationParseTest()
        {
            ED ved = new ED(Encoding.UTF8.GetBytes("this is a binary with thumbnail and translation"), "text/plain");
            ved.Thumbnail = ved.Compress(EncapsulatedDataCompression.GZ);
            ved.Translation = new SET<ED>(
                new ED[] {
                    "this is translation 1",
                    "this is translation 2"
                }
            );
            String actualXml = R2SerializationHelper.SerializeAsString(ved);
            var ved2 = R2SerializationHelper.ParseString<ED>(actualXml);
            Assert.AreEqual(ved, ved2);

        }
        /// <summary>
        /// Test the serialization of an ED with a flavor
        /// </summary>
        [TestMethod]
        public void R2EDContentBinFlavorParseTest()
        {
            var ved = new ED(System.Text.Encoding.UTF8.GetBytes("This is binary"), "bin/plain") { Flavor = "ED.DOC" };
            String actualXml = R2SerializationHelper.SerializeAsString(ved);
            var ved2 = R2SerializationHelper.ParseString<ED>(actualXml);
            Assert.AreEqual(ved, ved2);

        }

    }
}
