using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Pipes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Interfaces;
using MARC.Everest.RMIM.UV.NE2008.COCT_MT050000UV01;

namespace MARC.Everest.Test.Manual_Interfaces
{
    /// <summary>
    /// Summary description for IIntereaction
    /// </summary>
    [TestClass]
    public class IInteractionTest
    {
        public IInteractionTest()
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

        /* Example 64 */

        /// <summary>
        /// This test will parse a valid instance of type 'IInteraction'.
        /// IsNotNull assertions should return TRUE.
        /// </summary>
        //[TestMethod]
        //public void IInteractionTest01()
        //{
        //    // Setup the formatter as XML ITS 1 with Canadian Data Types R1
        //    var formatter = new XmlIts1Formatter();
        //    formatter.ValidateConformance = false;
        //    formatter.GraphAides.Add(new DatatypeFormatter());

        //    // Parse an instance from disk in the file:
        //    IInteraction instance = formatter.ParseObject(File.OpenRead
        //        (@"C:/Test Files/PRPA_IN101103CA.xml")
        //        )
        //        as IInteraction;

        //    if (instance != null)   // Instance is an interaction
        //    {
        //        // Add all properties to a list and make sure aech
        //        // property in the list of properties is populated
        //        // to ensure for a valid IInteraction instance.
        //        List<Object> propertyList = new List<Object>();
        //        propertyList.Add(instance.Id);
        //        propertyList.Add(instance.InteractionId.Extension);
        //        propertyList.Add(instance.CreationTime);
        //        propertyList.Add(instance.ProcessingModeCode);
        //        propertyList.Add(instance.VersionCode);

        //        foreach (var property in propertyList)
        //            Assert.IsNotNull(propertyList);

        //        Console.WriteLine("Interaction ID: {0}\r\nCreated On: {1}\r\nProcessing Mode Code: {2}\r\nVersion: {3}",
        //            instance.InteractionId.Extension,
        //            instance.CreationTime,
        //            instance.ProcessingModeCode,
        //            instance.VersionCode
        //        );
        //    }
        //    else
        //    {
        //        // if instance is null, fail assertion
        //        Assert.Fail();
        //    }
        //}


        ///// <summary>
        ///// This test will parse a valid instance of type 'IInteraction',
        ///// and compare resulting properties to expected properties from Xml.
        ///// AreEqual assertions should pass, verifying that the properties
        ///// are being read by the formatter as expected.
        ///// </summary>
        //[TestMethod]
        //public void IInteractionTest02()
        //{
        //    // Setup the formatter as XML ITS 1 with Canadian Data Types R1
        //    var formatter = new XmlIts1Formatter();
        //    formatter.ValidateConformance = false;
        //    formatter.GraphAides.Add(new DatatypeFormatter());

        //    // Parse an instance from disk in the file:
        //    IInteraction instance = formatter.ParseObject(File.OpenRead
        //        (@"C:/Test Files/PRPA_IN101103CA.xml")
        //        )
        //        as IInteraction;

        //    if (instance != null)   // Instance is an interaction
        //    {
        //        Assert.AreEqual(instance.InteractionId.Extension.ToString(), "PRPA_IN101103CA");
        //        Assert.AreEqual(instance.ProcessingModeCode.ToString(), "T");
        //        Assert.AreEqual(instance.VersionCode.ToString(), "V3-2008N");

        //        Console.WriteLine("Interaction ID: {0}\r\nCreated On: {1}\r\nProcessing Mode Code: {2}\r\nVersion: {3}",
        //            instance.InteractionId.Extension,
        //            instance.CreationTime,
        //            instance.ProcessingModeCode,
        //            instance.VersionCode
        //        );
        //    }
        //    else
        //    {
        //        // if instance is null, fail assertion
        //        Assert.Fail();
        //    }
        //}



        ///// <summary>
        ///// This test will read an instance of type 'Patient'.
        ///// IsNull assertion should return TRUE (invalid instance of IInteraction).
        ///// </summary>
        //[TestMethod]
        //public void IInteractionTest03()
        //{
        //    // Setup the formatter as XML ITS 1 with Canadian Data Types R1
        //    var formatter = new XmlIts1Formatter();
        //    formatter.ValidateConformance = false;
        //    formatter.GraphAides.Add(new DatatypeFormatter());

        //    // Parse an instance from disk in the file:
        //    IInteraction instance = formatter.ParseObject(File.OpenRead
        //        (@"C:/Test Files/patientSample.xml")
        //        ) as IInteraction;

        //    if (instance != null)   // Instance is an interaction
        //    {
        //        Console.WriteLine("Instance is an interaction.");
        //    }
        //    else
        //    {
        //        // Verify that the instance was never created
        //        // (which would only happen if it was not a valid instance of IInteraction).
        //        Assert.IsNull(instance);
        //        Console.WriteLine("Instance is not valid IInteraction");
        //    }
        //}
    }
}
