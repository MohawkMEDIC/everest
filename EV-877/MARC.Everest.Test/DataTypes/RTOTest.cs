using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test
{
    /// <summary>
    /// Summary description for RTOTest
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RTO"), TestClass]
    public class RTOTest
    {
        public RTOTest()
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
        /// Ensures that RTO can be cast to a double.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RTO"), TestMethod]
        public void RTOCastToDoubleTest()
        {
            Double d = (double)new RTO<INT, INT>(1, 2);
            Assert.AreEqual(0.5f, d);
        }

        ///// <summary>
        ///// Ensures that RTO&lt;INT, INT&gt; can be cast to a double.
        ///// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RTO"), TestMethod]
        //public void RTOCastRTOToDoubleTest()
        //{
        //    Double d = new RTO<INT, INT>(5, 1);
        //    Assert.AreEqual(5.0f, d);
        //}

        /// <summary>
        /// Ensures that RTO.ToString() works as expected.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RTO"), TestMethod]
        public void RTOToString()
        {
            RTO<INT, INT> testRTO = new RTO<INT, INT>(1,5);
            Assert.AreEqual("1/5", testRTO.ToString());
        }
    }
}
