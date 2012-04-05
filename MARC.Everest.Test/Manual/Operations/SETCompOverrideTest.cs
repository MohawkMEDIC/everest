using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;

namespace MARC.Everest.Test.DataTypes.Manual.Operations
{
    /// <summary>
    /// Summary description for SETCompOverrideTest
    /// </summary>
    [TestClass]
    public class SETCompOverrideTest
    {
        public SETCompOverrideTest()
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

        /* Example 60 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Items       :   Members of the SET
        /// </summary>
        [TestMethod]
        public void CustomCompTest01()
        {
            // Create a custom comparator using an anonymous delegate
            SET<II> identifiers = new SET<II>();
            identifiers.Comparator = delegate(II a, II b)
            {
                return a == b || a.Equals(b) ? 0 : 1;
            };
            
            // Create custom comparator using lambda
            identifiers.Comparator = (a, b) => a == b || a.Equals(b) ? 0 : 1;

            // the instance identifiers in these sets cannot match

            identifiers.NullFlavor = NullFlavor.NoInformation;
            Assert.IsTrue(identifiers.Validate());
        }
    }
}
