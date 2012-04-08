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
    /// Summary description for IVLExamplesTest
    /// </summary>
    [TestClass]
    public class IVLExamplesTest
    {
        public IVLExamplesTest()
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

        /*  Example 46 
            Use of the IVL Value property
         */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IVLExampleTest01()
        {
            // create new timestamp interval
            IVL<TS> currentlyOccuring = new IVL<TS>(DateTime.Now);
            currentlyOccuring.NullFlavor = null;
            Console.WriteLine(currentlyOccuring.ToString());
            Assert.IsTrue(currentlyOccuring.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IVLExampleTest02()
        {
            IVL<TS> currentlyOccuring = new IVL<TS>(DateTime.Now);
            currentlyOccuring.NullFlavor = NullFlavor.Other;
            Console.WriteLine(currentlyOccuring.ToString());
            Assert.IsFalse(currentlyOccuring.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Value
        /// </summary>
        [TestMethod]
        public void IVLExampleTest03()
        {
            IVL<TS> currentlyOccuring = new IVL<TS>();
            currentlyOccuring.NullFlavor = NullFlavor.Other;
            Console.WriteLine(currentlyOccuring.ToString());
            Assert.IsTrue(currentlyOccuring.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Value
        /// </summary>
        [TestMethod]
        public void IVLExampleTest04()
        {
            IVL<TS> currentlyOccuring = new IVL<TS>();
            currentlyOccuring.NullFlavor = null;
            Console.WriteLine(currentlyOccuring.ToString());
            Assert.IsFalse(currentlyOccuring.Validate());
        }
        /* Example 45 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Low         :   Describes the lower bounds of the interval
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ConstructingIVLTest01()
        {
            // Using low/high
            IVL<TS> janStart = new IVL<TS>(
                new TS(new DateTime(2012, 01, 01), DatePrecision.Day),
                new TS(new DateTime(2012, 01, 01), DatePrecision.Day)
            );
            janStart.NullFlavor = null;
            Assert.IsTrue(janStart.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Low         :   Describes the lower bounds of the interval
        ///     Width       :   Physical quantity that expresses the width of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ConstructingIVLTest02()
        {
            // Using low/width
            IVL<TS> janStartLow = new IVL<TS>()
            {
                Low = new TS(new DateTime(2012, 01, 01), DatePrecision.Day),
                Width = new PQ(15, "d")
            };
            janStartLow.NullFlavor = null;
            Assert.IsTrue(janStartLow.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Width       :   Physical quantity that expresses the width of the interval
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ConstructingIVLTest03()
        {
            // Using high/width
            IVL<TS> janStartHigh = new IVL<TS>()
            {
                Width = new PQ(15, "d"),
                High = new TS(new DateTime(2012, 01, 05), DatePrecision.Day)
            };
            janStartHigh.NullFlavor = null;
            Assert.IsTrue(janStartHigh.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Low         :   Describes the lower bounds of the interval
        ///     High        :   Describes the upper bounds of the interval
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ConstructingIVLTest04()
        {
            // Using low
            IVL<TS> janStart = new IVL<TS>()
            {
                Low = new TS(new DateTime(2012, 01, 05), DatePrecision.Day),
                High = new TS(new DateTime(2012, 01, 05), DatePrecision.Day)
            };
            janStart.NullFlavor = NullFlavor.Unknown;
            Assert.IsFalse(janStart.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     LowClosed   :   used to indicate whether the lower bounds
        ///                     of the inerval is included in the set
        ///     High        :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ConstructingIVLTest05()
        {
            // Using low
            IVL<TS> janStart = new IVL<TS>()
            {
                LowClosed = true,
                //Low = new TS(new DateTime(2012, 01, 05), DatePrecision.Day),
                High = new TS(new DateTime(2012, 01, 05), DatePrecision.Day)
            };
            Assert.IsFalse(janStart.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     HighClosed  :   used to indicate whether the upper bounds
        ///                     of the inerval is included in the set
        ///     Low         :   Describes the upper bounds of the interval
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ConstructingIVLTest06()
        {
            // Using low
            IVL<TS> janStart = new IVL<TS>()
            {
                HighClosed = true,
                Low = new TS(new DateTime(2012, 01, 05), DatePrecision.Day),
                // High = new TS(new DateTime(2012, 01, 05), DatePrecision.Day)
            };
            janStart.NullFlavor = null;
            Assert.IsFalse(janStart.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// And the following values are Nullified:
        ///     Value
        ///     NullFlavor
        ///     High
        ///     Low
        ///     HighClosed
        ///     LowClosed
        /// </summary>
        [TestMethod]
        public void ConstructingIVLTest07()
        {
            // Using low
            IVL<TS> janStart = new IVL<TS>();
            janStart.Value = null;
            janStart.NullFlavor = null;
            janStart.High = null;
            janStart.Low = null;
            janStart.HighClosed = null;
            janStart.LowClosed = null;
            Assert.IsFalse(janStart.Validate());
        }

    }
}
