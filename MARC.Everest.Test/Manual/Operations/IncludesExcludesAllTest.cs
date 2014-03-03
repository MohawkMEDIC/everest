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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes.Manual.Operations
{
    /// <summary>
    /// Summary description for IncludesExcludesAll
    /// </summary>
    [TestClass]
    public class IncludesExcludesAll
    {
        public IncludesExcludesAll()
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

        /* Example 57 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   Any items in the LIST
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IncExAllTest01()
        {
            // create new empty lists of integers
            LIST<INT> aList = new LIST<INT>(10),
            bList = new LIST<INT>(10),
            cList = new LIST<INT>(10);

            // populate all lists
            for (INT i=1; i <= 5; i++)
            {
                aList.Add(i);       // Items: 1,2,3,4,5
                bList.Add(i + 1);   // Items: 2,3,4,5,6
                cList.Add(i * 10);  // Items: 10,20,30,40,50
            }

                // Returns true as aList contains all of the items of aList
                var containsVal = aList.IncludesAll(aList);
                Console.WriteLine("aList Includes All aList: " + containsVal.ToString());

                Assert.AreEqual(containsVal,true);
            
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   Any items in the LIST
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IncExAllTest02()
        {
            LIST<INT> aList = new LIST<INT>(),
            bList = new LIST<INT>(),
            cList = new LIST<INT>();

            for (INT i = 1; i <= 5; i++)
            {
                aList.Add(i);
                bList.Add(i + 1);
                cList.Add(i * 10);
            }
                        
                // Returns true as aList contains none of the items of cList
                var containsVal = aList.ExcludesAll(bList);
                Console.WriteLine("aList Excludes All bList: " + containsVal.ToString());
            
                Assert.AreEqual(containsVal, false);
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   Any items in the LIST
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IncExAllTest03()
        {
            LIST<INT> aList = new LIST<INT>(10),
            bList = new LIST<INT>(10),
            cList = new LIST<INT>(10);

            for (INT i = 1; i <= 5; i++)
            {
                aList.Add(i);
                bList.Add(i + 1);
                cList.Add(i * 10);
            }

            // Returns false as aList contains none of the items of bList
            var containsVal = aList.ExcludesAll(bList);
            Console.WriteLine("aList Excludes All bList: " + containsVal.ToString());

            Assert.IsTrue(containsVal.Validate());
            Assert.AreEqual(containsVal, false);
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value       :   Any items in the LIST
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IncExAllTest04()
        {
            LIST<INT> aList = new LIST<INT>(10),
            bList = new LIST<INT>(10),
            cList = new LIST<INT>(10);

            for (INT i = 1; i <= 5; i++)
            {
                aList.Add(i);
                bList.Add(i + 1);
                cList.Add(i * 10);
            }

            // Returns false as aList contains none of the items of bList
            var containsVal = aList.IncludesAll(bList);
            Console.WriteLine("aList Includes All bList: " + containsVal.ToString());

            Assert.IsTrue(containsVal.Validate());
            Assert.AreEqual(containsVal, false);
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void IncExAllTest05()
        {
            LIST<INT> aList = new LIST<INT>(10);
            aList.Add(1);
            aList.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(aList.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor    
        /// And the following values are Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        /// </summary>
        [TestMethod]
        public void IncExAllTest06()
        {
            LIST<INT> aList = new LIST<INT>();
            aList.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(aList.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     NullFlavor    
        /// And the following values are Nullified:
        ///     Value       :   The initial value of the CO instanceCodeType
        /// </summary>
        [TestMethod]
        public void IncExAllTest07()
        {
            LIST<INT> aList = new LIST<INT>();
            aList.NullFlavor = null;
            Assert.IsFalse(aList.Validate());
        }
    }
}
