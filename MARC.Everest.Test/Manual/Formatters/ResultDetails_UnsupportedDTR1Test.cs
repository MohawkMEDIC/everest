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
using System.IO;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MARC.Everest.RMIM.CA.R020402.Interactions;
using MARC.Everest.RMIM.CA.R020402.Vocabulary;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.RMIM.CA.R020403.REPC_MT500003CA;
using MARC.Everest.Test.Manual.Interfaces;
using System.Reflection;

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for ResultDetails_UnsupportedDTR1
    /// </summary>
    [TestClass]
    public class ResultDetails_UnsupportedDTR1Test
    {
        public ResultDetails_UnsupportedDTR1Test()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string[] GetResourceList()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceNames();
        }

        public static Stream GetResourceStream(string scriptname)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(scriptname);
        }

        public static string findResource(string targetResource)
        {
            // Find the resource to be parsed.
            string neededResource = "";
            foreach (string name in GetResourceList())
            {
                if (name.ToString().Contains(targetResource))
                {
                    neededResource = name;
                }
            }
            return neededResource;
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
        /// Example 86
        /// 
        /// Tests will be made for all datatypes.
        /// 
        /// Extracting the unsupported datatype propertiess from result details.
        /// Create and test different datatypes to see what detail information is returned.
        /// 
        /// Instance Tested:                    Patient
        /// Datatype Tested:                    AD
        /// 
        /// Expected Unsupported Properties:    ControlActExt
        ///                                     ControlActRoot
        ///                                     ValidTimeHigh
        ///                                     ValidTimeLow
        ///                                     Flavor
        ///                                     UpdateMode
        ///                                     
        /// Resutling Unsupported Properties:   ControlActExt
        ///                                     ControlActRoot
        ///                                     ValidTimeHigh
        ///                                     ValidTimeLow
        /// </summary>
        [TestMethod]
        public void ExtractingUnsupportedPropertiesTest01()
        {   
            XmlWriter xw = null;

            try
            {
                XmlIts1Formatter its1Formatter = new XmlIts1Formatter()
                {
                    ValidateConformance = false,
                    CreateRequiredElements = true
                };

                // Assign R1 graphing aide
                its1Formatter.GraphAides.Add(new DatatypeFormatter(){
                    ValidateConformance = true,
                    CompatibilityMode = DatatypeFormatterCompatibilityMode.Universal
                });

                // initialize the XmlWriter & State Writer
                StringWriter sw = new StringWriter();

                // Initialize XmlWriter and state writer
                xw = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true });
                XmlStateWriter xsw = new XmlStateWriter(xw);

                // Create and populate an instance of type 'Patient'
                var instance = Utils.CreatePatient(
                    Guid.NewGuid(),
                    new EN(),
                    new AD(PostalAddressUse.VacationHome,
                        new ADXP[] { }
                    )
                    {
                        // populate AD properties to see which are not supported
                        ControlActExt = "populated",
                        ControlActRoot = "populated",
                        Flavor = "AD.BASIC",
                        IsNotOrdered = true,
                        UpdateMode = new CS<UpdateMode>(),
                        Use = new SET<CS<PostalAddressUse>>(),
                        UseablePeriod = new GTS(),
                        ValidTimeHigh = new TS(),
                        ValidTimeLow = new TS(),
                        NullFlavor = null
                    },
                    new TEL()
                );
                
                // Format
                var result = its1Formatter.Graph(xsw, instance);

                // Iterate through the result details and get the unsupported details
                foreach (var dtl in result.Details)
                {
                    if (dtl is UnsupportedDatatypeR1PropertyResultDetail)
                    {
                        var unsupp = dtl as UnsupportedDatatypeR1PropertyResultDetail;
                        Console.WriteLine("The porperty '{0}' in datatype '{1}' was not formatted!",
                            unsupp.PropertyName, unsupp.DatatypeName);
                    }
                }
                // Flush the xml state writer
                xsw.Flush();
            }
            finally
            {
                if (xw != null)
                    xw.Close();
            }
        }// end test method
    }
}
