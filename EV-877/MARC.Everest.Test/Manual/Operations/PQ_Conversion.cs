using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes.Converters;

namespace MARC.Everest.Test.Manual.Operations
{
    /// <summary>
    /// Summary description for PQ_Conversion
    /// </summary>
    [TestClass]
    public class PQ_Conversion
    {
        public PQ_Conversion()
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

        /* Example 27 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// converting from kilometers to centimeters.
        /// </summary>
        [TestMethod]
        public void PQTranslationsTest01()
        {
            PQ kilometers = new PQ(1, "km");
            try
            {
                Console.WriteLine(kilometers.Convert("cm"));
                // this throws an exception
            }
            catch (Exception)
            {
                Console.WriteLine("No converter was found!");
            }

            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            Console.WriteLine(kilometers.Convert("cm"));    // this results in 100,000 cm being printed
            Assert.IsTrue(kilometers.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// Converting from kilometers to centimeters.
        /// Testing if result from conversion is equal to original PQ.
        /// </summary>
        [TestMethod]
        public void PQTranslationsTest02()
        {
            PQ kilometers = new PQ(1, "km");
            try
            {
                Console.WriteLine(kilometers.Convert("cm"));
                // this throws an exception
            }
            catch (Exception)
            {
                Console.WriteLine("No converter was found!");
            }
            
            // add unit converter
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());

            // convert
            PQ centimeters = kilometers.Convert("cm");
            Console.WriteLine(centimeters);    // this results in 100,000 m being printed
            Assert.AreNotEqual(kilometers, centimeters);
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// Converting from kilometers to Kelvin.
        /// Testing if conversion is valid when converting illogical units
        ///     (eg. kilometers to Kelvin)
        /// </summary>
        [TestMethod]
        public void PQTranslationsTest03()
        {
            PQ kilometers = new PQ(1, "km");
            
            // add unit converter
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());

            try
            {
                // convert
                PQ temperature = kilometers.Convert("K");
                Console.WriteLine(temperature);    // this results in 100,000 m being printed
                Assert.IsFalse(temperature.Validate());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                // Make sure we get a 'cannot convert' exception message.
                Assert.IsTrue(e.ToString().Contains("Cannot convert"));
            }
        }
    }
}
