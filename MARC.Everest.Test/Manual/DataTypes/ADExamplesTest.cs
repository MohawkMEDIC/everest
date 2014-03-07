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
    /// Summary description for ADExamplesTest
    /// </summary>
    [TestClass]
    public class ADExamplesTest
    {
        public ADExamplesTest()
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

        /* Example 36 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADExampleTest01()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[]{
                    new ADXP("321 Cedar Rd. North", AddressPartType.StreetAddressLine),
                    new ADXP("Thunder Bay", AddressPartType.City),
                    new ADXP("Ontario", AddressPartType.State),
                    new ADXP("Canada", AddressPartType.Country),
                    new ADXP("N2N2N4", AddressPartType.PostalCode)
                }
                );
            vacationHome.NullFlavor = null;
            Console.WriteLine(vacationHome.ToString("{ZIP} - {CTY}, {STA} {CNT}"));
            // output: N2N2N4 - Thunder Bay, Ontario Canada
            Assert.IsTrue(vacationHome.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values ARE Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADExampleTest02()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[]{}
                );
            vacationHome.NullFlavor = null;
            Console.WriteLine(vacationHome.ToString("{ZIP} - {CTY}, {STA} {CNT}"));
            // output: N2N2N4 - Thunder Bay, Ontario Canada
            Assert.IsFalse(vacationHome.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values ARE Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADExampleTest03()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[] {
                    new ADXP("321 Cedar Rd. North", AddressPartType.StreetAddressLine),
                    new ADXP("Thunder Bay", AddressPartType.City),
                    new ADXP("Ontario", AddressPartType.State),
                    new ADXP("Canada", AddressPartType.Country),
                    new ADXP("N2N2N4", AddressPartType.PostalCode)
                }
                );
            vacationHome.NullFlavor = NullFlavor.Other;
            Console.WriteLine(vacationHome.ToString("{ZIP} - {CTY}, {STA} {CNT}"));
            // output: N2N2N4 - Thunder Bay, Ontario Canada
            Assert.IsFalse(vacationHome.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values ARE Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADExampleTest04()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[] {}
                );
            vacationHome.NullFlavor = NullFlavor.Other;
            Console.WriteLine(vacationHome.ToString("{ZIP} - {CTY}, {STA} {CNT}"));
            // output: N2N2N4 - Thunder Bay, Ontario Canada
            Assert.IsTrue(vacationHome.Validate());
        }


        /* TEST FOR FLAVOR NOT ALLOWED */

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values ARE Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADExampleTest05()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[] {
                    new ADXP("321 Cedar Rd. North", AddressPartType.StreetAddressLine),
                    new ADXP("Thunder Bay", AddressPartType.City),
                    new ADXP("Ontario", AddressPartType.State),
                    new ADXP("Canada", AddressPartType.Country),
                    new ADXP("N2N2N4", AddressPartType.PostalCode)
                }
                );
            vacationHome.NullFlavor = null;
            Console.WriteLine(vacationHome.ToString("{ZIP} - {CTY}, {STA} {CNT}"));
            // output: N2N2N4 - Thunder Bay, Ontario Canada
            Assert.IsFalse(AD.IsValidBasicFlavor(vacationHome));
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values ARE Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADExampleTest06()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[] {
                    new ADXP("Thunder Bay", AddressPartType.City)
                }
                );
            vacationHome.NullFlavor = null;
            Console.WriteLine(vacationHome.ToString("{ZIP} - {CTY}, {STA} {CNT}"));
            // output: Thunder Bay
            Assert.IsTrue(AD.IsValidBasicFlavor(vacationHome));
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values ARE Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADExampleTest07()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[] {
                    new ADXP("Ontario", AddressPartType.State)
                }
                );
            vacationHome.NullFlavor = null;
            Console.WriteLine(vacationHome.ToString("{ZIP} - {CTY}, {STA} {CNT}"));
            // output: Ontario
            Assert.IsTrue(AD.IsValidBasicFlavor(vacationHome));
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values ARE Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADExampleTest08()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[] {
                    new ADXP("Canada", AddressPartType.Country)
                }
                );
            vacationHome.NullFlavor = null;
            Console.WriteLine(vacationHome.ToString("{ZIP} - {CTY}, {STA} {CNT}"));
            // output: Canada
            Assert.IsFalse(AD.IsValidBasicFlavor(vacationHome));
        }
    }
}
