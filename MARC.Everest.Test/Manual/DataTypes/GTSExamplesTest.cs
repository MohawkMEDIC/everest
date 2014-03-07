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
    /// Summary description for GTSExamplesTest
    /// </summary>
    [TestClass]
    public class GTSExamplesTest
    {
        public GTSExamplesTest()
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

        /* Example 55 */
        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     Hull        :   describes the timing specification set expression
        /// And the following values are Nullified:
        ///     NullFlavor
        ///     
        /// Periodic interval of all mondays is built by
        /// converting a timestamp to an interval.
        /// </summary>
        [TestMethod]
        public void GTSExamplesTest01()
        {
            // Create a periodic interval with first week of all Septembers
            PIVL<TS> firstWeekofSept = new PIVL<TS>
                (
                    new IVL<TS>(
                        new TS(new DateTime(2011, 09, 01), DatePrecision.Day),
                        new TS(new DateTime(2011, 09, 08), DatePrecision.Day)
                    ),
                    new PQ (1, "a")
                );

            // Create a periodic interval of all mondays
            PIVL<TS> mondays = new PIVL<TS>
            (
               new TS(new DateTime(2011, 01, 03), DatePrecision.Day).ToIVL(),
               new PQ(1, "wk")                             
            );

            // Intersect to get labour day
            GTS labourDay = new GTS(
                QSI<TS>.CreateQSI(
                    firstWeekofSept,
                    mondays
                    )
                );

            labourDay.NullFlavor = null;
            Console.WriteLine(labourDay);
            Assert.IsTrue(labourDay.Validate());
        }

        
        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     Hull        :   describes the timing specification set expression
        /// And the following values are Nullified:
        ///     NullFlavor
        ///     
        /// Periodic interval of all mondays is built by
        /// instanciating a new interval with a high and a low.
        /// </summary>
        [TestMethod]
        public void GTSExamplesTest02()
        {
            // Create a periodic interval with first week of all Septembers
            PIVL<TS> firstWeekofSept = new PIVL<TS>
                (
                    new IVL<TS>(
                        new TS(new DateTime(2011, 09, 01), DatePrecision.Day),
                        new TS(new DateTime(2011, 09, 08), DatePrecision.Day)
                    ),
                    new PQ(1, "a")
                );
            
            // Create a periodic interval of all mondays
            // using a new IVL instead of TS.ToIVL
            PIVL<TS> mondays = new PIVL<TS>(
                new IVL<TS>(
                    new TS(new DateTime(2011, 01, 03), DatePrecision.Day)
                ),
                new PQ(1, "wk")
            );

            // Intersect to get labour day
            GTS labourDay = new GTS(
                QSI<TS>.CreateQSI(
                    firstWeekofSept,
                    mondays
                    )
                );
            labourDay.NullFlavor = null;
            Console.WriteLine(labourDay);
            Assert.IsTrue(labourDay.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     Hull        :   describes the timing specification set expression
        /// And the following values are Nullified:
        ///     NullFlavor
        ///     
        /// Compare 2 different methods of creating "mondays" PIVLs
        /// First method uses a new timestamp interval,
        /// while the second converts a timestamp using the .ToIVL() method
        /// </summary>
        [TestMethod]
        public void GTSExamplesTest03()
        {
            // Create a periodic interval with first week of all Septembers
            PIVL<TS> firstWeekofSept = new PIVL<TS>
                (
                    new IVL<TS>(
                        new TS(new DateTime(2011, 09, 01), DatePrecision.Day),
                        new TS(new DateTime(2011, 09, 08), DatePrecision.Day)
                    ),
                    new PQ(1, "a")
                );

            // Create a periodic interval of all mondays
            PIVL<TS> mondays = new PIVL<TS>(
                    new IVL<TS>(
                        new TS(new DateTime(2011, 02, 03), DatePrecision.Day)
                    ),
                    new PQ(1, "wk")
            );

            // Create a second periodic interval of all mondays
            PIVL<TS> mondays2 = new PIVL<TS>(
                new TS(new DateTime(2011, 01, 03), DatePrecision.Day).ToIVL(),
                new PQ(1, "wk")
            );

            // Intersect to get labour day
            GTS labourDay = new GTS(
                QSI<TS>.CreateQSI(
                    firstWeekofSept,
                    mondays
                    )
                );

            // Intersect to get labour day
            GTS labourDay2 = new GTS(
                QSI<TS>.CreateQSI(
                    firstWeekofSept,
                    mondays2
                    )
                );
            Console.WriteLine(mondays);
            Console.WriteLine(mondays2);
            Assert.AreNotEqual(labourDay, labourDay2);
        }


        /// <summary>
        /// Testing Function Validate must return FALSE
        /// When the following values are Nullified:
        ///     Hull        :       describes the timing specification set expression
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void GTSExamplesTest04()
        {
            // Create empty instance of GTS
            GTS labourDay = new GTS();
            labourDay.NullFlavor = null;

            Console.WriteLine(labourDay);
            Assert.IsFalse(labourDay.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE
        /// When the following values are not Nullified:
        ///     Hull        :       describes the timing specification set expression
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void GTSExamplesTest05()
        {
            // Create a periodic interval with first week of all Septembers
            PIVL<TS> firstWeekofSept = new PIVL<TS>
                (
                    new IVL<TS>(
                        new TS(new DateTime(2011, 09, 01), DatePrecision.Day),
                        new TS(new DateTime(2011, 09, 08), DatePrecision.Day)
                    ),
                    new PQ(1, "a")
                );
            // Create a periodic interval of all mondays
            PIVL<TS> mondays = new PIVL<TS>
            (
               new TS(new DateTime(2011, 01, 03), DatePrecision.Day).ToIVL(),
               new PQ(1, "wk")
            );

            // Intersect to get labour day
            GTS labourDay = new GTS(
                QSI<TS>.CreateQSI(
                    firstWeekofSept,
                    mondays
                    )
                );
            labourDay.NullFlavor = NullFlavor.Other;
            Console.WriteLine(labourDay);
            Assert.IsFalse(labourDay.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Hull        :       describes the timing specification set expression
        /// </summary>
        [TestMethod]
        public void GTSExamplesTest06()
        {
            // Create empty instance of GTS
            GTS labourDay = new GTS();
            labourDay.NullFlavor = NullFlavor.Other;
            Console.WriteLine(labourDay);
            Assert.IsTrue(labourDay.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     Hull        :       describes the timing specification set expression
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void GTSExamplesTest07()
        {
            // Create a periodic interval with first week of all Septembers
            PIVL<TS> firstWeekofSept = new PIVL<TS>
                (
                    new IVL<TS>(
                        new TS(new DateTime(2011, 09, 01), DatePrecision.Day),
                        new TS(new DateTime(2011, 09, 08), DatePrecision.Day)
                    ),
                    new PQ(1, "a")
                );
            // Create a periodic interval of all mondays
            PIVL<TS> mondays = new PIVL<TS>
            (
               new TS(new DateTime(2011, 01, 03), DatePrecision.Day).ToIVL(),
               new PQ(1, "wk")
            );

            // Intersect to get labour day
            GTS labourDay = new GTS(
                QSI<TS>.CreateQSI(
                    firstWeekofSept,
                    mondays
                    )
                );
            labourDay.NullFlavor = null;
            Console.WriteLine(labourDay);
            Assert.IsTrue(GTS.IsValidBoundedPivlFlavor(labourDay));
        }

        /*
        /// <summary>
        /// Testing Function Validate must return FALSE
        /// When the following values are not Nullified:
        ///     Hull        :       describes the timing specification set expression
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void GTSExamplesTest08()
        {
            // Create a periodic interval with first week of all Septembers
            PIVL<TS> firstWeekofSept = new PIVL<TS>
                (
                    new IVL<TS>(
                        new TS(new DateTime(2011, 09, 01), DatePrecision.Day),
                        new TS(new DateTime(2011, 09, 08), DatePrecision.Day)
                    ),
                    new PQ(1, "a")
                );
            // Create a periodic interval of all mondays
            PIVL<TS> mondays = new PIVL<TS>
            (
               new TS(new DateTime(2011, 01, 03), DatePrecision.Day).ToIVL(),
               new PQ(1, "wk")
            );

            // Intersect to get labour day
            GTS labourDay = new GTS(
                new QSP<TS>(
                    firstWeekofSept,
                    mondays
                    )
                );

            labourDay.NullFlavor = null;
            Console.WriteLine(labourDay);
            Assert.IsFalse(GTS.IsValidBoundedPivlFlavor(labourDay));
        }
        */
    }
}
