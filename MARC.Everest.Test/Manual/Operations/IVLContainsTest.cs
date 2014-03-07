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


namespace MARC.Everest.Test.DataTypes.Manual.Operations
{
    /// <summary>
    /// Summary description for IVL_Contains
    /// </summary>
    [TestClass]
    public class IVLContainsTest
    {
        public IVLContainsTest()
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

        /* Example 47 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Low         :   Describes the lower bounds of the interval
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IVL_ContainsTest01()
        {
            // Create an instance of IVL
            IVL<PQ> acceptableRange = new IVL<PQ>(
                new PQ(0, "L"),
                new PQ(10, "L")
            )
            {
                LowClosed = true,
                HighClosed = true
            };

            // Since we need to convert PQ units, add a unit converter
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            PQ test = new PQ(9, "L");

            // Determine if the value is in range (true)
            bool isInRange = acceptableRange.Contains(test);

            acceptableRange.NullFlavor = NullFlavor.NoInformation;
            Assert.IsTrue(isInRange);
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Low         :   Describes the lower bounds of the interval
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// Test is NOT within range of acceptable range.
        /// </summary>
        [TestMethod]
        public void IVL_ContainsTest02()
        {
            // Create an instance of IVL
            IVL<PQ> acceptableRange = new IVL<PQ>(
                new PQ(0, "L"),
                new PQ(10, "L")
            )
            {
                LowClosed = true,
                HighClosed = true
            };

            // Since we need to convert PQ units, add a unit converter
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            PQ test = new PQ(100, "L");

            // Determine if the value is in range (true)
            bool isInRange = acceptableRange.Contains(test);

            acceptableRange.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(isInRange);
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Low         :   Describes the lower bounds of the interval
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// Test is NOT within range of acceptable range.
        /// Testing if unit converter will convert PQ units before testing.
        /// </summary>
        [TestMethod]
        public void IVL_ContainsTest03()
        {
            // Create an instance of IVL
            IVL<PQ> acceptableRange = new IVL<PQ>(
                new PQ(0, "L"),
                new PQ(10, "L")
            )
            {
                LowClosed = true,
                HighClosed = true
            };

            // Since we need to convert PQ units, add a unit converter
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            PQ test = new PQ(100000, "mL");

            // Determine if the value is in range (true)
            bool isInRange = acceptableRange.Contains(test);

            acceptableRange.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(isInRange);
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Low         :   Describes the lower bounds of the interval
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// Test is within range of acceptable range.
        /// Testing if unit converter will convert PQ units before testing.
        /// </summary>
        [TestMethod]
        public void IVL_ContainsTest04()
        {
            // Create an instance of IVL
            IVL<PQ> acceptableRange = new IVL<PQ>(
                new PQ(0, "L"),
                new PQ(10, "L")
            )
            {
                LowClosed = true,
                HighClosed = true
            };

            // Since we need to convert PQ units, add a unit converter
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            PQ test = new PQ(100, "mL");

            // Determine if the value is in range (true)
            bool isInRange = acceptableRange.Contains(test);

            acceptableRange.NullFlavor = NullFlavor.NoInformation;
            Assert.IsTrue(isInRange);
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Low         :   Describes the lower bounds of the interval
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// Test is within range of acceptable range.
        /// Testing if illogical conversion will pass (eg. converting Litres to meters)
        /// </summary>
        [TestMethod]
        public void IVL_ContainsTest05()
        {
            // Create an instance of IVL
            IVL<PQ> acceptableRange = new IVL<PQ>(
                new PQ(0, "L"),
                new PQ(10, "L")
            )
            {
                LowClosed = true,
                HighClosed = true
            };

            // Since we need to convert PQ units, add a unit converter
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            PQ test = new PQ(10, "m");

            try
            {
                // Determine if the value is in range (true)
                bool isInRange = acceptableRange.Contains(test);
                acceptableRange.NullFlavor = NullFlavor.NoInformation;
                Assert.IsFalse(isInRange);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.ToString().Contains("Units must match to compare PQ"));
            }
        }
    }
}
