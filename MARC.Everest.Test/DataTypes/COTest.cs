using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for COTest
    /// </summary>
    [TestClass]
    public class COTest
    {
        public COTest()
        {
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
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code             
        ///     Value            
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void COValueCodeTest()
        {
            CO co = new CO();
            co.NullFlavor = null;
            co.Code = "Hello";
            co.Value = 3;
            Assert.IsTrue(co.Validate());
        }

        /// <summary>
        /// Confirms that a CO is not equal to a String
        /// </summary>
        [TestMethod]
        public void COTypeMismatchEqualityTest()
        {
            CO a = new CO()
            {
                Value = (Decimal)1.2,
                Code = "12345"
            };
            Assert.IsFalse(a.Equals(5));
        }

        /// <summary>
        /// Confirms that two COs with equal content are equal
        /// </summary>
        [TestMethod]
        public void COContentSameEqualityTest()
        {
            CO a = new CO()
            {
                Value = (Decimal)1.2,
                Code = "12345"
            },
            b = new CO()
            {
                Value = (Decimal)1.2,
                Code = "12345"
            };
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
        }

        /// <summary>
        /// Confirms that two CDs with different content aren't equal
        /// </summary>
        [TestMethod]
        public void COContentDifferentEqualityTest()
        {
            CO a = new CO()
            {
                Value = (Decimal)1.2,
                Code = "12345"
            },
            b = new CO()
            {
                Value = (Decimal)1.3,
                Code = "12345"
            };
            Assert.IsFalse(a.Equals(b));
        }

        /// <summary>
        /// Test conversion of CO to double
        /// </summary>
        [TestMethod]
        public void COToDoubleTest()
        {
            CO foo = new CO() { Value = (Decimal)1.2 };
            Assert.AreEqual(1.2, foo.ToDouble());
        }

        /// <summary>
        /// Test conversion of CO to double
        /// </summary>
        [TestMethod]
        public void COToIntTest()
        {
            CO foo = new CO() { Value = (Decimal)1.2 };
            Assert.AreEqual(1, foo.ToInt());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Code             
        ///     Value            
        ///     NullFlavor
        /// And, there are no nullified variables
        /// </summary>
        [TestMethod]
        public void COValueCodeNullFlavorTest()
        {
            CO co = new CO();
            co.NullFlavor = NullFlavor.NotAsked;
            co.Code = "Hello";
            co.Value = 3;
            Assert.IsFalse(co.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When there are no values being populated:
        /// And, the following variables are nullified:
        ///     Code             
        ///     Value            
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CONullTest()
        {
            CO co = new CO();
            co.NullFlavor = null;
            co.Code = null;
            co.Value = null;
            Assert.IsFalse(co.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are nullified:
        ///     Code             
        ///     Value            
        /// </summary>
        [TestMethod]
        public void CONullFlavorTest()
        {
            CO co = new CO();
            co.NullFlavor = NullFlavor.NotAsked;
            co.Code = null;
            co.Value = null;
            Assert.IsTrue(co.Validate());
        }

        /// <summary>
        /// TDouble() on CO
        /// </summary>
        [TestMethod]
        public void COUsingToDoubleOnCOTest()
        {
                // create instance assigning value and using the ToDouble() function.
                // NOTE: the coInstance value does not actually change in memory
                CO coInstance = new CO();
                coInstance.Value = (Decimal)4.5f;

                // true if the ToDouble method works correctly
                Assert.IsTrue(coInstance.ToDouble() == 4.5f); // passes
        }

        /// <summary>
        /// ToInt() on CO
        /// </summary>
        [TestMethod]
        public void COUsingToIntOnCOTest()
        {
                // create instance assigning value and using the ToInt() function.
                // NOTE: the value of the coInstance object does not change.
                CO coInstance = new CO();
                coInstance.Value = (Decimal)4.5f;

                // is true if the ToInt() method works correctly.
                Assert.IsTrue(coInstance.ToInt() == 4);
        }
    }
}
