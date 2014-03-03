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

namespace MARC.Everest.Test.DataTypes.Manual.Operations
{
    /// <summary>
    /// Summary description for PIVL_Freq_RepeatTest
    /// </summary>
    [TestClass]
    public class PIVL_Freq_RepeatTest
    {
        public PIVL_Freq_RepeatTest()
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

        /* Example 53 */

        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     Frequency          :   Number of times the interval will repeat (RTO).
        /// And the following values are Nullified:
        ///     Interval    :   Range that will be repeated (due to the frequency)
        /// </summary>
        [TestMethod]
        public void Freq_To_Represent_RepeatTest01()
        {
            // Represents an interval that repeats twice per day
            // No interval specified, only frequency
            PIVL<TS> twicePerDay = new PIVL<TS>
            (   null, new RTO<INT, PQ>(2, new PQ(1, "d"))  )
            {
                InstitutionSpecified = true
                // indicates whether the exact timing is up to the party executing the schedule
            };
            Assert.IsTrue(twicePerDay.Validate());
        }


        //// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     Frequency          :   Number of times the interval will repeat (RTO).
        ///     Interval    :   Range that will be repeated (due to the frequency)
        /// </summary>
        [TestMethod]
        public void Freq_To_Represent_RepeatTest02()
        {
            // create a new period interval with an EMPTY interval
            PIVL<TS> thricePerDay = new PIVL<TS>
            (
            new IVL<TS>(), 
            new RTO<INT, PQ>(3, new PQ(1, "d")))
            {
                InstitutionSpecified = true
                // indicates whether the exact timing is up to the party executing the schedule
            };
            Assert.IsTrue(thricePerDay.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     Frequency   :   Number of times the interval will repeat (RTO).
        ///     Interval    :   Range that will be repeated (due to the frequency)
        /// </summary>
        [TestMethod]
        public void Freq_To_Represent_RepeatTest03()
        {
            // create a new period interval with an POPULATED interval
            PIVL<TS> thricePerDay = new PIVL<TS>
            (
            new IVL<TS>(DateTime.Now, new DateTime(2013, 01, 01)),
            new RTO<INT, PQ>(3, new PQ(1, "d")))
            {
                InstitutionSpecified = true
                // indicates whether the exact timing is up to the party executing the schedule
            };
            Assert.IsTrue(thricePerDay.Validate());
        }
    }
}
