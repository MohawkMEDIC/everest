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
    /// Summary description for PIVLExamplesTest
    /// </summary>
    [TestClass]
    public class PIVLExamplesTest
    {
        public PIVLExamplesTest()
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

        /* Example 52 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Period                  :   quantity that indicates frequency
        ///                                 at which the phase is repeated
        ///     Frequency               :   "                                "
        ///     Phase                   :   the ienterval of time that represents the
        ///                                 prototype for the repeating interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PIVLExamplesTest01()
        {
            // Represents an interval of time from 9am-5pm
            IVL<TS> nineToFiveIVL = new IVL<TS>
                (
                    // create new lower boundary timestamp (9am)
                    new TS (new DateTime(2011, 01, 01, 09, 00, 00),
                        DatePrecision.Minute),

                    // create new upper boundary timestamp (5pm)
                    new TS(new DateTime(2011, 01, 01, 17, 00, 00),
                        DatePrecision.Minute)   
                );

            // create new period interval
            PIVL<TS> nineToFiveEveryDay = new PIVL<TS>
            (
                nineToFiveIVL,              // specify interval (9am-5pm)
                new PQ(1, "d")              // specify frequency (once per day)
            );
            nineToFiveEveryDay.NullFlavor = null;
            Assert.IsTrue(nineToFiveEveryDay.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Period                  :   quantity that indicates frequency
        ///                                 at which the phase is repeated
        ///     Frequency               :   "                                "
        ///     Phase                   :   the ienterval of time that represents the
        ///                                 prototype for the repeating interval
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PIVLExamplesTest02()
        {
            // Represents an interval of time from 9am-5pm
            IVL<TS> nineToFiveIVL = new IVL<TS>
                (
                    new TS(new DateTime(2011, 01, 01, 09, 00, 00),
                        DatePrecision.Minute),
                    new TS(new DateTime(2011, 01, 01, 17, 00, 00),
                        DatePrecision.Minute)
                );
            PIVL<TS> nineToFiveEveryDay = new PIVL<TS>
            (
                nineToFiveIVL,
                new PQ(1, "d")
            );
            nineToFiveEveryDay.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(nineToFiveEveryDay.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:    
        ///     NullFlavor
        /// And the following values are Nullifeid:
        ///     Period                  :   quantity that indicates frequency
        ///                                 at which the phase is repeated
        ///     Frequency               :   "                                "
        ///     Phase                   :   the ienterval of time that represents the
        ///                                 prototype for the repeating interval
        /// </summary>
        [TestMethod]
        public void PIVLExamplesTest03()
        {
            // Represents an interval of time from 9am-5pm
            IVL<TS> nineToFiveIVL = new IVL<TS>
                (
                    new TS(new DateTime(2011, 01, 01, 09, 00, 00),
                        DatePrecision.Minute),
                    new TS(new DateTime(2011, 01, 01, 17, 00, 00),
                        DatePrecision.Minute)
                );

            // create empty period interval (no interval or frequency)
            PIVL<TS> nineToFiveEveryDay = new PIVL<TS>();

            nineToFiveEveryDay.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(nineToFiveEveryDay.Validate());
        }
    }
}
