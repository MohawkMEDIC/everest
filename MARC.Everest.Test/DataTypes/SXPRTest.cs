using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using System.IO;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Xml;
using System.Xml;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Tests the set expression
    /// </summary>
    [TestClass]
    public class SXPRTest
    {
        public SXPRTest()
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
        /// Creating an SXPR of TS from an existing collection of 
        /// SXCM components.
        /// Test    :   Ensure that the number of items in the original list
        ///             is equal to the number of items in the set expression
        /// </summary>
        [TestMethod]
        public void SXPRTSExistingCollectionTest()
        {
            SXPR<TS> test = SXPR<TS>.CreateSXPR(new IVL<TS>(DateTime.Now),
                 new IVL<TS>(DateTime.Now.AddDays(1)));
            Assert.AreEqual(test.Count, 2);
        }

        /// <summary>
        /// Test that SXPRST With Mixed Components will be formatted properly
        /// Test    :   When formatted, components are output correctly
        /// </summary>
        [TestMethod]
        public void SXPRSTMixedComponentsFormatting()
        {

            SXPR<RTO<INT, INT>> test = SXPR<RTO<INT, INT>>.CreateSXPR(new IVL<RTO<INT,INT>>(new RTO<INT,INT>(1,3), new RTO<INT,INT>(2,3)),
                new PIVL<RTO<INT,INT>>(
                    new IVL<RTO<INT,INT>>(new RTO<INT,INT>(2,3),new RTO<INT,INT>(5,6)),
                    new PQ((decimal)1.0, "y")
                )
                //new SXCM<RTO<INT,INT>>(new RTO<INT,INT>(1,2)) { Operator = SetOperator.A },
                //new IVL<RTO<INT,INT>>(new RTO<INT,INT>(1,2)) { Operator = SetOperator.Intersect 
                );

            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter();
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw));
            xw.WriteStartElement("sxpr");
            fmtr.Graph(xw, test);
            xw.WriteEndElement(); // comp
            xw.Flush();
            Tracer.Trace(sw.ToString());
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            var parse = fmtr.Parse(rdr, typeof(SXPR<RTO<INT, INT>>)).Structure as SXPR<RTO<INT, INT>>;
            Assert.AreEqual(parse.Count, test.Count);
            for (int i = 0; i < parse.Count; i++)
                Assert.AreEqual(parse[i].GetType(), test[i].GetType());

        }

        /// <summary>
        /// Test that SXPRTS With Mixed Components will be formatted properly
        /// Test    :   When formatted, components are output correctly
        /// </summary>
        [TestMethod]
        public void SXPRTSMixedComponentsFormatting()
        {
            SXPR<TS> test = SXPR<TS>.CreateSXPR(new IVL<TS>(DateTime.Now, DateTime.Now.AddDays(1)),
                new PIVL<TS>(
                    new IVL<TS>(DateTime.Now, DateTime.Now.AddDays(1)),
                    new PQ((decimal)1.0, "y")
                ),
                //new SXCM<TS>(DateTime.Now) { Operator = SetOperator.A },
                new IVL<TS>(DateTime.Now) { Operator = SetOperator.Intersect },
                new EIVL<TS>(DomainTimingEventType.BeforeLunch,
                    new IVL<PQ>(
                        new PQ((decimal)1.0, "d")
                        )
                ) { Operator = SetOperator.Inclusive });

            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter();
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw));
            xw.WriteStartElement("sxpr");
            fmtr.Graph(xw, test);
            xw.WriteEndElement(); // comp
            xw.Flush();
            Tracer.Trace(sw.ToString());
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            var parse = fmtr.Parse(rdr, typeof(SXPR<TS>)).Structure as SXPR<TS>;
            Assert.AreEqual(parse.Count, test.Count);
            for (int i = 0; i < parse.Count; i++)
                Assert.AreEqual(parse[i].GetType(), test[i].GetType());

        }
    }
}
