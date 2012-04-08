using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;


namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for ENTest
    /// </summary>
    [TestClass]
    public class ENTest
    {
        public ENTest()
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
        public void ENPartTest()
        {
            EN en = new EN();
            en.Part.AddRange(GenerateNames());
            en.NullFlavor = null;
            Assert.IsTrue(en.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are nullified:
        ///     Part    : Gets the parts that make up this entity name
        /// </summary>    
        [TestMethod]
        public void ENNullFlavorTest()
        {
            EN en = new EN();
            en.NullFlavor = NullFlavor.NotAsked;
            en.Part.Clear();
            Assert.IsTrue(en.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When there are no values being populated:
        /// And, the following variables are nullified:
        ///     Part    : Gets the parts that make up this entity name
        ///     NullFlavor
        /// </summary>    
        [TestMethod]
        public void ENNullPartNullFlavorTest()
        {
            EN en = new EN();
            en.NullFlavor = null;
            en.Part.Clear();
            Assert.IsFalse(en.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     NullFlavor
        ///     Part    : Gets the parts that make up this entity name
        /// And, there are novariables being nullified:
        /// </summary>    
        [TestMethod]
        public void ENPartNullFlavorTest()
        {
            EN en = new EN();
            en.NullFlavor = NullFlavor.Other;
            en.Part.AddRange(GenerateNames());
            Assert.IsFalse(en.Validate());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private List<ENXP> GenerateNames()
        {
            List<ENXP> names = new List<ENXP>
            {
                new ENXP("John", EntityNamePartType.Given),
                new ENXP("Jangle", EntityNamePartType.Given),
                new ENXP("Smith", EntityNamePartType.Family)
            };
            return names;
        }
        
        /// <summary>
        /// Tests the ToString() method of the EN class. 
        /// 
        /// Checks to see if the representation of the EN object in string format is correct. 
        /// 
        /// Expected Result: "James T Kirk"
        /// </summary>
        [TestMethod]
        public void ENToString()
        {
            EN enInstance = new EN(
            EntityNameUse.Legal, new ENXP[] { 
            new ENXP("James", EntityNamePartType.Given), 
            new ENXP("T", EntityNamePartType.Given), 
            new ENXP("Kirk", EntityNamePartType.Family) });

            //Console.Write(enInstance.ToString("|" + "{GIV}{FAM}" + "|"));
            Assert.AreEqual(enInstance.ToString("{GIV}{FAM}") ,"James T Kirk");
        }
    }
}
