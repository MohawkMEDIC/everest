using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for PIVLTest
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIVL"), TestClass]
    public class PIVLTest
    {
        public PIVLTest()
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
        /// Ensures thta validation succeeds when nullflavor is null;
        /// and phase & period are populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void PIVLValidationNULLNullflavorPOPPhasePeriodTest()
        {
            PIVL<INT> pivl = new PIVL<INT>();
            pivl.NullFlavor = null;
            pivl.Phase = new IVL<INT>(1);
            pivl.Period = new PQ(1, "y");
            Assert.IsTrue(pivl.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds when phase & period is null;
        /// and nullflavor is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void PIVLValidationNULLPhasePeriodPOPNullflavorTest()
        {
            PIVL<INT> pivl = new PIVL<INT>();
            pivl.NullFlavor = NullFlavor.NotAsked;
            pivl.Phase = null;
            pivl.Period = null;
            Assert.IsTrue(pivl.Validate());
        }

        /// <summary>
        /// Ensures that validation fails when nullflavor & phase are null;
        /// and period is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void PIVLValidationNULLPhaseNullflavorPOPPeriodTest()
        {
            PIVL<INT> pivl = new PIVL<INT>();
            pivl.NullFlavor = null;
            pivl.Phase = null;
            pivl.Period = new PQ(1, "y");
            Assert.IsTrue(pivl.Validate());
        }

        /// <summary>
        /// Ensures that validation fails when nullflavor, period & phase are null.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void PIVLValidationNULLNullflavorPhasePeriodTest()
        {
            PIVL<INT> pivl = new PIVL<INT>();
            pivl.NullFlavor = null;
            pivl.Phase = null;
            pivl.Period = null;
            Assert.IsFalse(pivl.Validate());
        }

        /// <summary>
        /// Ensures that validation fails when nullflavor, period & phase are populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void PIVLValidationPOPNullflavorPhasePeriodTest()
        {
            PIVL<INT> pivl = new PIVL<INT>();
            pivl.NullFlavor = NullFlavor.NotAsked;
            pivl.Phase = new IVL<INT>(1);
            pivl.Period = new PQ(1, "y");
            Assert.IsFalse(pivl.Validate());
        }
    }
}
