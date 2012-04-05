using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Primitives;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes.Manual.Operations
{
    /// <summary>
    /// Summary description for DateSubtractionTest
    /// </summary>
    [TestClass]
    public class DateSubtractionTest
    {
        public DateSubtractionTest()
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

        /* Example 5 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        /// 
        /// And the following are Nullified
        ///     Nullflavor
        /// </summary>
        [TestMethod]
        public void DateSubTest01()
        {
            TS now = DateTime.Now,
                other = DateTime.Parse("2000-03-14");

            // Subtract
            PQ dist = now - other;
            dist.Precision = 2;
            Console.WriteLine(dist.ToString());     // output: xxxxx.xx s

            // Convert To Weeks
            dist = dist.Convert("wk");
            dist.Precision = 2;
            Console.WriteLine(dist);                // output: yyyyy.yy wk

            Assert.IsTrue(dist.Validate());
        }
    }
}
