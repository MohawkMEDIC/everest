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
using MARC.Everest.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using MARC.Everest.DataTypes.Interfaces;
using System.Text;
using System;
using System.Xml;
namespace MARC.Everest.Test.DataTypes
{


    /// <summary>
    ///This is a test class for EDTest and is intended
    ///to contain all EDTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EDTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ED Constructor
        ///</summary>
        [TestMethod()]
        public void EDConstructorTest()
        {
            byte[] data = new byte[0]; // TODO: Initialize to an appropriate value
            string mediaType = "text/plain"; // TODO: Initialize to an appropriate value
            ED target = new ED(data, mediaType);
            Assert.AreEqual(target.Data, data);
            Assert.AreEqual(target.MediaType, "text/plain");
        }

        /// <summary>
        /// Ensures that Comparrison succeeds 
        /// </summary>       
        [TestMethod]
        public void EDCompressionTest()
        {
            byte[] filebytes = new byte[1024];
            RandomNumberGenerator random = new RNGCryptoServiceProvider();
            random.GetBytes(filebytes);
            ED t = new ED(filebytes, "application/bin");
            t.Compression = EncapsulatedDataCompression.GZ;
            t.Data = t.Compress(EncapsulatedDataCompression.GZ);
            t.Data = t.UnCompress();
            Assert.AreEqual(filebytes.Length, t.Data.Length);
            for (int i = 0; i < filebytes.Length; i++)
                Assert.AreEqual(filebytes[i], t.Data[i]);
        }

        /// <summary>
        /// Ensures that Validate succeeds when the hash that the data within this ED 
        /// is valid according to the set integrity check algorithm and integrity check
        /// </summary>
        [TestMethod]
        public void EDValidateHash()
        {
            byte[] filebyte = new byte[1024];
            ED edTest = new ED(filebyte, "application/pdf");
            edTest.Representation = EncapsulatedDataRepresentation.B64;
            edTest.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA1;
            edTest.IntegrityCheck = edTest.ComputeIntegrityCheck();
            Assert.IsTrue(edTest.ValidateIntegrityCheck());
        }

        /// <summary>
        /// Ensures that Comparrison succeeds 
        /// </summary>       
        [TestMethod]
        public void EDThumbnailCheck()
        {
            byte[] b = new byte[1024];
            RandomNumberGenerator random = new RNGCryptoServiceProvider();
            random.GetBytes(b);
            ED edt = new ED((TEL)"http://www.google.ca");
            edt.Thumbnail = new ED(b, "image/png");
            byte[] checker = edt.Thumbnail;
            Assert.AreEqual(checker.Length, b.Length);
            for (int i = 0; i < b.Length; i++)
                Assert.AreEqual(checker[i], b[i]);
        }

        /// <summary>
        /// Ensure that Validation succeeds when a NullFlavor and no data
        ///</summary>
        [TestMethod]
        public void EDNullFlavorValidationTest()
        {
            ED ed = new ED();
            ed.NullFlavor = NullFlavor.NotAsked;

            Assert.IsTrue(ed.Validate(), "Validation with a NullFlavor and no data failed. The validation should succeed.");
        }

        /// <summary>
        /// Ensures that Validation succeeds when a Value and no NullFlavor
        ///</summary>
        [TestMethod]
        public void EDValueValidationTest()
        {
            ED ed = new ED("This is the content of ED");
            ed.NullFlavor = null;
            ed.MediaType = "";
            Assert.IsTrue(ed.Validate(), "Validation with a Value and no NullFlavor failed. The validation should succeed.");

        }

        /// <summary>
        /// Ensures that Validation fails when both a value and NullFlavor being populated
        ///</summary>
        [TestMethod]
        public void EDValueNullFlavorTest()
        {
            ED ed = new ED("This is the content of ED");
            ed.NullFlavor = NullFlavor.UnEncoded;

            Assert.IsFalse(ed.Validate(), "Validation succeeded but should have failed with both a value and NullFlavor");
        }

