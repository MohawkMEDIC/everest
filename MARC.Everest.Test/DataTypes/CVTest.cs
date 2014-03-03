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
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for CVTest
    /// </summary>
    [TestClass]
    public class CVTest
    {
        public CVTest()
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
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And, the rest of the variables are Nullified:
        ///     Code            : The initial code
        ///     CodeSystem      : The code system the code was picked from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        /// </summary>
        [TestMethod]
        public void CVNullFlavorTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = null;
            cv.CodeSystem = null;
            cv.CodeSystemName = null;
            cv.DisplayName = null;
            cv.NullFlavor = NullFlavor.NoInformation;
            Assert.IsTrue(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem      : The code system the code was picked from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVCodeTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = "284196006";
            cv.CodeSystem = null;
            cv.CodeSystemName = null;
            cv.DisplayName = null;
            cv.NullFlavor = null;
            Assert.IsTrue(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        ///     CodeSystem      : The code system the code was picked from
        /// And, the rest of the variables are Nullified:
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVCodeCodeSystemTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = "284196006";
            cv.CodeSystem = "2.16.840.1.113883.6.96";
            cv.CodeSystemName = null;
            cv.DisplayName = null;
            cv.NullFlavor = null;
            Assert.IsTrue(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        ///     DisplayName     : The display name for the code
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem      : The code system the code was picked from
        ///     CodeSystemName  : The name of the code system 
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVCodeDisplayNameTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = "284196006";
            cv.CodeSystem = null;
            cv.CodeSystemName = null;
            cv.DisplayName = "Burn of skin";
            cv.NullFlavor = null;
            Assert.IsTrue(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        ///     CodeSystem      : The code system the code was picked from
        ///     CodeSystemName  : The name of the code system 
        /// And, the rest of the variables are Nullified:
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVCodeCodeSystemCodeSystemNameTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = "284196006";
            cv.CodeSystem = "2.16.840.1.113883.6.96";
            cv.CodeSystemName = "SNOMED CT";
            cv.DisplayName = null;
            cv.NullFlavor = null;
            Assert.IsTrue(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        ///     CodeSystem      : The code system the code was picked from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVCodeCodeSystemCodeSystemNameDisplayNameTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = "284196006";
            cv.CodeSystem = "2.16.840.1.113883.6.96";
            cv.CodeSystemName = "SNOMED CT";
            cv.DisplayName = "Burn of skin";
            cv.NullFlavor = null;
            Assert.IsTrue(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        ///     CodeSystem      : The code system the code was picked from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// And, there are no nullified variables.
        /// </summary>
        [TestMethod]
        public void CVCodeCodeSystemCodeSystemNameDisplayNameNullFlavorTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = "284196006";
            cv.CodeSystem = "2.16.840.1.113883.6.96";
            cv.CodeSystemName = "SNOMED CT";
            cv.DisplayName = "Burn of skin";
            cv.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     DisplayName     : The display name for the code
        /// And, the rest of the variables are Nullified:
        ///     Code            : The initial code
        ///     CodeSystem      : The code system the code was picked from
        ///     CodeSystemName  : The name of the code system 
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVDisplayNameTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = null;
            cv.CodeSystem = null;
            cv.CodeSystemName = null;
            cv.DisplayName = "Burn of skin";
            cv.NullFlavor = null;
            Assert.IsFalse(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     CodeSystem      : The code system the code was picked from
        /// And, the rest of the variables are Nullified:
        ///     Code            : The initial code
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVCodeSystemTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = null;
            cv.CodeSystem = "2.16.840.1.113883.6.96";
            cv.CodeSystemName = null;
            cv.DisplayName = null;
            cv.NullFlavor = null;
            Assert.IsFalse(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     CodeSystemName  : The name of the code system 
        /// And, the rest of the variables are Nullified:
        ///     Code            : The initial code
        ///     CodeSystem      : The code system the code was picked from
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVCodeSystemNameTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = null;
            cv.CodeSystem = null;
            cv.CodeSystemName = "SNOMED CT";
            cv.DisplayName = null;
            cv.NullFlavor = null;
            Assert.IsFalse(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        ///     CodeSystemName  : The name of the code system 
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem      : The code system the code was picked from
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVCodeSystemNameCodeTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = "284196006";
            cv.CodeSystem = null;
            cv.CodeSystemName = "SNOMED CT";
            cv.DisplayName = null;
            cv.NullFlavor = null;
            Assert.IsFalse(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// And, the rest of the variables are Nullified:
        ///     Code            : The initial code
        ///     CodeSystem      : The code system the code was picked from
        ///     CodeSystemName  : The name of the code system 
        /// </summary>
        [TestMethod]
        public void CVDisplayNameNullFlavorTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = null;
            cv.CodeSystem = null;
            cv.CodeSystemName = null;
            cv.DisplayName = "Burn of skin";
            cv.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     OriginalText     : The original text for the code
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CVOriginalTextNullFlavorTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = null;
            cv.CodeSystem = "2.3.2.3.2.32.23.23";
            cv.CodeSystemName = null;
            cv.OriginalText = "Burn of skin";
            cv.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(cv.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     CodeSystemName  : The name of the code system 
        ///     NullFlavor
        /// And, the rest of the variables are Nullified:
        ///     Code            : The initial code
        ///     CodeSystem      : The code system the code was picked from
        ///     DisplayName     : The display name for the code
        /// </summary>
        [TestMethod]
        public void CVCodeSystemNameNullFlavorTest()
        {
            CV<String> cv = new CV<String>();
            cv.Code = null;
            cv.CodeSystem = null;
            cv.CodeSystemName = "SNOMED CT";
            cv.DisplayName = null;
            cv.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(cv.Validate());
        }

        /// <summary>
        /// Casting String to CV
        /// </summary>
        [TestMethod]
        public void CVCastStringtoCVTest()
        {
                // "OTH” Create a String object
                String strInstance = "OTH";
                
                // Create a CV<String> object
                CV<String> cvInstance = new CV<String>();

                // Cast from String to CV
                cvInstance = strInstance;

                // True if the cast was successful
                Assert.IsTrue(cvInstance.Code == "OTH");
        }

        /// <summary>
        /// Casting CV<String> to String
        /// </summary>
        [TestMethod]
        public void CVCastCVToStringTest()
        {
                // CV.Code = “OTH”
                CV<String> cvInstance = new CV<String>();
                cvInstance.Code = "OTH";

                // true if Cast from CV<String> to String is successful
                Assert.AreEqual((String)cvInstance, cvInstance.Code.ToString());
        }

        /// <summary>
        /// We are going to Parse a CV<String> from CV<String>(T)
        /// </summary>
        [TestMethod]
        public void CVParseCVtoGenericTest()
        {
                // create an CV<NullFlavor> instance with the NullFlavor.Code = “NoInformation”
                CV<NullFlavor> cvInstance = new CV<NullFlavor>();
                cvInstance.Code = NullFlavor.NoInformation;
                
                // True if the Parse was successful
                Assert.AreEqual("NI", Util.ToWireFormat(cvInstance.Code));
        }

        /// <summary>
        /// Parse CV<String>(T) from CV
        /// </summary>
        [TestMethod]
        public void CVParseCVGenerictoCVTest()
        {
                // set CV.Code = "OTH"
                CV<String> cvInstance = new CV<String>();
                cvInstance.Code = "OTH";

                // True if the parse was successful
            var fromWire = Util.FromWireFormat(cvInstance, typeof(CV<NullFlavor>)) as CV<NullFlavor>;
                Assert.AreEqual(NullFlavor.Other, (NullFlavor)fromWire.Code);
        }
    }
}
