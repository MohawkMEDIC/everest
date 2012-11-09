using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.RMIM.CA.R020402.Vocabulary;
using MARC.Everest.DataTypes.Primitives;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for CSExamplesTest
    /// </summary>
    [TestClass]
    public class CSExamplesTest
    {
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

        
        /* Example 9 */
        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code.
        /// </summary>
        [TestMethod]
        public void CSExample9Test()
        {
            CS<AcknowledgementCondition> condition =
                    new CS<AcknowledgementCondition>(AcknowledgementCondition.Always);
            // Direct assignment of a code
            condition = AcknowledgementCondition.Never;
            // Assignment through the code property
            condition.Code = AcknowledgementCondition.ErrorRejectOnly;
            // Assignment of a non-bound code
            condition.Code = CodeValue<AcknowledgementCondition>.Parse("OTHER CODE");
            Assert.IsFalse(condition.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are Nullified:
        ///     Code            : The initial code.
        /// </summary>
        [TestMethod]
        public void CSExample9Test02()
        {
            CS<AcknowledgementCondition> condition =
                    new CS<AcknowledgementCondition>(AcknowledgementCondition.Always);
            // Direct assignment of a code
            condition = AcknowledgementCondition.Never;
            // Assignment through the code property
            condition.Code = null;
            Assert.IsFalse(condition.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are Nullified:
        ///     Code            : The initial code.
        ///     Nullflavor
        ///     
        /// </summary>
        [TestMethod]
        public void CSExample9Test03()
        {
            CS<AcknowledgementCondition> condition =
                    new CS<AcknowledgementCondition>(AcknowledgementCondition.Always);

            condition.Code = AcknowledgementCondition.Never;
            condition.NullFlavor = NullFlavor.NoInformation;

            Assert.IsFalse(condition.Validate());

        }



        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are Nullified:
        ///     Code            : The initial code.
        /// And the following values are not Nullified:
        ///     Nullflavor
        /// </summary>
        [TestMethod]
        public void CSExample9Test04()
        {
            CS<AcknowledgementCondition> condition =
                    new CS<AcknowledgementCondition>(AcknowledgementCondition.Always);

            condition.Code = null;
            condition.NullFlavor = NullFlavor.NoInformation;

            Assert.IsTrue(condition.Validate());

        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are Nullified:
        ///     Code            : The initial code.
        /// And the following values are not Nullified:
        ///     Nullflavor
        /// </summary>
        [TestMethod]
        public void CSExample9Test05()
        {
            CS<AcknowledgementCondition> condition = 
                new CS<AcknowledgementCondition>(AcknowledgementCondition.Always);
            condition.Code = null;
            condition.NullFlavor = NullFlavor.NoInformation;

            Assert.IsTrue(condition.Validate());

        }

    }
}
