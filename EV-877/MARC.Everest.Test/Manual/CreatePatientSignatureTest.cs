using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.Manual
{
    /// <summary>
    /// Summary description for CreatePatientSignatureTest
    /// </summary>
    [TestClass]
    public class CreatePatientSignatureTest
    {
        public CreatePatientSignatureTest()
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

        /*
        [TestMethod]
        /// <summary>
        /// Example 66
        /// Creates a patient structure.
        /// This code is only demonstrational - no assertion.
        /// </summary>
        /// <param name="id">The unique identifier</param>
        /// <param name="name">The name of the patient</param>
        /// <param name="addr">The primary address</param>
        /// <param name="telecom">A primary telecom</param>
        /// <returns>A constructed patient structure</returns>
        public static MARC.Everest.RMIM.UV.NE2008.COCT_MT050000UV01.Patient CreatePatient(
                II id,
                EN name,
                AD addr,
                TEL telecom
            )
        {
            return null;
        }
        */
    }
}
