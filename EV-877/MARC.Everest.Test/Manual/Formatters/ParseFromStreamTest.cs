using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using System.Reflection;
using MARC.Everest.RMIM.CA.R020402.POLB_MT004100CA;
using MARC.Everest.RMIM.CA.R020402.Interactions;

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for ParseFromStream01
    /// </summary>
    [TestClass]
    public class ParseFromStreamTest
    {
        public ParseFromStreamTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string[] GetResourceList()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceNames();
        }

        public static Stream GetResourceStream(string scriptname)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(scriptname);
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
        /// Parsing from a Stream.
        /// This test will grab an instance from an assembly resource
        /// file, parse the file, and verifiy if the result contains a
        /// valid instance of MCCI_IN000000UV01.
        /// 
        /// Xml Instance Parsed     :   MCCI_IN000000UV01
        /// Xml Instance Expected   :   MCCI_IN000000UV01
        /// Assertion should return TRUE.
        /// </summary>
        [TestMethod]
        public void StreamParseTest01()
        {
            // Find the resource to be parsed.
            string neededResource = "";
            foreach (string name in ParseFromStreamTest.GetResourceList())
            {
                if (name.ToString().Contains("MCCI_IN000000UV01.xml"))
                {
                    neededResource = name;
                }
            }

            // Load the assembly into the current AppDomain
            Assembly.Load(new AssemblyName("MARC.Everest.RMIM.UV.NE2008, Version=1.0.4366.42027, Culture=neutral"));

            // Initialize stream that will read the needed resource file.
            Stream s = null;

            try
            {
                // Set the stream by reading from a file
                // whose datatype is MCCI_IN000000UV01
                s = GetResourceStream(neededResource);
                if (s == null)
                    Console.WriteLine("Invalid input stream.");

                
                // Setup the formatter
                IStructureFormatter structureFormatter = new XmlIts1Formatter()
                {
                    ValidateConformance = false
                };

                // Add graphing aides
                structureFormatter.GraphAides.Add(new DatatypeFormatter());

                // Parse Resource Stream
                var result = structureFormatter.Parse(s);

                // Output the type of instance that was parsed
                Console.WriteLine("This file contains a '{0}' instance.", result.Structure.GetType().Name);

                Assert.IsTrue(result.Structure.GetType() == typeof(MCCI_IN000000UV01));
                Assert.IsTrue(result.Structure.GetType().Name == "MCCI_IN000000UV01");
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
        }



        /// <summary>
        /// Parsing from a Stream.
        /// This test will grab an instance from an assembly resource
        /// file, parse the file, and verifiy if the result contains a
        /// valid instance of MCCI_IN000000UV01.
        /// 
        /// Xml Instance Parsed     :   PRPA_IN101103CA
        /// Xml Instance Expected   :   MCCI_IN000000UV01
        /// Assertion should return FALSE.
        /// </summary>
        [TestMethod]
        public void StreamParseTest02()
        {
            // Find the resource to be parsed.
            string neededResource = "";
            foreach (string name in ParseFromStreamTest.GetResourceList())
            {
                if (name.ToString().Contains("PRPA_IN101103CA.xml"))
                {
                    neededResource = name;
                }
            }

            // Load the assembly into the current AppDomain
            Assembly.Load(new AssemblyName("MARC.Everest.RMIM.UV.NE2008, Version=1.0.4366.42027, Culture=neutral"));

            // Initialize stream that will read the needed resource file.
            Stream s = null;

            try
            {
                // Set the stream by reading from a file
                // whose datatype is MCCI_IN000000UV01
                s = GetResourceStream(neededResource);
                if (s == null)
                    Console.WriteLine("Invalid input stream.");

                // Setup the formatter
                IStructureFormatter structureFormatter = new XmlIts1Formatter()
                {
                    ValidateConformance = false
                };

                // Add graphing aides
                structureFormatter.GraphAides.Add(new DatatypeFormatter());

                // Parse Resource Stream
                var result = structureFormatter.Parse(s);

                // Output the type of instance that was parsed
                Console.WriteLine("This file contains a '{0}' instance.", result.Structure.GetType().Name);

                // Main assertion
                Assert.IsFalse(result.Structure.GetType() == typeof(MCCI_IN000000UV01));

                // Correct parsing verification.
                Assert.IsTrue(result.Structure.GetType() == typeof(PRPA_IN101103CA));
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
        }
    }
}
