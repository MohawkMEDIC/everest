using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for TNTest
    /// </summary>
    [TestClass]
    public class TNTest
    {
        public TNTest()
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
        /// Ensures that Validation fails (returns FALSE)
        /// When the following values are being populated:
        ///     Part        : Values are being added through 2 Variable strings: s1 & s2 to 
        ///                   be converted to a TN
        /// And, the following variables are Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void TNMultiplePartsTest()
        {
            TN a = new TN();
            String s1 = "Andria";
            String s2 = "Samples";
            a.Part.Add(new ENXP(s1));
            a.Part.Add(new ENXP(s2));
            a.NullFlavor = null;
            Assert.IsFalse(a.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When there are no values being populated:
        /// And, the following variables are Nullified:
        ///     Part        : Values are being cleared
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void TNNullTest()
        {
            TN a = new TN();
            a.NullFlavor = null;
            a.Part.Clear();
            Assert.IsFalse(a.Validate());            
        }

        /// <summary>
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are Nullified:
        ///     Part        : Values are being added cleard
        /// </summary> 
        [TestMethod]
        public void TNNullFlavorTest()
        {
            TN a = new TN();
            a.Part.Clear();
            a.NullFlavor = NullFlavor.NotAsked;
            Assert.IsTrue(a.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     Part        : Values are being added through 1 Variable string: s to 
        ///                   be converted to a TN
        /// And, the following variables are Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void TNPartTest()
        {
            TN a = new TN();
            String s = "Yoshi";
            
            a.Part.Add(new ENXP(s));
            a.NullFlavor = null;
            Assert.IsTrue(a.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When the following values are being populated:
        ///     Part        : Value are being added
        ///     NullFlavor
        /// And, there are no variables being Nullified:
        /// </summary> 
        [TestMethod]
        public void TNPartNullFlavorTest()
        {
            TN a = new TN();
            a.Part.Add(new ENXP("Hiro"));
            a.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(a.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     a        : a new TN instance being create with a specific value
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void TNPart2Test()
        {
            TN a = new TN("Yoshi");
            a.NullFlavor = null;
            Assert.IsTrue(a.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When the following values are being populated:
        ///     a        : a new TN instance being create with a specific value
        ///     NullFlavor
        /// And, there are no variables being Nullified:
        /// </summary> 
        [TestMethod]
        public void TNPart2NullFlavorTest()
        {
            TN a = new TN("Hiro");
            a.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(a.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     a        : a new TN instance being create with a specific value
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void TNUsePartTest()
        {
            TN a = new TN("Andy");
            a.NullFlavor = null;
            Assert.IsTrue(a.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When the following values are being populated:
        ///     a        : a new TN instance being create with a specific value
        ///     NullFlavor
        /// And, there are no variables being Nullified:
        /// </summary> 
        [TestMethod]
        public void TNUsePartNullFlavorTest()
        {
            TN a = new TN( "Andy");
            a.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(a.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     Part       : being added 
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENXP"), TestMethod]
        public void TNPartENXPTypeTest()
        {
            TN a = new TN();
            a.Part.Add(new ENXP("Yoshi", EntityNamePartType.Family));
            a.NullFlavor = null;
            Assert.IsFalse(a.Validate());
        }
    }
}
