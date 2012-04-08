using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for ArithmeticOpsINT
    /// </summary>
    [TestClass]
    public class ArithmeticOpsINT
    {
        public ArithmeticOpsINT()
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

        /* Example 24 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        /// CodeSystem should be automatically assigned given
        /// the Code comes from an enumerated vocabulary.
        /// </summary>
        [TestMethod]
        public void ArOpsTest01()
        {
            INT heightCm = 180;
            INT weightKg = 60;
            INT bmiInt = weightKg / (heightCm * heightCm);
            REAL bmi = (REAL)(weightKg / (heightCm * heightCm));
            Console.WriteLine("BMI Int: " + bmiInt.ToString());
            Console.WriteLine("BMI Real: " + bmi.ToString());
            Assert.IsTrue(bmiInt.Validate());
            Assert.IsTrue(bmi.Validate());
        }
    }
}
