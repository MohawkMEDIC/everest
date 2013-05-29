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
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.RMIM.UV.NE2008.RCMR_MT000001UV02;
using MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01;
using MARC.Everest.RMIM.UV.NE2008.QUQI_MT000001UV01;
using System.Reflection;

namespace MARC.Everest.Test.Manual.Formatters
{
    /// <summary>
    /// Summary description for ResultsDetails
    /// </summary>
    [TestClass]
    public class ResultsDetailsTest
    {
        public ResultsDetailsTest()
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

        /* Example 76 and 82 */
        /// <summary>
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// Valid Instance                      :   Yes
        /// Validate Conformance                :   true
        /// DatatypesR1 Validate Conformance    :   false
        /// ResultCode Expected                 :   Accepted
        /// 
        /// Expecting no errors.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest01()
        {
            // Flag used to verify if the graph result
            // contains details alongside its errors.
            bool noDetails = true;

            // Create a simple instance that PASSES ValidationConformance
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                Guid.NewGuid(),
                DateTime.Now,
                MCCI_IN000000UV01.GetInteractionId(),
                ProcessingID.Production,
                "P",
                AcknowledgementCondition.Never,
                null,
                new Sender(new Device(new SET<II>(Guid.NewGuid()))),
                null
            );

            // Create the Xml ITS1 formatter
            var formatter = new XmlIts1Formatter() { ValidateConformance = true };

            // Assign R1 graphing aide
            formatter.GraphAides.Add(new DatatypeFormatter() { ValidateConformance = false });

            // Graph the instance where instance is any interaction type
            var graphResult = formatter.Graph(Console.OpenStandardOutput(), instance);

            // Get the results.
            // If there are any errors, list the details,
            // otherwise, acknowledge that instance creted is valid.
            if (graphResult.Code != ResultCode.Accepted)
            {
                Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                foreach (var detail in graphResult.Details)
                {
                    Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                }
                noDetails = false;
            }
            else
            {
                Console.WriteLine("No errors. Instance created is a valid instance.");
            }

            // Assert that the returned instance is valid.
            Assert.AreEqual(graphResult.Code, ResultCode.Accepted);
            Assert.AreEqual(noDetails, true);

        } // end test method



        /// <summary>
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// Valid Instance                      :   Yes
        /// Validate Conformance                :   true
        /// DatatypesR1 Validate Conformance    :   true
        /// ResultCode Expected                 :   Accepted
        /// 
        /// Expecting no errors.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest02()
        {
            // Flag used to verify if the graph result
            // contains details alongside its errors.
            bool noDetails = true;

            // Create a simple instance that PASSES ValidationConformance
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                Guid.NewGuid(),
                DateTime.Now,
                MCCI_IN000000UV01.GetInteractionId(),
                ProcessingID.Production,
                "P",
                AcknowledgementCondition.Never,
                null,
                new Sender(new Device(new SET<II>(Guid.NewGuid()))),
                null
            );

            // Create the Xml ITS1 formatter
            var formatter = new XmlIts1Formatter() { ValidateConformance = true };

            // Assign R1 graphing aide
            formatter.GraphAides.Add(new DatatypeFormatter() { ValidateConformance = false });

            // Graph the instance where instance is any interaction type
            var graphResult = formatter.Graph(Console.OpenStandardOutput(), instance);

            // Get the results.
            // If there are any errors, list the details,
            // otherwise, acknowledge that instance creted is valid.
            if (graphResult.Code != ResultCode.Accepted)
            {
                Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                foreach (var detail in graphResult.Details)
                {
                    Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                }
                noDetails = false;
            }
            else
            {
                Console.WriteLine("No errors. Instance created is a valid instance.");
            }

