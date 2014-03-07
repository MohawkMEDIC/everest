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
using MARC.Everest.Exceptions;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for SETExamplesTest
    /// </summary>
    [TestClass]
    public class SETExamplesTest
    {
        public SETExamplesTest()
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

        /* Example 59 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Items       :   Members of the SET
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void AddingDupesToSetTest01()
        {
            // Create the SET
            SET<INT> ints = new SET<INT>();

            // Add numbers 0 through 9
            for (var i = 0; i <= 9; i++)
            {
                ints.Add(i);
            }

            try
            {
                ints.Add(5);    // 5 already exists in the set. Exception is thrown
            }
            catch (DuplicateItemException e)
            {
                Console.WriteLine("Cannot add duplicate item.");
            }

            ints.NullFlavor = null;
            Assert.IsTrue(ints.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Items       :   Members of the SET
        ///     NullFlavor
        ///     
        /// </summary>
        [TestMethod]
        public void AddingDupesToSetTest02()
        {
            // Create the SET
            SET<INT> ints = new SET<INT>();

            // Add numbers 0 through 9
            for (var i = 0; i <= 9; i++)
            {
                ints.Add(i);
            }

            try
            {
                ints.Add(5);    //Exception is thrown
            }
            catch (DuplicateItemException e)
            {
                Console.WriteLine("Cannot add duplicate item.");
            }

            ints.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(ints.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Items       :   Members of the SET
        /// </summary>
        [TestMethod]
        public void AddingDupesToSetTest03()
        {
            // Create the SET
            SET<INT> ints = new SET<INT>();
            ints.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(ints.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Items       :   Members of the SET
        /// </summary>
        [TestMethod]
        public void AddingDupesToSetTest04()
        {
            // Create the SET
            SET<INT> ints = new SET<INT>();
            ints.NullFlavor = null;
            Assert.IsFalse(ints.Validate());
        }
    }
}
