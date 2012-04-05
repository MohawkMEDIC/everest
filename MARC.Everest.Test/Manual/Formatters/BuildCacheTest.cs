using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MARC.Everest.Xml;
using System.Xml;

using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.RMIM.UV.NE2008.RCMR_MT000001UV02;


namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for BuildCache
    /// </summary>
    [TestClass]
    public class BuildCacheTest
    {
        public BuildCacheTest()
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
        /// Example 77
        /// Using the BuildCache Method.
        /// This manual example is theoretical.
        /// </summary>
        [TestMethod]
        public void BuildCacheTest01()
        {
            /*
            // Create an instance of the primary formatter
            ICodeDomStructureFormatter formatter = new XmlIts1Formatter();
            formatter.GenerateInMemory = false;

            // Assign a graph aide
            formatter.GraphAides.Add(new DatatypeFormatter()
            {
                CompatibilityMode = DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture
            });

            // Let the user know we're initializing
            Console.WriteLine("Please wait, initializing...");

            // Build the ClinicalDocument type cache
            formatter.BuildCache(new Type[] { typeof(ClinicalDocument) });

            // Continue with the program here
            Console.WriteLine("Initialized, Program is continuing...");
            */
        }
    }
}
