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
    /// Summary description for NDDPExamplesTest
    /// </summary>
    [TestClass]
    public class NDDPExamplesTest
    {
        public NDDPExamplesTest()
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

        /* Example 33 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Items       :   List of UVPs
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void NPPDExamplesTest01()
        {
            // create a new NPPD object
            NPPD<PQ> ageRange = new NPPD<PQ>
                (
                    // give NPPD values to graph
                    new UVP<PQ>[] {
                        new UVP<PQ>(new PQ(1,"a"), (decimal)0.1),
                        new UVP<PQ>(new PQ(2,"a"), (decimal)0.2),
                        new UVP<PQ>(new PQ(3,"a"), (decimal)0.45),
                        new UVP<PQ>(new PQ(4,"a"), (decimal)0.1),
                        new UVP<PQ>(new PQ(5,"a"), (decimal)0.15),
                    }
                );

            ageRange.NullFlavor = null;
            Console.WriteLine(ageRange.ToString());
            Assert.IsTrue(ageRange.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void NPPDExamplesTest02()
        {
            NPPD<PQ> ageRange = new NPPD<PQ>
                (
                    new UVP<PQ>[] {
                        new UVP<PQ>(new PQ(1,"a"), (decimal)0.1),
                        new UVP<PQ>(new PQ(2,"a"), (decimal)0.2),
                        new UVP<PQ>(new PQ(3,"a"), (decimal)0.45),
                        new UVP<PQ>(new PQ(4,"a"), (decimal)0.1),
                        new UVP<PQ>(new PQ(5,"a"), (decimal)0.15),
                    }
                );
            ageRange.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(ageRange.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Items       :   List of UVPs
        /// And the following values are Nullified:
        ///     NullFlavor
        ///     
        /// Testing if valid with empty UVP list.
        /// </summary>
        [TestMethod]
        public void NPPDExamplesTest03()
        {
            NPPD<PQ> ageRange = new NPPD<PQ>
                (
                    new UVP<PQ>[] {}
                );
            ageRange.NullFlavor = null;
            Assert.IsFalse(ageRange.Validate());
        }
    }
}
