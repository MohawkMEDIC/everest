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
using MARC.Everest.RMIM.UV.NE2008.RCMR_MT000001UV02;

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for XMLITS1_Settings
    /// </summary>
    [TestClass]
    public class XMLITS1_SettingsTest
    {
        public XMLITS1_SettingsTest()
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
        /// Example 83
        /// Using the XML ITS 1.0 Formatter Settings.
        /// </summary>
        [TestMethod]
        public void XMLITS1_SettingsTest01()
        {
            try
            {
                // Create formatter and setup graph aides
                var formatter = new XmlIts1Formatter();
                formatter.GraphAides.Add(new DatatypeFormatter()
                {
                    CompatibilityMode = DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture
                });

                // Disable validation
                formatter.ValidateConformance = false;

                // Create settings
                formatter.Settings = SettingsType.UseReflectionFormat | SettingsType.AllowFlavorImposing;

                // If no exceptions, pass assertion
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail();
                Console.WriteLine(e);
            }
        }


        /// <summary>
        /// Using the XML ITS 1.0 Formatter Settings
        /// </summary>
        [TestMethod]
        public void XMLITS1_SettingsTest02()
        {
            try
            {
                // Create formatter and setup graph aides
                var formatter = new XmlIts1Formatter();
                formatter.GraphAides.Add(new DatatypeFormatter()
                {
                    CompatibilityMode = DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture
                });

                // Disable validation
                formatter.ValidateConformance = false;

                // Create Settings
                // Looks like all settings
                // can be assigned without issues.
                formatter.Settings = SettingsType.AllowFlavorImposing | SettingsType.AllowSupplierDomainImposing
                    | SettingsType.AllowUpdateModeImposing | SettingsType.DefaultLegacy
                    | SettingsType.DefaultMultiprocessor | SettingsType.DefaultUniprocessor
                    | SettingsType.EnableDeepLearning | SettingsType.UseGeneratorFormat
                    | SettingsType.UseReflectionFormat;

                // If no exceptions, pass assertion
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail();
                Console.WriteLine(e);
            }
        }
    }
}
