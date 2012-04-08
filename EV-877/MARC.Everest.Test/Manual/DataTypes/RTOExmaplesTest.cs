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
    /// Summary description for RTOExmaplesTest
    /// </summary>
    [TestClass]
    public class RTOExmaplesTest
    {
        public RTOExmaplesTest()
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

        /* Example 29 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : populated with money object over quantity object
        ///     Numerator       : Given numerator from Manual example
        ///     Denominator     : Given denominator from Manual example
        /// And the following values are Nullified:
        ///     Nullflavor
        ///     
        /// </summary>
        [TestMethod]
        public void RTOExample29Test01()
        {
            // create a new payrate ratio ($34 per hour)
            RTO<MO, PQ> payrate = new RTO<MO, PQ>(
                new MO((decimal)34.50, "CAD") { Precision = 2 },
                new PQ(1, "hr")
                );

            payrate.NullFlavor = null;
            Assert.IsTrue(payrate.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : Inherited; not assigned in code
        ///     Nullflavor      : NoInformation Given
        /// And the following values are Nullified:
        ///     Numerator       : Given numerator from Manual example
        ///     Denominator     : Given denominator from Manual example
        ///     
        /// </summary>
        [TestMethod]
        public void RTOExample29Test02()
        {
            RTO<MO, PQ> payrate = new RTO<MO, PQ>();
            payrate.NullFlavor = NullFlavor.NoInformation;
            Assert.IsTrue(payrate.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value           : populated but no numerator/denominator exists
        /// And the following values are Nullified:
        ///     Nullflavor
        ///     Numerator       : Given numerator from Manual example
        ///     Denominator     : Given denominator from Manual example
        ///     
        /// </summary>
        [TestMethod]
        public void RTOExample29Test03()
        {
            // create ratio with no values
            RTO<MO, PQ> payrate = new RTO<MO, PQ>();
            payrate.NullFlavor = null;
            Assert.IsFalse(payrate.Validate());
        }
        
        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value           : populated but no numerator/denominator exists
        /// And the following values are Nullified:
        ///     Nullflavor      
        ///     
        /// </summary>
        [TestMethod]
        public void RTOExample29Test04()
        {
            // test RTO given simple integers
            RTO<INT, INT> fraction = new RTO<INT, INT>(5, 10);
            fraction.NullFlavor = null;
            Console.WriteLine(fraction.ToString());
            Assert.AreEqual("5/10", fraction.ToString());
        }
    }
}
