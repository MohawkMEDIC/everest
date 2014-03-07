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
using System.Reflection;
using System.Xml;
using System.IO;

using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.RMIM.CA.R020402.Vocabulary;
using MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA;
using MARC.Everest.RMIM.CA.R020403.COCT_MT090302CA;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.CA.R020402.Interactions;


namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for CreateRequiredElemOutput
    /// </summary>
    [TestClass]
    public class CreateRequiredElemOutputTest
    {
        public CreateRequiredElemOutputTest()
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
        /// Example 85
        /// These tests will test if the XmlIts1Formatter will
        /// create the required elements when
        /// 
        /// Test 1: Instance Results in AcceptedNonConformant
        /// Test 2: Instance Results in Rejected
        /// Test 3: Instance Results in Accepted
        /// 
        /// Code inside the "/* STREAMWRITER CODE */" tags 
        /// can be uncommented so results are output to console.
        /// </summary>
        [TestMethod]
        public void CreateReqElemOutputTest01()
        {
            XmlWriter xw = null;
            Stream s = null;
            try{
          
                /* STREAMWRITER CODE 
                s = File.Create("C:/Test Files/example85Data.xml");
                StreamWriter sw1 = new StreamWriter(s);
                /* END STREAMWRITER CODE */


                // Setup the formatter
                var its1Formatter = new XmlIts1Formatter()
                {
                    ValidateConformance = false,
                    CreateRequiredElements = true
                };
                its1Formatter.GraphAides.Add(new DatatypeFormatter()
                    {
                        ValidateConformance = false
                    }
                    );

                // Initialize the XmlWriter & State writer
                StringWriter sw = new StringWriter();
                xw = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true });

                // State writer should always be used
                XmlStateWriter xsw = new XmlStateWriter(xw);

                // Create an instance
                REPC_IN000076CA instance = new REPC_IN000076CA(
                    Guid.NewGuid(),
                    DateTime.Now,
                    ResponseMode.Immediate,
                    REPC_IN000076CA.GetInteractionId(),
                    REPC_IN000076CA.GetProfileId(),
                    ProcessingID.Production,
                    AcknowledgementCondition.Always,
                    new MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA.Receiver(),
                    new MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA.Sender()
                );

                // Assign control act event (automatically populated with requirements)
                instance.controlActEvent = 
                    new MARC.Everest.RMIM.CA.R020402.MCAI_MT700210CA.ControlActEvent
                        <MARC.Everest.RMIM.CA.R020402.REPC_MT220001CA.Document>();

                // Format
                var result = its1Formatter.Graph(xsw, instance);
               
                /* STREAMWRITER CODE 
                var result2 = its1Formatter.Graph(s, instance);
                // Close streamwriter 
                sw1.Close();
                /* END STREAMWRITER CODE */
                
                // Flush the xml state writer
                xsw.Flush();

                if (result.Code != ResultCode.Accepted)
                {
                    Console.WriteLine("RESULT CODE: {0}", result.Code);
                    foreach (var detail in result.Details)
                    {
                        Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                    }
                }

                // Assert: Instance is accepted with errors.
                Assert.AreEqual(result.Code, ResultCode.AcceptedNonConformant);


                /* STREAMWRITER CODE 
                // Validate that the written file contains the two injected strings.
                StreamReader sr = new StreamReader("C:/Test Files/example85Data.xml");

                String xmlFromFile = "";
                // write everything in the stream to the console
                while (!sr.EndOfStream)
                {
                    // store lines into a string
                    xmlFromFile += sr.ReadLine();
                }

                // Write xml from the file onto the console
                Console.WriteLine(xmlFromFile);
                /* END STREAMWRITER CODE */
            }
            finally
            {
                if (xw != null)
                    xw.Close();
            }
        }// end test method



        /// <summary>
        /// Create required element output
        /// Instance Result is Rejected
        /// </summary>
        [TestMethod]
        public void CreateReqElemOutputTest02()
        {
            XmlWriter xw = null;
            Stream s = null;
            try
            {
                /* STREAMWRITER CODE 
                s = File.Create("C:/Test Files/example85Data2.xml");
                StreamWriter sw1 = new StreamWriter(s);

                /* END STREAMWRITER CODE */


                // Setup the formatter
                var its1Formatter = new XmlIts1Formatter()
                {
                    ValidateConformance = true,
                    CreateRequiredElements = true
                };
                its1Formatter.GraphAides.Add(new DatatypeFormatter()
                {
                    ValidateConformance = false
                }
                    );

                // Initialize the XmlWriter & State writer
                StringWriter sw = new StringWriter();
                xw = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true });

                // State writer should always be used
                XmlStateWriter xsw = new XmlStateWriter(xw);

                // Create an instance
                REPC_IN000076CA instance = new REPC_IN000076CA(
                    Guid.NewGuid(),
                    DateTime.Now,
                    ResponseMode.Immediate,
                    REPC_IN000076CA.GetInteractionId(),
                    REPC_IN000076CA.GetProfileId(),
                    ProcessingID.Production,
                    AcknowledgementCondition.Always,
                    new MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA.Receiver(),
                    new MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA.Sender()
                );
                instance.controlActEvent =
                    new MARC.Everest.RMIM.CA.R020402.MCAI_MT700210CA.ControlActEvent
                        <MARC.Everest.RMIM.CA.R020402.REPC_MT220001CA.Document>();

                // Format
                var result = its1Formatter.Graph(xsw, instance);
                
                /* STREAMWRITER CODE 
                var result2 = its1Formatter.Graph(s, instance);
                // Close streamwriter
                sw1.Close();
                /* END STREAMWRITER CODE */

                // Flush the xml state writer
                xsw.Flush();

                if (result.Code != ResultCode.Accepted)
                {
                    Console.WriteLine("RESULT CODE: {0}", result.Code);
                    foreach (var detail in result.Details)
                    {
                        Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                    }
                }
                 
                // Assert: Instance is not conformant.
                Assert.AreEqual(ResultCode.Rejected, result.Code);

                /* STREAMWRITER CODE 
                // Validate that the written file contains the two injected strings.
                StreamReader sr = new StreamReader("C:/Test Files/example85Data2.xml");

                String xmlFromFile = "";
                // write everything in the stream to the console
                while (!sr.EndOfStream)
                {
                    // store lines into a string
                    xmlFromFile += sr.ReadLine();
                }

                // Write xml from the file onto the console
                Console.WriteLine(xmlFromFile);
                /* END STREAMWRITER CODE */
            }
            finally
            {
                if (xw != null)
                    xw.Close();
            }
        }// end test method



        /// <summary>
        /// Create required element output
        /// Instance Result is Accepted
        /// </summary>
        [TestMethod]
        public void CreateReqElemOutputTest03()
        {
            /*
            XmlWriter xw = null;
            Stream s = null;
            try
            {
            
                /* STREAMWRITER CODE 
                s = File.Create("C:/Test Files/example85Data2.xml");
                StreamWriter sw1 = new StreamWriter(s);
                /* END STREAMWRITER CODE 


                // Setup the formatter
                var its1Formatter = new XmlIts1Formatter()
                {
                    ValidateConformance = false,
                    CreateRequiredElements = true
                };
                its1Formatter.GraphAides.Add(new DatatypeFormatter()
                {
                    ValidateConformance = false
                }
                    );

                // Initialize the XmlWriter & State writer
                StringWriter sw = new StringWriter();
                xw = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true });

                // State writer should always be used
                XmlStateWriter xsw = new XmlStateWriter(xw);

                // Create an instance
                REPC_IN000076CA instance = new REPC_IN000076CA(
                    Guid.NewGuid(),
                    DateTime.Now,
                    ResponseMode.Immediate,
                    REPC_IN000076CA.GetInteractionId(),
                    REPC_IN000076CA.GetProfileId(),
                    ProcessingID.Production,
                    AcknowledgementCondition.Always,
                    new MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA.Receiver(),
                    new MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA.Sender()
                );
                instance.controlActEvent =
                    new MARC.Everest.RMIM.CA.R020402.MCAI_MT700210CA.ControlActEvent
                        <MARC.Everest.RMIM.CA.R020402.REPC_MT220001CA.Document>();

                // Format
                var result = its1Formatter.Graph(xsw, instance);
                
                /* STREAMWRITER CODE 
                var result2 = its1Formatter.Graph(s, instance);
                // Close streamwriter
                sw1.Close();
                /* END STREAMWRITER CODE 

                // Flush the xml state writer
                xsw.Flush();

                if (result.Code != ResultCode.Accepted)
                {
                    Console.WriteLine("RESULT CODE: {0}", result.Code);
                    foreach (var detail in result.Details)
                    {
                        Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                    }
                }

                // Assert Instance is conformant and valid.
                Assert.AreEqual(result.Code, ResultCode.Accepted);


                /* STREAMWRITER CODE 
                // Validate that the written file contains the two injected strings.
                StreamReader sr = new StreamReader("C:/Test Files/example85Data2.xml");

                String xmlFromFile = "";
                // write everything in the stream to the console
                while (!sr.EndOfStream)
                {
                    // store lines into a string
                    xmlFromFile += sr.ReadLine();
                }

                // Write xml from the file onto the console
                Console.WriteLine(xmlFromFile);
                /* END STREAMWRITER CODE 
            }
            finally
            {
                if (xw != null)
                    xw.Close();
            }
           */
        }// end test method
    }   
}
