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
    /// Summary description for QSETExamplesTest
    /// </summary>
    [TestClass]
    public class QSETExamplesTest
    {
        public QSETExamplesTest()
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

        /* Example 40 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        /// Adn the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void QSETExamplesTest01()
        {
            // create a new intersection set expression
            // (difference of 2 intervals is intersected with a third interval)
            QSI<INT> setExpression = QSI<INT>.CreateQSI
            (
                // creates the set expression by differentiating two intervals
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

                // third interval
                new IVL<INT>(2, 7)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );

            setExpression.NullFlavor = null;
            Assert.IsTrue(setExpression.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void QSETExamplesTest02()
        {
            QSI<INT> setExpression = QSI<INT>.CreateQSI
            (
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
                new IVL<INT>(2, 7)
                {
                    LowClosed = true,
                    HighClosed = true
                }
            );
            setExpression.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(setExpression.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values ARE Nullified:
        ///     Value
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void QSETExamplesTest03()
        {
            QSI<INT> setExpression = new QSI<INT>();
            setExpression.NullFlavor = null;
            Assert.IsFalse(setExpression.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void QSETExamplesTest04()
        {
            QSI<INT> setExpression = new QSI<INT>();
            setExpression.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(setExpression.Validate());
        }
    }
}
