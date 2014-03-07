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
    /// Summary description for PQTest
    /// </summary>
    [TestClass]
    public class PQTest
    {
        public PQTest()
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

        [TestMethod]
        public void PQTranslationTest()
        {
            PQ h = new PQ((decimal)2.5, "m");
            h.Translation = new SET<PQR>(new PQR((decimal)82.021E+2, "ft_i", "123.23.24.3"), PQR.Comparator);
        }
        /// <summary>
        /// Tests to ensure PQ can be cast from a double.
        /// </summary>
        [TestMethod]
        public void PQCastFromDoubleTest()
        {
            decimal d = (decimal)23.32;
            PQ pq = new PQ();
            pq.Value = d; 
            pq.Unit = "ft_i";
            Assert.AreEqual(d, pq.Value);
            

        }
        /// <summary>
        /// Tests to ensure that PQ can be cast to a double.
        /// </summary>
        [TestMethod]
        public void PQCastToDoubleTest()
        {
            decimal d = (decimal)23.32;
            decimal value = (decimal)new PQ((decimal)23.32, "ft_i");
            Assert.AreEqual(d, value);
        }
        /// <summary>
        /// Ensures that validation succeeds when nullflavor is null;
        /// and value is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void PQValidationNULLNullflavorPOPValueTest()
        {
            PQ pq = new PQ();
            pq.NullFlavor = null;
            pq.Value = 1;
            Assert.IsTrue(pq.Validate());
        }
        /// <summary>
        /// Ensures that validation succeeds when value is null;
        /// and nullflavor is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PQNULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void PQNULLValuePOPNullflavorValidationTest()
        {
            PQ pq = new PQ();
            pq.Value = null;
            pq.NullFlavor = NullFlavor.NotAsked;
            Assert.IsTrue(pq.Validate());
        }
        /// <summary>
        /// Ensures that validation fails when nullflavor & value is null.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void PQValidationNULLNullflavorValueTest()
        {
            PQ pq = new PQ();
            pq.NullFlavor = null;
            pq.Value = null;
            Assert.IsFalse(pq.Validate());
        }
        /// <summary>
        /// Ensures that validation fails when nullflavor & value is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void PQValidationPOPNullflavorValueTest()
        {
            PQ pq = new PQ();
            pq.NullFlavor = NullFlavor.NotAsked;
            pq.Value = 1;
            Assert.IsFalse(pq.Validate());
        }
    }
}
