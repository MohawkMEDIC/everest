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

namespace MARC.Everest.Test
{
    /// <summary>
    /// Summary description for MOTest
    /// </summary>
    [TestClass]
    public class MOTest
    {
        public MOTest()
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
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     Value       : Value
        ///     Currency    : The currency code as defined by ISO 4217
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void MOValueCurrencyNullNullFlavorTest()
        {
            MO mo = new MO();
            mo.NullFlavor = null;
            mo.Value = (decimal)2.34;
            mo.Currency = "cdn";
            Assert.IsTrue(mo.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When the following values are being populated:
        ///     Currency    : The currency code as defined by ISO 4217
        /// And, the following variables are being Nullified:
        ///     Value       : Value
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void MOCurrencyNullValueNullFlavorTest()
        {
            MO mo = new MO();
            mo.NullFlavor = null;
            mo.Value = null;
            mo.Currency = "cdn";
            Assert.IsFalse(mo.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When the following values are being populated:
        ///     Value       : Value
        /// And, the following variables are being Nullified:
        ///     Currency    : The currency code as defined by ISO 4217
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void MOValueNullNullFlavorCurrencyTest()
        {
            MO mo = new MO();
            mo.NullFlavor = null;
            mo.Currency = null;
            mo.Value = (decimal)2.45;
            Assert.IsFalse(mo.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        ///     Value       : Value
        ///     Currency    : The currency code as defined by ISO 4217
        /// </summary> 
        [TestMethod]
        public void MONullFlavorNullCurrencyValueTest()
        {
            MO mo = new MO();
            mo.Currency = null;
            mo.Value = null;
            mo.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(mo.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When the following values are being populated:
        ///     Value       : Value
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        ///     Currency    : The currency code as defined by ISO 4217
        /// </summary> 
        [TestMethod]
        public void MOValueCurrencyNullFlavorTest()
        {
            MO mo = new MO();
            mo.Value = (decimal)2.34;
            mo.Currency = "cdn";
            mo.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(mo.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When there are no values being populated:
        /// And, the following variables are being Nullified:
        ///     Currency    : The currency code as defined by ISO 4217
        ///     Value       : Value
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void MONullNullFlavorCurrencyValueTest()
        {
            MO mo = new MO();
            mo.Value = null;
            mo.Currency = null;
            mo.NullFlavor = null;
            Assert.IsFalse(mo.Validate());
        }

     
    }
}
