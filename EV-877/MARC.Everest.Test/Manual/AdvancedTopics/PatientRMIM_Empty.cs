using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MARC.Everest;
using MARC.Everest.Xml;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.NE2008;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.COCT_MT050000UV01;    // used to create new Patient
using MARC.Everest.RMIM.UV.NE2008.COCT_MT030000UV04;
using MARC.Everest.RMIM.CA.R020402.Interactions;
using MARC.Everest.RMIM.CA.R020403.REPC_MT500005CA;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MARC.Everest.Test.Manual.AdvancedTopics
{
    /// <summary>
    /// Summary description for PatientRMIM_Empty
    /// </summary>
    [TestClass]
    public class PatientRMIM_Empty
    {
        public PatientRMIM_Empty()
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

        [TestMethod]
        /// <summary>
        /// Example 68
        /// Creates a patient structure
        /// </summary>
        /// <param name="id">The unique identifier</param>
        /// <param name="name">The name of the patient</param>
        /// <param name="addr">The primary address</param>
        /// <param name="telecom">A primary telecom</param>
        /// <returns>A constructed patient structure</returns>
        public Patient CreatePatient(
                II id,
                EN name,
                AD addr,
                TEL telecom
            )
        {
            // Instantiate the object
            var retVal = new Patient();

            // return a value in a unit test
            // will result in Not Runnable
            return retVal;
        }
    }
}
