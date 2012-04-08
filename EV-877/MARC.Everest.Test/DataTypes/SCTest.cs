using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for SCTest
    /// </summary>
    [TestClass]
    public class SCTest
    {
        public SCTest()
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
        /// Testing Function Validate must returns TRUE
        /// When the following value is not Nullified:
        ///     Value               : The data to create SC with.
        /// And, the rest of the variables are Nullified:
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor              
        /// </summary> 
        [TestMethod]
        public void SCValueTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = null;
            sc.NullFlavor = null;
            Assert.IsTrue(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns TRUE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueCodeTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.NullFlavor = null;
            Assert.IsTrue(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns TRUE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        /// And, the rest of the variables are Nullified:
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = null;
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = null;
            sc.NullFlavor = null;
            Assert.IsTrue(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns TRUE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        /// And, the rest of the variables are Nullified:
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemCodeSystemNameTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = "SNOMEDCT";
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = null;
            sc.NullFlavor = null;
            Assert.IsTrue(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystemName      : The name of the code system
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemNameTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = null;
            sc.Code.CodeSystemName = "SNOMEDCT";
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = null;
            sc.NullFlavor = null;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns TRUE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        /// And, the rest of the variables are Nullified:
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemCodeSystemNameCodeSystemVersionTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = "SNOMEDCT";
            sc.Code.CodeSystemVersion = "2009-B1";
            sc.Code.DisplayName = null;
            sc.NullFlavor = null;
            Assert.IsTrue(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystemVersion   : The code system version
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemVersionTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = null;
            sc.Code.CodeSystemName = null;
            sc.Code.CodeSystemVersion = "2009-B1";
            sc.Code.DisplayName = null;
            sc.NullFlavor = null;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemNameCodeSystemVersionTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = null;
            sc.Code.CodeSystemName = "SNOMEDCT";
            sc.Code.CodeSystemVersion = "2009-B1";
            sc.Code.DisplayName = null;
            sc.NullFlavor = null;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns TRUE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemCodeSystemNameCodeSystemVersionDisplayNameTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = "SNOMEDCT";
            sc.Code.CodeSystemVersion = "2009-B1";
            sc.Code.DisplayName = "Burn of skin";
            sc.NullFlavor = null;
            Assert.IsTrue(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns TRUE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     DisplayName         : A name or title for the code.
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueCodeDisplayNameTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = null;
            sc.Code.CodeSystemName = null;
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = "Burn of skin";
            sc.NullFlavor = null;
            Assert.IsTrue(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     DisplayName         : A name or title for the code.
        /// And, the rest of the variables are Nullified:
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCValueDisplayNameTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = new CD<string>() { NullFlavor = NullFlavor.NotApplicable };
            sc.NullFlavor = null;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns TRUE
        /// When the following values are not Nullified:
        ///     NullFlavor           
        /// And, the rest of the variables are Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        /// </summary> 
        [TestMethod]
        public void SCNullFlavorTest()
        {
            SC sc = new SC();
            sc.Value = null;
            sc.Code = null;

            sc.NullFlavor = NullFlavor.NotAsked;
            Assert.IsTrue(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     NullFlavor           
        /// And, the rest of the variables are Nullified:
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        /// </summary> 
        [TestMethod]
        public void SCValueNullFlavorTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = null;

            sc.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     NullFlavor           
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        /// </summary> 
        [TestMethod]
        public void SCValueCodeNullFlavorTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = null;
            sc.Code.CodeSystemName = null;
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = null;
            sc.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     NullFlavor           
        /// And, the rest of the variables are Nullified:
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemNullFlavorTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = null;
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = null;
            sc.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     NullFlavor           
        /// And, the rest of the variables are Nullified:
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemTestCodeSystemNameNullFlavorTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = "SNOMEDCT";
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = null;
            sc.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     NullFlavor           
        /// And, the rest of the variables are Nullified:
        ///     DisplayName         : A name or title for the code.
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemTestCodeSystemNameCodeSystemVersionNullFlavorTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = "SNOMEDCT";
            sc.Code.CodeSystemVersion = "2009-B1";
            sc.Code.DisplayName = null;
            sc.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// And, No variables are Nullified:
        /// </summary> 
        [TestMethod]
        public void SCValueCodeCodeSystemTestCodeSystemNameCodeSystemVersionDisplayNameNullFlavorTest()
        {
            SC sc = new SC();
            sc.Value = "Hello";
            sc.Code = "284196006";
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = "SNOMEDCT";
            sc.Code.CodeSystemVersion = "2009-B1";
            sc.Code.DisplayName = "Burn of skin";
            sc.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Code                : The plain code symbol defined by the code system.
        /// And, the rest of the variables are Nullified:
        ///     Value               : The data to create SC with.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCCodeTest()
        {
            SC sc = new SC();
            sc.Value = null;
            sc.Code = "284196006";
            sc.Code.CodeSystem = null;
            sc.Code.CodeSystemName = null;
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = null;
            sc.NullFlavor = null;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        /// And, the rest of the variables are Nullified:
        ///     Value               : The data to create SC with.
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCCodeSystemTest()
        {
            SC sc = new SC();
            sc.Value = null;
            sc.Code = new CD<string>();
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = null;
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = null;
            sc.NullFlavor = null;
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Code                : The plain code symbol defined by the code system.
        /// And, the rest of the variables are Nullified:
        ///     Value               : The data to create SC with.
        /// </summary> 
        [TestMethod]
        public void SCCodeCodeSystemNullValueTest()
        {
            SC sc = new SC();
            sc.Value = null;
            sc.Code = "284196006";
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Code                : The plain code symbol defined by the code system.
        ///     Value               : The value of the SC
        ///     Code.OriginalText   : Original Text
        /// </summary> 
        [TestMethod]
        public void SCCodeOriginalTextTest()
        {
            SC sc = new SC();
            sc.Value = null;
            sc.Code = "284196006";
            sc.Code.OriginalText = "Hello!";
            Assert.IsFalse(sc.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE
        /// When the following values are not Nullified:
        ///     Code                : The plain code symbol defined by the code system.
        ///     CodeSystem          : The OID representing the system from which the code was drawn from
        /// And, the rest of the variables are Nullified:
        ///     Value               : The data to create SC with.
        ///     CodeSystemName      : The name of the code system
        ///     CodeSystemVersion   : The code system version
        ///     DisplayName         : A name or title for the code.
        ///     NullFlavor           
        /// </summary> 
        [TestMethod]
        public void SCCodeCodeSystemTest()
        {
            SC sc = new SC();
            sc.Value = null;
            sc.Code = "284196006";
            sc.Code.CodeSystem = "2.16.840.1.113883.6.96";
            sc.Code.CodeSystemName = null;
            sc.Code.CodeSystemVersion = null;
            sc.Code.DisplayName = null;
            sc.NullFlavor = null;
            Assert.IsFalse(sc.Validate());
        }


    }
}
