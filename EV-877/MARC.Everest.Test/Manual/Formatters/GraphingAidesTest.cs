using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MARC.Everest;
using MARC.Everest.Xml;
using MARC.Everest.DataTypes;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.NE2008;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.Datatypes.R2;
using MARC.Everest.Test.Manual;
using System.IO;
using MARC.Everest.Test.Manual.Interfaces;

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for GraphingAides
    /// </summary>
    [TestClass]
    public class GraphingAidesTest
    {
        public GraphingAidesTest()
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

        /* Example 71 */

        /// <summary>
        /// Assigning a Graph Aide to a Formatter
        /// </summary>
        [TestMethod]
        public void GraphingAidesTest01()
        {
            Stream s = null;
            try
            {

                // Create an instance of the primary formatter
                IStructureFormatter formatter = new XmlIts1Formatter();

                Console.WriteLine("Number of graphing aides: {0}", formatter.GraphAides.Count());

                // Assign a graph aides
                formatter.GraphAides.Add(new DatatypeR2Formatter());

                // Make sure that the formatter has been properly assigned graphing aides
                Console.WriteLine("Number of graphing aides: {0}", formatter.GraphAides.Count());
                Console.WriteLine("Graphing Aide: {0}", formatter.GraphAides.ToString());

                // Test to ensure that a graphing aide has been assigned to the formatter.
                Assert.IsTrue(formatter.GraphAides.Count() > 0);
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
        }
    }
}
