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

namespace MARC.Everest.Test.Regressions
{
    /// <summary>
    /// Summary description for WI2102
    /// </summary>
    [TestClass]
    public class WI2102
    {
        public WI2102()
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
        /// Work item 2102 Validation : Graph to the wire
        /// </summary>
        [TestMethod]
        public void WI2102VerifyGraphFull()
        {
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><family partType=""FAM"">Smith</family><validTime><low value=""20090101""/><high value=""20100101""/></validTime></test>";
            EN instance = new EN();
            instance.Part.Add(new ENXP("Smith", EntityNamePartType.Family));
            instance.ValidTimeLow = (TS)"20090101";
            instance.ValidTimeHigh = (TS)"20100101";
            string actualXml = R1SerializationHelper.SerializeAsString(instance);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Work item 2102 validation : parse from the wire
        /// </summary>
        [TestMethod]
        public void WI2102VerifyParseFull()
        {
            EN instance = new EN();
            instance.Part.Add(new ENXP("Smith", EntityNamePartType.Family));
            instance.ValidTimeLow = (TS)"20090101";
            instance.ValidTimeHigh = (TS)"20100101";
            string actualXml = R1SerializationHelper.SerializeAsString(instance);
            var inti = R1SerializationHelper.ParseString<EN>(actualXml);
            Assert.AreEqual(instance, inti);
        }

        /// <summary>
        /// Work item 2102 Validation : Graph to the wire
        /// </summary>
        [TestMethod]
        public void WI2102VerifyGraphPart()
        {
            string expectedXml = @"<test xmlns=""urn:hl7-org:v3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><family partType=""FAM"">Smith</family><validTime><low value=""20090101""/></validTime></test>";
            EN instance = new EN();
            instance.Part.Add(new ENXP("Smith", EntityNamePartType.Family));
            instance.ValidTimeLow = (TS)"20090101";
            string actualXml = R1SerializationHelper.SerializeAsString(instance);
            R2SerializationHelper.XmlIsEquivalent(expectedXml, actualXml);
        }

        /// <summary>
        /// Work item 2102 validation : parse from the wire
        /// </summary>
        [TestMethod]
        public void WI2102VerifyParsePart()
        {
            EN instance = new EN();
            instance.Part.Add(new ENXP("Smith", EntityNamePartType.Family));
            instance.ValidTimeLow = (TS)"20090101";
            string actualXml = R1SerializationHelper.SerializeAsString(instance);
            var inti = R1SerializationHelper.ParseString<EN>(actualXml);
            Assert.AreEqual(instance, inti);
        }
    }
}
