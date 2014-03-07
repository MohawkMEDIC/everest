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
using MARC.Everest.DataTypes.Converters;
using MARC.Everest.Connectors;


namespace MARC.Everest.Test.Manual.Operations
{
    /// <summary>
    /// Summary description for EIVLExamplesTest
    /// </summary>
    [TestClass]
    public class EIVLExamplesTest
    {
        public EIVLExamplesTest()
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

        /* Example 51
         * Constructing an EIVL */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Offset  :   An interval of elapsed time when the action should be taken.
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ConstructingEIVLTest01()
        {
            // An hour after meal
            EIVL<TS> hourAfterMeal = new EIVL<TS>(
                DomainTimingEventType.Meal,
                new IVL<PQ>(new PQ(1, "h"))
                );

            EIVL<TS> eivl1 = new EIVL<TS>(
                DomainTimingEventType.Meal,
                new IVL<PQ>(
                    new PQ(1, "h")  // high
                )
                );

            // 1-2 hours before sleep
            EIVL<TS> beforeSleep = new EIVL<TS>(
                DomainTimingEventType.HourOfSleep,
                new IVL<PQ>( new PQ(-2, "h"), new PQ(-1, "h") )
                );

            // Construct instructions as a union of the two
            GTS instructionTime = new GTS();
            instructionTime.Hull = QSU<TS>.CreateQSU(
                hourAfterMeal,
                beforeSleep
                );

            Assert.IsTrue(eivl1.Validate());

            Assert.IsTrue(hourAfterMeal.Validate());
            Assert.IsTrue(beforeSleep.Validate());
            Assert.IsTrue(instructionTime.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Offset  :   An interval of elapsed time when the action should be taken.
        ///     NullFlavor
        /// Testing a single instance of EIVL rather than entire GTS.
        /// </summary>
        [TestMethod]
        public void ConstructingEIVLTest02()
        {
            // An hour after meal
            EIVL<TS> hourAfterMeal = new EIVL<TS>(
                new DomainTimingEventType(),    // eventType
                new IVL<PQ>()                   // offset
                );

            hourAfterMeal.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(hourAfterMeal.Validate());
        }
    }
}
