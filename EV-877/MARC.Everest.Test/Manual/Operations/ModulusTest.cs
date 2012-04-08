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
    /// Summary description for ModulusTest
    /// </summary>
    [TestClass]
    public class ModulusTest
    {
        public ModulusTest()
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

        /* Example 23 */
        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// </summary>
        [TestMethod]
        public void ModulusExampleTest01()
        {
            // print all odd numbers
            for (INT i = 0; i < 10; i++){
                if ((i % 2).Equals(1)){
                    Console.WriteLine("{0} is odd", i);
                }
            }
            Assert.AreEqual(10 % 5 == 0, true);
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// </summary>
        [TestMethod]
        public void ModulusExampleTest02()
        {
            // print all odd numbers
            for (INT i = 0; i < 10; i++)
            {
                if ((i % 2).Equals(1))
                {
                    Console.WriteLine("{0} is odd", i);
                }
            }
            Assert.AreNotEqual(10 % 5 == 0, false);
        }
    }
}
