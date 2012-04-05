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
    /// Summary description for Trans_PQ
    /// </summary>
    [TestClass]
    public class TranslatePQTest
    {
        public TranslatePQTest()
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


        /* Example 26 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   The initial value of the physical quantity
        ///     Code        :   UCUM code used to describe the units of the measured value
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PQTranslationsTest01()
        {
            PQ dist = new PQ((decimal)2.1, "m");
            // translating a measurement from one unit to another (meters to feet)
            dist.Translation = new SET<PQR>(
                new PQR((decimal)6.8897, "ft_i", "2.16.840.1.113883.6.8")
            );
            Assert.IsTrue(dist.Validate());
        }

 
        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        ///     Code        :   Contains the concept that describes the ordinal item.
        /// And the following values are Nullified:
        ///     NullFlavor
        ///     
        /// Testing if a PQ is still valid if translated to 
        /// a unit that makes no sense (eg. meters to Litres)
        /// </summary>
        [TestMethod]
        public void PQTranslationsTest02()
        {
            PQ dist = new PQ((decimal)2.1, "m");
            // translating from meters to Litres
            dist.Translation = new SET<PQR>(
                new PQR((decimal)6.8897, "L", "2.16.840.1.113883.6.8")
            );
            Assert.IsTrue(dist.Validate());
        }
    }
}
