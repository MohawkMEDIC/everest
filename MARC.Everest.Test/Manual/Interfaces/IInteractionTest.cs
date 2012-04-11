using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.IO.Pipes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MARC.Everest.Xml;
using MARC.Everest.DataTypes;
using MARC.Everest.Interfaces;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Test.Manual.Formatters;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.COCT_MT050000UV01;
using System.Reflection;

namespace MARC.Everest.Test.Manual_Interfaces
{
    /// <summary>
    /// Summary description for IIntereaction
    /// </summary>
    [TestClass]
    public class IInteractionTest
    {
        public IInteractionTest()
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

        public static string findResource(string neededResource)
        {
            foreach (string name in IInteractionTest.GetResourceList())
            {
                if (name.ToString().Contains("PRPA_IN101103CA.xml"))
                {
                    neededResource = name;
                }
            }
            return neededResource;
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

        /* Example 64 */

        /// <summary>
        /// This test will parse a valid instance of type 'IInteraction'.
        /// IsTrue assertion should return true.
        /// </summary>
        [TestMethod]
        public void IInteractionTest01()
        {
            // Find the resource to be parsed.
            string neededResource = findResource("PRPA_IN101103CA.xml");
            
            // Load the assembly into the current AppDomain
            Assembly.Load(new AssemblyName("MARC.Everest.RMIM.CA.R020402, Version=1.0.4366.42027, Culture=neutral"));

            // Initialize stream that will read the needed resource file.
            Stream s = null;

            try
            {
                // Set the stream by reading from a file
                // whose datatype is PRPA_IN101103CA
                s = GetResourceStream(neededResource);
                if (s == null)
                    Console.WriteLine("Invalid input stream.");
                
                // Setup the formatter as XML ITS 1 with Canadian Data Types R1
                var formatter = new XmlIts1Formatter();
                formatter.ValidateConformance = false;

                // Assign graphing aides to formatter
                formatter.GraphAides.Add(new DatatypeFormatter());

                var instance = formatter.ParseObject(s) as IInteraction;
                
                if (instance != null)   // instance is an interaction
                {
                    // Output instance information
                    Console.WriteLine("Interaction ID: {0}\r\nCreated On: {1}\r\nProcessing Mode Code: {2}\r\nVersion: {3}",
                        instance.InteractionId.Extension,
                        instance.CreationTime,
                        instance.ProcessingModeCode,
                        instance.VersionCode
                    );

                    // Ensure that we have a valid patient interaction instance
                    Assert.IsTrue(instance.InteractionId.Extension.StartsWith("PRPA"));
                }
                else
                {
                    // if instance is null, fail assertion
                    Assert.Fail();
                }
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
        }
    }
}
