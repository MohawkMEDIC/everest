using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for GTSTest
    /// </summary>
    [TestClass]
    public class GTSTest
    {
        public GTSTest()
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
        /// Tests the GTS(IVL) constructor. 
        /// 
        /// Tests the GTS class' IVL<TS> constructor to see if the datatype of the hull
        /// is the correct type.
        /// 
        /// Expected result: GTS.Hull.GetType = IVL<TS>
        /// </summary>
        [TestMethod]
        public void GTSIVLCTORTest()
        {
            //  interval from May 1, 2010 to May 10, 2010
            IVL<TS> tenDays = new IVL<TS>(DateTime.Parse("2010/05/1"), DateTime.Parse("2010/05/10"));
            GTS gtsInstance = new GTS(tenDays);

            Assert.AreEqual(typeof(IVL<TS>), gtsInstance.Hull.GetType());
        }

        /// <summary>
        /// Tests the GTS(PIVL) constructor. 
        /// 
        /// Tests the GTS class' PIVL<TS> constructor to see if the datatype of the hull
        /// is the correct type.
        /// 
        /// Expected result: GTS.Hull.GetType = PIVL<TS>
        /// </summary>
        [TestMethod]
        public void GTSPIVLCTORTest()
        {
            PIVL<TS> everyTwoDays = new PIVL<TS>(
                // this is every other day
               new IVL<TS>(DateTime.Parse("2010/05/1"), DateTime.Parse("2010/05/10")),
               new PQ((decimal)2.0, "d"));
            GTS gtsInstance = new GTS(everyTwoDays);

            Assert.AreEqual(typeof(PIVL<TS>), gtsInstance.Hull.GetType());
        }

        /// <summary>
        /// Tests the GTS(TS) constructor. 
        /// 
        /// Tests the GTS class' TS constructor to see if the datatype of the hull
        /// is the correct type.
        /// 
        /// Expected result: GTS.Hull.GetType = SXCM<TS>
        /// </summary>
        [TestMethod]
        public void GTSTSCTORTest()
        {
            TS tsDate = new TS(new DateTime(2010, 05, 01));
            GTS gtsInstance = new GTS(new IVL<TS>(tsDate));

            Assert.AreEqual(typeof(IVL<TS>), gtsInstance.Hull.GetType());
            //throw new NotImplementedException("GTS(TS) constructor does not exist.");
        }


    }
}
