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
    /// Summary description for LISTTest
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST"), TestClass]
    public class LISTTest
    {
        public LISTTest()
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
        /// Ensures that casting from generic list to LIST succeeds.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST"), TestMethod]
        public void LISTCastFromGenericListTest()
        {
            LIST<String> myList = new List<String>(new String[] {"Bob", "Jim", "Joe" });
            Assert.AreEqual(true, myList[0] == "Bob" && myList[1] == "Jim" && myList[2] == "Joe");
        }

        /// <summary>
        /// Ensures that Validate succeeds (returns TRUE) when List is empty and
        /// NullFlavor has been populated
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST"), TestMethod]
        public void LISTNullFlavorTest()
        {
            LIST<String> myList = new List<String>();
            myList.NullFlavor = NullFlavor.Other;
            Assert.AreEqual(true, myList.Validate());
        }

        /// <summary>
        /// Ensures that Validate fails (returns FALSE) when List is empty and
        /// NullFlavor has been nullified
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST"), TestMethod]
        public void LISTNullNullFlavorTest()
        {
            LIST<String> myList = new List<String>();
            myList.NullFlavor = null;
            Assert.AreEqual(false, myList.Validate());
        }

        /// <summary>
        /// Ensures that Validate succeeds (returns TRUE) when List is not empty and
        /// NullFlavor has been nullified
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST"), TestMethod]
        public void LISTPopulatedNullNullFlavorTest()
        {
            LIST<String> myList = new List<String>(new string[]{"test","test1","test2"});
            myList.NullFlavor = null;
            Assert.AreEqual(true, myList.Validate());
        }

        /// <summary>
        /// Ensures a new LIST&lt;T&gt; is created from a List&lt;T&gt; constructor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST"), TestMethod]
        public void LISTCreateGenericLISTFromGenericListCTORTest()
        {
            LIST<ST> testList = new List<ST>{
                                new ST("test"),
                                new ST("test1"),
                                new ST("test2"),
                                new ST("test3"),
                                new ST("test4")
            };
            Assert.AreEqual(5, testList.Count);
        }

        /// <summary>
        /// Checks if a new LIST&lt;ST&gt; can be created from a List&lt;ED&gt;(type is for example only).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST"), TestMethod]
        public void LISTCastFromList()
        {
            List<ED> EDList = new List<ED>{
                                new ED("test"),
                                new ED("test1"),
                                new ED("test2"),
                                new ED("test3"),
                                new ED("test4")
            };
            LIST<ST> STLIST = new LIST<ST>(EDList);
            //Bug: No cast exists in ST for ED
            Assert.AreEqual(5, STLIST.Count);
        }

        /// <summary>
        /// Ensures that SubSequence() works correctly.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LIST"), TestMethod]
        public void LISTSubSequenceTest()
        {
            LIST<ST> testList = new List<ST>{
                                new ST("test"),
                                new ST("test1"),
                                new ST("test2"),
                                new ST("test3"),
                                new ST("test4")
            };

            //Should return 2 
            // JF: Should return 2
            Assert.AreEqual(2, testList.SubSequence(2, 3).Count);
            //Should return the third element, "test2", in the list.
            Assert.AreEqual(testList.ElementAt(2), testList.SubSequence(2, 3)[0]);
        }            
    }
}