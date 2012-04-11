using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using System.Reflection;
using MARC.Everest.Interfaces;
using MARC.Everest.Xml;


using MARC.Everest.RMIM.UV.NE2008.COCT_MT050000UV01;
using MARC.Everest.RMIM.UV.NE2008.COCT_MT030000UV04;
using MARC.Everest.Test.Manual.Interfaces;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.RMIM.CA.R020402.Interactions;
using MARC.Everest.RMIM.CA.R020402.MCCI_MT002200CA;

namespace MARC.Everest.Test.Manual_Interfaces
{
    /// <summary>
    /// Summary description for IIdentifiable
    /// </summary>
    [TestClass]
    public class IIdentifiableTest
    {
        public IIdentifiableTest()
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

        
        /* Example 65 */
        /// <summary>
        /// Will continue this test.
        /// A function that will recursively print all identifiers in the message.
        /// </summary>
        /// <param name="g">The object to be scanned for identifiers</param>
        [TestMethod]
        public void IIdentifiableTest01()
        {
            /*
            MCCI_IN000000UV01 instance = new MCCI_IN000000UV01(
                    Guid.NewGuid(),
                    DateTime.Now,
                    MCCI_IN000000UV01.GetInteractionId(),
                    ProcessingID.Production,
                    "P",
                    AcknowledgementCondition.Always
                    );

            // dump patient into PrintIds to extract all Ids
            Utils.PrintIds(instance);
             * */
        }


        /// <summary>
        /// A function that will recursively print all identifiers in the message.
        /// </summary>
        /// <param name="g">The object to be scanned for identifiers</param>
        [TestMethod]
        public void IIdentifiableTest02()
        {
            /*
            //REPC_IN000076CA instance = new REPC_IN000076CA(
            MARC.Everest.RMIM.CA.R020402.Interactions.PRPA_IN101103CA query = new PRPA_IN101103CA(
                    Guid.NewGuid(),
                    DateTime.Now,
                    null,
                    null,
                    null,
                    null,
                    null
                    );
                    
            // dump patient into PrintIds to extract all Ids
            Utils.PrintIds(query);
            */
        }
        
    }
}
