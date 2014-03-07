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

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for CVTest
    /// </summary>
    [TestClass]
    public class CVExamplesTest
    {
        public CVExamplesTest()
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

        /* Example 10 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        /// CodeSystem should be automatically assigned given
        /// the Code comes from an enumerated vocabulary.
        /// </summary>
        [TestMethod]
        public void CVExample10Test01()
        {
            CV<NullFlavor> nullflavor = new CV<NullFlavor>();
            nullflavor.Code = NullFlavor.NoInformation;
            Console.WriteLine(nullflavor.CodeSystem);
            //output
            Assert.AreEqual(nullflavor.CodeSystem, "2.16.840.1.113883.5.1008");
            Assert.IsTrue(nullflavor.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        ///     CodeSystem
        ///     
        /// Testing if CV is valid given incorrect OID (3.16.840.1.113883.5.1008)
        /// </summary>
        [TestMethod]
        public void CVExample10Test02()
        {
            CV<NullFlavor> nullflavor = new CV<NullFlavor>();
            nullflavor.Code = NullFlavor.NoInformation;
            Console.WriteLine(nullflavor.CodeSystem);
            // output
            Assert.AreNotEqual(nullflavor.CodeSystem, "3.16.840.1.113883.5.1008");
            Assert.IsTrue(nullflavor.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code
        ///     CodeSystem
        /// </summary>
        [TestMethod]
        public void CVExample10Test03()
        {
            CV<NullFlavor> nullflavor = new CV<NullFlavor>();
            nullflavor = new CV<NullFlavor>();
            nullflavor.CodeSystem = "Hello";
            nullflavor.Code = NullFlavor.NoInformation;
            Console.WriteLine(nullflavor.CodeSystem);
            //output: Hello
            Assert.IsTrue(nullflavor.Validate());
        }


        /* Example 12 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     OriginalText    : The initial Original Text
        ///     CodeSystem      : The code system the code was picked from
        ///     NullFlavor
        /// Testing CV with Original Text while there is a NullFlavor
        /// </summary>
        [TestMethod]
        public void CVExample12Test01()
        {
            CV<String> member = new CV<String>()
            {
                NullFlavor = NullFlavor.Other
            };
            member.OriginalText = "Step-Brother In Law";
            member.CodeSystem = "2.16.840.1.113883.5.111";
            Assert.IsTrue(member.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     OriginalText    : The initial Original Text
        ///     CodeSystem      : The code system the code was picked from
        ///     
        /// And the following values are Nullfied:
        ///     NullFlavor
        ///     
        /// Testing CV with Original Text while there is no NullFlavor
        /// </summary>
        [TestMethod]
        public void CVExample12Test02()
        {
            CV<String> member = new CV<String>()
            {
                NullFlavor = null
            };
            member.OriginalText = "Step-Brother In Law";
            member.CodeSystem = "2.16.840.1.113883.5.111";
            Assert.IsFalse(member.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     OriginalText    : The initial Original Text
        ///     CodeSystem      : The code system the code was picked from
        ///     
        /// And the following values are Nullfied:
        ///     NullFlavor
        ///     
        /// Testing CV with Original Text while there is no NullFlavor
        /// </summary>
        [TestMethod]
        public void CVExample12Test03()
        {
            
        }
    }
}
