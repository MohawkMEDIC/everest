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
    /// Summary description for STExamplesTest
    /// </summary>
    [TestClass]
    public class STExamplesTest
    {
        public STExamplesTest()
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

        /* Example 19 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        ///     Length      :   The count of characters in the string (read-only)
        /// And the following are null:
        ///     Language    :   Language that the string value was written in
        ///     Translation :   Presents alternate representations of the string
        ///                     data in other languages.
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void STExampleTest01()
        {
            ST st = "Hello!";
            st.Language = null;
            st.Translation = null;
            st.NullFlavor = null;
            // test if valid string given ONLY value
            Assert.IsTrue(st.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value
        ///     Length      :   The count of characters in the string (read-only)
        ///     NullFlavor
        /// And the following are null:
        ///     Language    :   Language that the string value was written in
        ///     Translation :   Presents alternate representations of the string
        ///                     data in other languages.
        /// </summary>
        [TestMethod]
        public void STExampleTest02()
        {
            ST st = "Hello!";
            st.Language = null;
            st.Translation = null;
            st.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(st.Validate());
            // test if valid string given value and a nullflavor
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        ///     Length      :   The count of characters in the string (read-only)
        /// And the following are null:
        ///     Language    :   Language that the string value was written in
        ///     Translation :   Presents alternate representations of the string
        ///                     data in other languages.
        ///     NullFlavor
        ///     
        /// Testing if valid simple flavor given ONLY value
        /// </summary>
        [TestMethod]
        public void STExampleTest03()
        {
            ST st = "Hello!";
            st.Language = null;
            st.Translation = null;
            st.NullFlavor = null;
            Assert.IsTrue(ST.IsValidSimpleFlavor(st));
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value
        ///     Length      :   The count of characters in the string (read-only)
        ///     Language    :   Language that the string value was written in
        /// And the following are null:
        ///     Translation :   Presents alternate representations of the string
        ///                     data in other languages.
        ///     NullFlavor
        ///     
        /// Testing if valid string given value and langauge.
        /// </summary>
        [TestMethod]
        public void STExampleTest04()
        {
            ST st = "Hello!";
            st.Language = "en-ca";
            st.Translation = null;
            st.NullFlavor = null;
            Assert.IsFalse(ST.IsValidSimpleFlavor(st));
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        ///     Length      :   The count of characters in the string (read-only)
        ///     
        /// And the following are null:
        ///     Translation :   Presents alternate representations of the string
        ///                     data in other languages.
        ///     Language    :   Language that the string value was written in.
        ///     NullFlavor
        ///     
        /// Testing if valid NTFlavor given only value (no translation).
        /// </summary>
        [TestMethod]
        public void STExampleTest05()
        {
            ST st = "Hello!";
            st.Language = null;
            st.Translation = null;
            st.NullFlavor = null;
            Assert.IsTrue(ST.IsValidNtFlavor(st));
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        ///     Length      :   The count of characters in the string (read-only).
        /// And the following are null:
        ///     Value
        ///     Language    :   Language that the string value was written in.
        ///     Translation :   Presents alternate representations of the string
        ///                     data in other languages.
        ///                     
        /// Testing if string is valid string given no Value and a NullFlavor.
        /// </summary>
        [TestMethod]
        public void STExampleTest07()
        {
            ST st = new ST();
            st.Language = null;
            st.Translation = null;
            st.NullFlavor = NullFlavor.NoInformation;
            Assert.IsTrue(st.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        ///     Length      :   The count of characters in the string (read-only).
        /// And the following are null:
        ///     Value
        ///     Language    :   Language that the string value was written in.
        ///     Translation :   Presents alternate representations of the string
        ///                     data in other languages.
        ///                     
        /// Testing if string is valid given a value and NullFlavor.
        /// </summary>
        [TestMethod]
        public void STExampleTest08()
        {
            ST st = "Hello";
            st.Language = null;
            st.Translation = null;
            st.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(st.Validate());
        }
    }
}
