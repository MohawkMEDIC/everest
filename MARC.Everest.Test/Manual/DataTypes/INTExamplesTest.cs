using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for INTExamplesTest
    /// </summary>
    [TestClass]
    public class INTExamplesTest
    {
        public INTExamplesTest()
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

        /* Example 22 */

        /// <summary>
        /// Testing Function AreEqual must return TRUE.
        /// Min returns the lesser of the two integers.
        /// Max returns the greater of the two integers.
        /// </summary>
        [TestMethod]
        public void INTExamplesTest01()
        {
            INT one = 1, 
                two = 2;
            INT min = one.Min(two), // returns 1
                max = one.Max(two); // returns 2
            Assert.AreEqual(min,one);
            Assert.AreEqual(max, two);
        }


        /// <summary>
        /// Testing Function AreEqual must return FALSE.
        /// </summary>
        [TestMethod]
        public void INTExamplesTest02()
        {
            INT first = 1,
                second = 10;
            INT min = first.Min(second),    // returns 1
                max = first.Max(second);    // returns 10
            Assert.AreEqual(min, first);
            Assert.AreEqual(max, second);
        }

        /// <summary>
        /// Testing Function AreEqual must return TRUE.
        /// </summary>
        [TestMethod]
        public void INTExamplesTest03()
        {
            INT first = 1,
                second = 10;
            INT min = first.Min(second),    // returns 1
                max = first.Max(second);    // returns 10
            Assert.AreEqual(min, 1);
            Assert.AreEqual(max, 10);
        }

        /// <summary>
        /// Testing Flavor of INT. Function AreEqual must return TRUE.
        /// </summary>
        [TestMethod]
        public void INTExamplesTest04()
        {
            bool boolPositive = false;
            INT num = 1;

            // testing if valid Positive flavor given positive integer
            if (INT.IsValidPosFlavor(num))
            { boolPositive = true;}

            Assert.AreEqual(boolPositive, true);
        }


        /// <summary>
        /// Testing Flavor of INT. Function AreEqual must return TRUE.
        /// </summary>
        [TestMethod]
        public void INTExamplesTest05()
        {
            bool boolPositive = false;
            INT num = 1;

            // testing if valid positive flavor, given positive integer
            if (INT.IsValidPosFlavor(num))
            {
                boolPositive = true;
            }
            Assert.AreNotEqual(boolPositive, false);
        }

        /// <summary>
        /// Testing Flavor of INT.
        /// The Function AreEqual must return TRUE.
        /// </summary>
        [TestMethod]
        public void INTExamplesTest06()
        {
            bool boolPositive = false;
            INT num = 1;

            // test for valid negative flavor
            if (INT.IsValidNonNegFlavor(num))
            {
                boolPositive = true;
            }
            Assert.AreEqual(boolPositive, true);
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value       :       set integer
        ///     NullFlavour :       set nullflavor
        ///     
        /// </summary>
        [TestMethod]
        public void INTExamplesTest07()
        {
            INT num = new INT();
            num.Value = 9;
            num.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(num.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavour :       set nullflavor    
        /// And, the following of the variables are Nullified:
        ///     Value       :       set integer    
        /// 
        /// </summary>
        [TestMethod]
        public void INTExamplesTest08()
        {
            INT num = new INT();
            num.Value = null;
            num.NullFlavor = NullFlavor.NotAsked;
            Assert.IsTrue(num.Validate());
        }
    }
}
