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
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for CSTest
    /// </summary>
    [TestClass]
    public class CSTest
    {
        public CSTest()
        {

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
        /// Confirms that a CD is not equal to a String
        /// </summary>
        [TestMethod]
        public void CSTypeMismatchEqualityTest()
        {
            CS<NullFlavor> a = new CS<NullFlavor>(
                NullFlavor.NoInformation
                );
            Assert.IsFalse(a.Equals("12345"));
        }

        /// <summary>
        /// Confirms that two CDs with equal content are equal
        /// </summary>
        [TestMethod]
        public void CSContentSameEqualityTest()
        {
            CS<String> a = new CS<string>(
                "12345"
                ),
            b = new CS<String>(
                "12345"
                );
            Assert.IsTrue(a.Equals(b));
        }

        /// <summary>
        /// Confirms that two CDs with different content aren't equal
        /// </summary>
        [TestMethod]
        public void CSContentDifferentEqualityTest()
        {
            CS<String> a = new CS<string>(
                "12345"
                ),
            b = new CS<String>(
                "12346"
                );
            Assert.IsFalse(a.Equals(b));
        }


        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following values are nullified. 
        ///     Code        :   The plain code symbol defined by the code system
        /// </summary>        
        [TestMethod]
        public void CSNullFlavorTest()
        {
            CS<String> cs = new CS<String>();
            cs.Code = null;
            cs.NullFlavor = NullFlavor.NotApplicable;
            Assert.IsTrue(cs.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code        :   The plain code symbol defined by the code system
        /// And, the following values are nullified. 
        ///     NullFlavor
        /// </summary>        
        [TestMethod]
        public void CSCodeTest()
        {
            CS<String> cs = new CS<String>();
            cs.Code = "284196006";
            cs.NullFlavor = null;
            Assert.IsTrue(cs.Validate());
        }

        /// <summary>
        /// Ensures that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Code        :   The plain code symbol defined by the code system
        ///     NullFlavor
        /// And, there are no nullifiedvalues. 
        /// </summary>        
        [TestMethod]
        public void CSCodeNullFlavorTest()
        {
            CS<String> cs = new CS<String>();
            cs.Code = "284196006";
            cs.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(cs.Validate());
        }

        /// <summary>
        /// Ensures that validation fails (return FALSE)
        /// When there are no values being populated:
        /// And, the following values are nullified. 
        ///     Code        :   The plain code symbol defined by the code system
        ///     NullFlavor
        /// </summary>        
        [TestMethod]
        public void CSNullTest()
        {
            CS<String> cs = new CS<String>();
            cs.Code = null;
            cs.NullFlavor = null;
            Assert.IsFalse(cs.Validate());
        }

        /// <summary>
        /// Code System Auto Set on CS<String>(T)
        /// </summary>
        [TestMethod]
        public void CSCodeSystemAutoSetOnGenericTest()
        {
               // CS<NullFlavor>.Code = Other
                CV<NullFlavor> csNullFlavorInstance = new CV<NullFlavor>();
                csNullFlavorInstance.Code = NullFlavor.Other;

                Console.WriteLine(csNullFlavorInstance.CodeSystem);
                // CS<NullFlavor>.CodeSystem = " 2.16.840.1.113883.5.1008”
                // toDo: as of October 4/2010 this does not work, has a bug (generates exception)
                Assert.AreEqual("2.16.840.1.113883.5.1008", csNullFlavorInstance.CodeSystem); // CodeSystem is actually null
        }


        /// <summary>
        /// Cast CS<String> to CS<String>(T)
        /// </summary>
        [TestMethod]
        public void CSCastCStoGenericCSTest()
        {
                //CS.Code = “OTH”
                CS<String> csInstance = new CS<String>();
                csInstance.Code = "OTH";
                
                // create a CS<NullFlavor> object
                CS<NullFlavor> csNullFlavorInstance = new CS<NullFlavor>();

                //Cast CS<String> to CS<T>
                csNullFlavorInstance = Util.Convert<CS<NullFlavor>>(csInstance);
                
                // is true if cast was successful
                Assert.IsTrue(csNullFlavorInstance.Code == NullFlavor.Other);
        }


        /// <summary>
        /// Cast String to CS<String>(T)
        /// </summary>
        [TestMethod]
        public void CS32CastStringToGenericTest()
        {
            // “Other”
            String stringText = "OTH";

            // create CS<NullFlavor> instance
            CS<NullFlavor> csInstance = new CS<NullFlavor>();

            // cast String to CS<T>
            csInstance = Util.Convert<CS<NullFlavor>>(stringText);
            
            // True if the cast was successful
            Assert.IsTrue(csInstance.Code == NullFlavor.Other);
        }

        /// <summary>
        /// Cast CS<String>(T) to T
        /// </summary>
        [TestMethod]
        public void CSCastGenericToTypeTest()
        {
                //CS<NullFlavor>.Code = Other
                CS<NullFlavor> csInstance = new CS<NullFlavor>();
                csInstance.Code = NullFlavor.Other;

                // Cast CS<T> to T
                NullFlavor nvInstance = (NullFlavor)csInstance;
                
                // True if cast was successful
                Assert.IsTrue(nvInstance == NullFlavor.Other);
        }

        /// <summary>
        /// TODO: Set Alternate Code
        /// </summary>
        [TestMethod]
        public void CSSetAlternateCodeTest()
        {
        }

        /// <summary>
        /// TODO: Set Alternate Code No Code System Set
        /// </summary>
        [TestMethod]
        public void CSSetAlternateCodeNoCodeSystemSetTest()
        {
        }
    }
}
