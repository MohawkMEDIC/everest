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

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Test for the II data type
    /// </summary>
    [TestClass]
    public class IITest
    {
        public IITest()
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
        /// Serialization test for the II data type
        /// </summary>
        [TestMethod]
        public void R2IIBasicSerializationTest()
        {
            string ext = Guid.NewGuid().ToString();
            II inti = new II("1.2.3.4.5", ext);
            string expectedXml = String.Format(@"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" root=""1.2.3.4.5"" extension=""{0}"" xmlns=""urn:hl7-org:v3""/>", ext);
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Serialization test for the II data type with reliability
        /// </summary>
        [TestMethod]
        public void R2IIReliabilitySerializationTest()
        {
            string ext = Guid.NewGuid().ToString();
            II inti = new II("1.2.3.4.5", ext);
            inti.Reliability = IdentifierReliability.IssuedBySystem;
            string expectedXml = String.Format(@"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" root=""1.2.3.4.5"" extension=""{0}"" reliability=""ISS"" xmlns=""urn:hl7-org:v3""/>", ext);
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Serialization test for the II data type with scope
        /// </summary>
        [TestMethod]
        public void R2IIScopeSerializationTest()
        {
            string ext = Guid.NewGuid().ToString();
            II inti = new II("1.2.3.4.5", ext);
            inti.Scope = IdentifierScope.ObjectIdentifier;
            string expectedXml = String.Format(@"<test xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" root=""1.2.3.4.5"" extension=""{0}"" scope=""OBJ"" xmlns=""urn:hl7-org:v3""/>", ext);
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Serialization test for the II data type with all properties populated
        /// </summary>
        [TestMethod]
        public void R2IIFullSerializationTest()
        {
            string ext = Guid.NewGuid().ToString();
            II inti = new II("1.2.3.4.5", ext);
            inti.Scope = IdentifierScope.ObjectIdentifier;
            inti.Reliability = IdentifierReliability.VerifiedBySystem;
            inti.IdentifierName = "This is an identifier";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<II>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Serialization test for the II data type
        /// </summary>
        [TestMethod]
        public void R2IIBasicParseTest()
        {
            string ext = Guid.NewGuid().ToString();
            II inti = new II("1.2.3.4.5", ext);
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<II>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Serialization test for the II data type with reliability
        /// </summary>
        [TestMethod]
        public void R2IIReliabilityParseTest()
        {
            string ext = Guid.NewGuid().ToString();
            II inti = new II("1.2.3.4.5", ext);
            inti.Reliability = IdentifierReliability.IssuedBySystem;
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<II>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Serialization test for the II data type with scope
        /// </summary>
        [TestMethod]
        public void R2IIScopeParseTest()
        {
            string ext = Guid.NewGuid().ToString();
            II inti = new II("1.2.3.4.5", ext);
            inti.Scope = IdentifierScope.ObjectIdentifier;
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<II>(actualXml);
            Assert.AreEqual(inti, int2);
        }

        /// <summary>
        /// Serialization test for the II data type with all properties populated
        /// </summary>
        [TestMethod]
        public void R2IIFullParseTest()
        {
            string ext = Guid.NewGuid().ToString();
            II inti = new II("1.2.3.4.5", ext);
            inti.Scope = IdentifierScope.ObjectIdentifier;
            inti.Reliability = IdentifierReliability.VerifiedBySystem;
            inti.IdentifierName = "This is an identifier";
            var actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<II>(actualXml);
            Assert.AreEqual(inti, int2);
        }
    }
}
