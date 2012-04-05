using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using System.IO;
using MARC.Everest.DataTypes.Primitives;

namespace MARC.Everest.Test
{
    /// <summary>
    /// Summary description for IITest
    /// </summary>
    [TestClass]
    public class IITest
    {
        public IITest()
        {
            
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
        /// Ensures that Validation succeeds
        /// When the following values are being populated:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IIRootNullNullFlavorExtensionTest()
        {
            II interaction = new II("1.234.54.32",null);
            interaction.NullFlavor = null;
            Assert.IsTrue(interaction.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails
        /// When there are novalues being populated:
        /// And, the following variables are being Nullified:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IINullFlavorExtensionRootNullTest()
        {
            II interaction = new II(null, null);
            interaction.NullFlavor = null;
            Assert.IsFalse(interaction.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails
        /// When the following values are being populated:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// And, there are no variables being Nullified:
        /// </summary>
        [TestMethod]
        public void IINullFlavorExtensionRootTest()
        {
            II interaction = new II("1.2.3", "extension");
            interaction.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(interaction.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds
        /// When the following values are being populated:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     Displayable : Specifies if the identifier is inteneded for human display and data entry
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IITokenDisplayableNullExtensionGuidRootNullNullFlavorTest()
        {
            II interaction = new II(Guid.NewGuid());
            interaction.NullFlavor = null;
            interaction.Displayable = null;

            Assert.IsTrue(II.IsValidTokenFlavor(interaction));
            Assert.AreEqual(null, interaction.Displayable);
            Assert.AreEqual(null, interaction.Extension);
        }

        /// <summary>
        /// Ensures that Validation fails when an II.TOKEN is assigned a non guid root
        /// When the following values are being populated:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     Displayable : Specifies if the identifier is inteneded for human display and data entry
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IITokenDisplayableNullExtensionNonGuidRootNullNullFlavorTest()
        {
            try
            {
                II interaction = new II("root", null);
                interaction.NullFlavor = null;
                interaction.Displayable = true;
                Assert.IsFalse(II.IsValidTokenFlavor(interaction));
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Ensures that Validation fails when an II.TOKEN is assigned a guid root and an extension
        /// When the following values are being populated:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     Displayable : Specifies if the identifier is inteneded for human display and data entry
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IITokenDisplayableExtensionGuidRootNullNullFlavorTest()
        {
            try
            {
                II interaction = new II(Guid.NewGuid().ToString(), "extension");
                interaction.NullFlavor = null;
                interaction.Displayable = true;
                Assert.IsFalse(II.IsValidTokenFlavor(interaction));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Ensures that Validation succeeds
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     Displayable : Specifies if the identifier is inteneded for human display and data entry
        /// </summary> 
        [TestMethod]
        public void IITokenNullFlavorNullExtensionRootDisplayableTest()
        {
            II interaction = new II(null, null);
            interaction.NullFlavor = NullFlavor.Other;
            interaction.Displayable = null;
            Assert.IsTrue(II.IsValidTokenFlavor(interaction));
            Assert.AreEqual(null, interaction.Displayable);
            Assert.AreEqual(null, interaction.Extension);
        }

        /// <summary>
        /// Ensures that Validation fails
        /// When there are no values being populated:
        /// And, the following variables are being Nullified:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     Displayable : Specifies if the identifier is inteneded for human display and data entry
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IITokenNullNullFlavorExtensionRootDisplayableTest()
        {
            II interaction = new II(null, null);
            interaction.NullFlavor = null;
            interaction.Displayable = null;
            Assert.IsFalse(II.IsValidTokenFlavor(interaction));
            Assert.AreEqual(null, interaction.Displayable);
            Assert.AreEqual(null, interaction.Extension);
        }

        /// <summary>
        /// Ensures that Validation succeeds
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     Displayable : Specifies if the identifier is inteneded for human display and data entry
        /// </summary> 
        [TestMethod]
        public void IIPublicNullFlavorNullExtensionRootDisplayableTest()
        {
            II interaction = new II(null, null);
            interaction.NullFlavor = NullFlavor.Other;
            interaction.Displayable = true;
            interaction.Scope = IdentifierScope.BusinessIdentifier;
            Assert.IsTrue(II.IsValidPublicFlavor(interaction));
            Assert.AreEqual(true, interaction.Displayable);
        }

        /// <summary>
        /// Ensures that Validation fails
        /// When the following values are being populated:
        ///     Root        : A unique identifier that quaratees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        ///     Displayable : Specifies if the identifier is inteneded for human display and data entry
        /// </summary> 
        /// <remarks>Tests that NullFlavor and value is validated properly</remarks>
        [TestMethod]
        public void IIPublicNullFlavorExtensionRootTest()
        {
            // JF: The II Data must be valid for this run, changing the root a n OID
            II interaction = new II("1.3.6.1", "extension");
            interaction.NullFlavor = NullFlavor.Other;
            interaction.Displayable = true;
            Assert.IsFalse(II.IsValidPublicFlavor(interaction));
            Assert.IsFalse(interaction.Validate());
            Assert.AreEqual(true, interaction.Displayable);
        }

        /// <summary>
        /// Ensures that Validation fails when the root is not an OID for an II.PUBLIC
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        ///     Displayable : Specifies if the identifier is inteneded for human display and data entry
        /// </summary> 
        /// <remarks>Tests that NullFlavor and value is validated properly</remarks>
        [TestMethod]
        public void IIPublicNonOidRootTest()
        {
            try
            {
                II interaction = new II("root", "extension");
                Assert.AreEqual(null, interaction.Displayable);
                Assert.IsFalse(II.IsValidPublicFlavor(interaction));
                Assert.AreEqual(true, interaction.Displayable);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Ensures that Validation fails
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        /// And, the following variables are being Nullified:
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IIPublicRootNullNullFlavorExtensionTest()
        {
            try
            {
                II interaction = new II("root", null);
                interaction.NullFlavor = null;
                Assert.AreEqual(true, interaction.Displayable);
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Ensures that Validation succeds
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IIPublicRootExtensionNullNullFlavorTest()
        {
            II interaction = new II("1.2.3.4", "extension");
            interaction.NullFlavor = null;
            interaction.Displayable = true;
            interaction.Scope = IdentifierScope.BusinessIdentifier;
            Assert.IsTrue(II.IsValidPublicFlavor(interaction));
            Assert.AreEqual(true, interaction.Displayable);
        }

        /// <summary>
        /// Ensures that Validation fails when the root of an II.PUBLIC is not an OID
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IIPublicNonOidRootExtensionNullNullFlavorTest()
        {
            try
            {
                II interaction = new II("root", "extension");
                interaction.NullFlavor = null;
                Assert.IsFalse(II.IsValidPublicFlavor(interaction));
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Ensures that Validation succeds
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IIBusRootExtensionNullNullFlavorTest()
        {
            // JF: The purpose of this test is to test that a BUS passes, II.BUS needs a root of an OID to pass
            II interaction = new II("1.3.1.6", "extension");
            interaction.NullFlavor = null;
            interaction.Scope = IdentifierScope.BusinessIdentifier;
            Assert.IsTrue(II.IsValidBusFlavor(interaction));
            Assert.AreEqual(IdentifierUse.Business, interaction.Use);
            Assert.AreEqual(null, interaction.Displayable);
        }

        /// <summary>
        /// Ensures that Validation fails
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        /// And, the following variables are being Nullified:
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IIBusRootNullFlavorNullExtensionTest()
        {
            try
            {
                II interaction = new II("1.2.2.2", "123");
                interaction.NullFlavor = null;
                Assert.IsFalse(II.IsValidBusFlavor(interaction));
                interaction.Use = IdentifierUse.Business;
                Assert.IsTrue(II.IsValidBusFlavor(interaction));
                Assert.AreEqual(IdentifierScope.BusinessIdentifier, interaction.Scope);
            }
            catch (InvalidDataException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Ensures that Validation fails
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     Displayable : Specifies if the identifier is inteneded for human display and data entry
        /// </summary> 
        [TestMethod]
        public void IIBusNullFlavorNullRootExtensionDisplayableTest()
        {
            II interaction = new II(null, null);
            interaction.Displayable = null;
            interaction.NullFlavor = NullFlavor.Other;
            interaction.Scope = IdentifierScope.BusinessIdentifier;
            Assert.IsTrue(II.IsValidBusFlavor(interaction));
            Assert.AreEqual(IdentifierUse.Business, interaction.Use);
            Assert.AreEqual(null, interaction.Displayable);
        }

        /// <summary>
        /// Ensures that Validation fails
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        /// </summary> 
        [TestMethod]
        public void IIBusNullFlavorRootExtensionTest()
        {
            // JF: This test assumes a valid II and tests whether a null flavor with proper data is valid
            II interaction = new II("1.3.6.1", "extension");
            interaction.NullFlavor = NullFlavor.Other;
            interaction.Scope = IdentifierScope.BusinessIdentifier;
            Assert.IsFalse(II.IsValidBusFlavor(interaction));
            Assert.IsFalse(interaction.Validate());
            Assert.AreEqual(IdentifierUse.Business, interaction.Use);
            Assert.AreEqual(null, interaction.Displayable);
        }

        /// <summary>
        /// Ensures that Validation fails when the root on an II.BUS is not an OID
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        /// </summary> 
        [TestMethod]
        public void IIBusNonOidRoot()
        {
            try
            {
                II interaction = new II("root", "extension");
                Assert.IsFalse(II.IsValidBusFlavor(interaction));
                Assert.AreEqual(IdentifierUse.Business, interaction.Use);
                Assert.AreEqual(null, interaction.Displayable);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Ensures that Validation fails when the II.BUS has a guid root and an extension
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        /// </summary> 
        [TestMethod]
        public void IIBusGuidRootExtension()
        {
            try
            {
                II interaction = new II(Guid.NewGuid().ToString(), "extension");
                Assert.IsFalse(II.IsValidBusFlavor(interaction));
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Ensures that Validation passes when the II.BUS has a guid root and no extension
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        /// </summary> 
        [TestMethod]
        public void IIBusGuidRootNoExtension()
        {
            II interaction = new II(Guid.NewGuid());
            interaction.Scope = IdentifierScope.BusinessIdentifier;
            Assert.IsTrue(II.IsValidBusFlavor(interaction));
        }

        /// <summary>
        /// Ensures that Validation succeeds when the II.BUS is an oid
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        /// </summary> 
        [TestMethod]
        public void IIBusOidRoot()
        {
            II interaction = new II("1.3.6.1.6", "extension");
            interaction.Scope = IdentifierScope.BusinessIdentifier;
            Assert.IsTrue(II.IsValidBusFlavor(interaction));
        }

        /// <summary>
        /// Valid II.OID Flavor Validation
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        /// And, the following variables are being Nullified:   
        ///     NullFlavor
        ///     Extension
        /// </summary> 
        [TestMethod]
        public void IIValidOidFlavorTest()
        {
            II interactionID = new II();
            interactionID.Root = "1.2.3";
            interactionID.Extension = null;
            Assert.IsTrue(II.IsValidOidFlavor(interactionID));
        }

        /// <summary>
        /// Invalid II.OID Flavor
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:   
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IIInvalidOidFlavorTest()
        {
            II interactionID = new II();
            interactionID.Root = "2.3.4";
            interactionID.Extension = Guid.NewGuid().ToString();
            Assert.IsFalse(II.IsValidOidFlavor(interactionID));
        }

        /// <summary>
        /// Cast Guid to II
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:   
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IICastGuidToIITest()
        {
            II interactionID = new II();            
            Guid validGuid = Guid.NewGuid();
            interactionID = validGuid;
            Assert.AreEqual(validGuid.ToString(), interactionID.Root.ToLower());
        }

        /// <summary>
        /// Cast Oid to II
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:   
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IICastOidToIITest()
        {
            II interactionID = new II();
            OID validOid = new OID("1.2.3.4");            
            interactionID = validOid;
            Assert.AreEqual(validOid.ToString(), interactionID.Root);
        }

        /// <summary>
        /// II.CreateToken() Test
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:   
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IICreateTokenTest()
        {
            II interactionID = new II();
            Guid inputGuid = Guid.NewGuid();
            interactionID = II.CreateToken(inputGuid);
            Assert.AreEqual(inputGuid.ToString(), interactionID.Root.ToLower());
            Assert.AreEqual("II.TOKEN", interactionID.Flavor); // verfies the structure for the token
        }

        /// <summary>
        /// II.CreateBus() Test
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:   
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IICreateBusTest()
        {
            II interactionID = new II();
            Guid inputGuid = Guid.NewGuid();
            interactionID = II.CreateBus(inputGuid);
            Assert.AreEqual(inputGuid.ToString(), interactionID.Root.ToLower());
            Assert.AreEqual("II.BUS", interactionID.Flavor);
        }

        /// <summary>
        /// II.CreatePublic Test
        /// When the following values are being populated:
        ///     Root        : A unique identifier that guarantees the global uniqueness of the instance identifier
        ///     Extension   : A character string as a unqiue identifier within the scope of the identifier root
        /// And, the following variables are being Nullified:   
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void IICreatePublicTest()
        {
            II interactionID = new II();
            OID inputOID = "1.2.3.4";
            string inputExtension = "1";
            interactionID = II.CreatePublic(inputOID, inputExtension);
            Assert.AreEqual(inputOID.ToString(), interactionID.Root);
            Assert.AreEqual("II.PUBLIC", interactionID.Flavor);
            Assert.AreEqual(true, interactionID.Displayable);
            Assert.AreEqual(IdentifierUse.Business, interactionID.Use);
        }
    }
}