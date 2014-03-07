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
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace MARC.Everest.Test
{
    /// <summary>
    /// Tests for the XSI Type processing
    /// </summary>
    [TestClass]
    public class XSITypeProcessing
    {
        public XSITypeProcessing()
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
        /// Test the creation of an XSI Type string from a simple type
        /// </summary>
        [TestMethod]
        public void CreateXSITypeSimpleTest()
        {
            string expected = "II";
            string actual = Util.CreateXSITypeName(typeof(II));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the creation of an XSI Type string from a generic type
        /// </summary>
        [TestMethod]
        public void CreateXSITypeGenericTest()
        {
            string expected = "IVL_INT";
            string actual = Util.CreateXSITypeName(typeof(IVL<INT>));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the creation of an XSI Type string from a CS type
        /// </summary>
        [TestMethod]
        public void CreateXSITypeCSTest()
        {
            string expected = "CS";
            string actual = Util.CreateXSITypeName(typeof(CS<NullFlavor>));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the creation of an XSI Type string from a nested type
        /// </summary>
        [TestMethod]
        public void CreateXSITypeNestedTest()
        {
            string expected = "IVL_RTO_RTO_MO_PQ_PQ";
            string actual = Util.CreateXSITypeName(typeof(IVL<RTO<RTO<MO,PQ>,PQ>>));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the parse of an XSI Type string from a nested type type
        /// </summary>
        [TestMethod]
        public void CreateXSIDefaultTest()
        {
            string expected = "IVL_RTO";
            string actual = Util.CreateXSITypeName(typeof(IVL<RTO<IQuantity, IQuantity>>));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the parse of an XSI Type string from a simple type type
        /// </summary>
        [TestMethod]
        public void ParseXSISimpleTest()
        {
            Type expected = typeof(II);
            Type actual = Util.ParseXSITypeName("II");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the parse of an XSI Type string from a generic type type
        /// </summary>
        [TestMethod]
        public void ParseXSIGenericTest()
        {
            Type expected = typeof(IVL<INT>);
            Type actual = Util.ParseXSITypeName("IVL_INT");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the parse of an XSI Type string from a CS type type
        /// </summary>
        [TestMethod]
        public void ParseXSICSTest()
        {
            Type expected = typeof(CS<String>);
            Type actual = Util.ParseXSITypeName("CS");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the parse of an XSI Type string from a nested type type
        /// </summary>
        [TestMethod]
        public void ParseXSINestedTest()
        {
            Type expected = typeof(IVL<RTO<RTO<MO,PQ>,PQ>>);
            Type actual = Util.ParseXSITypeName("IVL_RTO_RTO_MO_PQ_PQ");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the parse of an XSI Type string from a nested type type
        /// </summary>
        [TestMethod]
        public void ParseXSIDefaultTest()
        {
            Type expected = typeof(IVL<RTO<IQuantity, IQuantity>>);
            Type actual = Util.ParseXSITypeName("IVL_RTO");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test the parse of an XSI Type string from a nested type type
        /// </summary>
        [TestMethod]
        public void ParseXSIFailTest()
        {
            try
            {
                Type expected = typeof(II);
                Type actual = Util.ParseXSITypeName("II_RTO");
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

    }
}
