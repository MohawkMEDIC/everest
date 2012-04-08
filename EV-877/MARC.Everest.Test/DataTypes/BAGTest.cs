using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for BAGTest
    /// </summary>
    [TestClass]
    public class BAGTest
    {
        public BAGTest()
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
        /// Tests the BAG does equals a bag with the same content
        /// </summary>
        [TestMethod]
        public void BAGContentSameEqualityTest()
        {

            BAG<ST> stringBag = new BAG<ST>{
                new ST("String1"),
                new ST("String2"),
                new ST("String3")
            }, stringBag2 = new BAG<ST>() {
                new ST("String1"),
                new ST("String2"),
                new ST("String3")
            };

            Assert.AreEqual(stringBag, stringBag2);
            
        }

        /// <summary>
        /// Tests the BAG does not equal a bag with different content
        /// </summary>
        [TestMethod]
        public void BAGContentDifferentEqualityTest()
        {

            BAG<ST> stringBag = new BAG<ST>{
                new ST("String1"),
                new ST("String2"),
                new ST("String3")
            }, stringBag2 = new BAG<ST>() {
                "String1",
                "String2"
            };

            Assert.IsFalse(stringBag.Equals(stringBag2));
            Assert.IsFalse(stringBag2.Equals(stringBag));
        }

        /// <summary>
        /// Tests that two identically instanced BAG classes are equal
        /// </summary>
        [TestMethod]
        public void BAGInstanceEqualityTest()
        {

            BAG<ST> stringBag = new BAG<ST>{
                new ST("String1"),
                new ST("String2"),
                new ST("String3")
            }, stringBag2 = stringBag;

            Assert.IsTrue(stringBag.Equals(stringBag2));
        }

        /// <summary>
        /// Tests the BAG does not equal a string
        /// </summary>
        [TestMethod]
        public void BAGObjectMismatchEqualityTest()
        {

            BAG<ST> stringBag = new BAG<ST>{
                new ST("String1"),
                new ST("String2"),
                new ST("String3")
            };

            Assert.IsFalse(stringBag.Equals("String1String2String3"));

        }

        /// <summary>
        /// Tests that a LIST of T will be properly represented as a BAG of T
        /// </summary>
        [TestMethod]
        public void BAGListConversionTest()
        {
            LIST<INT> list = new LIST<INT>(new INT[] { 1, 2, 3, 4 });
            BAG<INT> bag = (BAG<INT>)list;
            Assert.AreEqual(list.Count, bag.Count);
        }
        /// <summary>
        /// Tests the BAG collection's storage. 
        /// Adding objects should increment the Count property. 
        /// 
        /// Expected Result: BAG.Count = 3
        /// </summary>
        [TestMethod]
        public void BAGStorageTest()
        {

            BAG<ST> stringBag = new BAG<ST>{
                new ST("String1"),
                new ST("String2"),
                new ST("String3")
            };

            Assert.AreEqual(3, stringBag.Count);
          
        }
        
        /// <summary>
        /// Tests the BAG collection's find method. 
        /// Find takes an expression to search for and returns the result. 
        /// 
        /// Expected result: findBag.Count = 3 and Find = not a null.
        /// </summary>
        [TestMethod]
        public void BAGFindTest()
        {

            BAG<ST> findBag = new BAG<ST>{
                new ST("String1"),
                new ST("String2"),
                new ST("String3")
            };
            //throw new Exception("Find method does not exist for BAG.");
             
            Assert.AreEqual(3, findBag.Count);
            Assert.AreNotEqual(null, findBag.Find(o=>o.Value.Equals("String1")));

        }

        /// <summary>
        /// Tests the .Clear() method of the BAG collection.
        /// Clear should empty the bag of all objects stored inside.
        /// 
        /// Expected result: bag.Count = 0
        /// </summary>
        [TestMethod]
        public void BAGClearTest()
        {

            BAG<ST> clearBag = new BAG<ST>{
                new ST("String1"),
                new ST("String2"),
                new ST("String3")
            };
            clearBag.Clear();
            Assert.AreEqual(0, clearBag.Count); 

        }

        /// <summary>
        /// Tests the foreach loop with the BAG collection. 
        /// Ensures the Foreach loop behaves appropriately with the BAG collection.
        /// 
        /// Expected result: Each object should be identical in the collection and 
        /// the iteration variable.
        /// </summary>
        [TestMethod]
        public void BAGIterationTest()
        {
            int i = 0;
            ST[] stArray = new ST[]
            {
                new ST("String1"),
                new ST("String2"),
                new ST("String3"),
                new ST("String4"),
                new ST("String5"),
                new ST("String6"),
                new ST("String7"),
                new ST("String8"),
                new ST("String9"),
                new ST("String10")};



            BAG<ST> forBag = new BAG<ST>(stArray);


            foreach (ST item in forBag)
            {
                Assert.AreEqual(stArray[i], item);
                i++;
            }

        }
    }
}
