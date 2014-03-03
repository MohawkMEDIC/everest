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

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for ADXPTest
    /// </summary>
    [TestClass]
    public class ADXPTest
    {
        public ADXPTest()
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
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value               :   The actual string value of the part
        /// And, the following values are nullified. 
        ///     Type                :   The address part type
        ///     Code                :   A code assigned to the part by some coding system if appropriate
        ///     CodeSystem          :   The code system from which the code is taken
        ///     CodeSystemVersion   :   The version of the coding system if required
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADXPValueTest()
        {
            ADXP adxp = new ADXP();
            adxp.Value = "Port Dover";
            adxp.Type = null;
            adxp.Code = null;
            adxp.CodeSystem = null;
            adxp.CodeSystemVersion = null;
            adxp.NullFlavor = null;
            Assert.IsTrue(adxp.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value               :   The actual string value of the part
        ///     Type                :   The address part type
        /// And, the following values are nullified. 
        ///     Code                :   A code assigned to the part by some coding system if appropriate
        ///     CodeSystem          :   The code system from which the code is taken
        ///     CodeSystemVersion   :   The version of the coding system if required
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADXPValueTypeTest()
        {
            ADXP adxp = new ADXP();
            adxp.Value = "Port Dover";
            adxp.Type = AddressPartType.City;
            adxp.Code = null;
            adxp.CodeSystem = null;
            adxp.CodeSystemVersion = null;
            adxp.NullFlavor = null;
            Assert.IsTrue(adxp.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following values are nullified. 
        ///     Value               :   The actual string value of the part
        ///     Type                :   The address part type
        ///     Code                :   A code assigned to the part by some coding system if appropriate
        ///     CodeSystem          :   The code system from which the code is taken
        ///     CodeSystemVersion   :   The version of the coding system if required
        /// </summary>
        [TestMethod]
        public void ADXPNullFlavorTest()
        {
            ADXP adxp = new ADXP();
            adxp.Value = null;
            adxp.Type = null;
            adxp.Code = null;
            adxp.CodeSystem = null;
            adxp.CodeSystemVersion = null;
            adxp.NullFlavor = NullFlavor.NotApplicable;
            Assert.IsTrue(adxp.Validate());
        }

        /// <summary>
        /// Ensures that validation fails (return FALSE)
        /// When there are no values being populated:
        /// And, the following values are nullified. 
        ///     Value               :   The actual string value of the part
        ///     Type                :   The address part type
        ///     Code                :   A code assigned to the part by some coding system if appropriate
        ///     CodeSystem          :   The code system from which the code is taken
        ///     CodeSystemVersion   :   The version of the coding system if required
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADXPNullTest()
        {
            ADXP adxp = new ADXP();
            adxp.Value = null;
            adxp.Type = null;
            adxp.Code = null;
            adxp.CodeSystem = null;
            adxp.CodeSystemVersion = null;
            adxp.NullFlavor = null;
            Assert.IsFalse(adxp.Validate());
        }

        /// <summary>
        /// Ensures that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Type                :   The address part type
        /// And, the following values are nullified. 
        ///     Value               :   The actual string value of the part
        ///     Code                :   A code assigned to the part by some coding system if appropriate
        ///     CodeSystem          :   The code system from which the code is taken
        ///     CodeSystemVersion   :   The version of the coding system if required
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADXPTypeTest()
        {
            ADXP adxp = new ADXP();
            adxp.Value = null;
            adxp.Type = AddressPartType.City;
            adxp.Code = null;
            adxp.CodeSystem = null;
            adxp.CodeSystemVersion = null;
            adxp.NullFlavor = null;
            Assert.IsFalse(adxp.Validate());
        }

        /// <summary>
        /// Ensures that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Value               :   The actual string value of the part
        ///     Type                :   The address part type
        ///     Code                :   A code assigned to the part by some coding system if appropriate
        /// And, the following values are nullified. 
        ///     CodeSystem          :   The code system from which the code is taken
        ///     CodeSystemVersion   :   The version of the coding system if required
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADXPValueTypeCodeTest()
        {
            ADXP adxp = new ADXP();
            adxp.Value = "Port Dover";
            adxp.Type = AddressPartType.City;
            adxp.Code = "23.2483.122";
            adxp.CodeSystem = null;
            adxp.CodeSystemVersion = null;
            adxp.NullFlavor = null;
            Assert.IsFalse(adxp.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value               :   The actual string value of the part
        ///     Type                :   The address part type
        ///     Code                :   A code assigned to the part by some coding system if appropriate
        ///     CodeSystem          :   The code system from which the code is taken
        /// And, the following values are nullified. 
        ///     CodeSystemVersion   :   The version of the coding system if required
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADXPValueTypeCodeCodeSystemTest()
        {
            ADXP adxp = new ADXP();
            adxp.Value = "Port Dover";
            adxp.Type = AddressPartType.City;
            adxp.Code = "23.2487.35.12";
            adxp.CodeSystem = "something";
            adxp.CodeSystemVersion = null;
            adxp.NullFlavor = null;
            Assert.IsTrue(adxp.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value               :   The actual string value of the part
        ///     Type                :   The address part type
        ///     Code                :   A code assigned to the part by some coding system if appropriate
        ///     CodeSystem          :   The code system from which the code is taken
        ///     CodeSystemVersion   :   The version of the coding system if required
        /// And, the following values are nullified. 
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADXPValueTypeCodeCodeSystemCodeSystemVersionTest()
        {
            ADXP adxp = new ADXP();
            adxp.Value = "Port Dover";
            adxp.Type = AddressPartType.City;
            adxp.Code = "23.2487.35.12";
            adxp.CodeSystem = "something";
            adxp.CodeSystemVersion = "3";
            adxp.NullFlavor = null;
            Assert.IsTrue(adxp.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     A new instance of ADXP is being created
        /// And, the following values are nullified. 
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADXPValue2Test()
        {
            ADXP adxp = new ADXP("96 White Water Dr.");
            adxp.NullFlavor = null;
            Assert.IsTrue(adxp.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     A new instance of ADXP is being created
        /// And, the following values are nullified. 
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADXPValueType2Test()
        {
            ADXP adxp = new ADXP("Canada", AddressPartType.County);
            adxp.NullFlavor = null;
            Assert.IsTrue(adxp.Validate());
        }

        /// <summary>
        /// Tests casting a string to an ADXP object. 
        /// Giving it a string will assign the ADXP's value and AddressPartType. 
        /// 
        /// Expected result: ADXP object with a value of "Hamilton" and an AddressPartType of AddressLine.
        /// </summary>
        [TestMethod]
        public void ADXPCastStringToADXPTest()
        {
            ADXP StringToADXP = "Hamilton";

            Assert.AreEqual("Hamilton",StringToADXP.Value);
            Assert.AreEqual(null, StringToADXP.Type);
        }

        /// <summary>
        /// Tests casting an ADXP object to a string.
        /// Passing an ADXP object to a string should result in the string being set to the
        /// value of the ADXP object. 
        /// 
        /// Expected result: String with a value of "Hamilton"
        /// </summary>
        [TestMethod]
        public void ADXPCastADXPToStringTest()
        {
            ADXP ADXPToString = "Hamilton";
            String strOut = ADXPToString;

            Assert.AreEqual("Hamilton", strOut);
        }

        /// <summary>
        /// Tests the ADXP.ToString() method. 
        /// Should return a string representation of the ADXP object's value.
        /// 
        /// Expected Result: "Hamilton"
        /// </summary>
        [TestMethod]
        public void ADXPToStringMethodTest()
        {
            ADXP ADXPToStringMethod = "Hamilton";

            Assert.AreEqual("Hamilton", ADXPToStringMethod.ToString());
        }
    }
}
