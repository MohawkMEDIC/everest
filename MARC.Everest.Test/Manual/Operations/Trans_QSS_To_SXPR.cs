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
    /// Summary description for Trans_QSS_To_SXPR
    /// </summary>
    [TestClass]
    public class Trans_QSS_To_SXPR
    {
        public Trans_QSS_To_SXPR()
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

        /* Example 41 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void QSSToSXPRTest01()
        {
            // A QSS of times that shift by month each year
            QSS<TS> times = new QSS<TS>
            (
                new TS(new DateTime(2008, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2009, 02, 01), DatePrecision.Month),
                new TS(new DateTime(2010, 03, 01), DatePrecision.Month)
            );

            var times2 = times.TranslateToSXPR();
            times2.NullFlavor = null;
            Console.WriteLine(times.ToString());
            Assert.IsTrue(times2.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values ARE Nullified:
        ///     Value
        ///     NullFlavor
        ///     
        /// Testing to see if passes with no items for QSS
        /// </summary>
        [TestMethod]
        public void QSSToSXPRTest02()
        {
            // A QSS of times that shift by month each year
            QSS<TS> times = new QSS<TS>();
            times.NullFlavor = null;
            var times2 = times.TranslateToSXPR();
            Console.WriteLine(times2.ToString());
            Assert.IsFalse(times2.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Value
        /// </summary>
        [TestMethod]
        public void QSSToSXPRTest03()
        {
            // A QSS of times that shift by month each year
            QSS<TS> times = new QSS<TS>();
            times.NullFlavor = NullFlavor.Other;
            var times2 = times.TranslateToSXPR();
            Console.WriteLine(times2.ToString());
            Assert.IsTrue(times2.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void QSSToSXPRTest04()
        {
            // A QSS of times that shift by month each year
            QSS<TS> times = new QSS<TS>(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2009, 02, 01), DatePrecision.Month),
                new TS(new DateTime(2010, 03, 01), DatePrecision.Month)
            );

            var times2 = times.TranslateToSXPR();
            times2.NullFlavor = NullFlavor.Other;
            Console.WriteLine(times2.ToString());
            Assert.IsFalse(times2.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        ///     NullFlavor
        /// Testing to confirm that 'times' is a valid SXPR after translation.
        /// </summary>
        [TestMethod]
        public void QSSToSXPRTest05()
        {
            // A QSS of times that shift by month each year
            QSS<TS> times = new QSS<TS>(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2009, 02, 01), DatePrecision.Month),
                new TS(new DateTime(2010, 03, 01), DatePrecision.Month)
            );

            var times2 = times.TranslateToSXPR();
            times2.NullFlavor = null;

            Console.WriteLine("Times Type: {0}", times.GetType().Name.ToString());
            Console.WriteLine("Times2 Type: {0}", times2.GetType().Name.ToString());
            Assert.AreNotEqual(times.GetType(), times2.GetType());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        ///     NullFlavor
        /// Testing to confirm that 'times' is a valid SXPR after translation.
        /// </summary>
        [TestMethod]
        public void QSSToSXPRTest06()
        {
            // A QSS of times that shift by month each year
            QSS<TS> times = new QSS<TS>(
                new TS(new DateTime(2008, 01, 01), DatePrecision.Month),
                new TS(new DateTime(2009, 02, 01), DatePrecision.Month),
                new TS(new DateTime(2010, 03, 01), DatePrecision.Month)
            );

            Console.WriteLine("Times Type: {0}", times.GetType().Name.ToString());
            var times3 = times.TranslateToSXPR();
            Console.WriteLine("Times Type: {0}", times.GetType().Name.ToString());
            times.NullFlavor = null;

            // Create instance of SXPR<TS>
            SXPR<TS> times2 = new SXPR<TS>();

            //Console.WriteLine("Times Type: {0}", times.GetType().Name.ToString());
            Console.WriteLine("Times2 Type: {0}", times2.GetType().Name.ToString());
            Assert.AreEqual(times3.GetType(), times2.GetType());
        }
    }
}
