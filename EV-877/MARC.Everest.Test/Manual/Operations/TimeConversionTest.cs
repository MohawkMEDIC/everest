using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for TimeConversionTest
    /// </summary>
    [TestClass]
    public class TimeConversionTest
    {
        public TimeConversionTest()
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

        /* Example 28 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// Testing to see if PQ Time conversions are all valid.
        /// </summary>
        [TestMethod]
        public void TimeConvTest01()
        {
            TS y2kInstance = DateTime.Parse("2000-01-01");
            PQ distance = DateTime.Parse("2010-01-01") - y2kInstance;

            // nothing specified; measured in seconds
            Console.WriteLine("{0}", distance);     // outputs 315619200 s

            distance = distance.Convert("wk");      // outputs 521.857 wk
            distance.Precision = 3;
            Console.WriteLine("{0}", distance);

            distance = distance.Convert("a");       // 9.985 a
            distance.Precision = 3;
            Console.WriteLine("{0}", distance);

            //distance.UncertainRange = null;
            distance.NullFlavor = null;

            Assert.IsTrue(distance.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// Testing to see if PQ Time conversions are all valid.
        /// </summary>
        [TestMethod]
        public void TimeConvTest02()
        {
            TS y2kInstance = DateTime.Parse("2000-01-01");
            PQ distance = DateTime.Parse("2010-01-01") - y2kInstance;

            // nothing specified; measured in seconds
            Console.WriteLine("Before conversion: ");
            Console.WriteLine("{0}", distance);     // outputs 315619200 s

            try
            {
                distance = distance.Convert("m");       // convert years to meters (should fail)
                distance.Precision = 3;
                Console.WriteLine("After conversion: ");
                Console.WriteLine("{0}", distance);
                Assert.IsTrue(distance.Validate());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error converting");
                Console.WriteLine("{0}", e);
            }
        }
    }
}
