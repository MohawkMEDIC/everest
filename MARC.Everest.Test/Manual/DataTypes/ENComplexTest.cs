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
    /// Summary description for ENComplexTest
    /// </summary>
    [TestClass]
    public class ENComplexTest
    {
        public ENComplexTest()
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

        /* Example 37 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Parts        :   Sequence of entity names that that comprise each name.
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void EXComplexTest01()
        {
            EN name = new EN(EntityNameUse.License,
                new ENXP[] {

                    // add a Title name-part to the name
                    new ENXP("Dr.", EntityNamePartType.Title)
                    {
                        // assign qualifiers to the 'Title' name-part
                        Qualifier = new SET<CS<EntityNamePartQualifier>>()
                        {
                            EntityNamePartQualifier.Academic,
                            EntityNamePartQualifier.Prefix
                        }
                    },
                    
                    // add the first name as 'Given' name-part
                    new ENXP("John", EntityNamePartType.Given),

                    // add an initial as 'Given' name-part
                    new ENXP("G", EntityNamePartType.Given)
                    {
                        // assign the Initial qualifier to the name-part
                        Qualifier = new SET<CS<EntityNamePartQualifier>>()
                        {
                            EntityNamePartQualifier.Initial
                        }
                    },

                    // add a middle name as 'Given' name-part
                    new ENXP("Jacob", EntityNamePartType.Given)
                    {
                        // assign the 'Middle' qualifier to the middle name
                        Qualifier = new SET<CS<EntityNamePartQualifier>>()
                        {
                            EntityNamePartQualifier.Middle
                        }
                    },

                    // add part of the last name as 'Family' name-part
                    new ENXP("Jingleheimer", EntityNamePartType.Family),

                    // hyphens in a name must be a 'Delimiter' name-part
                    new ENXP("-", EntityNamePartType.Delimiter),

                    // add part of the last name as 'Family' name-part
                    new ENXP("Schmidt", EntityNamePartType.Family),

                    // add inherited suffix name-part with qualifiers 'Birth' and 'Suffix'
                    new ENXP("II")
                    {
                        Qualifier = new SET<CS<EntityNamePartQualifier>>()
                        {
                            EntityNamePartQualifier.Birth,
                            EntityNamePartQualifier.Suffix
                        }
                    } //end final ENXP
                } // end ENXP[] array
            ); // close parameters for EN

            Assert.IsTrue(name.Validate());

        } // end EXComplexTest01


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Parts        :   Sequence of entity names that that comprise each name.
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void EXComplexTest02()
        {
            EN name = new EN(EntityNameUse.License, new ENXP[]{});
            name.NullFlavor = null;
            Assert.IsFalse(name.Validate());
        }
    }
}
