using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Primitives;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for IIExamplesTest
    /// </summary>
    [TestClass]
    public class IIExamplesTest
    {
        public IIExamplesTest()
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

        /* Example 5 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Root        :   Given OID
        ///     Extension   :   Given extension
        /// And the following are Nullified
        ///     Nullflavor
        ///     
        /// Testing if valid given roots (OIDs) and extensions
        /// </summary>
        [TestMethod]
        public void IIExampleTest01()
        {
            II rootExtension = new II(new OID("1.2.3.4.5"), "1234");
            II rootExtension2 = new II("1.2.3.4.5.6", "1234");
            rootExtension.NullFlavor = null;
            rootExtension2.NullFlavor = null;
            Console.WriteLine("1= {0}, 2={1}", rootExtension.Root, rootExtension2.Root);
            Assert.IsTrue(rootExtension.Validate());
            Assert.IsTrue(rootExtension2.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Root        :   Given Object Identifier
        /// And the following are Nullified
        ///     Extension   :   Given extension
        ///     Nullflavor
        ///     
        /// Testing if valid given roots (GUIDs) and extensions
        /// </summary>
        [TestMethod]
        public void IIExampleTest02()
        {
            II token = new II(Guid.NewGuid());
            II token2 = Guid.NewGuid();
            token.Extension = null;
            token2.Extension = null;
            token.NullFlavor = null;
            token2.NullFlavor = null;
            Assert.IsTrue(token.Validate());
            Assert.IsTrue(token2.Validate());
        }

        /* Should fail with root and nullflavor */

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Root        :   Given Object Identifier
        ///     Extension   :   Given extension
        ///     Nullflavor
        ///     
        /// Testing if valid with root and NullFlavor for object
        /// </summary>
        [TestMethod]
        public void IIExampleTest03()
        {
            II rootExtension = new II(new OID("1.2.3.4.5.6"), "1234");
            rootExtension.NullFlavor = NullFlavor.Unknown;
            Assert.IsFalse(rootExtension.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Root        :   Given Globally Unique Identifier
        ///     Nullflavor  :   NoInformation
        ///     
        /// Testing if valid with root and nullflavor for token
        /// </summary>
        [TestMethod]
        public void IIExampleTest04()
        {
            II token = new II(Guid.NewGuid());
            token.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(token.Validate());
        }


        /* ------------ MAY BE A BUG ------------
         * Extension should have to be populated.
         */
        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Root        :   Given Object Identifier
        /// And the following values are Nullified:
        ///     Extension   :   Given extension
        ///     Nullflavor
        /// Testing if oid flavor is valid given ONLY root.
        /// </summary>
        [TestMethod]
        public void IIExampleTest05()
        {
            II oidII = new II();
            oidII.Root = "2.16.840.1.113883.1.18";
            oidII.Extension = null;         // Extension should have to be populated
            oidII.NullFlavor = null;        // when an OID is specified
            Assert.IsTrue(II.IsValidOidFlavor(oidII));
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Root        :   Given Object Identifier
        ///     Extension   :   Given extension
        /// Adn the following values are Nullified:
        ///     Nullflavor
        ///     
        /// Testing if oid flavor is valid given invalid root
        /// </summary>
        [TestMethod]
        public void IIExampleTest06()
        {
            II rootExtension = new II();
            rootExtension.Root = "2.16.840.1.11x3883.1.18";
            rootExtension.Extension = "12345";
            rootExtension.NullFlavor = null;
            Assert.IsFalse(II.IsValidOidFlavor(rootExtension));
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Root        :   Given Globally Unique Identifier
        ///     Nullflavor  :   NoInformation
        ///     
        /// Testing if token flavor is valid given ONLY root
        /// </summary>
        [TestMethod]
        public void IIExampleTest07()
        {
            II token = new II(Guid.NewGuid());
            token.NullFlavor = null;
            Assert.IsTrue(II.IsValidTokenFlavor(token));
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Root        :   Given Globally Unique Identifier
        /// And the following are Nullified:
        ///     Nullflavor  :   null
        ///     Extension   :   Given extension
        ///     Displayable :
        ///     Scope       :
        ///     
        /// Testing if token flavor is valid given extension
        /// </summary>
        [TestMethod]
        public void IIExampleTest08()
        {
            II token = new II(Guid.NewGuid());
            token.Extension = "1234";
            token.Displayable = null;
            token.Scope = null;
            Assert.IsFalse(II.IsValidTokenFlavor(token));
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Root        :   Given Globally Unique Identifier
        ///     Extension   :   Given extension
        /// And the following are Nullified:
        ///     Nullflavor  :   null
        ///     Displayable :   Indicates that identifier is intended for
        ///                     display and data entry purposes only.
        /// </summary>
        [TestMethod]
        public void IIExampleTest9()
        {
            II bus = new II();
            bus.Root = "1.2.3.4";
            bus.Extension = "1234";
            bus.Scope = 0;
            bus.Displayable = null;
            bus.NullFlavor = null;
            Assert.IsTrue(II.IsValidBusFlavor(bus));
            // testing if token flavor is valid given root, extension, and scope
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Root        :   Given Globally Unique Identifier
        ///     Extension   :   Given extension
        ///     Displayable :   Indicates that identifier is intended for
        ///                     display and data entry purposes only.
        ///     Scope       :   
        /// And the following are Nullified:
        ///     Nullflavor  :   null
        ///     
        /// </summary>
        [TestMethod]
        public void IIExampleTest10()
        {
            II bus = new II();
            bus.Root = "1.2.3.4";
            bus.Extension = "1234";
            bus.Scope = 0;
            bus.Displayable = true;
            bus.NullFlavor = null;
            Assert.IsFalse(II.IsValidBusFlavor(bus));
            // testing if token flavor is valid given displayable
        }
    }
}