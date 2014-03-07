/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: fyfej
 * Date: 3-6-2013
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test
{
    /// <summary>
    /// Summary description for TELTest
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestClass]
    public class TELTest
    {
        public TELTest()
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
        /// Emsures that TEL can be cast from a string.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELCastFromStringTest()
        {
            TEL email = "mailto:test@google.com";
            Assert.AreEqual("mailto:test@google.com", email.Value);
        }
        /// <summary>
        /// Ensures that TEL can be cast to a string.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELCastToStringTest()
        {
            TEL email = "mailto:test@google.com";
            String emailString = email;
            Assert.AreEqual("mailto:test@google.com", emailString);
        }
        /// <summary>
        /// Emsures that TEL can be cast from a Uri.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELCastFromUriTest()
        {
            Uri email2 = new Uri("mailto:test@google.com");
            TEL email = email2;
            Assert.AreEqual(email, email2);
        }
        /// <summary>
        /// Ensures that TEL can be cast to a Uri.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELCastToUrlTest()
        {
            TEL email = "mailto:test@google.com";
            Uri emailString = (Uri)email;
            Assert.AreEqual(email, emailString);
        }
        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When the following values are being populated:
        ///     Value       
        ///     NullFlavor
        /// And, there are no variables being Nullified:
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELValueNullFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = "something@whatever.com";
            tel.NullFlavor = NullFlavor.NotApplicable;
            Assert.IsFalse(tel.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     Value       
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELValidValueTest()
        {
            TEL tel = new TEL();
            tel.Value = "mailto:somethingelse@this.com";
            tel.NullFlavor = null;
            Assert.IsTrue(tel.Validate());
        }

        /// <summary>
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        ///     Value       
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELNullFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = null;
            tel.NullFlavor = NullFlavor.NotAsked;
            Assert.IsTrue(tel.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When there are no values being populated:
        /// And, the following variables are being Nullified:
        ///     Value       
        ///     NullFlavor
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELNullTest()
        {
            TEL tel = new TEL();
            tel.Value = null;
            tel.NullFlavor = null;
            Assert.IsFalse(tel.Validate());
        }

        /// <summary>
        /// Ensures that TEL URL Validator succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     Value       
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELValidURLFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = "http://www.nintendo.com";
            tel.NullFlavor = null;
            Assert.IsTrue(TEL.IsValidUrlFlavor(tel));
        }

        /// <summary>
        /// Ensures that TEL URL Validator fails (returns FALSE)
        /// When the following values are being populated:
        ///     Value       
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELInvalidURLFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = "www.xbox.com";
            tel.NullFlavor = null;
            Assert.IsFalse(TEL.IsValidUrlFlavor(tel));
        }

        /// <summary>
        /// Ensures that TEL URI Validator succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     Value       
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URI"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELValidURIFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = "file:blah/blah";
            tel.NullFlavor = null;
            Assert.IsTrue(TEL.IsValidUriFlavor(tel));
        }

        /// <summary>
        /// Ensures that TEL URI Validator fails (returns FALSE)
        /// When the following values are being populated:
        ///     Value       
        /// And, the following variables are being Nullified:
        ///     NullFlavor
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URI"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELInvalidURIFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = "nothing here";
            tel.NullFlavor = null;
            Assert.IsFalse(TEL.IsValidUriFlavor(tel));
        }

        // Tests both the EMail and Person flavors
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "EMail"), TestMethod]
        public void TELValidEMailFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = "mailto:yoshirocks@mario.ca";
            tel.NullFlavor = null;
            Assert.IsTrue(TEL.IsValidEMailFlavor(tel));
            Assert.IsTrue(TEL.IsValidPersonFlavor(tel));
        }

        /// <summary>
        /// Tests both the EMail and Person flavors.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "EMail"), TestMethod]
        public void TELInvalidEMailFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = "no emails thank you";
            tel.NullFlavor = null;
            Assert.IsFalse(TEL.IsValidEMailFlavor(tel));
            Assert.IsFalse(TEL.IsValidPersonFlavor(tel));
        }

        /// <summary>
        /// Tests both the Phone and Person flavors.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELValidPhoneFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = "tel://519-555-2345";
            tel.NullFlavor = null;
            Assert.IsTrue(TEL.IsValidPhoneFlavor(tel));
            Assert.IsTrue(TEL.IsValidPersonFlavor(tel));
        }

        /// <summary>
        /// Tests both the Phone and Person flavors
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TEL"), TestMethod]
        public void TELInvalidPhoneFlavorTest()
        {
            TEL tel = new TEL();
            tel.Value = "519-456-3423";
            tel.NullFlavor = null;
            Assert.IsFalse(TEL.IsValidPhoneFlavor(tel));
            Assert.IsFalse(TEL.IsValidPersonFlavor(tel));
        }
    }
}
