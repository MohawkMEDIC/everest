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
    /// Summary description for Find_FindAll
    /// </summary>
    [TestClass]
    public class FindAndFindAllTest
    {
        public FindAndFindAllTest()
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

        /* Example 58 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Items       :   Members of the list
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void FindFindAllTest01()
        {
            // create new list of names
            LIST<EN> names = LIST<EN>.CreateList(
                new EN(EntityNameUse.Legal, new ENXP[]{
                    new ENXP("John", EntityNamePartType.Given),
                    new ENXP("Smith",EntityNamePartType.Family)
                }),
                new EN(EntityNameUse.Legal, new ENXP[]{
                    new ENXP("Jane", EntityNamePartType.Given),
                    new ENXP("Smith", EntityNamePartType.Family)
                }),
                new EN(EntityNameUse.Legal, new ENXP[]{
                    new ENXP("Melany", EntityNamePartType.Given),
                    new ENXP("Smith", EntityNamePartType.Family)
                }),
                new EN(EntityNameUse.Legal, new ENXP[]{
                    new ENXP("Sue", EntityNamePartType.Given),
                    new ENXP("Ellen", EntityNamePartType.Family)
                })
            );

            // Finds all the legal names
            var dispNames = names.Find(en => en.Use.Contains(EntityNameUse.Legal));
            Console.WriteLine(dispNames + "\n");

            // Finds all names with a family name of Smith
            var dispAllNames = names.FindAll(en => en.Part.Exists(part => part.Value == "Smith"));

            foreach (var item in dispAllNames)
            {
                Console.WriteLine(item.ToString());
            }

            names.NullFlavor = null;
            Assert.IsTrue(names.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Items       :   Members of the list
        ///     NullFlavor     
        /// </summary>
        [TestMethod]
        public void FindFindAllTest02()
        {
            LIST<EN> names = LIST<EN>.CreateList(
                new EN(EntityNameUse.Legal, new ENXP[]{
                    new ENXP("John", EntityNamePartType.Given),
                    new ENXP("Smith",EntityNamePartType.Family)
                }),
                new EN(EntityNameUse.Legal, new ENXP[]{
                    new ENXP("Jane", EntityNamePartType.Given),
                    new ENXP("Smith", EntityNamePartType.Family)
                }),
                new EN(EntityNameUse.Legal, new ENXP[]{
                    new ENXP("Melany", EntityNamePartType.Given),
                    new ENXP("Smith", EntityNamePartType.Family)
                }),
                new EN(EntityNameUse.Legal, new ENXP[]{
                    new ENXP("Sue", EntityNamePartType.Given),
                    new ENXP("Ellen", EntityNamePartType.Family)
                })
            );

            // Finds all the legal names
            var dispNames = names.Find(en => en.Use.Contains(EntityNameUse.Legal));
            Console.WriteLine(dispNames + "\n");

            // Finds all names with a family name of Smith
            var dispAllNames = names.FindAll(en => en.Part.Exists(part => part.Value == "Smith"));

            foreach (var item in dispAllNames)
            {
                Console.WriteLine(item.ToString());
            }

            names.NullFlavor = NullFlavor.Other;
            Assert.IsFalse(names.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are Nullified:
        ///     Items       :   Members of the list
        ///     NullFlavor     
        /// </summary>
        [TestMethod]
        public void FindFindAllTest03()
        {
            LIST<EN> names = LIST<EN>.CreateList();
            names.NullFlavor = null;
            Assert.IsFalse(names.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Items       :   Members of the list
        /// And the following values are Nullified:
        ///     NullFlavor     
        /// </summary>
        [TestMethod]
        public void FindFindAllTest04()
        {
            LIST<EN> names = LIST<EN>.CreateList();
            names.NullFlavor = NullFlavor.Other;
            Assert.IsTrue(names.Validate());
        }
    }
}
