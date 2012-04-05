using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for PQRTest
    /// </summary>
    [TestClass]
    public class PQRTest
    {
        public PQRTest()
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
        ///     Value       : The value of the measurement
        ///     Code        : The code of the unit of measure
        ///     CodeSystem  : The code system the unit of measure was drawn from
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PQRValueCodeCodeSystemTest()
        {
            PQR pqr = new PQR();
            pqr.Value = (decimal)3.456;
            pqr.Code = "";
            pqr.CodeSystem = "";
            pqr.NullFlavor = null;
            Assert.IsTrue(pqr.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Value       : The value of the measurement
        ///     Code        : The code of the unit of measure
        ///     CodeSystem  : The code system the unit of measure was drawn from
        ///     NullFlavor
        /// And, there are no variables being nullified:
        /// </summary>
        [TestMethod]
        public void PQRValueNullFlavorTest()
        {
            PQR pqr = new PQR();
            pqr.Value = (decimal)3.456;
            pqr.Code = "";
            pqr.CodeSystem = "";
            pqr.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(pqr.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When there are no values being populated:
        /// And, the following variables are nullified:
        ///     Value       : The value of the measurement
        ///     Code        : The code of the unit of measure
        ///     CodeSystem  : The code system the unit of measure was drawn from
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PQRNullTest()
        {
            PQR pqr = new PQR();
            pqr.Value = null;
            pqr.Code = null;
            pqr.NullFlavor = null;
            Assert.IsFalse(pqr.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are nullified:
        ///     Value       : The value of the measurement
        ///     Code        : The code of the unit of measure
        ///     CodeSystem  : The code system the unit of measure was drawn from
        /// </summary>
        [TestMethod]
        public void PQRNullFlavorTest()
        {
            PQR pqr = new PQR();
            pqr.Value = null;
            pqr.Code = null;
            pqr.NullFlavor = NullFlavor.NoInformation;
            Assert.IsTrue(pqr.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value       : The value of the measurement
        ///     Code        : The code of the unit of measure
        /// And, the following variables are nullified:
        ///     CodeSystem  : The code system the unit of measure was drawn from
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PQRValueCodeTest()
        {
            PQR pqr = new PQR();
            pqr.Value = (decimal)4.57332;
            pqr.Code = "";
            pqr.CodeSystem = null;
            pqr.NullFlavor = null;
            Assert.IsTrue(pqr.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Value       : The value of the measurement
        /// And, the following variables are nullified:
        ///     Code        : The code of the unit of measure
        ///     CodeSystem  : The code system the unit of measure was drawn from
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PQRValueTest()
        {
            PQR pqr = new PQR();
            pqr.Value = (decimal)4.56323;
            pqr.Code = null;
            pqr.CodeSystem = null;
            pqr.NullFlavor = null;
            Assert.IsFalse(pqr.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value       : The value of the measurement
        ///     Code        : The code of the unit of measure
        ///     CodeSystem  : The code system the unit of measure was drawn from
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PQRValueCodeCodeSystem2Test()
        {
            Double value = 3.4567;
            String code = "";
            String codesystem = "";
            PQR pqr = new PQR((decimal)value, code, codesystem);
            pqr.NullFlavor = null;
            Assert.IsTrue(pqr.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value       : The value of the measurement
        ///     Code        : The code of the unit of measure
        ///     CodeSystem  : The code system the unit of measure was drawn from
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PQRValueCodeCodeSystem3Test()
        {
            PQR pqr = new PQR((decimal)2.345465, "", "");
            pqr.NullFlavor = null;
            Assert.IsTrue(pqr.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value       : The value of the measurement
        ///     Code        : The code of the unit of measure
        ///     Precision   : The precision of the PQR
        /// And, the following variables are nullified:
        ///     CodeSystem  : The code system the unit of measure was drawn from
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PQRValueCode2Test()
        {
            Double value = 3.42346;
            String code = "";
            String codesystem = null;
            PQR pqr = new PQR((decimal)value, code, codesystem);
            pqr.NullFlavor = null;
            pqr.Precision = 2;
            Assert.IsTrue(pqr.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Value       : The value of the measurement
        ///     Code        : The code of the unit of measure
        /// And, the following variables are nullified:
        ///     CodeSystem  : The code system the unit of measure was drawn from
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void PQRValueCode3Test()
        {
            PQR pqr = new PQR((decimal)7.4322, "", null);
            pqr.NullFlavor = null;
            Assert.IsTrue(pqr.Validate());
        }
    }
}