        /// <summary>
        /// Ensures that Validation fails whenno value or NullFlavor are nullified
        ///</summary>
        [TestMethod]
        public void EDNullTest()
        {
            ED ed = new ED();
            ed.Value = string.Empty;
            ed.Data = null;

            Assert.IsFalse(ed.Validate(), "Validation succeeded but should have failed with no value or NullFlavor");
        }

        /// <summary>
        /// Tests casting the contents of ED t oa string.
        /// </summary>
        [TestMethod]
        public void EDSetStringToContentsOfEDTest()
        {
            string content = new ED("Test");
            Assert.AreEqual("Test", content);
        }

        /// <summary>
        /// Tests assigning the value of ED to a byte array.
        /// </summary>
        [TestMethod]
        public void EDByteSetToContentOfEDTest()
        {
            byte[] content = new ED("Test");
            byte[] expected = Encoding.ASCII.GetBytes("Test");
            Boolean passed = true;
            for (int i = 0; i < expected.Length; i++)
                if (content[i] != expected[i])
                    passed = false;
            Assert.AreEqual(true, passed && content.Length == expected.Length);
        }

        /// <summary>
        /// Tests casting a string into the contents of ED.
        /// </summary>
        [TestMethod]
        public void EDContentsOfEDSetToStringTest()
        {
            ED edInstance = "Test";
            byte[] expected = Encoding.ASCII.GetBytes("Test");
            Boolean passed = true;
            for (int i = 0; i < expected.Length; i++)
                if (edInstance.Data[i] != expected[i])
                    passed = false;
            Assert.AreEqual(true, passed && edInstance.Data.Length == expected.Length);
        }

        /// <summary>
        /// Base64EncodedED
        /// </summary>
        [TestMethod]
        public void EDBase64EncodedEDTest()
        {
            string lazyString = "The quick brown fox jumps over the lazy dog";

            // Create and ED object with the value equal to the lazyString above.
            ED edInstance = new ED();
            edInstance.Value = lazyString;
            
            // Ste the Representation of the ED object to Base64 Encoded
            edInstance.Representation = EncapsulatedDataRepresentation.B64;
            
            // Converts string to Bytes
            Byte[] testBytes = System.Text.Encoding.UTF8.GetBytes(lazyString);

            // True if we can convert the testBytes to a Base64 String.
            Assert.AreEqual(edInstance.Base64Data, Convert.ToBase64String(testBytes));
        }

        /// <summary>
        /// Xml Encoded Data
        /// </summary>
        [TestMethod]
        public void EDXmlEncodedDataTest()
        {
            string testString = "<test />";

            // create instance and set the value
            ED edInstance = new ED();
            edInstance.Value = testString;

            // Ste the Representation of the ED object to XML
            edInstance.Representation = EncapsulatedDataRepresentation.XML;

            // Checks to see if there is one and only one Root Element
            XmlNode parentNode = edInstance.XmlData.ParentNode;
            XmlNodeList nodeList = edInstance.XmlData.ParentNode.ChildNodes; //Parent is the document itself
            Assert.IsTrue(parentNode.NodeType == XmlNodeType.Document &&  nodeList.Count == 1 && 
            nodeList[0].LocalName == "test");
        }


        /// <summary>
        /// Parse empty ED
        /// </summary>
        [TestMethod]
        public void EDParseEmptyContentTest()
        {
            string testString = "<hl7:code xmlns:hl7=\"urn:hl7-org:v3\" code=\"11503-0\" codeSystem=\"2.16.840.1.113883.6.1\" codeSystemName=\"LOINC\" codeSystemVersion=\"2.44\" displayName=\"Medical Records\"><hl7:originalText representation=\"TXT\" mediaType=\"text/plain\" /></hl7:code>";
            var cd = R1SerializationHelper.ParseString<CD<String>>(testString);
            Assert.IsNull(cd.OriginalText.Value, "Value is not null");
        }
    }
}
