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

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for SXPRExamplesTest01
    /// </summary>
    [TestClass]
    public class SXPRExamplesTest01
    {
        public SXPRExamplesTest01()
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

        /* Example 39 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Terms       :   used to store the sequence of expression components
        ///                     that make up the continuous set expression
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void SXPRExampleTest01()
        {
            // create new set expression such that:
            //      - the first and second intervals are exclusive of each other and
            //      - the result is intersected with the third interval
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
                new IVL<INT>(2,7) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Intersect
                }
            };

            Console.WriteLine(setExpression.ToString());
            Assert.IsTrue(setExpression.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are Nullified:
        ///     Terms       :   used to store the sequence of expression components
        ///                     that make up the continuous set expression
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void SXPRExampleTest02()
        {
            SXPR<INT> setExpression = new SXPR<INT>();
            setExpression.NullFlavor = null;
            Console.WriteLine(setExpression.ToString());
            Assert.IsFalse(setExpression.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Terms       :   used to store the sequence of expression components
        ///                     that make up the continuous set expression
        ///     NullFlavor     
        /// </summary>
        [TestMethod]
        public void SXPRExampleTest03()
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
                new IVL<INT>(2,7) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Intersect
                }
            };
            setExpression.NullFlavor = NullFlavor.Other;
            Console.WriteLine(setExpression.ToString());
            Assert.IsFalse(setExpression.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And the following values are Nullified:
        ///     Terms       :   used to store the sequence of expression components
        ///                     that make up the continuous set expression
        /// </summary>
        [TestMethod]
        public void SXPRExampleTest04()
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
                new IVL<INT>(2,7) {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Intersect
                }
            };

            Console.WriteLine(setExpression.ToString());
            Assert.IsTrue(setExpression.Validate());
        }
    }
}

