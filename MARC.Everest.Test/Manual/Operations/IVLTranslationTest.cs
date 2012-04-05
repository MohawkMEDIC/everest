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
    /// Summary description for IVL_Trans
    /// </summary>
    [TestClass]
    public class IVL_Trans
    {
        public IVL_Trans()
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

        /* Example 50 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        /// </summary>
        [TestMethod]
        public void IVL_Trans_Test01()
        {
            IVL<TS> may2012 = new TS(new DateTime(2012, 05, 01), DatePrecision.Month).ToIVL();
            var may2032 = may2012.Translate(new PQ(20, "a"));
            Console.WriteLine(may2012.ToString());
            // outputs 2012,05,01 to 2012,05,31
            Console.WriteLine(may2032.ToString());
            // outputs 2032,05,01 to 2032,05,31
            Assert.IsTrue(may2032.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        /// </summary>
        [TestMethod]
        public void IVL_Trans_Test02()
        {
            IVL<TS> may2012 = new TS(new DateTime(2012, 05, 01), DatePrecision.Month).ToIVL();
            var may2032 = may2012.Translate(new PQ(20, "a"));
            Console.WriteLine(may2012.ToString());
            // outputs 2012,05,01 to 2012,05,31
            Console.WriteLine(may2032.ToString());
            // outputs 2032,05,01 to 2032,05,31
            Assert.AreNotEqual(may2012, may2032);
        }
    }
}
