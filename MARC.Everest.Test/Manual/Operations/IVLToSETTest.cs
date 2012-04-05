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
    /// Summary description for IVL_To_SET
    /// </summary>
    [TestClass]
    public class IVL_To_SET
    {
        public IVL_To_SET()
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

        /* Example 49 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IvlToSetTest01()
        {
            IVL<INT> interval = new IVL<INT>(1, 10)
            {
                HighClosed = true
            };
            foreach (var i in interval.ToSet())
                Console.WriteLine(i);
            interval.NullFlavor = null;
            Assert.IsTrue(interval.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IvlToSetTest02()
        {
            IVL<INT> interval = new IVL<INT>(1, 10)
            {
                HighClosed = true
            };
            foreach (var i in interval.ToSet())
                Console.WriteLine(i);
            interval.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(interval.Validate());
        }
    }
}
