using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.Xml;
using System.Xml;
using System.IO;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for IVLTest
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), TestClass]
    public class IVLTest
    {
        public IVLTest()
        {
            //
            // TODO: Add constructor logic here
            //

            for (INT i = 0; i < 10; i++)
                if (i % 2 == 1)
                    Console.WriteLine("{0} is odd", i);

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
        /// Ensures that validation succeeds when nullflavor, low, LowIncluded & HighIncluded are null;
        /// and high & width are populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLValidationNULLNullflavorLowLowIncludedHighIncludedPOPHighWidthTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.NullFlavor = null;
            ivl.Low = null;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.High = 10;
            ivl.Width = 5;
            Assert.IsTrue(ivl.Validate());
        }//Should validate
        
        /// <summary>
        /// Ensures that validation succeeds when nullflavor, high, LowIncluded & HighIncluded are null;
        /// and low & width are populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLValidationNULLNullflavorHighLowIncludedHighIncludedPOPLowWidthTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.NullFlavor = null;
            ivl.High = null;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.Low = 1;
            ivl.Width = 5;
            Assert.IsTrue(ivl.Validate());
        }//should validate
        
        /// <summary>
        /// Ensures that validation succeeds when nullflavor, low, LowIncluded, HighIncluded, high & width are null;
        /// and value is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), TestMethod]
        public void IVLValidationNULLNullflavorLowLowIncludedHighIncludedHighWidthPOPValueTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.NullFlavor = null;
            ivl.Low = null;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.High = null;
            ivl.Width = null;
            ivl.Value = 10;
            Assert.IsTrue(ivl.Validate());
        }

        /// <summary>
        /// Ensures that validation fails when nullflavor, low, high & width are null;
        /// and HighIncluded & low closed are populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLValidationNULLNullflavorLowHighWidthPOPHighIncludedLowIncludedTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.NullFlavor = null;
            ivl.Low = null;
            ivl.High = null;
            ivl.Width = null;
            ivl.HighClosed = true;
            ivl.LowClosed = true;
            Assert.IsFalse(ivl.Validate());
        }

        /// <summary>
        /// Ensures validation succeeds when nullflavor, width, LowIncluded & HighIncluded are null;\
        /// and low & high are populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLValidationNULLNullflavorWidthLowIncludedHighIncludedPOPLowHighTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.NullFlavor = null;
            ivl.Width = null;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.Low = 0;
            ivl.High = 5;
            Assert.IsTrue(ivl.Validate());
        }

        /// <summary>
        /// Ensures that validation fails when nullflavor, LowIncluded & HighIncluded are null;
        /// and low, high & width are populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLValidationNULLNullflavorLowIncludedHighIncludedPOPLowHighWidthTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.NullFlavor = null;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.Low = 0;
            ivl.High = 5;
            ivl.Width = 5;
            Assert.IsTrue(ivl.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds when low, LowIncluded, HighIncluded, high, width & value are null;
        /// and nullflavor is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLValidationNULLLowLowIncludedHighIncludedHighWidthValuePOPNullflavorTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.Low = null;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.High = null;
            ivl.Width = null;
            ivl.Value = null;
            ivl.NullFlavor = NullFlavor.NotAsked;
            Assert.IsTrue(ivl.Validate());
        }

        /// <summary>
        /// Ensures validation succeeds when LowIncluded, nullflavor & HighIncluded are null;
        /// and low, high, width & value are populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLValidationNULLNullflavorLowIncludedHighIncludedPOPLowHighWidthValueTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.Low = 0;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.High = 5;
            ivl.Width = 5;
            ivl.Value = 1;
            ivl.NullFlavor = null;
            Assert.IsTrue(ivl.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds when nullflavor, high, LowIncluded, HighIncluded & width are populated;
        /// and low is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLValidationNULLNullflavorHighLowIncludedHighIncludedWidthPOPLowTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.NullFlavor = null;
            ivl.High = null;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.Width = null;
            ivl.Low = 0;
            Assert.IsTrue(ivl.Validate());
        }

        /// <summary>
        /// Ensures that validation fails when high, LowIncluded, HighIncluded & width are null;
        /// and low & nullflavor is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "POP"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLValidationNULLHighLowIncludedHighIncludedWidthPOPNullflavorLowTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.High = null;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.Width = null;
            ivl.Low = 0;
            ivl.NullFlavor = NullFlavor.NotAsked;
            Assert.IsFalse(ivl.Validate());
        }

        /// <summary>
        /// Ensures that when low/high closed are supplied, the formatter outputs an inclusive attribute
        /// </summary>
        [TestMethod]
        public void IVLInclusiveFormattingTest()
        {
            IVL<INT> intIvl = new IVL<INT>(1, 4);
            intIvl.LowClosed = true;
            intIvl.HighClosed = false;
            MemoryStream ms = new MemoryStream();
            XmlStateWriter writer = new XmlStateWriter(XmlWriter.Create(ms));
            writer.WriteStartElement("ivl", "urn:hl7-org:v3");
            MARC.Everest.Formatters.XML.Datatypes.R1.DatatypeFormatter fmtr = new MARC.Everest.Formatters.XML.Datatypes.R1.DatatypeFormatter();
            fmtr.Graph(writer, intIvl);
            writer.Close();
            ms.Seek(0, SeekOrigin.Begin);
            XmlDocument d = new XmlDocument();
            d.Load(ms);
            Tracer.Trace(d.OuterXml);
            Assert.IsTrue(d.OuterXml.Contains("inclusive"));

        }

        /// <summary>
        /// Ensures that when an IVL has inclusive on low or high that the property is read into
        /// the low/high closed
        /// </summary>
        [TestMethod]
        public void IVLInclusiveParsingTest()
        {
            StringReader sr = new StringReader("<ivl xmlns=\"urn:hl7-org:v3\"><low inclusive=\"true\" value=\"1\" /><high inclusive=\"false\" value=\"4\" /></ivl>");
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            
            // Read to first node
            while(rdr.NodeType != XmlNodeType.Element)
                rdr.Read();

            // Parse
            MARC.Everest.Formatters.XML.Datatypes.R1.DatatypeFormatter fmtr = new MARC.Everest.Formatters.XML.Datatypes.R1.DatatypeFormatter();
            IVL<INT> retVal = fmtr.Parse(rdr, typeof(IVL<INT>)).Structure as IVL<INT>;
            Assert.IsTrue(retVal.HighClosed.HasValue);
            Assert.IsTrue(retVal.LowClosed.HasValue);
            Assert.IsFalse(retVal.HighClosed.Value);
            Assert.IsTrue(retVal.LowClosed.Value);
        }


        /// <summary>
        /// Ensure that validation fails when nullflavor, high, LowIncluded, HighIncluded & width are null;
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IVLNULL"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullflavor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "LowIncluded"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "HighIncluded"), TestMethod]
        public void IVLNULLNullflavorHighLowIncludedHighIncludedWidthValidationTest()
        {
            IVL<INT> ivl = new IVL<INT>();
            ivl.NullFlavor = null;
            ivl.High = null;
            ivl.LowClosed = null;
            ivl.HighClosed = null;
            ivl.Width = null;
            Assert.IsFalse(ivl.Validate());
        } 
    }
}
