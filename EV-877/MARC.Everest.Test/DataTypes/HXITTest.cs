using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for HXITTest
    /// </summary>
    [TestClass]
    public class HXITTest
    {
        public HXITTest()
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

        /// <summary>
        /// Ensures that Validation succeeds
        /// When the following variables is being populated:
        ///     Value   : The initial value of the HXIT
        /// </summary> 
        [TestMethod]
        public void HXITValeTest()
        {
            HXIT<CS<String>> h = new HXIT<CS<String>>();
            h.Value = "4726224";
            Assert.IsTrue(h.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails
        /// When the following variables is being nullified:
        ///     Value   : The initial value of the HXIT
        /// </summary>         
        [TestMethod]
        public void HXITNullTest()
        {
            HXIT<CS<String>> h = new HXIT<CS<String>>();
            h.Value = null;
            Assert.IsFalse(h.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds
        /// When the following variables are being pupulated:
        ///     Value           : The initial value of the HXIT
        ///     ValidTimeHigh   : Identifies the time that the given information has or will no longer be valid
        /// </summary>         
        [TestMethod]
        public void HXITTimeHighTest()
        {
            HXIT<CS<String>> h = new HXIT<CS<String>>();
            h.Value = "";
            h.ValidTimeHigh = DateTime.Now;
            Assert.IsTrue(h.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds
        /// When the following variables is being pupulated:
        ///     Value           : The initial value of the HXIT
        /// </summary>         
        [TestMethod]
        public void HXITValue2Test()
        {
            HXIT<CS<String>> h = new HXIT<CS<String>>("4567777");
            Assert.IsTrue(h.Validate());
        }
    }
}
