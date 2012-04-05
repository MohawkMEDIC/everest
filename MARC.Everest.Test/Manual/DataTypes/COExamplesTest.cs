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
    /// Summary description for COExamplesTest
    /// </summary>
    [TestClass]
    public class COExamplesTest
    {
        public COExamplesTest()
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

        /* Example 15 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        ///     Code        :   Contains the concept that describes the ordinal item.
        ///     CodeSystem
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void COExamplesTest01()
        {
            CO poor = new CO(1, new CD<String>("1","2.3.4.5.6.7"));
            poor.Code.DisplayName = "poor";
            poor.NullFlavor = null;
            Assert.IsTrue(poor.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        ///     Code        :   Contains the concept that describes the ordinal item.
        ///     CodeSystem
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void COExamplesTest02()
        {
            CO poor = new CO(1, new CD<String>("1", "2.3.4.5.6.7"));
            poor.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(poor.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code        :   Contains the concept that describes the ordinal item.
        ///     CodeSystem
        /// And the following values are Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void COExamplesTest03()
        {
            CO poor = new CO(new CD<String>("1", "2.3.4.5.6.7"));
            poor.NullFlavor = null;
            poor.Value = null;
            Assert.IsTrue(poor.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Code        :   Contains the concept that describes the ordinal item.
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        /// </summary>
        [TestMethod]
        public void COExamplesTest04()
        {
            CO poor = new CO(new CD<String>("1", "2.3.4.5.6.7"));
            poor.NullFlavor = NullFlavor.Other;
            poor.Value = null;
            Assert.IsFalse(poor.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Code        :   Contains the concept that describes the ordinal item.
        ///     CodeSystem
        /// </summary>
        [TestMethod]
        public void COExamplesTest05()
        {
            CO poor = new CO(1);
            poor.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(poor.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        /// And the following values are Nullified:
        ///     Code        :   Contains the concept that describes the ordinal item.
        ///     CodeSystem
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void COExamplesTest06()
        {
            CO poor = new CO(1);
            poor.NullFlavor = null;
            Assert.IsTrue(poor.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        ///     Code        :   Contains the concept that describes the ordinal item.
        ///     CodeSysetm
        /// </summary>
        [TestMethod]
        public void COExamplesTest07()
        {
            CO poor = new CO();
            poor.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(poor.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        ///     Code        :   Contains the concept that describes the ordinal item.
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void COExamplesTest08()
        {
            CO poor = new CO();
            poor.NullFlavor = null;
            Assert.IsFalse(poor.Validate());
        }
    }
}
