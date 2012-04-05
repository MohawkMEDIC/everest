using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test
{
    /// <summary>
    /// Summary description for PNTest
    /// </summary>
    [TestClass]
    public class PNTest
    {
        public PNTest()
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
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Part    : Gets the parts that make up this entity name
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PNPartTest()
        {
            PN pn = new PN();
            pn.Part.Add(new ENXP("Andria"));
            pn.NullFlavor = null;
            Assert.IsTrue(pn.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Part    : Gets the parts that make up this entity name
        ///               It is being added twice
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PNMultiplePartTest()
        {
            PN pn = new PN();
            pn.Part.Add(new ENXP("Andria"));
            pn.Part.Add(new ENXP("Oscar"));
            pn.NullFlavor = null;
            Assert.IsTrue(pn.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are nullified:
        ///     Part    : Gets the parts that make up this entity name
        /// </summary>
        [TestMethod]
        public void PNNullFlavorTest()
        {
            PN pn = new PN();
            pn.Part.Clear();
            pn.NullFlavor = NullFlavor.NotAsked;
            Assert.IsTrue(pn.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When there are no values being populated:
        /// And, the following variables are nullified:
        ///     Part    : Gets the parts that make up this entity name
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PNNullTest()
        {
            PN pn = new PN();
            pn.Part.Clear();
            pn.NullFlavor = null;
            Assert.IsFalse(pn.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Part    : Gets the parts that make up this entity name
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PNValidQualifierTest()
        {
            PN pn = new PN();
            ENXP enxp = new ENXP();
            enxp.Value = "Andria";
            enxp.Qualifier = new SET<CS<EntityNamePartQualifier>>(EntityNamePartQualifier.Nobility);
            pn.Part.Add(enxp);
            pn.NullFlavor = null;
            Assert.IsTrue(pn.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Part    : Gets the parts that make up this entity name
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PNInvalidQualifierTest()
        {
            PN pn = new PN();
            ENXP enxp = new ENXP();
            enxp.Value = "Andria";
            enxp.Qualifier = new SET<CS<EntityNamePartQualifier>>(EntityNamePartQualifier.LegalStatus);
            pn.Part.Add(enxp);
            pn.NullFlavor = null;
            Assert.IsFalse(pn.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Part    : Gets the parts that make up this entity name
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PNNullQualifierTest()
        {
            PN pn = new PN();
            ENXP enxp = new ENXP();
            enxp.Value = "Andria";
            enxp.Qualifier = null;
            pn.Part.Add(enxp);
            pn.NullFlavor = null;
            Assert.IsTrue(pn.Validate());
        }
    }
}
