using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for SXCMTest
    /// </summary>
    [TestClass]
    public class SXCMTest
    {
        public SXCMTest()
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
        /// Ensures that SXCM Validate succeeds (returns TRUE)
        /// When the following values is being populated:
        ///     Value       
        /// </summary>         
        [TestMethod]
        public void SXCMValueTest1()
        {
            SXCM<TEL> sxcm = new IVL<TEL>();
            sxcm.Value = "HOLA";
            Assert.IsTrue(sxcm.Validate());
        }

        /// <summary>
        /// Ensures that SXCM Validate succeeds (returns TRUE)
        /// When the following values is being populated:
        ///     Value       
        /// </summary>         
        [TestMethod]
        public void SXCMValueTest2()
        {
            string p1 = "HOLA";
            SXCM<TEL> sxcm = new SXCM<TEL>(p1);
            Assert.IsTrue(sxcm.Validate());
        }


        /// <summary>
        /// Ensures that SXCM Validate succeeds (returns FALSE as an SXCM that is created must have a value or nullFlavor)
        /// When the following value is being nullified:
        ///     Value       
        /// </summary>         
        [TestMethod]
        public void SXCMNullValueTest()
        {
            SXCM<TEL> sxcm = new IVL<TEL>();
            sxcm.Value = null;
            Assert.IsFalse(sxcm.Validate());
        }

    }
}
