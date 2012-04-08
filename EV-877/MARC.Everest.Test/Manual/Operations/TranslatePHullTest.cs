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
    /// Summary description for Trans_Per_Hull
    /// </summary>
    [TestClass]
    public class TranslatePHullTest
    {
        public TranslatePHullTest()
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

        /* Example 43
         * Resultant Translation of Periodic Hull Test
         */

        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     Terms       :   used to store the sequence of expression
        ///                     components that make up the continuous 
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void TPHTest01()
        {
            // create a periodic hull set expression of type SXPR

            SXPR<INT> setExpression = new SXPR<INT>(){
                new IVL<INT>(1,10) {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(5,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.PeriodicHull
                },
                new IVL<INT>(7,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Exclusive
                }
            };
            
            // translate period hull set expression to type QSET
            var setExpression2 = setExpression.TranslateToQSET();

            setExpression2.NullFlavor = null;
            Assert.IsTrue(setExpression2.Validate());
        }
        

        /// <summary>
        /// Testing Function Validate must return FALSE    
        /// And the following values are Nullified:
        ///     Terms       :   used to store the sequence of expression
        ///                     components that make up the continuous 
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void TPHTest02()
        {
            SXPR<INT> setExpression = new SXPR<INT>();
            var setExpression2 = setExpression.TranslateToQSET();
            setExpression2.NullFlavor = null;
            Assert.IsFalse(setExpression2.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Terms       :   used to store the sequence of expression
        ///                     components that make up the continuous 
        /// </summary>
        [TestMethod]
        public void TPHTest03()
        {
            SXPR<INT> setExpression = new SXPR<INT>();
            var setExpression2 = setExpression.TranslateToQSET();
            setExpression2.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(setExpression2.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE
        /// When the following values are not Nullified:
        ///     Terms       :   used to store the sequence of expression
        ///                     components that make up the continuous
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void TPHTest04()
        {
            SXPR<INT> setExpression = new SXPR<INT>(){
                new IVL<INT>(1,10) {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(5,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.PeriodicHull
                },
                new IVL<INT>(7,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Exclusive
                }
            };

            var setExpression2 = setExpression.TranslateToQSET(); 
            setExpression2.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(setExpression2.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE
        /// When the following values are not Nullified:
        ///     Terms       :   used to store the sequence of expression
        ///                     components that make up the continuous
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void TPHTest05()
        {
            SXPR<INT> setExpression = new SXPR<INT>(){
                new IVL<INT>(),
                new IVL<INT>(),
                new IVL<INT>()
            };

            var setExpression2 = setExpression.TranslateToQSET();
            setExpression2.NullFlavor = null;
            Assert.IsTrue(setExpression2.Validate());
        }


        /// <summary>
        /// Testing equality of the SXPR vs QSET methods of Periodic Hull.
        /// Even though they do the same thing, they should not be equal.
        /// AreNotEqual test should return TRUE.
        /// </summary>
        [TestMethod]
        public void TPHTest06()
        {
            SXPR<INT> setExpression = new SXPR<INT>(){
                new IVL<INT>(1,10) {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(5,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.PeriodicHull
                },
                new IVL<INT>(7,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Exclusive
                }
            };
            
            // Translate SXPR to QSET
            var setExpression2 = setExpression.TranslateToQSET();

            // NTURAL QSET VERSION
            QSD<INT> PHRange = new QSD<INT>
                (
                     new QSP<INT>(
                            new IVL<INT>(1,10),
                            new IVL<INT>(5,8)
                         ),
                     new IVL<INT>(7,8)
                );

            Assert.AreNotEqual(setExpression2, PHRange);
        }
    }
}
