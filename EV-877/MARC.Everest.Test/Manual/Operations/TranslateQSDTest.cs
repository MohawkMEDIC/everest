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
    /// Summary description for Trans_QSD
    /// </summary>
    [TestClass]
    public class TranslateQSDTest
    {
        public TranslateQSDTest()
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

        /* Example 42 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void QSDTransTest01()
        {
            // create differentiating set expression of type SXPR
            SXPR<INT> setExpression = new SXPR<INT>(){
                new IVL<INT>(1,10) {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(5,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Exclusive
                },
                new IVL<INT>(7,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Exclusive
                }
            };

            // translate set expression to type QSD
            var setExpression2 = setExpression.TranslateToQSET();

            Assert.IsTrue(setExpression2.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void QSDTransTest02()
        {
            SXPR<INT> setExpression = new SXPR<INT>(){
                new IVL<INT>(1,10) {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(5,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Exclusive
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
        /// Testing Function Validate must return FALSE.
        /// When the following values are Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void QSDTransTest03()
        {
            SXPR<INT> setExpression = new SXPR<INT>();
            setExpression.TranslateToQSET();
            Assert.IsFalse(setExpression.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        ///     
        /// </summary>
        [TestMethod]
        public void QSDTransTest04()
        {
            SXPR<INT> setExpression = new SXPR<INT>();
            var setExpression2 = setExpression.TranslateToQSET();
            setExpression2.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(setExpression2.Validate());
        }


        /// <summary>
        /// Testing Function Validate on 'setExpression' must return TRUE.
        /// Testing Function Validate on 'difference' must return TRUE.
        /// Testing Function AreEqual must return TRUE.
        /// 
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        ///     
        /// </summary>
        [TestMethod]
        public void QSDTransTest05()
        {
            SXPR<INT> setExpression = new SXPR<INT>(){
                new IVL<INT>(1,10) {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(5,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Exclusive
                },
                new IVL<INT>(7,8) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Exclusive
                }
            };

            // Translate SXPR to QSET
            var setExpression2 = setExpression.TranslateToQSET();

            // NATURAL QSD
            QSD<INT> difference = new QSD<INT>(
                new QSD<INT>(
                    new IVL<INT>(1, 10)
                    {
                        LowClosed = true,
                        HighClosed = true
                    },
                    new IVL<INT>(5, 8)
                    {
                        LowClosed = true,
                        HighClosed = true
                    }
                ),
                new IVL<INT>(7, 8)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            // setExpression.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(setExpression2.Validate());
            Assert.IsTrue(difference.Validate());
            Assert.AreNotEqual(setExpression2, difference);
        }
    }
}