            // Assert that the returned instance is valid.
            Assert.AreEqual(graphResult.Code, ResultCode.Accepted);
            Assert.AreEqual(noDetails, true);

        } // end test method



        /// <summary>
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// Valid Instance                      :   Yes
        /// Validate Conformance                :   false
        /// DatatypesR1 Validate Conformance    :   true
        /// ResultCode Expected                 :   Accepted
        /// 
        /// Expecting no errors.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest03()
        {
            // Flag used to verify if the graph result
            // contains details alongside its errors.
            bool noDetails = true;

            // Create a simple instance that PASSES ValidationConformance
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                Guid.NewGuid(),
                DateTime.Now,
                MCCI_IN000000UV01.GetInteractionId(),
                ProcessingID.Production,
                "P",
                AcknowledgementCondition.Never,
                null,
                new Sender(new Device(new SET<II>(Guid.NewGuid()))),
                null
            );

            // Create the Xml ITS1 formatter
            var formatter = new XmlIts1Formatter() { ValidateConformance = false };

            // Assign R1 graphing aide
            formatter.GraphAides.Add(new DatatypeFormatter() { ValidateConformance = true });

            // Graph the instance where instance is any interaction type
            var graphResult = formatter.Graph(Console.OpenStandardOutput(), instance);

            // Get the results.
            // If there are any errors, list the details,
            // otherwise, acknowledge that instance creted is valid.
            if (graphResult.Code != ResultCode.Accepted)
            {
                Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                foreach (var detail in graphResult.Details)
                {
                    Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                }
                noDetails = false;
            }
            else
            {
                Console.WriteLine("No errors. Instance created is a valid instance.");
            }

            // Assert that the returned instance is valid.
            Assert.AreEqual(graphResult.Code, ResultCode.Accepted);
            Assert.AreEqual(noDetails, true);

        } // end test method



        /// <summary>
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// Valid Instance                      :   Yes
        /// Validate Conformance                :   false
        /// DatatypesR1 Validate Conformance    :   false
        /// ResultCode Expected                 :   Accepted
        /// 
        /// Expecting no errors.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest04()
        {
            // Flag used to verify if the graph result
            // contains details alongside its errors.
            bool noDetails = true;

            // Create a simple instance that PASSES ValidationConformance
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                Guid.NewGuid(),
                DateTime.Now,
                MCCI_IN000000UV01.GetInteractionId(),
                ProcessingID.Production,
                "P",
                AcknowledgementCondition.Never,
                null,
                new Sender(new Device(new SET<II>(Guid.NewGuid()))),
                null
            );

            // Create the Xml ITS1 formatter
            var formatter = new XmlIts1Formatter() { ValidateConformance = false };

            // Assign R1 graphing aide
            formatter.GraphAides.Add(new DatatypeFormatter() { ValidateConformance = false });

            // Graph the instance where instance is any interaction type
            var graphResult = formatter.Graph(Console.OpenStandardOutput(), instance);

            // Get the results.
            // If there are any errors, list the details,
            // otherwise, acknowledge that instance creted is valid.
            if (graphResult.Code != ResultCode.Accepted)
            {
                Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                foreach (var detail in graphResult.Details)
                {
                    Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                }
                noDetails = false;
            }
            else
            {
                Console.WriteLine("No errors. Instance created is a valid instance.");
            }

            // Assert that the returned instance is valid.
            Assert.AreEqual(graphResult.Code, ResultCode.Accepted);
            Assert.AreEqual(noDetails, true);

        } // end test method
        
        
        
        
        /// <summary>
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// Valid Instance                      :   No
        /// Formatter Validate Conformance      :   true
        /// DatatypesR1 Validate Conformance    :   true
        /// ResultCode Expected                 :   Rejected
        /// 
        /// Expecting errors. Details should be printed.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest05()
        {
            bool noDetails = true;

            // Create a simple instance that FAILS ValidationConformance
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                Guid.NewGuid(),
                DateTime.Now,
                MCCI_IN000000UV01.GetInteractionId(),
                ProcessingID.Production,
                "P",
                AcknowledgementCondition.Never,
                null,
                null,
                null
            );

            // Create the Xml ITS1 formatter.
            var formatter = new XmlIts1Formatter() { ValidateConformance = true };

            // Assign R1 graphing aide
            formatter.GraphAides.Add(new DatatypeFormatter() { ValidateConformance = true });

            // Graph the instance where instance is any interaction type.
            var graphResult = formatter.Graph(Console.OpenStandardOutput(), instance);

            // Get the results.
            if (graphResult.Code != ResultCode.Accepted)
            {
                Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                foreach (var detail in graphResult.Details)
                {
                    Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                }
                noDetails = false;
            }
            else
            {
                Console.WriteLine("No errors. Instance created is a valid instance.");
            }

            // Assert:  Errors are found.
            Assert.AreEqual(noDetails, false);
            Assert.AreEqual(graphResult.Code, ResultCode.Rejected);
        } // end test method


        

        // <summary>
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// Valid Instance                      :   No
        /// Formatter Validate Conformance      :   true
        /// DatatypesR1 Validate Conformance    :   false
        /// ResultCode Expected                 :   Rejected
        /// 
        /// Expecting errors. Details should be printed.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest06()
        {
            bool noDetails = true;

            // Create a simple instance that FAILS ValidationConformance
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                Guid.NewGuid(),
                DateTime.Now,
                MCCI_IN000000UV01.GetInteractionId(),
                ProcessingID.Production,
                "P",
                AcknowledgementCondition.Never,
                null,
                null,
                null
            );

            // Create the Xml ITS1 formatter.
            var formatter = new XmlIts1Formatter() { ValidateConformance = true };

            // Assign R1 graphing aide
            formatter.GraphAides.Add(new DatatypeFormatter() { ValidateConformance = false });

            // Graph the instance where instance is any interaction type.
            var graphResult = formatter.Graph(Console.OpenStandardOutput(), instance);

            // Get the results.
            if (graphResult.Code != ResultCode.Accepted)
            {
                Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                foreach (var detail in graphResult.Details)
                {
                    Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                }
                noDetails = false;
            }
            else
            {
                Console.WriteLine("No errors. Instance created is a valid instance.");
            }

            // Assert:  Errors are found.
            Assert.AreEqual(noDetails, false);
            Assert.AreEqual(graphResult.Code, ResultCode.Rejected);
        } // end test method



        // <summary>
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// Valid Instance                      :   No
        /// Formatter Validate Conformance      :   false
        /// DatatypesR1 Validate Conformance    :   true
        /// ResultCode Expected                 :   Accepted
        /// 
        /// Expecting errors.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest07()
        {
            bool noDetails = true;

            // Create a simple instance that FAILS ValidationConformance
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                Guid.NewGuid(),
                DateTime.Now,
                MCCI_IN000000UV01.GetInteractionId(),
                ProcessingID.Production,
                "P",
                AcknowledgementCondition.Never,
                null,
                null,
                null
            );

            // Create the Xml ITS1 formatter.
            var formatter = new XmlIts1Formatter() { ValidateConformance = false };

            // Assign R1 graphing aide
            formatter.GraphAides.Add(new DatatypeFormatter() { ValidateConformance = true });

            // Graph the instance where instance is any interaction type.
            var graphResult = formatter.Graph(Console.OpenStandardOutput(), instance);

            // Get the results.
            if (graphResult.Code != ResultCode.Accepted)
            {
                Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                foreach (var detail in graphResult.Details)
                {
                    Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                }
                noDetails = false;
            }
            else
            {
                Console.WriteLine("No errors. Instance created is a valid instance.");
            }

            // Assert:  Errors are found.
            Assert.AreEqual(noDetails, true);
            Assert.AreEqual(graphResult.Code, ResultCode.Accepted);
        } // end test method



        // <summary>
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// Valid Instance                      :   No
        /// Formatter Validate Conformance      :   false
        /// DatatypesR1 Validate Conformance    :   false
        /// ResultCode Expected                 :   Accepted
        /// 
        /// Expecting errors.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest08()
        {
            bool noDetails = true;

            // Create a simple instance that FAILS ValidationConformance
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                Guid.NewGuid(),
                DateTime.Now,
                MCCI_IN000000UV01.GetInteractionId(),
                ProcessingID.Production,
                "P",
                AcknowledgementCondition.Never,
                null,
                null,
                null
            );

            // Create the Xml ITS1 formatter.
            var formatter = new XmlIts1Formatter() { ValidateConformance = false };

            // Assign R1 graphing aide
            formatter.GraphAides.Add(new DatatypeFormatter() { ValidateConformance = false });

            // Graph the instance where instance is any interaction type.
            var graphResult = formatter.Graph(Console.OpenStandardOutput(), instance);

            // Get the results.
            if (graphResult.Code != ResultCode.Accepted)
            {
                Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                foreach (var detail in graphResult.Details)
                {
                    Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                }
                noDetails = false;
            }
            else
            {
                Console.WriteLine("No errors. Instance created is a valid instance.");
            }

            // Assert:  Errors are found.
            Assert.AreEqual(noDetails, true);
            Assert.AreEqual(graphResult.Code, ResultCode.Accepted);
        } // end test method


 
        /// <summary> 
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// This test will parse an instance from a resource file.
        /// 
        /// Valid Instance                      :   Yes
        /// Formatter Validate Conformance      :   false
        /// ResultCode Expected                 :   Accepted
        /// 
        /// Expecting no errors.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest09()
        {
            bool noDetails = true;

            // Find the necessary resource
            string neededResource = findResource("MCCI_IN000000UV01.xml");

            // StreamReader is for outputting result to console
            Stream s = null;

            try
            {
                // Set the stream by reading from a file
                // whose datatype is MCCI_IN000000UV01
                s = GetResourceStream(neededResource);
                if (s == null)
                    Console.WriteLine("Invalid input stream.");

                // Setup the formatter
                var formatter = new XmlIts1Formatter()
                {
                    ValidateConformance = false
                };

                formatter.GraphAides.Add(new DatatypeFormatter() { 
                    ValidateConformance = false
                });

                // Parse and print result type.
                var graphResult = formatter.Parse(s);
                Console.WriteLine("Instance type: {0}", graphResult.Structure.GetType().Name);

                // Get the results.
                if (graphResult.Code != ResultCode.Accepted)
                {
                    Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                    foreach (var detail in graphResult.Details)
                    {
                        Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                    }
                    noDetails = false;
                }
                else
                {
                    Console.WriteLine("No errors. Instance created is a valid instance.");
                }

                // Make sure the parsed file gives us an instance of MCCI_IN000000UV01
                Assert.AreEqual(graphResult.Structure.GetType(), typeof(MCCI_IN000000UV01));

                // See if there are any errors in our results.
                Assert.AreEqual(noDetails, true);
                Assert.AreEqual(graphResult.Code, ResultCode.Accepted);
            }
            finally
            {
                if (s != null)
                    s.Close();
            } // end try-catch
        } // end test method


        /// <summary> 
        /// Get graphing result details using
        /// IFormatterGraphResult and iFormatterParseResult.
        /// 
        /// This test will parse an instance from a resource file.
        /// 
        /// Valid Instance                      :   Yes
        /// Formatter Validate Conformance      :   true
        /// ResultCode Expected                 :   Rejected
        /// 
        /// Expecting errors. Detaisl should be printed.
        /// Expecting AreEqual assertion to return TRUE.
        /// </summary>
        [TestMethod]
        public void displayGraphResultsTest10()
        {
            bool noDetails = true;

            // Find the necessary resource
            string neededResource = findResource("MCCI_IN000000UV01.xml");

            // StreamReader is for outputting result to console
            Stream s = null;

            try
            {
                // Set the stream by reading from a file
                // whose datatype is MCCI_IN000000UV01
                s = GetResourceStream(neededResource);
                if (s == null)
                    Console.WriteLine("Invalid input stream.");

                // Setup the formatter
                var formatter = new XmlIts1Formatter()
                {
                    ValidateConformance = true
                };

                formatter.GraphAides.Add(new DatatypeFormatter()
                {
                    ValidateConformance = false
                });

                // Parse and print result type.
                var graphResult = formatter.Parse(s);
                Console.WriteLine("Instance type: {0}", graphResult.Structure.GetType().Name);

                // Get the results.
                if (graphResult.Code != ResultCode.Accepted)
                {
                    Console.WriteLine("RESULT CODE: {0}", graphResult.Code);
                    foreach (var detail in graphResult.Details)
                    {
                        Console.WriteLine("{0}: {1}", detail.Type, detail.Message);
                    }
                    noDetails = false;
                }
                else
                {
                    Console.WriteLine("No errors. Instance created is a valid instance.");
                }

                // Make sure the parsed file gives us an instance of MCCI_IN000000UV01
                //Assert.AreEqual(graphResult.Structure.GetType(), typeof(MCCI_IN000000UV01));

                // See if there are any errors in our results.
                Assert.AreEqual(noDetails, false);
                Assert.AreEqual(graphResult.Code, ResultCode.Rejected);
            }
            finally
            {
                if (s != null)
                    s.Close();
            } // end try-catch
        } // end test method
    }
}
