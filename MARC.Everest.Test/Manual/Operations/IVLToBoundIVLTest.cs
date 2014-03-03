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
    /// Summary description for IVL_To_BoundIVL
    /// </summary>
    [TestClass]
    public class IVL_To_BoundIVL
    {
        public IVL_To_BoundIVL()
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

        /* Example 48 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Low         :   Describes the lower bounds of the interval
        ///     Width       :   Describes the width of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void BoundIVLTest01()
        {
            IVL<TS> janStartLow = new IVL<TS>()
            {
                Low = new TS(new DateTime(2012, 01, 01), DatePrecision.Day),
                Width = new PQ(14, "d")
            };
            var janStart = janStartLow.ToBoundIVL();
            janStart.NullFlavor = null;
            Console.WriteLine("{{ {0}...{1} }}",janStart.Low,janStart.High);
            Assert.IsTrue(janStart.Validate());
            // output is: {20120101...20120115}
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Width       :   Describes the width of the interval
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void BoundIVLTest02()
        {
            IVL<TS> janStartHigh = new IVL<TS>()
            {
                Width = new PQ(14, "d"),
                High = new TS(new DateTime(2012, 01, 30), DatePrecision.Day),
            };
            var janStart = janStartHigh.ToBoundIVL();
            janStart.NullFlavor = null;
            Console.WriteLine("{{ {0}...{1} }}", janStart.Low, janStart.High);
            // output is: {20120116...20120130}
            Assert.IsTrue(janStart.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     High        :   Describes the upper bounds of the interval
        ///     Width         :   Describes the width of the interval
        ///     NullFlavor     
        /// </summary>
        [TestMethod]
        public void BoundIVLTest03()
        {
            IVL<TS> janStartHigh = new IVL<TS>()
            {
                Width = new PQ(14, "d"),
                High = new TS(new DateTime(2012, 01, 30), DatePrecision.Day),
            };
            var janStart = janStartHigh.ToBoundIVL();
            janStart.NullFlavor = NullFlavor.Other;
            Console.WriteLine("{{ {0}...{1} }}", janStart.Low, janStart.High);
            Assert.IsFalse(janStart.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        [TestMethod]
        public void BoundIVLTest04()
        {
            IVL<TS> janStartHigh = new IVL<TS>()
            {
                High = new TS(new DateTime(2012, 01, 30), DatePrecision.Day)
            };
            var janStart = janStartHigh.ToBoundIVL();
            janStart.NullFlavor = null;
            Console.WriteLine("{{ {0}...{1} }}", janStart.Low, janStart.High);
            Assert.IsTrue(janStart.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Low        :   Describes the lower bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void BoundIVLTest05()
        {
            IVL<TS> janStartLow = new IVL<TS>()
            {
                Low = new TS(new DateTime(2012, 01, 01), DatePrecision.Day)
            };
            var janStart = janStartLow.ToBoundIVL();
            janStart.NullFlavor = null;
            Console.WriteLine("{{ {0}...{1} }}", janStart.Low, janStart.High);
            // outputs: {20120101...}
            Assert.IsTrue(janStart.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Width         :   Describes the width of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void BoundIVLTest06()
        {
            IVL<TS> janStartW = new IVL<TS>()
            {
                Width = new PQ(14, "d")
            };
            try
            {
                var janStart = janStartW.ToBoundIVL();
                janStart.NullFlavor = NullFlavor.Unknown;
                Assert.IsFalse(janStart.Validate());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Low        :   Describes the lower bounds of the interval
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void BoundIVLTest07()
        {
            IVL<TS> janStartLow = new IVL<TS>()
            {
                Low = new TS(new DateTime(2012, 01, 01), DatePrecision.Day),
                High = new TS(new DateTime(2012, 01, 15), DatePrecision.Day)
            };
            var janStart = janStartLow.ToBoundIVL();
            janStart.NullFlavor =null;
            Console.WriteLine("{{ {0}...{1} }}", janStart.Low, janStart.High);
            Assert.IsTrue(janStart.Validate());
        }
    }
}
