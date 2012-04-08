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
    /// Summary description for ENExamplesTest
    /// </summary>
    [TestClass]
    public class ENExamplesTest
    {
        public ENExamplesTest()
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

        /* Example 38 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Parts       : sequence of entity name expression parts that comprise the name
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ENExampleTest01()
        {
            EN name = new EN
                (
                    EntityNameUse.Legal,
                    new ENXP[] {

                        // add name-parts to name
                        new ENXP("James", EntityNamePartType.Given),
                        new ENXP("Tiberius", EntityNamePartType.Given),
                        new ENXP("Kirk", EntityNamePartType.Family),
                    }
                );
            name.NullFlavor = null;
            Console.WriteLine(name.ToString("{FAM}, {GIV}"));
            // should output James Kirk Tieberius
            Assert.IsTrue(name.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values ARE Nullified:
        ///     Parts       : sequence of entity name expression parts that comprise the name
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ENExampleTest02()
        {
            EN name = new EN
                (
                    EntityNameUse.Legal,
                    new ENXP[] {}
                    );
            name.NullFlavor = null;
            Console.WriteLine(name.ToString("{FAM}, {GIV}"));
            // should output James Kirk Tieberius
            Assert.IsFalse(name.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Parts       : sequence of entity name expression parts that comprise the name
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ENExampleTest03()
        {
            EN name = new EN
                (
                    EntityNameUse.Legal,
                    new ENXP[] {

                        // add name-parts to the names
                        new ENXP("James", EntityNamePartType.Given),
                        new ENXP("Tiberius", EntityNamePartType.Given),
                        new ENXP("Kirk", EntityNamePartType.Family),
                    }
                );
            name.NullFlavor = NullFlavor.Other;
            Console.WriteLine(name.ToString("{FAM}, {GIV}"));
            // should output James Kirk Tieberius
            Assert.IsFalse(name.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullfied:
        ///     Parts       : sequence of entity name expression parts that comprise the name
        /// </summary>
        [TestMethod]
        public void ENExampleTest04()
        {
            EN name = new EN
                (
                    EntityNameUse.Legal,
                    new ENXP[] {}
                );
            name.NullFlavor = NullFlavor.Other;
            Console.WriteLine(name.ToString("{FAM}, {GIV}"));
            // should output James Kirk Tieberius
            Assert.IsTrue(name.Validate());
        }
    }
}
