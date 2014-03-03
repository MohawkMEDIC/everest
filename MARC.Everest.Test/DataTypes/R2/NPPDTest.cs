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

namespace MARC.Everest.Test.DataTypes.R2
{
    /// <summary>
    /// Summary description for NPPDTest
    /// </summary>
    [TestClass]
    public class NPPDTest
    {
        public NPPDTest()
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
        public void R2NPPDParseTest()
        {

            // NPPD data
            NPPD<PQ> inti = new NPPD<PQ>
            (
                new UVP<PQ>[] {
                    new UVP<PQ>(new PQ(1, "a"), (decimal)0.1),
                    new UVP<PQ>(new PQ(2, "a"), (decimal)0.2),
                    new UVP<PQ>(new PQ(3, "a"), (decimal)0.45), 
                    new UVP<PQ>(new PQ(4, "a"), (decimal)0.1),
                    new UVP<PQ>(new PQ(5, "a"), (decimal)0.15)
                }
            );

            string actualXml = R2SerializationHelper.SerializeAsString(inti);
            var int2 = R2SerializationHelper.ParseString<NPPD<PQ>>(actualXml);
            Assert.AreEqual(inti, int2);
        }
    }
}
