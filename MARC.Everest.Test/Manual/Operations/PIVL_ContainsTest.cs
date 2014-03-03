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
    /// Summary description for PIVL_ContainsTest
    /// </summary>
    [TestClass]
    public class PIVL_ContainsTest
    {
        public PIVL_ContainsTest()
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
        /// Testing Function Validate must return TRUE
        /// </summary>
        [TestMethod]
        public void PIVL_ConstainsTest01()
        {
            // Find a time that is a Saturday like Saturday November 12, 2011
            // and repeat it every week
            PIVL<TS> saturdays = new PIVL<TS>
            (
                new TS(new DateTime(2011, 11, 12), DatePrecision.Day).ToIVL(),
                new PQ(1, "wk")
            );
            
            // Determine if November 19, 2011 is a member of the PIVL
            // and if December 20, 2011 is a member

            TS nov19 = new TS(new DateTime(2011, 11, 19), DatePrecision.Day),
                dec20 = new TS(new DateTime(2011, 12, 20), DatePrecision.Day);

            // outout the date and the result of the contains function
            Console.WriteLine("'{0}' a Saturday? {1}\r\n'{2} a Saturday? {3}",
                nov19.DateValue,
                saturdays.Contains(nov19),
                dec20.DateValue,
                saturdays.Contains(dec20)
                );

            Assert.IsTrue(saturdays.Validate());
        }
    }
}
