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
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using MARC.Everest;
using MARC.Everest.Xml;
using MARC.Everest.DataTypes;

using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.NE2008;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.COCT_MT050000UV01;
using MARC.Everest.RMIM.UV.NE2008.COCT_MT030000UV04;
using MARC.Everest.Test.Manual.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MARC.Everest.Test.Manual.AdvancedTopics
{
    /// <summary>
    /// Summary description for ScaffoldingCode
    /// </summary>
    [TestClass]
    public class ScaffoldingCode
    {
        public ScaffoldingCode()
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
        ///  
        /// </summary>
        [TestMethod]
        public void FormatPatient()
        {
            // Create a formatter with a graph aide
            XmlIts1Formatter fmtr = new XmlIts1Formatter();

            // Gets or sets a list of aide formatters
            // that assist in the formatting of this instance
            fmtr.GraphAides.Add(new DatatypeFormatter());

            // Format to a string
            StringWriter sw = new StringWriter();
            XmlStateWriter xmlSw = new XmlStateWriter(
                XmlWriter.Create(sw, new XmlWriterSettings(){Indent = true})
                );

            // Call our method here...
            var patient = Utils.CreatePatient(
                Guid.NewGuid(),
                EN.CreateEN(EntityNameUse.Legal,
                    new ENXP ("John", EntityNamePartType.Given),
                    new ENXP ("Smith", EntityNamePartType.Family)
                    ),
                AD.CreateAD(PostalAddressUse.HomeAddress,
                    "123 Main Street West",
                    "Hamilton",
                    "Ontario",
                    "Canada"
                ),
                 "john.smith@mohawkcollege.ca"
                );

            // Format to the formatter.
            // Details results in a result code
            //  that indicates the outcome of the graph.
            var details = fmtr.Graph(xmlSw, patient);

            // Flush and close
            xmlSw.Close();

            // Output to console
            Console.WriteLine("XML: ");
            Console.WriteLine(sw.ToString());
            Console.WriteLine("Validation:");
            foreach (var dtl in details.Details) {
                Console.WriteLine("{0} : {1}", dtl.Type, dtl.Message);
            }
        }
    }
}
