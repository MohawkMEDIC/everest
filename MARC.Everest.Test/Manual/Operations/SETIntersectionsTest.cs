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

namespace MARC.Everest.Test.DataTypes.Manual.Operations
{
    /// <summary>
    /// Summary description for SETIntersections
    /// </summary>
    [TestClass]
    public class SETIntersectionsTest
    {
        public SETIntersectionsTest()
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

        /* Example 62 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Items       :   Members of the SET
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void SetIntersectTest01()
        {
            // Create sets
            SET<INT> firstSet = SET<INT>.CreateSET(1,2,3,4),
                secondSet = SET<INT>.CreateSET(2,4,6);

            // Intersect the sets
            SET<INT> resultant = firstSet.Intersection(secondSet);
            Console.WriteLine("The following integers intersect:");
            foreach (var i in resultant)
                Console.WriteLine(i);

            // output:
            // The following integers intersect:
            // 2
            // 4

            resultant.NullFlavor = null;
            Assert.IsTrue(resultant.Validate());
        }
    }
}
