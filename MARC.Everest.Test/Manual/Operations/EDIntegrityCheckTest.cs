using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.Test.Manual.Operations
{
    /// <summary>
    /// Summary description for EDIntegrityCheck
    /// </summary>
    [TestClass]
    public class EDIntegrityCheckTest
    {
        public EDIntegrityCheckTest()
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

        /* Example 18 */


        /// <summary>
        /// Testing the integrity checking of an instance of ED.
        /// Integrity check should fail, resulting in the assertion returning TRUE.
        /// </summary>
        [TestMethod]
        public void IntegrityCheckTest01()
        {
            try
            {
                // create new instance of ED and store xml data
                ED edInstance = new ED(File.ReadAllBytes(@"C:\Test Files\mydata.xml"), "text/xml");

                // assign integrity checking algorithm
                edInstance.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA1;
                edInstance.IntegrityCheck = edInstance.ComputeIntegrityCheck();

                // Corrupt the data, validate integrity check should fail
                edInstance.Data[20] = 0;

                if (!edInstance.ValidateIntegrityCheck())
                    Console.WriteLine("Integrity check worked!");

                Assert.AreEqual(edInstance.ValidateIntegrityCheck(), false);
            }
            catch (Exception e)
            {
                // Don't handle
            }
        }


        /// <summary>
        /// Testing the integrity checking of an instance of ED.
        /// Integrity check should pass, resulting in the assertion returning TRUE.
        /// </summary>
        [TestMethod]
        public void IntegrityCheckTest02()
        {
            try
            {
                // create new instance of ED and store xml data
                ED edInstance = new ED(File.ReadAllBytes(@"C:\Test Files\mydata.xml"), "text/xml");

                // assign integrity checking algorithm
                edInstance.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA1;
                edInstance.IntegrityCheck = edInstance.ComputeIntegrityCheck();

                // Do NOT corrput the data

                if (edInstance.ValidateIntegrityCheck())
                {
                    Console.WriteLine("Integrity check found no corruption.");
                }
                else
                {
                    Console.WriteLine("Integrity check found corruption when data was not corrupted.");
                }

                Assert.AreEqual(edInstance.ValidateIntegrityCheck(), true);
            }
            catch (Exception e)
            {
                // Don't handle
            }
        }


        /// <summary>
        /// Testing the integrity checking of an instance of ED.
        /// Integrity check should fail.
        /// Compare Integrity Check results from SHA1 vs SHA256 and make sure they are different.
        /// Display all bytes in the Integrity Check.
        /// </summary>
        [TestMethod]
        public void IntegrityCheckTest03()
        {
            try
            {
                // Create first ED instance
                ED edInstance1 = new ED(File.ReadAllBytes(@"C:\Test Files\testData.txt"), "text/xml");
                edInstance1.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA1;
                edInstance1.IntegrityCheck = edInstance1.ComputeIntegrityCheck();

                // Create second ED instance
                ED edInstance2 = new ED(File.ReadAllBytes(@"C:\Test Files\testData.txt"), "text/xml");
                edInstance2.IntegrityCheckAlgorithm = EncapsulatedDataIntegrityAlgorithm.SHA256;
                edInstance2.IntegrityCheck = edInstance2.ComputeIntegrityCheck();


                // Corrput the data
                edInstance1.Data[10] = 0;
                edInstance2.Data[10] = 0;

                if (!edInstance1.ValidateIntegrityCheck() && !edInstance2.ValidateIntegrityCheck())
                {
                    Console.WriteLine("Integrity check worked!");
                    Console.WriteLine("Check 1:");
                    foreach (Byte bytes in edInstance1.IntegrityCheck)
                        Console.Write("{0}", bytes);
                    Console.WriteLine("");
                    Console.WriteLine("Check 2: ");
                    foreach (Byte bytes in edInstance2.IntegrityCheck)
                        Console.Write("{0}", bytes);
                }

                Assert.AreNotEqual(edInstance1.IntegrityCheck, edInstance2.IntegrityCheck);
            }
            catch (Exception e)
            {
                // Don't handle
            }
        }
    }
}
