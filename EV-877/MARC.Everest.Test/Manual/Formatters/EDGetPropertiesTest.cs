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

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for ED_GetProperties
    /// </summary>
    [TestClass]
    public class EDGetPropertiesTest
    {
        public EDGetPropertiesTest()
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
        /// Example 78
        /// Getting the supported properties of ED.
        /// 
        /// Determining of TRANSLATION is a supported property
        /// of the datatype formatter for the ED datatype.
        /// 
        /// Expecting Assertion test to return TRUE.
        /// </summary>
        [TestMethod]
        public void GetSupportedPropertiesTest01()
        {
            // Create the data type formatter
            IDatatypeStructureFormatter datatypeFormatter = new DatatypeFormatter();

            // Get supported properties for ED
            var supportedProperties =
                datatypeFormatter.GetSupportedProperties(typeof(ED));

            // Determine if Translation is supported
            bool isTranslationSupported =
                Array.Exists(supportedProperties, o => o.Name == "Translation");
            Console.WriteLine("ED.Translation is supported? {0}", isTranslationSupported);

            Assert.AreNotEqual(true, isTranslationSupported);

        }



        /// <summary>
        /// Getting the supported properties of ED.
        /// 
        /// Determining of TRANSLATION is a supported property
        /// of the datatype formatter for the ED datatype.
        /// 
        /// Expecting Assertion test to return TRUE.
        /// </summary>
        [TestMethod]
        public void GetSupportedPropertiesTest02()
        {
            // Create the data type formatter
            IDatatypeStructureFormatter datatypeFormatter = new DatatypeFormatter();

            // Get supported properties for ED
            var supportedProperties =
                datatypeFormatter.GetSupportedProperties(typeof(ED));

            // Determine if Translation is supported
            bool isTranslationSupported =
                Array.Exists(supportedProperties, o => o.Name == "Translation");
            Console.WriteLine("ED.Translation is supported? {0}", isTranslationSupported);

            // Same logic as test 1; rephrased for confirmation.
            Assert.AreEqual(false, isTranslationSupported);

        }


        /// <summary>
        /// Getting the supported properties of ED.
        /// 
        /// Determining of COMPRESSION is a supported property
        /// of the datatype formatter for the ED datatype.
        /// 
        /// Expecting Assertion test to return TRUE.
        /// </summary>
        [TestMethod]
        public void GetSupportedPropertiesTest03()
        {
            // Create the data type formatter
            IDatatypeStructureFormatter datatypeFormatter = new DatatypeFormatter();

            // Get supported properties for ED
            var supportedProperties =
                datatypeFormatter.GetSupportedProperties(typeof(ED));

            // Determine if Compression is supported
            bool isCompressionSupported =
                Array.Exists(supportedProperties, o => o.Name == "Compression");
            Console.WriteLine("ED.Compression is supported? {0}", isCompressionSupported);

            Assert.AreEqual(true, isCompressionSupported);
        }

        /// <summary>
        /// Getting the supported properties of ED.
        /// 
        /// Determining of COMPRESSION is a supported property
        /// of the datatype formatter for the ED datatype.
        /// 
        /// Expecting Assertion test to return TRUE.
        /// </summary>
        [TestMethod]
        public void GetSupportedPropertiesTest04()
        {
            // Create the data type formatter
            IDatatypeStructureFormatter datatypeFormatter = new DatatypeFormatter();

            // Get supported properties for ED
            var supportedProperties =
                datatypeFormatter.GetSupportedProperties(typeof(ED));

            // Determine if Compression is supported
            bool isCompressionSupported =
                Array.Exists(supportedProperties, o => o.Name == "Compression");
            Console.WriteLine("ED.Compression is supported? {0}", isCompressionSupported);

            // Same logic as test 3; rephrased for confirmation.
            Assert.AreNotEqual(false, isCompressionSupported);
        }
    }
}
