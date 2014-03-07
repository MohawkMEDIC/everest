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
using MARC.Everest.Xml;
using System.Xml;

using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for RaiseEventCode
    /// </summary>
    [TestClass]
    public class RaiseEventCodeTest
    {
        public RaiseEventCodeTest()
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
        /*
        /// <summary>
        /// Example 79
        /// Sample raise event code.
        /// This manual example is theoretical.
        /// </summary>
        [TestMethod]
        public void RaiseAnatomyOnClick(Point diagramAnatomyPoint, Anatomy parentView)
        {
         /*   
            CD<String> primaryCode = new 
                CD<string>(cboPrimaryObservation.SelectedValue,
                 "2.16.840.1.113883.6.96", "SNOMED-CT", "200911", null,
                 cboPrimaryObservation.SelectedText);
            LIST<CR<String>> qualifier =
                parentView.CreateHL7Qualifier(diagramAnatomyPoint);
            primaryCode.Qualifier = qualifier;

            if (this.AnatomySelected != null)
            {
                this.AnatomySelected(this, new AnatomySelectionEventArgs()
                        {
                            primaryCode = primaryCode,
                            qualifier = qualifier
                        }
                );
            } // end if
         
        } // end test method */
    }
}
