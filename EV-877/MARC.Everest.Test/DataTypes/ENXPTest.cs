using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for ENXPTest
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENXP"), TestClass]
    public class ENXPTest
    {
        public ENXPTest()
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
        /// Tests casting a EXNP to a string.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENXP"), TestMethod]
        public void ENXPCastToStringTest()
        {
            string enxpString = new ENXP("Test");
            Assert.AreEqual("Test", enxpString);
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value               : The value of the ENXP
        /// And, the following variables are nullified:
        ///     Code                : A code assigned to the name part by a coding system if applicable
        ///     CodeSystem          : The code system from whcih the code is taken
        ///     CodeSystemVersion   : The cersion of the coding system
        ///     NullFlavor          
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENXP"), TestMethod]
        public void ENXPValueTest()
        {
            ENXP x = new ENXP();
            x.Value = "Andria";
            x.Code = null;
            x.CodeSystem = null;
            x.CodeSystemVersion = null;
            x.NullFlavor = null;
            Assert.IsTrue(x.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     NullFlavor          
        /// And, the following variables are nullified:
        ///     Value               : The value of the ENXP
        ///     Code                : A code assigned to the name part by a coding system if applicable
        ///     CodeSystem          : The code system from whcih the code is taken
        ///     CodeSystemVersion   : The cersion of the coding system
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENXP"), TestMethod]
        public void ENXPNullFlavorTest()
        {
            ENXP x = new ENXP();
            x.Value = null;
            x.Code = null;
            x.CodeSystem = null;
            x.CodeSystemVersion = null;
            x.NullFlavor = NullFlavor.NotAsked;
            Assert.IsTrue(x.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When there are no values being populated:
        /// And, the following variables are nullified:
        ///     Value               : The value of the ENXP
        ///     Code                : A code assigned to the name part by a coding system if applicable
        ///     CodeSystem          : The code system from whcih the code is taken
        ///     CodeSystemVersion   : The cersion of the coding system
        ///     NullFlavor          
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENX"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pnull"), TestMethod]
        public void ENXPnullTest()
        {
            ENXP x = new ENXP();
            x.Value = null;
            x.Code = null;
            x.CodeSystem = null;
            x.CodeSystemVersion = null;
            x.NullFlavor = null;
            Assert.IsFalse(x.Validate());
        }

/// <summary>
/// Ensure that validation fails (return FALSE)
/// When the following values are being populated:
///     Value               : The value of the ENXP
///     NullFlavor          
/// And, the following variables are nullified:
///     Code                : A code assigned to the name part by a coding system if applicable
///     CodeSystem          : The code system from whcih the code is taken
///     CodeSystemVersion   : The cersion of the coding system
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENXP"), TestMethod]
public void ENXPValueNullFlavorTest()
{
    ENXP x = new ENXP();
    x.Value = "Andria";
    x.Code = null;
    x.CodeSystem = null;
    x.CodeSystemVersion = null;
    x.NullFlavor = NullFlavor.NotAsked;
    Assert.IsFalse(x.Validate());
}

        //[TestMethod]
        //public void ENXPValueCodeTest()
        //{
        //    ENXP x = new ENXP();
        //    x.Value = "Andria";
        //    x.Code = "whatever";
        //    x.CodeSystem = null;
        //    x.CodeSystemVersion = null;
        //    x.NullFlavor = null;
        //    Assert.IsTrue(x.Validate());
        //}

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value               : The value of the ENXP
        ///     Code                : A code assigned to the name part by a coding system if applicable
        ///     CodeSystem          : The code system from whcih the code is taken
        /// And, the following variables are nullified:
        ///     CodeSystemVersion   : The cersion of the coding system
        ///     NullFlavor          
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENXP"), TestMethod]
        public void ENXPValueCodeCodeSystemTest()
        {
            ENXP x = new ENXP();
            x.Value = "Andria";
            x.Code = "here";
            x.CodeSystem = "there";
            x.CodeSystemVersion = null;
            x.NullFlavor = null;
            Assert.IsTrue(x.Validate());
        }

        //[TestMethod]
        //public void ENXPValueCodeSytemTest()
        //{
        //    ENXP x = new ENXP();
        //    x.Value = "Andria";
        //    x.Code = null;
        //    x.CodeSystem = "whatever";
        //    x.CodeSystemVersion = null;
        //    x.NullFlavor = null;
        //    Assert.IsFalse(x.Validate());
        //}

        //[TestMethod]
        //public void ENXPCodeTest()
        //{
        //    ENXP x = new ENXP();
        //    x.Value = null;
        //    x.Code = "name";
        //    x.CodeSystem = null;
        //    x.CodeSystemVersion = null;
        //    x.NullFlavor = null;
        //    Assert.IsTrue(x.Validate());
        //}
    }
}
