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
    /// Summary description for CEExamplesTest
    /// </summary>
    [TestClass]
    public class CEExamplesTest
    {
        public CEExamplesTest()
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

        /* Example 13 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code
        ///     Original Text
        ///     Translation
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CEExampleTest01()
        {
            CE<String> alternateVirus = new CE<string>();
            alternateVirus.Code = "284196006";
            alternateVirus.OriginalText = "External Virus - Black Oil";
            alternateVirus.Translation = new SET<CD<string>>(
                new CD<String>("X^0012", "2.16.3.234.34343.343")
                );
            alternateVirus.NullFlavor = null;
            Console.WriteLine(alternateVirus.Translation.ToString());
            Assert.IsTrue(alternateVirus.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Original Text
        ///     Translation
        /// And the following values are Nullified:
        ///     Code
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CEExampleTest02()
        {
            CE<String> alternateVirus = new CE<string>();
            alternateVirus.Code = null;
            alternateVirus.NullFlavor = null;
            alternateVirus.OriginalText = "External Virus - Black Oil";

            // code descriptor
            alternateVirus.Translation = new SET<CD<string>>(
                new CD<String>("X^0012", "2.16.3.234.34343.343")
                );
            Console.WriteLine(alternateVirus.Translation.ToString());
            Assert.IsFalse(alternateVirus.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Original Text
        ///     Code
        /// And the following values are Nullified:
        ///     Translation
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CEExampleTest03()
        {
            CE<String> alternateVirus = new CE<string>();
            alternateVirus.Code = "284196006";
            alternateVirus.OriginalText = "External Virus - Black Oil";
            alternateVirus.NullFlavor = null;
            alternateVirus.Translation = null;
            Assert.IsTrue(alternateVirus.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code
        /// And the following values are Nullified:
        ///     Translation
        ///     Original Text
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CEExampleTest04()
        {
            CE<String> alternateVirus = new CE<string>();
            alternateVirus.Code = "284196006";
            alternateVirus.OriginalText = null;
            alternateVirus.NullFlavor = null;
            alternateVirus.Translation = null;
            try
            {
                Console.WriteLine(alternateVirus.Translation.ToString());
            }
            catch
            {
                Console.WriteLine("Error: Cannot output translation because it is set to null.");
            }
            Assert.IsTrue(alternateVirus.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Code
        ///     Original Text
        ///     Translation
        /// And the following values are Nullified:
        ///     NullFlavor
        ///     
        /// Returns false because instance of translation is not valid (empty set of CD<string>s)
        /// </summary>
        [TestMethod]
        public void CEExampleTest05()
        {
            CE<String> alternateVirus = new CE<string>();
            alternateVirus.Code = "284196006";
            alternateVirus.OriginalText = "External Virus - Black Oil";
            alternateVirus.Translation = new SET<CD<string>>(
                new CD<String>()
                );
            alternateVirus.NullFlavor = null;
            Console.WriteLine(alternateVirus.Translation.ToString());
            Assert.IsFalse(alternateVirus.Validate());
        }
    }
}
