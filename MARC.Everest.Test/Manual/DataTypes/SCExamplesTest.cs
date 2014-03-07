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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for SCExamplesTest
    /// </summary>
    [TestClass]
    public class SCExamplesTest
    {
        public SCExamplesTest()
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

        /* Example 20 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   String value representing the data.
        ///     Code        :   Represents codified data associated with the data.
        /// And the following are Nullified:
        ///     NullFlavor
        ///     
        /// Testing for valid SC given string and code
        /// </summary>
        [TestMethod]
        public void SCExampleTest01()
        {
            SC pregnancy = "Patient is 6 months pregnant";
            pregnancy.Code = new CD<string>("Z33", "2.16.840.1.113883.6.90")
            {
                DisplayName = "Pregnancy State, Incidental"
            };
            pregnancy.NullFlavor = null;
            Assert.IsTrue(pregnancy.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   String value representing the data.
        /// And the following values are Nullified:
        ///     Code        :   Represents codified data associated with the data.
        ///     NullFlavor
        ///     
        /// Testing for valid SC given only string
        /// </summary>
        [TestMethod]
        public void SCExampleTest02()
        {
            SC pregnancy = "Patient is 6 months pregnant";
            pregnancy.Code = null;
            pregnancy.NullFlavor = null;
            Assert.IsTrue(pregnancy.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value       :   String value representing the data.
        ///     Code        :   Represents codified data associated with the data. 
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void SCExampleTest03()
        {
            SC pregnancy = "Patient is 6 months pregnant";
            pregnancy.Code = new CD<string>("Z33", "2.16.840.1.113883.6.90")
            {
                DisplayName = "Pregnancy State, Incidental"
            };
            pregnancy.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(pregnancy.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   String value representing the data.
        ///     Code        :   Represents codified data associated with the data.
        /// And the following values are nullified:
        ///     Translation 
        ///     NullFlavor
        ///     
        /// Testing if SC is a valid NTFalvor given no translation.
        /// </summary>
        [TestMethod]
        public void SCExampleTest04()
        {
            SC pregnancy = "Patient is 6 months pregnant";
            pregnancy.Code = new CD<string>("Z33", "2.16.840.1.113883.6.90")
            {
                DisplayName = "Pregnancy State, Incidental"
            };
            pregnancy.Translation = null;
            pregnancy.NullFlavor = null;
            Assert.IsTrue(SC.IsValidNtFlavor(pregnancy));
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value              :   String value representing the data.
        ///     Code               :   Represents codified data associated with the data.
        ///     Code Original Text
        /// And the following values are nullified:
        ///     Translation
        ///     NullFlavor
        ///     
        /// Testing if SC is valid given Code and Original Text
        /// </summary>
        [TestMethod]
        public void SCExampleTest05()
        {
            SC pregnancy = "Patient is 6 months pregnant";
            pregnancy.Code = new CD<string>("Z33", "2.16.840.1.113883.6.90")
            {
                DisplayName = "Pregnancy State, Incidental",
                OriginalText = "This is some original text"
            };
            pregnancy.Translation = null;
            pregnancy.NullFlavor = null;
            Assert.IsFalse(pregnancy.Validate());
        }
    }
}
