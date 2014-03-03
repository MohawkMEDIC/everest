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

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for RealTest
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "REAL"), TestClass]
    public class REALTest
    {
        public REALTest()
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
        /// Ensures that Validation succeeds (returns TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are being Nullified:
        ///     Value       
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "REAL"), TestMethod]
        public void REALNullFlavorTest()
        {
            REAL r = new REAL();
            r.NullFlavor = NullFlavor.NotAsked;
            r.Value = null;
            Assert.IsTrue(r.Validate());
        }

        /// <summary>
        /// Validate Value as real data type
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "REAL"), TestMethod]
        public void REALValueTest()
        {
            REAL r = new REAL();
            r.NullFlavor = null;
            r.Value = 123456789.0987; 
            Assert.IsTrue(r.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When there are no values being populated:
        /// And, the following variables are being Nullified:
        ///     Value       
        ///     NullFlavor
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "REAL"), TestMethod]
        public void REALNullTest()
        {
            REAL r = new REAL();
            r.NullFlavor = null;
            r.Value = null;
            Assert.IsFalse(r.Validate());
        }

        /// <summary>
        /// Ensures that Validation fails (returns FALSE)
        /// When the following values are being populated:
        ///     Value       
        ///     NullFlavor
        /// And, there are no variables being Nullified
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "REAL"), TestMethod]
        public void REALValueNullFlavorTest()
        {
            REAL r = new REAL();
            r.NullFlavor = NullFlavor.NotAsked;
            r.Value = 123456789.0987; 
            Assert.IsFalse(r.Validate());
        }

        /// <summary>
        /// Ensures REAL can be cast to a double.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "REAL"), TestMethod]
        public void REALCastToDoubleTest()
        {
            REAL r = new REAL(2.34f);
            double d = 2.34f;
            Assert.AreEqual(d, (double)r);      
        }

        /// <summary>
        /// Ensures double can cast to REAL.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "REAL"), TestMethod]
        public void REALCastFromDoubleTest()
        {
            double d = 2.34f;
            REAL r = d;
            Assert.AreEqual(d, (double)r);        
        }
    }
}