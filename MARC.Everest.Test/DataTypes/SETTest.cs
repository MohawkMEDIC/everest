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
using MARC.Everest.Interfaces;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for LINQTest
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SET"), TestClass]
    public class SETTest
    {
        public SETTest()
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
        /// Attempts to create a SET of ST from a LIST of IGraphable
        /// </summary>
        [TestMethod]
        public void SETFromIGraphibleCollectionTest()
        {
            LIST<IGraphable> stuff = new LIST<IGraphable>(
                new ST[] { "A", "B", "C", "D" }
            );
            SET<ST> set = new SET<ST>(stuff);
            Assert.AreEqual(stuff.Count, set.Count);
        }

        /// <summary>
        /// Attempts to create a SET of ST from a LIST of ST
        /// </summary>
        [TestMethod]
        public void SETFromLISTTest()
        {
            LIST<ST> stuff = new LIST<ST>(
                new ST[] { "A", "B", "C", "D" }
            );
            SET<ST> set = (SET<ST>)stuff;
            Assert.AreEqual(stuff.Count, set.Count);
        }

        /// <summary>
        /// Confirms that a SET is not equal to a String
        /// </summary>
        [TestMethod]
        public void SETTypeMismatchEqualityTest()
        {
            SET<ST> a = new SET<ST>(new ST[] { "A", "B", "C" }),
                b = new SET<ST>(new ST[] { "D", "E" });
            Assert.IsFalse(a.Equals("A B C"));
        }

        /// <summary>
        /// Confirms that two SET with equal content are equal
        /// </summary>
        [TestMethod]
        public void SETContentSameEqualityTest()
        {
            SET<ST> a = new SET<ST>(new ST[] { "A", "B", "C" }),
                b = new SET<ST>(new ST[] { "A", "B", "C" });
            Assert.IsTrue(a.Equals(b));
        }

        /// <summary>
        /// Confirms that two SETS with different content aren't equal
        /// </summary>
        [TestMethod]
        public void SETContentDifferentEqualityTest()
        {
            SET<ST> a = new SET<ST>(new ST[] { "A", "B", "C" }),
                b = new SET<ST>(new ST[] { "A", "B", "D", "E" });
            Assert.IsFalse(a.Equals(b));
        }

        /// <summary>
        /// Tests a union of a set 
        /// </summary>
        [TestMethod]
        public void SETUnionSETTest()
        {
            SET<ST> a = new SET<ST>(new ST[] { "A", "B", "C" }),
                b = new SET<ST>(new ST[] { "D", "E" });
            Assert.AreEqual(5, a.Union(b).Count);
        }

        /// <summary>
        /// Test SET unioned with a single item
        /// </summary>
        [TestMethod]
        public void SETUnionItemTest()
        {
            SET<ST> a = new SET<ST>(new ST[] { "A", "B", "C" });
            Assert.AreEqual(4, a.Union((ST)"D").Count);
        }

        /// <summary>
        /// Tests the exception of one set from another 
        /// </summary>
        [TestMethod]
        public void SETExceptSETTest()
        {
            SET<ST> a = new SET<ST>(new ST[] { "A", "B", "C" }),
                b = new SET<ST>(new ST[] { "B", "C", "D", "E" });
            Assert.AreEqual(1, a.Except(b).Count);
        }

        /// <summary>
        /// Tests the intersection of one set with another 
        /// </summary>
        [TestMethod]
        public void SETIntersectionSETTest()
        {
            SET<ST> a = new SET<ST>(new ST[] { "A", "B", "C" }),
                b = new SET<ST>(new ST[] { "B", "C", "D", "E" });
            Assert.AreEqual(2, a.Intersection(b).Count);
        }

        /// <summary>
        /// Ensures that Count Validator succeeds (returns TRUE)
        /// When the number of elements given is right
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SET"), TestMethod]
        public void SETLinqTest1()
        {
            SET<ST> test = new SET<ST>(new ST[]{
               "Bob", "Matt", "Corey", "Jasper",
               "Trevor", "Justin", "Mike", "Brian",
               "Mark", "Duane", "Charley"},
                (a, b) => a.Value.CompareTo(b.Value));
            var names = from name in test
                        where name.Value.StartsWith("M")
                        select name;
            Assert.AreEqual(names.Count(), 3);
        }

        /// <summary>
        /// Ensures that Count Validator fails (returns FALSE)
        /// When the number of elements given is wrong
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SET"), TestMethod]
        public void SETLinqTest2()
        {
            SET<ST> test = new SET<ST>(new ST[]{
               "Bob", "Matt", "Corey", "Jasper",
               "Trevor", "Justin", "Mike", "Brian",
               "Mark", "Duane", "Charley"},
                (a, b) => a.Value.CompareTo(b.Value));
            var names = from name in test
                        where name.Value.StartsWith("M")
                        select name;
            Assert.AreNotEqual(names.Count(), 2);
        }

    }
}
